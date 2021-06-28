Imports GIT.Core
Partial Class ErrorWebForm
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        Dim message As String = Request.QueryString("id")
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

        Select Case message
            Case "GEN9001"
                message = GetGlobalResourceObject("Resource", "DeniedAccess")
        End Select

        ErrorLabel.Text = message

        If (GetGlobalResourceObject("Resource", "DeniedAccess") = message _
            AndAlso Not InMotionGIT.FrontOffice.Proxy.Helpers.Authentication.ValidateAcccesByToken(True, "")) Then

            FormsAuthentication.SignOut()
            Session.Clear()
            Session.Abandon()
            Dim cookie1 As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, "")
            cookie1.Expires = DateTime.Now.AddYears(-1)
            Response.Cookies.Add(cookie1)

            Dim cookie2 As HttpCookie = New HttpCookie(".ASPXROLES", "")
            cookie2.Expires = DateTime.Now.AddYears(-1)
            Response.Cookies.Add(cookie2)

            Dim cookie3 As HttpCookie = New HttpCookie(".DBANON", "")
            cookie3.Expires = DateTime.Now.AddYears(-1)
            Response.Cookies.Add(cookie3)

            Dim cookie4 As HttpCookie = New HttpCookie("ASP.NET_SessionId", "")
            cookie4.Expires = DateTime.Now.AddYears(-1)
            Response.Cookies.Add(cookie4)
        End If
    End Sub

    'Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
    '    Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
    '    System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
    '    System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
    'End Sub

End Class
