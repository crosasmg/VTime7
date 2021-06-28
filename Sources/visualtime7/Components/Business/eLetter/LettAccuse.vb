Option Strict Off
Option Explicit On
Public Class LettAccuse
	'**+Objetive: Clase generada a partir de la tabla 'LETTACCUSE' que es Acuse de correspondencia.
	'**+Un registro por cada carta generada a partir de una solicitud de envío, donde se  indica el acuse (aviso de entrega y de respuesta del cliente).
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'LETTACCUSE' Acknowledgment of letter receipt.A record per every letter.
	'+Version: $$Revision: 9 $
	'**-Objective: Temporary variable that will contain the code of the client.
	'-Objetivo: Variable temporal que contendra el codigo del cliente.
	Private nOldsClient As String
	
	'**-Objective: Temporary variable that will contain the code of the request of the correspondence.
	'-Objetivo: Variable temporal que contendrá el código de la solicitud de la correspondencia.
	Private nOldlettrequest As Short
	
	'**-Objective: Number of the request for remittance of correspondence.
	'-Objetivo: Número de solicitud de envío.
	Public nLettRequest As Short
	
	'**-Objective: Code of the client.
	'-Objetivo: Código que identifica al cliente.
	Public sClient As String
	
	'**-Objective: Date when the addressee answers
	'-Objetivo: Fecha en que se recibe respuesta por parte del destinatario
	Public dAnswerDate As Date
	
	'**-Objective: Date when the letter is received by the addressee.
	'-Objetivo: Fecha en que se entregó la correspondencia al destinatario.
	Public dToHandOver As Date
	
	'**-Objective: Information to replace the variables in a letter format
	'-Objetivo: Información que sustituye las variables en el modelo de carta
	Public tletter As Object
	
	'**-Objective: Temporary variable that will maintain the information that replaces the variables in the letter model
	'-Objetivo: Variable temporal que mantendrá la información que sustituye las variables en el modelo de carta.
	Private mstrLetter As String
	
	'**-Objective:Type of letter format
	'**                   1 - Template
	'**                   2 - Customized
	'-Objetivo: Tipo de modelo de carta
	'-                    1 - Modelo (template)
	'-                    2 - Personalizado
	Public nTypeLetter As Short
	
	'**-Objective: Status of The Letter.Possible values as per table 624
	'-Objetivo: Estado de la carta.Valores posibles según tabla 624
	Public nStatLetter As Short
	
	'**-Objective: Number of the note containing the comments.
	'-Objetivo: Número de la nota que contiene el texto libre.
	Public nNoteNum As Short
	
	'**-Objective: Letter Answer.
	'-Objetivo: Carta respuesta del destinatario.
	Public tAnswer As String
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Short
	
	'VT MODS: SI119 Screen related Changes.
	Public sDescript As String
	'VT MODS: SI119 Screen related Changes.
	
	'VT MODS: SI119 Screen related Changes.
	Public dPrintDate As Date
	'+VT MODS: 04/29/2003
	
	'**-Objective: Temporary variable that will maintain the information that replaces the variables in the letter model
	'-Objetivo: Variable temporal que mantendrá la información que sustituye las variables en el modelo de carta.
	Private mclsLettRequest As LettRequest
	
	'**%Objective: This function is in charge of adding information to the main table of the class.
	'**%Parameters:
	'**%  nLettRequest  - Number of the request for remittance of correspondence.
	'**%  sClient       - Code of the client..
	'**%  dAnswerDate   - Date when the addressee answers.
	'**%  dToHandOver   - Date when the letter is received by the addressee.
	'**%  tletter       - Information to replace the variables in a letter format.
	'**%  nUsercode     - Code of the user creating or updating the record.
	'**%  nTypeLetter   - Type of letter format 1 - Template 2 - Customized
	'**%  nStatLetter   - Status of The Letter.Possible values as per table 624
	'**%  nNoteNum      - Number of the note containing the comments.
	'**%  tAnswer       - Letter Answer.
	'%Objetivo: Esta función se encarga de agregar información en la tabla principal de la clase.
	'%Parámetros:
	'%    nLettRequest  - Número de solicitud de envío.
	'%    sClient       - Código que identifica al cliente..
	'%    dAnswerDate   - Fecha en que se recibe respuesta por parte del destinatario
	'%    dToHandOver   - Fecha en que se entregó la correspondencia al destinatario.
	'%    tletter       - Información que sustituye las variables en el modelo de carta.
	'%    nUsercode     - Código del usuario que crea o actualiza el registro.
	'%    nTypeLetter   - Tipo de modelo de carta 1 - Modelo (template) 2 - Personalizado
	'%    nStatLetter   - Estado de la carta.Valores posibles según tabla 624
	'%    nNoteNum      - Número de la nota que contiene el texto libre.
	'%    tAnswer       - Carta respuesta del destinatario.
	Public Function Add(Optional ByVal nLettRequest As Short = intNull, Optional ByVal sClient As String = "", Optional ByVal dAnswerDate As Date = #12:00:00 AM#, Optional ByVal dToHandOver As Date = #12:00:00 AM#, Optional ByVal tletter As String = "", Optional ByVal nUsercode As Short = intNull, Optional ByVal nTypeLetter As Short = intNull, Optional ByVal nStatLetter As Short = intNull, Optional ByVal nNoteNum As Short = intNull, Optional ByVal tAnswer As String = "") As Boolean
		Dim lreccreLettAccuse As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lreccreLettAccuse = New eRemoteDB.Execute
		
		If nLettRequest <> intNull Then
			Me.nLettRequest = nLettRequest
		End If
		
		If sClient <> String.Empty Then
			Me.sClient = sClient
		End If
		
		If sClient <> String.Empty Then
			Me.dAnswerDate = dAnswerDate
		End If
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Not IsNothing(dAnswerDate) Then
			Me.dAnswerDate = dAnswerDate
		End If
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Not IsNothing(dToHandOver) Then
			Me.dToHandOver = dToHandOver
		End If
		
		If tletter <> String.Empty Then
			Me.tletter = tletter
		End If
		
		If nUsercode <> intNull Then
			Me.nUsercode = nUsercode
		End If
		
		If nTypeLetter <> intNull Then
			Me.nTypeLetter = nTypeLetter
		Else
			Me.nTypeLetter = 1
		End If
		
		If nStatLetter <> intNull Then
			Me.nStatLetter = nStatLetter
		End If
		
		If nNoteNum <> intNull Then
			Me.nNoteNum = nNoteNum
		End If
		
		If tAnswer <> String.Empty Then
			Me.tAnswer = tAnswer
		End If
		
		With lreccreLettAccuse
			.StoredProcedure = "creLettAccuse"
			.Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", Me.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tLetter", Me.tletter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeLetter", Me.nTypeLetter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatLetter", Me.nStatLetter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Add = True
			End If
		End With
		lreccreLettAccuse = Nothing
		
		Exit Function
		lreccreLettAccuse = Nothing
	End Function
	
	'**%Objective: This function is in charge of updating the data in the main table of the class.
	'**%Parameters:
	'**%  nLettRequest  - Number of the request for remittance of correspondence.
	'**%  sClient       - Code of the client..
	'**%  dAnswerDate   - Date when the addressee answers.
	'**%  dToHandOver   - Date when the letter is received by the addressee.
	'**%  nUsercode     - Code of the user creating or updating the record.
	'**%  nTypeLetter   - Type of letter format 1 - Template 2 - Customized
	'**%  nStatLetter   - Status of The Letter.Possible values as per table 624
	'**%  nNoteNum      - Number of the note containing the comments.
	'**%  tAnswer       - Letter Answer.
	'%Objetivo: Esta función se encarga de actualizar información en la tabla principal de la clase.
	'%Parámetros:
	'%    nLettRequest  - Número de solicitud de envío.
	'%    sClient       - Código que identifica al cliente..
	'%    dAnswerDate   - Fecha en que se recibe respuesta por parte del destinatario
	'%    dToHandOver   - Fecha en que se entregó la correspondencia al destinatario.
	'%    nUsercode     - Código del usuario que crea o actualiza el registro.
	'%    nTypeLetter   - Tipo de modelo de carta 1 - Modelo (template) 2 - Personalizado
	'%    nStatLetter   - Estado de la carta.Valores posibles según tabla 624
	'%    nNoteNum      - Número de la nota que contiene el texto libre.
	'%    tAnswer       - Carta respuesta del destinatario.
	Private Function Update(ByVal nLettRequest As Short, ByVal sClient As String, ByVal sInitial As String, ByVal sAccessw As String, Optional ByVal dAnswerDate As Date = dtmNull, Optional ByVal dToHandOver As Date = dtmNull, Optional ByVal nUsercode As Short = intNull, Optional ByVal nTypeLetter As Short = intNull, Optional ByVal nStatLetter As Short = intNull, Optional ByVal nNoteNum As Short = intNull, Optional ByVal tAnswer As String = "") As Boolean
		'------------------------------------- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		Dim lreccreLettAccuse As eRemoteDB.Execute

		If Not IsIDEMode Then
		End If
		
		lreccreLettAccuse = New eRemoteDB.Execute
		
		If nLettRequest <> intNull Then
			Me.nLettRequest = nLettRequest
		End If
		
		If sClient <> String.Empty Then
			Me.sClient = sClient
		End If
		
		If sClient <> String.Empty Then
			Me.dAnswerDate = dAnswerDate
		End If
		
        If Not IsNothing(dAnswerDate) Then
            Me.dAnswerDate = dAnswerDate
        End If
		
        If Not IsNothing(dToHandOver) Then
            Me.dToHandOver = dToHandOver
        End If
		
		If nUsercode <> intNull Then
			Me.nUsercode = nUsercode
		End If
		
		If nTypeLetter <> intNull Then
			Me.nTypeLetter = nTypeLetter
		End If
		
		If nStatLetter <> intNull Then
			Me.nStatLetter = nStatLetter
		End If
		
		If nNoteNum <> intNull Then
			Me.nNoteNum = nNoteNum
		End If
		
		If tAnswer <> String.Empty Then
			Me.tAnswer = tAnswer
		End If
		
		With lreccreLettAccuse
			.StoredProcedure = "updLettAccuse"
			.Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAnswerDate", dAnswerDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dToHandOver", dToHandOver, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeLetter", nTypeLetter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatLetter", nStatLetter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteNum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
		End With
		
		lreccreLettAccuse = Nothing
        Exit Function
		lreccreLettAccuse = Nothing
	End Function
	
	'**% StrDecode: Password des-encryptment routine
	'%StrDecode: Rutina de des-encriptamiento de password
	Public Function StrDecode(ByVal s As String) As String
		Dim Key As Integer
		Dim salt As Boolean
		Dim n As Integer
		Dim i As Integer
		Dim ss As String
		Dim k1 As Integer
		Dim k2 As Integer
		Dim k3 As Integer
		Dim k4 As Integer


        StrDecode = String.Empty

		If Trim(s) <> String.Empty Then
			
			Key = 1234567890
			salt = False
			
			n = Len(s)
			ss = Space(n)
			Dim sn(n) As Integer
			
			k1 = 11 + (Key Mod 233) : k2 = 7 + (Key Mod 239)
			k3 = 5 + (Key Mod 241) : k4 = 3 + (Key Mod 251)
			
			For i = 1 To n : sn(i) = Asc(Mid(s, i, 1)) : Next 
			
			For i = 1 To n - 2 : sn(i) = sn(i) Xor sn(i + 2) Xor (k4 * sn(i + 1)) Mod 256 : Next 
			For i = n To 3 Step -1 : sn(i) = sn(i) Xor sn(i - 2) Xor (k3 * sn(i - 1)) Mod 256 : Next 
			For i = 1 To n - 1 : sn(i) = sn(i) Xor sn(i + 1) Xor (k2 * sn(i + 1)) Mod 256 : Next 
			For i = n To 2 Step -1 : sn(i) = sn(i) Xor sn(i - 1) Xor (k1 * sn(i - 1)) Mod 256 : Next 
			
			For i = 1 To n : Mid(ss, i, 1) = Chr(sn(i)) : Next i
			
			If salt Then StrDecode = Mid(ss, 3, Len(ss) - 4) Else StrDecode = ss
		End If
	End Function
	
	'**Objective: delete the information in the main table of the class.
	'**%Parameters:
	'**%  nLettRequest  - Number of the request for remittance of correspondence.
	'%Objetivo: Esta función se encarga de eliminar información en la tabla principal de la clase.
	'%Parámetros:
	'%    nLettRequest  - Número de solicitud de envío.
    Public Function Delete(Optional ByVal nLettRequest As Short = -32768) As Boolean
        Dim lreccreLettAccuse As eRemoteDB.Execute

        If Not IsIDEMode Then
        End If

        lreccreLettAccuse = New eRemoteDB.Execute

        If nLettRequest <> intNull Then
            Me.nLettRequest = nLettRequest
        End If

        With lreccreLettAccuse
            .StoredProcedure = "DelLettAccuse"
            .Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Delete = True
            End If
        End With
        lreccreLettAccuse = Nothing

        Exit Function
        lreccreLettAccuse = Nothing
    End Function
	
	'**%Objective: Search of the letter generates in the LettAccuse table.
	'**%Parameters:
	'**%  lintLettRequest   - Code of the request of correspondence
	'**%  lstrClient        - Code of the client associated to the correspondence
	'%Objetivo: Realiza la busqueda de la carta genera en la tabla LettAccuse.
	'%Parámetros:
	'%    lintLettRequest   - Código de la solicitud de la correspondencia.
	'%    lstrClient        - Código del cliente asociado a la correspondencia
	Public Function Find(ByVal lintLettRequest As Short, ByVal lstrClient As String) As Boolean
		Dim lrecreaLettAccuse As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecreaLettAccuse = New eRemoteDB.Execute
		
		Me.nLettRequest = lintLettRequest
		Me.sClient = lstrClient
		
		With lrecreaLettAccuse
			.StoredProcedure = "reaLettAccuse"
			
			.Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", Me.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				
				Me.nLettRequest = .FieldToClass("nLettRequest")
				Me.sClient = .FieldToClass("sClient")
				Me.dAnswerDate = .FieldToClass("dAnswerDate")
				Me.dToHandOver = lrecreaLettAccuse.FieldToClass("dToHandOver")
				Me.nTypeLetter = .FieldToClass("nTypeLetter")
				Me.nStatLetter = .FieldToClass("nStatLetter")
				Me.nNoteNum = .FieldToClass("nNoteNum")
				Me.tAnswer = .FieldToClass("tAnswer")
				
				Find = True
				.RCloseRec()
			End If
		End With
		lrecreaLettAccuse = Nothing
		Me.tletter = tLetters
		
		Exit Function
		lrecreaLettAccuse = Nothing
	End Function
	
	'**%Objective: validate the data entered on the header zone for the form.
	'**%Parameters:
	'**%  nAction    - Number of the action in execution
	'**%  nLetterNum - Code of the letter model
	'**%  sClient    - Code of the client.
	'%Objetivo: Esta función se encarga de validar los datos introducidos en la zona de cabecera
	'%Parámetros:
	'%    nAction    - Número de la acción en ejecución
	'%    nLetterNum - Código del modelo de carta
	'%    sClient    - Código que identifica al cliente.
    Public Function valLT004_K(ByVal nAction As Short, ByVal nLetterRequest As Integer, ByVal sClient As String) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsLettRequest As LettRequest
        Dim lclsClient As eClient.Client

        If Not IsIDEMode() Then
        End If

        lobjErrors = New eFunctions.Errors
        lclsLettRequest = New LettRequest
        lclsClient = New eClient.Client

        If (Trim(sClient) = String.Empty) And (nLetterRequest = intNull) Then
            lobjErrors.ErrorMessage("LT004", 8088)
        Else
            '**+ Validation of the request
            '+ Validación de la solicitud
            If (Trim(sClient) <> String.Empty) And (nLetterRequest <> intNull) Then
                If Not lclsLettRequest.Find(nLetterRequest) Then
                    lobjErrors.ErrorMessage("LT004", 8051)
                End If
                If Not lclsClient.Find(sClient) Then
                    lobjErrors.ErrorMessage("LT004", 1007)
                End If
            Else
                If (Trim(sClient) = String.Empty) Then
                    If Not lclsLettRequest.Find(nLetterRequest) Then
                        lobjErrors.ErrorMessage("LT004", 8051)
                    End If
                End If

                '**+ Validation of the client
                '+ Validación del cliente

                If (nLetterRequest = intNull) Then
                    If Not lclsClient.Find(sClient) Then
                        lobjErrors.ErrorMessage("LT004", 1007)
                    End If
                End If
            End If
        End If

        valLT004_K = lobjErrors.Confirm

        lobjErrors = Nothing
        lclsClient = Nothing
        lclsLettRequest = Nothing

        Exit Function
        lobjErrors = Nothing
        lclsClient = Nothing
        lclsLettRequest = Nothing
    End Function
	
	'**%Objective: validate the data entered on the detail zone for the form.
	'**%Parameters:
	'**%  sAction       - Description of the action to execute.       -
	'**%  dPrintDate    - Date when the letter is printed.
	'**%  dAnswerDate   - Date when the addressee answers.
	'**%  dToHandOver   - Date when the letter is received by the addressee
	'%Objetivo: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%          forma.
	'%Parámetros:
	'%    sAction       - Descripción de la acción a ejecutarse.       -
	'%    dPrintDate    - Fecha de impresión de la carta
	'%    dAnswerDate   - Fecha en que se recibe respuesta por parte del destinatario
	'%    dToHandOver   - Fecha de entrega de la correspondencia.
	Public Function valLT004(ByVal sAction As String, ByVal dPrintDate As Date, ByVal dToHandOver As Date, ByVal dAnswerDate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		
		lobjErrors = New eFunctions.Errors
		
		'**+ Validation of the date of delivery
		'+ Validación de la fecha de entrega
		
		If dToHandOver = dtmNull Then
			lobjErrors.ErrorMessage("LT004", 3370)
		Else
			If dToHandOver < dPrintDate Then
				lobjErrors.ErrorMessage("LT004", 8403)
			End If
		End If
		
		'**+ Validation of the date of answer
		'+ Validación de la fecha de respuesta
		
		If Not dAnswerDate = dtmNull Then
			If dAnswerDate < dToHandOver Then
				lobjErrors.ErrorMessage("LT004", 8404)
			End If
		End If
		
		valLT004 = lobjErrors.Confirm
		
		lobjErrors = Nothing
		
		Exit Function
		lobjErrors = Nothing
	End Function
	
	'**%Objetive: Obtain the correspondence depending on the number of request.
	'%Objetivo: Obtener la correspondencia dependiendo del número de solicitud.
	Public ReadOnly Property oLettRequest() As LettRequest
		Get
			If Not IsIDEMode Then
			End If
			
			If mclsLettRequest Is Nothing Then
				mclsLettRequest = New LettRequest
				mclsLettRequest.Find(nLettRequest)
			End If
			oLettRequest = mclsLettRequest
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Obtain the correspondence stored according to the selection criterion
	'%Objetivo: Obtener la correspondencia almacenada según el criterio de selección
	
	'**%Objective: Store to the code of the client and the content of the letter in temporary variables.
	'%Objetivo: Almacenar el código del cliente y el contenido de la carta en variables temporales.
	'-----------------------------------------------------------
	Public Property tLetters() As String
		Get
			Dim lobjtLetter As eRemoteDB.Execute
			
			If Not IsIDEMode Then
            End If

            tLetters = String.Empty

			If nOldlettrequest <> nLettRequest Then
				lobjtLetter = New eRemoteDB.Execute
				lobjtLetter.SQL = "select tletter from lettaccuse where nLettrequest=" & nLettRequest
				lobjtLetter.SQL = lobjtLetter.SQL & " AND sclient='" & Me.sClient & "'"
				
				If lobjtLetter.Run Then
					nOldlettrequest = Me.nLettRequest
					nOldsClient = Me.sClient
					mstrLetter = lobjtLetter.FieldToClass("tletter")
					tLetters = mstrLetter
					lobjtLetter.RCloseRec()
				End If
			Else
				tLetters = mstrLetter
			End If
			lobjtLetter = Nothing
			
			Exit Property
		End Get
		Set(ByVal Value As String)
			'-----------------------------------------------------------
			If Not IsIDEMode Then
			End If
			
			mstrLetter = Value
			nOldsClient = sClient
			
			Exit Property
		End Set
	End Property
	
	'**%Objective: This function is in charge to make the update of window LT004
	'**%Parameters:
	'**%  sAction       - Description of the action to execute.        -
	'**%  nLettRequest   - Number of the request for remittance of correspondence.
	'**%  sClient        - Code of the client.
	'**%  dAnswerDate    - Date when the addressee answers.
	'**%  dToHandOver    - Date when the letter is received by the addressee.
	'**%  nUsercode      - Code of the user creating or updating the record.
	'**%  nTypeLetter    - Type of letter format 1 - Template 2 - Customized
	'**%  nStatLetter    - Status of The Letter.Possible values as per table 624
	'**%  nNoteNum       - Number of the note containing the comments.
	'**%  tAnswer        - Letter Answer.
	'%Objetivo: Esta funcion se encarga de realizar la actualización de la ventana LT004
	'%Parámetros:
	'%    sAction       - Descripción de la acción a ejecutarse.   -
	'%    nLettRequest  - Número de solicitud de envío.
	'%    sClient       - Código que identifica al cliente.
	'%    dAnswerDate   - Fecha en que se recibe respuesta por parte del destinatario
	'%    dToHandOver   - Fecha en que se entregó la correspondencia al destinatario.
	'%    nUsercode     - Código del usuario que crea o actualiza el registro.
	'%    nTypeLetter   - Tipo de modelo de carta 1 - Modelo (template) 2 - Personalizado
	'%    nStatLetter   - Estado de la carta.Valores posibles según tabla 624
	'%    nNoteNum      - Número de la nota que contiene el texto libre.
	'%    tAnswer       - Carta respuesta del destinatario.
	Public Function insPostLT004(ByVal sAction As String, ByVal nLettRequest As Short, ByVal sClient As String, ByVal dAnswerDate As Date, ByVal dToHandOver As Date, ByVal nUsercode As Short, ByVal nTypeLetter As Short, ByVal nStatLetter As Short, ByVal nNoteNum As Short, ByVal tAnswer As String, ByVal sInitial As String, ByVal sAccessw As String) As Boolean
		If Not IsIDEMode Then
		End If
		
		Select Case sAction
			
			Case "Update"
				insPostLT004 = Update(nLettRequest, sClient, sInitial, sAccessw, dAnswerDate, dToHandOver, nUsercode, nTypeLetter, nStatLetter, nNoteNum, tAnswer)
		End Select
		
		Exit Function
		insPostLT004 = False
	End Function
	
	'**%Objetive: Class finalizes.
	'%Objetivo: Finaliza la clase.
	Private Sub Class_Terminate_Renamed()
		If Not IsIDEMode Then
		End If
		
		mclsLettRequest = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	'**%Objective: This function is in charge to make the update of window LT004
	'**%Parameters:
	'**%  nLettRequest   - Number of the request for remittance of correspondence.
	'**%  sClient        - Code of the client.
	'**%  tletter        - Letter string.
	'**%  nUsercode      - Code of the user creating or updating the record.
	Public Function insPostSI119Upd(ByVal nLettRequest As Short, ByVal sClient As String, ByVal tletter As String, ByVal nUsercode As Short) As Boolean
		Dim lreccreLettAccuse As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lreccreLettAccuse = New eRemoteDB.Execute
		
		With lreccreLettAccuse
			.StoredProcedure = "updLettAccusesi119"
			.Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tLetter", tletter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostSI119Upd = True
			End If
		End With
		lreccreLettAccuse = Nothing
		
		Exit Function
		lreccreLettAccuse = Nothing
	End Function
	'VT MODS: SI119 Changes
	Public Function DeleteSI119(Optional ByVal nLettRequest As Short = -32768) As Boolean
		Dim lreccreLettAccuse As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lreccreLettAccuse = New eRemoteDB.Execute
		
		If nLettRequest <> intNull Then
			Me.nLettRequest = nLettRequest
		End If
		
		With lreccreLettAccuse
			.StoredProcedure = "DelLettAccuse"
			.Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				DeleteSI119 = True
			End If
		End With
		lreccreLettAccuse = Nothing
		
		Exit Function
		lreccreLettAccuse = Nothing
	End Function
	Public Function insPostSI119(ByVal nLettRequest As Short, ByVal nStatLetter As Short, ByVal nUsercode As Short) As Boolean
		Dim lreccreLettAccuse As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lreccreLettAccuse = New eRemoteDB.Execute
		
		With lreccreLettAccuse
			.StoredProcedure = "updlettaccusesi119post"
			.Parameters.Add("nLettrequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatletter", nStatLetter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostSI119 = True
			End If
		End With
		lreccreLettAccuse = Nothing
		
		Exit Function
		lreccreLettAccuse = Nothing
	End Function

    '**%Objective: This function is responsible for updating the status of the request for dispatch.
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of correspondence.
    '**%  sClient       - Code of the client..
    '%Objetivo: Esta función se encarga de actualizar el estado de la solicitud de envío.
    '%Parámetros:
    '%    nLettRequest  - Número de solicitud de envío.
    '%    sClient       - Código que identifica al cliente..
    '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Function insPostLT002(ByVal nLettRequest As Integer, ByVal sClient As String) As Boolean
        '------------------------------------- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lreccreLettAccuse As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lreccreLettAccuse = New eRemoteDB.Execute

        With lreccreLettAccuse
            .StoredProcedure = "updLettRequest2"
            .Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostLT002 = .Run(False)
        End With

        lreccreLettAccuse = Nothing

        Exit Function
        lreccreLettAccuse = Nothing
    End Function
End Class






