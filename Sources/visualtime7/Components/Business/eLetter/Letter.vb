Option Strict Off
Option Explicit On

Imports System.Transactions
Imports Word = Microsoft.Office.Interop.Word

Public Class Letter
	'**+Objetive: Clase generada a partir de la tabla 'TAB_LETTERS' que es Modelos de cartas de correspondencia personalizados.Un registro por modelo de carta.
	'**+Version: $$Revision: 3 $
	'+Objetivo: Clase generada a partir de la tabla 'TAB_LETTERS'  Customized letter formats.A record per every letter format
	'+Version: $$Revision: 3 $
	
	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_FIELDSTART_TAG As String = "\field"
	
	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_OPENFIELD_CHAR As String = "{"
	
	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_CLOSEFIELD_CHAR As String = "}"
	
	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_SEPARATOR As Short = &H7Cs
	
	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_ESC_VAL As String = "\'"
	
	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_MERGE_TAG As String = "MERGEFIELD"
	
	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_MERGE_TAG_LEN As Short = 11
	
	'**-Objective: Constant, route of generated storage of the letter.
	'-Objetivo: Constante, ruta de almacenamiento de las carta generadas.
    Private Const C_DOC_PATH As String = "c:\InetPub\wwwroot\VTimeNet\TFiles"
	
	'**-Objective: Constant, number of characters to take.
	'-Objetivo: Constante, numero de caracteres a tomar.
	Private Const C_NUM_PARAMETERS As Short = 10
	
	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_ATTR As String = "\hich\af2\dbch\af23\loch\f2"
	
	'**-Objective: Constant, possible letters models according to its receiver - Beneficiary.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Beneficiario.
	Private Const CN_BENEF As Short = 0
	
	'**-Objective: Constant, possible letters models according to its receiver - Intermediary.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Intermediario.
	Private Const CN_INTERMEDIA As Short = 1
	
	'**-Objective: Constant, possible letters models according to its receiver - Client.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Cliente.
	Private Const CN_CLIENT As Short = 2
	
	'**-Objective: Constant, possible letters models according to its receiver - Policy.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Poliza/Cobertura.
	Private Const CN_POLICY As Short = 3
	
	'**-Objective: Constant, possible letters models according to its receiver - Receiving.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Receptoria.
	Private Const CN_RECEIPT As Short = 4
	
	'**-Objective: Constant, possible letters models according to its receiver - Claim.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Siniestro.
	Private Const CN_CLAIM As Short = 5
	
	'**-Objective: Constant, possible letters models according to its receiver - Intervention professional.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Intervención profesional.
	Private Const CN_INTERPROF As Short = 6
	
	'**-Objective: Constant, sent of correspondence of individual.
	'-Objetivo: Constante, envio de correspondencia individual.
	Private Const CN_INDIVIDUAL As Short = 1
	
	'**-Objective: Constant, sent of correspondence of masive.
	'-Objetivo: Constante, envio de correspondencia masiva.
	Private Const CN_MASIVE As Short = 2
	
	'**-Objective: Number identifying the letter template.
	'-Objetivo: Código del modelo de carta.
	Public nLetterNum As Short
	
	'**-Objective: Description of the letter template.
	'-Objetivo: Descripción del modelo de carta.
	Public sDescript As String
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Public dEffecDate As Date
	
	'**-Objective: Date when the record is cancelled.
	'-Objetivo: Fecha de anulación del registro.
	Public dNullDate As Date
	
	'**-Objective: Code of the language in which the data are expressed.Sole values as per table 85
	'-Objetivo: Lenguaje en que se muestra la información del sistema.Valores únicos según tabla 85
    Public nLanguage As Short = 0
	
	'**-Objective: Code of the language in which the data are expressed.Sole values as per table 85
	'-Objetivo: Lenguaje en que se muestra la información del sistema.Valores únicos según tabla 85
    Public nLanguageUsers As Short = 0
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
    Public nUsercode As Double
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
    Public nClient As Double
	
	'**-Objective: Parameter Code. The possible values as per table622.
	'-Objetivo: Codigo del parametro. Posibles valores según la table622
	Public nParameter As Integer
	
	'**-Objective: Parameter Code. The fixed values: 1- Intermediary 2 - Client 3 - Policy/Certificate 4 - Premium invoice 5 - Claim 6 - Intervention professional
	'-Objetivo: Codigo del parametro. Unicos valores: 1- Intermediario 2 - Cliente 3 - Poliza/Certificado 4 - Recepción  5 - Siniestro 6 - Intervención profesional
	Public nParameters As Integer
	
	'**-Objective: Name of document rtf generated during merge
	'-Objetivo: Nombre del documento rtf generado durante el merge
	Public sDocumentName As String
	
	'**-Objective: Variable that stores the direction of email of the applicant.
	'-Objetivo: Variable que almacena la dirección de correo electronico del solicitante.
	Public sEmailUsers As String
	
	'**-Objective: Stores the final result of merge.
	'-Objetivo: Almacena el resultado final de merge.
	Public sMergeResult As String
	
	'**-Objective: Correspondence Control Indicator.Sole Values: 1 - Affirmative 2 - Negative
	'-Objetivo: Indicador de control (seguimiento) de la correspondencia.Valores únicos: 1 - Afirmativo 2 - Negativo
	Public sCtroLettInd As String
	
	'**-Objective: Delivery to invalid address indicator. 1 - Affirmative; 2 - Negative
	'-Objetivo: Indicador de envio a direcciones invalidas .Valores únicos: 1 - Afirmativo 2 - Negativo
	Public sDelivInvalidInd As String
	
	'**-Objective: Response time.
	'-Objetivo: Tiempo de respuesta. Tiempo máximo que debe esperar el sistema por una respuesta del destinatario.
	Public nMinTimeAns As Short
	
	'**-Objective: Collection of parametros that compose a letter
	'-Objetivo: Colección de parametros que componen una carta
	Public oParameters As Collection
	
	'**-Objective:Collection of values that will be replaced in the correspondence to generate
	'-Objetivo: Colección de valores que serán sustituidos en la correspondencia a generar
	Public oTables As Collection
	
	'**-Objective: Variable of temporary use, stores the letter model
	'-Objetivo: Variable de uso temporal, almacena el modelo de carta
	Private mstrLetter As String
	
    Private lstrAction As String

    Public nValue As Integer

	
	'**-Objective: Variable of temporary use, stores type of letter model
	'-Objetivo: Variable de uso temporal, almacena tipo de modelo de carta
	Private nOldLetterNum As Integer
	
	'**-Objective: Variable of temporary use, stores to the type lenguage used.
	'-Objetivo: Variable de uso temporal, almacena el tipo el lenguage empleado.
	Private nOldLanguage As Integer
	
	'**-Objective: Response time.
	'-Objetivo: Tiempo de respuesta.
	Public Enum eUpdateKind
		eSameDay = 0
		eLaterDate = 1
	End Enum
	
	'**-Objective: Local variable(s) to hold property value(s)
	'-Objetivo: Variable local, para alamcenar valores de los parametros.
	Private mvarParameters As LettParams
	
	'**-Objective: local variable(s) to hold property value(s)
	'-Objetivo: local variable(s) to hold property value(s)
	Public mobjletter As Letter
	
	'**-Objective: Group of variables
	'-Objetivo: Bloque de variables
	Private Structure eTypePrepare
		Dim eRecordset As eRemoteDB.Execute
		Dim eExist As Boolean
	End Structure
	
	'**-Objective: It indicates if the letter template whether contains a
	'**-mailing address or not.
	'-Objetivo: Indica si la plantilla de carta contiene o no una dirección
	'-de correo
	Private bMailAddress As Boolean
	
	'**-Objective: It indicate the process type ejecution (validation(True), Posted(false))
	'-Objetivo: Indica el tipo de proceso en ejecución (Validación(Verdadero), Actualización(Falso))
	'-de correo
	Private bValidate As Boolean
	
	'**-Objective: It indicate if the letter template has variables of the beneficiary(13) group of variables
	'-Objetivo: Indica si el modelo de cartas tiene variables del grupo de variables del beneficiario(13)
	Private bBeneficiary As Boolean


	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
	Private Const C_ENCLOSE_MERGEFIELD As String = "\hich\af0\dbch\af31505\loch\f0"

	'**-Objective: Constant, part of the code of the letter models.
	'-Objetivo: Constante, parte del código de los modelos de cartas.
    Private Const C_RIGHT_SIDE_VARIABLE as String = "\loch\af0\dbch\af31505\hich\f0" 

	
	'**%Objective:  Used when assigning an Object to the property, on the left side of a Set statement.Syntax: Set x.Parameters = Form1
	'**%Parameters:
	'**%  vData - Contains parameter and the value that is adjudged.
	'%Objetivo: Utilizado al asignar las propiedades de un objeto, al lado izqueirdo de la sentencia .Sintaxis: Set x.Parameters = Form1
	'%Parámetros:
	'%    vData - Contiene el parametro y el valor que se adjudica.
	
	'**%Objective: Used when retrieving value of a property, on the right side of an assignment.Syntax: Debug.Print X.Parameters
	'%Objetivo: Usado para recuperar el valor de una propiedad, sobre el lado derecho de una asignación.Syntaxis: Debug.Print X.Parameters
	Public Property LettParameters() As LettParams
		Get
			If Not IsIDEMode Then
			End If
			
			If mvarParameters Is Nothing Then
				mvarParameters = New LettParams
				mvarParameters.FindByLetter(nLetterNum, dEffecDate)
			End If
			LettParameters = mvarParameters
			
			Exit Property
		End Get
		Set(ByVal Value As LettParams)
			If Not IsIDEMode Then
			End If
			
			mvarParameters = Value
			
			Exit Property
		End Set
	End Property
	
	'**%Objective: Obtain the correspondence stored according to the selection criterion
	'%Objetivo: Obtener la correspondencia almacenada según el criterio de selección
	
	
	'**%Objective: Store to the code of the client and the content of the letter in temporary variables.
	'%Objetivo: Almacenar el código del cliente y el contenido de la carta en variables temporales.
	'-----------------------------------------------------------
	Public Property tletter() As String
		Get
            '			Dim lobjtLetter As eRemoteDB.Letter
            Dim lobjtLetter As eRemoteDB.Letter
            Dim lobjtLetter_Ex As eRemoteDB.Execute
            Dim mobjValues As eFunctions.Values
            mobjValues = New eFunctions.Values


			If Not IsIDEMode Then
			End If
			
			If nOldLetterNum <> nLetterNum Then
                lobjtLetter = New eRemoteDB.Letter
                lobjtLetter_Ex = New eRemoteDB.Execute

                lobjtLetter.SQL = "select tletter from " & lobjtLetter_Ex.Owner & IIf(InStr(1, lobjtLetter_Ex.Owner, ".") > 0, "", ".") & "lettlanguage where nLetterNum=" & nLetterNum
                lobjtLetter.SQL = lobjtLetter.SQL & " AND nLanguage=" & nLanguageUsers
                'If lobjtLetter.Server = 2 Then
                'lobjtLetter.SQL = lobjtLetter.SQL & " AND dEffecDate <=" & "To_Char('" & Today.ToString("dd/MM/yyyy") & "')" & "   AND (dNullDate IS NULL " & "    OR dNullDate > " & "To_Char('" & Today.ToString("dd/MM/yyyy") & "'))"
                ''lobjtLetter.SQL = lobjtLetter.SQL & " AND dEffecDate <=" & "'" & Today.ToString("dd/MM/yyyy") & "'" & " AND (dNullDate IS NULL " & " OR dNullDate > " & "'" & Today.ToString("dd/MM/yyyy") & "')"""
                'Else
                '   lobjtLetter.SQL = lobjtLetter.SQL & " AND dEffecDate <='" & Today.ToString("MM/dd/yyyy") & "'" & " AND (dNullDate IS NULL " & "  OR dNullDate >'" & Today.ToString("MM/dd/yyyy") & "')"
                'End If

                If lobjtLetter.findLettLanguage(mobjValues.StringToType(Me.nLetterNum, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Me.nLanguageUsers, eFunctions.Values.eTypeData.etdInteger)) Then
                    'lobjtLetter.Run Then

                    nOldLetterNum = Me.nLetterNum
                    nOldLanguage = Me.nLanguageUsers
                    mstrLetter = lobjtLetter.tletter
                    tletter = mstrLetter
                    lobjtLetter_Ex.RCloseRec()
                Else
                    tletter = String.Empty
                End If





                'lobjtLetter = New eRemoteDB.Letter
                'lobjtLetter.SQL = "SELECT tletter FROM LETTLANGUAGE WHERE nLetterNum=" & nLetterNum & "   AND nLanguage=" & nLanguage & "   AND dEffecDate <=" & "To_Char('" & Today & "')" & "   AND (dNullDate IS NULL " & "    OR dNullDate > " & "To_Char('" & Today & "'))"

                'If lobjtLetter.FindtLetter Then

                'nOldLetterNum = Me.nLetterNum
                'nOldLanguage = Me.nLanguage
                'mstrLetter = lobjtLetter.tletter
                'tletter = mstrLetter

                'End If
            Else
                tletter = mstrLetter
            End If

            lobjtLetter = Nothing
            lobjtLetter_Ex = Nothing

            Exit Property
		End Get
		Set(ByVal Value As String)
			'-----------------------------------------------------------
			If Not IsIDEMode Then
			End If
			
			mstrLetter = Value
			nOldLetterNum = nLetterNum
			
			Exit Property
		End Set
	End Property
	
	'**%Objective: Make the search of a letter model according to the selected criterion of search
	'**%Parameters:
	'**%  nLetterNum     - Number identifying the letter template.
	'**%  dEffecDate     - Date which from the record is valid.
	'%Objetivo: Realizar la busqueda de un modelo de carta según el criterio de busqueda seleccionado.
	'%Parámetros:
	'%    nLetterNum     - Código del modelo de carta.
	'%    dEffecDate     - Fecha de efecto del registro.
    Public Function Find(ByVal nLetterNum As Double, ByVal nLanguage As Short, ByVal dEffecDate As Date) As Boolean
        Dim lrecreaTab_Letters As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lrecreaTab_Letters = New eRemoteDB.Execute

        Find = True
        With lrecreaTab_Letters
            .StoredProcedure = "reaTab_Letters"
            .Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLanguage", nLanguage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.nLetterNum = nLetterNum
                Me.sDescript = .FieldToClass("sDescript")
                Me.dEffecDate = .FieldToClass("dEffecDate")
                Me.dNullDate = .FieldToClass("dNullDate")
                Me.nLanguageUsers = .FieldToClass("nLanguage")
                Me.sCtroLettInd = .FieldToClass("sCtroLettInd")
                Me.nMinTimeAns = .FieldToClass("nMinTimeAns")
                Me.nParameter = .FieldToClass("nP1")
                Me.nParameters = IIf(.FieldToClass("nP1") <> .FieldToClass("nP2"), 1, 0)
                Me.sDelivInvalidInd = .FieldToClass("sDelivInvalid")

                '**+ whiten the collection that contains the values associated to the parameters of a letter model because
                '**+ have been made a new searchfor another model
                '+ Se blanquea la colección que contiene los valores asociados a los parámetros de un modelo de carta pues se ha realizado
                '+ una nueva búsqueda para otro modelo.

                mvarParameters = Nothing

                .RCloseRec()
            Else
                Find = False
            End If
        End With

        Exit Function
        mvarParameters = Nothing
    End Function
	
	'**%Objective: This function is in charge of adding information to the TAB_LETTERS table
	'**%Parameters:
	'**%  nLetterNum    - Number identifying the letter template.
	'**%  sDescript     - Description of the letter template.
	'**%  dEffecDate    - Date which from the record is valid.
	'**%  nLanguage     - Code of the language in which the data are expressed.Sole values as per table 85
	'**%  tLetter       - Content of the letter.
	'**%  nUsercode     - Code of the user creating or updating the record.
	'**%  sCtroLettInd  - Correspondence Control Indicator.Sole Values: 1 - Affirmative 2 - Negative
	'**%  nMinTimeAns   - Response time.
	'**%  sDelivInvalid - Delivery to invalid address indicator.
	'%Objetivo: Esta función se encarga de agregar información en la tabla principal de la clase TAB_LETTERS
	'%Parámetros:
	'%    nLetterNum    - Código del modelo de carta.
	'%    sDescript     - Descripción del modelo de carta.
	'%    dEffecDate    - Fecha de efecto del registro.
	'%    nLanguage     - Lenguaje en que se muestra la información del sistema.Valores únicos según tabla 85
	'%    tLetter       - Contenido de la carta.
	'%    nUsercode     - Código del usuario que crea o actualiza el registro.
	'%    sCtroLettInd  - Indicador de control (seguimiento) de la correspondencia.Valores únicos: 1 - Afirmativo 2 - Negativo
	'%    nMinTimeAns   - Tiempo de respuesta. Tiempo máximo que debe esperar el sistema por una respuesta del destinatario.
	'%    sDelivInvalid  - Indicador de envío a direcciones invalidas
    Private Function Add(ByVal nLetterNum As Double, ByVal sDescript As String, ByVal dEffecDate As Date, ByVal nLanguage As Short, ByVal tletter As String, ByVal nUsercode As Double, ByVal sCtroLettInd As String, ByVal nMinTimeAns As Short, ByVal sDelivInvalid As String) As Boolean
        Dim lreccreTab_Letters As eRemoteDB.Execute
        Dim ltnLetterNum As Double
        Dim lclsGroupV As eLetter.GroupVariables
        Dim sql As String = ""
        Dim nCount As Integer
        Dim lQuery As eRemoteDB.Query
        Dim dLoc_Effecdate As Date

        If Not IsIDEMode() Then
        End If

        lreccreTab_Letters = New eRemoteDB.Execute
        lclsGroupV = New eLetter.GroupVariables

        If nLetterNum <> intNull Then
            Me.nLetterNum = nLetterNum
        End If

        If sDescript <> String.Empty Then
            Me.sDescript = sDescript
        End If

        If tletter <> String.Empty Then
            Me.tletter = tletter
        End If

        If nLanguage <> intNull Then
            Me.nLanguage = nLanguage
        End If

        If nUsercode <> intNull Then
            Me.nUsercode = nUsercode
        End If

        If sCtroLettInd <> String.Empty Then
            Me.sCtroLettInd = sCtroLettInd
        End If

        If nMinTimeAns <> intNull Then
            Me.nMinTimeAns = nMinTimeAns
        Else
            Me.nMinTimeAns = intNull
        End If

        If dEffecDate <> dtmNull Then
            Me.dEffecDate = dEffecDate
        Else
            Me.dEffecDate = Today
        End If

        If sDelivInvalid = "1" Then
            Me.sDelivInvalidInd = "1"
        Else
            Me.sDelivInvalidInd = "2"
        End If


        '**+ The letter variables used in the template are found and transferred as
        '**+ parameters to the routine whereby letter template parameters are obtained
        '+ Se extraen las variables de cartas utilizadas en el modelo y se pasan
        '+ como parámetros a la rutina que permite obtener los parámetros del modelo

        setParameters()

        '**+ Once obtained, the letter template parameters are recorded in the Parameters
        '**+ Required in a Letter Template table (LettParam), taking into account that only
        '**+ the parameter with the lowest internal code must be recorded in addition to
        '**+ the beneficiary 's parameter (if any).
        '+ Al obtener los parámetro(s) del modelo de carta, se registran en la Tabla
        '+ de Parámetros Requeridos por el Modelo de Carta (LettParam), tomando en
        '+ cuenta que sólo se debe registrar un parámetro (el que tenga el menor código
        '+ interno), además del parámetro de beneficiario, en el caso de que aplique.

        ltnLetterNum = 2

        '+ No se utiliza el procedimiento creTab_Letters porque hay un problema con el tamaño de los parámetros tipo CLOB,
        '+ por lo cual no se pueden agregar imágenes al modelo de carta utilizando procedimientos de Oracle.
        'Using scope As New TransactionScope(TransactionScopeOption.RequiresNew, New TimeSpan(0, 3, 0, 0, 0))
        With lreccreTab_Letters
            .StoredProcedure = "reaTab_Letters"
            .Parameters.Add("nLetterNum", Me.nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLanguage", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Me.dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                ltnLetterNum = 1
                .RCloseRec()
            End If
        End With

        lreccreTab_Letters.Connection = Nothing
        lreccreTab_Letters = Nothing


        lQuery = New eRemoteDB.Query

        If lQuery.OpenQuery("TAB_LETTERS", "COUNT(NLETTERNUM)", "NLETTERNUM=" & Me.nLetterNum.ToString) Then
            nCount = lQuery.FieldToClass("COUNT(NLETTERNUM)")
        End If

        If nCount = 0 Then
            sql = "INSERT INTO TAB_LETTERS (NLETTERNUM,  SDESCRIPT, SCTROLETTIND," & _
                                           "NMINTIMEANS, DCOMPDATE, NUSERCODE, SDELIVINVALID_IND)" & _
                                   "VALUES (" & Me.nLetterNum & ",'" & Me.sDescript & "','" & Me.sCtroLettInd & "'," & _
                                            Me.nMinTimeAns & ",SYSDATE," & Me.nUsercode & ",'" & Me.sDelivInvalidInd & "')"
        Else
            sql = "UPDATE TAB_LETTERS " & _
                    "SET NUSERCODE = " & Me.nUsercode & "," & _
                        "SCTROLETTIND = '" & Me.sCtroLettInd & "'," & _
                        "NMINTIMEANS = " & Me.nMinTimeAns & "," & _
                        "DCOMPDATE = SYSDATE," & _
                        "SDESCRIPT = '" & Me.sDescript & "'," & _
                        "SDELIVINVALID_IND = '" & Me.sDelivInvalidInd & "' " & _
                   "WHERE NLETTERNUM = " & Me.nLetterNum
        End If

        lreccreTab_Letters = New eRemoteDB.Execute
        lreccreTab_Letters.SQL = sql
        lreccreTab_Letters.Run(False)

        lQuery = New eRemoteDB.Query

        If lQuery.OpenQuery("LETTLANGUAGE", _
                            "DEFFECDATE", _
                            "NLETTERNUM = " & Me.nLetterNum & _
                      "AND NLANGUAGE  =  " & Me.nLanguage & _
                            " AND DEFFECDATE <= TRUNC(SYSDATE) " & _
                            "AND (DNULLDATE IS NULL " & _
                            "OR DNULLDATE  > TRUNC(SYSDATE))") Then

            dLoc_Effecdate = lQuery.FieldToClass("DEFFECDATE")

            If dLoc_Effecdate <> dtmNull Then
                If dLoc_Effecdate = Me.dEffecDate Then
                    lreccreTab_Letters = New eRemoteDB.Execute
                    lreccreTab_Letters.SQL = "DELETE " & _
                                               "FROM LETTPARAM " & _
                                              "WHERE NLETTERNUM = " & Me.nLetterNum & _
                                         " AND NLANGUAGE  = " & Me.nLanguage & _
                                         " AND DEFFECDATE = TRUNC(SYSDATE)"
                    lreccreTab_Letters.Run(False)

                    lreccreTab_Letters = New eRemoteDB.Execute
                    lreccreTab_Letters.SQL = "DELETE " & _
                                               "FROM LETTLANGUAGE " & _
                                              "WHERE NLETTERNUM = " & Me.nLetterNum & _
                                         " AND NLANGUAGE  = " & Me.nLanguage & _
                                         " AND DEFFECDATE = TRUNC(SYSDATE)"
                    lreccreTab_Letters.Run(False)
                Else
                    lreccreTab_Letters = New eRemoteDB.Execute
                    lreccreTab_Letters.SQL = "UPDATE LETTLANGUAGE " & _
                                               "SET DNULLDATE = DEFFECDATE," & _
                                                   "NUSERCODE = " & Me.nUsercode & "," & _
                                                   "DCOMPDATE = SYSDATE " & _
                                             "WHERE NLETTERNUM = " & Me.nLetterNum & _
                                         " AND NLANGUAGE  = " & Me.nLanguage & _
                                         " AND DEFFECDATE = TRUNC(SYSDATE)"
                    lreccreTab_Letters.Run(False)
                End If
            End If
        End If

        lreccreTab_Letters = New eRemoteDB.Execute
        lreccreTab_Letters.SQL = "INSERT INTO LETTLANGUAGE (NLETTERNUM, NLANGUAGE, DEFFECDATE," & _
                                                           "TLETTER,    NUSERCODE, DCOMPDATE) " & _
                                                   "VALUES (" & Me.nLetterNum & "," & Me.nLanguage & ",TRUNC(SYSDATE),:TLETTER," & _
                                                                Me.nUsercode & ", SYSDATE)"
        lreccreTab_Letters.Parameters.Add(":TLETTER", CleanLetter((Me.tletter)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        lreccreTab_Letters.Run(False)

        If Me.nParameter <> 0 And Me.nParameter <> intNull Then
            lreccreTab_Letters = New eRemoteDB.Execute
            With lreccreTab_Letters
                .StoredProcedure = "CRELETTPARAM"
                .Parameters.Add("nLetterNum", Me.nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nParameters", nParameter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nLanguage", Me.nLanguage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecDate", Me.dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUserCode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Run(False)
            End With
        End If

        'If Not bBeneficiary Then
        '    lreccreTab_Letters = New eRemoteDB.Execute
        '    With lreccreTab_Letters
        '        .StoredProcedure = "CRELETTPARAM"
        '        .Parameters.Add("nLetterNum", Me.nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '        .Parameters.Add("nParameters", IIf(bBeneficiary, 0, 1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '        .Parameters.Add("nLanguage", Me.nLanguage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '        .Parameters.Add("dEffecDate", Me.dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '        .Parameters.Add("nUserCode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '        .Run(False)
        '    End With
        'End If

        lreccreTab_Letters = Nothing

        If ltnLetterNum <> intNull Then
            'scope.Complete()
            Add = True
        Else
            Throw New Exception("Error al agregar modelo de carta")
        End If

        ' End Using

        lreccreTab_Letters = Nothing
        lclsGroupV = Nothing

        Exit Function
        lreccreTab_Letters = Nothing
    End Function
	
	'**%Objective: Makes the update of the registry in the Tab_letters table
	'**%Parameters:
	'**%  nLetterNum    - Number identifying the letter template.
	'**%  sDescript     - Description of the letter template.
	'**%  dEffecDate    - Date which from the record is valid.
	'**%  nLanguage     - Code of the language in which the data are expressed.Sole values as per table 85
	'**%  tLetter       - Content of the letter.
	'**%  nUsercode     - Code of the user creating or updating the record.
	'**%  sCtroLettInd  - Correspondence Control Indicator.Sole Values: 1 - Affirmative 2 - Negative
	'**%  nMinTimeAns   - Response time.
	'**%  sDelivInvalid - Delivery to invalid address indicator.
	'%Objetivo:
	'%Parámetros: Realiza la actualización del registro en la la tabla tab_letters
	'%    nLetterNum    - Código del modelo de carta.
	'%    sDescript     - Descripción del modelo de carta.
	'%    dEffecDate    - Fecha de efecto del registro.
	'%    nLanguage     - Lenguaje en que se muestra la información del sistema.Valores únicos según tabla 85
	'%    tLetter       - Contenido de la carta.
	'%    nUsercode     - Código del usuario que crea o actualiza el registro.
	'%    sCtroLettInd  - Indicador de control (seguimiento) de la correspondencia.Valores únicos: 1 - Afirmativo 2 - Negativo
	'%    nMinTimeAns   - Tiempo de respuesta. Tiempo máximo que debe esperar el sistema por una respuesta del destinatario.
	'%    sDelivInvalid  - Indicador de envío a direcciones invalidas
    Private Function Update(Optional ByVal nLetterNum As Double = -32768, Optional ByVal sDescript As String = "", Optional ByVal dEffecDate As Date = #12:00:00 AM#, Optional ByVal nLanguage As Short = -32768, Optional ByVal tletter As String = "", Optional ByVal nUsercode As Double = -32768, Optional ByVal sCtroLettInd As String = "", Optional ByVal nMinTimeAns As Short = 0, Optional ByVal sDelivInvalid As String = "") As Boolean
        Dim lreccreTab_Letters As eRemoteDB.Execute
        Dim lrecupdTab_Letters As eRemoteDB.Execute
        Dim nLaterDate As Short

        If Not IsIDEMode() Then
        End If

        lrecupdTab_Letters = New eRemoteDB.Execute

        If nLetterNum <> intNull Then
            Me.nLetterNum = nLetterNum
        End If

        If sDescript <> String.Empty Then
            Me.sDescript = sDescript
        End If

        If tletter <> String.Empty Then
            Me.tletter = tletter
        End If

        If nLanguage <> intNull Then
            Me.nLanguageUsers = nLanguage
        End If

        If nUsercode <> intNull Then
            Me.nUsercode = nUsercode
        End If

        If sCtroLettInd <> String.Empty Then
            Me.sCtroLettInd = sCtroLettInd
        End If

        If nMinTimeAns <> intNull Then
            Me.nMinTimeAns = nMinTimeAns
        End If

        If dEffecDate <> dtmNull Then
            Me.dEffecDate = dEffecDate
        End If

        If Me.dEffecDate = Today Then
            'nLaterDate = CInt(eSameDay)
            nLaterDate = 0
        Else
            'nLaterDate = eLaterDate
            nLaterDate = 1
            Me.dEffecDate = Today
        End If

        If sDelivInvalid = "1" Then
            Me.sDelivInvalidInd = "1"
        Else
            Me.sDelivInvalidInd = "2"
        End If

        '**+ The letter variables used in the template are found and transferred as
        '**+ parameters to the routine whereby letter template parameters are obtained
        '+ Se extraen las variables de cartas utilizadas en el modelo y se pasan
        '+ como parámetros a la rutina que permite obtener los parámetros del modelo

        setParameters()

        '**+ Once obtained, the letter template parameters are recorded in the Parameters
        '**+ Required in a Letter Template table (LettParam), taking into account that only
        '**+ the parameter with the lowest internal code must be recorded in addition to
        '**+ the beneficiary 's parameter (if any).
        '+ Al obtener los parámetro(s) del modelo de carta, se registran en la Tabla
        '+ de Parámetros Requeridos por el Modelo de Carta (LettParam), tomando en
        '+ cuenta que sólo se debe registrar un parámetro (el que tenga el menor código
        '+ interno), además del parámetro de beneficiario, en el caso de que aplique.


        With lrecupdTab_Letters
            .StoredProcedure = "updTab_Letters"
            .Parameters.Add("nLetterNum", Me.nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Me.dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLanguage", Me.nLanguageUsers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("tLetter", CleanLetter((Me.tletter)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCtroLettInd", Me.sCtroLettInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinTimeAns", Me.nMinTimeAns, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLaterDate", nLaterDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nParameter1", nParameter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nParameter2", IIf(bBeneficiary, 0, 1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SDELIVINVALID", Me.sDelivInvalidInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With
        lrecupdTab_Letters = Nothing

        lreccreTab_Letters = New eRemoteDB.Execute
        lreccreTab_Letters.SQL = "INSERT INTO LETTLANGUAGE (NLETTERNUM, NLANGUAGE, DEFFECDATE," & _
                                                           "TLETTER,    NUSERCODE, DCOMPDATE) " & _
                                                   "VALUES (" & Me.nLetterNum & "," & Me.nLanguageUsers & ",TRUNC(SYSDATE),:TLETTER," & _
                                                                Me.nUsercode & ", SYSDATE)"
        lreccreTab_Letters.Parameters.Add(":TLETTER", CleanLetter((Me.tletter)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        lreccreTab_Letters.Run(False)

        Exit Function
        lrecupdTab_Letters = Nothing
        lreccreTab_Letters = Nothing
    End Function
	
	'**Objective: Delete the information in the main table of the class.
	'**%Parameters:
	'**%  nLetterNum   - Number identifying the letter template.
	'**%  dEffecDate   - Date which from the record is valid.'
	'**%  nUsercode    - Code of the user creating or updating the record.
	'%Objetivo: Esta función se encarga de eliminar información en la tabla principal de la clase.
	'%Parámetros:
	'%    nLetterNum   - Código del modelo de carta.
	'%    dEffecDate   - Fecha de efecto del registro.'
	'%    nUsercode    - Código del usuario que crea o actualiza el registro.
    Public Function Delete(ByVal nLetterNum As Double, ByVal dEffecDate As Date, ByVal nLanguage As Short, ByVal nUsercode As Double) As Boolean
        Dim lrecdelTab_Letters As eRemoteDB.Execute
        Dim nLaterDate As eUpdateKind

        If Not IsIDEMode() Then
        End If

        lrecdelTab_Letters = New eRemoteDB.Execute

        If dEffecDate = Today Then
            ' nLaterDate = eSameDay
            nLaterDate = 0
        Else
            'nLaterDate = eLaterDate
            nLaterDate = 0
        End If

        With lrecdelTab_Letters
            .StoredProcedure = "delTab_Letters"
            .Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLanguage", nLanguage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLaterDate", nLaterDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Delete = True
            End If
        End With
        lrecdelTab_Letters = Nothing

        Exit Function
        lrecdelTab_Letters = Nothing
    End Function
	
	'**%Objective: Validate the content of page LT001
	'**%Parameters:
	'**%  sAction       - Description of the action to execute.       -
	'**%  nLetterNum    - Number identifying the letter template.
	'**%  sDescript     - Description of the letter template.
	'**%  dEffecDate    - Date which from the record is valid.
	'**%  nLanguage     - Code of the language in which the data are expressed.Sole values as per table 85
	'**%  tLetter       - Content of the letter.
	'**%  nUsercode     - Code of the user creating or updating the record.
	'**%  sCtroLettInd  - Correspondence Control Indicator.Sole Values: 1 - Affirmative 2 - Negative
	'**%  nMinTimeAns   - Response time.
	'%Objetivo: Se encarga de validar el contenido de la pagina LT001
	'%Parámetros:
	'%    sAction       - Descripción de la acción a ejecutarse.
	'%    nLetterNum    - Código del modelo de carta.
	'%    sDescript     - Descripción del modelo de carta.
	'%    dEffecDate    - Fecha de efecto del registro.
	'%    nLanguage     - Lenguaje en que se muestra la información del sistema.Valores únicos según tabla 85
	'%    tLetter       - Contenido de la carta.
	'%    nUsercode     - Código del usuario que crea o actualiza el registro.
	'%    sCtroLettInd  - Indicador de control (seguimiento) de la correspondencia.Valores únicos: 1 - Afirmativo 2 - Negativo
	'%    nMinTimeAns   - Tiempo de respuesta. Tiempo máximo que debe esperar el sistema por una respuesta del destinatario
    Public Function Validate(ByVal sAction As String, ByVal nLetterNum As Double, ByVal sDescript As String, ByVal dEffecDate As Date, ByVal nLanguage As Short, ByVal tletter As String, ByVal nUsercode As Double, ByVal sCtroLettInd As String, ByVal nMinTimeAns As Short) As String
        Dim lobjErrors As eFunctions.Errors

        If Not IsIDEMode() Then
        End If

        lobjErrors = New eFunctions.Errors

        '**+ Validations on the letter model
        '+ Validaciones sobre el modelo de carta
        If nLetterNum = intNull Then
            lobjErrors.ErrorMessage("LT001", 8204)
        ElseIf sAction = "Add" Then
            If Find(nLetterNum, nLanguage, dEffecDate) Then
                lobjErrors.ErrorMessage("LT001", 8089)
            End If
        End If

        '**+ Validations on the field decripción
        '+ Validaciones sobre el campo decripción
        If sDescript = String.Empty Then
            lobjErrors.ErrorMessage("LT001", 10010)
        End If

        '**+ Validations on the field language
        '+ Validaciones sobre el campo idioma
        If nLanguage = 0 Then
            lobjErrors.ErrorMessage("LT001", 10007)
        End If

        '**+ Validations on the field file name
        '+ Validaciones sobre el campo ruta del archivo
        'If sAction = "Add" And (tletter = String.Empty Or tletter = "") Then
        '	lobjErrors.ErrorMessage("LT001", 8340)
        '	''lobjErrors.ErrorMessage "LT001", 1401
        'End If

        'If sAction = "Update" And (tletter = String.Empty Or tletter = "") Then
        '	lobjErrors.ErrorMessage("LT001", 8340)
        '	''lobjErrors.ErrorMessage "LT001", 1401
        'End If


        If sAction <> "Del" And tletter <> String.Empty Then
            Me.tletter = tletter
            bValidate = True

            '**+The letter variables used in the template are found and transferred as
            '**+parameters to the routine whereby letter template parameters are obtained
            '**+(insLetterParameters). If this routine returns the mail address indicator set
            '**+as "False", then an error message must be sent (process stops).

            '+Se extraen las variables de cartas utilizadas en el modelo y se pasan como
            '+parámetros a la rutina que permite obtener los parámetros del modelo
            '+(insLetterParameters). Si al ejecutar este procedimeinto el Indicador
            '+de dirección de correo es "Falso" el proceso se detiene y se envia un mensaje
            '+ de error

            setParameters()

            '**+ The letter template must contain a mail address
            '+ El modelo de carta debe contener una dirección de correo

            '        If Not bMailAddress Then
            '            lobjErrors.ErrorMessage "LT001", 30426
            '        End If

        End If

        Validate = lobjErrors.Confirm

        lobjErrors = Nothing

        Exit Function
    End Function
	
	'**%Objective: Stores, updates or eliminates the content of the LT001 in the data base
	'**%Parameters:
	'**%  sAction       - Description of the action to execute.
	'**%  nLetterNum    - Number identifying the letter template.
	'**%  sDescript     - Description of the letter template.
	'**%  dEffecDate    - Date which from the record is valid.
	'**%  nLanguage     - Code of the language in which the data are expressed.Sole values as per table 85
	'**%  tLetter       - Content of the letter.
	'**%  nUsercode     - Code of the user creating or updating the record.
	'**%  nMinTimeAns   - Response time.
	'**%  sDelivInvalid - Delivery to invalid address indicator.
	'%Objetivo: Almacena, actualiza o elimina el contenido de la LT001 en la base de datos
	'%Parámetros:
	'%    sAction       - Descripción de la acción a ejecutarse.
	'%    nLetterNum    - Código del modelo de carta.
	'%    sDescript     - Descripción del modelo de carta.
	'%    dEffecDate    - Fecha de efecto del registro.
	'%    nLanguage     - Lenguaje en que se muestra la información del sistema.Valores únicos según tabla 85
	'%    tLetter       - Contenido de la carta.
	'%    nUsercode     - Código del usuario que crea o actualiza el registro.
	'%    nMinTimeAns   - Tiempo de respuesta. Tiempo máximo que debe esperar el sistema por una respuesta del destinatario.
	'%    sDelivInvalid  - Indicador de envío a direcciones invalidas
    Public Function insPostLT001(ByVal sAction As String, ByVal nLetterNum As Double, ByVal sDescript As String, ByVal dEffecDate As Date, ByVal nLanguage As Short, ByVal tletter As String, ByVal nUsercode As Double, ByVal sCtroLettInd As String, ByVal nMinTimeAns As Short, ByVal sDelivInvalid As String) As Boolean
        If Not IsIDEMode() Then
        End If

        Select Case sAction
            Case "Add"
                insPostLT001 = Add(nLetterNum, sDescript, dEffecDate, nLanguage, tletter, nUsercode, sCtroLettInd, nMinTimeAns, sDelivInvalid)
            Case "Update"
                insPostLT001 = Update(nLetterNum, sDescript, dEffecDate, nLanguage, tletter, nUsercode, sCtroLettInd, nMinTimeAns, sDelivInvalid)

            Case "Delete"
                insPostLT001 = Delete(nLetterNum, dEffecDate, nLanguage, nUsercode)
        End Select

        Exit Function
    End Function
	
	'**%Objective: Look for the elements merge that they are contained in the letter model
	'**% The parameters definitios for the letter templates
	'**% (Routines insletterparaments according to the functional), is made at the class
	'**% level by the execution of privates procedures/functions initialized by "setParameters"
	'%Objetivo: Buscar los elementos merge que estan contenidos en el modelo de carta
	'%La definición de parametros para los modelos de carta
	'%(Rutina insLetterParameters según funcional) se realiza a nivel
	'%de la clase por la ejecución de funciones/procedimientos privados
	'%iniciados por "setParameters"
	'-----------------------------------------------
	Private Function setParameters() As Boolean
		'-----------------------------------------------
		If Not IsIDEMode Then
		End If
		
		ScanDocument(tletter)
		
		Exit Function
	End Function
	
	'**%Objective: Look for the elements merge that they are contained in the letter model
	'**%Parameters:
	'**%  sFile - Content of the letter in Word code.
	'%Objetivo: Buscar los elementos merge que estan contenidos en el modelo de carta
	'%Parámetros:
	'%    sFile - Contenido de la carta en código de word.
	Private Sub GetVariables(ByVal sFile As String)
		'--------------------------------------------

		If Not IsIDEMode Then
		End If
		

        ScanDocument(IO.File.ReadAllText(sFile))

        Exit Sub
    End Sub
	
	'**%Objective: Merge extracts the fields that are annexed to the letter model
	'**%Parameters:
	'**%  sFileContent - Content of the letter in Word code.
	'%Objetivo: Extrae los campos merge que se encuentran anexados al modelo de carta
	'%Parámetros:
	'%    sFileContent - Contenido de la carta en código de word.
	Private Sub ScanDocument(ByVal sFileContent As String)
		Dim nIndex As Integer
		Dim sName As String
		Dim nLength As String
		Dim oVariables As Collection
		Dim oVariable As Object
		Dim nResult As Integer
		
		oVariables = New Collection

        On Error Resume Next

		nResult = 32767
		
		nIndex = InStr(nIndex + 1, sFileContent, C_MERGE_TAG)
		
		Do While nIndex > 0
			nIndex = nIndex + C_MERGE_TAG_LEN
			nLength = CStr(InStr(nIndex, sFileContent, "}") - nIndex)
            sName = TranslateName(Mid(sFileContent, nIndex, CInt(nLength)))
            oVariables.Add(sName, sName)

			nIndex = InStr(nIndex + 1, sFileContent, C_MERGE_TAG)
		Loop 
		
		If oVariables.Count() > 0 Then
			For nIndex = 1 To oVariables.Count()
				insLetterParameters(oVariables.Item(nIndex), nResult)
			Next nIndex
			
			Me.nParameters = nResult
			Me.nParameter = getMinParam(nParameters)
		Else
			Me.nParameters = intNull
			Me.nParameter = intNull
		End If
		
        oVariables = Nothing
		
		Exit sub
	End sub
	
	'**%Objective: Extract the name of the field merge.
	'**%Parameters:
	'**%  sPriorName - Name of the field merge to look
	'%Objetivo: Extraer el nombre del campo merge.
	'%Parámetros:
	'%    sPriorName - Nombre del campo merge a buscar
	Private Function TranslateName(ByVal sPriorName As String) As Object
		Dim nIndex As Integer
		Dim sXChar As String
		Dim sXCode As String
		
		If Not IsIDEMode Then
		End If
		
		sPriorName = Replace(sPriorName, "\~", String.Empty)
		sPriorName = Replace(sPriorName, "~", String.Empty)
		sPriorName = Replace(sPriorName, C_ATTR, String.Empty)
		sPriorName = Replace(sPriorName, "\" & Chr(34), String.Empty)
		sPriorName = Replace(sPriorName, Chr(34), String.Empty)
        sPriorName = Replace(sPriorName, C_ENCLOSE_MERGEFIELD, String.Empty)
		nIndex = InStr(1, sPriorName, "\")
		
		Do While nIndex > 0
			nIndex = nIndex + 2
			sXCode = Mid(sPriorName, nIndex, 2)
			sXChar = Chr(CInt("&H" & sXCode))
			sPriorName = Replace(sPriorName, "\" & sXCode, sXChar)
			nIndex = InStr(1, sPriorName, "\")
		Loop 
		
		TranslateName = sPriorName
		Exit Function
	End Function
	
	'**%Objective: Look the equivalent of the field merge in the GroupParams table.
	'**%Parameters:
	'**%  sVariable - Name of the field merge found
	'**%  nResult   - Variable result
	'%Objetivo: Buscar el equivalente del campo merge en la tabla GroupParams.
	'%Parámetros:
	'%    sVariable - Nombre del campo merge encontrado
	'%    nResult   - Variable resultado
	Private Sub insLetterParameters(ByVal sVariable As String, ByRef nResult As Integer)
		Dim oLetter As eLetter.GroupVariables
		Dim oGroupParam As eLetter.GroupParams
		
		If Not IsIDEMode Then
		End If
		
		oLetter = New eLetter.GroupVariables
		oGroupParam = oLetter.FindGroupParams(sVariable)
		
		If Not oGroupParam Is Nothing Then
			With oGroupParam
				
				If bValidate Then
					
					'**+ if a address variable is present (GroupVariables.sTableName = ADDRESS)
					'**+ the "mailing address indicator" is set as "True"
					'**+ Si existe una variable de dirección (GroupVariables.sTableName = ADDRESS)
					'**+ el indicador de dirección de correo es colocado en Verdadero
					
					If UCase(.sTableName) = "ADDRESS" Then
						bMailAddress = True
					End If
				End If
				If .nLett_group <> 80 Then
					nResult = nResult And insConvertToLong(.sParameters)
				End If
				If .nLett_group = 13 Then
					bBeneficiary = True
				End If
			End With
		End If
		
		oLetter = Nothing
		oGroupParam = Nothing
		
		Exit Sub
		oLetter = Nothing
		oGroupParam = Nothing
	End Sub
	
	'**%Objective: Obtain a mathematical result and to determine to that group belongs the letter
	'**%Parameters:
	'**%  sParameters - Variable that contains the value of the field merge.
	'%Objetivo: Obtener un resultado matematico y determinar a que grupo pertenece la carta
	'%Parámetros:
	'%    sParameters - Variable que contiene el valor del campo merge
	Private Function insConvertToLong(ByVal sParameters As String) As Integer
		Dim lintIndex As Short
		
		If Not IsIDEMode Then
		End If
		
		sParameters = Replace(sParameters, "2", "0")
		For lintIndex = 1 To Len(sParameters)
			insConvertToLong = insConvertToLong + CInt(CDbl(Mid(sParameters, lintIndex, 1)) * 2 ^ (lintIndex - 1))
		Next lintIndex
		
		Exit Function
	End Function
	
	'**%Objective: Executes a mathematical function to determine the type of parameters of the letter model.
	'**%Parameters:
	'**%  nValue - Variable that contains the numerical value of the field merge.
	'%Objetivo: Ejecuta una función matemática para determinar el tipo de parametros del modelo de carta.
	'%Parámetros:
	'%    nValue - Variable que contiene el valor numérico del campo merge.
	Private Function getMinParam(ByVal nValue As Integer) As Integer
		Dim lintIndex As Short
		
		If Not IsIDEMode Then
		End If
		
		For lintIndex = 1 To 10
			If nValue And 2 ^ lintIndex Then
				getMinParam = lintIndex
				Exit For
			End If
		Next 
		
		Exit Function
	End Function

    '**%Objective: Generates the correspondences required by the user.
    '**%Parameters:
    '**%  oParameters   - Collection of parameters
    '**%  oLettAccuse   - Collection of generated letters.
    '**%  dEffecDate    - Date which from the record is valid.
    '**%  nUsercode     - Code of the user creating or updating the record.
    '**%  bPrint        - Boolean variable of type which indicates if it is required to print or no.
    '**%  nLetterNum    - Number identifying the letter template.
    '**%  nLanguage     - Code that identifies the language.
    '**%  sPath         - Location where one goes away to store the generated correspondences.
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  bPreview      - Boolean variable of type, indicates if it is due to store or not it generated correspondence.
    '%Objetivo: Genera las correspondencias solicitadas por el usuario.
    '%Parámetros:
    '%  oParameters     - Colección de parametros.
    '%  oLettAccuse     - Colección de las cartas generadas.
    '%  dEffecDate      - Fecha de efecto del registro.
    '%  nUsercode       - Código del usuario que crea o actualiza el registro.
    '%  bPrint          - Variable de tipo booleana el cual indica si se requiere imprimir o no.
    '%  nLetterNum      - Código del modelo de carta.
    '%  nLanguage       - Código que identifica el idioma.
    '%  sPath           - Ubicación donde se va almacenar las correspondencias generadas.
    '%  nLettRequest    - Numero de la solicitud.
    '%  bPreview        - Variable de tipo booleana, indica si se debe almacenar o no la correspondencia generada.
    Public Function MergeDocument(ByVal oParameters As Collection, ByVal oLettAccuse As eLetter.LettAccuse, ByVal dEffecDate As Date, ByVal nUsercode As Double, ByVal bPrint As Boolean, ByVal lintTypeLetter As Short, Optional ByVal nLetterNum As Double = 0, Optional ByVal nLanguage As Short = 0, Optional ByVal sPath As String = "", Optional ByVal nLettRequest As Double = 0, Optional ByVal bPreview As Boolean = False) As Boolean
        Dim lrecreaTab_Letters As eRemoteDB.Execute
        Dim lrecrea_Letter As eRemoteDB.VisualTimeConfig
        Dim nIndex As Integer
        Dim sName As String
        Dim nLength As String
        Dim oVariables As GroupVariabless
        Dim oVariable As GroupVariables
        Dim oTable As eRemoteDB.Execute = Nothing
        Dim sVarList As String = String.Empty
        Dim sFailedList As String = String.Empty
        Dim oFile As IO.StreamWriter
        Dim sFileName As String
        Dim sFileContent As String
        Dim sClient As String = String.Empty
        Dim sValue As String
        Dim bErr As Boolean
        Dim lintLen As Integer
        Dim lintPosBegin As Integer
        Dim lintPosEnd As Integer
        Dim lintPosFind As Integer
        Dim lstrChar As String
        Dim lstrChain1 As String
        Dim lstrChain2 As String
        Dim lintPosT As Integer
        Dim lclsLetters As eLetter.LettRequest
        Dim lcolletterss As eLetter.LettRequests
        Dim lobjEmail As System.Net.Mail.MailMessage 'CDONTS.NewMail
        Dim sEmailUsers As String = String.Empty
        Dim sInvalid_ind As String = String.Empty
        Dim lintStatLetter As Integer
        Dim lngIndexBegin As Integer
        Dim lngIndexFinish As Integer
        Dim lngIndexLast As Integer
        Dim sHostSMTP As String
        Dim sUserSMTP As String
        Dim sPasswordSMTP As String
        Dim sPortSMTP As String
        Dim sDefaultMailFrom As String
        Dim eMailclient As Net.Mail.SmtpClient
        Dim eMailMessage As System.Net.Mail.MailMessage
        Dim lreccreLettAccuse As eRemoteDB.Execute

        Dim fileDateTime As String = "_" & DateTime.Now.ToString("yyyyMMdd") & "_" & DateTime.Now.ToString("HHmmss")

        lrecreaTab_Letters = New eRemoteDB.Execute
        With lrecreaTab_Letters
            .StoredProcedure = "reaUsersClient"
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sEmailUsers = .FieldToClass("sStreet")
                sInvalid_ind = .FieldToClass("sInvalid_ind")
                .RCloseRec()
            End If
        End With
        lrecreaTab_Letters = Nothing

        oVariables = New GroupVariabless

        If oParameters Is Nothing Then
            oParameters = Me.oParameters
        End If
        If nLetterNum <> intNull Then
            If Not Find(nLetterNum, IIf(nLanguage <> intNull, nLanguage, 0), dEffecDate) Then
                If Not Find(nLetterNum, IIf(Me.nLanguageUsers <> intNull, Me.nLanguageUsers, 0), dEffecDate) Then
                    Call Find(nLetterNum, 0, dEffecDate)
                End If
            End If
        End If
        sFileContent = tletter

        lngIndexLast = 0
        nIndex = InStr(nIndex + 1, sFileContent, C_MERGE_TAG)
        Do While nIndex > 0
            oVariable = New GroupVariables

            lngIndexBegin = InStrRev(Left(sFileContent, nIndex), "{\field")
            If lngIndexBegin > lngIndexLast Then
                lngIndexFinish = InStr(nIndex, sFileContent, "}}}")
                If lngIndexFinish > lngIndexBegin Then
                    oVariable.sFldSource = Mid(sFileContent, lngIndexBegin, (lngIndexFinish - lngIndexBegin) + 3)

                    lngIndexBegin = InStr(oVariable.sFldSource, "{\fldrslt ")
                    lngIndexFinish = InStr(lngIndexBegin, oVariable.sFldSource, "}}")
                    If lngIndexFinish > lngIndexBegin Then
                        oVariable.sFldValue = Mid(oVariable.sFldSource, lngIndexBegin + 10, (lngIndexFinish - lngIndexBegin) - 9)
                    End If
                End If
            End If

            ' ''If InStr(nIndex, sFileContent, "\hich\af0\dbch\af31505\loch\f0") > 0 then
            ' ''    nIndex = nIndex + 42
            ' ''    nLength = InStr(nIndex, sFileContent, "\hich\af0\dbch\af31505\loch\f0") - nIndex
            ' ''    sName = TranslateName(Mid(sFileContent, nIndex, CInt(nLength)))
            ' ''Else
            ' ''    nIndex = nIndex + C_MERGE_TAG_LEN
            ' ''    nLength = CStr(InStr(nIndex, sFileContent, "}") - nIndex)
            ' ''    sName = TranslateName(Mid(sFileContent, nIndex, CInt(nLength)))
            ' ''End If
            nIndex = nIndex + C_MERGE_TAG_LEN
            nLength = CStr(InStr(nIndex, sFileContent, "}") - nIndex)
            sName = TranslateName(Mid(sFileContent, nIndex, CInt(nLength)))

            If oVariable.FindByName(sName) Then
                On Error Resume Next
                oVariables.Add(oVariable)
            End If
            oVariable = Nothing
            lngIndexLast = nIndex
            nIndex = InStr(nIndex + 1, sFileContent, C_MERGE_TAG)

        Loop
        oTables = New Collection

        For Each oVariable In oVariables

            If Not Exists(oTables, oVariable.sTableName & oVariable.nLett_group) Then
                oTable = New eRemoteDB.Execute
                With oTable
                    .StoredProcedure = "reaDocVariables"
                    .Parameters.Add("sTable", oVariable.sTableName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nParameter", nParameter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nGroup", oVariable.nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    For nIndex = 1 To C_NUM_PARAMETERS - 1
                        On Error Resume Next
                        .Parameters.Add("sGenericParam" & nIndex, oParameters.Item(nIndex), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        If Err.Number > 0 Then
                            Err.Clear()

                            .Parameters.Add("sGenericParam" & nIndex, System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        End If
                    Next
                    .Parameters.Add("sGenericParam" & C_NUM_PARAMETERS, nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Run(True)
                    oTables.Add(oTable, oVariable.sTableName & oVariable.nLett_group)
                End With
            End If
            bErr = False
            With oVariable
                '*****************************************************************************
                oTables.Item(.sTableName & oVariable.nLett_group).HideErrorMsg = True
                sValue = oTables.Item(oVariable.sTableName & oVariable.nLett_group).FieldToClass(oVariable.sAliasColumn)
                'sValue = 533330
                oTables.Item(.sTableName & oVariable.nLett_group).HideErrorMsg = False

                If sClient = String.Empty And oVariable.sTableName = "CLIENT" And Not Exists(oTables, oVariable.sTableName & oVariable.nLett_group) Then
                    oTable.HideErrorMsg = True
                    sClient = oTables.Item(.sTableName & oVariable.nLett_group).FieldToClass("sClient")
                    oTable.HideErrorMsg = False
                End If

                Err.Clear()
                '*****************************************************************************

                sVarList = sVarList & .sVariable & "=" & sValue & Chr(C_SEPARATOR)

                '**+Merge by its value found in the data base replaces the field
                '+Reemplaza el campo merge por su correspondiente valor encontrado en la BD.
                'sFileContent = Replace(sFileContent, "\'ab" + .sVariable + "\'bb", Trim$(sValue))


                ' ''If InStr(1, .sFldValue, "\hich\af0\dbch\af31505\loch\f0") > 0 then
                ' ''    sFileContent = Replace(sFileContent, .sFldSource, Replace(.sFldValue, "\'ab\hich\af0\dbch\af31505\loch\f0 " & .sVariable & "\loch\af0\dbch\af31505\hich\f0 \'bb", Trim(sValue)))
                ' ''Else
                ' ''    sFileContent = Replace(sFileContent, .sFldSource, Replace(.sFldValue, "\'ab" & .sVariable & "\'bb", Trim(sValue)))
                ' ''End If

                sFileContent = Replace(sFileContent, .sFldSource, Replace(.sFldValue, ReturnFldValue(.sFldValue), Trim(sValue)))

                'sFileContent = sFileContent.ToLower()

                '            ReplaceValue sFileContent, .sVariable, Trim$(sValue), False

                If bErr Then
                    sFailedList = sFailedList & .sVariable & "(" & .sTableName & "." & .sAliasColumn & Chr(C_SEPARATOR)
                End If

            End With
        Next oVariable

        '**+ begin the process of debugging of the letter. they
        '**+ eliminate all the code that belongs to the instruction "\field"
        '+ Se inicia el proceso de depuración de la carta. se eliminan todo el código que
        '+ pertenezca a la instrucción "\field"

        '    If InStr(1, sFileContent, "\field") > 0 Then
        '        Do
        '            lintLen = Len(sFileContent)
        '            lintPosBegin = InStr(1, sFileContent, "\field")
        '            lstrChain1 = Left(sFileContent, lintPosBegin - 1)
        '            lintPosFind = lintPosBegin
        '            lintPosEnd = 0
        '            Do While lintPosFind <= lintLen
        '                lstrChar = Right(Left(sFileContent, lintPosFind + 1), 1)
        '                If lstrChar = "{" Then
        '                    lintPosEnd = lintPosEnd + 1
        '                    If lintPosEnd = 4 Then
        '                        lintPosEnd = lintPosFind + 1
        '                        Exit Do
        '                    End If
        '                End If
        '                lintPosFind = lintPosFind + 1
        '            Loop
        '            lstrChain2 = Right(sFileContent, lintLen - lintPosEnd)
        '            sFileContent = lstrChain1 & lstrChain2
        '            lintPosT = InStr(1, sFileContent, "\field")
        '        Loop Until lintPosT = 0
        '    End If

        sFailedList = "FAILED ={" & sFailedList & "}"
        sVarList = sVarList & sFailedList
        If sPath = String.Empty Then
            lrecrea_Letter = New eRemoteDB.VisualTimeConfig
            'sPath = lrecrea_Letter.LoadSetting("CDoc_Path", , "Paths")
            sPath = lrecrea_Letter.LoadSetting("Correspondence", , "Paths")
            lrecrea_Letter = Nothing
        End If

        If bPrint Then
            sFileName = sPath & "\" & nLetterNum & fileDateTime & ".rtf"
        Else
            sFileName = sPath & "\" & IO.Path.GetTempPath() & ".rtf"
        End If

        'sFileName = sPath & "\" & IO.Path.GetTempPath() & ".rtf"
        'sFileName = sPath & "\" & nLetterNum & fileDateTime & ".rtf"
        'Codigo que verifica la existencia de la ruta predefinida para la creacion de las cartas

        If Not IO.Directory.Exists(sPath & "\") Then
            'Si no existe, la crea
            IO.Directory.CreateDirectory(sPath & "\")
        End If
        sVarList = sVarList & Chr(C_SEPARATOR) & "FILENAME=" & sFileName


        oFile = IO.File.CreateText(sFileName)
        oFile.Write(sFileContent)
        oFile.Close()

        sDocumentName = sFileName

        If bPrint Then
            If lPrintReport(sFileName) Then
                MergeDocument = True
                lintStatLetter = 2 '+ Printed
            Else
                lintStatLetter = 1 '+ Pending Printing
            End If
        End If

        sMergeResult = sFileContent

        For Each oTable In oTables
            If sClient = String.Empty Then
                oTable.HideErrorMsg = True
                sClient = oTable.FieldToClass("sClient")
                oTable.HideErrorMsg = False
            End If
            oTable.RCloseRec()
        Next oTable

        If Not bPrint Then
            If sInvalid_ind = "1" Then '+ Invalid Address
                If Me.sDelivInvalidInd = "1" Then
                    lintStatLetter = 7 '+ Pending Printing (Invalid Address)
                ElseIf Me.sDelivInvalidInd = "2" Then
                    lintStatLetter = 6 '+ Not Printing (Invalid Address)
                End If
            Else
                lintStatLetter = 1 '+ Pending Printing
            End If
        End If

        'VER BIEN JJ
        'If oLettAccuse Is Nothing And Not bPreview Then
        '    oLettAccuse = New LettAccuse

        '    lreccreLettAccuse = New eRemoteDB.Execute

        '    If sClient <> "" Then
        '        With oLettAccuse
        '            If .Delete(nLettRequest) Then
        '                lreccreLettAccuse.SQL = "INSERT INTO LETTACCUSE (NLETTREQUEST, SCLIENT, DANSWERDATE, DTOHANDOVER,DCOMPDATE,NUSERCODE, TLETTER, NTYPELETTER, NSTATLETTER) " & _
        '                                                           "VALUES (" & nLettRequest & ",'" & sClient & "',null,null,SYSDATE," & nUsercode & ",:TLETTER," & lintTypeLetter & ",'" & lintStatLetter & "')"

        '                lreccreLettAccuse.Parameters.Add(":TLETTER", CleanLetter((Me.tletter)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '                lreccreLettAccuse.Run(False)

        '                lreccreLettAccuse = Nothing
        '            End If
        '        End With
        '        'With oLettAccuse
        '        '    .Add(nLettRequest, sClient, , , CleanLetter(sFileContent), nUsercode, lintTypeLetter, lintStatLetter, , String.Empty)
        '        'End With
        '    End If
        'End If

        '**+ Aare made the search of the correspondences to which is sent a email.
        '**+ Uses the CDONTS.NewMail object to make this activity.
        '+ Se realiza la busqueda de las correspondencias a las cuales se les envía un correo electronico.
        '+ Se utiliza el objeto CDONTS.NewMail para realizar esta actividad.

        lclsLetters = New eLetter.LettRequest
        lcolletterss = New eLetter.LettRequests

        If lcolletterss.FindLetterRequestAccuse(nLettRequest, dEffecDate) Then

            lrecrea_Letter = New eRemoteDB.VisualTimeConfig
            sHostSMTP = lrecrea_Letter.LoadSetting("HostSMTP", , "Application")
            sUserSMTP = lrecrea_Letter.LoadSetting("UserSMTP", , "Application")
            sPasswordSMTP = lrecrea_Letter.LoadSetting("PasswordSMTP", , "Application")
            sPortSMTP = lrecrea_Letter.LoadSetting("PortSMTP", , "Application")
            sDefaultMailFrom = lrecrea_Letter.LoadSetting("DefaultMailFrom", , "Application")

            For Each lclsLetters In lcolletterss
                If (lclsLetters.nSendType = 1 Or lclsLetters.nSendType = 3 Or lclsLetters.nSendType = 5 Or lclsLetters.nSendType = 7) And lintStatLetter <> 6 Then
                    If InStr(1, lclsLetters.sStreet, "@") > 0 Then
                        lobjEmail = New System.Net.Mail.MailMessage
                        eMailclient = New Net.Mail.SmtpClient(sHostSMTP, IIf(sPortSMTP = String.Empty, 25, sPortSMTP))
                        If sUserSMTP = String.Empty Then
                            eMailclient.UseDefaultCredentials = True
                        Else
                            eMailclient.UseDefaultCredentials = False
                            eMailclient.Credentials = New System.Net.NetworkCredential(sUserSMTP, sPasswordSMTP)
                        End If

                        If Not lobjEmail Is Nothing Then
                            If sEmailUsers = String.Empty Then
                                sEmailUsers = sDefaultMailFrom
                            End If
                            eMailMessage = New System.Net.Mail.MailMessage(sEmailUsers, lclsLetters.sStreet, lclsLetters.sDescriptt, "")
                            eMailMessage.IsBodyHtml = False
                            eMailMessage.Attachments.Add(New System.Net.Mail.Attachment(sDocumentName))

                            eMailclient.Send(eMailMessage)
                            lobjEmail = Nothing
                            eMailclient = Nothing
                            eMailMessage = Nothing
                        End If
                    End If
                End If
            Next lclsLetters
        End If

        lrecrea_Letter = Nothing
        lclsLetters = Nothing
        lcolletterss = Nothing
        oFile = Nothing
        oTable = Nothing
        oVariables = Nothing
        oVariable = Nothing
        oTables = Nothing

        Exit Function
        lclsLetters = Nothing
        lcolletterss = Nothing
        oFile = Nothing
        oTables = Nothing
        oVariables = Nothing
        oVariable = Nothing
        oTable = Nothing
    End Function
    ''**%Objective: Generates the correspondences required by the user.
    ''**%Parameters:
    ''**%  oParameters   - Collection of parameters
    ''**%  oLettAccuse   - Collection of generated letters.
    ''**%  dEffecDate    - Date which from the record is valid.
    ''**%  nUsercode     - Code of the user creating or updating the record.
    '    '**%  bPrint        - Boolean variable of type which indicates if it is required to print or no.
    '    '**%  nLetterNum    - Number identifying the letter template.
    '    '**%  nLanguage     - Code that identifies the language.
    '    '**%  sPath         - Location where one goes away to store the generated correspondences.
    '    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '    '**%  bPreview      - Boolean variable of type, indicates if it is due to store or not it generated correspondence.
    '    '%Objetivo: Genera las correspondencias solicitadas por el usuario.
    '    '%Parámetros:
    '    '%  oParameters     - Colección de parametros.
    '    '%  oLettAccuse     - Colección de las cartas generadas.
    '    '%  dEffecDate      - Fecha de efecto del registro.
    '    '%  nUsercode       - Código del usuario que crea o actualiza el registro.
    '    '%  bPrint          - Variable de tipo booleana el cual indica si se requiere imprimir o no.
    '    '%  nLetterNum      - Código del modelo de carta.
    '    '%  nLanguage       - Código que identifica el idioma.
    '    '%  sPath           - Ubicación donde se va almacenar las correspondencias generadas.
    '    '%  nLettRequest    - Numero de la solicitud.
    '    '%  bPreview        - Variable de tipo booleana, indica si se debe almacenar o no la correspondencia generada.
    '    Public Function MergeDocument(ByVal oParameters As Collection, _
    '                                  ByVal oLettAccuse As eLetter.LettAccuse, _
    '                                  ByVal dEffecDate As Date, _
    '                                  ByVal nUsercode As Short, _
    '                                  ByVal bPrint As Boolean, _
    '                                  ByVal lintTypeLetter As Short, _
    '                                  Optional ByVal nLetterNum As Short = 0, _
    '                                  Optional ByVal nLanguage As Short = 0, _
    '                                  Optional ByVal sPath As String = "", _
    '                                  Optional ByVal nLettRequest As Short = 0, _
    '                                  Optional ByVal bPreview As Boolean = False) As Boolean

    '        Dim lrecreaTab_Letters As eRemoteDB.Execute
    '        Dim lrecrea_Letter As eRemoteDB.VisualTimeConfig
    '        Dim nIndex As Integer
    '        Dim sName As String
    '        Dim nLength As String
    '        Dim oVariables As GroupVariabless
    '        Dim oVariable As GroupVariables
    '        Dim oTable As eRemoteDB.Execute
    '        Dim sVarList As String
    '        Dim sFailedList As String
    '        Dim oFile As IO.StreamWriter
    '        Dim sFileName As String
    '        Dim sFileContent As String
    '        Dim sClient As String
    '        Dim sValue As String
    '        Dim bErr As Boolean
    '        Dim lintLen As Integer
    '        Dim lintPosBegin As Integer
    '        Dim lintPosEnd As Integer
    '        Dim lintPosFind As Integer
    '        Dim lstrChar As String
    '        Dim lstrChain1 As String
    '        Dim lstrChain2 As String
    '        Dim lintPosT As Integer
    '        Dim lclsLetters As eLetter.LettRequest
    '        Dim lcolletterss As eLetter.LettRequests
    '        Dim lobjEmail As Object 'CDONTS.NewMail
    '        Dim sEmailUsers As String
    '        Dim sInvalid_ind As String
    '        Dim lintStatLetter As Integer
    '        Dim lngIndexBegin As Integer
    '        Dim lngIndexFinish As Integer
    '        Dim lngIndexLast As Integer
    '        Dim lblnExist As Boolean
    '        Dim lstKey As String

    '        lstKey = String.Empty

    '        If Not IsIDEMode() Then
    '        End If

    '        lrecreaTab_Letters = New eRemoteDB.Execute
    '        With lrecreaTab_Letters
    '            .StoredProcedure = "reaUsersClient"
    '            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '            If .Run Then
    '                sEmailUsers = .FieldToClass("sStreet")
    '                sInvalid_ind = .FieldToClass("sInvalid_ind")
    '                .RCloseRec()
    '            End If
    '        End With
    '        lrecreaTab_Letters = Nothing

    '        oVariables = New GroupVariabless

    '        If oParameters Is Nothing Then
    '            oParameters = Me.oParameters
    '        End If
    '        If nLetterNum <> intNull Then
    '            If Not Find(nLetterNum, nLanguage, dEffecDate) Then
    '                If Not Find(nLetterNum, Me.nLanguageUsers, dEffecDate) Then
    '                    Call Find(nLetterNum, intNull, dEffecDate)
    '                End If
    '            End If
    '        End If
    '        sFileContent = tletter

    '        lngIndexLast = 0
    '        nIndex = InStr(nIndex + 1, sFileContent, C_MERGE_TAG)
    '        Do While nIndex > 0
    '            oVariable = New GroupVariables

    '            lngIndexBegin = InStrRev(Left(sFileContent, nIndex), "{\field")
    '            If lngIndexBegin > lngIndexLast Then
    '                lngIndexFinish = InStr(nIndex, sFileContent, "}}}")
    '                If lngIndexFinish > lngIndexBegin Then
    '                    oVariable.sFldSource = Mid(sFileContent, lngIndexBegin, (lngIndexFinish - lngIndexBegin) + 3)

    '                    lngIndexBegin = InStr(oVariable.sFldSource, "{\fldrslt ")
    '                    lngIndexFinish = InStr(lngIndexBegin, oVariable.sFldSource, "}}")
    '                    If lngIndexFinish > lngIndexBegin Then
    '                        oVariable.sFldValue = Mid(oVariable.sFldSource, lngIndexBegin + 10, (lngIndexFinish - lngIndexBegin) - 9)
    '                    End If
    '                End If
    '            End If
    '            nIndex = nIndex + C_MERGE_TAG_LEN
    '            nLength = CStr(InStr(nIndex, sFileContent, "}") - nIndex)
    '            sName = TranslateName(Mid(sFileContent, nIndex, CInt(nLength)))

    '            If oVariable.FindByName(sName) Then
    '                On Error Resume Next
    '                oVariables.Add(oVariable)
    '            End If
    '            oVariable = Nothing
    '            lngIndexLast = nIndex
    '            nIndex = InStr(nIndex + 1, sFileContent, C_MERGE_TAG)

    '        Loop
    '        oTables = New Collection
    '        ' Set oTable = New eRemoteDB.Execute
    '        For Each oVariable In oVariables
    '            'ojo yo modifique
    '            If lstKey <> (oVariable.sTableName & oVariable.nLett_group) Then
    '                oTable = New eRemoteDB.Execute
    '                With oTable
    '                    .StoredProcedure = "reaDocVariables"
    '                    .Parameters.Add("sTable", oVariable.sTableName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '                    .Parameters.Add("nParameter", nParameter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '                    .Parameters.Add("nGroup", oVariable.nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '                    .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '                    For nIndex = 1 To C_NUM_PARAMETERS - 1
    '                        On Error Resume Next
    '                        .Parameters.Add("sGenericParam" & nIndex, oParameters.Item(nIndex), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '                        If Err.Number > 0 Then
    '                            Err.Clear()
    '                            .Parameters.Add("sGenericParam" & nIndex, System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '                        End If
    '                    Next
    '                    .Parameters.Add("sGenericParam" & C_NUM_PARAMETERS, nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '                    lblnExist = False
    '                    If .Run Then
    '                        oTables.Add(oTable, oVariable.sTableName & oVariable.nLett_group)
    '                        lblnExist = True
    '                    End If
    '                End With
    '                lstKey = oVariable.sTableName & oVariable.nLett_group
    '            End If
    '            bErr = False
    '            With oVariable
    '                '*****************************************************************************
    '                If lblnExist Then
    '                    oTables.Item(oVariable.sTableName & oVariable.nLett_group).HideErrorMsg = True
    '                    sValue = oTables.Item(oVariable.sTableName & oVariable.nLett_group).FieldToClass(oVariable.sAliasColumn)
    '                    oTables.Item(.sTableName & oVariable.nLett_group).HideErrorMsg = False

    '                    If sClient = String.Empty And oVariable.sTableName = "CLIENT" Then
    '                        oTable.HideErrorMsg = True
    '                        sClient = oTables.Item(.sTableName & oVariable.nLett_group).FieldToClass("sClient")
    '                        oTable.HideErrorMsg = False
    '                    End If

    '                    Err.Clear()
    '                End If
    '                '*****************************************************************************

    '                sVarList = sVarList & .sVariable & "=" & sValue & Chr(C_SEPARATOR)

    '                '**+Merge by its value found in the data base replaces the field
    '                '+Reemplaza el campo merge por su correspondiente valor encontrado en la BD.
    '                '            sFileContent = Replace(sFileContent, "\'ab" + .sVariable + "\'bb", Trim$(sValue))

    '                sFileContent = Replace(sFileContent, .sFldSource, Replace(.sFldValue, "\'ab" & .sVariable & "\'bb", Trim(sValue)))

    '                '            ReplaceValue sFileContent, .sVariable, Trim$(sValue), False

    '                If bErr Then
    '                    sFailedList = sFailedList & .sVariable & "(" & .sTableName & "." & .sAliasColumn & Chr(C_SEPARATOR)
    '                End If

    '            End With
    '            oTable = Nothing
    '        Next oVariable

    '        '**+ begin the process of debugging of the letter. they
    '        '**+ eliminate all the code that belongs to the instruction "\field"
    '        '+ Se inicia el proceso de depuración de la carta. se eliminan todo el código que
    '        '+ pertenezca a la instrucción "\field"

    '        '    If InStr(1, sFileContent, "\field") > 0 Then
    '        '        Do
    '        '            lintLen = Len(sFileContent)
    '        '            lintPosBegin = InStr(1, sFileContent, "\field")
    '        '            lstrChain1 = Left(sFileContent, lintPosBegin - 1)
    '        '            lintPosFind = lintPosBegin
    '        '            lintPosEnd = 0
    '        '            Do While lintPosFind <= lintLen
    '        '                lstrChar = Right(Left(sFileContent, lintPosFind + 1), 1)
    '        '                If lstrChar = "{" Then
    '        '                    lintPosEnd = lintPosEnd + 1
    '        '                    If lintPosEnd = 4 Then
    '        '                        lintPosEnd = lintPosFind + 1
    '        '                        Exit Do
    '        '                    End If
    '        '                End If
    '        '                lintPosFind = lintPosFind + 1
    '        '            Loop
    '        '            lstrChain2 = Right(sFileContent, lintLen - lintPosEnd)
    '        '            sFileContent = lstrChain1 & lstrChain2
    '        '            lintPosT = InStr(1, sFileContent, "\field")
    '        '        Loop Until lintPosT = 0
    '        '    End If

    '        sFailedList = "FAILED ={" & sFailedList & "}"
    '        sVarList = sVarList & sFailedList
    '        If sPath = String.Empty Then
    '            lrecrea_Letter = New eRemoteDB.VisualTimeConfig
    '            sPath = lrecrea_Letter.LoadSetting("CDoc_Path", , "Paths")
    '            lrecrea_Letter = Nothing
    '        End If
    '        'sFileName = sPath & "\" & nLetterNum & ".rtf"
    '        sFileName = "C:\Models of letter\" & nLetterNum & ".rtf"
    '        sVarList = sVarList & Chr(C_SEPARATOR) & "FILENAME=" & sFileName

    '        oFile = IO.File.CreateText(sFileName)
    '        oFile.Write(sFileContent)
    '        oFile.Close()

    '        sDocumentName = sFileName
    '        sMergeResult = sFileContent

    '        For Each oTable In oTables
    '            If sClient = String.Empty Then
    '                oTable.HideErrorMsg = True
    '                sClient = oTable.FieldToClass("sClient")
    '                oTable.HideErrorMsg = False
    '            End If
    '            oTable.RCloseRec()
    '        Next oTable


    '        If sInvalid_ind = "1" Then '+ Invalid Address
    '            If Me.sDelivInvalidInd = "1" Then
    '                lintStatLetter = 7 '+ Pending Printing (Invalid Address)
    '            ElseIf Me.sDelivInvalidInd = "2" Then
    '                lintStatLetter = 6 '+ Not Printing (Invalid Address)
    '            End If
    '        Else
    '            lintStatLetter = 1 '+ Pending Printing
    '        End If


    '        If oLettAccuse Is Nothing And Not bPreview Then
    '            oLettAccuse = New LettAccuse
    '            With oLettAccuse
    '                .Add(nLettRequest, sClient, , , CleanLetter(sFileContent), nUsercode, lintTypeLetter, lintStatLetter, , String.Empty)
    '            End With
    '        End If

    '        '**+ Aare made the search of the correspondences to which is sent a email.
    '        '**+ Uses the CDONTS.NewMail object to make this activity.
    '        '+ Se realiza la busqueda de las correspondencias a las cuales se les envía un correo electronico.
    '        '+ Se utiliza el objeto CDONTS.NewMail para realizar esta actividad.

    '        lclsLetters = New eLetter.LettRequest
    '        lcolletterss = New eLetter.LettRequests

    '        If lcolletterss.FindLetterRequestAccuse(nLettRequest, dEffecDate) Then
    '            For Each lclsLetters In lcolletterss
    '                If (lclsLetters.nSendType = 1 Or lclsLetters.nSendType = 3 Or lclsLetters.nSendType = 5 Or lclsLetters.nSendType = 7) And lintStatLetter <> 6 Then
    '                    If InStr(1, lclsLetters.sStreet, "@") > 0 Then
    '                        lobjEmail = eRemoteDB.NetHelper.CreateClassInstance("CDONTS.NewMail")
    '                        If Not lobjEmail Is Nothing Then
    '                            lobjEmail.From = sEmailUsers
    '                            lobjEmail.To = lclsLetters.sStreet
    '                            lobjEmail.Importance = 1
    '                            lobjEmail.Subject = lclsLetters.sDescriptt
    '                            lobjEmail.Body = ""
    '                            lobjEmail.AttachFile(sDocumentName, lclsLetters.sDescriptt)
    '                            lobjEmail.sEnd()
    '                            lobjEmail = Nothing
    '                        End If
    '                    End If
    '                End If
    '            Next lclsLetters
    '        End If

    '        lclsLetters = Nothing
    '        lcolletterss = Nothing
    '        oFile = Nothing
    '        oTable = Nothing
    '        oVariables = Nothing
    '        oVariable = Nothing
    '        oTables = Nothing

    '        Exit Function
    'ErrorHandler:
    '        lclsLetters = Nothing
    '        lcolletterss = Nothing
    '        oFile = Nothing
    '        oTables = Nothing
    '        oVariables = Nothing
    '        oVariable = Nothing
    '        oTable = Nothing
    '        ProcError("Letter.MergeDocument(oParameters,oLettAccuse,dEffecDate,nUsercode,bPrint,nLetterNum,sPath,nLettRequest,bPreview)", New Object() {oParameters, oLettAccuse, dEffecDate, nUsercode, bPrint, nLetterNum, sPath, nLettRequest, bPreview})
    '    End Function
	
	'**%Objective: Merge by its value found in the data base replaces the field
	'**%Parameters:
	'**%  sData         - Content of the generated letter.
	'**%  sVariable     - Variable merge to being replaced
	'**%  sValue        - Variable that contains the value of the field merge.
	'**%  bAll          - Boolean variable of type. This indicates if culminate the task available.
	'%Objetivo: Reemplaza el campo merge por su correspondiente valor encontrado en la BD.
	'%Parámetros:
	'%    sData         - Contenido de la carta generada.
	'%    sVariable     - Variable merge a ser reemplazada
	'%    sValue        - Variable que contiene el valor del campo merge.
	'%    bAll          - Variable de tipo booleana. Esta indica si se culmino la tarea de reemplazo.
	Private Sub ReplaceValue(ByRef sData As String, ByVal sVariable As String, ByVal sValue As String, ByVal bAll As Boolean)
		Dim nPos As Integer
		Dim nStartField As Integer
		Dim nIndex As Integer
		Dim nCount As Integer
		Dim nLength As Integer
		
		If Not IsIDEMode Then
		End If
		
		nPos = InStr(1, sData, C_MERGE_TAG)
		nLength = InStr(nPos, sData, "}") - nPos
		
		nPos = nPos + InStr(1, Mid(sData, nPos, nLength), Trim(sVariable)) - 1
		Do While nPos > 0
			nStartField = InStrRev(sData, C_FIELDSTART_TAG, nPos)
			If nStartField > 0 Then
				nIndex = nStartField
				nCount = 0
				Do While True
					Select Case Mid(sData, nIndex, 1)
						Case C_OPENFIELD_CHAR
							nCount = nCount + 1
						Case C_CLOSEFIELD_CHAR
							nCount = nCount - 1
					End Select
					If nCount = -1 Then
						Exit Do
					End If
					nIndex = nIndex + 1
				Loop 
				sData = Mid(sData, 1, nStartField - 1) & sValue & Mid(sData, nIndex)
			End If
			nPos = InStr(1, sData, C_MERGE_TAG & " " & Trim(sVariable))
			If Not bAll Then
				Exit Do
			End If
		Loop 
		
		Exit Sub
	End Sub
	
    '**Objective:
    '%Objetivo:
    Public Function Exists(ByVal oTables As Collection, ByVal sKey As String) As Boolean

        Debug.Print(oTables.Item(sKey).StoredProcedure)
        Exists = True

        Exit Function
        ' ProcError "Letter.Exists(oTables,sKey)", Array(oTables, sKey)
    End Function
    ''**Objective:
    ''%Objetivo:
    'Public Function Exists(ByVal oTables As Collection, ByVal sKey As String) As Boolean
    'Dim lrecreaTab_Letters As eRemoteDB.Execute
    '
    '		If Not IsIDEMode Then
    '		End If
    '
    '		If oTables.Count() <> 0 Then
    '			Debug.Print(sKey)
    '			If oTables.Item(sKey).FieldToClass("sClient") Then
    '				Exists = False
    '			Else
    '				Exists = True
    '			End If
    '		Else
    '			Exists = True
    '		End If
    '
    '   '    Debug.Print oTables(sKey).StoredProcedure
    '
    '
    '		Exit Function
    'ErrorHandler: 
    '		ProcError("Letter.Exists(oTables,sKey)", New Object(){oTables, sKey})
    '	End Function


    '**%Objective: Prepares the options of request of sent of correspondence according to the conditions of entrance.
    '**%Parameters:
    '**%  nLettRequest    - Number of the request for remittance of  correspondence
    '**%  nParameter      - Parameter Code. The possible values as per table 622.
    '**%  dEffecDate      - Date which from the record is valid.
    '**%  nTypeRequest    - Type of massive requirement or individual requirement.
    '**%  nLetterNum      - Number identifying the letter template.
    '**%  sCodispl        - Código del usuario que crea o actualiza el registro.
    '**%  nAction         - Code of the action that this executing.
    '**%  nUsercode       - Code of the user creating or updating the record.
    '%Objetivo: Prepara las opciones de solicitud de envio de correspondencia según las condiciones de entrada.
    '%Parámetros:
    '%    nLettRequest    - Número de solicitud de envío.
    '%    nParameter      - Codigo del parametros. Posibles valores según la table622
    '%    dEffecDate      - Fecha de efecto del registro.
    '%    nTypeRequest    - Tipo de requerimiento masivo o individual.
    '%    nLetterNum      - Código del modelo de carta.
    '%    sCodispl        - Código del usuario que crea o actualiza el registro.
    '%    nAction         - Codigo de la acción que se esta ejecutando.
    '%    nUsercode       - Código del usuario que crea o actualiza el registro.
    Public Function PrepareMergeMasive(ByVal nLettRequest As Double, ByVal nParameter As Short, ByVal dEffecDate As Date, ByVal nTypeRequest As Short, ByVal nLetterNum As Double, ByVal sCodispl As String, ByVal nAction As Short, ByVal nUsercode As Double, Optional ByVal sPrint As String = "2") As Boolean
        Dim lclsLettValuess As eLetter.LettValuess
        Dim lclsLettValues As New eLetter.LettValues
        Dim lclsLettRequests As eLetter.LettRequests
        Dim lstrWhere As String
        Dim oPrepareRec As eTypePrepare
        Dim oCol As Collection
        Dim lintIndex As Short
        Dim sTables As String = String.Empty
        Dim sTable1 As String = String.Empty
        Dim bPrint As Boolean

        bPrint = False
        If sPrint = "1" Then
            bPrint = True
        End If

        If Not IsIDEMode() Then
        End If

        lclsLettValuess = New eLetter.LettValuess
        lclsLettRequests = New eLetter.LettRequests

        With oPrepareRec
            .eExist = False
            .eRecordset = Nothing
        End With

        '**+ Manage to Request Masive
        '+ Solicitud masiva

        If nTypeRequest = CN_MASIVE Then

            '**+ Invoke the function tha prepare the data to evaluate and so, load the Merge process for each record
            '+ Invoco la función, evalua los datos, cargue el proceso merge para cada correspondencia

            With lclsLettValues
                lstrWhere = insPrepareWhere(nLettRequest, sTables, sTable1)
                Select Case nParameter
                    Case CN_BENEF
                        oPrepareRec = insExecuteLoad("insPrepParameterBene", lstrWhere, dEffecDate, Mid(Mid(sTables, 1, 1), Len(Mid(sTables, 1, 1)), 1), sTable1)
                    Case CN_INTERMEDIA
                        oPrepareRec = insExecuteLoad("insPrepParameterInite", lstrWhere, dEffecDate, sTables)
                    Case CN_CLIENT
                        oPrepareRec = insExecuteLoad("insPrepParameterClie", lstrWhere, dEffecDate, sTables)
                    Case CN_POLICY
                        oPrepareRec = insExecuteLoad("insPrepParameterPoli", lstrWhere, dEffecDate, sTables)
                    Case CN_RECEIPT
                        oPrepareRec = insExecuteLoad("insPrepParameterRect", lstrWhere, dEffecDate, sTables)
                    Case CN_CLAIM
                        oPrepareRec = insExecuteLoad("insPreParameterClaim", lstrWhere, dEffecDate, sTables)
                End Select
            End With

            With oPrepareRec.eRecordset
                If oPrepareRec.eExist Then
                    While Not .EOF
                        oCol = New Collection
                        For lintIndex = 0 To .FieldsCount - 1
                            Debug.Print(.FieldToClass(.Item(lintIndex), String.Empty))
                            oCol.Add(.FieldToClass(.Item(lintIndex), String.Empty))
                        Next
                        Select Case nParameter
                            Case CN_BENEF
                                Call FindLanguageClient(nUsercode, nParameter, String.Empty, intNull, intNull, intNull, intNull, oCol.Item(1), intNull, intNull)
                            Case CN_INTERMEDIA
                                Call FindLanguageClient(nUsercode, nParameter, String.Empty, intNull, intNull, intNull, intNull, CStr(intNull), intNull, oCol.Item(1))
                            Case CN_CLIENT
                                Call FindLanguageClient(nUsercode, nParameter, String.Empty, intNull, intNull, intNull, intNull, oCol.Item(1), intNull, intNull)
                            Case CN_POLICY
                                Call FindLanguageClient(nUsercode, nParameter, oCol.Item(1), oCol.Item(2), oCol.Item(3), oCol.Item(4), oCol.Item(5), String.Empty, intNull, intNull)
                            Case CN_RECEIPT
                                Call FindLanguageClient(nUsercode, nParameter, oCol.Item(1), oCol.Item(2), oCol.Item(3), oCol.Item(4), oCol.Item(5))
                            Case CN_CLAIM
                                Call FindLanguageClient(nUsercode, nParameter, String.Empty, intNull, intNull, intNull, intNull, CStr(intNull), oCol.Item(1), intNull)
                        End Select
                        MergeDocument(oCol, Nothing, dEffecDate, nUsercode, bPrint, 1, nLetterNum, Me.nLanguage, , nLettRequest, False)
                        oCol = Nothing
                        .RNext()
                    End While
                    .RCloseRec()
                    PrepareMergeMasive = True
                Else
                    PrepareMergeMasive = False
                End If
            End With
        Else
            If lclsLettValuess.Find(nLettRequest, 1) Then

                oCol = New Collection
                For Each lclsLettValues In lclsLettValuess
                    oCol.Add(lclsLettValues.sValue)
                Next lclsLettValues
                Select Case nParameter
                    Case CN_BENEF
                        Call FindLanguageClient(nUsercode, nParameter, String.Empty, intNull, intNull, intNull, intNull, oCol.Item(1), intNull, intNull)
                    Case CN_INTERMEDIA
                        Call FindLanguageClient(nUsercode, nParameter, String.Empty, intNull, intNull, intNull, intNull, CStr(intNull), intNull, oCol.Item(1))
                    Case CN_CLIENT
                        Call FindLanguageClient(nUsercode, nParameter, String.Empty, intNull, intNull, intNull, intNull, oCol.Item(1), intNull, intNull)
                    Case CN_POLICY
                        Call FindLanguageClient(nUsercode, nParameter, oCol.Item(1), oCol.Item(2), oCol.Item(3), oCol.Item(4), oCol.Item(5), String.Empty, intNull, intNull)
                    Case CN_RECEIPT
                        Call FindLanguageClient(nUsercode, nParameter, oCol.Item(1), oCol.Item(2), oCol.Item(3), oCol.Item(4), oCol.Item(5))
                    Case CN_CLAIM
                        Call FindLanguageClient(nUsercode, nParameter, String.Empty, intNull, intNull, intNull, intNull, CStr(intNull), oCol.Item(1), intNull)
                End Select
                MergeDocument(oCol, Nothing, dEffecDate, nUsercode, bPrint, 1, nLetterNum, Me.nLanguage, , nLettRequest, False)
                oCol = Nothing
            End If
        End If
        lclsLettValues = Nothing
        lclsLettRequests = Nothing
        oCol = Nothing

        Exit Function
        lclsLettValues = Nothing
        lclsLettRequests = Nothing
        oCol = Nothing
    End Function

    '**%Objective: Execute a procedure corrispondig to the parameter that return a values to evaluate in Merge process
    '**%Parameters:
    '**%  sStoredProc   - Name of the Store procedure to execute
    '**%  sWhere        - Variable that contains the conditional one of the search
    '**%  dEffecDate    - Present Date
    '**%  sTables       - Name of the table with which one is going away to work.
    '%Objetivo: Ejecuta un proceso correspondiente a los parámetros,  retornando los valores ya evaluados en el proceso merge
    '%Parámetros:
    '%    sStoredProc   - Nombre del Store procedure a ejecutarse.
    '%    sWhere        - Variable que contiene el condicional de la busqueda.
    '%    dEffecDate    - Fecha actual
    '%    sTables       - Nombre de la tabla con la cual se va a trabajar.
    Private Function insExecuteLoad(ByVal sStoredProc As String, ByVal sWhere As String, ByVal dEffecDate As Date, Optional ByVal sTables As String = "", Optional ByVal sTable1 As String = "") As eTypePrepare
        Dim lrecStoredProc As eRemoteDB.Execute

        lrecStoredProc = New eRemoteDB.Execute
        sTables = IIf(sTables = "/", "", sTables)

        If Not IsIDEMode() Then
        End If

        With lrecStoredProc
            .StoredProcedure = sStoredProc
            .Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sWhere", sWhere, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTables", sTables, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTable1", sTable1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                insExecuteLoad.eExist = True
                insExecuteLoad.eRecordset = lrecStoredProc
            Else
                insExecuteLoad.eExist = False
                insExecuteLoad.eRecordset = Nothing
            End If
        End With
        lrecStoredProc = Nothing

        Exit Function
        lrecStoredProc = Nothing
    End Function

    '**%Objective:  Return the Where value to add the string to send the procedure
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  sTables       - Name of the table with which one is going away to work.
    '%Objetivo:  Retorna el valor con la cadena agregada al procedimiento que lo llamo.
    '%Parámetros:
    '%    nLettRequest  - Número de solicitud de envío.
    '%    sTables       - Nombre de la tabla con la cual se va a trabajar.
    Private Function insPrepareWhere(ByVal nLettRequest As Double, ByRef sTables As String, ByRef sTable1 As String) As String
        Dim lrecinsPrepareWhere As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lrecinsPrepareWhere = New eRemoteDB.Execute

        insPrepareWhere = String.Empty

        With lrecinsPrepareWhere
            .StoredProcedure = "insPrepareWhere"
            .Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                insPrepareWhere = .FieldToClass("sWhere", String.Empty)
                sTables = .FieldToClass("sTables")
                sTable1 = .FieldToClass("sTable1")
                .RCloseRec()
            End If
        End With
        lrecinsPrepareWhere = Nothing

        Exit Function
        lrecinsPrepareWhere = Nothing
    End Function

    '**%Objective: Initializes the class
    '%Objetivo: Inicializa la clase
    Private Sub Class_Initialize_Renamed()
        If Not IsIDEMode() Then
        End If

        oParameters = New Collection

        Exit Sub
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Objective: Class finalizes
    '%Objetivo: Finaliza la clase
    Private Sub Class_Terminate_Renamed()
        If Not IsIDEMode() Then
        End If

        oParameters = Nothing

        Exit Sub
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    '**%Objective: Places the simple comiles in the considered values String
    '**%Parameters:
    '**%  sWhere    - Variable that contains the conditional block.
    '%Objetivo: Coloca las comillas simples en los valores considerados string
    '%Parámetros:
    '%    sWhere    - Variable que contiene el bloque condicional.
    Private Function IncludeChar(ByVal sWhere As String) As String
        Dim intStart As Short

        If Not IsIDEMode() Then
        End If

        IncludeChar = sWhere
        intStart = InStr(1, sWhere, "'")
        Do While intStart > 0
            IncludeChar = Mid(IncludeChar, 1, intStart - 1) & "CHR(39)" & Mid(IncludeChar, intStart + 1)
            intStart = InStr(intStart + 1, IncludeChar, "'")
        Loop

        Exit Function
    End Function

    '**%Objective: obtain the usuary applicant in opposite case to place the usuary executor.
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  nUsercode     - Code of user.
    '%Objetivo: Obtener el usuario solicitante en caso contrario colocar el usuario ejecutor.
    '%Parámetros:
    '%    nLettRequest  - Número de solicitud de envío.
    '%    nUsercode     - Código del usuario
    Private Function insGetUser(ByVal nLettRequest As Double, ByVal nUsercode As Double) As String
        Dim lobjRequest As LettRequest

        If Not IsIDEMode() Then
        End If

        lobjRequest = New LettRequest
        With lobjRequest
            If .Find(nLettRequest) Then
                insGetUser = CStr(.nUser_Sol)
            Else
                insGetUser = CStr(nUsercode)
            End If
        End With
        lobjRequest = Nothing

        Exit Function
        lobjRequest = Nothing
    End Function

    '**%Objective: Read the registries of the tables certificat and client to find the languages
    '**%Parameters:
    '**%   nPolicy : Code of the policy
    '**%   nCertif : Code of certificat
    '**%   sCertype: Type or record.
    '**%             Sole values: 1-proposal 2-policy 3-quotation
    '**%   nProduct: Code of the product
    '**%   nBranch : Code of the branch
    '%Objetivo: Lee los registros de las tablas certificat y client para encontrar los idiomas
    '%Parámetros:
    '%   nPolicy : Código de la póliza
    '%   nCertif : Código del certificado
    '%   sCertype: Tipo de registro.
    '%             Únicos valores: 1-propuesta 2-póliza 3-cotizacion
    '%   nProduct: Código del producto
    '%   nBranch : Código del ramos
    Public Function FindLanguageClient(ByVal nUsercode As Double, Optional ByVal nParameters As Short = 0, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Double = 0, Optional ByVal nProduct As Double = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal sClient As String = "", Optional ByVal nClaim As Integer = 0, Optional ByVal nIntermed As Double = 0) As Short
        Dim lrecreaTab_Letters As eRemoteDB.Execute
        lrecreaTab_Letters = New eRemoteDB.Execute
        With lrecreaTab_Letters
            .StoredProcedure = "reaUsersClient"
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.nLanguageUsers = .FieldToClass("nLanguage")
                Me.sEmailUsers = .FieldToClass("sStreet")
                .RCloseRec()
            End If
        End With
        With lrecreaTab_Letters
            .StoredProcedure = "reaClientCertificat"
            .Parameters.Add("nParameters", nParameters, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                FindLanguageClient = .FieldToClass("nLanguage")
                Me.nLanguage = .FieldToClass("nLanguage")
                .RCloseRec()
            Else
                FindLanguageClient = Me.nLanguageUsers
                Me.nLanguage = Me.nLanguageUsers
                .RCloseRec()
            End If
        End With

        Exit Function
    End Function

    '**%Objective: Read the registries of the tables certificat and client to find the languages
    '**%Parameters:
    '**%   nPolicy : Code of the policy
    '**%   nCertif : Code of certificat
    '**%   sCertype: Type or record.
    '**%             Sole values: 1-proposal 2-policy 3-quotation
    '**%   nProduct: Code of the product
    '**%   nBranch : Code of the branch
    '%Objetivo: Lee los registros de las tablas certificat y client para encontrar los idiomas
    '%Parámetros:
    '%   nPolicy : Código de la póliza
    '%   nCertif : Código del certificado
    '%   sCertype: Tipo de registro.
    '%             Únicos valores: 1-propuesta 2-póliza 3-cotizacion
    '%   nProduct: Código del producto
    '%   nBranch : Código del ramos
    Public Function lPrintReport(ByVal Paths As String) As Boolean
        On Error GoTo ErrorHandle

        Dim oWrd As New Word.Application
        Dim oDoc As New Word.Document
        oDoc = oWrd.Documents.Open(Paths)

        oWrd.Visible = False
        'oWrd.Activate()
        oWrd.PrintOut()
        'oWrd.ActiveDocument.Close(0)
        'oWrd.Activate()
        oWrd.Quit()
        oWrd = Nothing
        oDoc = Nothing

        lPrintReport = True
ErrorHandle:
        Dim oFile As IO.StreamWriter
        oFile = IO.File.CreateText("C:\Model of correspondence\LTErrors.txt")
        oFile.Write(Err.Description)
        oFile.Close()
        lPrintReport = False
    End Function

	'**%Objective: Returns the portion of the RTF code that contain the Merge variable
	'**%Parameters:
	'**%   sFldValue : Property of internal use for the process of 'MergeDocument'
	'%Objetivo: Retorna la porción del código RTF que incluye la variable que sera combinada via Merge
	'%Parámetros:
	'%   sFldValue : Propiedad de uso interno para el proceso de 'MergeDocument'
'-------------------------------------------------------------------------------------------------------
    Private Function ReturnFldValue(ByVal sFldValue As String) As String
'-------------------------------------------------------------------------------------------------------
    Dim nLeftFldValue  As Integer = sFldValue.LastIndexOf("\'ab")
    Dim nRightFldValue As Integer = sFldValue.LastIndexOf("\'bb")
    ReturnFldValue = sFldValue.Substring(nLeftFldValue,(nRightFldValue-nLeftFldValue)+4)
    End Function

End Class



