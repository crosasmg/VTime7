Option Strict Off
Option Explicit On

Imports Oracle.DataAccess.Client ' ODP.NET Oracle managed provider
Imports Oracle.DataAccess.Types  ' ODP.NET Oracle managed provider
Imports System.IO


Public Class Images

    Public sDescript
    Public dCompdate
    Public dNulldate
    Public nRectype
    Public nUsercode
    Public iImage
    Private mstrServerName As String
    Private mstrDataBase As String
    Private mstrUser As String
    Private mstrPassword As String

    Public Function UpdateImage(ByVal nImagenum, ByVal nConsec, ByVal nRectype, ByVal nUsercode, ByVal sDescript, ByVal sSource, ByVal dNulldate) As Boolean
        Dim Conxn1 As OracleConnection
        Dim strConString As String  
        Dim tempBuff As Byte()
        Dim tx As OracleTransaction
        Dim cmd As OracleCommand
        Dim tempLob As OracleBlob

        UpdateImage = True

        If Not String.IsNullOrEmpty(sSource) Then

            LoadConnectionSettings(Nothing, "ImagesDB")

            'Initiate connection with oracle
            strConString = "User ID=" & mstrUser & ";Password=" & mstrPassword & ";Data Source=" & mstrDataBase
            Conxn1 = New OracleConnection(strConString)
            'Open the connection
            Conxn1.Open()

            iImage = Me.GetImage(sSource)

            tempBuff = iImage

            tx = Conxn1.BeginTransaction()

            cmd = Conxn1.CreateCommand()

            'PL/SQL to read blob data
            cmd.CommandText = "declare xx blob; begin " & _
                              "dbms_lob.createtemporary(xx, false, 0);" & _
                              " :tempblob := xx; end;"

            cmd.Parameters.Add(New OracleParameter("tempblob", OracleDbType.Blob)).Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()

            tempLob = cmd.Parameters.Item(0).Value

            tempLob.BeginChunkWrite()
            tempLob.Write(tempBuff, 0, tempBuff.Length)
            tempLob.EndChunkWrite()

            cmd.Parameters.Clear()
            cmd.CommandText = "UpdImage"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New OracleParameter("nImageNum", OracleDbType.Long)).Value = nImagenum
            cmd.Parameters.Add(New OracleParameter("nConsec", OracleDbType.Long)).Value = nConsec
            cmd.Parameters.Add(New OracleParameter("sDescript", OracleDbType.Char)).Value = sDescript
            cmd.Parameters.Add(New OracleParameter("image_data", OracleDbType.Blob)).Value = tempLob
            cmd.Parameters.Add(New OracleParameter("dNulldate", OracleDbType.Date)).Value = dNulldate
            cmd.Parameters.Add(New OracleParameter("nRecType", OracleDbType.Long)).Value = nRectype
            cmd.Parameters.Add(New OracleParameter("nUsercode", OracleDbType.Long)).Value = nUsercode
            'Execute the command to database
            cmd.ExecuteNonQuery()
            'Commit the transaction
            tx.Commit()

            Conxn1.Close()
        End If
    End Function

    Public Function FindImage(ByVal v1 As Object, ByVal v2 As Object) As Boolean
        Dim lrecreaImage As eRemoteDB.Execute

        'On Error GoTo ErrorHandler
        lrecreaImage = New eRemoteDB.Execute

        With lrecreaImage
            .StoredProcedure = "reaImage"
            .Parameters.Add("nImagenum", v1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsec", v2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                FindImage = True
                'nImagenum = Imagenum
                'nConsec = Consec
                sDescript = .FieldToClass("sDescript", intNull)
                dCompdate = .FieldToClass("dCompdate", dtmNull)
                dNulldate = .FieldToClass("dNulldate", dtmNull)
                nRectype = .FieldToClass("nRectype", intNull)
                nUsercode = .FieldToClass("nUsercode", intNull)
                Dim tempArr() As Byte = .FieldToClass("iImage", strNull)
                iImage = .FieldToClass("iImage", strNull)

                .RCloseRec()
            Else
                FindImage = False
            End If
        End With

        lrecreaImage = Nothing

        Exit Function
    End Function

    Public Function AddTextClob(ByVal sFilePath As String, ByVal sFileName As String, ByVal sKey As String, ByVal nId As Long, ByVal nUsercode As Long, ByVal sTypeProcess As String) As Boolean
        Dim Conxn1 As OracleConnection
        Dim strConString As String
        Dim tx As OracleTransaction
        Dim cmd As OracleCommand
        Dim tempClob As OracleClob
        Dim tmpBuff() As Byte

        On Error GoTo AddTextClob_Err

        If Not String.IsNullOrEmpty(sFilePath) Then
            LoadConnectionSettings(Nothing, "ImagesDB")
            'Initiate connection with oracle
            strConString = "User ID=" & mstrUser & ";Password=" & mstrPassword & ";Data Source=" & mstrDataBase
            Conxn1 = New OracleConnection(strConString)
            'Open the connection
            Conxn1.Open()

            Dim sr As New System.IO.StreamReader(sFilePath)
            tmpBuff = System.Text.Encoding.Unicode.GetBytes(sr.ReadToEnd())
            sr.Close()

            tx = Conxn1.BeginTransaction()
            cmd = Conxn1.CreateCommand()

            'to read clob data
            cmd.CommandText = "declare xx clob; begin " & _
                              "dbms_lob.createtemporary(xx, false, 0);" & _
                             " :tempclob := xx; end;"
            cmd.Parameters.Add(New OracleParameter("tempclob", OracleDbType.Clob)).Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()

            tempClob = cmd.Parameters.Item(0).Value
            tempClob.BeginChunkWrite()
            tempClob.Write(tmpBuff, 0, tmpBuff.Length)
            tempClob.EndChunkWrite()

            cmd.Parameters.Clear()
            cmd.CommandText = "INST_FILE_MASSIVECHARGE"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New OracleParameter("SKEY", OracleDbType.Varchar2)).Value = sKey
            cmd.Parameters.Add(New OracleParameter("NID", OracleDbType.Long)).Value = nId
            cmd.Parameters.Add(New OracleParameter("SFILENAME", OracleDbType.Varchar2)).Value = sFileName
            cmd.Parameters.Add(New OracleParameter("STEXT", OracleDbType.Clob)).Value = tempClob
            cmd.Parameters.Add(New OracleParameter("STYPE", OracleDbType.Char, 1)).Value = sTypeProcess
            cmd.Parameters.Add(New OracleParameter("NUSERCODE", OracleDbType.Long)).Value = nUsercode
            'Execute the command to database
            cmd.ExecuteNonQuery()
            'Commit the transaction
            tx.Commit()
            Conxn1.Close()

            AddTextClob = True
        End If

AddTextClob_Err:
        If Err.Number Then
            AddTextClob = False
        End If
        On Error GoTo 0

    End Function

    Public Function AddImage(ByVal nImagenum, ByVal nConsec, ByVal nRectype, ByVal nUsercode, ByVal sDescript, ByVal sSource, ByVal dNulldate) As Boolean
        Dim Conxn1 As OracleConnection
        Dim tempBuff As Byte()
        Dim strConString As String
        Dim tx As OracleTransaction
        Dim cmd As OracleCommand
        Dim tempLob As OracleBlob

        AddImage = True

        If Not String.IsNullOrEmpty(sSource) Then

            LoadConnectionSettings(Nothing, "ImagesDB")

            'Initiate connection with oracle
            strConString = "User ID=" & mstrUser & ";Password=" & mstrPassword & ";Data Source=" & mstrDataBase
            Conxn1 = New OracleConnection(strConString)
            'Open the connection
            Conxn1.Open()

            iImage = Me.GetImage(sSource)

            tempBuff = iImage

            tx = Conxn1.BeginTransaction()

            cmd = Conxn1.CreateCommand()

            'PL/SQL to read blob data
            cmd.CommandText = "declare xx blob; begin " & _
                              "dbms_lob.createtemporary(xx, false, 0);" & _
                              " :tempblob := xx; end;"

            cmd.Parameters.Add(New OracleParameter("tempblob", OracleDbType.Blob)).Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()

            tempLob = cmd.Parameters.Item(0).Value

            tempLob.BeginChunkWrite()
            tempLob.Write(tempBuff, 0, tempBuff.Length)
            tempLob.EndChunkWrite()

            cmd.Parameters.Clear()
            cmd.CommandText = "CreImageDN"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New OracleParameter("nImageNum", OracleDbType.Long)).Value = nImagenum
            cmd.Parameters.Add(New OracleParameter("nConsec", OracleDbType.Long)).Value = nConsec
            cmd.Parameters.Add(New OracleParameter("sDescript", OracleDbType.Char)).Value = sDescript
            cmd.Parameters.Add(New OracleParameter("image_data", OracleDbType.Blob)).Value = tempLob
            cmd.Parameters.Add(New OracleParameter("dNulldate", OracleDbType.Date)).Value = dNulldate
            cmd.Parameters.Add(New OracleParameter("nRecType", OracleDbType.Long)).Value = nRectype
            cmd.Parameters.Add(New OracleParameter("nUsercode", OracleDbType.Long)).Value = nUsercode
            'Execute the command to database
            cmd.ExecuteNonQuery()
            'Commit the transaction
            tx.Commit()

            Conxn1.Close()

        End If

    End Function

    '**%Objective: Reads a image file (specified in source) and returns a byte array containing the file binary info.
    '%Objetivo: Lee un archivo de imagen (especificado en source) y devuelve un array de bytes que contiene la información
    '%          binaria del archivo.
    Private Function GetImage(ByVal source As String) As Byte()
        Dim file As New FileStream(source, FileMode.Open)
        Dim fileBytes(file.Length) As Byte

        file.Read(fileBytes, 0, file.Length)

        Return fileBytes
    End Function

    Private Sub LoadConnectionSettings(ByRef ConfigSettings As eRemoteDB.VisualTimeConfig, Optional ByVal sGroup As String = "Database")
        If ConfigSettings Is Nothing Then
            ConfigSettings = New eRemoteDB.VisualTimeConfig
        End If

        '**+ Gets the Server in use
        '+ Se obtiene el Servidor con el cual se está trabajando.
        With ConfigSettings
            mstrDataBase = .LoadSetting("Database", "Not Database", sGroup)
            mstrUser = .LoadSetting("sInitials", "Not User", sGroup, True)
            mstrPassword = .LoadSetting("sAccessWo", "Not Password", sGroup, True)
        End With
    End Sub
End Class
