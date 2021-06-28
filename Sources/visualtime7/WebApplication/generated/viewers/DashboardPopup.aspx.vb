Imports DevExpress.DataAccess.ConnectionParameters

Partial Class DashboardPopup
    Inherits System.Web.UI.Page

    Protected Sub Page_Init1(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim ci As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture.Clone
        ci.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
dim filename as string = "ventas"
If Request.QueryString.Count > 0  Then
filename  = Request.QueryString(0)
End If
	ASPxDashboardViewer1.DashboardXmlFile="~/generated/dashboard/" & filename  & ".xml"
    
End Sub

    Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs) Handles ASPxDashboardViewer1.ConfigureDataConnection
If e.ConnectionName = "TIME_Connection" Then
        Dim parameters As OracleConnectionParameters = DirectCast(e.ConnectionParameters, OracleConnectionParameters)

        parameters.UserName = "INSUDB"
        parameters.Password = "INSUDB"

End If

If e.ConnectionName = "FrontOfficeConnection" Then
        Dim parameters As MsSqlConnectionParameters= DirectCast(e.ConnectionParameters, MsSqlConnectionParameters)

        parameters.UserName = "vtapps"
        parameters.Password = "vtapps"

End If
    End Sub
End Class
