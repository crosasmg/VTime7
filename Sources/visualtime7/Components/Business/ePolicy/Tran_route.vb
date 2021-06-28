Option Strict Off
Option Explicit On
Public Class tran_route
    '**+Objective: Class that supports the table Execute it's content is:
    '**+Version: $$Revision: 2 $
    '+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
    '+Version: $$Revision: 2 $

    '**+Objective: Properties according to the table 'tran_route' in the system 30/06/2004 11:43:55 a.m.
    '+Objetivo: Propiedades según la tabla 'tran_route' en el sistema 30/06/2004 11:43:55 a.m.
    Public sCertype As String
    Public nBranch As Short
    Public nProduct As Short
    Public nPolicy As Integer
    Public nCertif As Integer
    Public nRoute As Short
    'Public sDestination As String
    'Public sOrigin As String
    Public nTypRoute As Integer
    Public nNotenum As Integer
    'Public nStatistic As Integer
    Public nTransptype As Short

    '**%Objective: Add a record to the table "tran_route"
    '**%Parameters:
    '**%    nUsercode     - Code of the user creating the record
    '**%    sCertype      - Type of record
    '**%    nBranch       - Code of the line of business
    '**%    nProduct      - Code of the product
    '**%    nPolicy       - Number of the policy
    '**%    nCertif       - Number identifying the certificate
    '**%    dEffecdate    - Effective date of the record
    '**%    nRoute        - Code of the route covered
    '**%    nTypRoute     - Type of route
    '**%    nNoteNum      - Number of the note containing the comments
    '**%    nTranspType   - Transport type
    '%Objetivo: Agrega un registro a la tabla "tran_route"
    '%Parámetros:
    '%    nUsercode     - Código del usuario que crea el registro
    '%    sCertype      - Tipo de registro
    '%    nBranch       - Código de la línea del negocio
    '%    nProduct      - Código del producto
    '%    nPolicy       - Número de la póliza
    '%    nCertif       - Número que identifica el certificado
    '%    dEffecdate    - Fecha de efecto del registro
    '%    nRoute        - Código de la ruta asegurada
    '%    nTypRoute     - Tipo de ruta
    '%    nNoteNum      - Número de la nota que contiene los comentarios
    '%    nTranspType   - Tipo de transporte
    Private Function Add(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nRoute As Short, ByVal nTypRoute As Integer, ByVal nNotenum As Integer, ByVal nTransptype As Short) As Boolean
        Dim lclsTran_Route As eRemoteDB.Execute


        lclsTran_Route = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.cretran_route'. Generated on 30/06/2004 11:43:55 a.m.

        With lclsTran_Route
            'PENDING: Procedure not found
            .StoredProcedure = "cretran_route"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoute", nRoute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypRoute", nTypRoute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNoteNum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTranspType", nTransptype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

        lclsTran_Route = Nothing

        Exit Function
    End Function

    '**%Objective: Updates a registry to the table "tran_route" using the key for this table.
    '**%Parameters:
    '**%    nUsercode    - Code of the user creating the record
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nRoute       - Code of the route covered
    '**%    nTypRoute    - Type of route
    '**%    nNoteNum     - Number of the note containing the comments
    '**%    nTranspType  - Transport type
    '%Objetivo: Actualiza un registro a la tabla "tran_route" usando la clave para dicha tabla.
    '%Parámetros:
    '%    nUsercode    - Código del usuario que crea el registro
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código de la línea del negocio
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de la póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nRoute       - Código de la ruta asegurada
    '%    nTypRoute    - Tipo de ruta
    '%    nNoteNum     - Número de la nota que contiene los comentarios
    '%    nTranspType   - Tipo de transporte
    Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nRoute As Short, ByVal nTypRoute As Integer, ByVal nNotenum As Integer, ByVal nTransptype As Short) As Boolean
        Dim lclsTran_Route As eRemoteDB.Execute


        lclsTran_Route = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updtran_route'. Generated on 30/06/2004 11:43:55 a.m.
        With lclsTran_Route
            .StoredProcedure = "insupdtran_route"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoute", nRoute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypRoute", nTypRoute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNoteNum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTranspType", nTransptype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        lclsTran_Route = Nothing

        Exit Function
    End Function

    '**%Objective: Delete a registry the table "tran_route" using the key for this table.
    '**%Parameters:
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nRoute       - Code of the route covered
    '**%    nUsercode    - Code of the user creating the record
    '%Objetivo: Elimina un registro a la tabla "tran_route" usando la clave para dicha tabla.
    '%Parámetros:
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código de la línea del negocio
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de la póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nRoute       - Código de la ruta asegurada
    '%    nUsercode    - Código del usuario que crea el registro
    Private Function Delete(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nRoute As Short, ByVal nUsercode As Short) As Boolean
        Dim lclsTran_Route As eRemoteDB.Execute


        lclsTran_Route = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.deltran_route'. Generated on 30/06/2004 11:43:55 a.m.
        With lclsTran_Route
            .StoredProcedure = "deltran_route"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoute", nRoute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete = .Run(False)
        End With

        lclsTran_Route = Nothing

        Exit Function
    End Function

    '**%Objective: It verifies the existence of a registry in table "tran_route" using the key of this table.
    '**%Parameters:
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nRoute       - Code of the route covered
    '%Objetivo: Verifica la existencia de un registro en la tabla "tran_route" usando la clave de dicha tabla.
    '%Parámetros:
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código de la línea del negocio
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de la póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nRoute       - Código de la ruta asegurada
    Private Function IsExist(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nRoute As Short) As Boolean
        Dim lclsTran_Route As eRemoteDB.Execute
        Dim lintExist As Short


        lclsTran_Route = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valtran_routeExist'. Generated on 30/06/2004 11:43:55 a.m.
        With lclsTran_Route
            .StoredProcedure = "reatran_route_v"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoute", nRoute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsTran_Route = Nothing

        Exit Function
    End Function

    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    sCodispl     - Logical code that identifies the transaction.
    '**%    nMainAction  - Action being executed on the transaction.
    '**%    sAction      - Action begin executed on the grid of the transaction
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nRoute       - Code of the route covered
    '**%    nTypRoute    - Type of route
    '**%    nNoteNum     - Number of the note containing the comments
    '**%    nTranspType  - Transport type
    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    sCodispl     - Código lógico que identifica la transacción.
    '%    nMainAction  - Acción que se ejecuta sobre la transacción.
    '%    sAction      - Acción que se ejecuta sobre el grid de la transacción
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código de la línea del negocio
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de la póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nRoute       - Código de la ruta asegurada
    '%    nTypRoute    - Tipo de ruta
    '%    nNoteNum     - Número de la nota que contiene los comentarios
    '%    nTranspType  - Tipo de transporte
    Public Function InsValTR002(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nRoute As Short, ByVal nTypRoute As Integer, ByVal nNotenum As Integer, ByVal nTransptype As Short) As String
        Dim lclsErrors As eFunctions.Errors


        lclsErrors = New eFunctions.Errors

        If (nTypRoute = 0 Or nTypRoute = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3471)
        End If

        If (nTransptype = 0 Or nTransptype = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3443)
        End If
        If sAction = "Add" Then
            If nTypRoute <> 0 And nTypRoute <> eRemoteDB.Constants.intNull And nTransptype <> 0 And nTransptype <> eRemoteDB.Constants.intNull Then
                If insValPrevRoute(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTypRoute, nTransptype) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 90311)
                End If
            End If
        End If
        InsValTR002 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nTypRoute    - Type of route
    '**%    nTranspType  - Transport type
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código de la línea del negocio
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de la póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nTypRoute    - Tipo de ruta
    Public Function insValPrevRoute(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nTypRoute As Integer, ByVal nTransptype As Short) As Boolean
        Dim lclsTran_Routes As tran_routes
        Dim lclsTran_Route As tran_route



        lclsTran_Routes = New tran_routes

        With lclsTran_Routes
            If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                For Each lclsTran_Route In lclsTran_Routes
                    If lclsTran_Route.nTypRoute = nTypRoute And lclsTran_Route.nTransptype = nTransptype Then
                        insValPrevRoute = True
                        Exit For
                    End If
                Next lclsTran_Route
            End If
        End With

        lclsTran_Routes = Nothing
        Exit Function
        lclsTran_Routes = Nothing
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    nHeader      - Indicator of the zone (Header or detail)
    '**%    sCodispl     - Logical code that identifies the transaction
    '**%    nMainAction  - Action being executed on the transaction
    '**%    sAction      - Action begin executed on the grid of the transaction
    '**%    nUsercode    - Code of the user performing the transaction
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nRoute       - Code of the route covered
    '**%    nTypRoute    - Type of route
    '**%    nNoteNum     - Number of the note containing the comments
    '**%    nTranspType  - Transport type
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '*    nHeader      - Indicador de zona de encabezado o detalle
    '%    sCodispl     - Código lógico que identifica la transacción.
    '%    nMainAction  - Acción que se ejecuta sobre la transacción.
    '%    sAction      - Acción que se ejecuta sobre el grid de la transacción
    '%    nUsercode    - Código del usuario que ejecuta la transacción
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código de la línea del negocio
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de la póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nRoute       - Código de la ruta asegurada
    '%    nTypRoute    - Tipo de ruta
    '%    nNoteNum     - Número de la nota que contiene los comentarios
    '%    nTransport   - Tipo de transporte
    Public Function InsPostTR002(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nRoute As Short, ByVal nTypRoute As Integer, ByVal nNotenum As Integer, ByVal nTransptype As Short) As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win


        If sAction = "Del" Then
            InsPostTR002 = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nRoute, nUsercode)
        Else
            InsPostTR002 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nRoute, nTypRoute, nNotenum, nTransptype)
        End If

        If InsPostTR002 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "TR002", "2")
            lclsPolicyWin = Nothing
        End If

        Exit Function
    End Function
End Class











