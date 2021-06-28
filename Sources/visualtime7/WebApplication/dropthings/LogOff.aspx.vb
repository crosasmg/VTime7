Imports InMotionGIT.Core.Configuration.Enumerations

Partial Class dropthings_LogOff
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim value As String = String.Empty

        If Request.QueryString("id").IsNotEmpty() Then
            value = Request.QueryString("id")
        End If

        Dim userName As String = (New InMotionGIT.Membership.Providers.MemberContext).UserName

        InMotionGIT.Common.Helpers.Caching.RemoveStartWith("PropertyUser")

        If userName.IsNotEmpty Then
            Using client As New InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient
                client.UserStatusChange(userName, False)
            End Using
        End If

        Dim config As InMotionGIT.Core.Configuration.VisualTIME = DirectCast(System.Configuration.ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), InMotionGIT.Core.Configuration.VisualTIME)
        Dim urlTemporal As String = String.Empty
        Dim cookiesToClear As New List(Of String)()

        For Each cookieName As String In Context.Request.Cookies
            If Not config.Security.Mode = EnumSecurityMode.HeaderAuthentication AndAlso config.Security.Mode = EnumSecurityMode.Windows Then
                If Not String.Equals("EmailAddress", cookieName) Then
                    Dim cookie As HttpCookie = Context.Request.Cookies(cookieName)
                    cookiesToClear.Add(cookie.Name)
                    Exit For
                End If
            Else
                If Not String.Equals("EmailAddress", cookieName) Then
                    Dim cookie As HttpCookie = Context.Request.Cookies(cookieName)
                    cookiesToClear.Add(cookie.Name)
                End If
            End If
        Next

        For Each name As String In cookiesToClear
            Dim cookie As New HttpCookie(name, String.Empty)
            cookie.Expires = DateTime.Today.AddYears(-1)
            Context.Response.Cookies.[Set](cookie)
        Next

        RevokeRefreshToken()
        FormsAuthentication.SignOut()
        Context.Session.Abandon()
        Context.Session.Clear()
        Context.Session.Remove("IsFirtView")

        If InMotionGIT.Common.Helpers.Caching.Exist("InMotionGITToken") Then
            InMotionGIT.Common.Helpers.Caching.Remove("InMotionGITToken")
        End If

        If Request.QueryString("Revelation").IsNotEmpty Then
            ScriptManager.RegisterStartupScript(Page, GetType(dropthings_LogOff), "WindowsClose",
                                                     "<script type=text/javascript>WindowsClose();</script>", False)
        End If

        Select Case config.Security.Mode

            Case EnumSecurityMode.DataBase, EnumSecurityMode.Windows, EnumSecurityMode.ActiveDirectory

                If ConfigurationManager.AppSettings("FASI.Security.Logoff.Redirect").IsEmpty() Then

                    Session.Add("MasterPageRelog", True)

                    If config.Security.URLAuthentication.Trim() = String.Empty Then
                        urlTemporal = ConfigurationManager.AppSettings("Url.WebApplication").ToString() + "/dropthings/Default.aspx"
                    Else
                        If config.Security.URLAuthentication.ToLower().Contains("http") OrElse config.Security.URLAuthentication.ToLower().Contains("https") Then
                            urlTemporal = config.Security.URLAuthentication
                        Else
                            urlTemporal = ConfigurationManager.AppSettings("Url.WebApplication").ToString() + config.Security.URLAuthentication.ToString()
                        End If
                    End If

                    If value.IsEmpty Then
                        Response.Redirect(urlTemporal)
                    Else
                        If value.Contains("@@") Then
                            value = value.Split(New String() {"@@"}, StringSplitOptions.RemoveEmptyEntries)(1)
                        End If
                        If IsCallback Then
                            DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(String.Format("~/dropthings/Error.aspx?id={0}", Server.UrlEncode(value)))
                        Else
                            Response.Redirect(String.Format("~/dropthings/Error.aspx?id={0}", Server.UrlEncode(value)))
                        End If
                    End If
                Else
                    Response.Redirect(ConfigurationManager.AppSettings("FASI.Security.Logoff.Redirect"))
                End If

                Exit Select

            Case EnumSecurityMode.HeaderAuthentication
                If config.Security.URLAuthentication.Trim() = String.Empty Then
                    urlTemporal = "#"
                Else
                    If config.Security.URLAuthentication.ToLower().Contains("http") OrElse config.Security.URLAuthentication.ToLower().Contains("https") Then
                        urlTemporal = config.Security.URLAuthentication
                    Else
                        urlTemporal = ConfigurationManager.AppSettings("Url.WebApplication").ToString() + config.Security.URLAuthentication.ToString()
                    End If
                End If
                If urlTemporal.Equals("#") Then
                    ScriptManager.RegisterStartupScript(Page, GetType(dropthings_LogOff), "WindowsClose",
                                                 "<script type=text/javascript>WindowsClose();</script>", False)
                Else
                    Response.Redirect(urlTemporal)
                End If

                Exit Select

            Case Else

                Exit Select
        End Select

        Context.Session.RemoveAll()
    End Sub

    Private Sub RevokeRefreshToken()
        Context.Session.Remove("TokenResponse")
        Context.Session.Remove("AccessToken")

        'TODO: Versión framework >= 4.5
        'Dim token = DirectCast(Session("TokenResponse"), TokenResponse)
        'Dim token = DirectCast(Session("TokenResponse"), Thinktecture.IdentityModel.Client.TokenResponse)

        'If (Not IsNothing(token)) AndAlso (Not token.IsError) Then
        'Try
        'Using client As New WebClient()
        '    Dim url As String = ConfigurationManager.AppSettings("STS.URL") + "/core/connect/revocation"
        '    Dim refreshToken = token.RefreshToken

        '    Dim credentials As String =
        '        Convert.ToBase64String(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings("STS.Customer.Id") + ":" + ConfigurationManager.AppSettings("STS.Customer.Secret")))
        '    client.Headers(HttpRequestHeader.Authorization) = String.Format("Basic {0}", credentials)
        '    Dim values = New NameValueCollection() From {
        '        {"token", refreshToken},
        '        {"token_type_hint", "refresh_token"}
        '    }

        '    Dim response As Byte() = client.UploadValues(url, values)

        '    Dim result As String = System.Text.Encoding.UTF8.GetString(response)
        'End Using

        'TODO: Versión framework >= 4.5
        'Dim client = New HttpClient()
        'client.SetBasicAuthentication(ConfigurationManager.AppSettings("STS.Customer.Id"), ConfigurationManager.AppSettings("STS.Customer.Secret"))

        'Dim postBody = New Dictionary(Of String, String)() From {
        '    {"token", refreshToken},
        '    {"token_type_hint", "refresh_token"}
        '}

        'Dim result = client.PostAsync(url, New FormUrlEncodedContent(postBody))
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End If
    End Sub

End Class