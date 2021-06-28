Option Strict Off
Option Explicit On

Public Class Folios_comp

    Public nYear As Integer
    Public nStart As Long
    Public nEnd As Long
    Public sStatregt As String
    Public nUsercode As Integer

    '% Find: 
    Public Function Find(ByVal nYear As Integer, ByVal nStart As Long) As Boolean
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '- Se define la variable lrecreaFolios_comp
        Dim lrecreaFolios_comp As eRemoteDB.Execute

        lrecreaFolios_comp = New eRemoteDB.Execute

        With lrecreaFolios_comp
            .StoredProcedure = "reaFolios_comp"
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                nYear = .FieldToClass("nYear")
                nStart = .FieldToClass("nStart")
                nEnd = .FieldToClass("nEnd")
                sStatregt = .FieldToClass("sStatregt")

                .RCloseRec()
                lblnRead = True
            Else
                lblnRead = False
            End If
        End With

        Find = lblnRead
        lrecreaFolios_comp = Nothing
    End Function

    '% ValExistFolios_comp: Valida que el rango incluida no exista en otro rango (Folios_comp)
    Public Function ValExistFolios_comp(ByVal nYear As Integer, ByVal nStart As Long, ByVal nEnd As Long) As Boolean

        Dim lrecreaFolios_comp_v1 As eRemoteDB.Execute
        lrecreaFolios_comp_v1 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaFolios_comp_v1'
        '+ Información leída el 06/07/2001 09:51:44 a.m.
        With lrecreaFolios_comp_v1
            ' Esta pendiente
            .StoredProcedure = "reaFolios_comp_v1"
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd", nEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                If .Parameters("nYear").Value > 0 Then
                    ValExistFolios_comp = True
                Else
                    ValExistFolios_comp = False
                End If

                .RCloseRec()

            Else
                ValExistFolios_comp = False
            End If
        End With

        lrecreaFolios_comp_v1 = Nothing
    End Function

    '% Add: Agrega un registro a la tabla de Folios asignados a la compañía (Folios_comp)
    Public Function Add() As Boolean
        Dim lreccreFolios_comp As eRemoteDB.Execute

        On Error GoTo Add_err

        lreccreFolios_comp = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.creFolios_comp'
        '+ Información leída el 06/07/2001 05:37:41 p.m.
        With lreccreFolios_comp
            .StoredProcedure = "creFolios_comp"
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd", nEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0
        lreccreFolios_comp = Nothing
    End Function

    '% Update : Actualiza un registro en la tabla de Localidades (Folios_comp)
    Public Function Update() As Boolean
        Dim lrecupdFolios_comp As eRemoteDB.Execute

        On Error GoTo Update_err

        lrecupdFolios_comp = New eRemoteDB.Execute

        With lrecupdFolios_comp
            .StoredProcedure = "updFolios_comp"
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd", nEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

Update_err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        lrecupdFolios_comp = Nothing
    End Function

    '% Delete: Elimina un registro de la tabla de Localidades (Folios_comp)
    Public Function Delete() As Boolean
        Dim lrecdelFolios_comp As eRemoteDB.Execute

        On Error GoTo Delete_err

        lrecdelFolios_comp = New eRemoteDB.Execute

        With lrecdelFolios_comp
            .StoredProcedure = "delFolios_comp"
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        lrecdelFolios_comp = Nothing
    End Function

    '% insValCA980: Valida los datos introducidos en la página
    '---------------------------------------------------------
    Public Function insValCA980(ByVal sCodispl As String, ByVal nYear As Integer, ByVal nStart As Long, ByVal nEnd As Long, ByVal sStatregt As String, ByVal sAction As String) As String
        '---------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValCA980_Err

        lclsErrors = New eFunctions.Errors

        'Debe indicar el año.
        If nYear <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 60338)
        ElseIf nYear <> Today.Year Then
            Call lclsErrors.ErrorMessage(sCodispl, 90000074)
        End If

        'Incluya el rango inicial
        If nStart <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 10247)
        End If

        'Incluya el rango final
        If nEnd <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 10248)
        End If

        'Debe indicar el estado
        If String.IsNullOrEmpty(sStatregt) Or sStatregt = "0" Then
            Call lclsErrors.ErrorMessage(sCodispl, 55633)
        End If

        ' Rango final inferior al rango inicial  
        If nStart > 0 And nEnd > 0 Then
            If nEnd < nStart Then
                Call lclsErrors.ErrorMessage(sCodispl, 10184)
            End If
        End If

        If sAction = "Add" Then
            ' El rango está incluído en otro registro
            If ValExistFolios_comp(nYear, nStart, nEnd) Then
                Call lclsErrors.ErrorMessage(sCodispl, 11138)
            End If
        End If

        insValCA980 = lclsErrors.Confirm

insValCA980_Err:
        If Err.Number Then
            insValCA980 = insValCA980 & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function

    '% insPostCA980: Valida los datos introducidos en la zona de contenido para "frame" especifico
    Public Function insPostCA980(ByVal sAction As String, ByVal nYear As Integer, ByVal nStart As Long, ByVal nEnd As Long, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
        With Me
            .nYear = nYear
            .nStart = nStart
            .nEnd = nEnd
            .sStatregt = sStatregt
            .nUsercode = nUsercode

            Select Case sAction.Trim
                Case "Add"
                    insPostCA980 = Add()
                Case "Del"
                    insPostCA980 = Delete()
                Case "Update"
                    insPostCA980 = Update()
            End Select
        End With

    End Function

End Class
