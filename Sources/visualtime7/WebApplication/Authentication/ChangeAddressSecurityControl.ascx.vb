Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper

Partial Class ChangeAddressSecurityControl
    Inherits System.Web.UI.UserControl

#Region "Public Properties"

    Public Property PasswordAttempts() As Integer
        Get
            Return ViewState("Attempts")
        End Get
        Set(ByVal value As Integer)
            ViewState("Attempts") = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event BehaviorProcess(ByVal currentUserName As String, ByVal currentEmail As String, ByVal currentSecurityQuestion As String)

#End Region

#Region "Controls Events"

    Protected Sub FindButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FindButton.Click
        With InvalidEmailLabel
            If String.IsNullOrEmpty(EmailAddressTextBox.Text) Then
                .Text = Me.GetLocalResourceObject("EmailRequiredResource").ToString()
                .Visible = True

            Else
                Dim email As String = InMotionGIT.FrontOffice.Support.Helpers.ValidateFormat.MailAddress(EmailAddressTextBox.Text)

                If String.IsNullOrEmpty(email) Then
                    .Text = Me.GetLocalResourceObject("EmailInvalidResource").ToString()
                    .Visible = True
                Else

                    Dim users As MembershipUserCollection = Membership.FindUsersByEmail(email)

                    If Not IsNothing(users) AndAlso users.Count > 0 Then
                        .Visible = False

                        AddUsersSecurityTrace(email, EnumUserSecurity.ValidateEmail, String.Empty)

                        For Each user As MembershipUser In users
                            RaiseEvent BehaviorProcess(user.UserName, email, user.PasswordQuestion)
                        Next

                    Else
                        PasswordAttempts += 1

                        AddUsersSecurityTrace(email, EnumUserSecurity.InvalidEmail, String.Empty)

                        If Membership.MaxInvalidPasswordAttempts = PasswordAttempts Then
                            PasswordAttempts = 0
                            OptionsPanel.Visible = True

                        Else
                            .Text = Me.GetLocalResourceObject("UserExistResource").ToString()
                            .Visible = True
                        End If
                    End If
                End If
            End If
        End With
    End Sub

#End Region
  
End Class
