Imports Dropthings.Widget.Framework
Imports InMotionGIT.Common.Proxy
Imports System.Data
Imports GIT.Core

Namespace Dropthings.Widgets

    Partial Class DocumentPendingUserControl
        Inherits System.Web.UI.UserControl
        Implements IWidget

#Region "IWidget Members"

        Public Sub Closed() Implements Dropthings.Widget.Framework.IWidget.Closed
        End Sub

        Public Sub HideSettings() Implements Dropthings.Widget.Framework.IWidget.HideSettings

        End Sub

        Public Sub Init1(ByVal host As Dropthings.Widget.Framework.IWidgetHost) Implements Dropthings.Widget.Framework.IWidget.Init

        End Sub

        Public Sub Maximized() Implements Dropthings.Widget.Framework.IWidget.Maximized

        End Sub

        Public Sub Minimized() Implements Dropthings.Widget.Framework.IWidget.Minimized

        End Sub

        Public Sub ShowSettings() Implements Dropthings.Widget.Framework.IWidget.ShowSettings

        End Sub

#End Region

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Dim Current As PageBase = TryCast(HttpContext.Current.Handler, PageBase)
                With New DataManagerFactory(" SELECT " & _
                                        " 	DOCUMENTCACHE.FORMID, " & _
                                        " 	REPLACE(REPLACE(LOWER(PAGE), '~/', ''), 'usercontrol.ascx','.aspx') PAGE, " & _
                                        " 	DOCUMENTCACHE.CREATIONDATE, " & _
                                        " 	DOCUMENTCACHE.UPDATEDATE, " & _
                                        " 	MODEL.TITLE " & _
                                        " FROM " & _
                                        " 	DOCUMENTCACHE " & _
                                        " INNER JOIN MODEL ON MODEL.MODELID = DOCUMENTCACHE.MODELID " & _
                                        " WHERE DOCUMENTCACHE.USERID = @:USERNAME ",
                                        "DOCUMENTCACHE",
                                        "FrontOfficeConnectionString")
                    .AddParameter("USERNAME", Data.DbType.AnsiStringFixedLength, 256, False, Current.UserInfo.UserName)
                    Dim resultData As DataTable = .QueryExecuteToTable(True)
                    ListGridView.DataSource = resultData
                    ListGridView.DataBind()
                End With
            End If
        End Sub
    End Class
End Namespace