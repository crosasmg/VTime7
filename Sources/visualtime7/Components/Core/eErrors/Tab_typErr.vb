Option Strict Off
Option Explicit On
Public Class TAB_TYPERR
    '**+Objective: Class that supports the table 'Tab_typerr'.
    '**+Version: $$Revision: 4 $
    '+Objetivo: Clase que le da soporte a la tabla 'Tab_typerr'.
    '+Version: $$Revision: 4 $


    '**-Objective: Type of error
    '-Objetivo: Tipo de error
    Public nType_err As Integer

    '**-Objective: Type of error
    '-Objetivo: Tipo de error
    Public nTypeerr_pa As Integer

    '**-Objective: Description of the type of error
    '-Objetivo: Descripción del tipo de error
    Public sDescript As String

    '**-Objective: Abbreviated description of the tipo of error
    '-Objetivo: Descripción abreviada del tipo de error
    Public sShort_des As String

    '**-Objective: General status of the record.
    '-Objetivo: Estado general del registro
    Public sStatregt As String

    '**-Objective: Transitory type of error indicator.
    '-Objetivo: Indicador de que el tipo de error es transitorio
    Public sTransiti As String

    '**-Objective: Code of the user creating or updating the record.
    '-Objetivo: Código del usuario que crea o actualiza el registro
    Public nUsercode As String



    '**%Objective: This method updates or adds a record into the table "Tab_typerr"
    '**%Parameters:
    '**%    sAction     - The type of action to be executed for the record ("Add" or "Update")
    '**%    nType_err   - Type of error
    '**%    nTypeerr_pa - Type of error
    '**%    sDescript   - Description of the type of error
    '**%    sShort_des  - Abbreviated description of the tipo of error
    '**%    sStatregt   - General status of the record.
    '**%    sTransiti   - Transitory type of error indicator.
    '**%    nUsercode   - Code of the user that creates or updates the record.
    '%Objetivo: Este método permite agregar o actualizar un registro en la tabla "Tab_typerr"
    '%Parámetros:
    '%    sAction     - Indica el tipo de acción a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
    '%    nType_err   - Tipo de error
    '%    nTypeerr_pa - Tipo de error
    '%    sDescript   - Description of the type of error
    '%    sShort_des  - Abbreviated description of the tipo of error
    '%    sStatregt   - General status of the record.
    '%    sTransiti   - Transitory type of error indicator.
    '%    nUsercode   - Código del usuario que crea o actualiza el registro.
    Private Function AddUpdate(ByVal sAction As String, ByVal nType_err As Integer, ByVal nTypeerr_pa As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal sTransiti As String, ByVal nUsercode As Integer) As Boolean
        Dim lclsTab_typerr As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If
        lclsTab_typerr = New eRemoteDB.Execute

        With lclsTab_typerr
            .StoredProcedure = "creupdTab_typerr"
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_err", nType_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeerr_pa", nTypeerr_pa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTransiti", sTransiti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            AddUpdate = .Run(False)
        End With

        lclsTab_typerr = Nothing

        Exit Function
    End Function

    '**%Objective: Deletes a record from the table "Tab_typerr" by using the table's key.
    '**%Parameters:
    '**%    nUsercode   - Code of the user that creates or updates the record.
    '**%    nType_err   - Type of error
    '%Objetivo: Este método permite eliminar un registro de la tabla "Tab_typerr" a través de la clave de dicha tabla.
    '%Parámetros:
    '%    nUsercode   - Código del usuario que crea o actualiza el registro.
    '%    nType_err   - Tipo de error
    Private Function Delete(ByVal nUsercode As Integer, ByVal nType_err As Object) As Boolean
        Dim lclsTab_typerr As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If
        lclsTab_typerr = New eRemoteDB.Execute

        With lclsTab_typerr
            .StoredProcedure = "delTab_typerr"
            .Parameters.Add("nType_err", nType_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete = .Run(False)
        End With

        lclsTab_typerr = Nothing

        Exit Function
    End Function

    '**%Objective: Verifies the existence of a record in table "Tab_typerr" using the key.
    '**%Parameters:
    '**%    nType_err   - Type of error
    '%Objetivo: Esta función verifica la existencia de un registro en la tabla "Tab_typerr" usando la clave de dicha tabla.
    '%Parámetros:
    '%    nType_err   - Tipo de error
    Private Function IsExist(ByVal nType_err As Integer) As Boolean
        Dim lclsTab_typerr As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If
        lclsTab_typerr = New eRemoteDB.Execute

        With lclsTab_typerr
            .StoredProcedure = "reaTab_typerr_v"
            .Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_err", nType_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsTab_typerr = Nothing

        Exit Function
    End Function

    '**%Objective: Validates the data from the detail section of the page being processed.
    '**%Parameters:
    '**%    sCodispl - Code of the window (logical code).
    '**%    sAction     - The type of action to be executed for the record ("Add" or "Update")
    '**%    nType_err   - Type of error
    '**%    nTypeerr_pa - Type of error
    '**%    sDescript   - Description of the type of error
    '**%    sShort_des  - Abbreviated description of the tipo of error
    '**%    sStatregt   - General status of the record.
    '**%    sTransiti   - Transitory type of error indicator.
    '**%    nUsercode   - Code of the user that creates or updates the record.
    '%Objetivo: Esta función permite validar los datos del detalle de la página en tratamiento.
    '%Parámetros:
    '%    sCodispl - Código de la ventana (lógico).
    '%    sAction     - Indica el tipo de acción a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
    '%    nType_err   - Tipo de error
    '%    nTypeerr_pa - Tipo de error
    '%    sDescript   - Description of the type of error
    '%    sShort_des  - Abbreviated description of the tipo of error
    '%    sStatregt   - General status of the record.
    '%    sTransiti   - Transitory type of error indicator.
    '%    nUsercode   - Código del usuario que crea o actualiza el registro.
    Public Function InsValMER001(ByVal sCodispl As String, ByVal sAction As String, ByVal nType_err As Integer, ByVal nTypeerr_pa As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal sTransiti As String, ByVal nUsercode As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        If Not IsIDEMode() Then
        End If
        lclsErrors = New eFunctions.Errors
        With lclsErrors

            If (nType_err = eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 20021)
            Else
                If sAction = "Add" And IsExist(nType_err) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 20002)
                End If
            End If

            If sDescript = "" Then
                Call lclsErrors.ErrorMessage(sCodispl, 20021)
            End If

            If sShort_des = "" Then
                Call lclsErrors.ErrorMessage(sCodispl, 20021)
            End If

            If nTypeerr_pa = nType_err Then
                Call lclsErrors.ErrorMessage(sCodispl, 20005)
            End If

            InsValMER001 = .Confirm
        End With

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: Sends the information necessary to update the records in the database.
    '**%Parameters:
    '**%    sAction     - The type of action to be executed for the record ("Add" or "Update")
    '**%    sCodispl    - Code of the window (logical code).
    '**%    nType_err   - Type of error
    '**%    nTypeerr_pa - Type of error
    '**%    sDescript   - Description of the type of error
    '**%    sShort_des  - Abbreviated description of the tipo of error
    '**%    sStatregt   - General status of the record.
    '**%    sTransiti   - Transitory type of error indicator.
    '**%    nUsercode   - Code of the user that creates or updates the record.
    '**%    <__PARAMETER_LIST_DESC__>
    '%Objetivo: Esta función permite enviar la información necesaria de los registros en tratamiento a la base de datos para su
    '% posterior actualización.
    '%Parámetros:
    '%    sAction     - Indica el tipo de acción a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
    '%    sCodispl - Código de la ventana (lógico).
    '%    nType_err   - Tipo de error
    '%    nTypeerr_pa - Tipo de error
    '%    sDescript   - Description of the type of error
    '%    sShort_des  - Abbreviated description of the tipo of error
    '%    sStatregt   - General status of the record.
    '%    sTransiti   - Transitory type of error indicator.
    '%    nUsercode   - Código del usuario que crea o actualiza el registro.
    Public Function InsPostMER001(ByVal sAction As String, ByVal sCodispl As String, ByVal nType_err As Integer, ByVal nTypeerr_pa As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal sTransiti As String, ByVal nUsercode As Integer) As Boolean
        If Not IsIDEMode() Then
        End If

        Select Case sAction
            Case "Add", "Update"
                InsPostMER001 = AddUpdate(sAction, nType_err, nTypeerr_pa, sDescript, sShort_des, sStatregt, sTransiti, nUsercode)
            Case "Del"
                InsPostMER001 = Delete(nUsercode, nType_err)
        End Select

        Exit Function
    End Function
End Class











