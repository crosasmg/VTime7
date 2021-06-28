Public Class Letter
    Public SQL As String
    Public tletter As Object
    Public Find As Boolean

    Public Function FindtLetter() As Object
        FindtLetter = Nothing
    End Function

    Public Function findLettLanguage(ByVal nLetterNum As Integer, ByVal nLanguage As Integer) As Boolean

        Dim lreaLettLanguage As eRemoteDB.Execute

        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        lreaLettLanguage = New eRemoteDB.Execute

        'On Error GoTo ErrorHandler

        findLettLanguage = True
        With lreaLettLanguage
            .StoredProcedure = "FINDLETTER"
            .Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLanguage", nLanguage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.tletter = .FieldToClass("tLetter")

                .RCloseRec()
            Else
                findLettLanguage = False
            End If
        End With

        Exit Function
ErrorHandler:
        findLettLanguage = Nothing
        ProcError("Letter.findLettLanguage(nLetterNum,nLanguage)", New Object() {nLetterNum, nLanguage})

    End Function
End Class
