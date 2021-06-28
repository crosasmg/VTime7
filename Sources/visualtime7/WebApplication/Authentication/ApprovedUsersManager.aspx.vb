#Region "using"

Imports DevExpress.Web.Data
Imports System.Data
Imports GIT.Core
Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper
Imports System.IO
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Imports InMotionGIT.FrontOffice.Proxy

#End Region

Partial Class Authentication_ApprovedUsersManager
    Inherits PageBase

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UsersGridView.DataBind()
    End Sub

#End Region

#Region "Controls Events"

    Protected Sub btnTemplate_Click(sender As Object, e As EventArgs)
        Dim button As ASPxButton = TryCast(sender, ASPxButton)

        If button Is Nothing Then
            Return
        End If

        Dim visibleIndex As Integer = Integer.Parse(button.CommandArgument)

        BehaviorControls(UsersGridView.GetDataRow(visibleIndex).ItemArray(0))
        popupUserProfileData.ShowOnPageLoad = True
    End Sub

#End Region

#Region " UsersGridView Events"

    Protected Sub UsersGridView_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles UsersGridView.DataBinding
        UsersGridView.DataSource = GetAllUsers()
    End Sub

    Protected Sub UsersGridView_RowUpdated(ByVal sender As Object, ByVal e As ASPxDataUpdatedEventArgs) Handles UsersGridView.RowUpdated
        UsersGridView.DataBind()
    End Sub

    Protected Sub UsersGridView_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles UsersGridView.RowUpdating
        Dim user As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser = System.Web.Security.Membership.GetUser(e.Keys("OriginalUserName").ToString)

        With user
            '.Email = e.NewValues("Email")
            .Comment = e.NewValues("Comment")
            .IsApproved = e.NewValues("IsApproved")

            Membership.UpdateUser(user)

            If .IsApproved Then
                Dim subject As String = String.Empty
                user.Password = user.GetPassword

                If user.LanguageName.IsEmpty Then
                    user.LanguageName = InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(user.LanguageID)
                End If

                AddUsersSecurityTrace(.Email, EnumUserSecurity.SuccessfulApprovedAccount, String.Empty)

                Dim parameterConfigurations As New InMotionGIT.FrontOffice.Contracts.Parameter
                With parameterConfigurations
                    .To = user.Email
                    .TemplateName = "Authentication\ApprovedUsersManager"
                    .UserType = user.UserType.ToString
                    .Language = user.LanguageName
                    .Email = user.Email
                    .ParameterInternal = New Dictionary(Of String, String)
                    With .ParameterInternal
                        .Add("Password", user.Password)
                        .Add("FirstName", user.FirstName)
                        .Add("LastName", user.LastName)
                        .Add("RealUserName", user.UserName)
                    End With
                End With
                InMotionGIT.FrontOffice.Proxy.Helpers.Email.SendMailWithTemplate(parameterConfigurations)
            End If

            If .IsLockedOut AndAlso Not e.NewValues("IsLockedOut") Then
                .UnlockUser()
                AddUsersSecurityTrace(.Email, EnumUserSecurity.UnlockedAccount, String.Empty)
            End If
        End With

        With user
            .UserType = InMotionGIT.FrontOffice.Support.Helpers.Enumerations.GetEnumValue(New InMotionGIT.Membership.Providers.Enumerations.enumUserType, e.NewValues("UserType").ToString)
            System.Web.Security.Membership.UpdateUser(user)
        End With

        e.Cancel = True
        UsersGridView.CancelEdit()
    End Sub

    Protected Sub UsersGridView_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles UsersGridView.CustomCallback
        Select Case e.Parameters.ToLower

            Case "delete"
                Dim keyValues As Generic.List(Of Object) = UsersGridView.GetSelectedFieldValues("OriginalUserName")

                For Each key As Object In keyValues
                    Membership.DeleteUser(key)
                Next key

                UsersGridView.DataBind()
        End Select
    End Sub

#End Region

#Region "Methods"

    Private Function GetAllUsers() As DataTable
        Dim membershipUsersCollection As MembershipUserCollection = Membership.GetAllUsers()
        Dim newRow As DataRow
        Using resultTable As New DataTable("Users")
            Dim currentProfile As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser

            With resultTable.Columns
                .Add("OriginalUserName", GetType(String))
                .Add("UserName", GetType(String))

                .Add("UserType", GetType(String))
                .Add("Email", GetType(String))

                .Add("CreationDate", GetType(DateTime))
                .Add("LastLoginDate", GetType(DateTime))
                .Add("LastActivityDate", GetType(DateTime))
                .Add("LastLockoutDate", GetType(DateTime))
                .Add("IsLockedOut", GetType(Boolean))
                .Add("PasswordQuestion", GetType(String))
                .Add("Comment", GetType(String))
                .Add("IsApproved", GetType(Boolean))
                .Add("LastPasswordChangedDate", GetType(DateTime))
                .Add("IsOnline", GetType(Boolean))
            End With

            For Each membershipUser As MembershipUser In membershipUsersCollection
                With membershipUser

                    If Not .IsApproved Then
                        currentProfile = System.Web.Security.Membership.GetUser(.UserName)

                        newRow = resultTable.NewRow

                        newRow(0) = .UserName

                        With currentProfile
                            newRow(1) = .RealUserName
                            newRow(2) = .UserType
                        End With

                        newRow(3) = .Email

                        newRow(4) = .CreationDate
                        newRow(5) = .LastLoginDate
                        newRow(6) = .LastActivityDate
                        newRow(7) = .LastLockoutDate
                        newRow(8) = .IsLockedOut
                        newRow(9) = .PasswordQuestion
                        newRow(10) = .Comment
                        newRow(11) = .IsApproved
                        newRow(12) = .LastPasswordChangedDate
                        newRow(13) = .IsOnline

                        resultTable.Rows.Add(newRow)
                    End If
                End With
            Next

            Return resultTable
        End Using
    End Function

    Private Sub BehaviorControls(userProfileName As String)
        Dim userInformation As UserService.UserInformation

        Using securityServices As UserService.UsersClient = New UserService.UsersClient()
            userInformation = securityServices.RetrieveUserInformation(userProfileName)
        End Using

        Dim currentUserControlAgent As AccountInformationControl = AccountInformationRoundPanel.FindControl("AccountInformation1")
        Dim currentUserControlPersonalInformation As PersonalInformationControl = PersonalInformationRoundPanel.FindControl("PersonalInformation1")
        Dim currenUserPreferencesControl As UserPreferencesControl = PreferencesRoundPanel.FindControl("UserPreferencesControl1")

        Dim currentComboBox As ASPxComboBox = currenUserPreferencesControl.FindControl("ThemeList")
        Dim themes As String() = Directory.GetDirectories(String.Format("{0}App_Themes", Request.PhysicalApplicationPath))

        currentComboBox.Items.Clear()

        For Each theme As String In themes
            currentComboBox.Items.Add(theme.Substring(theme.LastIndexOf("\") + 1))
        Next

        currentUserControlPersonalInformation.ShowHideControls(userInformation.UserType.ToString)

        PreferencesRoundPanel.Visible = True

        currentUserControlAgent.SetUserData(userInformation)
        currentUserControlPersonalInformation.SetUserData(userInformation)
        currenUserPreferencesControl.SetUserData(userInformation)
    End Sub

#End Region

End Class
