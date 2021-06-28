Option Strict Off
Option Explicit On

Imports System.Text

Public Class LettRequest
    '**+Objetivo: Clase generada a partir de la tabla 'LETTREQUEST' Letter requests.A record per every letter request
    '**+Version: $$Revision: 9 $
    '+Objetive: Class generated from the table 'LETTREQUEST' que es Solicitud de envío de correspondencia.Un registro por cada solicitud de envío
    '+Version: $$Revision: 9 $

    '**-Objective: Used constant variable in the validation of pagina LT031
    '-Objetivo: Variable constante utilizada en la validación de la pagina LT031
    Const CN_INDIVIDUAL As Short = 1

    '**-Objective: Used constant variable in the validation of pagina LT031
    '-Objetivo: Variable constante utilizada en la validación de la pagina LT031
    Const CN_NOT_ALL As Short = 2

    '**-Objective: Used constant variable in the acceptance of pagina LT031
    '-Objetivo: Variable constante utilizada en la aceptación de la pagina LT031
    Const CN_EQUAL As Short = 1

    '**-Objective: Variable of condition. Slopes of printing or all
    '-Objetivo: Variable de condición. Pendientes de impresión o todas
    Public nCondition As Short

    '**-Objective: Number of request.
    '-Objetivo: Número de solicitud
    Public nLetterRequest As Integer

    '**-Objective: Number of the request for remittance of  correspondence
    '-Objetivo: Número de solicitud de envío
    Public nLettRequest As Integer

    '**-Objective: Code identifying the letter templates
    '-Objetivo: Código del modelo de carta.
    Public nLetterNum As Short

    '**-Objective: Code that identifies the language
    '-Objetivo: Código que identifica el idioma
    Public nLanguage As Short

    '**-Objective: Date when the request is recorded
    '-Objetivo: Fecha en que se registra la solicitud
    Public DinpDate As Date

    '**-Objective: Date when the letter must be removed from the system
    '-Objetivo: Fecha en que se debe eliminar la correspondencia del sistema
    Public dExpDate As Date

    '**-Objective: Date when the letter is printed.
    '-Objetivo: Fecha de impresion de la solicitud
    Public dPrintDate As Date

    '**-Objective: Variable that contains the code of the letter.
    '-Objetivo: Variable que contiene el código de la carta.
    Public tletter As String

    '**-Objective: General status of the record.Sole values as per table26.
    '-Objetivo: Estado general del registro.Valores únicos según tabla26.
    Public sStatregt As String

    '**-Objective: Description of the type of letter
    '-Objetivo: Descripción del tipo de carta
    Public sDescriptt As String

    '**-Objective: Description of state of the printing of the letter.
    '-Objetivo: Descripción de estado de la impresión de la carta.
    Public sDescripts As String

    '**-Objective: State of the printing of the document
    '-Objetivo: Estado de la impresión del documento
    Public nStatLetter As String

    '**-Objective: Code of the usuary applicant
    '-Objetivo: Codigo del usuario solicitante
    Public nUser_Sol As Short

    '**-Objective: Code of the user creating or updating the record.
    '-Objetivo: Código del usuario que crea o actualiza el registro.
    Public nUsercode As Short

    '**-Objective: Address / Street - correspondence
    '-Objetivo: Dirección - Calle -  Envío de la correspondencia.
    Public sStreet As String

    '**-Objective: Sent type Sole Values: 1 - email 2 - Post service 3 - Facsimile
    '-Objetivo: Tipo de envío.Valores únicos 1 - email 2 - Correo 3 - Fax
    Public nSendType As Short

    '**-Objective: Request type Sole Values: 1 - Individual 2 - Massive
    '-Objetivo: Tipo de envío.Valores únicos 1 - Individual 2 - Masivo
    Public nTypRequest As Short

    '**-Objective: Number that identifies the mailing preference of the client. Sole values as per table 4008.
    '-Objetivo: Número que identifica la preferencia de correspondecia del cliente. Únicos valores por la table4008.
    Public nMailIngPref As Short

    '**-Objective: Type of address. Sole values as per table8010.
    '-Objetivo: Tipo de dirección. Únicos valores según la table8010.
    Public nTypeOfAddress As Short

    '**-Objective: Date which from the record is valid.
    '-Objetivo: Fecha de efecto del registro.
    Public dEffecDate As Date

    '**-Objective: local variable to hold collection
    '-Objetivo: local variable to hold collection
    Public sClient As String

    '**-Objective: local variable to hold collection
    '-Objetivo: local variable to hold collection
    Public sClieName As String

    '**-Objective: local variable to hold collection
    '-Objetivo: local variable to hold collection
    Public sClientSol As String

    '**-Objective: local variable to hold collection
    '-Objetivo: local variable to hold collection
    Public sClieNameSol As String

    '**-Objective: Type of Record. Sole Values: 1 - Proposal 2 - Policy 3 - Quotation
    '-Objetivo: Tipo de registro. Valores únicos: 1 - Solicitud 2 - Póliza 3 - Cotización
    Public sCertype As String

    '**-Objective: Code of the Line of Business.The possible values as per table 10.
    '-Objetivo: Código del ramo comercial.Valores posibles según tabla 10.
    Public nBranch As Short

    '**-Objective: Code of The Product.
    '-Objetivo: Código del producto.
    Public nProduct As Short

    '**-Objective: Number identifying the policy
    '-Objetivo: Número identificativo de la póliza
    Public nPolicy As Integer

    '**-Objective: Number of The Certificate.
    '-Objetivo: Número identificativo del certificado.
    Public nCertif As Integer

    '**-Objective: Number identifying the claim
    '-Objetivo: Número que identifica al siniestro
    Public nClaim As Integer

    '**-Objective: Code identifying the claim case or claimant
    '-Objetivo: Código identificativo del caso o reclamante
    Public nCase_num As Short

    '**-Objective: Number of the collection schedule or form
    '-Objetivo: Número de la relación de cobro
    Public nBordereaux As Integer

    '**-Objective: Claim type Possible values as per table 692
    '-Objetivo: Tipos de reclamos, posibles valores según table692
    Public nDeman_type As Short

    '**-Objective: local variable to hold collection
    '-Objetivo: local variable to hold collection
    Public tdEffectDat1 As Date

    '**-Objective: local variable to hold collection
    '-Objetivo: local variable to hold collection
    Public tdEffectDat2 As Date

    '**-Objective: local variable to hold collection
    '-Objetivo: local variable to hold collection
    Public sCodispl As String

    '**-Objective: local variable(s) to hold property value(s)
    '-Objetivo: local variable(s) to hold property value(s)
    Public mobjletter As Letter

    '**-Objective: local variable(s) to hold property value(s)
    '-Objetivo: local variable(s) to hold property value(s)
    Public mvarLettValues As LettValuess

    '**-Objective: local variable(s) to hold property value(s)
    '-Objetivo: local variable(s) to hold property value(s)
    Public mintLettRequest As Short

    '**-Objective: local variable(s) to hold property value(s)
    '-Objetivo: local variable(s) to hold property value(s)
    Public mblnFind As Boolean

    '**-Objective:
    '-Objetivo: Indica el tipo de carta: 1.- template 2.- Personalizada
    Public nTypeLetter As Short

    Public insNumerator As Integer

    '**-Objective: Description of the lenguage
    '-Objetivo: Descripción del lenguaje
    Public sDescriptLanguage As String

    '**-Objective: It indicates if the letter model is required for the transaction.
    '-Objetivo: Indica si el modelo de carta es requerido para la transacción.
    Public sRequired As String

    '**-Objective: It indicates if the letter model is required for the transaction.
    '-Objetivo: Indica si el modelo de carta es requerido para la transacción.
    Public nEndorseType As Short

    '**-Objective: Stores the final result of merge.
    '-Objetivo: Almacena el resultado final de merge.
    Public sMergeResult As String

    '**%Objective: Used when assigning an Object to the property, on the left side of a Set statement.Syntax: Set x.LettValues = Form1
    ''%Objetivo: Utilizado al asignar la propiedades de un objeto. Sintaxis: Set x.LettValues = Form1

    '**%Objective: Used when retrieving value of a property, on the right side of an assignment.Syntax: Debug.Print X.LettValues
    ''%Objetivo: Utilizado al extraer el valor de una propiedad. Sintaxis: Debug.Print X.LettValues
    Public Property LettValues() As LettValuess
        Get

            If Not IsIDEMode() Then
            End If

            LettValues = mvarLettValues

            Exit Property
        End Get
        Set(ByVal Value As LettValuess)

            If Not IsIDEMode() Then
            End If

            mvarLettValues = Value

            Exit Property
        End Set
    End Property

    '**%Objective: Make the search of a letter model according to the selected criterion of search.
    '%Objetivo: Realizar la busqueda de un modelo de carta según el criterio de busqueda seleccionado.

    '**%Objective: Store the new content in the object.
    '%Objetivo: Almacenar el nuevo contenido en el objeto.
    Public Property oLetter() As Letter
        Get

            If Not IsIDEMode() Then
            End If

            If mobjletter Is Nothing Then
                mobjletter = New Letter
                Call mobjletter.Find(nLetterNum, Me.nLanguage, DinpDate)
            End If
            oLetter = mobjletter

            Exit Property
        End Get
        Set(ByVal Value As Letter)

            If Not IsIDEMode() Then
            End If

            mobjletter = Value

            Exit Property
        End Set
    End Property

    '**%Objective: This function has as purpose fullfil called the store procedure "reaLetterRequest" and find the records guarded in the BD specifically in the tables LettRequets and LettAccuse.
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  nLetterNum    - Number identifying the letter templates
    '**%  DinpDate      - Date when the request is recorded
    '**%  dExpDate      - Date when the letter must be removed from the system
    '**%  dPrintDate    - Date when the letter is printed.
    '**%  sStatregt     - General status of the record.Sole values as per table 26.
    '**%  nUser_sol     - Code of The User Requester.
    '**%  nUsercode     - Code of the user creating or updating the record.
    '**%  sStreet       - Address / Street - correspondence
    '**%  nSendType     - Sent type Sole Values: 1 - email 2 - Post service 3 - Facsimile
    '**%  nTypRequest   - Request type Sole Values: 1 - Individual 2 - Massive
    '**%  dEffecDate    - Date which from the record is valid.
    '**%  sClient       - Code of client.
    '**%  sCertype      - Type or Record.Sole Values:1- Proposal 2 - Policy 3 - Quotation
    '**%  nBranch       - Code of the Line of Business.The possible values as per table 10.
    '**%  nProduct      - Code of The Product.
    '**%  nPolicy       - Number identifying the policy.
    '**%  nCertif       - Number of The Certificate.
    '**%  nClaim        - Number identifying the claim
    '**%  nCase_num     - Code identifying the claim case or claimant
    '**%  nBordereaux   - Number of the collection schedule or form
    '**%  tletter       - Variable that contains the code of the letter.
    '%Objetivo: Esta función tiene como fin realizar el llamado a el store procedure "reaLetterRequest" y encontrar los registros guardados en la BD especificamente en las tablas LettRequets y LettAccuse.
    '%Parámetros:
    '%  nLettRequest  - Numero del requerimiento
    '%  nLetterNum    - Numero que identifica el modelo de carta
    '%  DinpDate      - Fecha en que el requerimiento es almacenado
    '%  dExpDate      - Fecha cuando el requerimiento es eliminado del sistema
    '%  dPrintDate    - Fecha de impresión de la carta.
    '%  sStatregt     - Estado general del registro. Segun los valores de la tabla table26
    '%  nUser_sol     - Codigo del usuario solicitante
    '%  nUsercode     - Codigo del usuario que genero el requerimiento.
    '%  sStreet       - Dirección o Localización de la correspondencia
    '%  nSendType     - Tipo de envio. Los valores posibles son: 1 - email 2 - Servicio Postal 3 - Fax
    '%  nTypRequest   - Tipo de requerimiento los valores posibles son: 1 - Individual 2 - Masivo
    '%  dEffecDate    - Fecha cuando el registro es valido.
    '%  sClient       - Codigo del cliente.
    '%  sCertype      - Tipo o registro: Valores posibles: 1- Propuesta 2 - Poliza 3 - Cotización
    '%  nBranch       - Codigo del ramo. Valores posibles segun la tabla 10.
    '%  nProduct      - Codigo del producto
    '%  nPolicy       - Numero que identifica la poliza
    '%  nCertif       - Numero del certificado
    '%  nClaim        - Numero que identifica el siniestro
    '%  nCase_num     - Código identificativo del caso o reclamante
    '%  nBordereaux   - Numero de la colección del schedule o forma.
    '%  tletter       - Variable que contiene el código de la carta.
    Private Function Add(Optional ByVal nLettRequest As Short = 0, Optional ByVal nLetterNum As Short = 0, Optional ByVal DinpDate As Date = #12:00:00 AM#, Optional ByVal dExpDate As Date = #12:00:00 AM#, Optional ByVal dPrintDate As Date = #12:00:00 AM#, Optional ByVal sStatregt As String = "", Optional ByVal nUser_Sol As Short = 0, Optional ByVal nUsercode As Short = 0, Optional ByVal sStreet As String = "", Optional ByVal nSendType As Short = 0, Optional ByVal nTypRequest As Short = 0, Optional ByVal dEffecDate As Date = #12:00:00 AM#, Optional ByVal sClient As String = "", Optional ByVal sCertype As String = "", Optional ByVal nBranch As Short = 0, Optional ByVal nProduct As Short = 0, Optional ByVal nPolicy As Integer = 0, Optional ByVal nCertif As Integer = 0, Optional ByVal nClaim As Integer = 0, Optional ByVal nCase_num As Short = 0, Optional ByVal nBordereaux As Integer = 0, Optional ByVal tletter As String = "", Optional ByVal nCustomDescript As Short = 0, Optional ByVal nEndorseType As Short = 0) As Boolean
        Dim lreccreLettRequest As eRemoteDB.Execute
        Dim lreccreLettAccuse As eRemoteDB.Execute
        Dim lrecreaBeaber As eRemoteDB.Execute

        Dim lclsLetter As eLetter.Letter

        Dim sClientBeaber As String

        If Not IsIDEMode() Then
        End If

        lreccreLettRequest = New eRemoteDB.Execute
        lclsLetter = New eLetter.Letter

        With Me
            .nLettRequest = nLettRequest
            .nLetterNum = nLetterNum
            .DinpDate = DinpDate
            .dExpDate = dExpDate
            .dPrintDate = dPrintDate
            .sStatregt = sStatregt
            .nUser_Sol = nUser_Sol
            .nUsercode = nUsercode
            .sStreet = sStreet
            .nSendType = nSendType
            .nTypRequest = nTypRequest
            .dEffecDate = dEffecDate
            .sClient = sClient
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nClaim = nClaim
            .nCase_num = nCase_num
            .nBordereaux = nBordereaux
            .nTypeLetter = nCustomDescript
            .nEndorseType = nEndorseType

            If tletter <> String.Empty Then
                .tletter = tletter
            Else
                If lclsLetter.Find(nLetterNum, Me.nLanguage, dEffecDate) Then
                    .tletter = lclsLetter.tletter
                End If
            End If
        End With

        With lreccreLettRequest
            .StoredProcedure = "creLettRequest"
            .Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLetterNum", Me.nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInpDate", Me.DinpDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpDate", Me.dExpDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPrintDate", Me.dPrintDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUser_sol", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStreet", Me.sStreet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 230, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSendType", Me.nSendType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypRequest", Me.nTypRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Me.dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", Me.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", Me.sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", Me.nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", Me.nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", Me.nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", Me.nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", Me.nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("tCustomText", tletter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SCUSTOMDESCRIPT", Me.nTypeLetter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Me.nLettRequest = .Parameters("nLettRequest").Value
                Add = True
            End If
        End With

        'If tletter <> String.Empty Then
        lrecreaBeaber = New eRemoteDB.Execute
        With lrecreaBeaber
            .StoredProcedure = "INSREABEARER"
            .Parameters.Add("sCertype", Me.sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", Me.nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", Me.nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", Me.nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", Me.nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_Type", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", Me.nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Me.dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                sClientBeaber = .Parameters("sClient").Value
                .RCloseRec()
            Else
                sClientBeaber = ""
            End If
        End With

        If sClientBeaber <> "" Then
            lreccreLettAccuse = New eRemoteDB.Execute
            lreccreLettAccuse.SQL = "INSERT INTO LETTACCUSE (NLETTREQUEST, SCLIENT, DANSWERDATE, DTOHANDOVER,DCOMPDATE,NUSERCODE, TLETTER, NTYPELETTER, NSTATLETTER) " & _
                                                       "VALUES (" & Me.nLettRequest & ",'" & sClientBeaber & "',null,null,SYSDATE," & Me.nUsercode & ",:TLETTER," & "2, 1)"
            lreccreLettAccuse.Parameters.Add(":TLETTER", CleanLetter((Me.tletter)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            lreccreLettAccuse.Run(False)

            lreccreLettAccuse = Nothing
            'End If

            lrecreaBeaber = Nothing
        End If

        lreccreLettRequest = Nothing
        Exit Function
    End Function

    Public Function Add_PrintDocuments(Optional ByVal nLettRequest As Short = 0, Optional ByVal nLetterNum As Short = 0, Optional ByVal DinpDate As Date = #12:00:00 AM#, Optional ByVal dExpDate As Date = #12:00:00 AM#, Optional ByVal dPrintDate As Date = #12:00:00 AM#, Optional ByVal sStatregt As String = "", Optional ByVal nUser_Sol As Short = 0, Optional ByVal nUsercode As Short = 0, Optional ByVal sStreet As String = "", Optional ByVal nSendType As Short = 0, Optional ByVal nTypRequest As Short = 0, Optional ByVal dEffecDate As Date = #12:00:00 AM#, Optional ByVal sClient As String = "", Optional ByVal sCertype As String = "", Optional ByVal nBranch As Short = 0, Optional ByVal nProduct As Short = 0, Optional ByVal nPolicy As Integer = 0, Optional ByVal nCertif As Integer = 0, Optional ByVal nClaim As Integer = 0, Optional ByVal nCase_num As Short = 0, Optional ByVal nBordereaux As Integer = 0, Optional ByVal tletter As String = "", Optional ByVal nCustomDescript As Short = 0, Optional ByVal nEndorseType As Short = 0) As Boolean
        Dim lreccreLettRequest As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lreccreLettRequest = New eRemoteDB.Execute

        Add_PrintDocuments = Me.Add(nLettRequest, nLetterNum, DinpDate, dExpDate, dPrintDate, sStatregt, nUser_Sol, nUsercode, sStreet, nSendType, nTypRequest, dEffecDate, sClient, sCertype, nBranch, nProduct, nPolicy, nCertif, nClaim, nCase_num, nBordereaux, tletter, nCustomDescript, nEndorseType)

        lreccreLettRequest = Nothing

        Exit Function
        lreccreLettRequest = Nothing
    End Function

    '**%Objective:
    '**%Parameters:
    '**%   sClient     - Code of client.
    '%Objetivo:
    '%Parámetros:
    '%    sClient      - Codigo del cliente.
    Public Function FindEndorsements(ByVal sClient As String, ByVal nUsercode As Short) As Object
        Dim lclsEndorsLetters As eLetter.EndorsLetterss

        FindEndorsements = Nothing

        lclsEndorsLetters = New eLetter.EndorsLetterss

        Call lclsEndorsLetters.FindEndorsLetters(sClient, nUsercode)

        lclsEndorsLetters = Nothing

        Exit Function
        lclsEndorsLetters = Nothing
    End Function
    '**%Objective: Been worth the originating values of entrance of page SCA008  (GRID)
    '**%Parameters:
    '**%   sCodispl    - Code of page.
    '**%   nAction     - Number action to execute
    '**%   sCertype    - Type or Record.Sole Values:1- Proposal 2 - Policy 3 - Quotation
    '**%   nBranch     - Code of the Line of Business.The possible values as per table 10.
    '**%   nProduct    - Code of The Product.
    '**%   nPolicy     - Number identifying the policy/ quotation/ proposal
    '**%   nCertif     - Number of The Certificate.
    '**%   nClaim      - Number identifying the claim
    '**%   nCase_num   - Code identifying the claim case or claimant
    '**%   nBordereaux - Number of the collection schedule or form
    '**%   sClient     - Code of client.
    '**%   dEffecDate  - Date which from the record is valid.
    '%Objetivo: Valida los valores de entrada provenientes de la página SCA008 (GRID)
    '%Parámetros:
    '%    sCodispl     - Codigo logico de la pagina.
    '%    nAction      - Numero de la accion a ejecutarse
    '%    sCertype     - Tipo de registro.Valores únicos: 1 - Solicitud 2 - Póliza 3 - Cotización
    '%    nBranch      - Código del ramo comercial.Valores posibles según tabla 10.
    '%    nProduct     - Código del producto.
    '%    nPolicy      - Número identificativo de la póliza/ cotización/ solicitud
    '%    nCertif      - Número identificativo del certificado.
    '%    nClaim       - Número que identifica al siniestro
    '%    nCase_num    - Código identificativo del caso o reclamante
    '%    nBordereaux  - Número de la relación de cobro
    '%    sClient      - Codigo del cliente.
    '%    dEffecDate   - Fecha de efecto del registro.
    Public Function ValSCA008Grid(ByVal sCodispl As String, ByVal nAction As Short, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Short = -32768, Optional ByVal nProduct As Short = -32768, Optional ByVal nPolicy As Integer = -32768, Optional ByVal nCertif As Integer = -32768, Optional ByVal nClaim As Integer = -32768, Optional ByVal nCase_num As Short = -32768, Optional ByVal nBordereaux As Integer = -32768, Optional ByVal sClient As String = "", Optional ByVal dEffecDate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Short = 0, Optional ByVal sCodispII As String = "") As String
        Dim lcolRequests As eLetter.LettRequests
        Dim lobjRequest As eLetter.LettRequest
        Dim lclsErrors As eFunctions.Errors
        Dim bRequired As Boolean

        If Not IsIDEMode() Then
        End If

        ValSCA008Grid = String.Empty

        lcolRequests = New eLetter.LettRequests
        lclsErrors = New eFunctions.Errors

        If lcolRequests.Find(sCodispl, 302, sCertype, nBranch, nProduct, nPolicy, nCertif, nClaim, nCase_num, nBordereaux, sClient, dEffecDate, nUsercode) Then

            For Each lobjRequest In lcolRequests

                With lobjRequest
                    If (.sRequired = "1" And .nLettRequest = intNull) Then
                        bRequired = True
                        Exit For
                    End If
                End With
            Next lobjRequest

            If bRequired Then
                If sCodispII = String.Empty Then
                    lclsErrors.ErrorMessage(sCodispl, 8222)
                Else
                    lclsErrors.ErrorMessage(sCodispII, 8222)
                End If
                ValSCA008Grid = lclsErrors.Confirm
            End If
        End If

        lcolRequests = Nothing
        lobjRequest = Nothing
        lclsErrors = Nothing

        Exit Function
        lclsErrors = Nothing
    End Function


    '**%Objective: Been worth the originating values of entrance of page SCA008
    '**%Parameters:
    '**%    sDescript       - Description of the object
    '**%    dExpDate        - Date when the letter must be removed from the system
    '**%    sFileName       - Route of the file to customized.
    '**%    chkCustom       - Personalized letter.
    '**%    sAddress        - Direction of the sending of the correspondence
    '%Objetivo: Valida los valores de entrada provenientes de la página SCA008
    '%Parámetros:
    '%      sDescript       - Descripción del objeto
    '%      dExpDate        - Fecha en la cual la carta debe ser eliminada del sistema
    '%      sFileName       - Ruta del archivo a personalizado.
    '%      chkCustom       - Carta personalizada.
    '%      sAddress        - Dirección del envio de la correspondencia
    Public Function ValSCA008(ByVal sDescript As String, ByVal dExpDate As Date, ByVal sFileName As String, ByVal chkCustom As Short, ByVal sAddress As String) As String
        Dim lobjError As eFunctions.Errors
        Dim lobjValues As eFunctions.Values

        If Not IsIDEMode() Then
        End If

        lobjError = New eFunctions.Errors
        lobjValues = New eFunctions.Values

        '**+ Been worth that the direction is full
        '+ Valida que la dirección esté llena
        'If sAddress = String.Empty Then
        'lobjError.ErrorMessage(sCodispl, 8356)
        'End If
        '**+ Been worth that the Maxima date is greater to the date of the day
        '+ Valida que la fecha máxima sea mayor a la fecha del día
        'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'If dExpDate < Today And Not IsNothing(dExpDate) Then
        '    lobjError.ErrorMessage(sCodispl, 2086)
        'End If
        '**+ Been worth that the location of the file this flood if customized action is fulfilled the condition.
        '+ Valida que la ubicación del archivo este llena si se cumple con la condición acción personalizada.
        'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If chkCustom = 1 And IsNothing(sFileName) Then
            lobjError.ErrorMessage(sCodispl, 1925)
        End If
        '**+ Been worth that the description is full
        '+ Valida que la descripción esté llena
        If sDescript = String.Empty Then
            lobjError.ErrorMessage(sCodispl, 3872)
        End If
        '**+It validates that the route and the file are valid.
        '+ Valida que la ruta y el archivo sean válidos
        'If chkCustom = 1 And sFileName = String.Empty Then
        'lobjError.ErrorMessage(sCodispl, 8340)
        'End If


        ValSCA008 = lobjError.Confirm
        lobjError = Nothing
        lobjValues = Nothing

        Exit Function
        lobjError = Nothing
        lobjValues = Nothing
    End Function

    '**%Objective: Function of the entrance of new correspondences generated by the SCA008.
    '**%Parameters:
    '**%  lintAction        - Type of action to execute
    '**%  sCodispl          - logical Code of the page
    '**%  nLettRequest      - Number of the request for remittance of  correspondence
    '**%  nLetterNum        - Number identifying the letter templates
    '**%  sDescript         - Description of the object
    '**%  nSendType         - Sent type Sole Values: 1 - email 2 - Post service 3 - Facsimile
    '**%  dExpDate          - Date when the letter must be removed from the system
    '**%  tletter           - Variable that contains the code of the letter.
    '**%  sAddress          - Direction of envio of the correspondence
    '**%  sClient           - Code of client
    '**%  sCertype          - Type or Record.Sole Values:1- Proposal 2 - Policy 3 - Quotation
    '**%  nBranch           - Code of the Line of Business.The possible values as per table 10.
    '**%  nProduct          - Code of The Product.
    '**%  nPolicy           - Number identifying the policy
    '**%  nCertif           - Number of The Certificate.
    '**%  nClaim            - Number identifying the claim
    '**%  nCase_num         - Code identifying the claim case or claimant
    '**%  nBordereaux       - Number of the collection schedule or form
    '**%  DinpDate          - Date when the request is recorded
    '**%  nUsercode         - Code of the user creating or updating the record.
    '**%  nCustomDescript    - Indicates if this peronalizada or not.
    '**%  dEffecDate        - Date which from the record is valid.
    '%Objetivo: Tiene como función del ingreso de nuevas correspondencias generados por la SCA008.
    '%Parámetros:
    '%  lintAction          - Tipo de acción a ejecutar
    '%  sCodispl            - Codigo logico de la pagina
    '%  nLettRequest        - Numero de la solicitu.
    '%  nLetterNum          - Numero que identifica el tipo de carta.
    '%  sDescript           - Descripción del objeto
    '%  nSendType           - Tipo de envio, posibles valores: 1 - Email 2 - Correo 3 - Fax
    '%  dExpDate            - Fecha cuando se elimino el modelo de carta del sistema.
    '%  tletter             - Variable que contiene el código de la carta.
    '%  sAddress            - Dirección de envio de la correspondencia
    '%  sClient             - Codigo del cliente
    '%  sCertype            - Tipo o registro: Valores posibles: 1- Propuesta 2 - Poliza 3 - Cotización
    '%  nBranch             - Codigo del ramo. Valores posibles segun la tabla 10.             - Code of the Line of Business.The possible values as per table 10.
    '%  nProduct            - Codigo del producto.
    '%  nPolicy             - Numero que identifica la poliza
    '%  nCertif             - Numero del certificado
    '%  nClaim              - Numero que identifica el siniestro
    '%  nCase_num           - Código identificativo del caso o reclamante.
    '%  nBordereaux         - Numero de la colección, schedule o forma.
    '%  DinpDate            - Fecha cuando el registro fue almacenado
    '%  nUsercode           - Codigo del usuario que ejecuta la acción
    '%  nCustomDescript      - Indica si la carta esta peronalizada o no
    '%  dEffecDate          - Fecha de efecto del registro
    Public Function PostSCA008(ByVal sCodispl As String, ByVal nLettRequest As Short, ByVal nLetterNum As Short, ByVal nLanguage As Short, ByVal sDescript As String, ByVal nSendType As Short, ByVal dExpDate As Date, ByVal tletter As String, ByVal sAddress As String, ByVal sClient As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nClaim As Integer, ByVal nCase_num As Short, ByVal nBordereaux As Integer, ByVal DinpDate As Date, ByVal nUsercode As Short, ByVal nCustomDescript As Short, ByVal dEffecDate As Date, ByVal nDeman_type As Short) As Boolean
        Dim lobjWin As Object
        Dim lobjLetter As eLetter.Letter
        Dim nCustomDe As Short
        Dim lreccreLettRequest As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        If nCustomDescript = 1 Then
            sDescript = String.Empty
            nCustomDe = 1
        Else
            nCustomDe = 2
        End If

        lreccreLettRequest = New eRemoteDB.Execute
        With lreccreLettRequest
            .StoredProcedure = "reaClientLanguage"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nLanguage = .FieldToClass("nLanguage")
                .RCloseRec()
            End If
        End With

        lreccreLettRequest = Nothing

        If nLettRequest <> intNull Then
            Me.nLettRequest = nLettRequest
        End If
        '**+ If the request does not exist, it is created
        '+ Si la solicitud no existe, se crea

        If nLettRequest = intNull Then
            PostSCA008 = Add(0, nLetterNum, DinpDate, dExpDate, CDate(Nothing), "1", nUsercode, nUsercode, sAddress, nSendType, 1, dEffecDate, sClient, sCertype, nBranch, nProduct, nPolicy, nCertif, nClaim, nCase_num, nBordereaux, tletter, nCustomDescript)
            insgetLettValues(sCodispl)
        Else
            PostSCA008 = Update(nLettRequest, nLetterNum, DinpDate, dExpDate, CDate(Nothing), "1", nUsercode, nUsercode, sAddress, nSendType, 1, dEffecDate, sClient, sCertype, nBranch, nProduct, nPolicy, nCertif, nClaim, nCase_num, nBordereaux, tletter, nCustomDescript)
        End If

        'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If IsNothing(tletter) Or tletter = vbNullString Then
            lobjLetter = New eLetter.Letter

            With lobjLetter.oParameters
                Select Case sCodispl
                    '+ Clientes
                    Case "SCA801"
                        .Add(sClient)
                        '+ Pólizas
                    Case "SCA802"
                        .Add(sCertype)
                        .Add(nBranch)
                        .Add(nProduct)
                        .Add(nPolicy)
                        .Add(nCertif)
                        '+ Siniestros
                    Case "SCA803"
                        .Add(nClaim)
                        .Add(nCase_num)
                        .Add(nDeman_type)
                    Case "SCA805"
                        .Add(sClient)
                        .Add(sCertype)
                        .Add(nBranch)
                        .Add(nProduct)
                        .Add(nPolicy)
                        .Add(nCertif)
                        .Add(nClaim)
                        .Add(nCase_num)
                        .Add(nDeman_type)
                End Select
            End With

            Call lobjLetter.MergeDocument(Nothing, Nothing, Today, nUsercode, False, nCustomDe, nLetterNum, nLanguage, String.Empty, Me.nLettRequest, False)
            tletter = lobjLetter.sMergeResult
        End If

        Select Case sCodispl
            '**+ Clients
            '+ Clientes
            Case "SCA801"
                lobjWin = eRemoteDB.NetHelper.CreateClassInstance("eClient.ClientWin")
                Call lobjWin.insUpdClient_win(sClient, sCodispl, "2", , , nUsercode)
                '**+ Policy
                '+ Polizas
            Case "SCA802"
                lobjWin = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_Win")
                lobjWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, DinpDate, nUsercode, sCodispl, "2")
                '**+ Claim
                '+ Siniestros
            Case "SCA803"
                lobjWin = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim_Win")
                lobjWin.Add_Claim_win(nClaim, sCodispl, "2", nUsercode)
        End Select
        lobjWin = Nothing

        Exit Function
        lobjWin = Nothing
    End Function

    '**%Objective: Delete an request.
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '%Objetivo: Elimina una solicitud.
    '%Parámetros:
    '%  nLettRequest    - Numero de la solicitud
    Public Function Delete(Optional ByVal nLettRequest As Short = -32768, Optional ByVal sClient As String = "", Optional ByVal sCodispl As String = "") As Boolean
        Dim lrecdelLettRequest As eRemoteDB.Execute
        Dim lclsClientWin As Object

        lclsClientWin = eRemoteDB.NetHelper.CreateClassInstance("eClient.ClientWin")

        If Not IsIDEMode() Then
        End If

        lrecdelLettRequest = New eRemoteDB.Execute
        If nLettRequest <> intNull Then
            Me.nLettRequest = nLettRequest
        End If
        With lrecdelLettRequest
            .StoredProcedure = "delLettRequest"
            .Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Delete = True
                If sClient <> String.Empty Then
                    If .Parameters("nCount").Value = 0 Then
                        Call lclsClientWin.insUpdClient_win(sClient, sCodispl, "1", , , nUsercode)
                    End If
                End If
            End If
        End With
        lrecdelLettRequest = Nothing
        lclsClientWin = Nothing

        Exit Function
        lrecdelLettRequest = Nothing
        lclsClientWin = Nothing
    End Function

    '**%Objective: It executes the action of mergedocument, by means of this we can take a document and unfold it
    '**%Parameters:
    '**%  lintAction        - Type of action to execute.
    '**%  sCodispl          - logical Code of the page.
    '**%  nAction           - Type of action to execute.
    '**%  sCertype          - Type or Record.Sole Values:1- Proposal 2 - Policy 3 - Quotation
    '**%  nBranch           - Code of the Line of Business.The possible values as per table 10.
    '**%  nProduct          - Code of The Product.
    '**%  nPolicy           - Number of policy.
    '**%  nCertif           - Number of The Certificate.
    '**%  nClaim            - Number identifying the claim.
    '**%  nCase_num         - Code identifying the claim case or claimant.
    '**%  nDeman_type       - Claim type Possible values as per table 692.
    '**%  nBordereaux       - Number of the collection schedule or form.
    '**%  sClient           - Code of client.
    '**%  dEffecDate        - Date which from the record is valid.
    '**%  nReceipt          - Receipt number.
    '**%  nDigit            - Receipt Control Digit.
    '**%  nPayNumbe         - Number of the payment associated with the receipt payment agreement
    '**%  nUsercode         - Code of the user creating or updating the record.
    '**%  nLetterNum        - Number identifying the letter templates.
    '**%  nLettRequest      - Number of the request for remittance of  correspondence.
    '**%  bPreview          - Boolean variable of type, indicates if it is due to store or not it generated correspondence.
    '**%  lcolLettValuess   - Variable of type collection, stores the return of LettValuess.
    '%Objetivo: Ejecuta la acción del mergedocument, por medio de este podemos tomar un documento y desplegarlo
    '%Parámetros:
    '%  lintAction          - Tipo de acción a ejecutarse.
    '%  sCodispl            - Codigo logico de la pagina.
    '%  nAction             - Tipo de acción a ejecutarse.
    '%  sCertype            - Tipo o registro: Valores posibles: 1- Propuesta 2 - Poliza 3 - Cotización
    '%  nBranch             - Codigo del ramo. Valores posibles segun la tabla 10.
    '%  nProduct            - Codigo del producto.
    '%  nPolicy             - Numero de la poliza.
    '%  nCertif             - Numero del certificacdo.
    '%  nClaim              - Numero que identifica el siniestro.
    '%  nCase_num           - Código identificativo del caso o reclamante.
    '%  nDeman_type         - Tipos de reclamos, posibles valores según table 692.
    '%  nBordereaux         - Numero de la colección, schedule o forma.
    '%  sClient             - Codigo del cliente.
    '%  dEffecDate          - Fecha de efecto del registro.
    '%  nReceipt            - Numero del recibo.
    '%  nDigit              - Digitos de control del recibo.
    '%  nPayNumbe           - Número del pago que se asoció al acuerdo del pago del recibo.
    '%  nUsercode           - Codigo del usuario que crea o actualiza el registro.
    '%  nLetterNum          - Numero que identifica el tipo de carta.
    '%  nLettRequest        - Numero de la solicitud.
    '%  bPreview            - Variable de tipo booleana, indica si se debe almacenar o no la correspondencia generada.
    '%  lcolLettValuess     - Variable de tipo colección, almacena el retorno de LettValuess
    Public Function MergeDocumentLR(ByVal sCodispl As String, ByVal nAction As Short, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Short = -32768, Optional ByVal nProduct As Short = -32768, Optional ByVal nPolicy As Integer = -32768, Optional ByVal nCertif As Integer = -32768, Optional ByVal nClaim As Integer = -32768, Optional ByVal nCase_num As Short = -32768, Optional ByVal nDeman_type As Short = -32768, Optional ByVal nBordereaux As Integer = -32768, Optional ByVal sClient As String = "", Optional ByVal dEffecDate As Date = #12:00:00 AM#, Optional ByVal nReceipt As Integer = -32768, Optional ByVal nDigit As Short = -32768, Optional ByVal nPayNumbe As Short = -32768, Optional ByVal nUsercode As Short = 0, Optional ByVal nLetterNum As Short = -32768, Optional ByVal nLettRequest As Short = -32768, Optional ByVal bPreview As Boolean = False, Optional ByVal lcolLettValuess As LettValuess = Nothing) As Object
        Dim lreccreLettRequest As eRemoteDB.Execute
        Dim lcolParameters As Collection
        Dim lclsLettValues As LettValues

        If Not IsIDEMode() Then
        End If

        lreccreLettRequest = New eRemoteDB.Execute
        lclsLettValues = New LettValues

        oLetter = mobjletter

        MergeDocumentLR = True
        If nLetterNum <> intNull Or Not oLetter Is Nothing Then
            lcolParameters = New Collection
            With oLetter
                If lcolLettValuess Is Nothing Then
                    lcolLettValuess = New LettValuess
                    If lcolLettValuess.Find(nLettRequest, 1) Then
                        For Each lclsLettValues In lcolLettValuess
                            lcolParameters.Add(lclsLettValues.sValue)
                        Next lclsLettValues
                        .MergeDocument(lcolParameters, Nothing, dEffecDate, nUsercode, False, 1, oLetter.nLetterNum, oLetter.nLanguage, String.Empty, nLettRequest, bPreview)
                        sMergeResult = oLetter.sMergeResult
                    End If
                Else
                    .MergeDocument(lcolParameters, Nothing, dEffecDate, nUsercode, False, 1, , oLetter.nLanguage, CStr(nLettRequest), bPreview)
                    sMergeResult = oLetter.sMergeResult
                End If
            End With
            lcolParameters = Nothing
        End If
        Exit Function
        lreccreLettRequest = Nothing
        lclsLettValues = Nothing
        lcolParameters = Nothing
    End Function

    '**%Objective: Been worth the fields associated to the headed one of the sequence of request of shipments (LT003_K)
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  DinpDate      - Date when the request is recorded
    '**%  nLetterNum    - Number identifying the letter templates
    '**%  nAction       - Type of action to execute
    '%Objetivo: Valida los campos asociados al encabezado de la secuencia de solicitud de envíos (LT003_K)
    '%Parámetros:
    '%    nLettRequest  - Numero de la solicitud
    '%    DinpDate      - Fecha cuando fue almacenado el requerimiento
    '%    nLetterNum    - Numero que identifica el tipo de carta
    '%    nAction       - Tipo de acción a ejecutarse
    Public Function insValLT003_K(ByVal nLettRequest As Integer, ByVal DinpDate As Date, ByVal nLetterNum As Short, ByVal nAction As Short) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjValues As eFunctions.Values

        If Not IsIDEMode() Then
        End If

        insValLT003_K = String.Empty
        lobjErrors = New eFunctions.Errors
        lobjValues = New eFunctions.Values

        '**+ Been worth that has add a number of request (only if it is to consult)
        '+ Se valida que se haya incluído un número de solicitud (sólo si es consultar)

        If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
            If nLettRequest = intNull Or nLettRequest = 0 Then
                lobjErrors.ErrorMessage("LT003_K", 8206, , eFunctions.Errors.TextAlign.LeftAling) '(Solicitud)
            Else
                'If Not insValLettRequest(nLettRequest) Then
                '   lobjErrors.ErrorMessage("LT003_K", 90291, , eFunctions.Errors.TextAlign.LeftAling)
                'End If
                If Not insValLettRequest(nLettRequest) Then
                    lobjErrors.ErrorMessage("LT003_K", 8207, , eFunctions.Errors.TextAlign.LeftAling)
                End If

            End If
        Else
            If nAction = 301 Then
                '**+ Been worth that has introduced a letter model
                '+ Se valida que se haya introducido un modelo de carta
                If nLetterNum = 0 Or nLetterNum = intNull Then
                    lobjErrors.ErrorMessage("LT003_K", 8001, , eFunctions.Errors.TextAlign.LeftAling)
                Else
                    '**+It is validated that the number of the letter requested exist in the system (Alone when the action is to register)
                    '+ Se valida que el número de la carta solicitada exista en el sistema (Solo cuando la acción es registrar)
                    If Not IsExist(nLetterNum) Then
                        lobjErrors.ErrorMessage("LT003_K", 8048, , eFunctions.Errors.TextAlign.LeftAling)
                    Else
                        Me.nLetterNum = nLetterNum
                        Me.DinpDate = DinpDate
                    End If
                End If
            End If
        End If

        '**+ Been worth that the introduced date is valid
        '+ Se valida que la fecha introducida sea válida

        'If DinpDate = dtmNull Then
        'lobjErrors.ErrorMessage("LT003_K", 8048, , eFunctions.Errors.TextAlign.LeftAling, lobjValues.getMessage(1006))
        'End If


        insValLT003_K = lobjErrors.Confirm
        lobjValues = Nothing
        lobjErrors = Nothing

        Exit Function
        lobjValues = Nothing
        lobjErrors = Nothing
    End Function

    '**%Objective: Been worth the fields associated to the headed one of frame of "Detail of the Request"
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  DinpDate      - Date when the request is recorded
    '**%  nLetterNum    - Number identifying the letter templates
    '**%  nTypRequest   - Request type Sole Values: 1 - Individual 2 - Massive
    '**%  sSendType     - Sent type Sole Values: 1 - email 2 - Post service 3 - Facsimile
    '**%  dExpDate      - Date when the letter must be removed from the system
    '**%  dPrintDate    - Date when the letter is printed.
    '**%  sClient       - Code of client
    '**%  nAction       - Type of action to execute
    '%Objetivo: Valida los campos asociados al encabezado del frame de "Detalle de la Solicitud"
    '%Parámetros:
    '%  nLettRequest    - Numero de la solicitud
    '%  DinpDate        - Fecha cuando el registro fue almacenado
    '%  nLetterNum      - Numero que identifica el tipo de carta
    '%  nTypRequest     - Tipo de envío.Valores únicos 1 - Individual 2 - Masivo
    '%  sSendType       - Tipo de envio posibles valores: 1 - email 2 - Correo 3 - Fax
    '%  dExpDate        - Fecha en la cual la carta debe ser eliminada del sistema
    '%  dPrintDate      - Fecha de la impresión de la carta.
    '%  sClient         - Codigo del cliente
    '%  nAction         - Tipo de acción a ejecutarse
    Public Function insValLT003(ByVal nLettRequest As Integer, ByVal DinpDate As Date, ByVal nLetterNum As Short, ByVal nTypRequest As Short, ByVal sSendType As String, ByVal dExpDate As Date, ByVal dPrintDate As Date, ByVal sClient As String, ByVal nAction As Short) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjValues As eFunctions.Values

        If Not IsIDEMode() Then
        End If

        insValLT003 = String.Empty
        lobjErrors = New eFunctions.Errors
        lobjValues = New eFunctions.Values


        '**+ Been worth the type of send.
        '+ Se valida el tipo de envío
        If sSendType = String.Empty Then
            lobjErrors.ErrorMessage("LT003", 8205)
        End If

        '**+ Been worth that the maximum date is posrterior to the date of the request
        '+ Se valida que la fecha máxima sea posrterior a la fecha de la solicitud
        If dExpDate <> dtmNull Then
            If dExpDate <= DinpDate Then
                lobjErrors.ErrorMessage("LT003", 90290, , eFunctions.Errors.TextAlign.LeftAling) '"(Fecha máxima de permanencia)"
            End If
        End If

        insValLT003 = lobjErrors.Confirm
        lobjValues = Nothing
        lobjErrors = Nothing

        Exit Function
        lobjValues = Nothing
        lobjErrors = Nothing
    End Function

    '**%Objective: Been worth the fields associated to the headed one of frame of "Parameters of the request"
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  DinpDate      - Date when the request is recorded
    '**%  nLetterNum    - Number identifying the letter templates
    '**%  sVariable     - Name of The Variable used in Correspondence.
    '**%  sValue        - Parameter or variable value
    '**%  nTypRequest   - Request type Sole Values: 1 - Individual 2 - Massive
    '**%  bPopUp        - Variable boolena, indicates if it comes from a PopUp window
    '**%  nAllValues    - Variable in the validation block.
    '%Objetivo: Valida los campos asociados al encabezado del frame de "Parámetros de la solicitud"
    '%Parámetros:
    '%    nLettRequest  - Numero de la solicitud
    '%    DinpDate      - Fecha cuando el registro fue almacenado
    '%    nLetterNum    - Numero que identifica el tipo de carta
    '%    sVariable     - Nombre de la variable que será usada en la correspondencia.
    '%    sValue        - Parametros o valor de variables
    '%    nTypRequest   - Tipos de requerimientos, posibles valores: 1- Individual 2- Masiva.
    '%    bPopUp        - Variable boolena, indica si viene de una ventana PopUp
    '%    nAllValues    - Variable en el bloque de validación.
    Public Function insValLT031(ByVal nLettRequest As Integer, ByVal DinpDate As Date, ByVal nLetterNum As Short, ByVal sVariable As String, ByVal sValue As String, ByVal nTypRequest As Short, ByVal bPopUp As Boolean, ByVal nAllValues As Short) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjValues As eFunctions.Values

        If Not IsIDEMode() Then
        End If

        insValLT031 = String.Empty
        lobjErrors = New eFunctions.Errors
        lobjValues = New eFunctions.Values
        If bPopUp Then

            '**+ Been worth the value introduced for the parameter
            '+ Se valida el valor introducido para el parámetro
            If sValue = String.Empty Then
                lobjErrors.ErrorMessage("LT031", 8353) '  "(Valor del parámetro)"
            End If
            '**+ Been worth that being the individual request, all the parameters have value if it is a massive validation
            '+ Se valida que siendo la solicitud individual, todos los parámetros tengan valor si es una validación masiva
        Else
            If nTypRequest = CN_INDIVIDUAL Then
                If nAllValues = CN_NOT_ALL Then
                    lobjErrors.ErrorMessage("LT031", 8353) '"(Parámetros)"
                End If
            End If
        End If
        '**+ Been worth that being ipode individual request, fills to the column "value" corresponding to all the "items" of grid.
        '+ Se valida que siendo el ipode solicitud individual, se llene la columna "valor" correspondiente a todos los "items" del grid.
        insValLT031 = lobjErrors.Confirm
        lobjValues = Nothing
        lobjErrors = Nothing

        Exit Function
        lobjValues = Nothing
        lobjErrors = Nothing
    End Function

    '**%Objective: Been worth the date of correspondence elimination
    '**%Parameters:
    '**% sCodispl       - Logical code of page
    '**% dInpDate:      - Selected date
    '%Objetivo: Valida la fecha de eliminación de correspondencia
    '%Parámetros:
    '%  sCodispl:       - Codigo logico de la pagina
    '%  sInpDate:       - Fecha de seleccionada
    Public Function insValLTL001(ByVal sCodispl As String, ByVal sInpDate As String) As String
        Dim lclsErrors As eFunctions.Errors

        If Not IsIDEMode() Then
        End If

        lclsErrors = New eFunctions.Errors
        '**+ Date of process must be full
        '+ Fecha de proceso debe estar lleno
        If sInpDate = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 1967)
        End If

        insValLTL001 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
        lclsErrors = Nothing
    End Function

    '**%Objective: Been worth the date of correspondence elimination
    '**%Parameters:
    '**% dInpDate:      - Selected date
    '**% lstrClient     - Code of cliente.
    '**% nLetterRequest - Number of request.
    '%Objetivo: Valida la fecha de eliminación de correspondencia
    '%Parámetros:
    '%  dInpDate:       - Fecha de seleccionada
    '%  lstrClient:     - Código del cliente seleccionado.
    '%  nLetterRequest  - Numero de la solicitud.
    Public Function insValLTL002(ByVal dInpDate As Date, ByVal lstrClient As String, ByVal nLetterRequest As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsLetter As eLetter.LettRequest

        If Not IsIDEMode() Then
        End If

        lclsErrors = New eFunctions.Errors
        '**+ Date of process must be full
        '+ Fecha de proceso debe estar lleno
        If dInpDate = dtmNull And lstrClient = String.Empty And nLetterRequest <= 0 Then
            Call lclsErrors.ErrorMessage("LTL002", 8088)
        End If

        If lstrClient <> String.Empty And nLetterRequest < 0 Then
            Call lclsErrors.ErrorMessage("LTL002", 8052)
        End If

        '**- If the field request this full.
        '- Si el campo solicitud esta lleno.
        If nLetterRequest > 0 Then
            lclsLetter = New eLetter.LettRequest
            If Not lclsLetter.Find(nLetterRequest) Then
                Call lclsErrors.ErrorMessage("LTL002", 8051)
            End If
        End If

        insValLTL002 = lclsErrors.Confirm
        lclsErrors = Nothing

        Exit Function
        lclsErrors = Nothing
    End Function

    '**%Objective: Been worth the date of correspondence elimination
    '**%Parameters:
    '**% sInpDate:      - Selected date
    '%Objetivo: Valida la fecha de eliminación de correspondencia
    '%Parámetros:
    '%  sInpDate:       - Fecha de seleccionada
    Public Function insValLTL971(ByVal sInpDate As String) As String
        Dim lclsErrors As eFunctions.Errors
        'Dim lclsLetter As eLetter.LettRequest

        lclsErrors = New eFunctions.Errors

        '**+ Date of process must be full
        '+ Fecha de proceso debe estar lleno
        If sInpDate = dtmNull Then
            Call lclsErrors.ErrorMessage("LTL971", 30123)
        End If

        insValLTL971 = lclsErrors.Confirm
        lclsErrors = Nothing

        Exit Function
        lclsErrors = Nothing
    End Function

    '**%Objective: Been worth that exists the number of the request in Base de Datos if the action is consultation
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '%Objetivo: Valida que exista el número de la solicitud en la Base de Datos si la acción es consulta
    '%Parámetros:
    '%  nLettRequest    - Numero de la solicitud
    Private Function insValLettRequest(Optional ByVal nLettRequest As Integer = 0) As Boolean
        Dim lrecreaLettRequest As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lrecreaLettRequest = New eRemoteDB.Execute
        With lrecreaLettRequest
            .StoredProcedure = "reaLettReqExist"
            .Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 4, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insValLettRequest = .Run
        End With
        lrecreaLettRequest = Nothing

        Exit Function
        lrecreaLettRequest = Nothing
    End Function

    '**%Objective: It adds the parametros to the object, for the search of its values.
    '**%Parameters:
    '**%  sCodispl      - logical Code of the page
    '%Objetivo: Agrega los parametros al objeto mvarLettValues, para la busqueda de sus valores.
    '%Parámetros:
    '%  sCodispl        - Codigo logico de la pagina
    Private Function insgetLettValues(ByVal sCodispl As String) As Boolean
        If Not IsIDEMode() Then
        End If

        If mvarLettValues Is Nothing Then
            mvarLettValues = New LettValuess
        End If
        Select Case sCodispl
            '**+ Clients
            '+ Clientes
            Case "SCA801"
                mvarLettValues.Add(nLettRequest, 0, , , "sclient", sClient, nUsercode, , 1)
                '**+ Policy
                '+ Pólizas
            Case "SCA802"
                mvarLettValues.Add(nLettRequest, 0, , , "scertype", sCertype, nUsercode, , 1)
                mvarLettValues.Add(nLettRequest, 0, , , "nbranch", CStr(nBranch), nUsercode, , 1)
                mvarLettValues.Add(nLettRequest, 0, , , "nproduct", CStr(nProduct), nUsercode, 1)
                mvarLettValues.Add(nLettRequest, 0, , , "npolicy", CStr(nPolicy), nUsercode, , 1)
                mvarLettValues.Add(nLettRequest, 0, , , "ncertif", CStr(nCertif), nUsercode, , 1)
                '**+ Claims
                '+ Siniestros
            Case "SCA803"
                mvarLettValues.Add(nLettRequest, 0, , , "nclaim", CStr(nClaim), nUsercode, , 1)
                mvarLettValues.Add(nLettRequest, 0, , , "ncase_num", CStr(nCase_num), nUsercode, , 1)
                mvarLettValues.Add(nLettRequest, 0, , , "ndeman_type", CStr(nDeman_type), nUsercode, , 1)
                mvarLettValues.Add(nLettRequest, 0, , , "nbranch", CStr(nBranch), nUsercode, , 1)
                mvarLettValues.Add(nLettRequest, 0, , , "ncertif", CStr(nCertif), nUsercode, , 1)
                '** Receive
                '+ Cobranzas
            Case "SCA804"
                mvarLettValues.Add(nLettRequest, 0, , , "nbordereaux", CStr(nBordereaux), nUsercode, , 1)
        End Select
        insgetLettValues = mvarLettValues.Update()

        Exit Function
    End Function

    '**%Objective: Realiza el manejo según la acción para la llave de la tabla Lett_Request
    '**%Parameters:
    '**% nAction       - Type of action to execute.
    '**% nLettRequest  - Number of the request for remittance of  correspondence.
    '**% nLetterNum    - Number identifying the letter templates.
    '**% DinpDate      - Date when the request is recorded.
    '**% nUsercode     - Code of the user creating or updating the record.
    '**% nUser_sol     - Code of The User Requester.
    '%Objetivo: Realiza el manejo según la acción para la llave de la tabla LettRequest
    '%Parámetros:
    '%   nAction        - Numero de la acción a ejecutarse.
    '%   nLettRequest   - Numero de la solicitud.
    '%   nLetterNum     - Numero que identifica el tipo de carta.
    '%   DinpDate       - fecha cuando el registro fue almacenado.
    '%   nUsercode      - Codigo del usuario que crea o actualiza el registro.
    '%   nUser_sol      - Codigo del usuario solicitante.
    Public Function insPostLT003_K(ByVal nAction As Short, ByVal nLettRequest As Integer, ByVal nLetterNum As Short, ByVal DinpDate As Date, ByVal nUsercode As Short, ByVal nUser_Sol As Short) As Boolean
        If Not IsIDEMode() Then
        End If

        With Me
            .nLettRequest = nLettRequest
            .nLetterNum = nLetterNum
            .DinpDate = DinpDate
            .dEffecDate = .oLetter.dEffecDate
            .nUsercode = nUsercode
            .nUser_Sol = nUser_Sol
        End With
        Select Case nAction
            Case eFunctions.Menues.TypeActions.clngActionadd
                With Me
                    insPostLT003_K = Add(0, .nLetterNum, .DinpDate, dtmNull, dtmNull, "1", .nUser_Sol, .nUsercode, String.Empty, intNull, intNull, .dEffecDate, String.Empty, String.Empty, intNull, intNull, intNull, intNull, intNull, intNull, intNull, String.Empty, intNull)
                End With
        End Select

        Exit Function
    End Function

    '**%Objective: Makes the handling according to the action for the key of the Lett_Request Table
    '**%Parameters:
    '**%   nAction          - Type of action to execute.
    '**%   nLettRequest     - Number of the request for remittance of  correspondence
    '**%   nLetterNum       - Number identifying the letter templates
    '**%   DinpDate         - Date when the request is recorded
    '**%   dExpDate         - Date when the letter must be removed from the system
    '**%   nTypeRequest     - Type of required request. Massive or individual.
    '**%   nSendType        - Sent type Sole values: 1 - email 2 - Post service 3 - Facsimile
    '**%   sClient          - Code of client
    '**%   dPrintDate       - Date when the letter is printed.
    '**%   nUsercode        - Code of the user creating or updating the record.
    '**%   nUser_sol        - Code of The User Requester.
    '%Objective: Realiza el manejo según la acción para la llave de la tabla Lett_Request
    '%Parámetros:
    '%     nAction          - Numero de la acción a ejecutarse.
    '%     nLettRequest     - Numero de la solicitud
    '%     nLetterNum       - Numero que identifica el tipo de carta
    '%     DinpDate         - Fecha cuando el registro fue almacenado
    '%     dExpDate         - Fecha en la cual la carta debe ser eliminada del sistema
    '%     nTypeRequest     - Tipo de solicitud requerida. Masiva o individual.
    '%     nSendType        - Tipo de envio, posibles valores: 1 - Email 2 - Correo 3 - Fax.
    '%     sClient          - Codigo del cliente
    '%     dPrintDate       - fecha de impresión de la carta.
    '%     nUsercode        - Codigo del usuario que crea o actualiza el registro.
    '%     nUser_sol        - Codigo del usuario solicitante.
    Public Function insPostLT003(ByVal nAction As Short, ByVal nLettRequest As Integer, ByVal nLetterNum As Short, ByVal DinpDate As Date, ByVal dExpDate As Date, ByVal nTypeRequest As Short, ByVal nSendType As Short, ByVal sClient As String, ByVal dPrintDate As Date, ByVal nUsercode As Short, ByVal nUser_Sol As Short, ByVal bSen As Boolean) As Boolean
        Dim lnLanguageUsers As eRemoteDB.Execute
        Dim lstrsStreet As String

        If Not IsIDEMode() Then
        End If

        lnLanguageUsers = New eRemoteDB.Execute
        With lnLanguageUsers
            .StoredProcedure = "reaUsersClient"
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nLanguage = .FieldToClass("nLanguage")
                lstrsStreet = .FieldToClass("sStreet")
                .RCloseRec()
            End If
        End With
        lnLanguageUsers = Nothing
        With Me
            .nLettRequest = nLettRequest
            .nLetterNum = nLetterNum
            .DinpDate = DinpDate
            .dExpDate = dExpDate
            .dEffecDate = .oLetter.dEffecDate
            .nTypRequest = nTypeRequest
            .nSendType = nSendType
            .sClient = sClient
            .dPrintDate = dExpDate
            .nUsercode = nUsercode
            .nUser_Sol = nUser_Sol
        End With
        Select Case nAction
            Case eFunctions.Menues.TypeActions.clngActionadd
                With Me
                    insPostLT003 = Update(.nLettRequest, .nLetterNum, .DinpDate, .dExpDate, .dPrintDate, "1", .nUser_Sol, .nUsercode, String.Empty, .nSendType, .nTypRequest, .dEffecDate, .sClient, String.Empty, intNull, intNull, intNull, intNull, intNull, intNull, intNull, String.Empty, intNull)
                End With
        End Select
        Dim lobjGrid As eFunctions.Grid
        If bSen = True Then
            lobjGrid = New eFunctions.Grid
            With lobjGrid
                .bOnlyForQuery = False
                .AddButton = False
                .DeleteButton = False
                .Codispl = "GE099"
                .AltRowColor = False
            End With
            With lobjGrid.Columns
                .AddTextColumn(0, "", "tctValue", 30, String.Empty, , , , "InsExecute()")
            End With
        End If
        Exit Function
    End Function

    '**%Objective: Add the new registry with the originating data of page LT031 to the data base
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  nParameter    - Code specifying the amount to be entered in the entry line
    '**%  sVariable     - Name of The Variable used in Correspondence.
    '**%  sValue        - Parameter or variable value
    '**%  nUsercode     - Code of the user creating or updating the record.
    '%Objetivo: Agrega el nuevo registro con los datos provenientes de la pagina LT031 a la base de datos
    '%Parámetros:
    '%  nLettRequest    - Numero de la solicitud
    '%  nParameter      - Codigo especifico de líneas de entrada
    '%  sVariable       - Nombre de la variable que será usada en la correspondecia.
    '%  sValue          - Valor del parametro o variable
    '%  nUsercode       - Codigo del usuario que crea o actualiza el registro
    Public Function insPostLT031(ByVal nLettRequest As Integer, ByVal nParameter As Short, ByVal sVariable As String, ByVal sValue As String, ByVal nUsercode As Short) As Boolean
        Dim lclsLettValues As LettValues

        If Not IsIDEMode() Then
        End If

        lclsLettValues = New LettValues
        With lclsLettValues
            insPostLT031 = .Add(nLettRequest, 0, intNull, nParameter, sVariable, sValue, nUsercode, CN_EQUAL)
        End With
        lclsLettValues = Nothing

        Exit Function
        lclsLettValues = Nothing
    End Function

    '**%Objective: Makes the consultation of all the requests emitted by a number of requirement
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '**%  lblnFind      - Variable of boolean condition
    '%Objetivo: Realiza la consulta de todas las solicitudes emitidas por un numero de requerimiento
    '%Parámetros:
    '%  lintLettRequest - Numero de requemiento
    '%  lblnFind        - Variable de condición boleana
    Public Function Find(ByVal lintLettRequest As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaLettRequest As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        If lintLettRequest <> mintLettRequest Or lblnFind Then
            lrecreaLettRequest = New eRemoteDB.Execute
            With lrecreaLettRequest
                .StoredProcedure = "reaLettRequest"
                .Parameters.Add("nLettRequest", lintLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("sCodispl", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nAction", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("sCertype", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCertif", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nClaim", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCase_num", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nBordereaux", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nLanguage", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    nLettRequest = .FieldToClass("nLettRequest")
                    nLetterNum = .FieldToClass("nLetterNum")
                    DinpDate = .FieldToClass("dInpDate")
                    dExpDate = .FieldToClass("dExpDate")
                    dPrintDate = .FieldToClass("dPrintDate")
                    sStatregt = .FieldToClass("sStatregt")
                    nUser_Sol = .FieldToClass("nUser_sol")
                    sStreet = .FieldToClass("sStreet")
                    nSendType = .FieldToClass("nSendType")
                    nTypRequest = .FieldToClass("nTypRequest")
                    nBordereaux = .FieldToClass("nBordereaux")
                    sClient = .FieldToClass("sClient")
                    sCertype = .FieldToClass("sCertype")
                    nBranch = .FieldToClass("nBranch")
                    nProduct = .FieldToClass("nProduct")
                    nPolicy = .FieldToClass("nPolicy")
                    nCertif = .FieldToClass("nCertif")
                    nClaim = .FieldToClass("nClaim")
                    nCase_num = .FieldToClass("nCase_num")
                    mblnFind = True
                    .RCloseRec()
                Else
                    mblnFind = False
                End If
            End With
        End If
        Find = mblnFind
        lrecreaLettRequest = Nothing

        Exit Function
        lrecreaLettRequest = Nothing
    End Function

    '**%Objective: Function of the entrance of new correspondences generated by the SCA008.
    '**%Parameters:
    '**%  nLettRequest      - Number of the request for remittance of  correspondence
    '**%  nLetterNum        - Number identifying the letter templates
    '**%  DinpDate          - Date when the request is recorded
    '**%  dExpDate          - Date when the letter must be removed from the system
    '**%  dPrintDate        - Date when the letter is printed.
    '**%  lintAction        - Type of action to execute
    '**%  nUser_Sol         - Código del solicitante.
    '**%  nUsercode         - Code of the user creating or updating the record.
    '**%  sStreet           - Address / Street - correspondence
    '**%  nSendType         - Sent type Sole Values: 1 - email 2 - Post service 3 - Facsimile
    '**%  dEffecDate        - Date which from the record is valid.
    '**%  nTypRequest       - Request type Sole Values: 1 - Individual 2 - Massive
    '**%  sClient           - Code of client
    '**%  sCertype          - Type or Record.Sole Values:1- Proposal 2 - Policy 3 - Quotation
    '**%  nBranch           - Code of the Line of Business.The possible values as per table 10.
    '**%  nProduct          - Code of The Product.
    '**%  nPolicy           - Number identifying the policy
    '**%  nCertif           - Number of The Certificate.
    '**%  nClaim            - Number identifying the claim
    '**%  nCase_num         - Code identifying the claim case or claimant
    '**%  nBordereaux       - Number of the collection schedule or form
    '**%  tletter           - Variable that contains the code of the letter.
    '**%  nCustomDescript   - Indicates if this peronalizada or not.
    '%Objetivo: Tiene como función del ingreso de nuevas correspondencias generados por la SCA008.
    '%Parámetros:
    '%  nLettRequest        - Numero de la solicitu.
    '%  nLetterNum          - Numero que identifica el tipo de carta.
    '%  DinpDate            - Fecha cuando el registro fue almacenado
    '%  dExpDate            - Fecha cuando se elimino el modelo de carta del sistema.
    '%  dPrintDate          - Fecha de impresión de la carta.
    '%  lintAction          - Tipo de acción a ejecutar
    '%  nUser_Sol           - Código del solicitante.
    '%  nUsercode           - Codigo del usuario que ejecuta la acción
    '%  sStreet             - Descripción de la dirección del cliente.
    '%  nSendType           - Tipo de envio, posibles valores: 1 - Email 2 - Correo 3 - Fax
    '%  dEffecDate          - Fecha de efecto del registro
    '%  nTypRequest         - Tipo de envío.Valores únicos 1 - Individual 2 - Masivo
    '%  sClient             - Codigo del cliente
    '%  sCertype            - Tipo o registro: Valores posibles: 1- Propuesta 2 - Poliza 3 - Cotización
    '%  nBranch             - Codigo del ramo. Valores posibles segun la tabla 10.             - Code of the Line of Business.The possible values as per table 10.
    '%  nProduct            - Codigo del producto.
    '%  nPolicy             - Numero que identifica la poliza
    '%  nCertif             - Numero del certificado
    '%  nClaim              - Numero que identifica el siniestro
    '%  nCase_num           - Código identificativo del caso o reclamante.
    '%  nBordereaux         - Numero de la colección, schedule o forma.
    '%  tletter             - Variable que contiene el código de la carta.
    '%  nCustomDescript     - Indica si la carta esta peronalizada o no
    Private Function Update(Optional ByVal nLettRequest As Short = 0, Optional ByVal nLetterNum As Short = 0, Optional ByVal DinpDate As Date = #12:00:00 AM#, Optional ByVal dExpDate As Date = #12:00:00 AM#, Optional ByVal dPrintDate As Date = #12:00:00 AM#, Optional ByVal sStatregt As String = "", Optional ByVal nUser_Sol As Short = 0, Optional ByVal nUsercode As Short = 0, Optional ByVal sStreet As String = "", Optional ByVal nSendType As Short = 0, Optional ByVal nTypRequest As Short = 0, Optional ByVal dEffecDate As Date = #12:00:00 AM#, Optional ByVal sClient As String = "", Optional ByVal sCertype As String = "", Optional ByVal nBranch As Short = 0, Optional ByVal nProduct As Short = 0, Optional ByVal nPolicy As Integer = 0, Optional ByVal nCertif As Integer = 0, Optional ByVal nClaim As Integer = 0, Optional ByVal nCase_num As Short = 0, Optional ByVal nBordereaux As Integer = 0, Optional ByVal tletter As String = "", Optional ByVal nCustomDescript As Short = 0) As Boolean
        Dim lreccreLettRequest As eRemoteDB.Execute
        Dim lreccreLettAccuse As eRemoteDB.Execute
        Dim lrecreaBeaber As eRemoteDB.Execute
        Dim lclsLetter As eLetter.Letter

        Dim sClientBeaber As String

        If Not IsIDEMode() Then
        End If

        lclsLetter = New eLetter.Letter

        lreccreLettRequest = New eRemoteDB.Execute
        With Me
            .nLettRequest = nLettRequest
            .nLetterNum = nLetterNum
            .DinpDate = DinpDate
            .dExpDate = dExpDate
            .dPrintDate = dPrintDate
            .sStatregt = sStatregt
            .nUser_Sol = nUser_Sol
            .nUsercode = nUsercode
            .sStreet = sStreet
            .nSendType = nSendType
            .nTypRequest = nTypRequest
            .dEffecDate = dEffecDate
            .sClient = sClient
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nClaim = nClaim
            .nCase_num = nCase_num
            .nBordereaux = nBordereaux

            If tletter <> String.Empty Then
                .tletter = tletter
            Else
                If lclsLetter.Find(nLetterNum, Me.nLanguage, dEffecDate) Then
                    .tletter = lclsLetter.tletter
                End If
            End If
        End With

        With lreccreLettRequest
            .StoredProcedure = "updLettRequest"
            .Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLetterNum", Me.nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInpDate", Me.DinpDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpDate", Me.dExpDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPrintDate", Me.dPrintDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUser_sol", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStreet", Me.sStreet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 230, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSendType", Me.nSendType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypRequest", Me.nTypRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Me.dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", Me.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", Me.sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", Me.nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", Me.nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", Me.nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", Me.nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", Me.nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("tCustomText", tletter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SCUSTOMDESCRIPT", IIf(CShort(nCustomDescript) = 0, System.DBNull.Value, CShort(nCustomDescript)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        'If tletter <> String.Empty Then
        lrecreaBeaber = New eRemoteDB.Execute
        With lrecreaBeaber
            .StoredProcedure = "INSREABEARER"
            .Parameters.Add("sCertype", Me.sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", Me.nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", Me.nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", Me.nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", Me.nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_Type", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", Me.nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Me.dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                sClientBeaber = .Parameters("sClient").Value
                .RCloseRec()
            Else
                sClientBeaber = ""
            End If
        End With

        If sClientBeaber <> "" Then
            lreccreLettAccuse = New eRemoteDB.Execute
            lreccreLettAccuse.SQL = "INSERT INTO LETTACCUSE (NLETTREQUEST, SCLIENT, DANSWERDATE, DTOHANDOVER,DCOMPDATE,NUSERCODE, TLETTER, NTYPELETTER, NSTATLETTER) " & _
                                                       "VALUES (" & Me.nLettRequest & ",'" & sClientBeaber & "',null,null,SYSDATE," & Me.nUsercode & ",:TLETTER," & "2, 1)"
            lreccreLettAccuse.Parameters.Add(":TLETTER", CleanLetter((Me.tletter)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            lreccreLettAccuse.Run(False)

            lreccreLettAccuse = Nothing
        End If

        lrecreaBeaber = Nothing
        'End If

        lreccreLettRequest = Nothing
        Exit Function
        lreccreLettRequest = Nothing
        lrecreaBeaber = Nothing
    End Function

    '**%Objective: Makes the previous load or boot of the values of frame LT003.
    '**%Parameters:
    '**%  nLettRequest  - Number of the request for remittance of  correspondence
    '%Objetivo: Realiza la carga previa o inicialización de los valores del frame LT003
    '%Parámetros:
    '%  nLettRequest    - Numero de la solicitud
    Public Sub insPreLT003(ByVal nLettRequest As Short)
        If Not IsIDEMode() Then
        End If

        Find(nLettRequest)
        'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If IsNothing(dPrintDate) Then dPrintDate = Today

        Exit Sub
    End Sub

    '**%Objective: Gives back the value by defect of special fields like "Options" or "Checks"
    '**%Parameters:
    '**%  sField    - Type of massive or individual correspondence
    '%Objetivo: Devuelve el valor por defecto de campos especiales como "Options" o "Checks"
    '%Parámetros:
    '%   sField     - Tipo de correspondencia masiva o individual
    Public Function DefaultValuesLT003(ByVal sField As String) As Short
        If Not IsIDEMode() Then
        End If

        Select Case sField
            Case "optIndividual"
                If nTypRequest = intNull Or nTypRequest = 1 Then
                    DefaultValuesLT003 = 1
                Else
                    DefaultValuesLT003 = 0
                End If
            Case "optMasive"
                If nTypRequest = 2 Then
                    DefaultValuesLT003 = 1
                Else
                    DefaultValuesLT003 = 0
                End If
            Case "chkEMail"
                DefaultValuesLT003 = IIf(nSendType = 1 Or nSendType = 3 Or nSendType = 5 Or nSendType = 7 Or nSendType = intNull, 1, 2)
            Case "chkMail"
                DefaultValuesLT003 = IIf(nSendType = 2 Or nSendType = 3 Or nSendType = 6 Or nSendType = 7, 1, 2)
            Case "chkFax"
                DefaultValuesLT003 = IIf(nSendType = 4 Or nSendType = 5 Or nSendType = 6 Or nSendType = 7, 1, 2)
        End Select

        Exit Function
    End Function

    '**%Objective: Validate the fields sent by pagina LTC001_K.aspx, associated to the headed of correspondence request
    '**%Parameters:
    '**% sCertype               - Type of registry
    '**% nLetterRequest         - Number of request
    '**% sClient                - Code of client
    '**% nApplicant             - Code of applicant
    '**% nBranch                - Code of branch
    '**% nProduct               - Code of product
    '**% nPolicy                - Number of policy
    '**% nCertif                - Number of certificate
    '**% nClaim                 - Number of claim
    '**% dEffecDat1             - Date of beginning
    '**% dEffecDat2             - Date until
    '**% sCodispl               - Logical Code of the page
    '%Objetivo: Valida los campos enviados por la pagina LTC001_K.aspx, asociados al encabezado de solicitud
    '%Parámetros:
    '%    sCertype              - Tipo de registro
    '%    nLetterRequest        - Numero de la solicitud
    '%    sClient               - Codigo del cliente
    '%    nApplicant            - Codigo del usuario
    '%    nBranch               - Codigo del ramo
    '%    nProduct              - Codigo del producto
    '%    nPolicy               - Numero de la polizá
    '%    nCertif               - Numero del certificado
    '%    nClaim                - Numero del siniestro
    '%    dEffecDat1            - Fecha de inicio
    '%    dEffecDat2            - Fecha fin
    '%    sCodispl              - Codigo logica de la pagina
    Public Function insValLTC001_K(ByVal sCertype As String, ByVal nLetterRequest As Integer, ByVal sClient As String, ByVal nApplicant As Short, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nClaim As Integer, ByVal dEffecDat1 As Date, ByVal dEffecDat2 As Date, ByVal sCodispl As String) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lstrErrors As String

        If Not IsIDEMode() Then
        End If

        lobjErrors = New eFunctions.Errors

        '**+ At least one of these fields must be filled in
        '+ Al menos un campo debe estar lleno
        If (nLetterRequest <= 0 And sClient = strNull And nApplicant <= 0 And nBranch <= 0 And nProduct <= 0 And nPolicy <= 0 And nCertif <= 0 And nClaim <= 0 And dEffecDat1 = dtmNull And dEffecDat2 = dtmNull) Then
            Call lobjErrors.ErrorMessage(sCodispl, 12164)
        Else
            '**+ Validations that are executed in the data base
            '+ Validaciones que se ejecutan en la base de datos
            lstrErrors = insvalLTC001DB(sCertype, nLetterRequest, sClient, nApplicant, nBranch, nProduct, nPolicy, nCertif, nClaim, dEffecDat1, dEffecDat2)

            Call lobjErrors.ErrorMessage(sCodispl, , , , lstrErrors)

            '**- If the field tdEffectDat1 and tdEffectDat2 this full.
            '- Si el campo tdEffectDat1 y tdEffectDat2 esta lleno.
            If dEffecDat1 <> dtmNull Then
                If Not (IsDate(dEffecDat1)) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 1900)
                Else
                    If dEffecDat2 = dtmNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3239)
                    Else
                        If Not (IsDate(dEffecDat2)) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 1900)
                        Else
                            If dEffecDat2 < dEffecDat1 Then
                                Call lobjErrors.ErrorMessage(sCodispl, 1132)
                            End If
                        End If
                    End If
                End If
            Else
                If dEffecDat2 <> dtmNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 10937)
                End If
            End If
        End If


        insValLTC001_K = lobjErrors.Confirm

        lobjErrors = Nothing

        Exit Function
    End Function

    '%insPostSI119Upd:
    Public Function insPostSI119Upd(ByVal nLetterNum As Short, ByVal nLanguage As Short, ByVal nClaim As Integer, ByVal dEffecDate As Date, ByVal nUsercode As Short, ByVal nActivity As Integer) As Boolean
        Dim lrecinscreateletter As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lrecinscreateletter = New eRemoteDB.Execute

        '**+ Definition of parameters for stored procedure 'inscreateletter'
        With lrecinscreateletter
            'PENDING: Procedure not found
            .StoredProcedure = "InsCreateLetter"
            .Parameters.Add("nLetternum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLanguage", nLanguage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nActivity", nActivity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostSI119Upd = .Run(False)
        End With

        Exit Function
        lrecinscreateletter = Nothing
    End Function
    '%insPostSI119: Used for PopUp window to update the future date of correspondence for SI119 screen
    Public Function insPostSI119(ByVal nLettRequest As Short, ByVal dPrintDate As Date) As Boolean
        Dim lrecinscreateletter As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lrecinscreateletter = New eRemoteDB.Execute

        '**+ Definition of parameters for stored procedure 'inscreateletter'
        With lrecinscreateletter
            'PENDING: Procedure not found
            .StoredProcedure = "INSUPDLETTREQUESTSI119"
            .Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPrintDate", dPrintDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostSI119 = .Run(False)
        End With

        Exit Function
        lrecinscreateletter = Nothing
    End Function

    '**%Objective: Verifies the existence of a record in table "Tab_Letter" using the key.
    '**%Parameters:
    '**%    nLetterNum - Number identifying the letter templates.
    '%Objetivo: Esta función verifica la existencia de un registro en la tabla "Tab_Letter" usando la clave de dicha tabla.
    '%Parámetros:
    '%    nLetterNum   - Numero que identifica el tipo de carta.
    Private Function IsExist(ByVal nLetterNum As Short) As Boolean
        Dim lclsTab_Letter As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lclsTab_Letter = New eRemoteDB.Execute

        With lclsTab_Letter
            .StoredProcedure = "reaTab_Letter_v"
            .Parameters.Add("nExist", intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsTab_Letter = Nothing

        Exit Function
        ObjectRelease = lclsTab_Letter
    End Function

    Private Function insvalLTC001DB(ByVal sCertype As String, ByVal nLetterRequest As Integer, ByVal sClient As String, ByVal nApplicant As Short, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nClaim As Integer, ByVal dEffecDat1 As Date, ByVal dEffecDat2 As Date) As String
        Dim lclsRemote As eRemoteDB.Execute
        Dim dEffecDate As Date

        If Not IsIDEMode() Then
        End If

        insvalLTC001DB = String.Empty

        lclsRemote = New eRemoteDB.Execute

        dEffecDate = Today

        With lclsRemote
            .StoredProcedure = "ValLTC001"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NLETTREQUEST", nLetterRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nApplicant", nApplicant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDat1", dEffecDat1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDat2", dEffecDat2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayErrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insvalLTC001DB = Trim(.Parameters("sArrayErrors").Value)
            End If
        End With
        lclsRemote = Nothing
        Exit Function
    End Function
    '%insNumerator: 

    Public Function FindNumerator(ByVal nTypeNum As Integer, _
                                  ByVal nOrd_Num As Integer) As Integer

        Dim lrecNumerator As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lrecNumerator = New eRemoteDB.Execute

        With lrecNumerator
            .StoredProcedure = "INSNUMERATORLETTLANGUAGE"
            .Parameters.Add("nTypeNum", nTypeNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrd_Num", nOrd_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                FindNumerator = .FieldToClass("nLastNumb")
            Else
                FindNumerator = 0
            End If
        End With

        lrecNumerator = Nothing

        Exit Function
    End Function
    '**%Objective: Validation of impresion table
    '**%Parameters:
    '**% sCodispl       - Logical code of page
    '**% nDays:         - Days
    '%Objetivo: Valida la eliminacion de la tabla de impresion
    '%Parámetros:
    '%  sCodispl:       - Codigo logico de la pagina
    '%  nDays:          - Dias
    '------------------------------------------------------------------------------------------
    Public Function insvalLTL501(ByVal sCodispl As String, _
                                 ByVal nDays As String) As String
        '------------------------------------------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors

        If Not IsIDEMode() Then
        End If

        lclsErrors = New eFunctions.Errors
        '**+ Date of process must be full
        '+ Fecha de proceso debe estar lleno
        If nDays = vbNullString Then
            Call lclsErrors.ErrorMessage(sCodispl, 500110)
        End If

        insvalLTL501 = lclsErrors.Confirm
        lclsErrors = Nothing

        Exit Function
        lclsErrors = Nothing
    End Function

    '**%Objective:
    '**%Parameters:
    '**%   nDays     -
    '%Objective:
    '%Parámetros:
    '%     nDays     -
    '-----------------------------------------------------------------------------------------
    Public Function insPostLTL501(ByVal nDays As Integer) As Boolean
        '-----------------------------------------------------------------------------------------
        Dim lnLanguageUsers As eRemoteDB.Execute
        insPostLTL501 = False

        If Not IsIDEMode() Then
        End If

        lnLanguageUsers = New eRemoteDB.Execute

        With lnLanguageUsers
            .StoredProcedure = "DelPrintDocuments"
            .Parameters.Add("nDays", nDays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insPostLTL501 = True
            End If
        End With

        Exit Function
    End Function

    '% CleanLettRequest
    '------------------------------------------------------------
    Public Sub CleanLettRequest(ByRef tletter As String)
        '------------------------------------------------------------
        If Not IsNothing(tletter) Then
            Dim nThemedata As Integer = tletter.LastIndexOf("\par }{\*\themedata")
            Dim tletterBuilder As New StringBuilder(tletter)

            If nThemedata > 0 Then
                tletterBuilder.Insert(nThemedata + 5, "}")
                tletter = tletterBuilder.ToString(0, nThemedata + 7)
            End If
        End If
    End Sub
End Class











