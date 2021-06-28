Option Strict Off
Option Explicit On

Public Class ConstructSelect
	'**+Objective: Class that supports the table ConstructSelect
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase que le da soporte a la tabla ConstructSelect
	'+Version: $$Revision: 9 $
	
	'**-Objective: Usual Type that keep the Join relations structure between the  detail tables
	'-Objetivo: Tipo usuario que guarda la estructura de las relaciones Join entre las tablas
	Private Structure udtRelations
		Dim sNameTable As String
		Dim sAlias As String
		Dim eRelation As eRelationTables
		Dim sFilter As String
		Dim sMasterAliasTable As String
	End Structure
	
	'**-Objective: The enumerated type to contain the values of the possible relations between the child tables is declared
	'-Objetivo: Se declara el tipo numerado para contener los valores de las posibles relaciones entre las tablas hijas
	Public Enum eRelationTables
		RelLeft = 1
		RelRight = 2
		RelUnion = 3
		RelInner = 4
		RelFull = 5
	End Enum
	
	'**-Objective: The enumerated type that contains the values of the relations between two fields is declared
	'-Objetivo: Se delcara el tipo enumerado que contiene el valor de la relación entre dos campos
	Public Enum eWordConnection
		eAnd = 1
		eOr = 2
	End Enum
	
	'**-Objective: Defined type that contains the values to know what is going to be converted to a past value like parameter
	'-Objetivo: Tipo definido que contiene los valores para saber a qué se desea convertir un valor pasado como parámetro
	Public Enum eTypeValue
		TypCString = 1
		TypCNumeric = 2
		TypCDate = 3
	End Enum
	
	'**-Objective: Constant with the maximun number of element of the type user to contain relations between tables
	'-Objetivo: Constante con el número máximo de elementos del tipo usuario para contener relaciones entre tablas
	Private Const MAXRELATIONS As Short = 40
	
	'**-Objective: Will contain the SELECT clause
	'-Objetivo: Contendrá lo referente a la cláusula SELECT
	Private pstrSelectClause As String
	
	'**-Objective: Will contain the name of the master table of the operation
	'-Objetivo: Contendrá el nombre de la tabla padre de la operación
	Private pstrNameFather As String
	
	'**-Objective: Will contain the WHERE table
	'-Objetivo: Contendrá lo referente a la cláusula WHERE
	Private pstrWhere As String
	
	'**-Objective: Will contain the SELECT instruction
	'-Objetivo: Contendrá la instrucción SELECT armada y culminada
	Private pstrResult As String
	
	'**-Objective: Will contain the master tables to unite each one of them
	'-Objetivo: Contendrá el padre de las tablas para concatenárselo a c/u de ellas
	Private pstrOwner As String
	
	'**-Objective: Will Contain the database driver that we are working with to build the sentences according to this one
	'-Objetivo: Contendrá el tipo de manejador de BD con el que se está trabajando para construír las sentencias según éste
	Private pintTypeServer As Short
	
	'**-Objective: Will contain each relations between the tables
	'-Objetivo: Contendrá c/u de las relaciones entre las tablas
	Private pudtRelations(40) As udtRelations
	
	'**-Objective: Will contain the maximun number of relations between the tables included by the user
	'-Objetivo: Contendrá el máximo número de relaciones entre tablas incluído por el Usuario
	Private pintCountTables As Short
	
	'**-Objective: Will Contain the ALIAS of the master table to be used in the Join ( used in Oracle )
	'-Objetivo: Contendrá el ALIAS de la tabla padre para utilizarlo en los Join (uso en Oracle)
	Private pstrAliasFather As String
	
	'**-Objective: Will contain the switch value to execute the routine to fix the select clause chain for Oracle
	'-Objetivo: Contendrá el valor de switch para ejecutar o no la rutina de armar la cadena de la cláusula select para Oracle
	Private lblnEnter As Boolean
	
	'**-Objective: The variable that will contain the order by clause of the select is declare
	'-Objetivo: Se declara la variable que contendrá la cláusula order by del select
	Private lstrOrderBy As String
	
	'**%Objective: Controls the creation of an instance of the class
	'%Objetivo: Controla la creación de una instancia de la clase
	Private Sub Class_Initialize_Renamed()
		Dim lclsVisualTimeConfig As eRemoteDB.VisualTimeConfig
		Dim lintIndex As Short
		
		'**+Public variables are initiated to the form
		'+Se inicializan las variables públicas a la forma
		
		''On Error GoTo ErrorHandler
		pstrNameFather = String.Empty
		pstrSelectClause = String.Empty
		pstrWhere = " WHERE "
		pstrResult = String.Empty
		pintCountTables = 0
		lblnEnter = True
		For lintIndex = 0 To MAXRELATIONS
			pudtRelations(lintIndex).eRelation = eRelationTables.RelInner
			pudtRelations(lintIndex).sFilter = String.Empty
			pudtRelations(lintIndex).sAlias = String.Empty
			pudtRelations(lintIndex).sNameTable = String.Empty
			pudtRelations(lintIndex).sMasterAliasTable = String.Empty
		Next 
		
		
		
		lclsVisualTimeConfig = New eRemoteDB.VisualTimeConfig
		pstrOwner = lclsVisualTimeConfig.LoadSetting("Owner", String.Empty, "Database")
		If lclsVisualTimeConfig.LoadSetting("Server", String.Empty, "Database") = "Oracle" Then
			pintTypeServer = Connection.sTypeServer.sOracle
		Else
            pintTypeServer = Connection.sTypeServer.sSQLServer7
		End If
		lclsVisualTimeConfig = Nothing
		If pstrOwner <> String.Empty Then
			pstrOwner = pstrOwner & "."
		End If
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.Class_Initialize()")
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Increase the general count tables in the Select
	'%Objetivo: incrementa el contador general de tablas involucradas en el Select
	Private Sub IncreaseCountTables()
		''On Error GoTo ErrorHandler
		pintCountTables = pintCountTables + 1
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.IncreaseCountTables()")
	End Sub
	
	'**%Objective: assign the order by  clause value for the select statement
	'**%Parameters:
	'**%    OrderBy -
	'%Objetivo: toma el valor de la cláusula order by para colcársela al select
	'%Parámetros:
	'%      OrderBy -
	Public Sub OrderBy(ByVal OrderBy As String)
		''On Error GoTo ErrorHandler
		lstrOrderBy = OrderBy
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.OrderBy(OrderBy)", New Object(){OrderBy})
	End Sub
	
	'**%Objective: Taking the "Owner" value tables
	'**%Parameters:
	'**%    Owner         -
	'**%    TypeConection -
	'%Objetivo: Toma el valor del "Owner" de las tablas
	'%Parámetros:
	'%      Owner         -
	'%      TypeConection -
	Public Sub Owner(ByVal Owner As String, Optional ByVal TypeConection As Object = Nothing)
		''On Error GoTo ErrorHandler

		If Not IsNothing(TypeConection) Then
			pintTypeServer = TypeConection
		End If
		pstrOwner = Owner & "."
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.Owner(Owner,TypeConection)", New Object(){Owner, TypeConection})
	End Sub
	
	'**%Objective: assign the select clause value for the operation in curse
	'**%Parameters:
	'**%    SelectClause -
	'%Objetivo: Toma el valor de la cláusula select para la operación en curso
	'%Parámetros:
	'%      SelectClause -
	Public Sub SelectClause(ByVal SelectClause As String)
		''On Error GoTo ErrorHandler
		pstrSelectClause = SelectClause
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.SelectClause(SelectClause)", New Object(){SelectClause})
	End Sub
	
	'**%Objective: Gets the name of the master table with its respective alias
	'**%Parameters:
	'**%    NameFatherTable -
	'**%    AliasFather     -
	'%Objetivo: Toma el nombre de la tabla padre con su alias respectivo
	'%Parámetros:
	'%      NameFatherTable -
	'%      AliasFather     -
	Public Sub NameFatherTable(ByVal NameFatherTable As String, ByVal AliasFather As String)
		''On Error GoTo ErrorHandler
		pstrNameFather = NameFatherTable & " " & AliasFather
		pstrAliasFather = AliasFather
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.NameFatherTable(NameFatherTable,AliasFather)", New Object(){NameFatherTable, AliasFather})
	End Sub
	
	'**%Objective: Gets the value of the relations between the tables involved
	'**%Parameters:
	'**%    Relation      -
	'**%    Table         -
	'**%    Alias         -
	'**%    Filter        -
	'**%    WhoAliasTable -
	'%Objetivo: Toma el valor de las relaciones entre las tablas involucradas
	'%Parámetros:
	'%      Relation      -
	'%      Table         -
	'%      Alias         -
	'%      Filter        -
	'%      WhoAliasTable -
	Public Sub RelationsTables(ByRef Relation As eRelationTables, ByVal Table As String, ByVal Alias_Renamed As String, ByVal Filter_Renamed As String, Optional ByVal WhoAliasTable As String = "")
		''On Error GoTo ErrorHandler
		With pudtRelations(pintCountTables)
			.eRelation = Relation
			.sAlias = Alias_Renamed
			.sFilter = Filter_Renamed
			.sNameTable = Table
			.sMasterAliasTable = WhoAliasTable
		End With
		IncreaseCountTables()
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.RelationsTables(Relation,Table,Alias,Filter,WhoAliasTable)", New Object(){Relation, Table, Alias_Renamed, Filter_Renamed, WhoAliasTable})
	End Sub
	
	'**%Objective: Construct the where string of the Select sentence. Unite the field with
	'**%           the OR or AND and put, according the case, the connector between fields
	'**%Parameters:
	'**%    Field      -
	'**%    TypeValue  -
	'**%    ValueField -
	'**%    Conection  -
	'**%    BeforeWord -
	'**%    AfterWord  -
	'%Objetivo: Arma la cadena del where de la senetencia Select. Concatenca el campo con el OR o AND y coloca según sea el caso el conector entre campos
	'%Parámetros:
	'%      Field      -
	'%      TypeValue  -
	'%      ValueField -
	'%      Conection  -
	'%      BeforeWord -
	'%      AfterWord  -
	Public Function WhereClause(ByVal Field As String, ByVal TypeValue As eTypeValue, ByVal ValueField As String, Optional ByVal Conection As eWordConnection = 0, Optional ByVal BeforeWord As String = "", Optional ByVal AfterWord As String = "") As Boolean
		Dim lintPosChar As Short
		
		''On Error GoTo ErrorHandler
		If lblnEnter Then
			If InStr(1, pstrWhere, "WHERE  AND") <> 0 Then
				lintPosChar = InStr(1, pstrWhere, " AND")
				If Len(Trim(pstrWhere)) = Len("WHERE AND") Then
					pstrWhere = Mid(pstrWhere, 1, lintPosChar - 1)
				Else
					pstrWhere = Mid(pstrWhere, 1, lintPosChar - 1) & Mid(pstrWhere, lintPosChar + 4)
				End If
			End If
			lblnEnter = False
		End If
		WhereClause = insValField(BeforeWord, AfterWord, Field, TypeValue, ValueField, Conection)
		RemoveChar()
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.WhereClause(Field,TypeValue,ValueField,Conection,BeforeWord,AfterWord)", New Object(){Field, TypeValue, ValueField, Conection, BeforeWord, AfterWord})
	End Function
	
	'**%Objective: Remove the wrong concatenation of two "And" because of putting the condition
	'**%           of the join in the Where
	'%Objetivo: remueve la concatenación errada de dos "And" a causa de haber colocado la condición del
	'%          Join en el Where (sólo aplica en Oracle, SP1)
	Private Sub RemoveChar()
		Dim lintPosChar As Short
		
		''On Error GoTo ErrorHandler
		lintPosChar = InStr(1, pstrWhere, "AND  AND")
		If lintPosChar = 0 Then
			lintPosChar = InStr(1, pstrWhere, "AND AND")
		End If
		Do While lintPosChar > 0
			pstrWhere = Mid(pstrWhere, 1, lintPosChar - 1) & Mid(pstrWhere, lintPosChar + 4)
			lintPosChar = InStr(lintPosChar, pstrWhere, "AND  AND")
			If lintPosChar = 0 Then
				lintPosChar = InStr(1, pstrWhere, "AND AND")
			End If
		Loop 
		If Trim(Mid(pstrWhere, Len(pstrWhere) - 4, 4)) = "AND" Then
			pstrWhere = Mid(pstrWhere, 1, Len(pstrWhere) - 4)
		End If
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.RemoveChar()")
	End Sub
	
	'**%Objective: Constructs the select calling each one of the parts of the routine building part by part
	'%Objetivo: construye el select invocando c/u de las partes de la rutina armando parte a parte
	Private Function ConstrucSelect() As String
		''On Error GoTo ErrorHandler
		ConstrucSelect = String.Empty
		pstrResult = insConsFather
		If pintTypeServer = Connection.sTypeServer.sOracle Then
			pstrResult = pstrResult & insValEnviromentSelect
		Else
			pstrResult = pstrResult & insValEnviromentRelations
		End If
		If Trim(pstrWhere) <> "WHERE" Then
			pstrResult = pstrResult & pstrWhere
		End If
		
		If pintTypeServer = Connection.sTypeServer.sOracle Then
			pstrResult = pstrResult & insConstOracleWhere_1
		End If
		
		pstrResult = pstrResult & lstrOrderBy
		insValEnviromentResult()
		
		pstrResult = insReplaceMacros(pstrResult, "$DATE", pstrOwner & "DTCHAR")
		pstrResult = insReplaceMacros(pstrResult, "$CHAR", "TO_CHAR")
		pstrResult = insReplaceMacros(pstrResult, "'XXX'", String.Empty)
		
		ConstrucSelect = pstrResult
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.ConstrucSelect()")
	End Function
	
	'**%Objective: Deletes the Join symbol, if the case is Oracle, when it is placed between parenthesis with an "OR"
	'%Objetivo: quita el símbolo de Join si el caso es Oracle cuando se encuentre entre paréntesis con un "OR"
	Private Sub insValEnviromentResult()
		''On Error GoTo ErrorHandler
		
		insDelSymbol()
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.insValEnviromentResult()")
	End Sub
	
	'**%Objective: Constructs the select clause with the master table
	'%Objetivo: construye la cláusula select con la tabla padre
	Private Function insConsFather() As String
		''On Error GoTo ErrorHandler
		insConsFather = "SELECT " & pstrSelectClause & " FROM " & pstrOwner & pstrNameFather
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insConsFather()")
	End Function
	
	'**%Objective: Validation that builds the rest of the select clause in case of an Oracle enviroment
	'%Objetivo: valida que se construya el resto de la cláusula select en caso de ser ambiente Oracle
	Private Function insValEnviromentSelect() As String
		''On Error GoTo ErrorHandler
		
		insValEnviromentSelect = insConstOraSelect
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insValEnviromentSelect()")
	End Function
	
	'**%Objective: Completes the select clause if it is Oracle
	'%Objetivo: completa la cláusula select si es Oracle
	Private Function insConstOraSelect() As String
		Dim lintIndex As Short
		
		''On Error GoTo ErrorHandler
		insConstOraSelect = String.Empty
		For lintIndex = 0 To pintCountTables - 1
			insConstOraSelect = insConstOraSelect & ", " & pstrOwner & pudtRelations(lintIndex).sNameTable & " " & pudtRelations(lintIndex).sAlias
		Next lintIndex
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insConstOraSelect()")
	End Function
	
	'**%Objective: Validates the work enviroment to construct the relations between tables
	'%Objetivo: Valida el ambiente de trabajo para construír las relaciones entre las tablas
	Private Function insValEnviromentRelations() As String
		''On Error GoTo ErrorHandler
		
		insValEnviromentRelations = insConstSQLRelations
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insValEnviromentRelations()")
	End Function
	
	'**%Objective: Constructs the relations between tables according to the case
	'%Objetivo: Construye las relaciones entre las tablas según sea el caso
	Private Function insConstSQLRelations() As String
		Dim lintIndex As Short
		
		''On Error GoTo ErrorHandler
		insConstSQLRelations = String.Empty
		For lintIndex = 0 To pintCountTables - 1
			insConstSQLRelations = insConstSQLRelations & " "
			Select Case pudtRelations(lintIndex).eRelation
				Case eRelationTables.RelInner
					insConstSQLRelations = insConstSQLRelations & "INNER JOIN " & pstrOwner & pudtRelations(lintIndex).sNameTable & " " & pudtRelations(lintIndex).sAlias & " ON " & pudtRelations(lintIndex).sFilter
				Case eRelationTables.RelLeft
					insConstSQLRelations = insConstSQLRelations & "LEFT JOIN " & pstrOwner & pudtRelations(lintIndex).sNameTable & " " & pudtRelations(lintIndex).sAlias & " ON " & pudtRelations(lintIndex).sFilter
				Case eRelationTables.RelRight
					insConstSQLRelations = insConstSQLRelations & "RIGHT JOIN " & pstrOwner & pudtRelations(lintIndex).sNameTable & " " & pudtRelations(lintIndex).sAlias & " ON " & pudtRelations(lintIndex).sFilter
				Case eRelationTables.RelFull
					insConstSQLRelations = insConstSQLRelations & "FULL JOIN " & pstrOwner & pudtRelations(lintIndex).sNameTable & " " & pudtRelations(lintIndex).sAlias & " ON " & pudtRelations(lintIndex).sFilter
				Case eRelationTables.RelUnion
			End Select
		Next 
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insConstSQLRelations()")
	End Function
	
	'**%Objective: It builds the first part of the "WHERE" with the relations between the tables if it is Oracle
	'%Objetivo: va contruyendo la primera parte del where si es Oracle con las relaciones entre las tablas
	Private Function insConstOracleWhere_1() As String
		Dim lintIndex As Short
		Dim lintPosAlias As Short
		Dim lintPosCharac As Short
		Dim lstrTemp As String
		
		''On Error GoTo ErrorHandler
		insConstOracleWhere_1 = String.Empty
		
		For lintIndex = 0 To pintCountTables - 1
			lstrTemp = " "
			lintPosCharac = 0
			pudtRelations(lintIndex).sFilter = insReplaceMacros(pudtRelations(lintIndex).sFilter, "$DATE", "DTCHAR")
			Select Case pudtRelations(lintIndex).eRelation
				Case eRelationTables.RelInner
					lstrTemp = pudtRelations(lintIndex).sFilter
				Case eRelationTables.RelLeft
					If pudtRelations(lintIndex).sMasterAliasTable <> String.Empty Then
						lintPosAlias = InStr(1, pudtRelations(lintIndex).sFilter, pudtRelations(lintIndex).sMasterAliasTable & ".")
					Else
						lintPosAlias = InStr(1, pudtRelations(lintIndex).sFilter, pudtRelations(lintIndex).sAlias & ".")
					End If
					Do While lintPosAlias > 0
						lintPosCharac = 0
						If InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, ")") <> 0 And InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, " ") <> 0 Then
							If InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, ")") < InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, " ") Then
								lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, ")")
							Else
								lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, " ")
							End If
						End If
						If lintPosCharac = 0 Then
							lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, ")")
						End If
						If lintPosCharac = 0 Then
							lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, " ")
						End If
						If lintPosCharac = 0 Then
							lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, "=")
						End If
						If lintPosCharac = 0 Then
							lintPosCharac = Len(pudtRelations(lintIndex).sFilter)
							lstrTemp = Mid(pudtRelations(lintIndex).sFilter, 1, lintPosCharac) & " (+) " 'IIf(pintTypeServer = sOracle, " (+) ", String.Empty)
						Else
							lstrTemp = Mid(pudtRelations(lintIndex).sFilter, 1, lintPosCharac - 1) & " (+) " 'IIf(pintTypeServer = sOracle, " (+) ", String.Empty) & Mid$(pudtRelations(lintIndex).sFilter, lintPosCharac)
						End If
						pudtRelations(lintIndex).sFilter = lstrTemp
						lintPosAlias = InStr(lintPosCharac, pudtRelations(lintIndex).sFilter, pudtRelations(lintIndex).sAlias & ".")
					Loop 
				Case eRelationTables.RelRight
					If pudtRelations(lintIndex).sMasterAliasTable <> String.Empty Then
						lintPosAlias = InStr(1, pudtRelations(lintIndex).sFilter, pudtRelations(lintIndex).sMasterAliasTable & ".")
					Else
						lintPosAlias = InStr(1, pudtRelations(lintIndex).sFilter, pstrAliasFather & ".")
					End If
					Do While lintPosAlias > 0
						lintPosCharac = 0
						If InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, ")") <> 0 And InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, " ") <> 0 Then
							If InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, ")") < InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, " ") Then
								lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, ")")
							Else
								lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, " ")
							End If
						End If
						If lintPosCharac = 0 Then
							lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, ")")
						End If
						If lintPosCharac = 0 Then
							lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, " ")
						End If
						If lintPosCharac = 0 Then
							lintPosCharac = InStr(lintPosAlias, pudtRelations(lintIndex).sFilter, "=")
						End If
						If lintPosCharac = 0 Then
							lintPosCharac = Len(pudtRelations(lintIndex).sFilter)
							lstrTemp = Mid(pudtRelations(lintIndex).sFilter, 1, lintPosCharac) & " (+) " 'IIf(pintTypeServer = sOracle, " (+) ", String.Empty)
						Else
							lstrTemp = Mid(pudtRelations(lintIndex).sFilter, 1, lintPosCharac - 1) & " (+) " 'IIf(pintTypeServer = sOracle, " (+) ", String.Empty) & Mid$(pudtRelations(lintIndex).sFilter, lintPosCharac)
						End If
						pudtRelations(lintIndex).sFilter = lstrTemp
						lintPosAlias = InStr(lintPosCharac, pudtRelations(lintIndex).sFilter, pudtRelations(lintIndex).sAlias & ".")
					Loop 
				Case eRelationTables.RelFull
					lstrTemp = lstrTemp & lstrTemp
				Case eRelationTables.RelUnion
			End Select
			If insConstOracleWhere_1 = String.Empty Or insConstOracleWhere_1 = " " Then
				insConstOracleWhere_1 = " AND " & lstrTemp
			Else
				insConstOracleWhere_1 = insConstOracleWhere_1 & " AND " & lstrTemp
			End If
		Next 
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insConstOracleWhere_1()")
	End Function
	
	'**%Objective: Fix the complete string concatenating the different parts of the "SELECT"
	'%Objetivo: arma la cadena completa concatenándo las diferentes partes del SELECT
	Public Function Answer() As String
		''On Error GoTo ErrorHandler
		Answer = ConstrucSelect
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.Answer()")
	End Function
	
	'**%Objective: Calls the validation functions according to the value that was passed as parameter
	'**%Parameters:
	'**%    BeforeWord     -
	'**%    AfterWord      -
	'**%    lstrField      -
	'**%    eTypeValues    -
	'**%    lstrValueField -
	'**%    Conection      -
	'%Objetivo: llama a las funciones de validación según el tipo de valor que se pase como parámetro
	'%Parámetros:
	'%      BeforeWord     -
	'%      AfterWord      -
	'%      lstrField      -
	'%      eTypeValues    -
	'%      lstrValueField -
	'%      Conection      -
	Private Function insValField(ByVal BeforeWord As String, ByVal AfterWord As String, ByVal lstrField As String, ByVal eTypeValues As eTypeValue, ByVal lstrValueField As String, Optional ByVal Conection As eWordConnection = 0) As Boolean
		''On Error GoTo ErrorHandler
		
		Select Case eTypeValues
			'**+If the Data type is numerical
			'+Si el tipo de dato es Numérico
			
			Case eTypeValue.TypCNumeric
				insValField = insValNumeric(BeforeWord, AfterWord, lstrValueField, lstrField, Conection)
				'**+If the data type is date
				'+Si el tipo de dato es Fecha
				
			Case eTypeValue.TypCDate
				insValField = insValDate(BeforeWord, AfterWord, lstrValueField, lstrField, Conection)
				'**+If the data type is string
				'+Si el tipo de dato es Cadena
				
			Case eTypeValue.TypCString
				insValField = insValString(BeforeWord, AfterWord, lstrValueField, lstrField, Conection)
		End Select
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insValField(BeforeWord,AfterWord,lstrField,eTypeValues,lstrValueField,Conection)", New Object(){BeforeWord, AfterWord, lstrField, eTypeValues, lstrValueField, Conection})
	End Function
	
	'**%Objective: Validates the numeric expression with it's operands to build the "WHERE"
	'**%Parameters:
	'**%    BeforeWord     -
	'**%    AfterWord      -
	'**%    lstrValueField -
	'**%    lstrField      -
	'**%    Conection      -
	'%Objetivo: valida la expresión numérica con sus operandos para armar el where
	'%Parámetros:
	'%      BeforeWord     -
	'%      AfterWord      -
	'%      lstrValueField -
	'%      lstrField      -
	'%      Conection      -
	Private Function insValNumeric(ByVal BeforeWord As String, ByVal AfterWord As String, ByVal lstrValueField As String, ByVal lstrField As String, Optional ByVal Conection As eWordConnection = 0) As Boolean
		Dim lintIniPos As Short
		Dim lblnBetween As Boolean
		
		''On Error GoTo ErrorHandler
		lintIniPos = 0
		lstrValueField = insValFormat(lstrValueField)
		If Mid(lstrValueField, 1, 1) = ">" Or Mid(lstrValueField, 1, 1) = "<" Or Mid(lstrValueField, 1, 1) = "=" Then
			If Mid(lstrValueField, 1, 1) <> "=" Then
				If Mid(lstrValueField, 2, 1) = "=" Or (Mid(lstrValueField, 1, 1) = "<" And Mid(lstrValueField, 2, 1) = ">") Then
					lintIniPos = 2
				Else
					lintIniPos = 1
				End If
			Else
				lintIniPos = 1
			End If
			If IsNumeric(Mid(lstrValueField, 1 + lintIniPos)) Then
				pstrWhere = pstrWhere & insConstructNumeric(BeforeWord, AfterWord, Mid(lstrValueField, 1 + lintIniPos), lstrField, Mid(lstrValueField, 1, lintIniPos),  , Conection)
				insValNumeric = True
			End If
		Else
			lblnBetween = False
			For lintIniPos = 1 To Len(lstrValueField)
				If Mid(lstrValueField, lintIniPos, 1) = ":" Then
					lblnBetween = True
					Exit For
				End If
			Next lintIniPos
			If lblnBetween Then
				If IsNumeric(Mid(lstrValueField, 1, lintIniPos - 1)) And IsNumeric(Mid(lstrValueField, lintIniPos + 1)) Then
					pstrWhere = pstrWhere & insConstructNumeric(BeforeWord, AfterWord, Mid(lstrValueField, 1, lintIniPos - 1), lstrField, Mid(lstrValueField, lintIniPos, lintIniPos - 4), Mid(lstrValueField, lintIniPos + 1), Conection)
					insValNumeric = True
				End If
			Else
				If IsNumeric(lstrValueField) Then
					pstrWhere = pstrWhere & insConstructNumeric(BeforeWord, AfterWord, lstrValueField, lstrField, "=",  , Conection)
					insValNumeric = True
				End If
			End If
		End If
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insValNumeric(BeforeWord,AfterWord,lstrValueField,lstrField,Conection)", New Object(){BeforeWord, AfterWord, lstrValueField, lstrField, Conection})
	End Function
	
	'**%Objective: Validates the field passed as a parameter to the "WHERE" in case that it is the date to fix it
	'**%Parameters:
	'**%    BeforeWord     -
	'**%    AfterWord      -
	'**%    lstrValueField -
	'**%    lstrField      -
	'**%    Conection      -
	'%Objetivo: valida el campo pasado como parámetro al where en el caso de que sea fecha para agregarlo al mismo
	'%Parámetros:
	'%      BeforeWord     -
	'%      AfterWord      -
	'%      lstrValueField -
	'%      lstrField      -
	'%      Conection      -
	Private Function insValDate(ByVal BeforeWord As String, ByVal AfterWord As String, ByVal lstrValueField As String, ByVal lstrField As String, Optional ByVal Conection As eWordConnection = 0) As Boolean
		Dim lintIniPos As Short
		Dim lstrTemp As String
		
		''On Error GoTo ErrorHandler
		lintIniPos = 0
		If Mid(lstrValueField, 1, 1) = ">" Or Mid(lstrValueField, 1, 1) = "<" Or Mid(lstrValueField, 1, 1) = "=" Then
			If Mid(lstrValueField, 1, 1) <> "=" Then
				If Mid(lstrValueField, 2, 1) = "=" Or (Mid(lstrValueField, 1, 1) = "<" And Mid(lstrValueField, 2, 1) = ">") Then
					lintIniPos = 2
				Else
					lintIniPos = 1
				End If
			Else
				lintIniPos = 1
			End If
			
			If IsDate(Mid(lstrValueField, 1 + lintIniPos + 2, 10)) Then
				pstrWhere = pstrWhere & insConstructDate(BeforeWord, AfterWord, "'" & Trim(Mid(lstrValueField, 1 + lintIniPos, 10)) & Trim(Mid(lstrValueField, 1 + lintIniPos + 2 + 10)) & "'", lstrField, Mid(lstrValueField, 1, lintIniPos), Conection)
				insValDate = True
			Else
				If IsDate(CStr(Mid(lstrValueField, 1 + lintIniPos, 10))) Then
					lstrTemp = "'" & Mid(lstrValueField, 1 + lintIniPos, 10) & "'"
					pstrWhere = pstrWhere & insConstructDate(BeforeWord, AfterWord, lstrTemp, lstrField, Mid(lstrValueField, 1, lintIniPos), Conection)
					insValDate = True
				End If
			End If
			
		Else
			If IsDate(lstrValueField) Then
                pstrWhere = pstrWhere & insConstructDate(BeforeWord, AfterWord, "'" & Convert.ToDateTime(lstrValueField.Trim()).ToString("yyyyMMdd") & "'", lstrField, "=", Conection)
				insValDate = True
			End If
		End If
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insValDate(BeforeWord,AfterWord,lstrValueField,lstrField,Conection)", New Object(){BeforeWord, AfterWord, lstrValueField, lstrField, Conection})
	End Function
	
	'**%Objective: Validates the parameter passed as a parameter if the case is string to fix the "WHERE"
	'**%Parameters:
	'**%    BeforeWord     -
	'**%    AfterWord      -
	'**%    lstrValueField -
	'**%    lstrField      -
	'**%    Conection      -
	'%Objetivo: valida el parámetro pasado como parámetro si el caso es string para agregarlo al where
	'%Parámetros:
	'%      BeforeWord     -
	'%      AfterWord      -
	'%      lstrValueField -
	'%      lstrField      -
	'%      Conection      -
	Private Function insValString(ByVal BeforeWord As String, ByVal AfterWord As String, ByVal lstrValueField As String, ByVal lstrField As String, Optional ByVal Conection As eWordConnection = 0) As Boolean
		Dim lintIniPos As Short
		
		''On Error GoTo ErrorHandler
		lintIniPos = 0
		
		If Mid(lstrValueField, 1, 1) = ">" Or Mid(lstrValueField, 1, 1) = "<" Or Mid(lstrValueField, 1, 1) = "=" Then
			If Mid(lstrValueField, 1, 1) <> "=" Then
				If Mid(lstrValueField, 2, 1) = "=" Or (Mid(lstrValueField, 1, 1) = "<" And Mid(lstrValueField, 2, 1) = ">") Then
					lintIniPos = 2
				Else
					lintIniPos = 1
				End If
			Else
				lintIniPos = 1
			End If
			pstrWhere = pstrWhere & insConstructString(BeforeWord, AfterWord, Mid(lstrValueField, 1 + lintIniPos), lstrField, Mid(lstrValueField, 1, lintIniPos), Conection)
			insValString = True
		Else
			If UCase(Mid(lstrValueField, 1, 2)) = " IN " Then
				lintIniPos = 2
				pstrWhere = pstrWhere & insConstructString(BeforeWord, AfterWord, Mid(lstrValueField, 1 + lintIniPos), lstrField, Mid(lstrValueField, 1, lintIniPos), Conection)
			Else
				pstrWhere = pstrWhere & insConstructString(BeforeWord, AfterWord, lstrValueField, lstrField,  , Conection)
				insValString = True
			End If
		End If
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insValString(BeforeWord,AfterWord,lstrValueField,lstrField,Conection)", New Object(){BeforeWord, AfterWord, lstrValueField, lstrField, Conection})
	End Function
	
	'**%Objective: Validates and formats to an numeric expression that contains periods or comas, taking it to a simple chain without these
	'**%Parameters:
	'**%    lstrValue -
	'%Objetivo: valida y da formato a una expresión numérica que contenga puntos o comas, llevándola a una cadena sencilla sin estos.
	'%Parámetros:
	'%      lstrValue -
	Private Function insValFormat(ByVal lstrValue As String) As String
		Dim llngSubscript As Integer
		
		''On Error GoTo ErrorHandler
		llngSubscript = InStr(1, lstrValue, ".")
		Do While llngSubscript > 0
			lstrValue = Mid(lstrValue, 1, llngSubscript - 1) & Mid(lstrValue, llngSubscript + 1)
			llngSubscript = InStr(1, lstrValue, ".")
		Loop 
		llngSubscript = InStr(1, lstrValue, ",")
		Do While llngSubscript > 0
			lstrValue = Mid(lstrValue, 1, llngSubscript - 1) & "." & Mid(lstrValue, llngSubscript + 1)
			llngSubscript = InStr(1, lstrValue, ",")
		Loop 
		insValFormat = lstrValue
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insValFormat(lstrValue)", New Object(){lstrValue})
	End Function
	
	'**%Objective: Construct the string for dates and concatenate it to the "WHERE"
	'**%Parameters:
	'**%    BeforeWord  -
	'**%    AfterWord   -
	'**%    lstrValue   -
	'**%    lstrField   -
	'**%    lstrOperand -
	'**%    Conection   -
	'%Objetivo: construye la cadena para fechas y la concatena al where
	'%Parámetros:
	'%      BeforeWord  -
	'%      AfterWord   -
	'%      lstrValue   -
	'%      lstrField   -
	'%      lstrOperand -
	'%      Conection   -
	Private Function insConstructDate(ByVal BeforeWord As String, ByVal AfterWord As String, ByVal lstrValue As String, ByVal lstrField As String, Optional ByVal lstrOperand As String = "", Optional ByVal Conection As eWordConnection = 0) As String
		Dim lintPosChar As Short
		Dim lstrString As String = String.Empty
        Dim lintConstant As Short

        ''On Error GoTo ErrorHandler

        insConstructDate = String.Empty
		Select Case lstrOperand
			Case ":"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructDate = " AND "
					Case eWordConnection.eOr
						insConstructDate = " OR "
				End Select
				insConstructDate = insConstructDate & BeforeWord & lstrField & " BETWEEN " & lstrValue & AfterWord
				
			Case "<>", "="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructDate = " AND "
					Case eWordConnection.eOr
						insConstructDate = " OR "
				End Select
				insConstructDate = insConstructDate & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & AfterWord
				
			Case "<="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructDate = " AND "
					Case eWordConnection.eOr
						insConstructDate = " OR "
				End Select
				insConstructDate = insConstructDate & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & AfterWord
				
			Case ">="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructDate = " AND "
					Case eWordConnection.eOr
						insConstructDate = " OR "
				End Select
				insConstructDate = insConstructDate & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & AfterWord
				
			Case ">"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructDate = " AND "
					Case eWordConnection.eOr
						insConstructDate = " OR "
				End Select
				insConstructDate = insConstructDate & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & AfterWord
				
			Case "<"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructDate = " AND "
					Case eWordConnection.eOr
						insConstructDate = " OR "
				End Select
				insConstructDate = insConstructDate & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & AfterWord
				
		End Select
		
		If Trim(pstrWhere) = "WHERE" Then
			Select Case Conection
				Case eWordConnection.eAnd
					lstrString = "AND"
					lintConstant = 3
				Case eWordConnection.eOr
					lstrString = "OR"
					lintConstant = 2
			End Select
			lintPosChar = InStr(1, insConstructDate, lstrString)
			insConstructDate = Mid(insConstructDate, lintPosChar + lintConstant)
		End If
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insConstructDate(BeforeWord,AfterWord,lstrValue,lstrField,lstrOperand,Conection)", New Object(){BeforeWord, AfterWord, lstrValue, lstrField, lstrOperand, Conection})
	End Function
	
	'**%Objective: Construct the section for a string and concatenate it to the "WHERE" clause
	'**%Parameters:
	'**%    BeforeWord  -
	'**%    AfterWord   -
	'**%    lstrValue   -
	'**%    lstrField   -
	'**%    lstrOperand -
	'**%    Conection   -
	'%Objetivo: construye la cadena para un string y lo concatena al where
	'%Parámetros:
	'%      BeforeWord  -
	'%      AfterWord   -
	'%      lstrValue   -
	'%      lstrField   -
	'%      lstrOperand -
	'%      Conection   -
	Private Function insConstructString(ByVal BeforeWord As String, ByVal AfterWord As String, ByVal lstrValue As String, ByVal lstrField As String, Optional ByVal lstrOperand As String = "", Optional ByVal Conection As eWordConnection = 0) As String
		Dim lintPosChar As Short
		Dim lstrString As String = String.Empty
		Dim lintConstant As Short
		
        ''On Error GoTo ErrorHandler
        insConstructString = String.Empty
		Select Case lstrOperand
			Case String.Empty
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructString = " AND "
					Case eWordConnection.eOr
						insConstructString = " OR "
				End Select
				insConstructString = insConstructString & BeforeWord & "UPPER(" & Trim(lstrField) & ") LIKE UPPER('" & Trim(lstrValue) & "') " & AfterWord
				
			Case "IN", "in"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructString = " AND "
					Case eWordConnection.eOr
						insConstructString = " OR "
				End Select
				insConstructString = insConstructString & BeforeWord & lstrField & " " & lstrOperand & " " & Trim(lstrValue) & AfterWord
				
			Case "<>", "="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructString = " AND "
					Case eWordConnection.eOr
						insConstructString = " OR "
				End Select
				insConstructString = insConstructString & BeforeWord & lstrField & " " & lstrOperand & " '" & Trim(lstrValue) & "' " & AfterWord
				
			Case "<="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructString = " AND "
					Case eWordConnection.eOr
						insConstructString = " OR "
				End Select
				insConstructString = insConstructString & BeforeWord & lstrField & " " & lstrOperand & " '" & Trim(lstrValue) & "'" & AfterWord
				
			Case ">="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructString = " AND "
					Case eWordConnection.eOr
						insConstructString = " OR "
				End Select
				insConstructString = insConstructString & BeforeWord & lstrField & " " & lstrOperand & " '" & Trim(lstrValue) & "'" & AfterWord
				
			Case ">"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructString = " AND "
					Case eWordConnection.eOr
						insConstructString = " OR "
				End Select
				insConstructString = insConstructString & BeforeWord & lstrField & " " & lstrOperand & " '" & Trim(lstrValue) & "'" & AfterWord
				
			Case "<"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructString = " AND "
					Case eWordConnection.eOr
						insConstructString = " OR "
				End Select
				insConstructString = insConstructString & BeforeWord & lstrField & " " & lstrOperand & " '" & Trim(lstrValue) & "'" & AfterWord
		End Select
		If Trim(pstrWhere) = "WHERE" Then
			Select Case Conection
				Case eWordConnection.eAnd
					lstrString = "AND"
					lintConstant = 3
				Case eWordConnection.eOr
					lstrString = "OR"
					lintConstant = 2
			End Select
			lintPosChar = InStr(1, insConstructString, lstrString)
			insConstructString = Mid(insConstructString, lintPosChar + lintConstant)
		End If
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insConstructString(BeforeWord,AfterWord,lstrValue,lstrField,lstrOperand,Conection)", New Object(){BeforeWord, AfterWord, lstrValue, lstrField, lstrOperand, Conection})
	End Function
	
	'**%Objective: Construct the "WHERE" for the numeric cases
	'**%Parameters:
	'**%    BeforeWord  -
	'**%    AfterWord   -
	'**%    lstrValue   -
	'**%    lstrField   -
	'**%    lstrOperand -
	'**%    lstrValue2  -
	'**%    Conection   -
	'%Objetivo: contruye el where para casos numéricos
	'%Parámetros:
	'%      BeforeWord  -
	'%      AfterWord   -
	'%      lstrValue   -
	'%      lstrField   -
	'%      lstrOperand -
	'%      lstrValue2  -
	'%      Conection   -
	Private Function insConstructNumeric(ByVal BeforeWord As String, ByVal AfterWord As String, ByVal lstrValue As String, ByVal lstrField As String, Optional ByVal lstrOperand As String = "", Optional ByVal lstrValue2 As String = "", Optional ByVal Conection As eWordConnection = 0) As String
		Dim lintPosChar As Short
		Dim lstrString As String = String.Empty
		Dim lintConstant As Short
		
        ''On Error GoTo ErrorHandler
        insConstructNumeric = String.Empty
		Select Case lstrOperand
			Case ":"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructNumeric = " AND "
					Case eWordConnection.eOr
						insConstructNumeric = " OR "
				End Select
				insConstructNumeric = insConstructNumeric & BeforeWord & lstrField & " BETWEEN " & lstrValue & " AND " & lstrValue2 & " " & AfterWord
				
			Case "<>", "="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructNumeric = " AND "
					Case eWordConnection.eOr
						insConstructNumeric = " OR "
				End Select
				insConstructNumeric = insConstructNumeric & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & " " & AfterWord
				
			Case "<="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructNumeric = "AND "
					Case eWordConnection.eOr
						insConstructNumeric = "OR "
				End Select
				insConstructNumeric = insConstructNumeric & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & " " & AfterWord
				
			Case ">="
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructNumeric = "AND "
					Case eWordConnection.eOr
						insConstructNumeric = "OR "
				End Select
				insConstructNumeric = insConstructNumeric & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & " " & AfterWord
				
			Case ">"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructNumeric = "AND "
					Case eWordConnection.eOr
						insConstructNumeric = "OR "
				End Select
				insConstructNumeric = insConstructNumeric & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & " " & AfterWord
				
			Case "<"
				Select Case Conection
					Case eWordConnection.eAnd
						insConstructNumeric = "AND "
					Case eWordConnection.eOr
						insConstructNumeric = "OR "
				End Select
				insConstructNumeric = insConstructNumeric & BeforeWord & lstrField & " " & lstrOperand & " " & lstrValue & " " & AfterWord
				
		End Select
		If Trim(pstrWhere) = "WHERE" Then
			Select Case Conection
				Case eWordConnection.eAnd
					lstrString = "AND"
					lintConstant = 3
				Case eWordConnection.eOr
					lstrString = "OR"
					lintConstant = 2
			End Select
			lintPosChar = InStr(1, insConstructNumeric, lstrString)
			insConstructNumeric = Mid(insConstructNumeric, lintPosChar + lintConstant)
		End If
		
		Exit Function
ErrorHandler: 
		ProcError("ConstructSelect.insConstructNumeric(BeforeWord,AfterWord,lstrValue,lstrField,lstrOperand,lstrValue2,Conection)", New Object(){BeforeWord, AfterWord, lstrValue, lstrField, lstrOperand, lstrValue2, Conection})
	End Function
	
	'**%Objective: Is in charge to delete the joins inside of an "OR" relation
	'%Objetivo: se encarga de quitar los joins dentro de una relación OR
	Private Sub insDelSymbol()
		Dim lstrTemp As String
		Dim lintPosOpen(30) As Short
		Dim lintQuantOpen As Short
		Dim lintPosOr As Short
		Dim lintIndexOpen As Short
		Dim lintIndex As Short
		Dim lintPosInit As Short
		Dim lintPosFinish As Short
		Dim lintPosJoin As Short
		Dim lintLen As Short
		
		''On Error GoTo ErrorHandler
		lintIndexOpen = 0
		lintIndex = 1
		lintPosOr = 0
		lintPosOpen(0) = 0
		lintQuantOpen = 0
		lintPosOr = InStr(1, pstrResult, "OR")
		If lintPosOr > 0 Then
			If InStr(1, pstrResult, "(+)") > 0 Then
				lintLen = Len(pstrResult)
				lstrTemp = Mid(pstrResult, lintIndex, 1)
				Do While lintIndex <= lintPosOr
					If lstrTemp = "(" Then
						lintPosOpen(lintIndexOpen) = lintIndex
						lintIndexOpen = lintIndexOpen + 1
						lintQuantOpen = lintQuantOpen + 1
					Else
						If lstrTemp = ")" Then
							lintIndexOpen = lintIndexOpen - 2
							lintQuantOpen = lintQuantOpen - 1
							If lintIndexOpen < 0 Then
								lintIndexOpen = 0
							End If
						End If
					End If
					lintIndex = lintIndex + 1
					lstrTemp = Mid(pstrResult, lintIndex, 1)
				Loop 
				If lintQuantOpen <> 0 Then
					lintIndex = lintIndex + 1
					lstrTemp = Mid(pstrResult, lintIndex, 1)
					Do While lintIndex <= lintLen And lintQuantOpen <> 0
						If lstrTemp = "(" Then
							lintPosOpen(lintIndexOpen) = lintIndex
							lintIndexOpen = lintIndexOpen + 1
							lintQuantOpen = lintQuantOpen + 1
						Else
							If lstrTemp = ")" Then
								lintIndexOpen = lintIndexOpen - 2
								lintQuantOpen = lintQuantOpen - 1
								If lintIndexOpen < 0 Then
									lintIndexOpen = 0
								End If
							End If
						End If
						lintIndex = lintIndex + 1
						lstrTemp = Mid(pstrResult, lintIndex, 1)
					Loop 
					lintPosInit = lintPosOpen(lintIndexOpen)
					lintPosFinish = lintIndex
					lstrTemp = Mid(pstrResult, lintPosInit, lintPosFinish - lintPosInit)
					lintPosJoin = InStr(1, lstrTemp, "(+)")
					Do While lintPosJoin > 0
						lstrTemp = Mid(lstrTemp, 1, lintPosJoin - 1) & Mid(lstrTemp, lintPosJoin + 3)
						lintPosJoin = InStr(lintPosJoin, lstrTemp, "(+)")
					Loop 
					pstrResult = Mid(pstrResult, 1, lintPosInit - 1) & lstrTemp & Mid(pstrResult, lintPosFinish)
				End If
			End If
		End If
		
		Exit Sub
ErrorHandler: 
		ProcError("ConstructSelect.insDelSymbol()")
	End Sub
	
	'**%Objective: Replace the date macro according to parameters
	'**%Parameters:
	'**%    lstrString          -
	'**%    lstrMacro           -
	'**%    lstrMacroSubstitute -
	'**%    strMacroFinal       -
	'%Objetivo: reemplaza el macro de la fecha según parámetros
	'%Parámetros:
	'%      lstrString          -
	'%      lstrMacro           -
	'%      lstrMacroSubstitute -
	'%      strMacroFinal       -
    Private Function insReplaceMacros(ByVal lstrString As String, ByVal lstrMacro As String, ByVal lstrMacroSubstitute As String, Optional ByVal strMacroFinal As String = "") As String
        Dim lintPosMacro As Short
        Dim lintPosClose As Short
        Dim lstrTemp1 As String

        ''On Error GoTo ErrorHandler
        lintPosMacro = InStr(1, lstrString, lstrMacro)
        Do While lintPosMacro > 0
            lintPosClose = InStr(lintPosMacro, lstrString, ")")
            lstrTemp1 = Mid(lstrString, 1, lintPosMacro - 1) & lstrMacroSubstitute
            If lstrMacroSubstitute = "CONVERT(VARCHAR," Then
                lstrString = lstrTemp1 & Mid(lstrString, lintPosMacro + 6, lintPosClose - (lintPosMacro + 6)) & strMacroFinal & Mid(lstrString, lintPosClose)
            Else
                lstrString = lstrTemp1 & Mid(lstrString, lintPosMacro + 5, lintPosClose - (lintPosMacro + 4)) & Mid(lstrString, lintPosClose + 1)
            End If
            lintPosMacro = InStr(lintPosMacro, lstrString, lstrMacro)
        Loop
        insReplaceMacros = lstrString

        Exit Function
ErrorHandler:
        ProcError("ConstructSelect.insReplaceMacros(lstrString,lstrMacro,lstrMacroSubstitute,strMacroFinal)", New Object() {lstrString, lstrMacro, lstrMacroSubstitute, strMacroFinal})
    End Function
	
	'**%Objective:
	'%Objetivo:
	Public ReadOnly Property Server() As Connection.sTypeServer
		Get
			Dim lclsConfig As eRemoteDB.VisualTimeConfig
			Dim lstrServer As String
			
			''On Error GoTo ErrorHandler
			lclsConfig = New eRemoteDB.VisualTimeConfig
			lstrServer = UCase(lclsConfig.LoadSetting("Server", "Oracle", "Database"))
			Select Case lstrServer
				Case "ORACLE"
					Server = Connection.sTypeServer.sOracle
				Case "SQL SERVER", "SQLSERVER"
                    Server = Connection.sTypeServer.sSQLServer7
				Case "DB2"
					Server = Connection.sTypeServer.sDB2
				Case "INFORMIX"
					Server = Connection.sTypeServer.sInformix
			End Select
			
			lclsConfig = Nothing
			Exit Property
ErrorHandler: 
			ProcError("ConstructSelect.Server()")
		End Get
	End Property
End Class






