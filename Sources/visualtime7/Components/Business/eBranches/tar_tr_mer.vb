Option Strict Off
Option Explicit On
Public Class tar_tr_mer
    '**+Objective: Class that supports the table Execute it's content is:
    '**+Version: $$Revision: 2 $
    '+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
    '+Version: $$Revision: 2 $

    '**+Objective: Properties according to the table 'tar_tr_mer' in the system 30/08/2004 02:28:11 p.m.
    '+Objetivo: Propiedades según la tabla 'tar_tr_mer' en el sistema 30/08/2004 02:28:11 p.m.
    Public nBranch As Short
    Public nProduct As Short
    Public nCurrency As Short
    Public dEffecDate As Date
    Public nClassMerch As Short
    Public nPacking As Short
    Public nRate As Double

    '**%Objective: Updates a registry to the table "tar_tr_mer" using the key for this table.
    '**%Parameters:
    '**%    nUsercode   - The user code
    '**%    nBranch     - Code of the commercial branch.
    '**%    nProduct    - Code of the product.
    '**%    nCurrency   - Code of the currency.
    '**%    dEffecdate  - Date which from the record is valid.
    '**%    nClassMerch - Classification of the merchandise
    '**%    nPacking    - Packing code associated with the merchandise
    '**%    nRate       - Rate (o/oo) to be applied to obtain the premium
    '%Objetivo: Actualiza un registro a la tabla "tar_tr_mer" usando la clave para dicha tabla.
    '%Parámetros:
    '%    nUsercode   - Código del usuario
    '%    nBranch     - Codigo del ramo comercial.
    '%    nProduct    - Codigo del producto.
    '%    nCurrency   - Código de la moneda.
    '%    dEffecdate  - Fecha de efecto del registro.
    '%    nClassMerch - Clase de mercancía asegurada
    '%    nPacking    - Corresponde al tipo de embalaje posible de utilización en el transporte de mercancía
    '%    nRate       - Porcentaje a aplicar para obtener la prima
    Private Function Update(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nClassMerch As Short, ByVal nPacking As Short, ByVal nRate As Double) As Boolean
        Dim lclstar_tr_mer As eRemoteDB.Execute


        lclstar_tr_mer = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updtar_tr_mer'. Generated on 30/08/2004 02:28:11 p.m.
        With lclstar_tr_mer
            .StoredProcedure = "insupdtar_tr_mer"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassMerch", nClassMerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        lclstar_tr_mer = Nothing

        Exit Function
    End Function

    '**%Objective: Delete a registry the table "tar_tr_mer" using the key for this table.
    '**%Parameters:
    '**%    nBranch     - Code of the commercial branch.
    '**%    nProduct    - Code of the product.
    '**%    nCurrency   - Code of the currency.
    '**%    dEffecdate  - Date which from the record is valid.
    '**%    nClassMerch - Classification of the merchandise
    '**%    nUsercode   - The user code
    '%Objetivo: Elimina un registro a la tabla "tar_tr_mer" usando la clave para dicha tabla.
    '%Parámetros:
    '%    nBranch     - Codigo del ramo comercial.
    '%    nProduct    - Codigo del producto.
    '%    nCurrency   - Código de la moneda.
    '%    dEffecdate  - Fecha de efecto del registro.
    '%    nClassMerch - Clase de mercancía asegurada
    '%    nUsercode   - Código del usuario
    Private Function Delete(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nPacking As Short, ByVal nClassMerch As Short, ByVal nUsercode As Integer) As Boolean
        Dim lclstar_tr_mer As eRemoteDB.Execute


        lclstar_tr_mer = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.deltar_tr_mer'. Generated on 30/08/2004 02:28:11 p.m.
        With lclstar_tr_mer
            .StoredProcedure = "deltar_tr_mer"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassMerch", nClassMerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

        lclstar_tr_mer = Nothing

        Exit Function
    End Function

    '**%Objective: It verifies the existence of a registry in table "tar_tr_mer" using the key of this table.
    '**%Parameters:
    '**%    nBranch     - Code of the commercial branch.
    '**%    nProduct    - Code of the product.
    '**%    nClassMerch - Classification of the merchandise
    '**%    nPacking    - Packing code associated with the merchandise
    '**%    nCurrency   - Code of the currency.
    '**%    dEffecdate  - Date which from the record is valid.
    '%Objetivo: Verifica la existencia de un registro en la tabla "tar_tr_mer" usando la clave de dicha tabla.
    '%Parámetros:
    '%    nBranch     - Codigo del ramo comercial.
    '%    nProduct    - Codigo del producto.
    '%    nClassMerch - Clase de mercancía asegurada
    '%    nPacking    - Corresponde al tipo de embalaje posible de utilización en el transporte de mercancía
    '%    nCurrency   - Código de la moneda.
    '%    dEffecdate  - Fecha de efecto del registro.
    Private Function IsExist(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nClassMerch As Short, ByVal nPacking As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date) As Boolean
        Dim lclstar_tr_mer As eRemoteDB.Execute
        Dim lintExist As Short


        lclstar_tr_mer = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valtar_tr_merExist'. Generated on 30/08/2004 02:28:11 p.m.
        With lclstar_tr_mer
            .StoredProcedure = "reatar_tr_mer_v"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClassMerch", nClassMerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclstar_tr_mer = Nothing

        Exit Function
    End Function

    '**%Objective: Validation of the data for the page of the headed one.
    '**%Parameters:
    '**%    sCodispl    - Code of the page.
    '**%    nMaisAction - Action of the Menu.
    '**%    sAction     - Action of grid.
    '**%    nBranch     - Code of the commercial branch.
    '**%    nProduct    - Code of the product.
    '**%    nCurrency   - Code of the currency.
    '**%    dEffecdate  - Date which from the record is valid.
    '%Objetivo: Validación de los datos para la página del encabezado.
    '%Parámetros:
    '%    sCodispl    - Códigi de la página
    '%    nMaisAction - Acción del Menu.
    '%    sAction     - Acción del grid.
    '%    nBranch     - Codigo del ramo comercial.
    '%    nProduct    - Codigo del producto.
    '%    nCurrency   - Código de la moneda.
    '%    dEffecdate  - Fecha de efecto del registro.
    Public Function InsValMTR001_k(ByVal sCodispl As String, _
                                   ByVal nMainAction As Integer, _
                                   ByVal sAction As String, _
                                   ByVal nBranch As Short, _
                                   ByVal nProduct As Short, _
                                   ByVal nCurrency As Short, _
                                   ByVal dEffecDate As Date) As String
        Dim lclsErrors As eFunctions.Errors


        lclsErrors = New eFunctions.Errors

        If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 1022)
        End If
        If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 11009)
        End If
        If (nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 7132)
        End If
        If dEffecDate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 11198)
        Else
            If dEffecDate < Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 10868)
            End If
        End If

        If IsExist(eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dEffecDate) Then
            Call lclsErrors.ErrorMessage(sCodispl, 10869)
        End If

        InsValMTR001_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    sCodispl    - Code of the page.
    '**%    nMaisAction - Action of the Menu.
    '**%    sAction     - Action of grid.
    '**%    nBranch     - Code of the commercial branch.
    '**%    nProduct    - Code of the product.
    '**%    nCurrency   - Code of the currency.
    '**%    dEffecdate  - Date which from the record is valid.
    '**%    nClassMerch - Classification of the merchandise
    '**%    nPacking    - Packing code associated with the merchandise
    '**%    nRate       - Rate (o/oo) to be applied to obtain the premium
    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    sCodispl    - Códigi de la página
    '%    nMaisAction - Acción del Menu.
    '%    sAction     - Acción del grid.
    '%    nBranch     - Codigo del ramo comercial.
    '%    nProduct    - Codigo del producto.
    '%    nCurrency   - Código de la moneda.
    '%    dEffecdate  - Fecha de efecto del registro.
    '%    nClassMerch - Clase de mercancía asegurada
    '%    nPacking    - Corresponde al tipo de embalaje posible de utilización en el transporte de mercancía
    '%    nRate       - Porcentaje a aplicar para obtener la prima
    Public Function InsValMTR001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nClassMerch As Short, ByVal nPacking As Short, ByVal nRate As Double) As String
        Dim lclsErrors As eFunctions.Errors


        lclsErrors = New eFunctions.Errors

        If (nClassMerch = 0 Or nClassMerch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3280)
        End If
        If (nPacking = 0 Or nPacking = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3447)
        End If
        If (nRate = 0 Or nRate = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 2042)
        End If

        If sAction = "Add" And IsExist(nBranch, nProduct, nClassMerch, nPacking, nCurrency, dEffecDate) Then
            Call lclsErrors.ErrorMessage(sCodispl, 10284)
        End If

        InsValMTR001 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    nHeader     - Indicator of the page (headed or it details)
    '**%    sCodispl    - Code of the page.
    '**%    nMaisAction - Action of the Menu.
    '**%    sAction     - Action of grid.
    '**%    nUsercode   - The user code
    '**%    nBranch     - Code of the commercial branch.
    '**%    nProduct    - Code of the product.
    '**%    nCurrency   - Code of the currency.
    '**%    dEffecdate  - Date which from the record is valid.
    '**%    nClassMerch - Classification of the merchandise
    '**%    nPacking    - Packing code associated with the merchandise
    '**%    nRate       - Rate (o/oo) to be applied to obtain the premium
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    nHeader     - Indicador de la página (encabezado o detalle).
    '%    sCodispl    - Códigi de la página
    '%    nMaisAction - Acción del Menu.
    '%    sAction     - Acción del grid.
    '%    nUsercode   - Código del usuario
    '%    nBranch     - Codigo del ramo comercial.
    '%    nProduct    - Codigo del producto.
    '%    nCurrency   - Código de la moneda.
    '%    dEffecdate  - Fecha de efecto del registro.
    '%    nClassMerch - Clase de mercancía asegurada
    '%    nPacking    - Corresponde al tipo de embalaje posible de utilización en el transporte de mercancía
    '%    nRate       - Porcentaje a aplicar para obtener la prima
    Public Function InsPostMTR001(ByVal nHeader As Boolean, _
                                  ByVal sCodispl As String, _
                                  ByVal nMainAction As Integer, _
                                  ByVal sAction As String, _
                                  ByVal nUsercode As Integer, _
                                  ByVal nBranch As Short, _
                                  ByVal nProduct As Short, _
                                  ByVal nCurrency As Short, _
                                  ByVal dEffecDate As Date, _
                                  ByVal nClassMerch As Short, _
                                  ByVal nPacking As Short, _
                                  ByVal nRate As Double) As Boolean
        

        If nHeader Then
            InsPostMTR001 = True
        Else
            If sAction = "Add" Or sAction = "Update" Then
                InsPostMTR001 = Update(nUsercode, nBranch, nProduct, nCurrency, dEffecDate, nClassMerch, nPacking, nRate)
            ElseIf sAction = "Del" Then
                InsPostMTR001 = Delete(nBranch, nProduct, nCurrency, dEffecDate, nPacking, nClassMerch, nUsercode)
            End If
        End If

        Exit Function
    End Function
End Class











