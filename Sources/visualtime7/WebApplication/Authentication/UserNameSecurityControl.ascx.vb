#Region "using"

Imports GIT.Core.Helpers
Imports DevExpress.Web.ASPxEditors
Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper
Imports InMotionGIT.Core.Configuration

#End Region

Partial Class UserNameSecurityControl
    Inherits System.Web.UI.UserControl

#Region "Public Properties"

    Public Property CurrentUserName() As String
        Get
            Return ViewState("UserName")
        End Get
        Set(ByVal value As String)
            ViewState("UserName") = value
        End Set
    End Property

#End Region

    Protected Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click
        Dim newUserName As String = UserNameTextBox.Text
        Dim config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)

        Dim eMail As String = String.Format("{0}{1}", newUserName, config.Authentification.EmailSuffix)
        Dim users As MembershipUserCollection = Membership.FindUsersByEmail(eMail)

        If IsNothing(users) OrElse users.Count = 0 Then
            Dim currentUser As MembershipUser = Membership.GetUser(CurrentUserName)
            Dim currentProfile As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser = System.Web.Security.Membership.GetUser(CurrentUserName)

            If currentProfile.IsInactive Then
                With lblMessageUpdate
                    .Text = GetLocalResourceObject("InactiveUserMessageResource").ToString
                    .Visible = True
                End With

                UserNameTextBox.Enabled = False
                VerifyUserNameTextBox.Enabled = False
                UpdateButton.Visible = False
                CloseButton.Visible = True

            Else
                With currentProfile
                    .RealUserName = newUserName
                    System.Web.Security.Membership.UpdateUser(currentProfile)

                    currentUser.Email = eMail
                    Membership.UpdateUser(currentUser)

                    AddUsersSecurityTrace(eMail, EnumUserSecurity.SuccessfulChangedUserName, String.Empty)

                    Dim parameters As New Dictionary(Of String, Object) From {{"UserNameSecurity", currentProfile}}

                    InMotionGIT.Correspondence.Support.Mail.SendMailWithTemplate("Authenticacion\UserNameSecurity",
                                                                                  currentProfile.UserType.ToString,
                                                                                  currentProfile.LanguageName,
                                                                                  parameters,
                                                                                  eMail)

                    With lblMessageUpdate
                        .Text = GetLocalResourceObject("MessageUpdateResource").ToString().Replace("USERNAME", CurrentUserName)
                        .Visible = True
                    End With

                    UserNameTextBox.Enabled = False
                    VerifyUserNameTextBox.Enabled = False
                    UpdateButton.Visible = False
                    CloseButton.Visible = True
                End With
            End If

        Else
            lblMessage.Visible = True
        End If
    End Sub

End Class
