<%@ WebHandler Language="VB" Class="DeleteFileHandler" %>

Public Class DeleteFileHandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim nameFile As String = context.Request.Params("key")

        If (nameFile.IsNotEmpty) Then
            'IO.File.Delete(IO.Path.Combine(ConfigurationManager.AppSettings("WebApplicationPath"), "fasi\dli\Uploads\", nameFile))
            context.Response.ContentType = "application/json; charset=UTF-8"
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = nameFile}))
        End If
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class