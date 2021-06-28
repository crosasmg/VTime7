
Partial Class Authentication_Bridge
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        InMotionGIT.Common.Helpers.LogHandler.TraceLog("pruebaxx", String.Format("prueba ", ""))
        If Request.QueryString("UserName") IsNot Nothing Then
            InMotionGIT.Common.Helpers.LogHandler.TraceLog("pruebaxx", String.Format("prueba ", Request.QueryString("UserName") + "|" + Request.QueryString("Password")))
            HttpContext.Current.Response.Redirect("http://10.161.113.24:8084/Authentication/UserLogIn.aspx?UserName=" + Request.QueryString("UserName") + "&Password=" + Request.QueryString("Password"), False)
        End If
    End Sub

End Class
