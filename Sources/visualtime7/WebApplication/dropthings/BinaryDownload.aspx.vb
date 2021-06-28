Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.Modelo
Imports InMotionGIT.Seguridad.Proxy

Partial Class dropthings_BinaryDownload
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sequenceId As String = Request.QueryString("SequenceId")
        Dim consequenceId As String = Request.QueryString("ConsequenceId")
        Dim fileName As String = Request.QueryString("FileName")
        Dim view As String = Request.QueryString("View")
        Dim columnName As String = Request.QueryString("ColumnName")
        Dim provider As String = ConfigurationManager.AppSettings.Get("DNEProvider")
        'Dim binaryFile As Byte() = Convert.FromBase64String(Request.QueryString("file"))

        Dim composedResourceKey As New ComposedResourceKey(sequenceId, consequenceId)

        Dim binaryContent = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetResourceContent(composedResourceKey, TokenHelper.GetValidToken(), provider)
        SaveFile(binaryContent, fileName)
        'Select Case view
        '    Case "IMAGEN_DLI"
        '        Dim imageDTO As ImageDTO
        '        If columnName.ToLower.Contains("web") Then 'Web quality
        '            imageDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetWebQualityImage(composedResourceKey, "asdg", provider)
        '            SaveFile(imageDTO.WebQualityImage, fileName)
        '        Else 'Original quality
        '            imageDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetOriginalSizeImage(composedResourceKey, "asdg", provider)
        '            SaveFile(imageDTO.OriginalImage, fileName)
        '        End If
        '    Case "VIDEO_DLI"
        '        Dim videoDTO As VideoDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetVideo(composedResourceKey, "asdg", provider)
        '        SaveFile(videoDTO.Video, fileName)
        '    Case "ARCHIVO_SONIDO_DLI"
        '        Dim audioDTO As AudioDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetAudio(composedResourceKey, "asdg", provider)
        '        SaveFile(audioDTO.Sound, fileName)
        '    Case "DOCUMENTO_DLI"
        '        Dim documentDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetDocument(composedResourceKey, "asdg", provider)
        '        SaveFile(documentDTO.Document, fileName)
        'End Select
    End Sub

    Private Sub SaveFile(bytesContent As Byte(), fileName As String)
        If Not IsNothing(bytesContent) Then
            Response.ClearContent()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
            'Response.AddHeader("Content-Length", binaryFile.Length)
            Response.ContentType = "application/octet-stream"
            Response.BinaryWrite(bytesContent)
            Response.Flush()
            Response.End()
        End If
    End Sub
End Class
