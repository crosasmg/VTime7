Partial Class Underwriting_Controls_Partials_requirementSuscriptionRules
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
    End Sub
End Class