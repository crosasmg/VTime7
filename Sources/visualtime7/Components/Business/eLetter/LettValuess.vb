Option Strict Off
Option Explicit On
Public Class LettValuess
	Implements System.Collections.IEnumerable
	'**+Objetive: Clase generada a partir de la tabla 'LETTVALUES' que es Parámetros o condiciones de la solicitud de envío.Un registro por cada parámetro o variable de la condición
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'LETTVALUES' Parameters or conditions of the request.A record per every parameter or variable of the condition
	'+Version: $$Revision: 9 $
	
	'**-Objective: local variable to hold collection.
	'-Objetivo: Variable Local para almacenar la coleccion.
	Private mCol As Collection
	
	'**-Objective: Variable that contains the conditional one of querys
	'-Objetivo: Variable que contienen el condicional de un querys
	Public sSql As String
	
	'**-Objective: Variable of boolean type
	'-Objetivo: Variable de tipo booleano
	Private mintAll As Short
	
	'**%Objective: Adds a new instance of the LettValues class to the collection
	'**%Parameters:
	'**%  nLettRequest    - Number of the request for remittance of  correspondence.
	'**%  nConsec         - Consecutive number identifying the parameter or variable order.
	'**%  nLett_group     - Code of the variable group (Correspondence).
	'**%  nParameters     - Parameter Code the possible values as per table 622.
	'**%  sVariable       - Name of The Variable used in Correspondence.
	'**%  sValue          - Parameter or variable value.
	'**%  nUsercode       - Code of the user creating or updating the record.
	'**%  nAritOper       - Code of operation symbol sole values as per table 311.
	'**%  nStatusInstance - Status of the instance
	'**%  sKey            - Key field or index.
	'**%  sAritOper       - Description of the aritmetica operation.
	'**%  sColumName      - Description of the column.
	'%Objetivo: Añade una nueva instancia de la clase LettValues a la colección.
	'%Parámetros:
	'%    nLettRequest    - Número de solicitud de envío.
	'%    nConsec         - Consecutivo que identifica el orden del parámetro o variable.
	'%    nLett_group     - Código del grupo de variables (Correspondencia).
	'%    nParameters     - Código del parámetro valores posibles según tabla 622.
	'%    sVariable       - Nombre de la variable utilizada en correspondencia.
	'%    sValue          - Valor del parámetro o de la variable.
	'%    nUsercode       - Código del usuario que crea o actualiza el registro.
	'%    nAritOper       - Código del tipo de operando Valores posibles según tabla 311
	'%    nStatusInstance - Estado de la instancia
	'%    sKey            - Campo clave o índice.
	'%    sAritOper       - Descripción de la operación aritmetica.
	'%    sColumName      - Descripción de la columna.
	Public Function Add(Optional ByVal nLettRequest As Short = -32768, Optional ByVal nConsec As Short = -32768, Optional ByVal nLett_group As Short = -32768, Optional ByVal nParameters As Short = -32768, Optional ByVal sVariable As String = strNull, Optional ByVal sValue As String = strNull, Optional ByVal nUsercode As Short = -32768, Optional ByVal nAritOper As Short = 1, Optional ByVal nStatusInstance As Short = 0, Optional ByVal sKey As String = "", Optional ByVal sAritOper As String = "", Optional ByVal sColumName As String = "") As LettValues
		Dim objNewMember As LettValues
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New LettValues
		With objNewMember
			.nAritOper = nAritOper
			.nConsec = nConsec
			.nLett_group = nLett_group
			.nLettRequest = nLettRequest
			.nParameters = nParameters
			.nUsercode = nUsercode
			.sValue = sValue
			.sVariable = sVariable
			.nStatusInstance = nStatusInstance
			.sAritOper = sAritOper
			.sColumName = sColumName
		End With
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		Add = objNewMember
		objNewMember = Nothing
		
		Exit Function
		objNewMember = Nothing
	End Function

    'PENDING: Este metodo necesito ser reprogramado para solo manejar un recordset a la vez.
	'**%Objective: Restores a collection of objects of the LettValues type
	'**%Parameters:
	'**%  nLettRequest      - Number of the request for remittance of  correspondence.
	'**%  nType             - Type of load. Load to LT031 or LT030
	'%Objetivo: Devuelve una coleccion de objetos de tipo LettValues
	'%Parámetros:
	'%    nLettRequest      - Número de solicitud de envío..
	'%    nType             - Tipo de carga. Carga la LT031 o LT030
    Public Function Find(ByVal nLettRequest As Short, Optional ByVal nType As Short = 0) As Boolean
        Dim lclsQuery As eRemoteDB.Query = Nothing
        Dim lrecreaLettValues As eRemoteDB.Execute = Nothing
        Dim nIndex As Short
        Dim sWhere As String = String.Empty
        Dim nInitial As Short
        Dim nEnd As Short
        Dim lobjValues As LettValues = Nothing

        If Not IsIDEMode() Then
        End If

        lrecreaLettValues = New eRemoteDB.Execute
        lclsQuery = New eRemoteDB.Query

        Find = False
        With lrecreaLettValues
            .StoredProcedure = "reaLettValues"
            .Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", IIf(nType = 0, System.DBNull.Value, 1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                nIndex = 0
                Do While Not .EOF
                    nIndex = nIndex + 1
                    lobjValues = New LettValues
                    lclsQuery = New eRemoteDB.Query
                    Call lclsQuery.OpenQuery("Table311", "sDescript", "nAritOper=" & CStr(.FieldToClass("nAritOper")))
                    Add(.FieldToClass("nLettRequest"), .FieldToClass("nConsec"), .FieldToClass("nLett_group"), .FieldToClass("nParameters"), .FieldToClass("sVariable"), .FieldToClass("sValue"), .FieldToClass("nUsercode"), .FieldToClass("nAritOper"), 0, "v" & .FieldToClass("nLettRequest") & .FieldToClass("nConsec"), lclsQuery.FieldToClass("sDescript"), .FieldToClass("sColumName"))
                    lrecreaLettValues = Nothing
                    If nIndex > 1 Then
                        sWhere = sWhere & "  AND  "
                    End If
                    If lclsQuery.FieldToClass("sDescript") <> "Between" Then
                        sWhere = sWhere + .FieldToClass("sColumName") & " " & lclsQuery.FieldToClass("sDescript") & " " & .FieldToClass("sValue")
                    Else
                        nInitial = CShort(InStr(1, .FieldToClass("sValue"), ",")) - 1
                        nEnd = InStr(1, .FieldToClass("sValue"), ",") + 1
                        sWhere = sWhere + .FieldToClass("sColumName") + " " + lclsQuery.FieldToClass("sDescript") + " " + CStr(Mid(.FieldToClass("sValue"), 1, nInitial)) + " AND " + CStr(nEnd)
                    End If
                    lclsQuery.CloseQuery()
                    lclsQuery = Nothing
                    .RNext()
                Loop
                sSql = sWhere
                .RCloseRec()
            End If
        End With
        lrecreaLettValues = Nothing

        Exit Function
        lrecreaLettValues = Nothing
    End Function
	
	'**%Objective: This function is in charge of updating the data in a class of the collection.
	'%Objetivo: Esta función se encarga de actualizar información en una clase de la coleccion.
	Public Function Update() As Boolean
		Dim lobjLettValue As LettValues
		
		If Not IsIDEMode Then
		End If
		
		For	Each lobjLettValue In mCol
			With lobjLettValue
				Select Case .nStatusInstance
					Case 1
						Update = .Add
				End Select
			End With
		Next lobjLettValue
		
		Exit Function
	End Function
	
	'**%Objective: Used when referencing an element in the collection vntIndexKey contains either the Index or Key to the collection, this is why it is declared as a Variant Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'%Objetivo: Es usada para refenciar un elemento de la colección. La sintaxis: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As LettValues
		Get
			
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Restores the number of elements that the collection owns.
	'%Objetivo: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Allows to enumerate the collection for using it in a cycle For Each...Next
	'%Objetivo: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("LettValuess.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: Stores to the boolean value of the variable mintAll
	'%Objetivo: Almacena el valor booleano de la variable mintAll
	Public ReadOnly Property AllValues() As Short
		Get
			
			If Not IsIDEMode Then
			End If
			
			AllValues = mintAll
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Used when removing an element from the collection vntIndexKey contains either the Index or Key, which is why it is declared as a Variant Syntax: x.Remove(xyz)
	'%Objetivo: Se utiliza para eliminar un elemento de la colección. a sintaxis: x.Remove(xyz)
	Private Sub Remove(ByVal vntIndexKey As Object)
		
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created
	'%Objetivo: Crea la colección cunado la clase es creada.
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = New Collection
		mintAll = False
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated
	'%Objetivo: Elimina la colección cuando la clase finaliza.
	Private Sub Class_Terminate_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%Objective: Restores a collection of objects of LettValues by passing a parameter
	'**%Parameters:
	'**% nLettRequest   - Number of the request for remittance of  correspondence.
	'%Objetivo: Devuelve una coleccion de objetos de tipo LettValues pasando un parametro
	'%Parámetros:
	'%   nLettRequest   - Número de solicitud de envío.
	Public Function FindByParameters(ByVal nLettRequest As Short) As Boolean
		Dim lrecreaLettValuesLT031 As eRemoteDB.Execute
		Dim sVariable As String
		
		If Not IsIDEMode Then
		End If
		
		lrecreaLettValuesLT031 = New eRemoteDB.Execute
		
		FindByParameters = False
		With lrecreaLettValuesLT031
			.StoredProcedure = "reaLettValuesLT031"
			.Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindByParameters = True
				Do While Not .EOF
					sVariable = .FieldToClass("sVariable")
					Add(.FieldToClass("nLettRequest"), intNull, intNull, .FieldToClass("nParameters"), sVariable, .FieldToClass("sValue"), intNull, 1, 0, String.Empty, String.Empty, String.Empty)
					mintAll = .FieldToClass("nIndicator", 0)
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindByParameters = False
			End If
		End With
		lrecreaLettValuesLT031 = Nothing
		
		Exit Function
		lrecreaLettValuesLT031 = Nothing
	End Function
	
	'**%Objective: Convert "Users" string values to DataBase Values
	'%Objetivo:  Convierte los valores de cadena "usuario" a valores de "Base de datos"
	Private Function ConvertVariable(ByVal sVariable As String) As String
		Dim lclsValues As eFunctions.Values
		
		If Not IsIDEMode Then
		End If

        ConvertVariable = String.Empty

		lclsValues = New eFunctions.Values
		
		Select Case UCase(sVariable)
			Case "SCERTYPE"
                ConvertVariable = eFunctions.Values.GetMessage(10506)
            Case "NBRANCH"
                ConvertVariable = eFunctions.Values.GetMessage(212)
            Case "NPRODUCT"
                ConvertVariable = eFunctions.Values.GetMessage(251)
            Case "NPOLICY"
                ConvertVariable = eFunctions.Values.GetMessage(1)
            Case "NCERTIF"
                ConvertVariable = eFunctions.Values.GetMessage(213)
            Case "NRECEIPT"
                ConvertVariable = eFunctions.Values.GetMessage(7)
            Case "SCLIENT"
                ConvertVariable = eFunctions.Values.GetMessage(121)
            Case "DEFFECDATE"
                ConvertVariable = eFunctions.Values.GetMessage(110)
            Case "NCLAIM"
                ConvertVariable = eFunctions.Values.GetMessage(9)
            Case "NINTERMED"
                ConvertVariable = eFunctions.Values.GetMessage(122)
            Case "NDIGIT"
                ConvertVariable = eFunctions.Values.GetMessage(10507)
            Case "NPAYNUMBE"
                ConvertVariable = eFunctions.Values.GetMessage(10508)
        End Select
		
		lclsValues = Nothing
		
		Exit Function
		ConvertVariable = String.Empty
		lclsValues = Nothing
	End Function
End Class











