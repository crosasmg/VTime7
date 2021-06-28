#Region "using"

Imports GIT.Core
Imports System.Globalization
Imports InMotionGIT.Common.Helpers

#End Region

Partial Class dropthings_GeneralQuery_Page
    Inherits PageBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'ResourceManager.ScriptRegistrator.RegistredResources.Clear()
        'ASPxWebControl.RegisterBaseScript(Me)

        Dim _metadata As GIT.EDW.Query.Model.metadata = Nothing

        If Request.QueryString("ModelId").IsNotEmpty Then
            _metadata = GIT.EDW.Query.Model.Widget.LoadRepository(Request.QueryString("ModelId"),
                                                                  Request.QueryString("Release"),
                                                                 (Request.QueryString("debug") = "y"))
        ElseIf Request.QueryString("Name").IsNotEmpty Then
            _metadata = GIT.EDW.Query.Model.Widget.LoadRepositoryByName(Request.QueryString("Name"),
                                                                        (Request.QueryString("debug") = "y"))
        End If

        If Not IsNothing(_metadata) Then
            Dim roles As String = _metadata.roles

            If Not String.Equals(roles, "All", StringComparison.CurrentCultureIgnoreCase) Then
                Dim rolesList As String = String.Empty

                For Each roleData As String In roles.Split(",")
                    If rolesList.Length > 0 Then
                        rolesList += ";"
                    End If

                    rolesList += roleData.TrimStart
                Next

                If InMotionGIT.FrontOffice.Proxy.Helpers.RoleManager.IsRoleNotValidForAccess(UserInfo.UserName, rolesList) Then
                    If IsCallback Then
                        DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/dropthings/Error.aspx?id=GEN9001")
                    Else
                        Response.Redirect("~/dropthings/Error.aspx?id=GEN9001")
                    End If
                End If

            End If

            Me.Title = _metadata.root.TitleQuery.GetValue(LanguageId)
            QueryManagerUC._dateformat = ConfigurationManager.AppSettings(String.Format(CultureInfo.InvariantCulture, "Linked.{0}.DateFormat", _metadata.Repository))
            QueryManagerUC._metadata = _metadata
        End If

    End Sub
End Class
