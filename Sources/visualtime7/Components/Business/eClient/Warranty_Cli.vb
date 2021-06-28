Imports eRemoteDB.Parameter


Public Class Warranty_Cli

    Public Property nNoteNum As Integer

    Public Function FindNoteNum(ByVal sClient As String, ByVal sDocWarranty_Cli As String, ByVal nTypeWarranty_Cli As Integer) As Boolean
        Dim rdb As New eRemoteDB.Execute
        Dim result As Boolean = False
        rdb.SQL = "SELECT  WARRANTY_CLI.NTYPEWARRANTY, WARRANTY_CLI.SDOCWARRANTY, WARRANTY_CLI.NCURRENCY, " &
                         " WARRANTY_CLI.NCAPACITY    , WARRANTY_CLI.DMATURITY   , WARRANTY_CLI.NNOTENUM , " &
                         " WARRANTY_CLI.SCLIENT " &
                  "  FROM    Warranty_Cli Warranty_Cli  " &
                  " WHERE  SCLIENT = :SCLIENT " &
                  "   AND   SDOCWARRANTY  = :SDOCWARRANTY_CLI " &
                  "   AND   NTYPEWARRANTY = :NTYPEWARRANTY_CLI "

        rdb.Parameters.Add("SCLIENT", sClient, eRmtDataDir.rdbParamInput, eRmtDataType.rdbVarchar, 14, 0, 0, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("SDOCWARRANTY_CLI", sDocWarranty_Cli, eRmtDataDir.rdbParamInput, eRmtDataType.rdbVarchar, 14, 0, 0, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("NTYPEWARRANTY_CLI", nTypeWarranty_Cli, eRmtDataDir.rdbParamInput, eRmtDataType.rdbInteger, 14, 0, 0, eRmtDataAttrib.rdbParamNullable)
        If rdb.Run(True) Then
            result = True
            Do While Not rdb.EOF
                Me.nNoteNum = CInt(rdb.FieldToClass("nNotenum"))
            Loop
        Else
            result = False
        End If
        Return result
    End Function

    Public Function UpdateNoteNum(ByVal sClient As String, ByVal sDocWarranty_Cli As String, ByVal nTypeWarranty_Cli As Integer, ByVal nNoteNum As Integer, ByVal nUserCode As Integer) As Boolean

        Dim rdb = New eRemoteDB.Execute
        Dim mobjValues As New eFunctions.Values

        rdb.SQL = "UPDATE Warranty_Cli SET  NNOTENUM = :NNOTENUM , DCOMPDATE = SYSDATE , NUSERCODE = :NUSERCODE WHERE NTYPEWARRANTY = :NTYPEWARRANTY AND SDOCWARRANTY = :SDOCWARRANTY  AND SCLIENT = :SCLIENT"

        rdb.Parameters.Add("NNOTENUM", nNoteNum, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("NUSERCODE", nUserCode, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("NTYPEWARRANTY", nTypeWarranty_Cli, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("SDOCWARRANTY", sDocWarranty_Cli, eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 14, 0, 0, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("SCLIENT", sClient, eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 14, 0, 0, eRmtDataAttrib.rdbParamNullable)

        Return rdb.Run(False)

    End Function

End Class
