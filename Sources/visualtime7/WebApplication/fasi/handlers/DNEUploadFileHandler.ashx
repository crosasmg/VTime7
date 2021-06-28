<%@ WebHandler Language="VB" Class="DNEUploadFileHandler" %>

Public Class DNEUploadFileHandler : Implements IHttpHandler, IRequiresSessionState
    Private _url As String = ConfigurationManager.AppSettings("DNE.URL")
    Private _provider As String = ConfigurationManager.AppSettings("DNEProvider").ToUpper()
    Dim fileExtensionsPermited As New List(Of KeyValuePair(Of Integer, List(Of String)))

    Dim resourceTypes As New Dictionary(Of String, Int16)() From {
        {"Image", 1},
        {"Video", 2},
        {"Audio", 3},
        {"Document", 4}
    }

    Dim tagTypeIds As New Dictionary(Of String, Int16)() From {
        {"FormId", 1},
        {"RequirementId", 2},
        {"CaseId", 3},
        {"InformativeId", 4},
        {"RUT", 5},
        {"RUTFigure", 6},
        {"RUTFigureId", 7},
        {"Proposal", 8},
        {"DocumentDescription", 9},
        {"DocumentId", 10},
        {"RequirementDescription", 11},
        {"RequirementTypeId", 12},
        {"RequirementTypeDescription", 13},
        {"RequirementCreationDate", 14},
        {"PolicyId", 15}
    }

    ''' <summary>
    ''' Handles the request of a new file
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            context.Response.ContentType = "application/json"
            context.Response.Charset = "utf-8"
            context.Response.ContentEncoding = Encoding.UTF8

            Dim selector As String = context.Request.Params("selector").ToString()
            Dim token As String = context.Request.Params("token").ToString
            Dim uploadedFile As HttpPostedFile
            uploadedFile = context.Request.Files.Get(String.Concat(selector, "DNEFileInput"))

            If (uploadedFile IsNot Nothing) Then

                'Verifica la extensión del archivo
                Dim fileExtension = String.Empty
                If (Not uploadedFile.FileName.Split(".").Last.Equals(uploadedFile.FileName)) Then
                    fileExtension = "." + uploadedFile.FileName.Split(".").Last
                End If

                Dim fileData As Byte() = Nothing

                Using binaryReader As New IO.BinaryReader(uploadedFile.InputStream)
                    fileData = binaryReader.ReadBytes(uploadedFile.ContentLength)
                End Using

                'Crea un nuevo nombre único para el archivo
                'Dim fileName As String = String.Format("{0}{1}", Guid.NewGuid().ToString, fileExtension)
                'Graba el archivo en el disco
                'uploadedFile.SaveAs(IO.Path.Combine(ConfigurationManager.AppSettings("Path.Uploads"), fileName))

                fileExtensionsPermited = GetExtensions(token)
                Dim resouceType = GetFileType(fileExtension.ToUpper())
                If resouceType = -1 Then Throw New Exception(context.Request.Params("extensionNotAllowedMessage"))

                Dim resource As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ResourceDTO = New InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ResourceDTO

                Dim sequenceId As String = context.Request.Params("sequenceId")

                ' Si la secuencia es nula o cero, se crea una nueva
                If (String.IsNullOrEmpty(sequenceId) Or sequenceId = "0") Then
                    Using client As New Net.WebClient()
                        client.Encoding = Encoding.UTF8
                        client.Headers(Net.HttpRequestHeader.ContentType) = "application/json"
                        client.Headers(Net.HttpRequestHeader.Authorization) = String.Concat(Convert.ToString("Bearer "), token)
                        sequenceId = client.DownloadString(String.Concat(_url, "GenerateSequence", "?provider=", _provider))
                    End Using
                End If

                resource.SequenceId = sequenceId
                resource.Description = context.Request.Params("description")
                resource.Name = uploadedFile.FileName
                resource.ResourceTypeId = resouceType

                If (context.Request.Params("expirationDate").Trim() = "") then
                    resource.ExpirationDate = Nothing
                Else
                    resource.ExpirationDate = System.DateTime.ParseExact(
                        context.Request.Params("expirationDate"),
                        context.Request.Params("formatDate").Replace("D","d").Replace("Y","y").Replace("mm","MM"),
                        System.Globalization.DateTimeFormatInfo.InvariantInfo
                    )
                End If

                If (resource.ExpirationDate <= DateTime.Now.Date) Then
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(New With {.error = context.Request.Params("expirationDateErrorMessage")}))
                Else
                    ' Sets the content bytes of the file
                    SetResourceContent(resource, fileData)

                    ' Sets the tags
                    Dim contentTags As String = context.Request.Params("tags")
                    If Not String.IsNullOrEmpty(contentTags) Then
                        Dim tags As List(Of InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagDTO) =
                                Newtonsoft.Json.JsonConvert.DeserializeObject(contentTags, GetType(List(Of InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagDTO)))

                        resource.Tags = New HashSet(Of InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagDTO)(tags)
                    End If

                    'TODO estas dos propiedades tienen que ser puestas como propiedades del control
                    resource.ClientAssociatedCompany = 1
                    resource.ClientAssociatedPerson = 1

                    Dim result As String = String.Empty
                    Dim operationName = context.Request.Params("operationName")
                    Dim request As New Request With {.provider = _provider, .resource = resource}
                    Dim address As String = String.Concat(_url, operationName)
                    Dim serializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    serializer.MaxJsonLength = Int32.MaxValue

                    If (Convert.ToBoolean(ConfigurationManager.AppSettings("FrontOffice.Debug")))
                        InMotionGIT.Common.Helpers.LogHandler.TraceLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " DNERequest", serializer.Serialize(request), "DNE")
                    End If

                    Using client As New Net.WebClient()
                        client.Encoding = Encoding.UTF8
                        client.Headers(Net.HttpRequestHeader.ContentType) = "application/json"
                        client.Headers(Net.HttpRequestHeader.Authorization) = String.Concat(Convert.ToString("Bearer "), token)
                        result = client.UploadString(address, serializer.Serialize(request))
                    End Using

                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = sequenceId}))
                End If
            End If
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, ex)
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(New With {.error = ex.Message}))
        End Try
    End Sub


    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property


    ''' <summary>
    ''' Converts from shortTag to Tag
    ''' </summary>
    ''' <param name="shorTagDTO"></param>
    ''' <returns></returns>
    Private Function ConvertirShortTagDTOATagDTO(ByVal shorTagDTO As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ShortTagDTO) As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagDTO
        Dim tagDTO As New InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagDTO()
        tagDTO.Content = shorTagDTO.Content
        tagDTO.TagTypeId = shorTagDTO.TagTypeId
        Return tagDTO
    End Function


    ''' <summary>
    ''' Get the possible extensions for the resources
    ''' </summary>
    ''' <returns></returns>
    Private Function GetExtensions(token As String) As List(Of KeyValuePair(Of Integer, List(Of String)))
        Dim result As New List(Of KeyValuePair(Of Integer, List(Of String)))

        Dim address As String = String.Concat(_url, "GetFileExtensionsAllowed?provider=", _provider)
        Using client As New Net.WebClient()
            Try
                client.Encoding = Encoding.UTF8
                client.Headers(Net.HttpRequestHeader.ContentType) = "application/json"
                client.Headers(Net.HttpRequestHeader.Authorization) = String.Concat(Convert.ToString("Bearer "), token)
                result = Newtonsoft.Json.JsonConvert.DeserializeObject(client.DownloadString(address), GetType(List(Of KeyValuePair(Of Integer, List(Of String)))))
            Catch ex As Exception
                Throw ex
            End Try
        End Using

        Return result

    End Function


    ''' <summary>
    ''' Gets File type given an extension
    ''' </summary>
    ''' <param name="extension">Extension name</param>
    ''' <returns></returns>
    Private Function GetFileType(extension As String) As Integer
        Dim imageAcceptedExtensions = fileExtensionsPermited(0).Value
        Dim videoAcceptedExtensions = fileExtensionsPermited(1).Value
        Dim audioAcceptedExtensions = fileExtensionsPermited(2).Value
        Dim documentAcceptedExtensions = fileExtensionsPermited(3).Value

        If imageAcceptedExtensions.Contains(extension) Then
            Return resourceTypes("Image")
        ElseIf videoAcceptedExtensions.Contains(extension) Then
            Return resourceTypes("Video")
        ElseIf audioAcceptedExtensions.Contains(extension) Then
            Return resourceTypes("Audio")
        ElseIf documentAcceptedExtensions.Contains(extension) Then
            Return resourceTypes("Document")
        End If

        Return -1          'Si la extension recibida no esta en ninguno de las extensiones permitidas, retorna -1 
    End Function


    ''' <summary>
    ''' Loads the resource selected by the user and keeps it in the resource object
    ''' </summary>
    ''' <param name="resourceDTO">The object that will keep the resource</param>
    ''' <param name="contentFile">Content bytes of the file</param>
    Private Sub SetResourceContent(ByRef resourceDTO As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ResourceDTO, contentFile As Byte())
        Select Case resourceDTO.ResourceTypeId
            Case 1
                resourceDTO.Image = New InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ImageDTO
                resourceDTO.Image.SequenceId = resourceDTO.SequenceId
                resourceDTO.Image.OriginalImage = contentFile
            Case 2
                resourceDTO.Video = New InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.VideoDTO
                resourceDTO.Video.SequenceId = resourceDTO.SequenceId
                resourceDTO.Video.Video = contentFile
            Case 3
                resourceDTO.Audio = New InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.AudioDTO
                resourceDTO.Audio.SequenceId = resourceDTO.SequenceId
                resourceDTO.Audio.Sound = contentFile
            Case 4
                resourceDTO.Document = New InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.DocumentDTO
                resourceDTO.Document.SequenceId = resourceDTO.SequenceId
                resourceDTO.Document.Document = contentFile

        End Select
    End Sub


    ''' <summary>
    ''' Gets the method that will be used to populate the main Grid
    ''' </summary>
    ''' <param name="useOnlyTemporaryResources">Variable that defines whether only temporary Resources will be shown</param>
    ''' <param name="userRoles">User's roles</param>
    ''' <returns>The method that will be used to populate the Grid</returns>
    Private Function GetOperationContractMethodToGetResources(useOnlyTemporaryResources As Boolean, userRoles As String) As String
        If userRoles IsNot Nothing AndAlso Not userRoles.Equals("") Then
            If userRoles.Split(";").Contains("suscriptor") Then
                If useOnlyTemporaryResources Then
                    Return "GetResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetActiveResourceSequenceAndMyTemporals"
                End If
            ElseIf userRoles.Split(";").Contains("cliente") Then ' if userRole is Client
                If useOnlyTemporaryResources Then
                    Return "GetOwnResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetOwnResourceSequenceActiveAndTemporaryState"
                End If
            End If
        End If
        Return ""   'Si el usuario no es cliente, tampoco es administrador, no debería ver nada         
    End Function


    ''' <summary>
    ''' Clase para el request al momento de agregar un recurso
    ''' </summary>
    Private Class Request
        Private _provider As String
        Public Property provider() As String
            Get
                Return _provider
            End Get
            Set(ByVal value As String)
                _provider = value
            End Set
        End Property

        Private _resource As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ResourceDTO
        Public Property resource() As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ResourceDTO
            Get
                Return _resource
            End Get
            Set(ByVal value As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ResourceDTO)
                _resource = value
            End Set
        End Property
    End Class
End Class