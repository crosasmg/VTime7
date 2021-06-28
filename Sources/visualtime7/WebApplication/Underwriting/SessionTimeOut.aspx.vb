
Partial Class Underwriting_SessionTimeOut
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("SessionTimeOut") = "Yes"
        Response.Redirect("~\Underwriting\UnderwritingPanel.aspx")
    End Sub
End Class
