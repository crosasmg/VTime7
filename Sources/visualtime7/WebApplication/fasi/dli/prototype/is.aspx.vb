Imports System.Data
Imports System.IO
Imports System.Web.Script.Services
Imports System.Web.Services
Imports InMotionGIT.Common.Proxy

Partial Class Support_is
    Inherits System.Web.UI.Page

#Region "Methods"

#Region "Métodos generales"

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function Validator(key As String) As Object
        Return InMotionGIT.Common.Helpers.KeyValidator.KeyValidator(key) '   KeyValidator.KeyValidator(key)
    End Function

#End Region

#Region "Métodos de Viewer Logs"

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function FileLogs() As Object
        Dim path As String = ConfigurationManager.AppSettings("Path.Logs")
        Dim dateLimit As Date = Date.Now.AddDays(-30)
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

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function FileNames(dateStart As Date, dateEnd As Date) As Object

        Dim path As String = ConfigurationManager.AppSettings("Path.Logs")

        Dim result As New List(Of Object)
        Dim index As Integer = 0
        For Each file As String In IO.Directory.GetFiles(path)
            Dim di As New DirectoryInfo(file)
            If di.CreationTime >= dateStart AndAlso di.CreationTime <= dateEnd.AddDays(1) Then
                result.Add(New With {Key .Name = System.IO.Path.GetFileName(file),
                                     Key .Index = index,
                                     Key .Path = file,
                                     Key .Fecha = di.CreationTime.ToShortDateString,
                                     Key .CountLine = System.IO.File.ReadLines(file).Count})
                index = index + 1
            End If
        Next
        Return result
    End Function

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ReadFile(path As String) As Object
        Return System.IO.File.ReadAllText(path)
    End Function

#End Region

#Region "Methods de Data Factory"

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetConnetionsEnable(key As String) As Object
        Dim result As New List(Of Object)

        Dim clien As New InMotionGIT.Common.Proxy.DataManagerFactory()

        If key.IsNotEmpty Then
            For Each Item In clien.ConnectionStringAll(key)
                result.Add(New With {Key .Name = Item.Name,
                                     Key .Id = Item.Name})
            Next
        End If

        Return result
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ExcuteQuery(ConnectionStrinName As String, Query As String) As Object
        Dim result As Object
        Dim hasError As Boolean = False
        Dim MessageError As String = String.Empty
        Dim tableResul As DataTable
        Dim ColumnaNames As String() = Nothing
        Dim values As New List(Of Object)
        Dim valuesJSON As New List(Of Object)
        Dim count As Integer = 0
        Try
            If ConnectionStrinName.IsNotEmpty AndAlso Query.IsNotEmpty Then
                Query = Query.Replace(vbLf, " ")
                With New DataManagerFactory(Query, "GENERAL", ConnectionStrinName)
                    tableResul = .QueryExecuteToTable(True)
                    count = tableResul.Rows.Count
                    If tableResul.IsNotEmpty AndAlso tableResul.Rows.Count <> 0 Then
                        ColumnaNames = (From itemColumn As DataColumn In tableResul.Columns Select itemColumn.ColumnName).ToArray
                        For Each ItemRow As DataRow In tableResul.Rows
                            Dim itemRowObject As New List(Of Object)
                            Dim itemRowObjectJSON As New Object
                            Dim properties As New Dictionary(Of String, Object)
                            For index = 0 To ColumnaNames.Length - 1
                                itemRowObject.Add(New With {Key .Name = ColumnaNames(index),
                                                            Key .Value = ItemRow(index),
                                                            Key .Type = ItemRow(index).GetType.ToString})

                                '      properties.Add(index.ToString, ItemRow(index))

                            Next
                            ' valuesJSON.Add(InMotionGIT.FrontOffice.Tools.Helpers.DataFactoryConverts.GetDynamicObject(properties))
                            values.Add(New With {Key .Value = itemRowObject})
                        Next
                    End If
                End With
            Else
                hasError = True
                MessageError = "No se puede ejecutar el query si los campos ConnectionStrinName o el Query están vacíos"
            End If
        Catch ex As Exception
            hasError = True
            MessageError = ex.Message
        End Try

        If count = 0 And MessageError.IsEmpty Then
            MessageError = "El query retorno 0 registros"
        End If

        result = New With {Key .ColumnaNames = ColumnaNames,
                           Key .Values = values,
                           Key .ValuesJSON = valuesJSON,
                           Key .Count = count,
                           Key .HasError = hasError,
                           Key .MessageError = MessageError}
        Return result
    End Function

#End Region

#Region "FileManager"

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function FolderAndFiles(path As String) As Object
        Dim result As Object
        If path.IsEmpty Then
            path = System.Configuration.ConfigurationManager.AppSettings("WebApplicationPath")
            path = "E:\VisualTIMENet\WebApplication"
        End If
        Dim client As New InMotionGIT.Common.Services.DataManager()
        With client
            result = client.AppInfo(path)
        End With
        Return result
    End Function

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function Encrypted(value As String) As Object
        Return InMotionGIT.Common.Helpers.CryptSupportNew.EncryptString(value)
    End Function

#End Region

#End Region

#Region "Event page"

    Private Sub Support_Tls_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.QueryString.IsNotEmpty AndAlso Request.QueryString("Key").IsNotEmpty Then
            If InMotionGIT.Common.Helpers.KeyValidator.KeyValidator(Request.QueryString("Key")) Then

            End If
        Else

        End If

    End Sub

#End Region

End Class