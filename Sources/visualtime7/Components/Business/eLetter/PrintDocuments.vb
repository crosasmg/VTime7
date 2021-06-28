Option Strict Off
Option Explicit On
Public Class PrintDocuments

    '**-Objective: Code of the client
    '-Objetivo: Código del cliente
    Public sClient As String
    '**-Objective:
    '-Objetivo: Número consecutivo del registro asociado al cliente
    Public nConsecutive As Integer
    '**-Objective: Code of the window (logical code).
    '-Objetivo: Código lógico de la ventana
    Public sCodispl As String
    '**-Objective: Type or Record
    '-Objetivo: Tipo de registro
    Public sCertype As String
    '**-Objective: Code of the line of business
    '-Objetivo: Código de la linea de negocios
    Public nBranch As Integer
    '**-Objective: Code of the product
    '-Objetivo: Código del producto
    Public nProduct As Integer
    '**-Objective: Number identifying the policy/ quotation/ proposal
    '-Objetivo: Número que identifica la póliza / Cotización/ Solicitud
    Public nPolicy As Integer
    '**-Objective: Number identifying the certificate.
    '-Objetivo: Número que identifica el certificado
    Public nCertif As Integer
    '**-Objective: Code of the branch office.
    '-Objetivo: Código de la oficina ramo
    Public nOfficeAgen As Integer
    '**-Objective: Code of the bank agency
    '-Objetivo: Código de la agencia (Cap)
    Public nAgency As Integer
    '**-Objective: Code of the intermediary
    '-Objetivo: Código del intermediario
    Public nIntermed As Integer
    '**-Objective: Number of the request for remittance of correspondence
    '-Objetivo: Número del requerimiento para enviar la correspondecia
    Public nLettRequest As Integer
    '**-Objective:
    '-Objetivo: Estatus del requerimiento
    Public sPrintStatus As String
    '**-Objective:
    '-Objetivo: Código del cuestionario
    Public nCodForm As Integer
    '**-Objective: Type of reinsurance treaty
    '-Objetivo:
    Public nType As Integer
    '**-Objective:
    '-Objetivo: Código del tipo de envío
    Public nShipmentType As Integer
    '**-Objective:
    '-Objetivo: Tipo de docuemnto
    Public sTypeDocument As String
    '**-Objective:
    '-Objetivo: Número que identifica el recibo
    Public nReceipt As Integer
    '**-Objective:
    '-Objetivo: Código que identifica el modelo de la carta
    Public nLetterNum As Integer
    '**-Objective:
    '-Objetivo: Número de Certificado Official
    Public sOfficialCer As String
    '**-Objective:
    '-Objetivo: Dirección del cliente asociado a la solicitud
    Public sAddress As String
    '**-Objective:
    '-Objetivo: Contenido de la carta
    Public tLetter As String
    '**-Objective:
    '-Objetivo: Forma de despacho
    Public sDitribution As Integer
    '**-Objective:
    '-Objetivo: Situation
    Public nSituation As Integer


    '**%Objective: Validates the data from the header section of the page being processed.
    '**%Parameters:
    '**%    sCodispl - Code of the window (logical code).
    '**%    nShipmentType
    '**%    nOfficeAgen
    '**%    nAgency
    '**%    nIntermed
    '**%    sClient
    '**%    sCertype
    '**%    nBranch
    '**%    nProduct
    '**%    nPolicy
    '**%    nCertif
    '%Objetivo: Esta función valida los datos del encabezado de la página en tratamiento.
    '%Parámetros:
    '%    sCodispl - Código de la ventana (lógico).
    '%    nShipmentType
    '%    nOfficeAgen
    '%    nAgency
    '%    nIntermed
    '%    sClient
    '%    sCertype
    '%    nBranch
    '%    nProduct
    '%    nPolicy
    '%    nCertif
    '------------------------------------------------------------------------------------------------------------------------
    Public Function InsValLT500_K(ByVal sCodispl As String, _
                                  ByVal nShipmentType As Integer, _
                                  ByVal nOfficeAgen As Integer, _
                                  ByVal nAgency As Integer, _
                                  ByVal nIntermed As Integer, _
                                  ByVal sClient As String, _
                                  ByVal sCertype As String, _
                                  ByVal nBranch As Integer, _
                                  ByVal nProduct As Integer, _
                                  ByVal nPolicy As Integer, _
                                  ByVal nCertif As Integer) As String
        '------------------------------------------------------------------------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors
        Dim lclsClient As eClient.Client

        If Not IsIDEMode() Then
        End If

        lclsErrors = New eFunctions.Errors
        lclsClient = New eClient.Client

        With lclsErrors

            '% Se valida que por lo menos se indique un criterio de búsqueda

            If nShipmentType = intNull And _
               nOfficeAgen = intNull And _
               nAgency = intNull And _
               nIntermed = intNull And _
               sClient = vbNullString And _
               nBranch = intNull And _
               nProduct = intNull And _
               nPolicy = intNull And _
               nCertif = intNull Then
                .ErrorMessage(sCodispl, 99022)
            End If

            If sClient <> String.Empty Then
                If Not lclsClient.Find(sClient) Then
                    Call .ErrorMessage(sCodispl, 7050)
                End If
            End If

            InsValLT500_K = .Confirm
        End With

        lclsErrors = Nothing
        lclsClient = Nothing

        Exit Function
        'ObjectRelease = lclsError
    End Function

    '**%Objective: Validates the data from the detail section of the page being processed.
    '**%Parameters:
    '**%    sCodispl - Code of the window (logical code).
    '%Objetivo: Esta función permite validar los datos del detalle de la página en tratamiento.
    '%Parámetros:
    '%    sCodispl - Código de la ventana (lógico).
    '------------------------------------------------------------------------------------------------------------------------
    Public Function insValLT500(ByVal sCodispl As String) As String
        '------------------------------------------------------------------------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors

        If Not IsIDEMode() Then
        End If

        lclsErrors = New eFunctions.Errors
        With lclsErrors

            .ErrorMessage(sCodispl, 500107)

            InsValLT500 = .Confirm
        End With
        lclsErrors = Nothing

        Exit Function
        'ObjectRelease = lclsErrors
    End Function

    '**%Objective: Sends the information necessary to update the records in the database.
    '**%Parameters:
    '**%    sAction   - It indicates the type of action to be applied in the table ("Add", "Update" o "Del")
    '**%    nUsercode - Code of the user creating or updating the record.
    '%Objetivo: Esta función permite enviar la información necesaria de los registros en tratamiento a la base de datos para su
    '% posterior actualización.
    '%Parámetros:
    '%    sAction   - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar", "Actualizar" o "Eliminar").
    '%    nUsercode - Código del usuario que crea o actualiza el registro.
    '------------------------------------------------------------------------------------------------------------------------
    Public Function InsGenerateRequest(ByVal sCodispl As String, _
                                       ByVal sClient As String, _
                                       ByVal nLanguage As Integer, _
                                       ByVal nUsercode As Integer, _
                                       ByVal nLettRequest As Integer, _
                                       ByVal nLetterNum As Integer, _
                                       Optional ByVal sAddress As String = "", _
                                       Optional ByVal sCertype As String = "", _
                                       Optional ByVal nBranch As Integer = 0, _
                                       Optional ByVal nProduct As Integer = 0, _
                                       Optional ByVal nPolicy As Integer = 0, _
                                       Optional ByVal nCertif As Integer = 0) As Boolean
        '------------------------------------------------------------------------------------------------------------------------
        Dim lobjLetter As eLetter.Letter
        Dim lclsLettRequest As eLetter.LettRequest

        If Not IsIDEMode() Then
        End If

        '+ Se crea la solicitud del acuse de recibo de correspondencia
        lclsLettRequest = New eLetter.LettRequest

        InsGenerateRequest = lclsLettRequest.Add_PrintDocuments(0, nLetterNum, Today, dtmNull, dtmNull, "1", nUsercode, nUsercode, sAddress, intNull, 1, Today, sClient, sCertype, nBranch, nProduct, nPolicy, nCertif, intNull, intNull, intNull, vbNullString, intNull, 3)

        Me.nLettRequest = lclsLettRequest.nLettRequest

        lobjLetter = New eLetter.Letter

        With lobjLetter.oParameters
            .Add(sClient)
        End With

        Call lobjLetter.MergeDocument(Nothing, Nothing, Today, nUsercode, False, 2, nLetterNum, nLanguage, vbNullString, lclsLettRequest.nLettRequest, False)
        lobjLetter.tletter = lobjLetter.sMergeResult

        Exit Function
    End Function

    '**%Objective: This method updates or adds a record into the table "PrintDocuments"
    '**%Parameters:
    '**%    sAction   - The type of action to be executed for the record ("Update")
    '**%    nUsercode - Code of the user that creates or updates the record.
    '**%    nShipmentType -
    '**%    sTypeDocument -
    '**%    nOfficeAgen -
    '**%    nAgency -
    '**%    nIntermed -
    '**%    sClient -
    '**%    sCertype -
    '**%    nBranch -
    '**%    nProduct -
    '**%    nPolicy -
    '**%    nCertif -
    '**%    nUsercode -
    '%Objetivo: Este método permite agregar o actualizar un registro en la tabla "PrintDocuments"
    '%Parámetros:
    '%    sAction   - Indica el tipo de acción a ejecutar sobre el registro en la tabla ("Actualizar").
    '%    nUsercode - Código del usuario que crea o actualiza el registro.
    '%    nShipmentType -
    '%    sTypeDocument -
    '%    nOfficeAgen -
    '%    nAgency -
    '%    nIntermed -
    '%    sClient -
    '%    sCertype -
    '%    nBranch -
    '%    nProduct -
    '%    nPolicy -
    '%    nCertif -
    '%    nUsercode -
    '------------------------------------------------------------------------------------------------------------------------
    Private Function Update(ByVal nUsercode As Long) As Boolean
        '------------------------------------------------------------------------------------------------------------------------
        Dim lclsPrintDocuments As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lclsPrintDocuments = New eRemoteDB.Execute

        With lclsPrintDocuments
            .StoredProcedure = "updPrintDocuments"
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With
        lclsPrintDocuments = Nothing

        Exit Function
    End Function

    '**%Objective: Sends the information necessary to update the records in the database.
    '**%Parameters:
    '**%    sAction   - It indicates the type of action to be applied in the table ("Add", "Update" o "Del")
    '**%    nUsercode - Code of the user creating or updating the record.
    '**%    nShipmentType -
    '%Objetivo: Esta función permite enviar la información necesaria de los registros en tratamiento a la base de datos para su
    '% posterior actualización.
    '%Parámetros:
    '%    sAction   - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar", "Actualizar" o "Eliminar").
    '%    nUsercode - Código del usuario que crea o actualiza el registro.
    '%    nShipmentType -
    '------------------------------------------------------------------------------------------------------------------------
    Public Function InsPostLT500(ByVal sAction As String, _
                                 ByVal nUsercode As Long) As Boolean
        '------------------------------------------------------------------------------------------------------------------------
        If Not IsIDEMode() Then
        End If

        Select Case sAction
            Case "Update"
                InsPostLT500 = Update(nUsercode)
        End Select

        Exit Function
    End Function

    '**%Objective: This method updates or adds a record into the table "tmpPrintDocuments"
    '**%Parameters:
    '**%    sAction   - The type of action to be executed for the record ("Add" or "Update")
    '**%    nUsercode - Code of the user that creates or updates the record.
    '**%    <__PARAMETER_LIST_DESC__>
    '%Objetivo: Este método permite agregar o actualizar un registro en la tabla "tmpPrintDocuments"
    '%Parámetros:
    '%    sAction   - Indica el tipo de acción a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
    '%    nUsercode - Código del usuario que crea o actualiza el registro.
    '%    <__PARAMETER_LIST_DESC__>
    '------------------------------------------------------------------------------------------------------------------------
    Public Function AddUpdatetmpPrintDocuments(ByVal sAction As String, Optional ByVal sClient As String = "", _
                                               Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, _
                                               Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Integer = 0, _
                                               Optional ByVal nCertif As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0, _
                                               Optional ByVal nAgency As Integer = 0, Optional ByVal nIntermed As Integer = 0, _
                                               Optional ByVal nShipmentType As Integer = 0, Optional ByVal sTypeDocument As String = "", _
                                               Optional ByVal nReceipt As Integer = 0, Optional ByVal nLettRequest As Integer = 0, _
                                               Optional ByVal nType As Integer = 0, Optional ByVal nCodForm As Integer = 0, _
                                               Optional ByVal nConsecutive As Integer = 0, Optional ByVal sDistribution As Integer = 0, _
                                               Optional ByVal nSituation As Integer = 0) As Boolean
        '------------------------------------------------------------------------------------------------------------------------
        Dim lclstmpPrintDocuments As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lclstmpPrintDocuments = New eRemoteDB.Execute

        With lclstmpPrintDocuments
            .StoredProcedure = "insUpdtmpPrintDocuments"

            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nShipmentType", nShipmentType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypeDocument", sTypeDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCodForm", nCodForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsecutive", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDistribution", sDistribution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            AddUpdatetmpPrintDocuments = .Run(False)
        End With
        lclstmpPrintDocuments = Nothing

        Exit Function
    End Function

    'Antigua insPostCA050
    '**%Objective: It makes the changes into the table "Life" with the values of the VI949 transaction
    '**%Parameters:
    '**%  nTransactio        - Type of transaction. Policy setup, Certificate setup, etc.
    '**%  sCertype           - Type or Record. Sole values:     1-  Proposal     2 - Policy     3 - Quotation
    '**%  nBranch            - Code of the Line of Business. The possible values as per table 10.
    '**%  nProduct           - Code of the product.
    '**%  nPolicy            - Number identifying the policy/ quotation/ proposal
    '**%  nCertif            - Number identifying the certificate
    '**%  dEffecdate         - Date which from the record is valid.
    '%Objetivo: Permite actualizar la tabla Policy con los valores de la transacción CA001_K.
    '%Parámetros:
    '%    nTransactio        - Tipo de transacción. Emisión de póliza, emisión de certificado, etc.
    '%    sCertype           - Tipo de registro. Valores únicos:    1 - Solicitud    2 - Póliza    3 - Cotización
    '%    nBranch            - Código del ramo comercial. Valores posibles según tabla 10.
    '%    nProduct           - Código del producto.
    '%    nPolicy            - Número identificativo de la póliza/ cotización/ solicitud
    '%    nCertif            - Número del certificado
    '%    dEffecdate         - Fecha de efecto del registro.
    '----------------------------------------------------------------------------------------------------
    Public Function insPostPrintDocuments(ByVal nUsercode As Integer, ByVal nTransactio As Integer, _
                                 ByVal sCertype As String, ByVal nBranch As Integer, _
                                 ByVal nProduct As Integer, ByVal nPolicy As Long, _
                                 ByVal nCertif As Long, ByVal dEffecdate As Date) As Boolean
        'ByVal sColinvot As String, ByVal nAgency As Long, _
        'ByVal nOfficeAgen As Integer, ByVal sDitribution As String, _
        'ByVal nShipmentType As Integer, Optional ByVal nIntermed As Integer = 0
        'Optional ByVal nCapitalDoc As Double,  Optional ByVal nCapitalCrossingDoc As Double) As Boolean        '----------------------------------------------------------------------------------------------------
        Dim lrecpostCA050 As eRemoteDB.Execute
        Dim lclsPolicy As Object

        If Not IsIDEMode() Then
        End If

        lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
            '+ Se obtienen los datos de lapóliza en tratamiento.

            lrecpostCA050 = New eRemoteDB.Execute

            With lrecpostCA050
                .StoredProcedure = "insPostCA050"

                .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sColinvot", lclsPolicy.sColinvot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAgency", lclsPolicy.nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nOfficeAgen", lclsPolicy.nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sDitribution", sDitribution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'Se preestablece nShipmentType en 6 - Interseguro
                .Parameters.Add("nShipmentType", 6, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nIntermed", lclsPolicy.nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", lclsPolicy.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                insPostPrintDocuments = .Run(False)
            End With
        Else
            insPostPrintDocuments = False
        End If
        lrecpostCA050 = Nothing
        lclsPolicy = Nothing

        Exit Function
    End Function


    '% Find_Receipt:
    Public Function Find_Receipt(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Long) As Boolean
        Dim lrecreaPremium_a As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If
        lrecreaPremium_a = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaPremium_a'
        '+ Información leída el 06/11/2000 04:35:06 p.m.

        With lrecreaPremium_a
            .StoredProcedure = "reaPremium_a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_Receipt = True
                nReceipt = .FieldToClass("nReceipt")
                .RCloseRec()
            Else
                Find_Receipt = False
            End If
        End With

        lrecreaPremium_a = Nothing

        Exit Function
    End Function
End Class




