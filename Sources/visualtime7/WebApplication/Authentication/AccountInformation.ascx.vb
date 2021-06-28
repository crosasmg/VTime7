#Region "using"

Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.FrontOffice.Support
Imports InMotionGIT.FrontOffice.Proxy

#End Region

Partial Class AccountInformationControl
    Inherits System.Web.UI.UserControl

#Region "Public Properties"

    Public Property CurrentUserType() As String
        Get
            Return ViewState("UserType")
        End Get
        Set(ByVal value As String)
            ViewState("UserType") = value
        End Set
    End Property

#End Region

#Region "Controls Events"

    Protected Sub UserNameTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UserNameTextBox.TextChanged
        If Not String.IsNullOrEmpty(UserNameTextBox.Text) Then
            With InvalidUserLabel
                Dim users As MembershipUserCollection = Membership.FindUsersByName(UserNameTextBox.Text)

                If IsNothing(users) OrElse users.Count = 0 Then
                    .Visible = False
                Else
                    .Visible = True
                    .Text = GetLocalResourceObject("InvalidUserLabelResource").ToString()
                    UserNameTextBox.Text = String.Empty
                End If
            End With
        End If
    End Sub

    Protected Sub EmailAddressTextBox_TextChanged(sender As Object, e As System.EventArgs) Handles EmailAddressTextBox.TextChanged
        If Not String.IsNullOrEmpty(EmailAddressTextBox.Text) Then
            With InvalidEmailLabel
                Dim users As MembershipUserCollection = Membership.FindUsersByEmail(EmailAddressTextBox.Text)

                If IsNothing(users) OrElse users.Count = 0 Then
                    .Visible = False
                Else
                    .Visible = True
                    .Text = GetLocalResourceObject("InvalidEmailLabelResource").ToString()
                    EmailAddressTextBox.Text = String.Empty
                End If
            End With
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub GetSecurityData(ByRef userName As String, ByRef password As String, ByRef eMail As String,
                               ByRef secretQuestion As String, ByRef secretAnswer As String)

        userName = UserNameTextBox.Text
        password = AuthenticationHelper.CreateRandomPassword(Membership.MinRequiredPasswordLength, Membership.MinRequiredNonAlphanumericCharacters)
        eMail = EmailAddressTextBox.Text
        secretQuestion = SecurityQuestionMemo.Text
        secretAnswer = SecurityAnswerTextBox.Text
    End Sub

    Public Sub GetSecurityData(ByRef userInformation As UserService.UserInformation)
        With userInformation
            .UserName = UserNameTextBox.Text
            .Email = EmailAddressTextBox.Text
            .PasswordQuestion = SecurityQuestionMemo.Text
            .PasswordAnswer = SecurityAnswerTextBox.Text
        End With
    End Sub

    Public Sub SaveMembershipUser(ByRef userInformation As UserService.UserInformation)
        Dim currentUser As MembershipUser = Membership.GetUser(userInformation.UserName)

        With currentUser
            .Email = EmailAddressTextBox.Text
            Membership.UpdateUser(currentUser)
        End With

        userInformation.RealUserName = UserNameTextBox.Text
    End Sub

    Public Sub SetUserData(userInformation As UserService.UserInformation)
        With userInformation
            UserNameTextBox.Text = .RealUserName

            With EmailAddressTextBox
                .Text = userInformation.Email
                .Enabled = False
            End With

            VerifyEmailTextBox.Visible = False
            VerifyEmailLabel.Visible = False
            SecurityQuestionLabel.Visible = False
            SecurityQuestionMemo.Visible = False
            SecurityAnswerLabel.Visible = False
            SecurityAnswerTextBox.Visible = False
            VerifyEmailTextBox.Text = .Email
            EmailCompareValidator.Visible = False

            SecurityQuestionMemo.Text = .PasswordQuestion
        End With
    End Sub

#End Region

End Class

