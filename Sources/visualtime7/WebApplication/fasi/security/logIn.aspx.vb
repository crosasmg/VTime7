#Region "using"

Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Web.Script.Services
Imports System.Web.Services
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.FASI.Contracts.Enumerations
Imports InMotionGIT.Membership.Providers
Imports InMotionGIT.Seguridad.Proxy
Imports Newtonsoft.Json
Imports Thinktecture.IdentityModel.Client
Imports System.Globalization

#End Region

Partial Class fasi_security_logIn
    Inherits System.Web.UI.Page

#Region "Fields"

    Private Shared BaseUrl As String = ConfigurationManager.AppSettings("API.FASI.URL") & "/api/"
    Private Shared config As FASIConfiguration = FASIConfiguration.Configuration()

#End Region

#Region "Page Methods"

    Public Property UserNameField As String

    Private Sub fasi_security_logIn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim config As FASIConfiguration = InMotionGIT.Core.Configuration.FASIConfiguration.Configuration()

        If config.Security.Mode = FASI.Enumerations.EnumSecurityMode.Sesame Then
            If InMotionGIT.Integration.Sesame.Manager.HasRequestResponse() Then
                Dim body As InMotionGIT.Integration.Sesame.Result = InMotionGIT.Integration.Sesame.Manager.DecryptAssertionByResult()

                UserNameField = body.Login

            Else
                HttpContext.Current.Response.Redirect(InMotionGIT.Integration.Sesame.Manager.GetRedirectUrl())
            End If
        End If
    End Sub

#End Region

#Region "WebMethods"

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CompanyLookUp() As Object
        Dim result As New List(Of InMotionGIT.FASI.Contracts.General.MultiCompany)

        Using client As New HttpClient()
            client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", GetAnonymousToken)
            Dim Response = client.GetAsync(String.Format("{0}{1}", BaseUrl, "fasi/v1/MultiCompany")).Result

            If Response.IsSuccessStatusCode Then
                Dim body = Response.Content.ReadAsStringAsync().Result()
                result = JsonConvert.DeserializeObject(Of List(Of InMotionGIT.FASI.Contracts.General.MultiCompany))(body)
            End If

        End Using

        Return result
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function LogIn(user As String, password As String, isVisibleCompany As Boolean, companyId As Integer, languageId As Integer) As Object
        Dim _config As FASIConfiguration = InMotionGIT.Core.Configuration.FASIConfiguration.Configuration()
        Dim State As Boolean = False
        Dim Message As String = String.Empty
        Dim IsMultiCompany As Boolean = False
        Dim IsAuthenticated As Boolean = False
        Dim url As String = String.Empty
        Dim codeScript As String = String.Empty
        Dim ShowStartUpMessage As Boolean = False
        Dim StartUpMessage As String = String.Empty
        Dim page = DirectCast(HttpContext.Current.CurrentHandler, Page)
        Dim urlRelativePath As String = page.AppRelativeVirtualPath.Replace("~", "")
        Dim RecaptchaShow As Boolean = False
        Dim Theme As String = String.Empty
        Dim Language As String = String.Empty
        Dim ipAddress As String = String.Empty
        Dim encryptPassword As String = String.Empty

        If _config.Security.Mode <> FASI.Enumerations.EnumSecurityMode.Sesame Then
            encryptPassword = CryptSupportNew.EncryptString(password.Trim())
        End If

        Try
            ipAddress = InMotionGIT.Common.Helpers.Connection.GetIPRequest()

            Dim urlAcction = String.Format(CultureInfo.InvariantCulture, "{0}{1}?email={2}&password={3}&companyId={4}&ip={5}&languageId={6}",
                                           BaseUrl,
                                           "Authentication/v1/PortalAuthentication",
                                           user.Trim(),
                                           encryptPassword,
                                           companyId,
                                           ipAddress,
                                           languageId)

            Using client As New HttpClient()
                client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", GetAnonymousToken)

                Using Response = client.GetAsync(urlAcction).Result
                    If Response.IsSuccessStatusCode Then
                        Dim body = Response.Content.ReadAsStringAsync().Result()
                        Dim resultMethod = JsonConvert.DeserializeObject(body)

                        If resultMethod IsNot Nothing And resultMethod("Successfully").ToString = "True" Then
                            Dim resultInformation = JsonConvert.DeserializeObject(Of InMotionGIT.FASI.Contracts.Security.AuthenticationInformation)(resultMethod("Data").ToString())
                            With resultInformation

                                Select Case .Status
                                    Case EnumAuthenticationStatus.UserValid, EnumAuthenticationStatus.PasswordExpired
                                        IsAuthenticated = True

                                        If url.IsEmpty Then
                                            url = config.General.UrlHome
                                        End If

                                        If resultInformation.User.IsFirstVisit And resultInformation.User.FirstTimePasswordChange Then
                                            If config.Security.Mode = FASI.Enumerations.EnumSecurityMode.Windows Or config.Security.Mode = FASI.Enumerations.EnumSecurityMode.Database Then
                                                url = "FirstPasswordChange.html"
                                                System.Web.HttpContext.Current.Session.Add("FirstPasswordChange", True)
                                            End If
                                        End If

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

                                        HttpContext.Current.Session.Add("SecurityLevel", .User.SecurityLevel)

                                        InMotionGIT.FASI.Support.Authentication.ValidateFormsAuthenticationTicketOnly(resultInformation.User)

                                        If String.IsNullOrEmpty(.User.Theme) Then
                                            Theme = config.General.DefaultTheme
                                        Else
                                            Theme = .User.Theme
                                        End If

                                        If String.IsNullOrEmpty(.User.Language) Then
                                            Language = config.General.DefaultLanguage
                                        Else
                                            Language = .User.Language
                                        End If

                                        If .User.LanguageID = 0 Then
                                            languageId = InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext
                                        Else
                                            languageId = .User.LanguageID
                                        End If

                                        Dim producerId = 0

                                        If resultInformation.User.ProducerID.IsNotEmpty Then
                                            producerId = Integer.Parse(resultInformation.User.ProducerID)
                                        End If

                                        InMotionGIT.Membership.Providers.Helper.InformationUserLoad(.User.UserName, New FrontOfficeMembershipInfo With {.IsAdministrator = resultInformation.User.IsAdministrator,
                                                                                                                                  .IsEmployee = resultInformation.User.IsEmployee,
                                                                                                                                  .IsClient = resultInformation.User.IsClient,
                                                                                                                                  .IsProducer = resultInformation.User.IsProducer,
                                                                                                                                  .Theme = Theme,
                                                                                                                                  .LanguageId = languageId,
                                                                                                                                  .Language = Language,
                                                                                                                                  .UserId = resultInformation.User.UserID,
                                                                                                                                  .UserName = resultInformation.User.UserName,
                                                                                                                                  .AllowScheduler = resultInformation.User.AllowScheduler,
                                                                                                                                  .CurrentPageId = resultInformation.User.CurrentPageId,
                                                                                                                                  .ProducerID = producerId,
                                                                                                                                  .ClientID = resultInformation.User.ClientID,
                                                                                                                                  .RoleName = resultInformation.User.RolesAssigned,
                                                                                                                                  .PasswordNeverExpires = resultInformation.User.PasswordNeverExpires})

                                        InMotionGIT.FASI.Utils.Track.UserActivity.UpdateSessionOnAudit(HttpContext.Current.Session.SessionID, InMotionGIT.Common.Helpers.Connection.GetIPRequest(), resultInformation.User.Email, Date.MinValue)
                                        InMotionGIT.FASI.Utils.Track.UserActivity.Tracking(HttpContext.Current, HttpContext.Current.Session.SessionID, ipAddress, resultInformation.User.UserName, resultInformation.User.Email)

                                        State = True

                                        If .Status = EnumAuthenticationStatus.PasswordExpired Then
                                            If config.Security.Mode = FASI.Enumerations.EnumSecurityMode.Windows Or config.Security.Mode = FASI.Enumerations.EnumSecurityMode.Database Then
                                                url = "/fasi/dli/forms/PasswordChange.aspx?PasswordExpiration=True"
                                                IsAuthenticated = True
                                                System.Web.HttpContext.Current.Session.Add("PasswordExpiration", True)
                                            End If
                                        End If

                                    Case EnumAuthenticationStatus.SelectCompany
                                        IsMultiCompany = True

                                    Case Else
                                        If .RecaptchaShow Then
                                            RecaptchaShow = True
                                            Message = "RecaptchaShow"
                                        Else
                                            Message = .Message
                                        End If
                                End Select
                            End With

                            State = True

                        Else
                            State = False

                            If resultMethod IsNot Nothing Then
                                Message = resultMethod("Reason").ToString
                            End If
                        End If

                    Else
                        State = False
                    End If
                End Using
            End Using

        Catch ex As Exception
            LogHandler.ErrorLog("LogIn", "LogIn", ex)
            State = False
            Message = ex.Message
        End Try

        Return New With {Key .State = State,
                         Key .settings = New With {Key .Security = New With {Key .Mode = _config.Security.Mode.ToString()}},
                         Key .IsMultiCompany = IsMultiCompany,
                         Key .RecaptchaShow = RecaptchaShow,
                         Key .Message = Message,
                         Key .IsAuthenticated = IsAuthenticated,
                         Key .Url = url,
                         Key .ShowStartUpMessage = ShowStartUpMessage,
                         Key .StartUpMessage = StartUpMessage}
    End Function

#End Region

    Private Shared Function GetAnonymousToken() As String
        ' Si no existe token lo solicita
        If HttpContext.Current.Session("AnonymousTokenResponse") Is Nothing Then
            TokenHelper.RequestAnonymousToken()
        ElseIf DirectCast(HttpContext.Current.Session("AnonymousTokenResponse"), TokenResponse).AccessToken Is Nothing Then
            TokenHelper.RequestAnonymousToken()
        Else
            TokenHelper.GetAnonymousToken()
        End If

        Return DirectCast(HttpContext.Current.Session("AnonymousTokenResponse"), TokenResponse).AccessToken
    End Function

End Class