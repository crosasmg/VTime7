#Region "using"

Imports GIT.Core.Helpers.Language
Imports GIT.Core.Helpers.Enumerations
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxClasses
Imports GIT.Core
Imports System.IO
Imports Dropthings.Web.Framework
Imports InMotionGIT.FrontOffice.Support.AuthenticationHelper
Imports InMotionGIT.FrontOffice.Support.User
Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.Correspondence.Support.Mail

#End Region

Partial Class Authentication_UserRegister
    Inherits PageBase

#Region "Public Properties"

    Public Property CurrentUserName() As String
        Get
            Return ViewState("UserName")
        End Get
        Set(ByVal value As String)
            ViewState("UserName") = value
        End Set
    End Property

    Public Property CurrentUserType() As String
        Get
            Return ViewState("CurrentUserType")
        End Get
        Set(ByVal value As String)
            ViewState("CurrentUserType") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim type As String = String.Empty

            If Not IsNothing(Context.Request.QueryString("tipo")) AndAlso Not String.IsNullOrEmpty(Context.Request.QueryString("tipo")) Then
                type = Context.Request.QueryString("tipo")
            End If

            If Not String.IsNullOrEmpty(type) Then
                BehaviorControls()
            End If

        Else
            If Not String.IsNullOrEmpty(CurrentUserType) Then
                Dim currentUserControlTermsOfUse As TermsOfUseUserControl = TermsOfUseRoundPanel.FindControl("TermsOfUseUserControl1")

                currentUserControlTermsOfUse.LoadPageHTML(CurrentUserType)
            End If
        End If
    End Sub

#End Region

#Region "Controls Events"

    Protected Sub RegisterButton_Click(ByVal sender As Object, e As EventArgs) Handles RegisterButton.Click
        Dim currentAccountInformation As AccountInformationControl = AccountInformationRoundPanel.FindControl("AccountInformation1")
        Dim currentPersonalInformation As PersonalInformationControl = PersonalInformationRoundPanel.FindControl("PersonalInformation1")
        Dim currentUserPreferences As UserPreferencesControl = PreferencesRoundPanel.FindControl("UserPreferencesControl1")
        Dim currentProducerInformation As ProducerInformationControl = ProducerInformationRoundPanel.FindControl("ProducerInformationControl1")
        Dim currentContractOwnerInformation As ContractOwnerInformationControl = ContractOwnerInformationRoundPanel.FindControl("ContractOwnerInformationControl1")

        Dim userName As String = Nothing
        Dim password As String = Nothing
        Dim eMail As String = Nothing
        Dim secretQuestion As String = Nothing
        Dim secretAnswer As String = Nothing
        Dim assignedRole As String = Nothing
        Dim userInformation As New UserService.UserInformation

        If Not String.IsNullOrEmpty(CurrentUserName) Then
            currentAccountInformation.GetSecurityData(userInformation)
            currentProducerInformation.GetUserData(CurrentUserType, userInformation)
            currentPersonalInformation.GetUserData(userInformation)
            currentContractOwnerInformation.GetUserData(CurrentUserType, userInformation)
            currentUserPreferences.GetUserData(userInformation)

            userInformation.UserType = [Enum].Parse(GetType(UserService.EnumPortaUserType), CurrentUserType)

            With UserData
                userInformation.ClientID = .Get("ClientID")
                userInformation.ProducerID = .Get("ProducerID")
                userInformation.UserID = .Get("UserID")
            End With

            Using securityServices As UserService.UsersClient = New UserService.UsersClient()
                securityServices.UpdatePortalUser(userInformation, CurrentUserName, String.Empty)
            End Using

            With lblMessage
                .Visible = True
                .Text = GetLocalResourceObject("lblMessageText").ToString
            End With

            RegisterButton.Enabled = False
            CancelButton.Text = GetGlobalResourceObject("Resource", "Close")

        Else
            Dim currentRadioButton As ASPxRadioButtonList = UserType1.FindControl("IamRadioButtonList")
            CurrentUserType = currentRadioButton.SelectedItem.Value.ToString

            currentAccountInformation.GetSecurityData(userInformation)
            currentUserPreferences.GetUserData(userInformation)
            currentPersonalInformation.GetUserData(userInformation)

            Select Case CurrentUserType
                Case "Agent"
                    currentProducerInformation.GetUserData(CurrentUserType, userInformation)
                    userInformation.UserType = UserService.EnumPortaUserType.Agent

                Case "Client"
                    currentContractOwnerInformation.GetUserData(CurrentUserType, userInformation)
                    userInformation.UserType = UserService.EnumPortaUserType.Client

                Case Else
                    userInformation.UserType = UserService.EnumPortaUserType.User
            End Select

            userInformation.LanguageID = LanguageId

            Using securityServices As UserService.UsersClient = New UserService.UsersClient()
                securityServices.CreatePortalUser(userInformation)
            End Using

            CurrentUserName = userName

            StepMultiView.ActiveViewIndex = 2
        End If
    End Sub

    Protected Sub RedirectFormBtn_Click(sender As Object, e As EventArgs) Handles RedirectFormBtn.Click
        ValidateFormsAuthenticationTicket(CurrentUserName, False, CurrentUserType, Nothing, String.Empty)
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If Not String.IsNullOrEmpty(CurrentUserType) Then
            Select Case CurrentUserType
                Case "Agent"
                    Response.Redirect("~/dropthings/Default.aspx")

                Case Else
                    FormsAuthentication.RedirectFromLoginPage(CurrentUserName, False)

            End Select
        End If
    End Sub

    Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        If Not IsNothing(Context.Request.QueryString("tipo")) AndAlso Not String.IsNullOrEmpty(Context.Request.QueryString("tipo")) Then
            FormsAuthentication.RedirectFromLoginPage(CurrentUserName, False)
        Else
            StepMultiView.ActiveViewIndex = 0
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub BehaviorControls()
        Dim userInformation As UserService.UserInformation

        RegisterButton.Text = GetLocalResourceObject("SaveChangesResource").ToString()
        StepMultiView.ActiveViewIndex = 1

        TermsOfUseRoundPanel.Visible = False
        CurrentUserName = UserInfo.UserName

        Using securityServices As UserService.UsersClient = New UserService.UsersClient()
            userInformation = securityServices.RetrieveUserInformation(CurrentUserName)
        End Using

        Dim currentAccountInformation As AccountInformationControl = AccountInformationRoundPanel.FindControl("AccountInformation1")
        Dim currentPersonalInformation As PersonalInformationControl = PersonalInformationRoundPanel.FindControl("PersonalInformation1")
        Dim currentUserPreferences As UserPreferencesControl = PreferencesRoundPanel.FindControl("UserPreferencesControl1")
        Dim currentProducerInformation As ProducerInformationControl = ProducerInformationRoundPanel.FindControl("ProducerInformationControl1")

        Dim currentComboBox As ASPxComboBox = currentUserPreferences.FindControl("ThemeList")
        Dim themes As String() = Directory.GetDirectories(String.Format("{0}App_Themes", Request.PhysicalApplicationPath))

        currentComboBox.Items.Clear()

        For Each theme As String In themes
            currentComboBox.Items.Add(theme.Substring(theme.LastIndexOf("\") + 1))
        Next

        CurrentUserType = userInformation.UserType.ToString

        currentPersonalInformation.ShowHideControls(CurrentUserType)

        ContractOwnerInformationRoundPanel.Visible = False
        PreferencesRoundPanel.Visible = True
        ProducerInformationRoundPanel.Visible = False

        Select Case userInformation.UserType
            Case UserService.EnumPortaUserType.Agent
                ProducerInformationRoundPanel.Visible = True
                currentProducerInformation.SetUserData(userInformation)

            Case UserService.EnumPortaUserType.Client
                ContractOwnerInformationRoundPanel.Visible = True

                Dim currentContractOwnerInformation As ContractOwnerInformationControl = ContractOwnerInformationRoundPanel.FindControl("ContractOwnerInformationControl1")
                currentContractOwnerInformation.SetUserData(userInformation)

            Case Else
        End Select

        currentAccountInformation.SetUserData(userInformation)
        currentPersonalInformation.SetUserData(userInformation)
        currentUserPreferences.SetUserData(userInformation)

        With UserData
            .Add("ClientID", userInformation.ClientID)
            .Add("ProducerID", userInformation.ProducerID)
            .Add("UserID", userInformation.UserID)
        End With
    End Sub

    Public Sub Navigation(ByVal sender As Object, ByVal tagName As String)
        Dim currentRadioButton As ASPxRadioButtonList = UserType1.FindControl("IamRadioButtonList")
        Dim selectedUserType As String = currentRadioButton.SelectedItem.Value.ToString

        If StepMultiView.ActiveViewIndex = 0 Then
            Select Case tagName
                Case "Cancel"
                Case "Next"

                    StepMultiView.ActiveViewIndex = 1

                    Dim currentUserControlPersonalInformation As PersonalInformationControl = PersonalInformationRoundPanel.FindControl("PersonalInformation1")
                    Dim currentUserControlTermsOfUse As TermsOfUseUserControl = TermsOfUseRoundPanel.FindControl("TermsOfUseUserControl1")

                    currentUserControlTermsOfUse.LoadPageHTML(selectedUserType)
                    currentUserControlPersonalInformation.ShowHideControls(selectedUserType)

                    ContractOwnerInformationRoundPanel.Visible = False
                    ProducerInformationRoundPanel.Visible = False
                    TermsOfUseRoundPanel.Visible = True
                    CurrentUserType = selectedUserType

                    Select Case selectedUserType
                        Case "Agent"
                            ProducerInformationRoundPanel.Visible = True

                        Case "Client"
                            ContractOwnerInformationRoundPanel.Visible = True

                        Case Else
                    End Select

                Case Else
            End Select
        End If
    End Sub

#End Region

End Class
