#Region "using"

Imports DevExpress.Web.ASPxEditors
Imports GIT.Core
Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper

#End Region

Partial Class Authentication_ForgotPassword
    Inherits PageBase

    Protected Sub SendButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SendButton.Click
        Dim users As MembershipUserCollection = Membership.FindUsersByEmail(EmailAddressTextBox.Text)

        If Not IsNothing(users) AndAlso users.Count > 0 Then
            InvalidEmailLabel.Visible = False

            AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.ValidateEmail, String.Empty)

            StepMultiView.ActiveViewIndex = 2

            Dim currentTextBox As ASPxTextBox = ProfileSecurityControl1.FindControl("EmailAddressTextBox")
            Dim currentMemo As ASPxMemo = ProfileSecurityControl1.FindControl("SecurityQuestionMemo")

            For Each user As MembershipUser In users
                With user
                    currentTextBox.Text = EmailAddressTextBox.Text
                    currentMemo.Text = .PasswordQuestion

                    With ProfileSecurityControl1
                        .CurrentUserName = user.UserName
                        .RecoverPassword = True
                    End With
                End With
            Next
            InMotionGIT.FrontOffice.Support.Helpers.SecurityHandler.SecurityTest(False)
        Else
            Dim message As String = String.Empty
            If Not InMotionGIT.FrontOffice.Support.Helpers.SecurityHandler.SecurityTest(True) Then
                message = String.Format(InMotionGIT.FrontOffice.Support.My.Resources.SecurityValidate, InMotionGIT.Common.Helpers.Connection.GetIPEnviroment())
                SendButton.Enabled = False
            End If
            AddUsersSecurityTrace(EmailAddressTextBox.Text, EnumUserSecurity.InvalidEmail, String.Empty)
            If message.IsEmpty() Then
                message = Me.GetLocalResourceObject("UserMailMessageResource").ToString()
            End If
            With InvalidEmailLabel
                .Text = message
                .Visible = True
            End With
        End If

    End Sub

    Protected Sub ChangeAddressSecurityControl1_BehaviorProcess(ByVal currentUserName As String, ByVal currentEmail As String,
                                                                ByVal currentSecurityQuestion As String) Handles ChangeAddressSecurityControl1.BehaviorProcess
        StepMultiView.ActiveViewIndex = 2

        Dim currentTextBox As ASPxTextBox = ProfileSecurityControl1.FindControl("EmailAddressTextBox")
        Dim currentMemo As ASPxMemo = ProfileSecurityControl1.FindControl("SecurityQuestionMemo")

        currentTextBox.Text = currentEmail
        currentMemo.Text = currentSecurityQuestion

        With ProfileSecurityControl1
            .CurrentUserName = currentUserName
            .RecoverPassword = False
        End With
    End Sub

    Protected Sub ProfileSecurityControl1_BehaviorProcess(ByVal currentUserName As String) Handles ProfileSecurityControl1.BehaviorProcess
        Dim currentProfile As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser = System.Web.Security.Membership.GetUser(currentUserName)

        If currentProfile.UserType = InMotionGIT.Membership.Providers.Enumerations.enumUserType.User Then
            StepMultiView.ActiveViewIndex = 3
            UserNameSecurityControl1.CurrentUserName = currentUserName
        Else
            StepMultiView.ActiveViewIndex = 4
            AddressSecurityControl1.CurrentUserName = currentUserName
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim currentView As Integer

        If Not IsNothing(Context.Request.QueryString("View")) AndAlso Not String.IsNullOrEmpty(Context.Request.QueryString("View")) Then
            currentView = Context.Request.QueryString("View")
            StepMultiView.ActiveViewIndex = currentView

            'Else
            'StepMultiView.ActiveViewIndex = 0
        End If

        If Not InMotionGIT.FrontOffice.Support.Helpers.SecurityHandler.SecurityTest() Then
            SendButton.Enabled = False
            InvalidEmailLabel.Visible = True
            InvalidEmailLabel.Text = String.Format(InMotionGIT.FrontOffice.Support.My.Resources.SecurityValidate, InMotionGIT.Common.Helpers.Connection.GetIPEnviroment())
        End If

    End Sub

End Class