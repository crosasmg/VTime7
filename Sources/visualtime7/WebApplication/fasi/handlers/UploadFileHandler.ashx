<%@ WebHandler Language="VB" Class="UploadFileHandler" %>

Public Class UploadFileHandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim uploadedFile As HttpPostedFile
        uploadedFile = context.Request.Files.Get("file_data")

        If (uploadedFile IsNot Nothing) Then

            'Verifica la extensión del archivo
            Dim fileExtension = String.Empty
            If (Not uploadedFile.FileName.Split(".").Last.Equals(uploadedFile.FileName)) Then
                fileExtension = "." + uploadedFile.FileName.Split(".").Last
            End If


            'Modo : original=deja el nombre del archivo como tal | unique= utiliza un guid para definir un archivo unico.
            Dim mode = context.Request.QueryString("mode")
            If mode.IsEmpty() Then
                mode = "original"
            End If
            mode = mode.ToLower()

            'Crea un nuevo nombre único para el archivo
            Dim fileName As String = String.Empty
            If mode.Equals("unique") Then
                fileName = String.Format("{0}{1}", Guid.NewGuid().ToString, fileExtension)
            Else
                fileName = uploadedFile.FileName
            End If


            'Graba el archivo en el disco
            uploadedFile.SaveAs(IO.Path.Combine(ConfigurationManager.AppSettings("WebApplicationPath"), "fasi\dli\Uploads\", fileName))

            'Devuelve el nombre del archivo (Guid + extensión)
            context.Response.ContentType = "application/json; charset=UTF-8"
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = fileName}))
        End If
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class