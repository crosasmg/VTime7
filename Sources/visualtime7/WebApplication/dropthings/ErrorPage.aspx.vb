
Partial Class dropthings_ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ScriptManager.RegisterStartupScript(Page, GetType(dropthings_ErrorPage), "ShowPopupControl",
                                               "<script type=text/javascript>Message('" & Request.QueryString("ErrorMessage") & "');</script>", False)
    End Sub

End Class
