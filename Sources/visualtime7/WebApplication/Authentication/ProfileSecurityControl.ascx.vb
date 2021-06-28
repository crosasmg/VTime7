#Region "using"

Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper

#End Region

Partial Class ProfileSecurityControl
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

    Public Property RecoverPassword() As Boolean
        Get
            Return ViewState("RecoverPassword")
        End Get
        Set(ByVal value As Boolean)
            ViewState("RecoverPassword") = value
        End Set
    End Property

    Public Property AnswerAttempts() As Integer
        Get
            Return ViewState("AnswerAttempts")
        End Get
        Set(ByVal value As Integer)
            ViewState("AnswerAttempts") = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event BehaviorProcess(ByVal currentUserName As String)

#End Region

#Region "Controls Events"

    Protected Sub VerifyButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles VerifyButton.Click
        Dim currentProfile As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser = System.Web.Security.Membership.GetUser(CurrentUserName)

        If currentProfile.IsInactive Then
            SecurityAnswerTextBox.Enabled = False
            VerifyButton.Enabled = False
            CancelButton.Text = Me.GetLocalResourceObject("CloseBtn").ToString()
            lblMessage.Visible = True

            lblMessage.Text = GetLocalResourceObject("InactiveUserMessageResource").ToString
        Else

            Dim config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)
            Dim invalidAnswerAttempt As Integer = config.Authentification.InvalidAnswerAttempts
            Dim stateLockedCount As Boolean = False

            With currentProfile
                Try
                    If .IsLockedOut Then
                        stateLockedCount = True
                        .UnlockUser()
                    End If

                    Dim oldPassword = .GetPassword(SecurityAnswerTextBox.Text)

                    If RecoverPassword Then
                        SecurityAnswerPanel.Visible = False
                        Dim newPassword As String = CreateRandomPassword(Membership.MinRequiredPasswordLength, Membership.MinRequiredNonAlphanumericCharacters)

                        SecurityAnswerTextBox.Enabled = False
                        VerifyButton.Enabled = False
                        CancelButton.Text = Me.GetLocalResourceObject("CloseBtn").ToString()
                        lblMessage.Visible = True

                        Try
                            currentProfile.Password = newPassword
                            Dim configurationEmailSend As New InMotionGIT.FrontOffice.Contracts.Parameter

                            With configurationEmailSend

                                .TemplateName = "Authentication\ProfileSecurity"
                                .UserType = currentProfile.UserType.ToString
                                .Language = currentProfile.Language
                                .To = currentProfile.Email

                                .ParameterInternal = New Dictionary(Of String, String)
                                With .ParameterInternal
                                    .Add("FirstName", currentProfile.FirstName)
                                    .Add("LastName", currentProfile.LastName)
                                    .Add("RealUserName", currentProfile.RealUserName)
                                    .Add("Password", newPassword)
                                    .Add("name", "VisualTIME user")
                                End With
                            End With

                            InMotionGIT.FrontOffice.Proxy.Helpers.Email.SendMailWithTemplate(configurationEmailSend)

                            AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.SuccessfulSecurityAnswer, String.Empty)

                            With currentProfile
                                .AccountLockoutNotification = False
                                System.Web.Security.Membership.UpdateUser(currentProfile)
                            End With

                            With currentProfile
                                .ChangePassword(oldPassword, newPassword)
                            End With

                            lblMessage.Text = GetLocalResourceObject("ReceiveEmailMessageResource").ToString
                        Catch ex As Exception
                            lblMessage.Text = GetLocalResourceObject("FailEmailMessageResource").ToString
                        End Try
                    Else
                        AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.SuccessfulSecurityAnswer, String.Empty)
                        RaiseEvent BehaviorProcess(CurrentUserName)
                    End If
                Catch ex As MembershipPasswordException
                    AnswerAttempts += 1

                    If stateLockedCount Then
                        For attempt As Integer = 0 To Membership.MaxInvalidPasswordAttempts
                            Membership.ValidateUser(CurrentUserName, "_invalidPassword_")
                        Next
                    End If

                    If Not .IsLockedOut AndAlso invalidAnswerAttempt = AnswerAttempts Then
                        If Not currentProfile.AccountLockoutNotification Then
                            AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.LockedAccount, String.Empty)

                            Dim configurationEmailSend As New InMotionGIT.FrontOffice.Contracts.Parameter

                            With configurationEmailSend

                                .TemplateName = "Authentication\SendLockedAccountEmail"
                                .UserType = currentProfile.UserType.ToString
                                .Language = currentProfile.Language
                                .To = currentProfile.Email

                                .ParameterInternal = New Dictionary(Of String, String)
                                With .ParameterInternal
                                    .Add("FirstName", currentProfile.FirstName)
                                    .Add("LastName", currentProfile.LastName)
                                    .Add("RealUserName", currentProfile.RealUserName)
                                    .Add("name", "VisualTIME user")
                                End With
                            End With

                            InMotionGIT.FrontOffice.Proxy.Helpers.Email.SendMailWithTemplate(configurationEmailSend)

                        End If

                        BlokedAccountPanel.Visible = True
                    Else
                        AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.FailedSecurityAnswer, String.Empty)

                        Dim configurationEmailSend As New InMotionGIT.FrontOffice.Contracts.Parameter

                        With configurationEmailSend

                            .TemplateName = "AttemptAccess"
                            .UserType = currentProfile.UserType.ToString
                            .Language = currentProfile.Language
                            .To = currentProfile.Email

                            .ParameterInternal = New Dictionary(Of String, String)
                            With .ParameterInternal
                                .Add("FirstName", currentProfile.FirstName)
                                .Add("Email", currentProfile.Email)
                                .Add("Date", Date.Now.ToString())
                                .Add("IP", InMotionGIT.Common.Helpers.Connection.GetIPOnlyRequest())
                                .Add("name", "VisualTIME user")
                            End With
                        End With

                        InMotionGIT.FrontOffice.Proxy.Helpers.Email.SendMailWithTemplate(configurationEmailSend)

                        SecurityAnswerPanel.Visible = True
                    End If
                End Try
            End With
        End If
    End Sub

#End Region

End Class