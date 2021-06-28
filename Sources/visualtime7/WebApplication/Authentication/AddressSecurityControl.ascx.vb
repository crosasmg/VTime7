#Region "using"

Imports GIT.Core.Helpers
Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper

#End Region

Partial Class AddressSecurityControl
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

#Region "Controls Events"

    Protected Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click
        Dim users As MembershipUserCollection = Membership.FindUsersByEmail(EmailAddressTextBox.Text)
        Dim existEmail As Boolean

        If Not IsNothing(users) AndAlso users.Count > 0 Then
            existEmail = True
        Else
            existEmail = False
        End If

        If Not existEmail Then
            Dim currentProfile As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser = System.Web.Security.Membership.GetUser(CurrentUserName)

            If currentProfile.IsInactive Then
                lblSendMessage.Visible = True
                lblSendMessage.Text = GetLocalResourceObject("InactiveUserMessageResource").ToString

                EmailAddressTextBox.Enabled = False
                VerifyEmailTextBox.Enabled = False

                UpdateButton.Visible = False
                CloseButton.Visible = True

            Else
                Dim currentUser As MembershipUser = Membership.GetUser(CurrentUserName)
                Dim password As String = CreateRandomPassword(Membership.MinRequiredPasswordLength, Membership.MinRequiredNonAlphanumericCharacters)

                currentProfile.OldEMail = currentUser.Email

                currentUser.ChangePassword(currentUser.GetPassword(), password)

                currentUser.Email = EmailAddressTextBox.Text
                Membership.UpdateUser(currentUser)

                AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.SuccessfulChangedEmail, String.Empty)

                With currentProfile
                    System.Web.Security.Membership.UpdateUser(currentProfile)

                    Dim parameters As New Dictionary(Of String, Object) From {{"AddressSecurity", currentProfile},
                                                                              {"Password", password},
                                                                              {"EMAIL", currentUser.Email}}

                    InMotionGIT.Correspondence.Support.Mail.SendMailWithTemplate("Authentication\AddressSecurity",
                                                                                  currentProfile.UserType.ToString,
                                                                                  currentProfile.LanguageName,
                                                                                  parameters,
                                                                                  currentUser.Email)

                    InMotionGIT.Correspondence.Support.Mail.SendMailWithTemplate("Authentication\ChangeProfileEmail",
                                                                                  currentProfile.UserType.ToString,
                                                                                  currentProfile.LanguageName,
                                                                                  parameters,
                                                                                  .OldEMail)

                    lblSendMessage.Visible = True
                    lblSendMessage.Text = GetLocalResourceObject("lblSendMessageResource").ToString
                    EmailAddressTextBox.Enabled = False
                    VerifyEmailTextBox.Enabled = False

                    UpdateButton.Visible = False
                    CloseButton.Visible = True
                End With
            End If

        Else
            AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.FailedChangedEmail, String.Empty)
            OptionsPanel.Visible = True
        End If
    End Sub

#End Region
 
End Class
