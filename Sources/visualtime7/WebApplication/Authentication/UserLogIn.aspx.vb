#Region "using"

Imports System.Globalization
Imports System.Web.Script.Services
Imports System.Web.Services
Imports DevExpress.Web.ASPxEditors
Imports GIT.Core
Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Proxy.SettingsService
Imports InMotionGIT.FrontOffice.Proxy.UserService.AuthenticationInformation
Imports InMotionGIT.FrontOffice.Support
Imports InMotionGIT.Membership.Providers
Imports InMotionGIT.Seguridad.Proxy

#End Region

Partial Class Authentication_UserLogIn
    Inherits PageBase

#Region "Fields"

    Private Shared config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)

#End Region

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function LogIn(user As String, password As String, Remember As Boolean, isVisibleCompany As Boolean, companyId As Integer) As Object
        Dim State As Boolean = False
        Dim Message As String = String.Empty
        Dim IsMultiCompany As Boolean = False
        Dim IsAuthenticated As Boolean = False
        Dim url As String = String.Empty
        Dim codeScript As String = String.Empty
        Dim ShowStartUpMessage As Boolean = False
        Dim StartUpMessage As String = String.Empty
        Dim securityServices As New UserService.UsersClient
        Dim page = DirectCast(HttpContext.Current.CurrentHandler, Page)
        Dim urlRelativePath As String = page.AppRelativeVirtualPath.Replace("~", "")
        Dim IsShowRecaptcha As Boolean = False
        Dim SecurityTestResult = Nothing

        Try
            Dim userInformation As UserService.AuthenticationInformation = securityServices.PortalAuthentication(user, password, companyId, InMotionGIT.Common.Helpers.Connection.GetIPRequest())
            SecurityTestResult = InMotionGIT.FrontOffice.Support.Helpers.SecurityHandler.SecurityTest(userInformation.Status)
            If SecurityTestResult Then
                With userInformation
                    Select Case .Status
                        Case EnumAuthenticationStatus.UserValid
                            LoginSettings(userInformation, user, password, Remember, url, codeScript)
                            IsAuthenticated = True

                            InMotionGIT.FASI.Utils.Track.UserActivity.UpdateSessionOnAudit(HttpContext.Current.Session.SessionID, InMotionGIT.Common.Helpers.Connection.GetIPRequest(), userInformation.User.Email, Date.MinValue)
                            InMotionGIT.FASI.Utils.Track.UserActivity.Tracking(HttpContext.Current, HttpContext.Current.Session.SessionID, InMotionGIT.Common.Helpers.Connection.GetIPRequest(), userInformation.User.UserName, userInformation.User.Email)

                            If url.IsEmpty Then
                                url = ConfigurationManager.AppSettings("Url.WebApplication")
                            End If

                            ' Se solicita el token para el usuario.
                            If Convert.ToBoolean(ConfigurationManager.AppSettings("STS.Enable")) Then
                                TokenHelper.RequestToken(.User.Email)
                            End If
                        Case EnumAuthenticationStatus.SelectCompany
                            IsMultiCompany = True

                        Case EnumAuthenticationStatus.UserLock
                            Message = HttpContext.GetLocalResourceObject(urlRelativePath, "AccountLockedResource", System.Threading.Thread.CurrentThread.CurrentCulture)

                        Case EnumAuthenticationStatus.UserLoginFail
                            ''Message = HttpContext.GetLocalResourceObject(urlRelativePath, "MessageLoginFailResource", System.Threading.Thread.CurrentThread.CurrentCulture)
                            Message = HttpContext.GetLocalResourceObject(urlRelativePath, "MessageLoginGenericResource", System.Threading.Thread.CurrentThread.CurrentCulture)
                        Case EnumAuthenticationStatus.InvalidAccount
                            ''Message = HttpContext.GetLocalResourceObject(urlRelativePath, "InvalidMessageResource", System.Threading.Thread.CurrentThread.CurrentCulture)
                            Message = HttpContext.GetLocalResourceObject(urlRelativePath, "MessageLoginGenericResource", System.Threading.Thread.CurrentThread.CurrentCulture)
                        Case EnumAuthenticationStatus.InValidateEmail
                            ''Message = HttpContext.GetLocalResourceObject(urlRelativePath, "InvalidEmailMessageResource", System.Threading.Thread.CurrentThread.CurrentCulture)
                            Message = HttpContext.GetLocalResourceObject(urlRelativePath, "MessageLoginGenericResource", System.Threading.Thread.CurrentThread.CurrentCulture)
                        Case EnumAuthenticationStatus.InactiveUser
                            Message = HttpContext.GetLocalResourceObject(urlRelativePath, "InactiveUserMessageResource", System.Threading.Thread.CurrentThread.CurrentCulture)
                        Case Else
                            Message = .Message
                    End Select

                End With
            Else
                Message = String.Format(HttpContext.GetLocalResourceObject(urlRelativePath, "SecurityValidate", System.Threading.Thread.CurrentThread.CurrentCulture), InMotionGIT.Common.Helpers.Connection.GetIPPublic())
            End If
            If userInformation.Status <> EnumAuthenticationStatus.UserValid AndAlso userInformation.Status <> EnumAuthenticationStatus.SelectCompany Then
                If userInformation.FailedPasswordAttemptCount = (System.Web.Security.Membership.MaxInvalidPasswordAttempts - 1) AndAlso
                    config.Security.RecaptchaEnable Then
                    IsShowRecaptcha = True
                End If

                Try
                    Dim UserNameTemporal = System.Web.Security.Membership.GetUserNameByEmail(user)

                    If UserNameTemporal.IsNotEmpty() Then

                        Dim currentProfile As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser = System.Web.Security.Membership.GetUser(UserNameTemporal)

                        If currentProfile.IsNotEmpty() Then
                            SendMail(currentProfile.Language, currentProfile.Email, currentProfile.FirstName)
                        End If
                    End If
                Catch ex As Exception
                    InMotionGIT.Common.Helpers.LogHandler.TraceLog("LogIn", ex.Message)
                End Try

            End If
            State = True
        Catch ex As Exception
            State = False
            Message = HttpContext.GetLocalResourceObject(urlRelativePath, "ErrorMessage", System.Threading.Thread.CurrentThread.CurrentCulture)
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Login in Dev-express", ex.Message, ex)
        End Try

        Return New With {Key .State = State,
                         Key .SecurityValidate = SecurityTestResult,
                         Key .IsMultiCompany = IsMultiCompany,
                         Key .IsShowRecaptcha = IsShowRecaptcha,
                         Key .Message = Message,
                         Key .IsAuthenticated = IsAuthenticated,
                         Key .Url = url,
                         Key .ShowStartUpMessage = ShowStartUpMessage,
                         Key .StartUpMessage = StartUpMessage}
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CompanyLookUp() As Object
        Dim settingService As SettingsService.SettingsClient = New SettingsService.SettingsClient()
        Dim companies As MultiCompany() = settingService.MultiCompanyList
        Return companies
    End Function

#Region "Public Properties"

    Public TextBoxName As String

    Public Property IsFirstTime() As Boolean
        Get
            Return ViewState("IsFirstTime")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsFirstTime") = value
        End Set
    End Property

    Public Property SavedPassword() As String
        Get
            Return ViewState("SavedPassword")
        End Get
        Set(ByVal value As String)
            ViewState("SavedPassword") = value
        End Set
    End Property

#End Region

#Region "Controls Events"

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim companyId As Integer = 0
        Dim password As String = PasswordTextBox.Text

        If IsFirstTime AndAlso Not IsNothing(CompanyComboBox.SelectedItem) Then
            companyId = CompanyComboBox.SelectedItem.Value
        End If

        If Not String.IsNullOrEmpty(SavedPassword) Then
            password = SavedPassword
        End If

        Dim securityServices As New UserService.UsersClient
        Dim userInformation As UserService.AuthenticationInformation = securityServices.PortalAuthentication(EmailAddressTextBox.Text, password, companyId, InMotionGIT.Common.Helpers.Connection.GetIPRequest())

        With userInformation
            Select Case .Status
                Case EnumAuthenticationStatus.UserValid
                    Dim config As VisualTIME = ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection")

                    With config.Authentification
                        If .ShowStartUpMessage Then
                            If String.IsNullOrEmpty(.StartUpMessage) Then
                                lblMessage.Text = GetGlobalResourceObject("Resource", "StartUpMessage").ToString

                                Dim scriptText As String = String.Format(CultureInfo.InvariantCulture,
                                            "var parentWindow = window.parent;" &
                                            "parentWindow.LogInPopupControl.SetHeaderText('{0}');" &
                                            "parentWindow.LogInPopupControl.SetHeaderImageUrl('../images/generaluse/login/comments.png')",
                                            GetGlobalResourceObject("Resource", "RevelationHeaderText").ToString)

                                ClientScript.RegisterStartupScript(Me.GetType(), "CounterScript1", scriptText, True)
                            Else
                                lblMessage.Text = .StartUpMessage
                            End If

                            StepMultiView.ActiveViewIndex = 1
                        Else
                            LoginSettings(userInformation, String.Empty, String.Empty, False, "", "")
                        End If
                    End With

                    ' Se solicita el token para el usuario.
                    If Convert.ToBoolean(ConfigurationManager.AppSettings("STS.Enable")) Then
                        TokenHelper.RequestToken(.User.Email)
                    End If
                Case EnumAuthenticationStatus.SelectCompany
                    If Not IsFirstTime AndAlso IsNothing(CompanyComboBox.SelectedItem) Then
                        Dim settingService As SettingsService.SettingsClient = New SettingsService.SettingsClient()

                        EmailAddressTextBox.Enabled = False
                        SavedPasswordTextBox.Visible = True
                        PasswordTextBox.Visible = False

                        CompanyLabel.Visible = True
                        CompanyComboBox.Visible = True
                        IsFirstTime = True

                        Dim companies As MultiCompany() = settingService.MultiCompanyList
                        Dim item As ListEditItem

                        If companies.Count > 0 Then
                            For Each company As MultiCompany In companies
                                item = New ListEditItem

                                With item
                                    .Text = company.Name
                                    .Value = company.Identification
                                End With

                                CompanyComboBox.Items.Add(item)
                            Next

                            CompanyComboBox.Value = CompanyComboBox.Items(0).Value
                        End If

                        settingService.Close()
                    End If

                Case EnumAuthenticationStatus.UserLock
                    InvalidLogOnLabel.Visible = True
                    InvalidLogOnLabel.Text = GetLocalResourceObject("AccountLockedResource").ToString()
                    BlokedAccountPanel.Visible = True

                Case EnumAuthenticationStatus.UserLoginFail
                    InvalidLogOnLabel.Visible = True
                    InvalidLogOnLabel.Text = GetLocalResourceObject("MessageLoginFailResource").ToString

                Case EnumAuthenticationStatus.InvalidAccount
                    InvalidLogOnLabel.Visible = True
                    InvalidLogOnLabel.Text = GetLocalResourceObject("InvalidMessageResource").ToString()

                Case EnumAuthenticationStatus.InValidateEmail
                    InvalidLogOnLabel.Visible = True
                    InvalidLogOnLabel.Text = GetLocalResourceObject("InvalidEmailMessageResource").ToString()

                Case EnumAuthenticationStatus.InactiveUser
                    InvalidLogOnLabel.Visible = True
                    InvalidLogOnLabel.Text = GetLocalResourceObject("InactiveUserMessageResource").ToString()

                Case Else
                    InvalidLogOnLabel.Visible = True
                    InvalidLogOnLabel.Text = .Message
            End Select
        End With
    End Sub

    Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        If CompanyComboBox.ClientVisible Then
            Dim config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)

            Response.Redirect(config.Authentification.LinkLoginOnUrl)
        Else
            Dim scriptText As String = "var parentWindow = window.parent;" &
                                       "parentWindow.LogInPopupControl.Hide();"

            ClientScript.RegisterStartupScript(Me.GetType(), "CounterScript2", scriptText, True)
        End If

    End Sub

    Protected Sub AcceptBtn_Click(sender As Object, e As EventArgs) Handles AcceptBtn.Click
        LoginSettings(Nothing, String.Empty, String.Empty, False, "", "")
    End Sub

    Protected Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Dim config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)
        Dim scriptText As String = String.Format(CultureInfo.InvariantCulture,
                                                 "var parentWindow = window.parent;" &
                                                 "parentWindow.LogInPopupControl.SetHeaderText('{0}');" &
                                                 "parentWindow.LogInPopupControl.SetHeaderImageUrl('../images/generaluse/login/key.png')",
                                                 GetGlobalResourceObject("Resource", "LogInHeaderText").ToString)

        ClientScript.RegisterStartupScript(Me.GetType(), "CounterScript3", scriptText, True)

        StepMultiView.ActiveViewIndex = 0

        EmailAddressTextBox.Text = String.Empty
        PasswordTextBox.Text = String.Empty
        CompanyLabel.Visible = False
        CompanyComboBox.Visible = False
    End Sub

#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CompanyLabel.ClientVisible = False
            CompanyComboBox.ClientVisible = False
        End If

        If config.Security.Mode = InMotionGIT.Core.Configuration.Enumerations.EnumSecurityMode.ActiveDirectory Then
            EmailAddressTextBox.ValidationSettings.RegularExpression.ValidationExpression = ""
            EmailAddressLabel.Text = GetLocalResourceObject("UsernnameCaption")
        End If

        SetPageSettings()
        If Not IsNothing(Request.QueryString) AndAlso
            Request.QueryString.Count <> 0 Then
            If Not IsNothing(Request.QueryString("View")) Then
                StepMultiView.ActiveViewIndex = Request.QueryString("View")
                lblMessage.Text = GetGlobalResourceObject("Resource", "StartUpMessage").ToString()
            End If
            If Not IsNothing(Request.QueryString("CallPage")) Then
                AcceptBtn.Visible = False
                CancelBtn.Visible = False
            End If

        End If

        If Not InMotionGIT.FrontOffice.Support.Helpers.SecurityHandler.SecurityTest() Then
            LoginButton.Enabled = False
            InvalidLogOnLabel.Visible = True
            InvalidLogOnLabel.ClientVisible = True
            InvalidLogOnLabel.ForeColor = Drawing.Color.Red
            InvalidLogOnLabel.Text = String.Format(InMotionGIT.FrontOffice.Support.My.Resources.SecurityValidate, InMotionGIT.Common.Helpers.Connection.GetIPEnviroment())
        End If

    End Sub

#End Region

#Region "Methods"

    Public Shared Sub SendMail(Language As String, Email As String, FirstName As String)
        Dim configurationEmailSend As New InMotionGIT.FrontOffice.Contracts.Parameter

        With configurationEmailSend

            .TemplateName = "AccessNotification"
            .UserType = ""
            .Language = Language
            .To = Email

            .ParameterInternal = New Dictionary(Of String, String)
            With .ParameterInternal
                .Add("FirstName", FirstName)
                .Add("Email", Email)
                .Add("Date", Date.Now.ToString())
                .Add("IP", InMotionGIT.Common.Helpers.Connection.GetIPOnlyRequest())
                .Add("name", "VisualTIME user")
            End With
        End With

        InMotionGIT.FrontOffice.Proxy.Helpers.Email.SendMailWithTemplate(configurationEmailSend)
    End Sub

    Private Shared Sub LoginSettings(userInformation As UserService.AuthenticationInformation, email As String, password As String, SavePassword As Boolean, ByRef url As String, ByRef codeScript As String)
        Dim securityMod = config.Security.Mode

        Using client As New InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient
            client.UserStatusChange(userInformation.User.UserName, True)
        End Using

        If IsNothing(userInformation) Then
            Dim companyId As Integer = 0
            'Dim password As String = PasswordTextBox.Text

            'If IsFirstTime AndAlso Not IsNothing(CompanyComboBox.SelectedItem) Then
            '    companyId = CompanyComboBox.SelectedItem.Value
            'End If

            'If Not String.IsNullOrEmpty(SavedPassword) Then
            '    password = SavedPassword
            'End If

            Using securityServices As New UserService.UsersClient
                userInformation = securityServices.PortalAuthentication(email, password, companyId, InMotionGIT.Common.Helpers.Connection.GetIPRequest())
            End Using
        End If

        'Create a new cookie, passing the name into the constructor and set the cookies value
        Dim cookie As New HttpCookie("EmailAddress") With {.Value = email,
                                                           .Expires = Now.AddHours(60)}
        cookie.HttpOnly = True
        'Add the cookie
        HttpContext.Current.Response.Cookies.Add(cookie)

        With userInformation

            InMotionGIT.FrontOffice.Support.Helpers.Language.UpdateLanguage(userInformation)

            If Not IsNothing(.SessionEnviroment) AndAlso .SessionEnviroment.Count > 0 Then
                For Each item In .SessionEnviroment

                    With item
                        If .Key.StartsWith("Application.") Then
                            HttpContext.Current.Application.Lock()
                            HttpContext.Current.Application.Add(.Key.Replace("Application.", ""), .Value)
                            HttpContext.Current.Application.UnLock()
                        Else
                            HttpContext.Current.Session.Add(.Key, .Value)
                        End If
                    End With
                Next

                HttpContext.Current.Session.Add("SessionID", HttpContext.Current.Session.SessionID)

            End If

            Dim securityLevel As Integer = userInformation.User.SecurityLevel

            If securityLevel = 9 Then
                Dim userAsigne As String = userInformation.User.RolesAssigned
                securityLevel = InMotionGIT.Membership.Providers.Helper.RoleSecurityLavel(userAsigne, "FrontOfficeConnectionString")
            End If

            HttpContext.Current.Session.Add("SecurityLevel", securityLevel)

            InMotionGIT.FrontOffice.Support.AuthenticationHelper.ValidateFormsAuthenticationTicketOnly(.User.UserName, SavePassword, "ClientRedirect", url, codeScript)

            Dim _theme As String = String.Empty
            Dim _language As String = String.Empty
            Dim _languageId As String = 0

            If String.IsNullOrEmpty(userInformation.User.Theme) Then
                _theme = config.General.DefaultTheme
            Else
                _theme = userInformation.User.Theme
            End If

            If String.IsNullOrEmpty(userInformation.User.Language) Then
                _language = config.General.DefaultLanguage
            Else
                _language = userInformation.User.Language
            End If

            If userInformation.User.LanguageID = 0 Then
                _languageId = InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext
            Else
                _languageId = userInformation.User.LanguageID
            End If

            HttpContext.Current.Session("UserPageSetup") = Nothing

            InMotionGIT.Membership.Providers.Helper.InformationUserLoad(.User.UserName, New FrontOfficeMembershipInfo With {.IsAdministrator = userInformation.User.IsAdministrator,
                                                                                                                            .IsEmployee = userInformation.User.IsEmployee,
                                                                                                                            .IsClient = userInformation.User.IsClient,
                                                                                                                            .IsProducer = userInformation.User.IsProducer,
                                                                                                                            .Theme = _theme,
                                                                                                                            .LanguageId = _languageId,
                                                                                                                            .Language = _language,
                                                                                                                            .UserId = userInformation.User.UserID,
                                                                                                                            .UserName = userInformation.User.UserName,
                                                                                                                            .AllowScheduler = userInformation.User.AllowScheduler,
                                                                                                                            .CurrentPageId = userInformation.User.CurrentPageId,
                                                                                                                            .ProducerID = userInformation.User.ProducerID,
                                                                                                                            .ClientID = userInformation.User.ClientID,
                                                                                                                            .RoleName = userInformation.User.RolesAssigned,
                                                                                                                            .PasswordNeverExpires = userInformation.User.PasswordNeverExpires})
            Dim setup As InMotionGIT.FrontOffice.Contracts.UserPageSetup = InMotionGIT.FrontOffice.Proxy.Helpers.FrontOfficeHelper.GetUserPageSetupAll(userInformation.User.UserID, userInformation.User.LanguageID, userInformation.User.RolesAssigned)
            HttpContext.Current.Session("UserPageSetup") = setup
        End With
    End Sub

    Private Sub SetPageSettings()
        Dim settingValue As Boolean = False

        settingValue = config.Authentification.RememberMeVisible

        If settingValue Then
            RememberCheckBox.Visible = True
        Else
            RememberCheckBox.Visible = False
        End If

        settingValue = config.Authentification.RememberMeDisabled

        If settingValue Then
            RememberCheckBox.Enabled = True
        Else
            RememberCheckBox.Enabled = False
        End If

        'Grab the cookie
        Dim cookie As HttpCookie = Request.Cookies("EmailAddress")
        'Write the cookie value
        If Not IsNothing(cookie) AndAlso Not IsPostBack Then
            EmailAddressTextBox.Text = cookie.Value
            TextBoxName = "PasswordTextBox"
            PasswordTextBox.Focus()
        Else
            TextBoxName = "EmailAddressTextBox"
            EmailAddressTextBox.Focus()
        End If
    End Sub

    Private Sub ChangeSecurityQuestionAndAnswer()
        Dim users As MembershipUserCollection = Membership.GetAllUsers
        Dim result As Boolean
        Dim secretQuestion As String = GetGlobalResourceObject("Resource", "SecretQuestionColor").ToString()

        For Each user As MembershipUser In users
            user.UnlockUser()

            result = user.ChangePasswordQuestionAndAnswer(user.GetPassword, secretQuestion, AuthenticationHelper.CreateRandomSecretAnswer())

            If Not result Then
                Exit For
            End If
        Next
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Session("IsFirtView") = True
    End Sub

#End Region

End Class