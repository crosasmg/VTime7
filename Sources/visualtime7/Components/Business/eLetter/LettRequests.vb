Option Strict Off
Option Explicit On
Public Class LettRequests
	Implements System.Collections.IEnumerable
	'**+Objetive: Clase generada a partir de la tabla 'LETTREQUEST' que es Solicitud de envío de correspondencia.Un registro por cada solicitud de envío
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'LETTREQUEST' Letter requests.A record per every letter request
	'+Version: $$Revision: 9 $
	
	'**-Objective: local variable to hold collection
	'-Objetivo: Variable local para almacenar una colección
	Private mCol As Collection
	
	'**-Objective: local variable to hold collection
	'-Objetivo: Variable local para almacenar una colección
	Private mvarLettRequestWin As LettRequestWin
	
	'**%Objective: this property allows you to enumerate this collection with the For...Each syntax
	'%Objetivo: Esta propiedad le permite enumerar la colección utilizando la sintaxis For...Each.
	
	'**%Objective: this property allows you to enumerate this collection with the For...Each syntax
	'%Objetivo: Esta propiedad le permite enumerar la colección utilizando la sintaxis For...Each.
	Public Property LettRequestWin() As LettRequestWin
		Get
			If Not IsIDEMode Then
			End If
			
			If mvarLettRequestWin Is Nothing Then
				mvarLettRequestWin = New LettRequestWin
			End If
			
			LettRequestWin = mvarLettRequestWin
			
			Exit Property
		End Get
		Set(ByVal Value As LettRequestWin)
			If Not IsIDEMode Then
			End If
			
			mvarLettRequestWin = Value
			
			Exit Property
		End Set
	End Property
	
	'**%Objective: used when referencing an element in the collection vntIndexKey contains either the Index or Key to the collection this is why it is declared as a Variant
	'**%Parameters:
	'**%  vntIndexKey   - Variable key or index
	'%Objetivo: Se utiliza para hacer referencia a un elemento de la colección.
	'%Parameters:
	'%  vntIndexKey - Variable clave o índice
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As LettRequest
		Get
			
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Restores the number of elements that the collection
	'%Objetivo: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: this property allows you to enumerate this collection with the For...Each syntax
	'%Objetivo: Esta propiedad le permite enumerar la colección con la sintaxis For...Each.
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
			'ProcError("LettRequests.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
    '**%Objective: Stores the turn out of the search in the object to objNewMember of the collection.
    '**%Parameters:
    '**%  nBordereaux  - Number of the collection schedule or form
    '**%  nCase_num    - Code identifying the claim case or claimant
    '**%  nClaim       - Number identifying the claim
    '**%  nCertif      - Number of The Certificate.
    '**%  nPolicy      - Number identifying the policy/ quotation/ proposal
    '**%  nProduct     - Code of The Product.
    '**%  nBranch      - Code of the Line of Business.The possible values as per table 10.
    '**%  sCertype     - Type or Record.Sole Values:1- Proposal 2 - Policy 3 - Quotation
    '**%  sClient      - Code of client.
    '**%  dEffecDate   - Date which from the record is valid.
    '**%  nTypRequest  - Request type Sole Values: 1 - Individual 2 - Massive
    '**%  nSendType    - Sent type Sole Values: 1 - email 2 - Post service 3 - Facsimile
    '**%  sStreet      - Address / Street - correspondence
    '**%  nUsercode    - Code of the user creating or updating the record.
    '**%  nUser_sol    - Code of The User Requester.
    '**%  sStatregt    - General status of the record.Sole values as per table 26.
    '**%  dPrintDate   - Date when the letter is printed.
    '**%  dExpDate     - Date when the letter must be removed from the system
    '**%  DinpDate     - Date when the request is recorded
    '**%  nLetterNum   - Number identifying the letter templates
    '**%  nLettRequest - Number of the request for remittance of  correspondence
    '**%  sKey         - Variable key or index.
    '%Objetivo: Almacena el resultado de la busqueda en el objeto objNewMember de la colección
    '%Parámetros:
    '%  nBordereaux    - Número de la relación de cobro
    '%  nCase_num      - Código identificativo del caso o reclamante
    '%  nClaim         - Número que identifica al siniestro
    '%  nCertif        - Número identificativo del certificado.
    '%  nPolicy        - Número identificativo de la póliza/ cotización/ solicitud
    '%  nProduct       - Código del producto.
    '%  nBranch        - Código del ramo comercial.Valores posibles según tabla 10.
    '%  sCertype       - Tipo de registro.Valores únicos: 1 - Solicitud 2 - Póliza 3 - Cotización
    '%  sClient        - Codigo del cliente.
    '%  dEffecDate     - Fecha de efecto del registro.
    '%  nTypRequest    - Tipo de envío.Valores únicos 1 - Individual 2 - Masivo
    '%  nSendType      - Tipo de envío.Valores únicos 1 - email 2 - Correo 3 - Fax
    '%  sStreet        - Dirección - Calle -  Envío de la correspondencia.
    '%  nUsercode      - Código del usuario que crea o actualiza el registro.
    '%  nUser_sol      - Código del usuario que solicita el envío.
    '%  sStatregt      - Estado general del registro.Valores únicos según tabla 26.
    '%  dPrintDate     - Fecha de impresion de la solicitud
    '%  dExpDate       - Fecha en que se debe eliminar la correspondencia del sistema
    '%  DinpDate       - Fecha en que se registra la solicitud
    '%  nLetterNum     - Código del modelo de carta.
    '%  nLettRequest   - Número de solicitud de envío
    '%  sKey           - Variable clave o índice.
    Private Function Add(ByVal nBordereaux As Integer, ByVal nCase_num As Short, ByVal nClaim As Integer, ByVal nCertif As Integer, ByVal nPolicy As Integer, ByVal nProduct As Short, ByVal nBranch As Short, ByVal sCertype As String, ByVal sClient As String, ByVal nTypRequest As Short, ByVal nSendType As Short, ByVal sStreet As String, ByVal nUsercode As Short, ByVal nUser_Sol As Short, ByVal sStatregt As String, ByVal dPrintDate As Date, ByVal dExpDate As Date, ByVal DinpDate As Date, ByVal nLetterNum As Short, ByVal nLettRequest As Short, ByVal nLanguage As Short, Optional ByVal sKey As String = "", Optional ByRef sDescriptLanguage As String = "", Optional ByVal nTypeLetter As Short = 0, Optional ByVal sRequired As String = "", Optional ByVal nEndorseType As Short = 0, Optional ByVal sDescript As String = "") As LettRequest
        Dim objNewMember As LettRequest

        objNewMember = New LettRequest

        With objNewMember
            .nBordereaux = nBordereaux
            .nCase_num = nCase_num
            .nClaim = nClaim
            .nCertif = nCertif
            .nPolicy = nPolicy
            .nProduct = nProduct
            .nBranch = nBranch
            .sCertype = sCertype
            .sClient = sClient
            .nTypRequest = nTypRequest
            .nSendType = nSendType
            .sStreet = sStreet
            .nUsercode = nUsercode
            .nUser_Sol = nUser_Sol
            .sStatregt = sStatregt
            .dPrintDate = dPrintDate
            .dExpDate = dExpDate
            .DinpDate = DinpDate
            .nLetterNum = nLetterNum
            .nLettRequest = nLettRequest
            .nLanguage = nLanguage
            .sDescriptLanguage = sDescriptLanguage
            .nTypeLetter = nTypeLetter
            .sRequired = sRequired
            .nEndorseType = nEndorseType
            .sDescripts = sDescript
            If Len(sKey) = 0 Then
                mCol.Add(objNewMember)
            Else
                mCol.Add(objNewMember, sKey)
            End If

            Add = objNewMember
            objNewMember = Nothing
        End With
        Exit Function
        Add = objNewMember
        objNewMember = Nothing
    End Function
	
	'**%Objective: Makes a general consultation of the LettRequest table.
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
	'%Objetivo: Realiza una consulta general de la tabla LettRequest
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
    Public Function Find(ByVal sCodispl As String, ByVal nAction As Double, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Double = -32768, Optional ByVal nProduct As Double = -32768, Optional ByVal nPolicy As Double = -32768, Optional ByVal nCertif As Double = -32768, Optional ByVal nClaim As Double = -32768, Optional ByVal nCase_num As Double = -32768, Optional ByVal nBordereaux As Double = -32768, Optional ByVal sClient As String = "", Optional ByVal dEffecDate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Double = 0, Optional ByVal nDeman_type As Double = -32768) As Boolean
        Dim lreccreLettRequest As eRemoteDB.Execute
        Dim nLangUser As Short
        Dim nParameters As Integer

        'If Not IsIDEMode Then
        'End If

        lreccreLettRequest = New eRemoteDB.Execute

        Select Case sCodispl
            '**+ Clients
            '+ Clientes
            Case "SCA801"
                nParameters = 0

                '**+ Policies
                '+ Pólizas
            Case "SCA802", "CA034", "CA033", "SCA805"
                nParameters = 3

                '**+ Claims
                '+ Siniestros
            Case "SCA803"
                nParameters = 5

        End Select
        With lreccreLettRequest
            .StoredProcedure = "reaClientCertificat"
            .Parameters.Add("nParameters", nParameters, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nLangUser = .FieldToClass("nLanguage")
                .RCloseRec()
            Else
                nLangUser = 1
                .RCloseRec()
            End If
        End With

        With lreccreLettRequest
            .StoredProcedure = "reaLettRequest"
            .Parameters.Add("nLettRequest", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLanguage", nLangUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                If sCodispl <> "SCA802" Then
                    Do While Not .EOF

                        'Add(.FieldToClass("nBordereaux"), .FieldToClass("nCase_Num"), .FieldToClass("nClaim"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("sClient"), .FieldToClass("nTypRequest"), .FieldToClass("nSendType"), .FieldToClass("sStreet"), eRemoteDB.Constants.intNull, .FieldToClass("nUser_Sol"), .FieldToClass("sStatRegt"), .FieldToClass("dPrintDate"), .FieldToClass("dExpDate"), .FieldToClass("dInpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nLettRequest"), .FieldToClass("nLanguage"), , .FieldToClass("sDescriptLanguage"), IIf(.FieldToClass("nTypeLetter") = "", eRemoteDB.Constants.intNull, .FieldToClass("nTypeLetter")), .FieldToClass("sRequired"), IIf(.FieldToClass("nEndorseType") = "", eRemoteDB.Constants.intNull, .FieldToClass("nEndorseType")), .FieldToClass("sDescript"))
                        Add(.FieldToClass("nBordereaux"), .FieldToClass("nCase_Num"), .FieldToClass("nClaim"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("sClient"), .FieldToClass("nTypRequest"), .FieldToClass("nSendType"), .FieldToClass("sStreet"), intNull, .FieldToClass("nUser_Sol"), .FieldToClass("sStatRegt"), .FieldToClass("dPrintDate"), .FieldToClass("dExpDate"), .FieldToClass("dInpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nLettRequest"), .FieldToClass("nLanguage"), , .FieldToClass("sDescriptLanguage"), .FieldToClass("nTypeLetter"), .FieldToClass("sRequired"), .FieldToClass("nEndorseType"), .FieldToClass("sDescript"))

                        .RNext()
                    Loop
                    .RCloseRec()
                Else
                    Do While Not .EOF
                        'Add(.FieldToClass("nBordereaux"), .FieldToClass("nCase_Num"), .FieldToClass("nClaim"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("sClient"), .FieldToClass("nTypRequest"), .FieldToClass("nSendType"), .FieldToClass("sStreet"), intNull, .FieldToClass("nUser_Sol"), .FieldToClass("sStatRegt"), .FieldToClass("dPrintDate"), .FieldToClass("dExpDate"), .FieldToClass("dInpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nLettRequest"), IIf(.FieldToClass("nLanguage") <= 0, eRemoteDB.Constants.dblNull, .FieldToClass("nLanguage")), , .FieldToClass("sDescriptLanguage"), IIf(.FieldToClass("nTypeLetter") <= 0, eRemoteDB.Constants.intNull, .FieldToClass("nTypeLetter")), .FieldToClass("sRequired"), IIf(.FieldToClass("nEndorseType") <= 0, eRemoteDB.Constants.intNull, .FieldToClass("nEndorseType")), .FieldToClass("sDescript"))
                        Add(.FieldToClass("nBordereaux"), .FieldToClass("nCase_Num"), .FieldToClass("nClaim"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("sClient"), .FieldToClass("nTypRequest"), .FieldToClass("nSendType"), .FieldToClass("sStreet"), intNull, .FieldToClass("nUser_Sol"), .FieldToClass("sStatRegt"), .FieldToClass("dPrintDate"), .FieldToClass("dExpDate"), .FieldToClass("dInpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nLettRequest"), .FieldToClass("nLanguage"), , .FieldToClass("sDescriptLanguage"), .FieldToClass("nTypeLetter"), .FieldToClass("sRequired"), .FieldToClass("nEndorseType"), .FieldToClass("sDescript"))

                        .RNext()
                    Loop
                    .RCloseRec()

                End If
            Else
                .StoredProcedure = "reaLettRequesnofilter"
                .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Find = True
                    Do While Not .EOF
                        If nLangUser = .FieldToClass("nLanguage") Then
                            'JJ: Los campos que retornan null generan un error al enviarlos a la colección
                            'Add(.FieldToClass("nBordereaux"), .FieldToClass("nCase_Num"), .FieldToClass("nClaim"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("sClient"), eRemoteDB.Constants.intNull, .FieldToClass("nSendType"), .FieldToClass("sStreet"), intNull, .FieldToClass("nUser_Sol"), .FieldToClass("sStatRegt"), .FieldToClass("dPrintDate"), .FieldToClass("dExpDate"), .FieldToClass("dInpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nLettRequest"), IIf(.FieldToClass("nLanguage") = eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nLanguage")), , , , , .FieldToClass("nEndorseType"), .FieldToClass("sDescript"))
                            'Add(.FieldToClass("nBordereaux"), .FieldToClass("nCase_Num"), .FieldToClass("nClaim"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("sClient"), .FieldToClass("nTypRequest"), .FieldToClass("nSendType"), .FieldToClass("sStreet"), nUsercode, .FieldToClass("nUser_Sol"), .FieldToClass("sStatRegt"), .FieldToClass("dPrintDate"), .FieldToClass("dExpDate"), .FieldToClass("dInpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nLettRequest"), .FieldToClass("nLanguage"), , , , .FieldToClass("sRequired"), .FieldToClass("nEndorseType"), .FieldToClass("sDescript"))
                            Add(.FieldToClass("nBordereaux"), .FieldToClass("nCase_Num"), .FieldToClass("nClaim"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("sClient"), .FieldToClass("nTypRequest"), .FieldToClass("nSendType"), .FieldToClass("sStreet"), nUsercode, .FieldToClass("nUser_Sol"), .FieldToClass("sStatRegt"), .FieldToClass("dPrintDate"), .FieldToClass("dExpDate"), .FieldToClass("dInpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nLettRequest"), .FieldToClass("nLanguage"), , , , , .FieldToClass("nEndorseType"), .FieldToClass("sDescript"))

                            'Add(0, 0, 0, .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), 10, "2", "", 1, 1, "", 1971, 1971, "1", Date.Now, Date.Now, Date.Now, 1, 1, 1)

                        End If
                        .RNext()
                    Loop
                    .RCloseRec()
                End If
            End If
        End With

        lreccreLettRequest = Nothing

        Exit Function
        lreccreLettRequest = Nothing
    End Function
	
	'**%Objective: Eliminates correspondence requests according to the list.
	'**%Parameters:
	'**%  sList - Ready of requests.
	'%Objetivo: Elimina solicitudes de correspondencia según la lista.
	'%Parámetros:
	'%   sList  - Lista de solicitudes.
	Public Function DeletebyList(ByVal sList As String) As Boolean
		Dim lrecdelLettRequestbyList As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecdelLettRequestbyList = New eRemoteDB.Execute
		With lrecdelLettRequestbyList
			.StoredProcedure = "delLettRequestbyList"
			.Parameters.Add("sList", sList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				DeletebyList = True
			End If
		End With
		lrecdelLettRequestbyList = Nothing
		
		Exit Function
		lrecdelLettRequestbyList = Nothing
	End Function
	
	'**%Objective: Generates the correspondences required by the user.
	'**%Parameters:
	'**%  sCodispl     - Code of page.
	'**%  nAction      - Number of action to execute
	'**%  sCertype     - Type or Record.Sole Values:1- Proposal 2 - Policy 3 - Quotation
	'**%  nBranch      - Code of the Line of Business.The possible values as per table 10.
	'**%  nProduct     - Code of The Product.
	'**%  nPolicy      - Number identifying the policy/ quotation/ proposal
	'**%  nCertif      - Number of The Certificate.
	'**%  nClaim       - Number identifying the claim
	'**%  nCase_num    - Code identifying the claim case or claimant
	'**%  nDeman_type  - Claim type Possible values as per table 692
	'**%  nBordereaux  - Number of the collection schedule or form
	'**%  sClient      - Code of client
	'**%  dEffecDate   - Date which from the record is valid.
	'**%  nReceipt     - Receipt number
	'**%  nDigit       - Receipt Control Digit.
	'**%  nPayNumbe    - Number of the payment associated with the receipt payment agreement
	'**%  nUsercode    - Code of the user creating or updating the record.
	'%Objetivo: Genera las correspondencias solicitadas por el usuario.
	'%Parámetros:
	'%   sCodispl      - Codigo de la pagina.
	'%   nAction       - Numero de acción a ejecutarse.
	'%   sCertype      - Tipo de registro.Valores únicos: 1 - Solicitud 2 - Póliza 3 - Cotización
	'%   nBranch       - Código del ramo comercial.Valores posibles según tabla 10.
	'%   nProduct      - Código del producto.
	'%   nPolicy       - Codigo del cliente
	'%   nCertif       - Número identificativo del certificado.
	'%   nClaim        - Número que identifica al siniestro
	'%   nCase_num     - Código identificativo del caso o reclamante
	'%   nDeman_type   - Tipo de siniestros, según table692.
	'%   nBordereaux   - Número de la relación de cobro
	'%   sClient       - Codigo del cliente.
	'%   dEffecDate    - Fecha de efecto del registro.
	'%   nReceipt      - Numero de la recibo.
	'%   nDigit        - Digitos de control del recibo de pago.
	'%   nPayNumbe     - Número del pago que se asoció al acuerdo del pago del recibo.
	'%   nUsercode     - Código del usuario que crea o actualiza el registro.
    Public Function MergeDocuments(ByVal sCodispl As String, ByVal nAction As Short, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Short = -32768, Optional ByVal nProduct As Short = -32768, Optional ByVal nPolicy As Integer = -32768, Optional ByVal nCertif As Integer = -32768, Optional ByVal nClaim As Integer = -32768, Optional ByVal nCase_num As Short = -32768, Optional ByVal nDeman_type As Short = -32768, Optional ByVal nBordereaux As Integer = -32768, Optional ByVal sClient As String = "", Optional ByVal dEffecDate As Date = #12:00:00 AM#, Optional ByVal nReceipt As Integer = -32768, Optional ByVal nDigit As Short = -32768, Optional ByVal nPayNumbe As Short = -32768, Optional ByVal nUsercode As Short = 0) As Boolean
        Dim lreccreLettRequest As eRemoteDB.Execute
        Dim lobjLettRequest As eLetter.LettRequest
        Dim lcolParameters As Collection

        If Not IsIDEMode() Then
        End If

        MergeDocuments = False

        lreccreLettRequest = New eRemoteDB.Execute

        With lreccreLettRequest
            .StoredProcedure = "reaLettRequest"
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nLettRequest", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                MergeDocuments = True
                Do While Not .EOF
                    Add(.FieldToClass("nBordereaux"), .FieldToClass("nCase_Num"), .FieldToClass("nClaim"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("sClient"), .FieldToClass("nTypRequest"), .FieldToClass("nSendType"), .FieldToClass("sStreet"), intNull, .FieldToClass("nUser_Sol"), .FieldToClass("sStatRegt"), .FieldToClass("dPrintDate"), .FieldToClass("dExpDate"), .FieldToClass("dInpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nLettRequest"), .FieldToClass("nLanguage"))
                    .RNext()
                Loop
                .RCloseRec()

                For Each lobjLettRequest In mCol
                    If lobjLettRequest.nLettRequest <> intNull Then
                        lcolParameters = New Collection
                        With lobjLettRequest.oLetter

                            Select Case .nParameter
                                '**+ Beneficiary
                                '+ Beneficiario
                                Case 0
                                    '**+ Intermediary
                                    '+ Intermediario
                                Case 1
                                    '**+ Client
                                    '+ Cliente
                                Case 2
                                    lcolParameters.Add(sClient)
                                    '**+ Policy/Certificate
                                    '+ Póliza/Certificado
                                Case 3
                                    lcolParameters.Add(sCertype)
                                    lcolParameters.Add(nBranch)
                                    lcolParameters.Add(nProduct)
                                    lcolParameters.Add(nPolicy)
                                    lcolParameters.Add(nCertif)
                                    '**+ Receive
                                    '+ Recibo
                                Case 4
                                    lcolParameters.Add(sCertype)
                                    lcolParameters.Add(nBranch)
                                    lcolParameters.Add(nProduct)
                                    lcolParameters.Add(nReceipt)
                                    lcolParameters.Add(nDigit)
                                    lcolParameters.Add(nPayNumbe)
                                    '**+ Claims
                                    '+ Siniestros
                                Case 5
                                    lcolParameters.Add(nClaim)
                                    lcolParameters.Add(nCase_num)
                                    lcolParameters.Add(nDeman_type)
                                    '**+ Professional
                                    '+ Profesional
                                Case 6
                            End Select

                            .MergeDocument(lcolParameters, Nothing, dEffecDate, nUsercode, False, lobjLettRequest.nLetterNum, , lobjLettRequest.nLettRequest)
                        End With
                        lcolParameters = Nothing
                    End If
                Next lobjLettRequest
            End If
        End With

        lreccreLettRequest = Nothing

        Exit Function
        lreccreLettRequest = Nothing
    End Function
	
	'**%Objective: used when removing an element from the collection vntIndexKey contains either the Index or Key, which is why it is declared as a Variant
	'**%Parameters:
	'**%  vntIndexKey   - Variable key or index
	'%Objetivo:  Se utiliza para eliminar un elemento de la collección.
	'%Parámetros:
	'%  vntIndexKey - Variables clave o índice
	Private Sub Remove(ByVal vntIndexKey As Object)
		
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created
	'%Objetivo: Crea la collección cuando la clase es creada
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
	'%Objetivo: Elimina la colección cuando la clase finaliza.
	Private Sub Class_Terminate_Renamed()
		If Not IsIDEMode Then
		End If
		
		mvarLettRequestWin = Nothing
		mCol = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%Objective: This function has as purpose fullfil called the store procedure "reaLetterRequest" and find the records guarded in the BD specifically in the tables LettRequets and LettAccuse.
	'**%Parameters:
	'**%  nCondition    - Condition of search, possible values 1 - all or 2 - Of printing
	'**%  nLettRequest  - Number of the request for remittance of  correspondence
	'**%  sClient       - Code of client
	'**%  nBranch       - Code of the Line of Business.The possible values as per table 10.
	'**%  nProduct      - Code of The Product.
	'**%  nPolicy       - Number identifying the policy/ quotation/ proposa
	'**%  nCertif       - Number of The Certificate.
	'**%  nClaim        - Number identifying the claim
	'**%  tdEffectDat1  - Date begin
	'**%  tdEffectDat2  - Date end.
	'%Objetivo: Esta función tiene como fin realizar el llamado a el store procedure "reaLetterRequest" y encontrar los registros guardados en la BD especificamente en las tablas LettRequets y LettAccuse.
	'%Parámetros:
	'%    nCondition    - Condición de busqueda, posibles valores 1 - todos o 2 - Por Imprimir
	'%    nLettRequest  - Número de solicitud de envío
	'%    sClient       - Codigo del cliente
	'%    nBranch       - Código del ramo comercial.Valores posibles según tabla 10.
	'%    nProduct      - Código del producto.
	'%    nPolicy       - Número identificativo de la póliza
	'%    nCertif       - Número identificativo del certificado.
	'%    nClaim        - Número que identifica al siniestro.
	'%    tdEffectDat1  - Fecha inicio periodo.
	'%    tdEffectDat2  - Fecha fin periodo.
	
	Public Function FindLetter(ByVal nCondition As Short, Optional ByVal nLettRequest As Short = 0, Optional ByVal sClient As String = "", Optional ByVal nBranch As Short = 0, Optional ByVal nProduct As Short = 0, Optional ByVal nPolicy As Short = 0, Optional ByVal nCertif As Integer = 0, Optional ByVal nClaim As Integer = 0, Optional ByVal tdEffectDat1 As Date = #12:00:00 AM#, Optional ByVal tdEffectDat2 As Date = #12:00:00 AM#, Optional ByVal lsAplicant As Short = 0) As Boolean
		Dim lrecLettRequest As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecLettRequest = New eRemoteDB.Execute
		
		With lrecLettRequest
			.StoredProcedure = "reaLetterRequest"
            .Parameters.Add("nCondition", nCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tdEffectDat1", tdEffectDat1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tdEffectDat2", tdEffectDat2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("lsAplicant", lsAplicant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindLetter = True
				Do While Not .EOF
					'**+ Does called to the function AddLetter to capture the values found in the search
					'+ Se hace el llamado al la función AddLetter para capturar los valores encontrados en la busqueda
					Call AddLetter(.FieldToClass("sClient"), .FieldToClass("sClieName"), .FieldToClass("nLetterRequest"), .FieldToClass("sDescriptt"), .FieldToClass("sDescripts"), .FieldToClass("sClientSol"), .FieldToClass("sClieNameSol"), .FieldToClass("DinpDate"), .FieldToClass("nLetterNum"), .FieldToClass("nTypeLetter"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		lrecLettRequest = Nothing
		
		Exit Function
		lrecLettRequest = Nothing
	End Function
	
	'**%Objective: This function has as purpose store the records found by the execution of the store procedure, this records are stored in the object objNewMember.
	'**%Parameters:
	'**% sClient         - Code of client
	'**% sClieName       - Description of client
	'**% nLetterRequest  - Number of request of shipment
	'**% sDescriptt      - Type of letter model
	'**% sDescripts      - Description of the state of letter
	'**% sClientSol      - Code of applicant
	'**% sClieNameSol    - Description of the applicant.
	'**% DinpDate        - Date when the request is recorded
	'**% nTypeLetter     - Indicates the type of model of letter: Template or Customized
	'%Objetivo: Esta función tiene como fin almacenar los registros encontrados por la ejecución del store procedure, dichos registros se almacenan en el objeto objNewMember.
	'%Parámetros:
	'%  sClient          - Codigo del cliente.
	'%  sClieName        - Descripción del cliente.
	'%  nLetterRequest   - Número de solicitud de envío.
	'%  sDescriptt       - Tipo de modelo de carta.
	'%  sDescripts       - Descripción del estado de la carta.
	'%  sClientSol       - Codigo del solicitante.
	'%  sClieNameSol     - Descripción del codigo del solicitante.
	'%  DinpDate         - Fecha en que se registra la solicitud.
	'%  nTypeLetter      - Indica el tipo de modelo de carta: Template o Personalizada
    Private Function AddLetter(ByVal sClient As String, ByVal sClieName As String, ByVal nLetterRequest As Integer, ByVal sDescriptt As String, ByVal sDescripts As String, ByVal sClientSol As String, ByVal sClieNameSol As String, ByVal DinpDate As Date, ByVal nLetterNum As Short, ByVal nTypeLetter As Short) As LettRequest
        Dim objNewMember As LettRequest

        If Not IsIDEMode() Then
        End If

        objNewMember = New LettRequest

        '**+ Set the properties passed into the method
        '+ Se almacenan los valores de las variables de entrada en los objetos creados anteriormente
        objNewMember.sClient = sClient
        objNewMember.sClieName = sClieName
        objNewMember.nLettRequest = nLetterRequest
        objNewMember.sDescriptt = sDescriptt
        objNewMember.sDescripts = sDescripts
        objNewMember.sClientSol = sClientSol
        objNewMember.sClieNameSol = sClieNameSol
        objNewMember.DinpDate = DinpDate
        objNewMember.nLetterNum = nLetterNum
        objNewMember.nTypeLetter = nTypeLetter

        mCol.Add(objNewMember)

        '**+ Return the object created
        '+ Retorna el objeto creado con los objetos almacenados
        AddLetter = objNewMember
        objNewMember = Nothing

        Exit Function
        AddLetter = objNewMember
        objNewMember = Nothing
    End Function
	
	'**% Objective: This function has as purpose fullfil called the store procedure
	'**%            "reaLettRequestAccuse" and find the records guarded in the BD
	'**%            specifically in the tables LettRequets and LettAccuse.
	'**% Parameters:
	'**%    nLettRequest  - Number of the request for remittance of  correspondence
	'
	'% Objetivo: Esta función tiene como fin realizar el llamado a el store procedure
	'%           "reaLettRequestAccuse" y encontrar los registros  guardados en la BD
	'%           especificamente en las tablas LettRequets y LettAccuse.
	'% Parámetros:
	'%    nLettRequest  - Número de solicitud de envío
	Public Function FindLetterRequestAccuse(ByVal nLettRequest As Short, ByVal dEffecDate As Date) As Boolean
		Dim lrecLettRequest As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecLettRequest = New eRemoteDB.Execute
		
		With lrecLettRequest
			.StoredProcedure = "reaLettRequestAccuse"
			.Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffectDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                FindLetterRequestAccuse = True
                Do While Not .EOF
                    '**+ Does called to the function AddLetterRequestAccuse to capture the values found
                    '**+ in the search.
                    '+ Se hace el llamado al la función AddLetterRequestAccuse para capturar los valores
                    '+ encontrados en la busqueda.
                    Call AddLetterRequestAccuse(.FieldToClass("nLettRequest"), .FieldToClass("nLetterNum"), .FieldToClass("sDescript"), .FieldToClass("nUser_Sol"), .FieldToClass("nSendType"), .FieldToClass("sClient"), .FieldToClass("sClieName"), .FieldToClass("nMailIngPref"), .FieldToClass("nTypeOfAddress"), .FieldToClass("sStreet"))
                    .RNext()
                Loop
                .RCloseRec()
            End If
		End With
		lrecLettRequest = Nothing
		
		Exit Function
		lrecLettRequest = Nothing
	End Function
	
	'**%Objective: This function has as purpose store the records found by the execution
	'**%           of the store procedure, this records are stored in the object objNewMember.
	'**%Parameters:
	'**% nLettRequest   - Number of request
	'**% nLetterNum     - Number of letter model
	'**% sDescript      - Description of letter model
	'**% nUser_Sol      - Number of applicant
	'**% nSendType      - Number type of send
	'**% sClient        - Code of client
	'**% sClieName      - Description of client
	'**% nMailIngPref   - Number that identifies the mailing preference of the client
	'**% nTypeOfAddress - Type of address
	'**% sStreet        - Description of The Address / Name of The Street
	'**% tLetter        - Content of the letter
	'
	'%Objetivo: Esta función tiene como fin almacenar los registros encontrados por la
	'%          ejecución del store procedure, dichos registros se almacenan en el objeto objNewMember.
	'%Parámetros:
	'% nLettRequest   - Numero el requerimiento
	'% nLetterNum     - Numero del tipo de carta
	'% sDescript      - Nombre del modelo de carta
	'% nUser_Sol      - Numero del solicitante
	'% nSendType      - Numero del tipo de envio solicitado
	'% sClient        - Codigo del cliente
	'% sClieName      - Descripción del cliente
	'% nMailIngPref   - Numero que identifica la preferencia del tipo de envio de correspondecia al cliente
	'% nTypeOfAddress - Tipo de dirección
	'% sStreet        - Descripción de la calle o nombre de la calle
	'% tLetter        - Contenido de la carta
	Private Function AddLetterRequestAccuse(ByVal nLettRequest As Short, ByVal nLetterNum As Short, ByVal sDescript As String, ByVal nUser_Sol As Short, ByVal nSendType As Short, ByVal sClient As String, ByVal sClieName As String, ByVal nMailIngPref As Short, ByVal nTypeOfAddress As Short, ByVal sStreet As String) As LettRequest
		Dim objNewMember As LettRequest
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New LettRequest
		
		'**+ Set the properties passed into the method
		'+ Se almacenan los valores de las variables de entrada en los objetos creados anteriormente
		objNewMember.nLettRequest = nLettRequest
		objNewMember.nLetterNum = nLetterNum
		objNewMember.sDescriptt = sDescript
		objNewMember.nUser_Sol = nUser_Sol
		objNewMember.nSendType = nSendType
		objNewMember.sClient = sClient
		objNewMember.sClieName = sClieName
		objNewMember.nMailIngPref = nMailIngPref
		objNewMember.nTypeOfAddress = nTypeOfAddress
		objNewMember.sStreet = sStreet
		
		mCol.Add(objNewMember)
		
		'**+ Return the object created
		'+ Retorna el objeto creado con los objetos almacenados
		AddLetterRequestAccuse = objNewMember
		objNewMember = Nothing
		
		Exit Function
		AddLetterRequestAccuse = objNewMember
		objNewMember = Nothing
	End Function
End Class











