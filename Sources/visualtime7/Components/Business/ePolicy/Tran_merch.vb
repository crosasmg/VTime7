Option Strict Off
Option Explicit On
Public Class Tran_merch
    '**+Objective: Class that supports the table Execute it's content is:
    '**+Version: $$Revision: 2 $
    '+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
    '+Version: $$Revision: 2 $

    '**+Objective: Properties according to the table 'Tran_merch' in the system 02/07/2004 11:08:19 a.m.
    '+Objetivo: Propiedades según la tabla 'Tran_merch' en el sistema 02/07/2004 11:08:19 a.m.
    Public sCertype As String
    Public nBranch As Short
    Public nProduct As Short
    Public nPolicy As Integer
    Public nCertif As Integer
    Public nClassmerch As Short
    Public nPacking As Short
    Public sDescript As String
    Public nQuantrans As Short
    Public nUnit As Short
    Public nAmount As Double
    Public sFranDedi As String
    Public nFranDedRate As Double
    Public nMinAmount As String
    Public nCurrency As Integer
    Public nFrandedi As Double




    '**%Objective: Add a record to the table "Tran_merch"
    '**%Parameters:
    '**%    nUsercode    - Code of the user creating creating the record
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Policy number
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nClassMerch  - Classification of the merchandise
    '**%    nPacking     - Packing Code associated with the Merchandise
    '**%    sDescript    - Description of the merchandise
    '**%    nQuanTrans   - Number of elements that are transported on the basis of the specified unit
    '**%    nUnit        - Unit of capacity or weight of the elements that are transported
    '**%    nAmount      - Sum insured of the shipped merchandise
    '**%    sFranDedi    - Code of the indicator of franchise or deductible
    '**%    nFranDedRate - Percentage of the franchise of deductible
    '**%    nMinAmount   - Minimum amount of the franchise of deductible
    '%Objetivo: Agrega un registro a la tabla "Tran_merch"
    '%Parámetros:
    '%    nUsercode    - Código del usuario que crea el registro
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código del ramo comercial
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nClassMerch  - Clasificación de la mercancía
    '%    nPacking     - Tipo de embalaje en el transporte de mercancía
    '%    sDescript    - Descripción de la mercancía
    '%    nQuanTrans   - Número de elementos que se transportan en base a la unidad especificada
    '%    nUnit        - Unidad de capacidad o peso de los elementos que se transportan
    '%    nAmount      - Suma asegurada de la mercancía transportada.
    '%    sFranDedi    - Código del indicador de franquicía o deducible
    '%    nFranDedRate - Porcentaje de la franquicía o deducible
    '%    nMinAmount   - Monto Mínimo de la franquicía o deducible
    Private Function Add(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short, ByVal sDescript As String, ByVal nQuantrans As Short, ByVal nUnit As Short, ByVal nAmount As Double, ByVal sFranDedi As String, ByVal nFranDedRate As Double, ByVal nMinAmount As String, ByVal nCurrency As Integer) As Boolean
        Dim lclsTran_merch As eRemoteDB.Execute


        lclsTran_merch = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.creTran_merch'. Generated on 02/07/2004 11:08:19 a.m.

        With lclsTran_merch
            .StoredProcedure = "creTran_merch"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassMerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuanTrans", nQuantrans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnit", nUnit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFranDedi", sFranDedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFranDedRate", nFranDedRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinAmount", nMinAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

        lclsTran_merch = Nothing

        Exit Function
    End Function

    '**%Objective: Updates a registry to the table "Tran_merch" using the key for this table.
    '**%Parameters:
    '**%    nUsercode    - Code of the user updating the record
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Policy number
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nClassMerch  - Classification of the merchandise
    '**%    nPacking     - Packing Code associated with the Merchandise
    '**%    sDescript    - Description of the merchandise
    '**%    nQuanTrans   - Number of elements that are transported on the basis of the specified unit
    '**%    nUnit        - Unit of capacity or weight of the elements that are transported
    '**%    nAmount      - Sum insured of the shipped merchandise
    '**%    sFranDedi    - Code of the indicator of franchise or deductible
    '**%    nFranDedRate - Percentage of the franchise of deductible
    '**%    nMinAmount   - Minimum amount of the franchise of deductible
    '**%    nCurrency    - Code of the currency
    '%Objetivo: Actualiza un registro a la tabla "Tran_merch" usando la clave para dicha tabla.
    '%Parámetros:
    '%    nUsercode    - Código del usuario que actualiza el registro
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código del ramo comercial
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nClassMerch  - Clasificación de la mercancía
    '%    nPacking     - Tipo de embalaje en el transporte de mercancía
    '%    sDescript    - Descripción de la mercancía
    '%    nQuanTrans   - Número de elementos que se transportan en base a la unidad especificada
    '%    nUnit        - Unidad de capacidad o peso de los elementos que se transportan
    '%    nAmount      - Suma asegurada de la mercancía transportada.
    '%    sFranDedi    - Código del indicador de franquicía o deducible
    '%    nFranDedRate - Porcentaje de la franquicía o deducible
    '%    nMinAmount   - Monto Mínimo de la franquicía o deducible
    '%    nCurrency    - Código de la moneda
    Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short, ByVal sDescript As String, ByVal nQuantrans As Short, ByVal nUnit As Short, ByVal nAmount As Double, ByVal sFranDedi As String, ByVal nFranDedRate As Double, ByVal nMinAmount As String, ByVal nCurrency As Integer) As Boolean
        Dim lclsTran_merch As eRemoteDB.Execute


        lclsTran_merch = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updTran_merch'. Generated on 02/07/2004 11:08:19 a.m.
        With lclsTran_merch
            .StoredProcedure = "insupdTran_merch"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassMerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuanTrans", nQuantrans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnit", nUnit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFranDedi", sFranDedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFranDedRate", nFranDedRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinAmount", nMinAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        lclsTran_merch = Nothing

        Exit Function
    End Function

    '**%Objective: Delete a registry the table "Tran_merch" using the key for this table.
    '**%Parameters:
    '**%    sCertype    - Type of record
    '**%    nBranch     - Code of the line of business
    '**%    nProduct    - Code of the product
    '**%    nPolicy     - Policy number
    '**%    nCertif     - Number identifying the certificate
    '**%    dEffecdate  - Effective date of the record
    '**%    nClassMerch - Classification of the merchandise
    '**%    nPacking    - Packing Code associated with the Merchandise
    '**%    nCurrency   - Code of the currency
    '**%    nUsercode   - Code of the user deleting the record
    '%Objetivo: Elimina un registro a la tabla "Tran_merch" usando la clave para dicha tabla.
    '%Parámetros:
    '%    sCertype    - Tipo de registro
    '%    nBranch     - Código del ramo comercial
    '%    nProduct    - Código del producto
    '%    nPolicy     - Número de póliza
    '%    nCertif     - Número que identifica el certificado
    '%    dEffecdate  - Fecha de efecto del registro
    '%    nClassMerch - Clasificación de la mercancía
    '%    nPacking    - Tipo de embalaje en el transporte de mercancía
    '%    nCurrency   - Código de la moneda
    '%    nUsercode   - Código del usuario que elimina el registro
    Private Function Delete(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lclsTran_merch As eRemoteDB.Execute


        lclsTran_merch = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.delTran_merch'. Generated on 02/07/2004 11:08:19 a.m.
        With lclsTran_merch
            .StoredProcedure = "delTran_merch"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassMerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

        lclsTran_merch = Nothing

        Exit Function
    End Function

    '**%Objective: It verifies the existence of a registry in table "Tran_merch" using the key of this table.
    '**%Parameters:
    '**%    sCertype    - Type of record
    '**%    nBranch     - Code of the line of business
    '**%    nProduct    - Code of the product
    '**%    nPolicy     - Policy number
    '**%    nCertif     - Number identifying the certificate
    '**%    dEffecdate  - Effective date of the record
    '**%    nClassMerch - Classification of the merchandise
    '**%    nPacking    - Packing Code associated with the Merchandise
    '**%    nCurrency   - Code of the currency
    '%Objetivo: Verifica la existencia de un registro en la tabla "Tran_merch" usando la clave de dicha tabla.
    '%Parámetros:
    '%    sCertype    - Tipo de registro
    '%    nBranch     - Código del ramo comercial
    '%    nProduct    - Código del producto
    '%    nPolicy     - Número de póliza
    '%    nCertif     - Número que identifica el certificado
    '%    dEffecdate  - Fecha de efecto del registro
    '%    nClassMerch - Clasificación de la mercancía
    '%    nPacking    - Tipo de embalaje en el transporte de mercancía
    '%    nCurrency   - Código de la moneda
    Private Function IsExist(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short, ByVal nCurrency As Integer) As Boolean
        Dim lclsTran_merch As eRemoteDB.Execute
        Dim lintExist As Short


        lclsTran_merch = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valTran_merchExist'. Generated on 02/07/2004 11:08:19 a.m.
        With lclsTran_merch
            .StoredProcedure = "reaTran_merch_v"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassMerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsTran_merch = Nothing

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
    '**%    nPolicy      - Policy number
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nClassMerch  - Classification of the merchandise
    '**%    nPacking     - Packing Code associated with the Merchandise
    '**%    sDescript    - Description of the merchandise
    '**%    nQuanTrans   - Number of elements that are transported on the basis of the specified unit
    '**%    nUnit        - Unit of capacity or weight of the elements that are transported
    '**%    nAmount      - Sum insured of the shipped merchandise
    '**%    sFranDedi    - Code of the indicator of franchise or deductible
    '**%    nFranDedRate - Percentage of the franchise of deductible
    '**%    nMinAmount   - Minimum amount of the franchise of deductible
    '**%    nCurrency    - Code of the currency
    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    sCodispl     - Código lógico que identifica la transacción.
    '%    nMainAction  - Acción que se ejecuta sobre la transacción.
    '%    sAction      - Acción que se ejecuta sobre el grid de la transacción
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código del ramo comercial
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nClassMerch  - Clasificación de la mercancía
    '%    nPacking     - Tipo de embalaje en el transporte de mercancía
    '%    sDescript    - Descripción de la mercancía
    '%    nQuanTrans   - Número de elementos que se transportan en base a la unidad especificada
    '%    nUnit        - Unidad de capacidad o peso de los elementos que se transportan
    '%    nAmount      - Suma asegurada de la mercancía transportada.
    '%    sFranDedi    - Código del indicador de franquicía o deducible
    '%    nFranDedRate - Porcentaje de la franquicía o deducible
    '%    nMinAmount   - Monto Mínimo de la franquicía o deducible
    '%    nCurrency    - Código de la moneda
    Public Function InsValTR003(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short, ByVal sDescript As String, ByVal nQuantrans As Short, ByVal nUnit As Short, ByVal nAmount As Double, ByVal sFranDedi As String, ByVal nFranDedRate As Double, ByVal nMinAmount As String, ByVal nCurrency As Integer) As String
        Dim lclsErrors As eFunctions.Errors


        lclsErrors = New eFunctions.Errors

        If (nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 10107)
        End If

        If (nClassmerch = 0 Or nClassmerch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3280)
        End If
        If (nPacking = 0 Or nPacking = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3447)
        End If
        If (nAmount = 0 Or nAmount = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3543)
        End If

        InsValTR003 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
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
    '**%    nPolicy      - Policy number
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nClassMerch  - Classification of the merchandise
    '**%    nPacking     - Packing Code associated with the Merchandise
    '**%    sDescript    - Description of the merchandise
    '**%    nQuanTrans   - Number of elements that are transported on the basis of the specified unit
    '**%    nUnit        - Unit of capacity or weight of the elements that are transported
    '**%    nAmount      - Sum insured of the shipped merchandise
    '**%    sFranDedi    - Code of the indicator of franchise or deductible
    '**%    nFranDedRate - Percentage of the franchise of deductible
    '**%    nMinAmount   - Minimum amount of the franchise of deductible
    '**%    nCurrency    - Code of the currency
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '*    nHeader      - Indicador de zona de encabezado o detalle
    '%    sCodispl     - Código lógico que identifica la transacción.
    '%    nMainAction  - Acción que se ejecuta sobre la transacción.
    '%    sAction      - Acción que se ejecuta sobre el grid de la transacción
    '%    nUsercode    - Código del usuario que elimina el registro
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código del ramo comercial
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nClassMerch  - Clasificación de la mercancía
    '%    nPacking     - Tipo de embalaje en el transporte de mercancía
    '%    sDescript    - Descripción de la mercancía
    '%    nQuanTrans   - Número de elementos que se transportan en base a la unidad especificada
    '%    nUnit        - Unidad de capacidad o peso de los elementos que se transportan
    '%    nAmount      - Suma asegurada de la mercancía transportada.
    '%    sFranDedi    - Código del indicador de franquicía o deducible
    '%    nFranDedRate - Porcentaje de la franquicía o deducible
    '%    nMinAmount   - Monto Mínimo de la franquicía o deducible
    '%    nCurrency    - Código de la moneda
    Public Function InsPostTR003(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short, ByVal sDescript As String, ByVal nQuantrans As Short, ByVal nUnit As Short, ByVal nAmount As Double, ByVal sFranDedi As String, ByVal nFranDedRate As Double, ByVal nMinAmount As String, ByVal nCurrency As Integer) As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win


        If sAction = "Del" Then
            InsPostTR003 = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nClassmerch, nPacking, nCurrency, nUsercode)
        Else
            InsPostTR003 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nClassmerch, nPacking, sDescript, nQuantrans, nUnit, nAmount, sFranDedi, nFranDedRate, nMinAmount, nCurrency)
            If InsPostTR003 Then
                lclsPolicyWin = New ePolicy.Policy_Win
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "TR003", "2")
                lclsPolicyWin = Nothing
            End If
        End If

        Exit Function
    End Function

    '**%Objective: Function that makes the search in the table 'Tran_merch'.
    '**%Parameters:
    '**%    sCertype   - Type of record
    '**%    nBranch    - Code of the line of business
    '**%    nProduct   - Code of the product
    '**%    nPolicy    - Policy number
    '**%    nCertif    - Number identifying of the certificate
    '**%    nCurrency  - Code of the currency
    '**%    dEffecdate - Effective date of the record
    '%Objetivo: Función que realiza la busqueda en la tabla 'Tran_merch'.
    '%Parámetros:
    '%    sCertype   - Tipo de registro
    '%    nBranch    - Código del ramo comercial
    '%    nProduct   - Código del producto
    '%    nPolicy    - Número de póliza
    '%    nCertif    - Número que identifica el certificado
    '%    nCurrency  - Código de la moneda
    '%    dEffecdate - Fecha de efecto del registro
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nCurrency As Integer, ByVal nClassmerch As Integer, ByVal nPacking As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lclsTran_merch As eRemoteDB.Execute


        lclsTran_merch = New eRemoteDB.Execute

        With lclsTran_merch
            .StoredProcedure = "reaTran_merchdet"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassmerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Me.sCertype = sCertype
                Me.nBranch = nBranch
                Me.nProduct = nProduct
                Me.nPolicy = nPolicy
                Me.nCertif = nCertif
                Me.nClassmerch = .FieldToClass("nClassMerch")
                Me.nPacking = .FieldToClass("nPacking")
                Me.sDescript = .FieldToClass("sDescript")
                Me.nQuantrans = .FieldToClass("nQuanTrans")
                Me.nUnit = .FieldToClass("nUnit")
                Me.nAmount = .FieldToClass("nAmount")
                Me.sFranDedi = .FieldToClass("sFranDedi")
                Me.nFranDedRate = .FieldToClass("nFranDedRate")
                Me.nMinAmount = .FieldToClass("nMinAmount")
                Me.nCurrency = .FieldToClass("nCurrency")
                Me.nFrandedi = IIf(.FieldToClass("nAmount") * IIf(.FieldToClass("nFranDedRate") < 1, .FieldToClass("nFranDedRate"), .FieldToClass("nFranDedRate") / 100) < 0, 0, .FieldToClass("nAmount") * IIf(.FieldToClass("nFranDedRate") < 1, .FieldToClass("nFranDedRate"), .FieldToClass("nFranDedRate") / 100))
                If Me.nFrandedi < CDbl(Me.nMinAmount) Then
                    Me.nFrandedi = IIf(CDbl(Me.nMinAmount) < 0, 0, Me.nMinAmount)
                End If
                .RCloseRec()
                Find = True
            Else
                Find = False
            End If
        End With

        lclsTran_merch = Nothing

        Exit Function
    End Function
End Class











