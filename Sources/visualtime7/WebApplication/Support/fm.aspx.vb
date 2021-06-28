
Partial Class Support_fm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString.IsNotEmpty AndAlso Request.QueryString("Key").IsNotEmpty Then
                If InMotionGIT.Common.Helpers.KeyValidator.KeyValidator(Request.QueryString("Key")) Then
                    If Request.QueryString("root").IsNotEmpty Then
                        ASPxFileManager1.Settings.RootFolder = Request.QueryString("root")
                    End If
                    ASPxFileManager1.ClientVisible = True
                End If
            End If
        End If
    End Sub
End Class
