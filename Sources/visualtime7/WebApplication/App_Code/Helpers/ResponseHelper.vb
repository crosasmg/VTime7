Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic
Imports Thinktecture.IdentityModel.Client
Imports System.Net.Http
Imports System.Net
Imports System.IO
Imports InMotionGIT.FrontOffice.Proxy
Imports System.Configuration
Imports System.Web
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Security.Cryptography

Public Class ResponseHelper
    Private Shared _STSKey As String = ConfigurationManager.AppSettings("STS.Key").ToString()
    Private Shared _address As String = ConfigurationManager.AppSettings("STS.URL").ToString() + "/core/connect/token"
    Private Shared _OAuthClient As OAuth2Client = New OAuth2Client(New Uri(_address),
                                                                   ConfigurationManager.AppSettings("STS.Customer.Id"),
                                                                   ConfigurationManager.AppSettings("STS.Customer.Secret"))

    Private Shared _anonymousClient As OAuth2Client = New OAuth2Client(New Uri(_address),
                                                                      ConfigurationManager.AppSettings("STS.Customer.Id.Anonymous"),
                                                                      HMACCreator(ConfigurationManager.AppSettings("STS.Customer.Secret.Anonymous"), _STSKey))

    Public Shared Sub VerifyEditMode()
        If Not HttpContext.Current.Session("IsEditMode") Then
            Throw New InvalidOperationException()
        End If
    End Sub

    Public Shared Sub ErrorToClient(exception As Exception, context As HttpContext)
        context.Response.ClearHeaders()
        context.Response.ClearContent()
        context.Response.Clear()
        context.Response.StatusCode = 500  'TODO se tienen que crear errores mas especifico de acuerdo al error que se genere
        context.Response.StatusDescription = exception.Message
        context.Response.ContentType = "application/json"
        context.Response.AddHeader("jsonerror", "true")
        context.Response.Charset = context.Response.Charset
        context.Response.TrySkipIisCustomErrors = True
        context.Response.Write(New JavaScriptSerializer().Serialize(BuildWebServiceError(exception.Message)))
        context.Response.Flush()
        context.Response.End()
    End Sub

    Public Shared Sub ErrorToClient(exception As Exception, context As HttpContext, statusCode As Integer)
        context.Response.ClearHeaders()
        context.Response.ClearContent()
        context.Response.Clear()
        context.Response.StatusCode = If(statusCode.IsEmpty, 500, statusCode)
        context.Response.StatusDescription = exception.Message
        context.Response.ContentType = "application/json"
        context.Response.AddHeader("jsonerror", "true")
        context.Response.Charset = context.Response.Charset
        context.Response.TrySkipIisCustomErrors = True
        context.Response.Write(New JavaScriptSerializer().Serialize(BuildWebServiceError(exception.Message)))
        context.Response.Flush()
        context.Response.End()
    End Sub

    Public Shared Function BuildWebServiceError(msg As String) As Dictionary(Of String, String)
        Dim result = New Dictionary(Of String, String)()
        result("Message") = msg
        Return result
    End Function

    ''' <summary>
    ''' Refresh the token, if it's invalid, asks for a new one and it is returned.
    ''' </summary>
    ''' <returns>Token</returns>
    Public Shared Function GetValidToken() As String
        Try
            If (Not Convert.ToBoolean(ConfigurationManager.AppSettings("STS.Enable"))) Then Return ""
			
            Dim token As TokenResponse
            Dim refreshToken = DirectCast(HttpContext.Current.Session("TokenResponse"), TokenResponse).RefreshToken

            If (Not Convert.ToBoolean(ConfigurationManager.AppSettings("STS.UseOfValidCertificate"))) Then ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications

            token = _OAuthClient.RequestRefreshTokenAsync(refreshToken).Result
            HttpContext.Current.Session("TokenResponse") = token
            HttpContext.Current.Session("AccessToken") = token.AccessToken

            ' If the refresh returns an error, a new token is requested
            If token.IsError Then
                RequestToken(HttpContext.Current.Request.Cookies("EmailAddress").Value)
                Return HttpContext.Current.Session("AccessToken").ToString()
            Else
                Return token.AccessToken
            End If
        Catch ex As WebException
            Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, resp, ex)
            Throw ex
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, ex)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Refresh the anonymous token, if it's invalid, asks for a new one and it is returned.
    ''' </summary>
    ''' <returns>Token</returns>
    Public Shared Function GetAnonymousToken() As String
        Try
            If (Not Convert.ToBoolean(ConfigurationManager.AppSettings("STS.Enable"))) Then Return ""
			
            Dim token As TokenResponse
            Dim refreshToken = DirectCast(HttpContext.Current.Session("AnonymousTokenResponse"), TokenResponse).RefreshToken

            If (Not Convert.ToBoolean(ConfigurationManager.AppSettings("STS.UseOfValidCertificate"))) Then ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications

            token = _anonymousClient.RequestRefreshTokenAsync(refreshToken).Result
            HttpContext.Current.Session("AnonymousTokenResponse") = token
            HttpContext.Current.Session("AnonymousAccessToken") = token.AccessToken

            ' If the refresh returns an error, a new token is requested
            If token.IsError Then
                RequestAnonymousToken()
                Return HttpContext.Current.Session("AnonymousAccessToken").ToString()
            Else
                Return token.AccessToken
            End If
        Catch ex As WebException
            Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, resp, ex)
            Throw ex
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, ex)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Invokes the STS to request a token.
    ''' If the response is successful, "TokenResponse" and "AccessToken" sessions variables will be created.
    ''' </summary>
    ''' <param name="userEmail">User name for requesting the token.</param>
    Public Shared Sub RequestToken(userEmail As String)
        Try
            ' Custom values for the request.
            Dim aditionalValues As Dictionary(Of String, String) = New Dictionary(Of String, String)
            aditionalValues.Add("custom_validator", userEmail.ToLower().Trim())

            If (Not Convert.ToBoolean(ConfigurationManager.AppSettings("STS.UseOfValidCertificate"))) Then ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
            ' Se agrega offline_access para el refresh_token
            Dim token As TokenResponse = _OAuthClient.RequestCustomGrantAsync("customUser", "InMotionScope offline_access", aditionalValues).Result

            ' Se incluye el token en la sesión, si este es válido.
            ' Se guarda el objeto del token (almacena el tokenRefresh, entre otros valores)
            HttpContext.Current.Session.Add("TokenResponse", token)
            HttpContext.Current.Session.Add("AccessToken", token.AccessToken)

            ' Se limpian los valores del token anónimo.
            HttpContext.Current.Session("AnonymousTokenResponse") = Nothing
            HttpContext.Current.Session("AnonymousAccessToken") = Nothing
        Catch ex As WebException
            Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, resp, ex)
            Throw ex
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, ex)
            Throw ex
        End Try

        ''TODO: Versión framework >= 4.5
        'Dim _tokenClient As TokenClient = New TokenClient(address,
        '                                                  ConfigurationManager.AppSettings("STS.Customer.Id"),
        '                                                  ConfigurationManager.AppSettings("STS.Customer.Secret"))
        '' Se agrega offline_access para el refresh_token
        'Dim token = _tokenClient.RequestResourceOwnerPasswordAsync(userName, password, "InMotionScope offline_access").Result
    End Sub

    ''' <summary>
    ''' Invokes the STS to request an anonymous token.
    ''' If the response is successful, "TokenResponse" and "AccessToken" sessions variables will be created.
    ''' </summary>
    Public Shared Sub RequestAnonymousToken()
        Try
            If (Not Convert.ToBoolean(ConfigurationManager.AppSettings("STS.UseOfValidCertificate"))) Then ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications

            Dim token As TokenResponse = _anonymousClient.RequestResourceOwnerPasswordAsync("Anonymous", HMACCreator(ConfigurationManager.AppSettings("STS.User.Secret.Anonymous"), _STSKey), "InMotionScope").Result

            ' Se incluye el token en la sesión, si este es válido.
            ' Se guarda el objeto del token (almacena el tokenRefresh, entre otros valores)
            HttpContext.Current.Session.Add("AnonymousTokenResponse", token)
            HttpContext.Current.Session.Add("AnonymousAccessToken", token.AccessToken)
        Catch ex As WebException
            Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, resp, ex)
            Throw ex
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, ex)
            Throw ex
        End Try
    End Sub

    Public Shared Sub Initialization()
        Dim usersBO As New InMotionGIT.General.Entity.Contracts.Security.UserCollection
        Dim usersPortal As New List(Of UserService.UserInformation)
        Try
            Dim STSCompanyId As Short = Convert.ToInt16(ConfigurationManager.AppSettings("STS.CompanyId").ToString())
            Dim consumerId As Short = Convert.ToInt16(ConfigurationManager.AppSettings("STS.ConsumerId").ToString())
            Dim defaultRoleId As String = ConfigurationManager.AppSettings("STS.DefaultRoleId").ToString()

			InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("Obteniendo usuarios del BO"))

            'Se busca información de los usuarios de BO a través del proxy
            usersBO = InMotionGIT.BackOffice.Support.BackOfficeProcess.getUsers()

            InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("Usuarios del BO: {0}", usersBO.Count()))

            InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("Obteniendo usuarios del portal"))

            Dim securityServices = New InMotionGIT.FrontOffice.Proxy.UserService.UsersClient
            usersPortal = securityServices.GetBasicInformationOfPortalUsers().ToList()

			InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("Usuarios del portal: {0}", usersPortal.Count()))
						
            Dim usersDTO As New List(Of InMotionGIT.Seguridad.Contrato.DTOs.UserDTO)
            usersPortal = usersPortal.Where(Function(x) Not x.IsInactive And Not IsNothing(x.UserName) And Not IsNothing(x.Email)).GroupBy(Function(z) z.Email.ToLower().Trim()).Select(Function(a) a.FirstOrDefault()).ToList()
            Dim usersBO2 = usersBO.Where(Function(y) y.RecordStatus.Equals("1") And Not IsNothing(y.UserInitials))

            InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("Total BO users: {0} - Total FASI Users: {1}", usersBO2.Count, usersPortal.Count))

			InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("Portal: {0}", New JavaScriptSerializer().Serialize(usersPortal)))
			InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("BO: {0}", New JavaScriptSerializer().Serialize(usersBO2)))
			
            ' Subject property is used to authorize the user through custom validations, must be unique
            usersDTO =
                (From portal In usersPortal
                 Group Join bo In usersBO2 On portal.UserName.Trim().ToLower() Equals bo.UserInitials.Trim().ToLower() Into compiledUsers = Group
                 From core In compiledUsers.DefaultIfEmpty()
                 Select New InMotionGIT.Seguridad.Contrato.DTOs.UserDTO With {
                        .LastName = If(portal.LastName = "", "LastName", portal.LastName),
                        .Name = If(portal.FirstName = "", "FirstName", portal.FirstName),
                        .EmailVerified = "1",
                        .Username = If(portal.UserName = "", "UserName", portal.UserName),
                        .Password = HMACCreator(CreateDefaultUserPassword(portal.FirstName, portal.LastName), _STSKey),
                        .Subject = If(portal.Email = "", Guid.NewGuid().ToString(), portal.Email.ToLower().Trim()),
                        .Usercode = If(core Is Nothing, 0, core.UserCode),
                        .Nickname = If(portal.UserName Is Nothing, If(core.UserInitials Is Nothing, "", core.UserInitials), portal.UserName),
                        .Email = If(portal.Email = "", "No Email", portal.Email.ToLower().Trim()),
                        .CompanyId = STSCompanyId,
                        .ConsumerId = consumerId,
                        .UserId = portal.Id,
                        .Employee = If(portal.IsEmployee, "1", "0"),
                        .ExternalUser = "0",
                        .RolesDescription = portal.RolesAssignedByName,
                        .RolesId = If(String.IsNullOrWhiteSpace(portal.RolesAssigned), defaultRoleId, portal.RolesAssigned)
                     }).ToList()

			InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("UsersDTO: {0}", New JavaScriptSerializer().Serialize(usersDTO)))
					 
			' Dim admin = 
				' (From portal In usersPortal.Where(Function(x) x.Email.Trim().ToLower().Equals("admin@visualtime.com"))
                 ' Select New InMotionGIT.Seguridad.Contrato.DTOs.UserDTO With {
                        ' .LastName = If(portal.LastName = "", "LastName", portal.LastName),
                        ' .Name = If(portal.FirstName = "", "FirstName", portal.FirstName),
                        ' .EmailVerified = "1",
                        ' .Username = If(portal.UserName = "", "UserName", portal.UserName),
                        ' .Password = HMACCreator(CreateDefaultUserPassword(portal.FirstName, portal.LastName), _STSKey),
                        ' .Subject = If(portal.Email = "", Guid.NewGuid().ToString(), portal.Email.ToLower().Trim()),
                        ' .Usercode = portal.UserCode,
                        ' .Nickname = If(portal.UserName Is Nothing, "", portal.UserName),
                        ' .Email = If(portal.Email = "", "No Email", portal.Email.ToLower().Trim()),
                        ' .CompanyId = STSCompanyId,
                        ' .ConsumerId = consumerId,
                        ' .UserId = portal.Id,
                        ' .Employee = If(portal.IsEmployee, "1", "0"),
                        ' .ExternalUser = "0",
                        ' .RolesDescription = portal.RolesAssignedByName,
                        ' .RolesId = If(String.IsNullOrWhiteSpace(portal.RolesAssigned), defaultRoleId, portal.RolesAssigned)
                     ' }).SingleOrDefault()
			
			'InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("Admin: {0}", New JavaScriptSerializer().Serialize(admin)))
			
			'usersDTO.Add(admin)
			
			InMotionGIT.Common.Helpers.LogHandler.TraceLog("ResponseHelper.Initialization", String.Format("Total de usuarios: {0}", usersDTO.Count()))
			
            If (Not Convert.ToBoolean(ConfigurationManager.AppSettings("STS.UseOfValidCertificate"))) Then ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
			
            Dim address As String = String.Concat(ConfigurationManager.AppSettings("API.SecurityUsers.URL"), "/users/SyncUsers")
            Dim result = String.Empty
            Using client As New WebClient()
                client.Encoding = Encoding.UTF8
                client.Headers(HttpRequestHeader.ContentType) = "application/json"
                client.Headers(HttpRequestHeader.Authorization) = String.Concat("Bearer ", HttpContext.Current.Session("AccessToken").ToString)
                result = client.UploadString(address, New JavaScriptSerializer().Serialize(usersDTO))
            End Using
        Catch ex As WebException
            Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("WebException -> " & System.Reflection.MethodBase.GetCurrentMethod().Name, resp, ex)
            Throw ex
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Exception -> " & System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, ex)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Bypass the cert validation.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="certification"></param>
    ''' <param name="chain"></param>
    ''' <param name="sslPolicyErrors"></param>
    ''' <returns></returns>
    Private Shared Function AcceptAllCertifications(ByVal sender As Object, ByVal certification As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    ''' <summary>
    ''' Creates a default user password for the initialization.
    ''' Uses the first name, second name and the current year.
    ''' For example, for the user John Doe the password will be jdoe2018
    ''' </summary>
    ''' <param name="firstName">First name.</param>
    ''' <param name="lastName">Last name</param>
    ''' <returns>Password.</returns>
    Private Shared Function CreateDefaultUserPassword(firstName As String, lastName As String) As String
        If (String.IsNullOrWhiteSpace(firstName)) Then firstName = "firstName"
        If (String.IsNullOrWhiteSpace(lastName)) Then firstName = "lastName"

        Return String.Concat(firstName.Substring(0, 1), lastName, DateTime.Now.Year.ToString())
    End Function

    ''' <summary>
    ''' Creates a SHA256 hash of the specified input.
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns></returns>
    Private Shared Function Sha256Creator(input As String) As String
        If (String.IsNullOrWhiteSpace(input)) Then Throw New ArgumentNullException("Parameter input is null or empty")

        Using sha As SHA256 = SHA256.Create()
            Dim bytes = Encoding.UTF8.GetBytes(input)
            Dim hash = sha.ComputeHash(bytes)

            Return Convert.ToBase64String(hash)
        End Using
    End Function

    ''' <summary>
    ''' Creates a HMAC has of the specified input and uses the sha256 of the STSKey as the key.
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="key">STSKey.</param>
    ''' <returns></returns>
    Private Shared Function HMACCreator(input As String, key As String) As String
        If (String.IsNullOrWhiteSpace(input)) Then Throw New ArgumentNullException("Parameter input is null or empty")
        If (String.IsNullOrWhiteSpace(key)) Then Throw New ArgumentNullException("Parameter key is null or empty")

        Using hmac As HMACSHA256 = New HMACSHA256(Encoding.UTF8.GetBytes(Sha256Creator(key)))
            Dim bytes = Encoding.UTF8.GetBytes(input)
            Dim hash = hmac.ComputeHash(bytes)

            Return Convert.ToBase64String(hash)
        End Using
    End Function
End Class
