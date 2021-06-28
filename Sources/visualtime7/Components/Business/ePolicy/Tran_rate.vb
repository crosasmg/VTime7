Option Strict Off
Option Explicit On
Public Class Tran_rate
    '**+Objective: Class that supports the table Execute it's content is:
    '**+Version: $$Revision: 2 $
    '+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
    '+Version: $$Revision: 2 $

    '**+Objective: Properties according to the table 'tran_rate' in the system 30/06/2004 11:43:55 a.m.
    '+Objetivo: Propiedades según la tabla 'tran_rate' en el sistema 30/06/2004 11:43:55 a.m.
    Public sCertype As String
    Public nBranch As Short
    Public nProduct As Short
    Public nPolicy As Integer
    Public nCertif As Integer
    Public nClassmerch As Short
    Public nPacking As Short
    Public nLimitcapital As Double
    Public nRate As Double
    Public nAmo_deduc As Double
    Public nDeductible As Double
    Public nMaxamount As Double
    Public nMinamount As Double
    Public sFrancapl As String
    Public dNullDate As Date
    Public nUsercode As Short

    '**%Objective: Updates a registry to the table "tran_rate" using the key for this table.
    '**%Parameters:
    '**%    nUsercode    - Code of the user creating the record
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '%Objetivo: Actualiza un registro a la tabla "tran_rate" usando la clave para dicha tabla.
    '%Parámetros:
    '%    nUsercode    - Código del usuario que crea el registro
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código de la línea del negocio
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de la póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short, ByVal nLimitcapital As Double, ByVal nRate As Double, ByVal nAmo_deduc As Double, ByVal nDeductible As Double, ByVal nMaxamount As Double, ByVal nMinamount As Double, ByVal sFrancapl As String) As Boolean
        Dim lclstran_rate As eRemoteDB.Execute

        lclstran_rate = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updtran_rate'. Generated on 30/06/2004 11:43:55 a.m.
        With lclstran_rate
            .StoredProcedure = "insupdtran_rate"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassmerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLimitcapital", nLimitcapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmo_deduc", nAmo_deduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeductible", nDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFrancapl", sFrancapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        lclstran_rate = Nothing

        Exit Function
    End Function

    '**%Parameters: Delete a registry the table "tran_rate" using the key for this table.
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nClassmerch  - Classification of the merchandise
    '**%    nPacking     - Packing code associated with the merchandise
    '**%    nUsercode    - Code of the user deleting the record
    '%Parámetros: Elimina un registro a la tabla "tran_rate" usando la clave para dicha tabla.
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código del ramo
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de póliza
    '%    nCertif      - Número del certificado
    '%    dEffecdate   - Fecha efectiva del registro
    '%    nClassmerch  -
    '%    nPacking     - Tipo de registro
    '%    nUsercode    - Código del usario que borra el registro
    Private Function Delete(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Integer, ByVal nPacking As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lrecdeltran_rate As eRemoteDB.Execute


        lrecdeltran_rate = New eRemoteDB.Execute
        With lrecdeltran_rate
            .StoredProcedure = "deltran_rate"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassmerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

        lrecdeltran_rate = Nothing

        Exit Function
    End Function

    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    sCodispl        - Logical code that identifies the transaction.
    '**%    nMainAction     - Action being executed on the transaction.
    '**%    sAction         - Action begin executed on the grid of the transaction
    '**%    sCertype        - Type of record
    '**%    nBranch         - Code of the line of business
    '**%    nProduct        - Code of the product
    '**%    nPolicy         - Number of the policy
    '**%    nCertif         - Number identifying the certificate
    '**%    dEffecdate      - Effective date of the record
    '**%    nClassmerch     - Classification of the merchandise
    '**%    nPacking        - Packing code associated with the merchandise
    '**%    nLimitcapital   - Amount limit of the value of the merchandise
    '**%    nRate           - Rate to apply
    '**%    nAmo_deduc      - Deductible amount
    '**%    nDeductible     - Deductible Percentaje
    '**%    nMaxamount      - Maximum deductible amount
    '**%    nMinamount      - Minimum deductible amount
    '**%    sFrancapl       - Code of application of franchises type
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
    '%    nClassmerch   - Clase de la mercancía asegurada
    '%    nPacking      - Tipo de embalaje
    '%    nLimitcapital - Monto límite del valor de la mercancía
    '%    nRate         - Tasa a aplicar
    '%    nAmo_deduc    - Monto de deducible
    '%    nDeductible   - Porcentaje de deducible
    '%    nMaxamount    - Monto máximo deducible
    '%    nMinamount    - Monto mínimo deducible
    '%    sFrancapl     - Tipo de aplicación de la franquicia
    Public Function InsValTR6000Upd(ByVal sCodispl As String, _
                                    ByVal nMainAction As Integer, _
                                    ByVal sAction As String, _
                                    ByVal sCertype As String, _
                                    ByVal nBranch As Short, _
                                    ByVal nProduct As Short, _
                                    ByVal nPolicy As Integer, _
                                    ByVal nCertif As Integer, _
                                    ByVal dEffecdate As Date, _
                                    ByVal nClassmerch As Short, _
                                    ByVal nPacking As Short, _
                                    ByVal nLimitcapital As Double, _
                                    ByVal nLimit As Double, _
                                    ByVal nRate As Double, _
                                    ByVal nAmo_deduc As Double, _
                                    ByVal nDeductible As Double, _
                                    ByVal nMaxamount As Double, _
                                    ByVal nMinamount As Double, _
                                    ByVal sFrancapl As String) As String

        Dim lclsErrors As eFunctions.Errors


        lclsErrors = New eFunctions.Errors

        If (nClassmerch = 0 Or nClassmerch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 90062)
        End If

        If (nPacking = 0 Or nPacking = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3447)
        End If

        If (nLimit = 0 Or nLimit = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 90063)
        Else
            If nLimitcapital <> 0 And nLimitcapital <> eRemoteDB.Constants.intNull Then
                If nLimit > nLimitcapital Then
                    Call lclsErrors.ErrorMessage(sCodispl, 90064)
                End If
            End If
        End If

        If nAmo_deduc > 0 And nDeductible > 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 3046)
        End If

        If (nAmo_deduc = 0 Or nAmo_deduc = eRemoteDB.Constants.intNull) And (nDeductible = 0 Or nDeductible = eRemoteDB.Constants.intNull) And sFrancapl <> "1" Then
            Call lclsErrors.ErrorMessage(sCodispl, 38038)
        End If

        If nMaxamount > 0 Then
            If nMaxamount < nMinamount Then
                Call lclsErrors.ErrorMessage(sCodispl, 3462)
            End If
        End If

        If (nRate = 0 Or nRate = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 2042)
        End If

        If sAction <> "Update" Then
            If Me.IsExist(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nClassmerch, nPacking) Then
                Call lclsErrors.ErrorMessage(sCodispl, 10284)
            End If
        End If

        InsValTR6000Upd = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function


    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    sCodispl        - Logical code that identifies the transaction.
    '**%    nMainAction     - Action being executed on the transaction.
    '**%    sAction         - Action begin executed on the grid of the transaction
    '**%    sCertype        - Type of record
    '**%    nBranch         - Code of the line of business
    '**%    nProduct        - Code of the product
    '**%    nPolicy         - Number of the policy
    '**%    nCertif         - Number identifying the certificate
    '**%    dEffecdate      - Effective date of the record
    '**%    nClassmerch     - Classification of the merchandise
    '**%    nPacking        - Packing code associated with the merchandise
    '**%    nLimitcapital   - Amount limit of the value of the merchandise
    '**%    nRate           - Rate to apply
    '**%    nAmo_deduc      - Deductible amount
    '**%    nDeductible     - Deductible Percentaje
    '**%    nMaxamount      - Maximum deductible amount
    '**%    nMinamount      - Minimum deductible amount
    '**%    sFrancapl       - Code of application of franchises type
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
    '%    nClassmerch   - Clase de la mercancía asegurada
    '%    nPacking      - Tipo de embalaje
    '%    nLimitcapital - Monto límite del valor de la mercancía
    '%    nRate         - Tasa a aplicar
    '%    nAmo_deduc    - Monto de deducible
    '%    nDeductible   - Porcentaje de deducible
    '%    nMaxamount    - Monto máximo deducible
    '%    nMinamount    - Monto mínimo deducible
    '%    sFrancapl     - Tipo de aplicación de la franquicia
    Public Function InsValTR6000(ByVal sCodispl As String, _
                                 ByVal nMainAction As Integer, _
                                 ByVal sCertype As String, _
                                 ByVal nBranch As Short, _
                                 ByVal nProduct As Short, _
                                 ByVal nPolicy As Integer, _
                                 ByVal nCertif As Integer, _
                                 ByVal dEffecdate As Date, _
                                 ByVal nLimitcapital As Double) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lcolTran_rates As ePolicy.Tran_rates
        Dim lclstran_rate As ePolicy.Tran_rate
        Dim lclsPolicyWin As ePolicy.Policy_Win

        Dim lblnError As Boolean
        Dim lintLine As Short


        lclsErrors = New eFunctions.Errors
        lcolTran_rates = New ePolicy.Tran_rates

        lintLine = 1
        lblnError = False

        If lcolTran_rates.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            If nLimitcapital <> 0 And nLimitcapital <> eRemoteDB.Constants.intNull Then
                For Each lclstran_rate In lcolTran_rates
                    If lclstran_rate.nLimitcapital > nLimitcapital Then
                        Call lclsErrors.ErrorMessage(sCodispl, 90064, lintLine)
                        lblnError = True
                    End If
                    lintLine = lintLine + 1
                Next lclstran_rate
            End If

            If Not lblnError Then
                lclsPolicyWin = New ePolicy.Policy_Win
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "TR6000", "2")
                lclsPolicyWin = Nothing
            End If
        End If

        InsValTR6000 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    nHeader         - Indicator of the zone (Header or detail)
    '**%    sCodispl        - Logical code that identifies the transaction
    '**%    nMainAction     - Action being executed on the transaction
    '**%    sAction         - Action begin executed on the grid of the transaction
    '**%    nUsercode       - Code of the user performing the transaction
    '**%    sCertype        - Type of record
    '**%    nBranch         - Code of the line of business
    '**%    nProduct        - Code of the product
    '**%    nPolicy         - Number of the policy
    '**%    nCertif         - Number identifying the certificate
    '**%    dEffecdate      - Effective date of the record
    '**%    nClassmerch     - Classification of the merchandise
    '**%    nPacking        - Packing code associated with the merchandise
    '**%    nLimitcapital   - Amount limit of the value of the merchandise
    '**%    nRate           - Rate to apply
    '**%    nAmo_deduc      - Deductible amount
    '**%    nDeductible     - Deductible Percentaje
    '**%    nMaxamount      - Maximum deductible amount
    '**%    nMinamount      - Minimum deductible amount
    '**%    sFrancapl       - Code of application of franchises type
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '*    nHeader       - Indicador de zona de encabezado o detalle
    '%    sCodispl      - Código lógico que identifica la transacción.
    '%    nMainAction   - Acción que se ejecuta sobre la transacción.
    '%    sAction       - Acción que se ejecuta sobre el grid de la transacción
    '%    nUsercode     - Código del usuario que ejecuta la transacción
    '%    sCertype      - Tipo de registro
    '%    nBranch       - Código de la línea del negocio
    '%    nProduct      - Código del producto
    '%    nPolicy       - Número de la póliza
    '%    nCertif       - Número que identifica el certificado
    '%    dEffecdate    - Fecha de efecto del registro
    '%    nClassmerch   - Clase de la mercancía asegurada
    '%    nPacking      - Tipo de embalaje
    '%    nLimitcapital - Monto límite del valor de la mercancía
    '%    nRate         - Tasa a aplicar
    '%    nAmo_deduc    - Monto de deducible
    '%    nDeductible   - Porcentaje de deducible
    '%    nMaxamount    - Monto máximo deducible
    '%    nMinamount    - Monto mínimo deducible
    '%    sFrancapl     - Tipo de aplicación de la franquicia
    Public Function InsPostTR6000(ByVal nHeader As Boolean, _
                                  ByVal sCodispl As String, _
                                  ByVal nMainAction As Integer, _
                                  ByVal sAction As String, _
                                  ByVal nUsercode As Integer, _
                                  ByVal sCertype As String, _
                                  ByVal nBranch As Short, _
                                  ByVal nProduct As Short, _
                                  ByVal nPolicy As Integer, _
                                  ByVal nCertif As Integer, _
                                  ByVal dEffecdate As Date, _
                               ByVal nClassmerch As Short, _
                               ByVal nPacking As Short, _
                               ByVal nLimitcapital As Double, _
                               ByVal nRate As Double, _
                               ByVal nAmo_deduc As Double, _
                               ByVal nDeductible As Double, _
                               ByVal nMaxamount As Double, _
                               ByVal nMinamount As Double, _
                               ByVal sFrancapl As String) As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lcolTran_rates As ePolicy.Tran_rates
        Dim mstrContent As String = eRemoteDB.Constants.strNull

        If sAction = "Del" Then
            InsPostTR6000 = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nClassmerch, nPacking, nUsercode)

            '+ Se llama a la función FIND de la colección Tran_rates para saber si hay o no registros

            lcolTran_rates = New ePolicy.Tran_rates
            Call lcolTran_rates.Find(sCertype, _
                                      nBranch, _
                                      nProduct, _
                                      nPolicy, _
                                      nCertif, _
                                      dEffecdate)

            If lcolTran_rates.Count = 0 Then
                mstrContent = "1"
            Else
                mstrContent = "2"
            End If

        Else
            InsPostTR6000 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nClassmerch, nPacking, nLimitcapital, nRate, nAmo_deduc, nDeductible, nMaxamount, nMinamount, sFrancapl)
            mstrContent = "2"
        End If

        If InsPostTR6000 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "TR6000", mstrContent)
            lclsPolicyWin = Nothing
        End If

        Exit Function
    End Function


    '**%Objective: It verifies the existence of a registry in table "tran_rate" using the key of this table.
    '**%Parameters:
    '**%    sCertype     - Type of record
    '**%    nBranch      - Code of the line of business
    '**%    nProduct     - Code of the product
    '**%    nPolicy      - Number of the policy
    '**%    nCertif      - Number identifying the certificate
    '**%    dEffecdate   - Effective date of the record
    '**%    nClassmerch  - Classification of the merchandise
    '**%    nPacking     - Packing code associated with the merchandise
    '%Objetivo: Verifica la existencia de un registro en la tabla "tran_rate" usando la clave de dicha tabla.
    '%Parámetros:
    '%    sCertype     - Tipo de registro
    '%    nBranch      - Código de la línea del negocio
    '%    nProduct     - Código del producto
    '%    nPolicy      - Número de la póliza
    '%    nCertif      - Número que identifica el certificado
    '%    dEffecdate   - Fecha de efecto del registro
    '%    nClassmerch  - Clase de la mercancía asegurada
    '%    nPacking     - Tipo de embalaje
    Public Function IsExist(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short) As Boolean
        Dim lclstran_rate As eRemoteDB.Execute
        Dim lintExist As Short

        lclstran_rate = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valtran_rateExist'. Generated on 30/06/2004 11:43:55 a.m.
        With lclstran_rate
            .StoredProcedure = "reatran_rate_v"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassmerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclstran_rate = Nothing

        Exit Function
    End Function


    '**%Objective: Function that makes the search in the table 'tran_rate'.
    '**%Parameters:
    '**%    sCertype   - Type of record
    '**%    nBranch    - Code of the line of business
    '**%    nProduct   - Code of the product
    '**%    nPolicy    - Policy number
    '**%    nCertif    - Number identifying the certificate
    '**%    dEffecdate - Effective date
    '**%    nClassmerch - Class of the merchandise
    '**%    nPacking    - Type of packing
    '%Objetivo: Función que realiza la busqueda en la tabla 'tran_rate'.
    '%Parámetros:
    '%    sCertype    - Tipo de registro
    '%    nBranch     - Código de la línea del negocio
    '%    nProduct    - Código del producto
    '%    nPolicy     - Número de la póliza
    '%    nCertif     - Número que identifica el certificado
    '%    dEffecdate  - Fecha de efecto
    '%    nClassmerch - Clase de la mercancía
    '%    nPacking    - Tipo de embalaje
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Short, ByVal nPacking As Short) As Boolean
        Dim lclstran_rate As eRemoteDB.Execute

        lclstran_rate = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reatran_rate'. Generated on 30/06/2004 11:43:55 a.m.
        With lclstran_rate
            .StoredProcedure = "reatran_rate_Deduc"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassmerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Me.nRate = .FieldToClass("nRate")
                Me.nAmo_deduc = .FieldToClass("nAmo_deduc")
                Me.nDeductible = .FieldToClass("nDeductible")
                Me.nMaxamount = .FieldToClass("nMaxamount")
                Me.nMinamount = .FieldToClass("nMinamount")
                Me.sFrancapl = .FieldToClass("sFrancapl")
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclstran_rate = Nothing

        Exit Function
    End Function
End Class












