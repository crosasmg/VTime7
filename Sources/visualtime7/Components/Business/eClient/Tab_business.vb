Option Strict Off
Option Explicit On
Public Class Tab_business
    '**+Objective: Class that supports the table Execute it's content is:
    '**+Version: $$Revision: 2 $
    '+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
    '+Version: $$Revision: 2 $

    '**+Objective: Properties according to the table 'Tab_business' in the system 11/17/2004 9:19:29 AM
    '+Objetivo: Propiedades según la tabla 'Tab_business' en el sistema 11/17/2004 9:19:29 AM

    '**+Objective:
    '+Objetivo: Codigo del tipo de la compañia
    Public nTypeCompany As Short

    '**+Objective:
    '+Objetivo: Descripción delo que hace la compañia
    Public sDescript As String

    '**+Objective:
    '+Objetivo: descripción abreviada de lo que hace la compañia
    Public sShort_des As String

    '**+Objective:
    '+Objetivo: Indica si es de tipo ruc
    Public sRUC_ind As String

    '**+Objective:
    '+Objetivo: Estado del registro
    Public sStatRegt As String

    '**+Objective:
    '+Objetivo: Verifica si existe un cliente asociado a la compañia
    Public sExist As String



    '**%Objective: Add a record to the table "Tab_business"
    '**%Parameters:
    '**%    nUsercode  -
    '**%    nTypeCompany -
    '**%    sDescript -
    '**%    sShort_des  -
    '**%    sRUC_ind  -
    '**%    sStatRegt -
    '%Objetivo: Agrega un registro a la tabla "Tab_business"
    '%Parámetros:
    '%    nUsercode  - Código del usuario
    '%    nTypeCompany - Codigo del tipo de la compañia
    '%    sDescript - Descripción delo que hace la compañia
    '%    sShort_des  - descripción abreviada de lo que hace la compañia
    '%    sRUC_ind  - Indica si es de tipo ruc
    '%    sStatRegt - Estado del registro
    Private Function Add(ByVal nUsercode As Integer, ByVal nTypeCompany As Short, ByVal sDescript As String, ByVal sShort_des As String, ByVal sRUC_ind As String, ByVal sStatRegt As String) As Boolean
        Dim lclsTab_business As eRemoteDB.Execute

        lclsTab_business = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.creTab_business'. Generated on 11/17/2004 9:19:29 AM

        With lclsTab_business
            .StoredProcedure = "creTab_business"
            .Parameters.Add("nTypeCompany", nTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRUC_ind", IIf(sRUC_ind = String.Empty, "2", "1"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatRegt", sStatRegt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

        lclsTab_business = Nothing

        Exit Function
    End Function

    '**%Objective: Updates a registry to the table "Tab_business" using the key for this table.
    '**%Parameters:
    '**%    nUsercode  -
    '**%    nTypeCompany -
    '**%    sDescript -
    '**%    sShort_des  -
    '**%    sRUC_ind  -
    '**%    sStatRegt -
    '%Objetivo: Actualiza un registro a la tabla "Tab_business" usando la clave para dicha tabla.
    '%Parámetros:
    '%    nUsercode  - Código del usuario
    '%    nTypeCompany - Codigo del tipo de la compañia
    '%    sDescript - Descripción delo que hace la compañia
    '%    sShort_des  - descripción abreviada de lo que hace la compañia
    '%    sRUC_ind  - Indica si es de tipo ruc
    '%    sStatRegt - Estado del registro
    Private Function Update(ByVal nUsercode As Integer, ByVal nTypeCompany As Short, ByVal sDescript As String, ByVal sShort_des As String, ByVal sRUC_ind As String, ByVal sStatRegt As String) As Boolean
        Dim lclsTab_business As eRemoteDB.Execute

        lclsTab_business = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updTab_business'. Generated on 11/17/2004 9:19:29 AM
        With lclsTab_business
            .StoredProcedure = "updTab_business"
            .Parameters.Add("nTypeCompany", nTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRUC_ind", IIf(sRUC_ind = String.Empty, "2", "1"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatRegt", sStatRegt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        lclsTab_business = Nothing

        Exit Function
    End Function

    '**%Objective: Delete a registry the table "Tab_business" using the key for this table.
    '**%Parameters:
    '**%    nTypeCompany -
    '%Objetivo: Elimina un registro a la tabla "Tab_business" usando la clave para dicha tabla.
    '%Parámetros:
    '%    nTypeCompany - Codigo del tipo de la compañia
    Private Function Delete(ByVal nTypeCompany As Short) As Boolean
        Dim lclsTab_business As eRemoteDB.Execute

        lclsTab_business = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.delTab_business'. Generated on 11/17/2004 9:19:29 AM
        With lclsTab_business
            .StoredProcedure = "delTab_business"
            .Parameters.Add("nTypeCompany", nTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

        lclsTab_business = Nothing

        Exit Function
    End Function

    '**%Objective: It verifies the existence of a registry in table "Tab_business" using the key of this table.
    '**%Parameters:
    '**%    nTypeCompany -
    '%Objetivo: Verifica la existencia de un registro en la tabla "Tab_business" usando la clave de dicha tabla.
    '%Parámetros:
    '%    nTypeCompany - Codigo del tipo de la compañia
    Private Function IsExist(ByVal nTypeCompany As Short) As Boolean
        Dim lclsTab_business As eRemoteDB.Execute
        Dim lintExist As Short

        lclsTab_business = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valTab_businessExist'. Generated on 11/17/2004 9:19:29 AM
        With lclsTab_business
            .StoredProcedure = "reaTab_business_v"
            .Parameters.Add("nTypeCompany", nTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsTab_business = Nothing

        Exit Function
    End Function

    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    sCodispl  -
    '**%    nMainAction  -
    '**%    sAction -
    '**%    nTypeCompany -
    '**%    sDescript -
    '**%    sShort_des  -
    '**%    sRUC_ind  -
    '**%    sStatRegt -
    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    sCodispl  - Código de la transacción
    '%    nMainAction  - Numero de la acción a ejecutar
    '%    sAction - Acción a ejecutar
    '%    nTypeCompany - Codigo del tipo de la compañia
    '%    sDescript - Descripción delo que hace la compañia
    '%    sShort_des  - descripción abreviada de lo que hace la compañia
    '%    sRUC_ind  - Indica si es de tipo ruc
    '%    sStatRegt - Estado del registro
    Public Function InsValMBC6001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nTypeCompany As Short, ByVal sDescript As String, ByVal sShort_des As String, ByVal sRUC_ind As String, ByVal sStatRegt As String) As String
        Dim lclsErrors As eFunctions.Errors

        lclsErrors = New eFunctions.Errors

        If (nTypeCompany <> eRemoteDB.Constants.intNull) Then
            If sAction = "Add" And IsExist(nTypeCompany) Then
                Call lclsErrors.ErrorMessage(sCodispl, 12089)
            End If

            If sDescript = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 12080)
            End If

            If sShort_des = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 12079)
            End If

            If sStatRegt = "0" Then
                Call lclsErrors.ErrorMessage(sCodispl, 1922)
            End If
        Else
            If (sDescript <> String.Empty Or sShort_des <> String.Empty Or sRUC_ind = "0" Or sStatRegt <> String.Empty) Then
                Call lclsErrors.ErrorMessage(sCodispl, 1084)
            End If
        End If

        InsValMBC6001 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    pblnHeader  -
    '**%    sCodispl  -
    '**%    nMainAction  -
    '**%    sAction -
    '**%    nUsercode -
    '**%    nTypeCompany -
    '**%    sDescript -
    '**%    sShort_des  -
    '**%    sRUC_ind  -
    '**%    sStatRegt -
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    pblnHeader  -
    '%    sCodispl  - Código de la transacción
    '%    nMainAction  - Numero de la acción a ejecutar
    '%    sAction - Acción a ejecutar
    '%    nUsercode  - Código del usuario
    '%    nTypeCompany - Codigo del tipo de la compañia
    '%    sDescript - Descripción delo que hace la compañia
    '%    sShort_des  - descripción abreviada de lo que hace la compañia
    '%    sRUC_ind  - Indica si es de tipo ruc
    '%    sStatRegt - Estado del registro
    Public Function InsPostMBC6001(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nTypeCompany As Short, ByVal sDescript As String, ByVal sShort_des As String, ByVal sRUC_ind As String, ByVal nStatRegt As Short) As Boolean

        If pblnHeader Then
            InsPostMBC6001 = True
        Else
            If sAction = "Add" Then
                InsPostMBC6001 = Add(nUsercode, nTypeCompany, sDescript, sShort_des, sRUC_ind, CStr(nStatRegt))
            ElseIf sAction = "Update" Then
                InsPostMBC6001 = Update(nUsercode, nTypeCompany, sDescript, sShort_des, sRUC_ind, CStr(nStatRegt))
            ElseIf sAction = "Del" Then
                InsPostMBC6001 = Delete(nTypeCompany)
            End If
        End If

        Exit Function
    End Function

    '**%Objective: This method obtains the information from the table tab_business
    '**%Parameters:
    '**%    nTypecompany - .
    '%Objetivo: Este método realiza la lectura de la información de la tabla en tratamiento tab_business
    '%Parámetros:
    '%    nTypecompany - .
    Public Function Find(ByVal nTypeCompany As Short) As Boolean
        Dim lrecreatab_business As eRemoteDB.Execute

        lrecreatab_business = New eRemoteDB.Execute

        With lrecreatab_business
            .StoredProcedure = "reatab_business"
            .Parameters.Add("nTypeCompany", nTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Find = True
                nTypeCompany = .FieldToClass("nTypeCompany")
                sDescript = .FieldToClass("sDescript")
                sShort_des = .FieldToClass("sShort_des")
                sRUC_ind = .FieldToClass("sRUC_ind")
                sStatRegt = .FieldToClass("sStatRegt")
                .RCloseRec()
            End If
        End With

        lrecreatab_business = Nothing

        Exit Function
    End Function
End Class











