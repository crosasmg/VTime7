#Region "using"

Imports GIT.Core

#End Region

Partial Class dropthings_UploadFile
    Inherits PageBase


    Protected Sub UploadFiles_FileUploadComplete(sender As Object, e As DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs) Handles UploadFiles.FileUploadComplete
        e.UploadedFile.SaveAs(IO.Path.Combine(ConfigurationManager.AppSettings("Path.Uploads"),
                                              e.UploadedFile.FileName))
        e.CallbackData = e.UploadedFile.FileName
    End Sub

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load

        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(),
                                                  "SetParameters",
                                                  "<script language='javascript' type='text/javascript'>" &
                                                  " var editorClientId = '" & Request.QueryString("ctrlname") & "';" &
                                                  " var imageClientId = '" & Request.QueryString("imgname") & "';" &
                                                  " var removeClientId = '" & Request.QueryString("remname") & "';" &
                                                  "</script>")

    End Sub

End Class



