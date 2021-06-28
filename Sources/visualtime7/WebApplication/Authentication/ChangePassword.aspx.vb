#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Proxy.UserService
Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper

#End Region

Partial Class Authentication_ChangePassword
    Inherits PageBase

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim minPassLength As Integer = Membership.MinRequiredPasswordLength

        With NewPasswordTextBox.ValidationSettings.RegularExpression
            .ValidationExpression = String.Format(CultureInfo.InvariantCulture, ".{{{0},20}}", minPassLength)
            .ErrorText = GetLocalResourceObject("NewPasswordTextBoxResource.ValidationSettings.RegularExpression.ErrorText").ToString
            .ErrorText = .ErrorText.Replace(" 6 ", String.Format(CultureInfo.InvariantCulture, " {0} ", minPassLength))

        End With

    End Sub

#End Region

#Region "Controls Events"

    Protected Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Dim userName As String = UserInfo.UserName
        Dim currentUser As MembershipUser = Membership.GetUser(userName)

        lblMessage.Visible = False

        If Not IsNothing(currentUser) Then
            Dim actualPassword As String = currentUser.GetPassword()
            Dim result As AuthenticationInformation.EnumAuthenticationStatus

            If Not String.Equals(actualPassword, NewPasswordTextBox.Text, StringComparison.CurrentCultureIgnoreCase) Then

                If InMotionGIT.Membership.Providers.Helper.ValidatePassword(NewPasswordTextBox.Text) Then

                    Using securityServices As UserService.UsersClient = New UserService.UsersClient()
                        Try
                            result = securityServices.ChangePasswordPortalUser(userName, OldPasswordTextBox.Text, NewPasswordTextBox.Text)

                            Select Case result
                                Case AuthenticationInformation.EnumAuthenticationStatus.None
                                    With lblMessage
                                        .Text = GetLocalResourceObject("lblMessageSuccesfully").ToString()
                                        .Visible = True

                                        OldPasswordTextBox.Enabled = False
                                        NewPasswordTextBox.Enabled = False
                                        RetypeNewPasswordTextBox.Enabled = False
                                        SaveButton.Enabled = False
                                        CancelButton.Text = GetLocalResourceObject("CancelButtonText").ToString()
                                        PasswordCompareValidator.Visible = False
                                        HelpLabel.Visible = False
                                    End With

                                Case AuthenticationInformation.EnumAuthenticationStatus.PasswordNotMatch
                                    AddUsersSecurityTrace(currentUser.Email, EnumUserSecurity.FailedChangedPassword, String.Empty)

                                    With lblMessage
                                        .Text = GetLocalResourceObject("lblMessageNotMatch").ToString()
                                        .Visible = True
                                    End With

                                Case AuthenticationInformation.EnumAuthenticationStatus.UsedPassword
                                    AddUsersSecurityTrace(currentUser.Email, EnumUserSecurity.FailedChangedPassword, String.Empty)

                                    With lblMessage
                                        .Text = GetLocalResourceObject("lblExistencePassword").ToString()
                                        .Visible = True
                                    End With

                            End Select
                        Catch ex As Exception
                            With lblMessage
                                .Text = ex.Message
                                .Visible = True
                            End With
                        End Try
                    End Using
                Else
                    With lblMessage
                        .Text = InMotionGIT.FrontOffice.Support.My.Resources.IncorrectPasswordFormat
                        .Visible = True
                        .ForeColor = Drawing.Color.Red
                    End With
                End If
            Else
                With lblMessage
                    .Text = GetLocalResourceObject("SamePasswordText").ToString()
                    .Visible = True
                End With
            End If
        Else
            With lblMessage
                .Text = String.Format(GetLocalResourceObject("TechnicalFailureText").ToString(), userName)
                .Visible = True
            End With
        End If
    End Sub

    Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        Dim userName As String = String.Empty
        Dim isMaster = Request.QueryString("IsMaster").IsNotEmpty()
        Dim scriptText As String = "var parentWindow = window.parent;"

        If Not IsNothing(Request.QueryString("UserName")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("UserName")) Then
            userName = Request.QueryString("UserName")
        Else
            userName = UserInfo.UserName
        End If

        If String.Equals(CancelButton.Text, GetLocalResourceObject("CancelButtonText").ToString(), StringComparison.CurrentCultureIgnoreCase) Then
            If isMaster Then
                scriptText = scriptText &
                        "parentWindow.loadingPanel.ShowInElement(parentWindow.ChangePasswordPopupControl.GetContentIFrame());"
                scriptText = scriptText & " parentWindow.location.href='/dropthings/LogOff.aspx';"
            Else
                scriptText = scriptText & " parentWindow.ChangePasswordPopupControl.Hide();"
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "CounterScript1", scriptText, True)
        Else

            Dim currentProfile As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser = System.Web.Security.Membership.GetUser(userName)

            If currentProfile.FirstTimePasswordChange OrElse
               (Not IsNothing(Request.QueryString("mode")) AndAlso
                String.Equals(Request.QueryString("mode"), "expiration", StringComparison.CurrentCultureIgnoreCase)) Then
                If isMaster Then
                    scriptText = scriptText &
                                 "parentWindow.loadingPanel.ShowInElement(parentWindow.ChangePasswordPopupControl.GetContentIFrame());"
                    scriptText = scriptText & " parentWindow.location.href='/dropthings/LogOff.aspx';"
                Else
                    scriptText = scriptText & " parentWindow.ChangePasswordPopupControl.Hide();"
                End If
                ClientScript.RegisterStartupScript(Me.GetType(), "CounterScript1", scriptText, True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "CounterScript1",
                                       "var parentWindow = window.parent; parentWindow.ChangePasswordPopupControl.Hide();", True)
            End If
        End If
    End Sub

#End Region

End Class