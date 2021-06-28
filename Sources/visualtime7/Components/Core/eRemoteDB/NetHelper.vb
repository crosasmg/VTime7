Imports System.Diagnostics

Public Class NetHelper

    ''' <summary>
    ''' Creates an instance of a assembly's class allowing it to be used
    ''' </summary>
    ''' <param name="AssemblyClassName">Assembly and class name</param>
    ''' <returns>Assembly instance</returns>
    Public Shared Function CreateClassInstance(ByVal AssemblyClassName As String) As Object
        Dim AssemblyItem As Reflection.Assembly
        Dim ClassInstance As Object
        Dim AssemblyName As String

        AssemblyName = IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\", "") + "\" + _
                       AssemblyClassName.Substring(0, AssemblyClassName.LastIndexOf(".")) + ".dll"

        If Not IO.File.Exists(AssemblyName) Then
            Err.Raise(-1, "CreateClassInstance", "assembly '" + AssemblyClassName.Substring(0, AssemblyClassName.LastIndexOf(".")) + ".dll" + "' could not be found, wrong assembly name given ")
        End If

        AssemblyItem = Reflection.Assembly.LoadFrom(AssemblyName)
        ClassInstance = AssemblyItem.CreateInstance(AssemblyClassName, False, Reflection.BindingFlags.CreateInstance, Nothing, Nothing, Nothing, Nothing)

        If ClassInstance Is Nothing Then
            Err.Raise(-1, "LoadMeByName", "assembly could not be found, wrong assembly name given " & _
                vbcrlf & "AssemblyClassName=" & AssemblyClassName)
        End If

        Return ClassInstance
    End Function

    ''' <summary>
    ''' Purge all of the files and subfolders in a specific path, according to a pattern 
    ''' </summary>
    ''' <param name="directoryPath">Path to the directory</param>
    ''' <param name="searchPattern">Search pattern</param>
    ''' <param name="withSubDirectory">Indicates if the purge is recursive</param>
    Public Shared Sub PurgeDirectory(ByVal directoryPath As String, ByVal searchPattern As String, Optional ByVal withSubDirectory As Boolean = False)
        Dim arrFiles() As String

        If directoryPath.Length > 0 Then
            If IO.Directory.Exists(directoryPath) Then
                arrFiles = IO.Directory.GetFiles(directoryPath, searchPattern)
                For Each strFileName As String In arrFiles
                    IO.File.Delete(strFileName)
                Next strFileName

                If withSubDirectory Then
                    Dim arrFolders() As String = IO.Directory.GetDirectories(directoryPath)

                    For Each strFolderName As String In arrFolders
                        PurgeDirectory(strFolderName, searchPattern, withSubDirectory)
                        IO.Directory.Delete(strFolderName)
                    Next
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Write Entry to Event Log using VB.NET
    ''' </summary>
    ''' <param name="Entry">Value to Write</param>
    ''' <param name="AppName">Name of Client Application. Needed because before writing to event log, you must have a named EventLog source</param>
    ''' <param name="EventType">Entry Type, from EventLogEntryType Structure e.g., EventLogEntryType.Warning, EventLogEntryType.Error</param>
    ''' <param name="LogName">Name of Log (System, Application; Security is read-only) If you specify a non-existent log, the log will be created</param>
    ''' <returns>True if successful, false if not</returns>
    ''' <remarks></remarks>
    Public Shared Function WriteToEventLog(ByVal Entry As String, _
                           Optional ByVal AppName As String = "Application", _
                           Optional ByVal EventType As EventLogEntryType = EventLogEntryType.Warning, _
                           Optional ByVal LogName As String = "Application") As Boolean
        'If Not EventLog.SourceExists(AppName) Then
        '    EventLog.CreateEventSource(AppName, LogName)
        'End If
        Try
            Dim myEventLog As New EventLog()

            myEventLog.Source = AppName
            myEventLog.WriteEntry(Entry, EventType)
            Return True

        Catch Ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get description for a code
    ''' </summary>
    ''' <param name="Code">Code to search</param>
    ''' <param name="Table">Table name</param>
    ''' <param name="FieldCode">Field code name </param>
    ''' <param name="DescriptField">Field description name</param>
    Public Shared Function GetDescriptByCode(ByVal Code As Integer, Optional ByVal Table As String = "InternalMSG", Optional ByVal FieldCode As String = "nCodigint", Optional ByVal DescriptField As String = "sDescript") As String
        Dim lclsQuery As eRemoteDB.Query

        lclsQuery = New eRemoteDB.Query
        With lclsQuery
            If .OpenQuery(Table, DescriptField, FieldCode & "=" & Code.ToString) Then
                GetDescriptByCode = .FieldToClass(DescriptField)
                .CloseQuery()
            Else
                GetDescriptByCode = String.Empty
            End If
        End With
        lclsQuery = Nothing
    End Function

    Public Shared Function CreateValueList(ByVal sNames As String, ByVal aValues As Object) As String
        Dim arrNames() As String = Nothing
        Dim intIndex As Short
        Dim strBuffer As String = String.Empty
        arrNames = Microsoft.VisualBasic.Split(sNames, ",")
        For intIndex = 0 To UBound(aValues)
            If strBuffer > String.Empty Then
                strBuffer = strBuffer & "," & vbCrLf
            End If
            If arrNames(intIndex).EndsWith(")") Then
                strBuffer = strBuffer & arrNames(intIndex).Replace(")", "") & ":=" & ErrVarToString(aValues(intIndex)) & ")"
            Else
                strBuffer = strBuffer & arrNames(intIndex) & ":=" & ErrVarToString(aValues(intIndex))
            End If

        Next intIndex
        Return strBuffer

    End Function

    Private Shared Function ErrVarToString(ByVal vValue As Object) As String
        If IsArray(vValue) Then
            ErrVarToString = "{Array}"
        Else
            Select Case VarType(vValue)
                Case VariantType.Short, VariantType.Integer, VariantType.Byte, VariantType.Single, VariantType.Double, VariantType.Decimal, VariantType.Decimal
                    ErrVarToString = CStr(vValue)
                Case VariantType.Boolean
                    If vValue Then
                        ErrVarToString = "True"
                    Else
                        ErrVarToString = "False"
                    End If
                Case VariantType.Date
                    ErrVarToString = """" & CStr(vValue) & """"
                Case VariantType.Error
                    ErrVarToString = ""
                Case VariantType.Empty
                    ErrVarToString = "{Empty}"
                Case VariantType.Null
                    ErrVarToString = "{Null}"
                Case VariantType.String

                    vValue = Replace(vValue, vbNewLine, String.Empty)
                    vValue = Replace(vValue, vbTab, " ")

                    If Len(vValue) > 100 Then
                        vValue = Left(vValue, 100) & "..."
                    End If
                    ErrVarToString = """" & vValue & """"
                Case VariantType.Object
                    ErrVarToString = "{" & TypeName(vValue) & "}" 'Value of Nothing will be shown as "Nothing"
                Case Else
                    ErrVarToString = "{?}"
            End Select
        End If
    End Function

End Class
