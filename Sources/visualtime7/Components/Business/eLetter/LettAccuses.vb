Option Strict Off
Option Explicit On
Public Class LettAccuses
	Implements System.Collections.IEnumerable
	'**+Objetive: Clase generada a partir de la tabla 'LETTACCUSE' que es Acuse de correspondencia.
	'**+Un registro por cada carta generada a partir de una solicitud de envío, donde se  indica el acuse (aviso de entrega y de respuesta del cliente).
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'LETTACCUSE' Acknowledgment of letter receipt.A record per every letter.
	'+Version: $$Revision: 9 $
	
	'**-Objective: local variable to hold collection.
	'-Objetivo: Variable Local para almacenar la coleccion.
	Private mCol As Collection
	
	'**-Objective: Local variable to store of temporary form the number of the request of the correspondence.
	'-Objetivo: Variable local para almacenar de forma temporal el número de la solicitud de la correspondencia.
	Private mintLettRequest As Short
	
	'**-Objective: Local variable to store of temporary form the code of the client.
	'-Objetivo: Variable local para almacenar de forma temporal el código del cliente.
	Private mstrClient As String
	
	'**-Objective: Variable of boolena condition, to indicate if it found some registry or no.
	'-Objetivo: Variable de condición boolena, para indicar si encontro algún registro o no.
	Private mblnCharge As Boolean
	
	'**%Objective: Adds a new instance of the LettValues class to the collection
	'**%Parameters:
	'**%  nStatLetter   - Status of The Letter.Possible values as per table 624
	'**%  nTypeLetter   - Type of letter format 1 - Template 2 - Customized
	'**%  tletter       - Information to replace the variables in a letter format.
	'**%  dToHandOver   - Date when the letter is received by the addressee.
	'**%  sClient       - Code of the client.
	'**%  nLettRequest  - Number of the request for remittance of correspondence.
	'**%  nNoteNum      - Number of the note containing the comments.
	'**%  tAnswer       - Letter Answer.
	'**%  sKey          - Variable with functions of key field
	'%Objetivo: Añade una nueva instancia de la clase LettValues a la colección.
	'%Parámetros:
	'%    nStatLetter   - Estado de la carta.Valores posibles según tabla 624
	'%    nTypeLetter   - Tipo de modelo de carta 1 - Modelo (template) 2 - Personalizado
	'%    tletter       - Información que sustituye las variables en el modelo de carta.
	'%    dToHandOver   - Fecha en que se entregó la correspondencia al destinatario.
	'%    sClient       - Código que identifica al cliente.
	'%    nLettRequest  - Número de solicitud de envío.
	'%    nNoteNum      - Número de la nota que contiene el texto libre.
	'%    tAnswer       - Carta respuesta del destinatario.
	'%    sKey          - Variable con funciones de campo clave
	Private Function Add(ByVal nStatLetter As Short, ByVal nTypeLetter As Short, ByVal dToHandOver As Date, ByVal dAnswerDate As Date, ByVal sClient As String, ByVal nLettRequest As Short, ByVal nNoteNum As Short, ByVal tAnswer As String, ByVal sDescript As String, Optional ByVal sKey As String = "") As LettAccuse
		Dim objNewMember As LettAccuse
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New LettAccuse
		
		'**+ Set the properties passed into the method
		'+ Almacena las variables en el objeto.
		
		objNewMember.nStatLetter = nStatLetter
		objNewMember.nTypeLetter = nTypeLetter
		objNewMember.dToHandOver = dToHandOver
		objNewMember.dAnswerDate = dAnswerDate
		objNewMember.sClient = sClient
		objNewMember.nLettRequest = nLettRequest
		objNewMember.nNoteNum = nNoteNum
		objNewMember.tAnswer = tAnswer
		objNewMember.sDescript = sDescript
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		'**+ Return the object created
		'* devuelve el objeto creado.
		
		Add = objNewMember
		objNewMember = Nothing
		
		Exit Function
		Add = Nothing
		objNewMember = Nothing
	End Function
	
	'**%Objective: Used when referencing an element in the collection
	'**%           vntIndexKey contains either the Index or Key to the collection,
	'**%           this is why it is declared as a Variant
	'**%           Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'**%Parámeters:
	'**%  vntIndexKey -  Indicates the position or indice to consult.
	'%Objetivo:  Utilizado para referirse a un elemento de la colección vntIndexKey que
	'%           contiene el índice o clave a la colección, es la razón por la cual se
	'%           declara como variante.
	'%           Sintaxis: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'%Parámetros:
	'%    vntIndexKey - Indica la posición o indice a consultar.
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As LettAccuse
		Get
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Restores the number of elements that the collection owns.Used when retrieving the number of elements in the collection. Syntax: Debug.Print x.Count
	'%Objetivo: Devuelve el número de elementos que posee la colección.Used when retrieving the number of elements in the collection. Syntax: Debug.Print x.Count
	Public ReadOnly Property Count() As Integer
		Get
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
            Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the For...Each syntax
	'%Objetivo:  Esta propiedad le permite enumerar esta colección con la  sintaxis For...Each
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("LettAccuses.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: used when removing an element from the collection
	'**%           vntIndexKey contains either the Index or Key, which is why
	'**%           it is declared as a Variant
	'**%           Syntax: x.Remove(xyz)
	'**%Parámeters:
	'**%  vntIndexKey -  Indicates the position or indice to consult.
	'%Objetivo: Se utiliza para eliminar un elemento de la colección
	'           vntIndexKey contenido en el indice o campo clave, es por ello
	'           que este se declare como variante
	'           Sintaxis: x.Remove(xyz)
	'%Parámetros:
	'%    vntIndexKey - Indica la posición o indice a consultar.
	Private Sub Remove(ByVal vntIndexKey As Object)
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective:  Creates the collection when this class is created
	'%Objetivo: Crea la colección cuando esta clase es creada
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated
	'%Objetivo:  Destruye la colección cuando esta clase es terminada
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
	
	'**%Objective: Restores a collection of objects of the LettAcusse type
	'**%Parameters:
	'**%  lintLettRequest - number of the requirement or request of correspondence
	'**%  lstrClient      - Code of the client
	'**%  lblnAll         - Variable of boolean type
	'%Objetivo: Devuelve una coleccion de objetos de tipo LettValues
	'%Parámetros:
	'%    lintLettRequest - Numero del requerimiento o solicitud de la correspondencia.
	'%    lstrClient      - Código del cliente.
	'%    lblnAll         - Variable de tipo booleano.
	Public Function Find(Optional ByVal lintLettRequest As Short = eRemoteDB.Constants.intNull, Optional ByVal lstrClient As String = "", Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecreaLettAccuse As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecreaLettAccuse = New eRemoteDB.Execute
		
		If mintLettRequest <> lintLettRequest Or mstrClient <> lstrClient Or lblnAll Then
			mCol = New Collection
			With lrecreaLettAccuse
				.StoredProcedure = "realettaccuse"
				.Parameters.Add("nLettrequest", lintLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						Add(.FieldToClass("nStatLetter"), .FieldToClass("nTypeLetter"), .FieldToClass("dToHandOver"), .FieldToClass("dAnswerDate"), .FieldToClass("sClient"), .FieldToClass("nLettRequest"), .FieldToClass("nNoteNum"), .FieldToClass("tAnswer"), .FieldToClass("sDescript"))
						.RNext()
					Loop 
					.RCloseRec()
					mintLettRequest = lintLettRequest
					mstrClient = lstrClient
					mblnCharge = True
				Else
					mblnCharge = False
				End If
			End With
			lrecreaLettAccuse = Nothing
		End If
		Find = mblnCharge
		
		Exit Function
		lrecreaLettAccuse = Nothing
	End Function
	'**%Objective: Restores a collection of objects of the LettAcusse type
	'**%Parameters:
	'**%  lintLettRequest - number of the requirement or request of correspondence
	'**%  lstrClient      - Code of the client
	'**%  lblnAll         - Variable of boolean type
	'%Objetivo: Devuelve una coleccion de objetos de tipo LettValues
	'%Parámetros:
	'%    lintLettRequest - Numero del requerimiento o solicitud de la correspondencia.
	'%    lstrClient      - Código del cliente.
	'%    lblnAll         - Variable de tipo booleano.
	Public Function FindSI119(Optional ByVal lintLettRequest As Short = intNull, Optional ByVal lstrClient As String = strNull, Optional ByVal llngClaim As Integer = intNull, Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecreaLettAccuse As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecreaLettAccuse = New eRemoteDB.Execute
		
		If mintLettRequest <> lintLettRequest Or mstrClient <> lstrClient Or lblnAll Then
			mCol = New Collection
			With lrecreaLettAccuse
				.StoredProcedure = "REALETTACCUSESI119"
				.Parameters.Add("nLettRequest", lintLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						AddSI119(.FieldToClass("nStatLetter"), .FieldToClass("nTypeLetter"), .FieldToClass("dToHandOver"), .FieldToClass("dAnswerDate"), .FieldToClass("sClient"), .FieldToClass("nLettRequest"), .FieldToClass("nNoteNum"), .FieldToClass("tAnswer"), .FieldToClass("sDescript"), .FieldToClass("dPrintDate"))
						.RNext()
					Loop 
					.RCloseRec()
					mintLettRequest = lintLettRequest
					mstrClient = lstrClient
					mblnCharge = True
				Else
					mblnCharge = False
				End If
			End With
			lrecreaLettAccuse = Nothing
		End If
		FindSI119 = mblnCharge
		
		Exit Function
		lrecreaLettAccuse = Nothing
	End Function
	'**%Objective: Adds a new instance of the LettValues class to the collection
	'**%Parameters:
	'**%  nStatLetter   - Status of The Letter.Possible values as per table 624
	'**%  nTypeLetter   - Type of letter format 1 - Template 2 - Customized
	'**%  tletter       - Information to replace the variables in a letter format.
	'**%  dToHandOver   - Date when the letter is received by the addressee.
	'**%  sClient       - Code of the client.
	'**%  nLettRequest  - Number of the request for remittance of correspondence.
	'**%  nNoteNum      - Number of the note containing the comments.
	'**%  tAnswer       - Letter Answer.
	'**%  sKey          - Variable with functions of key field
	'%Objetivo: Añade una nueva instancia de la clase LettValues a la colección.
	'%Parámetros:
	'%    nStatLetter   - Estado de la carta.Valores posibles según tabla 624
	'%    nTypeLetter   - Tipo de modelo de carta 1 - Modelo (template) 2 - Personalizado
	'%    tletter       - Información que sustituye las variables en el modelo de carta.
	'%    dToHandOver   - Fecha en que se entregó la correspondencia al destinatario.
	'%    sClient       - Código que identifica al cliente.
	'%    nLettRequest  - Número de solicitud de envío.
	'%    nNoteNum      - Número de la nota que contiene el texto libre.
	'%    tAnswer       - Carta respuesta del destinatario.
	'%    sKey          - Variable con funciones de campo clave
	Private Function AddSI119(ByVal nStatLetter As Short, ByVal nTypeLetter As Short, ByVal dToHandOver As Date, ByVal dAnswerDate As Date, ByVal sClient As String, ByVal nLettRequest As Short, ByVal nNoteNum As Short, ByVal tAnswer As String, ByVal sDescript As String, ByVal dPrintDate As Date, Optional ByVal sKey As String = "") As LettAccuse
		Dim objNewMember As LettAccuse
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New LettAccuse
		
		'**+ Set the properties passed into the method
		'+ Almacena las variables en el objeto.
		
		objNewMember.nStatLetter = nStatLetter
		objNewMember.nTypeLetter = nTypeLetter
		objNewMember.dToHandOver = dToHandOver
		objNewMember.dAnswerDate = dAnswerDate
		objNewMember.sClient = sClient
		objNewMember.nLettRequest = nLettRequest
		objNewMember.nNoteNum = nNoteNum
		objNewMember.tAnswer = tAnswer
		objNewMember.sDescript = sDescript
		objNewMember.dPrintDate = dPrintDate
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		'**+ Return the object created
		'* Devuelve el objeto creado.
		
		AddSI119 = objNewMember
		objNewMember = Nothing
		
		Exit Function
		AddSI119 = Nothing
		objNewMember = Nothing
	End Function
    '**%Objective: Restores a collection of objects of the LettAcusse type
    '**%Parameters:
    '**%  lintLettRequest - number of the requirement or request of correspondence
    '**%  lstrClient      - Code of the client
    '**%  ldatInpDate     - Date when the request is recorded
    '%Objetivo: Devuelve una coleccion de objetos de tipo LettAccuse
    '%Parámetros:
    '%    lintLettRequest - Numero del requerimiento o solicitud de la correspondencia.
    '%    lstrClient      - Código del cliente.
    '%    ldatInpDate     - Fecha de registro.
    '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Function FindLTL002(Optional ByVal lintLettRequest As Integer = eRemoteDB.Constants.intNull, Optional ByVal lstrClient As String = eRemoteDB.Constants.strNull, Optional ByVal ldatInpDate As Date = eRemoteDB.Constants.dtmNull) As Boolean
        '-----------------------------------------------------------------------------------------------------------------------
        Dim lrecreaLettAccuse As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lrecreaLettAccuse = New eRemoteDB.Execute

        mCol = New Collection
        With lrecreaLettAccuse
            .StoredProcedure = "realtl002"
            .Parameters.Add("NLETTREQUEST", lintLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SCLIENT", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DEFFECDATE", ldatInpDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Do While Not .EOF
                    Call Add(.FieldToClass("nStatLetter"), .FieldToClass("nTypeLetter"), .FieldToClass("dToHandOver"), .FieldToClass("dAnswerDate"), .FieldToClass("sClient"), .FieldToClass("nLettRequest"), .FieldToClass("nNoteNum"), .FieldToClass("tAnswer"), .FieldToClass("sDescript"))
                    .RNext()
                Loop
                .RCloseRec()
                mintLettRequest = lintLettRequest
                mstrClient = lstrClient
                mblnCharge = True
            Else
                mblnCharge = False
            End If
        End With
        lrecreaLettAccuse = Nothing

        FindLTL002 = mblnCharge

        Exit Function
        lrecreaLettAccuse = Nothing
    End Function
End Class












