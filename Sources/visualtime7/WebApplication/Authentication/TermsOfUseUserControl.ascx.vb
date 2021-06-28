Imports InMotionGIT.FrontOffice.Support

Partial Class TermsOfUseUserControl
    Inherits System.Web.UI.UserControl

#Region "Page Methods"

    Public Sub LoadPageHTML(ByVal currentUserType As String)
        Dim template As String = HttpContext.GetLocalResourceObject("~/Authentication/TermsOfUseUserControl.ascx", String.Format("{0}TemplateResource", currentUserType)).ToString()

        Dim newLabel As New Label() With {.Text = template}
        TermsPanel.Controls.Add(newLabel)
    End Sub

#End Region

End Class
