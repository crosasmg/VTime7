#Region "using"

Imports System.Data
Imports System.Globalization
Imports System.Net
Imports DevExpress.Web.ASPxClasses
Imports GIT.Core
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.FrontOffice.Support
Imports InMotionGIT.FrontOffice.Support.User

#End Region

Partial Class _support
    Inherits PageBase

#Region "Field"

    Private dataSourceTable As DataTable

#End Region

    Protected Sub AdminUserCallbackPanel_Callback(sender As Object, e As CallbackEventArgsBase) Handles AdminUserCallbackPanel.Callback
        Try
            GeneratedAnonimous()
            GenerateAdminUser()
            AdminUserImageOK.Visible = True
        Catch ex As Exception
            AdminUserImageFail.Visible = True
            AdminUserLabel.Text = ex.Message.ToString
        End Try
    End Sub

    Public Sub GeneratedAnonimous()
        Dim config As FASIConfiguration = FASIConfiguration.Configuration()
        Dim User = "Anonymous"
        'User = ConfigurationManager.AppSettings("STS.Customer.Id.Anonymous")
        Dim email As String = String.Format("{0}@{1}", User, txtEmail.Text.Split("@")(1)).ToLower()
        'Creación de usuario anónimos


        UserManager("delete", User, String.Empty, String.Empty, String.Empty, String.Empty,
                        InMotionGIT.Membership.Providers.Enumerations.enumUserType.User, String.Empty, String.Empty, Nothing, False, True)

        LogHandler.WarningLog("_support", String.Format("Se elimino el usuario {0}", User))

        'Se crea el usuario admin

        UserManager("add", User, User, User,
                        email, config.Security.AnonymousRole, InMotionGIT.Membership.Providers.Enumerations.enumUserType.User,
                        GetGlobalResourceObject("Resource", "SecretQuestionColor").ToString(), String.Empty, Nothing, False, False, False, IsEmployee:=False, IsAdministrator:=False, AllowScheduler:=False, SecurityLavel:=9)

        LogHandler.WarningLog("_support", String.Format("Se creo el usuario {0}", User))


        Dim Query = String.Format("SELECT USERID FROM USERMEMBER WHERE LOWER (USERMEMBER.USERNAME) = LOWER ('{0}')", User)
        InMotionGIT.Common.Helpers.LogHandler.TraceLog("InMotionGIT.FASI.Support=>UserOffLineAll", String.Format("Commando a ejecutar '{0}'", Query))
        Dim client = New InMotionGIT.Common.Proxy.DataManagerFactory(Query, "FrontOfficeConnectionString")
        Dim userId = client.QueryExecuteScalarToInteger()


        Query = String.Format("select ROLEID from ROLE where lower(ROLENAME) = lower('{0}')", config.Security.AnonymousRole)
        client = New InMotionGIT.Common.Proxy.DataManagerFactory(Query, "FrontOfficeConnectionString")
        Dim roleId = client.QueryExecuteScalarToInteger()


        InMotionGIT.FASI.Support.Members.UserAnonimous(User, email, userId, roleId, config.Security.AnonymousRole)
    End Sub

    Private Sub GenerateAdminUser()
        InMotionGIT.FASI.Support.Authentication.GetAnonymousToken()
        Dim user As String = txtUser.Text
        Dim config As FASIConfiguration = FASIConfiguration.Configuration()
        Dim password As String = AuthenticationHelper.CreateAdministratorPassword(Membership.MinRequiredPasswordLength,
                                                                                  Membership.MinRequiredNonAlphanumericCharacters)

        If chkIsEmployee.Checked Then
            IsValidUser(txtUser.Text)

            'Se borra el usuario
            UserManager("delete", user, String.Empty, String.Empty, String.Empty, String.Empty,
                         InMotionGIT.Membership.Providers.Enumerations.enumUserType.User, String.Empty, String.Empty, Nothing, False, True)

            LogHandler.WarningLog("_support", String.Format("Se elimino el usuario {0}", user))

            'Se crea el usuario admin

            UserManager("add", user, user, password,
                        txtEmail.Text.Trim(), config.Security.AdministratorRole, InMotionGIT.Membership.Providers.Enumerations.enumUserType.User,
                        GetGlobalResourceObject("Resource", "SecretQuestionColor").ToString(), String.Empty, Nothing, False, False, False, IsEmployee:=True, IsAdministrator:=True, AllowScheduler:=False, SecurityLavel:=9)

            LogHandler.WarningLog("_support", String.Format("Se creo el usuario {0}", user))
            AdminUserLabel.Text = String.Format(CultureInfo.InvariantCulture, "Password/Contraseña = {0}", password)
        Else

            'Se borra el usuario
            UserManager("delete", user, String.Empty, String.Empty, String.Empty, String.Empty,
                        InMotionGIT.Membership.Providers.Enumerations.enumUserType.User, String.Empty, String.Empty, Nothing, False, True)

            LogHandler.WarningLog("_support", String.Format("Se elimino el usuario {0}", user))

            'Se crea el usuario admin

            UserManager("add", user, user, password,
                        txtEmail.Text, config.Security.AdministratorRole, InMotionGIT.Membership.Providers.Enumerations.enumUserType.User,
                        GetGlobalResourceObject("Resource", "SecretQuestionColor").ToString(), String.Empty, Nothing, False, False, False, IsEmployee:=False, IsAdministrator:=True, AllowScheduler:=False, SecurityLavel:=9)

            LogHandler.WarningLog("_support", String.Format("Se creo el usuario {0}", user))
            AdminUserLabel.Text = String.Format(CultureInfo.InvariantCulture, "Password/Contraseña = {0}", password)

        End If
        Dim Query As String = ""
        Dim roleId As Integer
        Dim userId As Integer
        Dim client As InMotionGIT.Common.Proxy.DataManagerFactory
        If user.IsNotEmpty() Then
            Query = String.Format("SELECT USERID FROM USERMEMBER WHERE LOWER (USERMEMBER.USERNAME) = LOWER ('{0}')", user)
            InMotionGIT.Common.Helpers.LogHandler.TraceLog("InMotionGIT.FASI.Support=>UserOffLineAll", String.Format("Commando a ejecutar '{0}'", Query))
            client = New InMotionGIT.Common.Proxy.DataManagerFactory(Query, "FrontOfficeConnectionString")
            userId = client.QueryExecuteScalarToInteger()


            Query = String.Format("select ROLEID from ROLE where lower(ROLENAME) = lower('{0}')", config.Security.AdministratorRole)
            client = New InMotionGIT.Common.Proxy.DataManagerFactory(Query, "FrontOfficeConnectionString")
            roleId = client.QueryExecuteScalarToInteger()

            'If userId <> 0 AndAlso roleId <> 0 Then
            '    Query = String.Format("INSERT INTO USERMEMBERROLE (USERID, ROLEID) VALUES ('{0}', '{1}')", userId, roleId)
            '    client = New InMotionGIT.Common.Proxy.DataManagerFactory(Query, "FrontOfficeConnectionString")
            '    client.CommandExecute()
            'End If
            InMotionGIT.FASI.Support.Members.UserAdmin(user, txtEmail.Text.Trim(), userId, roleId, config.Security.AdministratorRole)

        End If



    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsUserValid() Then
            Dim config As VisualTIME = TryCast(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)
            Dim urlHome As String = config.Security.URLAuthentication
            Response.Redirect(String.Format("{0}{1}", ConfigurationManager.AppSettings("Url.WebApplication"), urlHome))
        End If
    End Sub

    ''' <summary>
    ''' Verify the security of the page
    ''' </summary>
    ''' <returns>True if all conditions are valid, call from localhost and user admin</returns>
    Private Shared Function IsUserValid() As Boolean
        Try
            If Not IsLocalIpAddress(HttpContext.Current.Request.Url.Host) Then
                Return False
            End If
        Catch
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Verify if the host is localhost
    ''' </summary>
    ''' <param name="host"></param>
    ''' <returns>True if it's localhost</returns>
    Private Shared Function IsLocalIpAddress(host As String) As Boolean
        Try
            Dim hostIPs As IPAddress() = Dns.GetHostAddresses(host)
            ' get local IP addresses
            Dim localIPs As IPAddress() = Dns.GetHostAddresses(Dns.GetHostName())

            If Not HttpContext.Current.Request.IsLocal Then
                Return False
            End If

            For Each hostIP As IPAddress In hostIPs
                ' is localhost
                If IPAddress.IsLoopback(hostIP) Then
                    Return True
                End If

                ' is local address
                For Each localIP As IPAddress In localIPs
                    If hostIP.Equals(localIP) Then
                        Return True
                    End If
                Next
            Next
        Catch
        End Try
        Return False
    End Function

    Private Function IsValidUser(userName As String) As Boolean
        Dim result As Boolean = False
        dataSourceTable = GetDataUsersBackOffice(Integer.MinValue)
        If dataSourceTable.IsNotEmpty AndAlso dataSourceTable.Rows.Count <> 0 Then
            For Each Item As DataRow In dataSourceTable.Rows
                If userName.Equals(Item.StringValue("SINITIALS")) Then
                    Return True
                End If
            Next
        Else
            Throw New Exception("Could not get the names of users in Backoffice")
        End If
        If result = False Then
            Throw New Exception("The user is trying to add as user does not exist in the backoffice.")
        End If
        Return result
    End Function

End Class