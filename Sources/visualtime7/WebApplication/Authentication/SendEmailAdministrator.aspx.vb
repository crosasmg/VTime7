#Region "using"

Imports GIT.Core.Helpers
Imports GIT.Core
Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper

#End Region

Partial Class Authentication_SendEmailAdministrator
    Inherits PageBase

    Protected Sub SendButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SendButton.Click
        Dim subject As String = String.Empty
        'Dim template As String = FindTemplateResource(String.Empty, "~/Authentication/SendEmailAdministrator.aspx", subject)
        Dim parameters As New Dictionary(Of String, Object)

        With parameters
            .Add("@@name@@", CompleteNameTextBox.Text)
            .Add("@@email@@", EmailAddressTextBox.Text)
            .Add("@@telephone@@", TelephoneTextBox.Text)
            .Add("@@comment@@", CommentMemo.Text)
        End With

        'SendUserEmail(template, subject, EmailAddressTextBox.Text, Nothing , String.Empty, String.Empty, String.Empty, dataPersonal)
        InMotionGIT.Correspondence.Support.Mail.SendMailWithTemplate("Authentication\SendEmailAdministrator",
                                                                  Nothing,
                                                                  Nothing,
                                                                  parameters,
                                                                  EmailAddressTextBox.Text)

        AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.SendEmailAdministrator, String.Empty)

        popupMessageControl.ShowOnPageLoad = True
    End Sub

End Class
