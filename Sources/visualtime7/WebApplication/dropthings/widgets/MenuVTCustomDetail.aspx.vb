#Region "using"

Imports System.Data
Imports Page = DashboardDataAccess.Page
Imports GIT.Core
Imports System.Web.UI.Page
Imports System.Data.SqlClient
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Proxy.MenuService
Imports InMotionGIT.Common.Proxy

#End Region

Partial Class dropthings_MenuVTCustomDetail
    Inherits PageBase

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        GridView.DataBind()

        If Not IsPostBack Then
            If Request.QueryString.Item("List") <> "" Then
                For Each sCodispl As String In Request.QueryString.Item("List").ToString.Split(",")
                    GridView.Selection.SelectRowByKey(sCodispl)
                Next
            End If
        End If
    End Sub

#End Region

#Region "Controls Events"

    Protected Sub GridView_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridView.CustomCallback

        GridView.JSProperties.Clear()

        Dim mHTML As String = "<br><ul>"

        Dim KeyValues As Generic.List(Of Object) = GridView.GetSelectedFieldValues("Description")

        For Each key As Object In KeyValues
            mHTML += String.Format("<li>{0}", key)
        Next key

        mHTML += "</ul>"

        GridView.JSProperties.Add("cp_sList", mHTML)
    End Sub

    Protected Sub GridView_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles GridView.DataBinding
        GridView.DataSource = DirectAccessWindowsList(String.Empty, String.Empty)
    End Sub

    Protected Sub Submmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Submmit.Click


        Dim list As String = String.Empty

        For Each sCodispl As String In GridView.GetSelectedFieldValues("WindowLogicalCode").ToArray()
            If String.IsNullOrEmpty(list) Then
                list = sCodispl
            Else
                list = String.Format("{0}'',''{1}", sCodispl, list)
            End If
        Next

        Dim State As String = String.Format("<state><Modules>{0}</Modules></state>", list)

        If SaveState(State) Then
            Response.Redirect("~/dropthings/Default.aspx")
        End If


    End Sub

#End Region

#Region "Methods"

    Function SaveState(ByVal State As String) As Boolean
        Try
            With New DataManagerFactory(String.Format("UPDATE WidgetInstance SET State = '{0}' WHERE Id = {1}", State, Request.QueryString("Id")),
                                        "WidgetInstance",
                                       "FrontOfficeConnectionString")
                .CommandExecute()
            End With
            Session("UserPageSetup") = Nothing
            SaveState = True
        Catch ex As Exception
            SaveState = False
            InMotionGIT.Common.Helpers.LogHandler.TraceLog("MenuVTCustomDetail SaveState", String.Format("{0}:{1}", "Error", ex.Message))
        End Try

    End Function

    Public Function DirectAccessWindowsList(ByVal sFilter As String, ByVal sSche_Code As String) As MenuInformationList
        Try
            Dim modules As MenuInformationList
            modules = InMotionGIT.FrontOffice.Proxy.Helpers.Menu.FullWindowsList(sFilter, sSche_Code, Session("CompanyId"))

            Return modules
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.TraceLog("MenuVTCustomDetail DirectAccessWindowsList", String.Format("{0}:{1}", "Error", ex.Message))
        End Try

    End Function

#End Region

End Class