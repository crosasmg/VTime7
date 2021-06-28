Option Strict Off
Option Explicit On
Public Class EndorsLetters
	'**+Objective: Class that supports the table 'EndorsLetters'.
	'**+Version: $$Revision: $
	'+Objetivo: Clase que le da soporte a la tabla 'EndorsLetters'.
	'+Version: $$Revision: $
	
	'**-Objective: Type of endorsement which a letter template will be associated to.
	'-Objetivo: Tipo de endoso asociado a una carta.
    Public nEndorseType As Double
	
	'**-Objective: Number identifying the letter template selected.
	'-Objetivo: Número que identidica el template de carta asociada.
    Public nLetterNum As Double
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
    Public nUsercode As Double
	
	'**-Objective: Type of endorsement description
	'-Objetivo: Descripción del tipo de endoso
	Public sDescriptTable3012 As String
	
	'**-Objective: Description of the letter template.
	'-Objetivo: Descripción del template de carta.
	Public sDescriptTab_Letter As String
	
	'**-Objective:
	'-Objetivo:
    Public nLanguage As Double
	
	'**-Objective:
	'-Objetivo:
	Public tLetters As String
	
	
	'**%Objective: This method updates or adds a record into the table "EndorsLetters"
	'**%Parameters:
	'**%    nUsercode    - Code of the user that creates or updates the record.
	'**%    nEndorseType - Type of endorsement which a letter template will be associated to.
	'**%    nLetterNum   - Number identifying the letter template selected.
	'%Objetivo: Este método permite agregar o actualizar un registro en la tabla "EndorsLetters"
	'%Parámetros:
	'%    nUsercode    - Código del usuario que crea o actualiza el registro.
	'%    nEndorseType - Tipo de endoso asociado a una carta.
	'%    nLetterNum   - Número que identidica el template de carta asociada.
    Private Function Add(ByVal nUsercode As Double, ByVal nEndorseType As Double, ByVal nLetterNum As Double) As Boolean

        Dim lclsEndorsLetters As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lclsEndorsLetters = New eRemoteDB.Execute

        With lclsEndorsLetters
            .StoredProcedure = "creEndorsLetters"
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndorseType", nEndorseType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With
        lclsEndorsLetters = Nothing

        Exit Function
        ObjectRelease = lclsEndorsLetters
    End Function
	
	'**%Objective: Deletes a record from the table "EndorsLetters" by using the table's key.
	'**%Parameters:
	'**%    nEndorseType - Type of endorsement which a letter template will be associated to.
	'**%    nLetterNum   - Number identifying the letter template selected.
	'%Objetivo: Este método permite eliminar un registro de la tabla "EndorsLetters" a través de la clave de dicha tabla.
	'%Parámetros:
	'%    nEndorseType - Tipo de endoso asociado a una carta.
	'%    nLetterNum   - Número que identidica el template de carta asociada.
    Private Function Delete(ByVal nEndorseType As Double, ByVal nLetterNum As Double) As Boolean

        Dim lclsEndorsLetters As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lclsEndorsLetters = New eRemoteDB.Execute

        With lclsEndorsLetters
            .StoredProcedure = "delEndorsLetters"
            .Parameters.Add("nEndorseType", nEndorseType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With
        lclsEndorsLetters = Nothing

        Exit Function
        ObjectRelease = lclsEndorsLetters
    End Function
	
	'**%Objective: Verifies the existence of a record in table "EndorsLetters" using the key.
	'**%Parameters:
	'**%    nEndorseType - Type of endorsement which a letter template will be associated to.
	'**%    nLetterNum   - Number identifying the letter template selected.
	'%Objetivo: Esta función verifica la existencia de un registro en la tabla "EndorsLetters" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nEndorseType - Tipo de endoso asociado a una carta.
	'%    nLetterNum   - Número que identidica el template de carta asociada.
    Private Function IsExist(ByVal nEndorseType As Double, ByVal nLetterNum As Double) As Boolean

        Dim lclsEndorsLetters As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lclsEndorsLetters = New eRemoteDB.Execute

        With lclsEndorsLetters
            .StoredProcedure = "reaEndorsLetters_v"
            .Parameters.Add("nExist", intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndorseType", nEndorseType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsEndorsLetters = Nothing

        Exit Function
        ObjectRelease = lclsEndorsLetters
    End Function
	
	'**%Objective: Validates the data from the header section of the page being processed.
	'**%Parameters:
	'**%    sCodispl     - Code of the window (logic).
	'**%    nEndorseType - Type of endorsement which a letter template will be associated to.
	'**%    nLetterNum   - Number identifying the letter template selected.
	'%Objetivo: Esta función valida los datos del encabezado de la página en tratamiento.
	'%Parámetros:
	'%    sCodispl     - Código identificativo de la ventana (lógico).
	'%    nEndorseType - Tipo de endoso asociado a una carta.
	'%    nLetterNum   - Número que identidica el template de carta asociada.
    Public Function InsValLT970_K(ByVal sCodispl As String, ByVal nEndorseType As Double, ByVal nLetterNum As Double) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsLetters As eLetter.Letters

        If Not IsIDEMode() Then
        End If

        lclsErrors = New eFunctions.Errors
        lclsLetters = New eLetter.Letters

        With lclsErrors

            If nEndorseType = intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 30239)
            End If

            If nLetterNum = intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 8075)
            Else
                If Not lclsLetters.FindTab_Letters(nLetterNum) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 8048)
                End If
            End If

            If IsExist(nEndorseType, nLetterNum) Then
                Call lclsErrors.ErrorMessage(sCodispl, 30275)
            End If

            InsValLT970_K = .Confirm

        End With

        lclsErrors = Nothing
        lclsLetters = Nothing

        Exit Function
        ObjectRelease = lclsErrors
    End Function
	
	'**%Objective: Sends the information necessary to update the records in the database.
	'**%Parameters:
	'**%    sAction      - It indicates the type of action to be applied in the table ("Add" or "Del")
	'**%    nUsercode    - Code of the user creating or updating the record.
	'**%    nEndorseType - Type of endorsement which a letter template will be associated to.
	'**%    nLetterNum   - Number identifying the letter template selected.
	'%Objetivo: Esta función permite enviar la información necesaria de los registros en tratamiento a la base de datos para su
	'% posterior actualización.
	'%Parámetros:
	'%    sAction      - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar" o "Eliminar").
	'%    nUsercode    - Código del usuario que crea o actualiza el registro.
	'%    nEndorseType - Tipo de endoso asociado a una carta.
	'%    nLetterNum   - Número que identidica el template de carta asociada.
    Public Function InsPostLT970(ByVal sAction As String, ByVal nUsercode As Integer, ByVal nEndorseType As Double, ByVal nLetterNum As Double) As Boolean
        If Not IsIDEMode() Then
        End If

        Select Case sAction
            Case "Add"
                InsPostLT970 = Add(nUsercode, nEndorseType, nLetterNum)
            Case "Del"
                InsPostLT970 = Delete(nEndorseType, nLetterNum)
        End Select

        Exit Function
    End Function
End Class











