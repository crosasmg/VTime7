Imports System.Web.Script.Services
Imports System.Web.Services

Partial Class Support_ViewLogs
    Inherits System.Web.UI.Page

    Private Sub Support_ViewLogs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim clean As Boolean = Context.Request.QueryString("clean").IsNotEmpty AndAlso
                       Context.Request.QueryString("clean").ToString.StartsWith("y", StringComparison.CurrentCultureIgnoreCase)
        If clean Then
            CleanDirectory(ConfigurationManager.AppSettings("Path.Logs"))
        End If
    End Sub

    Private Shared Sub CleanDirectory(path As String)
        For Each folder In IO.Directory.GetDirectories(path)
            If Not folder.EndsWith(".svn") Then
                CleanDirectory(folder)
            End If
        Next

        For Each file In IO.Directory.GetFiles(path)
            IO.File.Delete(file)

            Try
                IO.File.Delete(file)

            Catch ex As Exception
                InMotionGIT.Common.Helpers.LogHandler.ErrorLog("viewlog", "delete fail " & file, ex)
            End Try
        Next
    End Sub

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function IsEnable() As Object
        Dim result As Boolean = False
        If Not ConfigurationManager.AppSettings.AllKeys.Contains("ViewLog.Enable") Then
            result = True
        Else
            result = Boolean.Parse(ConfigurationManager.AppSettings("ViewLog.Enable"))
        End If
        Return result
    End Function

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function Encrypted(value As String) As Object
        Return InMotionGIT.Common.Helpers.CryptSupportNew.EncryptString(value)
    End Function

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function FileLogs() As Object
        Dim path As String = ConfigurationManager.AppSettings("Path.Logs")
        Dim dateLimit As Date = Date.Now.AddDays(-15)
        Dim result As New List(Of InMotionGIT.Common.Services.Contracts.info)
        Dim client As New InMotionGIT.Common.Services.DataManager()
        With client
            Dim root = client.AppInfo(path)
            If root.Childs.IsNotEmpty() AndAlso root.Childs.Count <> 0 Then
                For Each item In root.Childs
                    If Not item.IsFolder AndAlso item.LastWrite >= dateLimit Then
                        result.Add(item)
                    End If
                Next
            End If
        End With
        result = (From s In result
                  Order By s.LastWrite Descending
                  Select s).ToList

        Return result
    End Function

End Class