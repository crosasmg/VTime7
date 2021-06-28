Imports DevExpress.Web.ASPxUploadControl
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxClasses
Imports System.IO
Imports System.Configuration
Imports InMotionGIT.Underwriting.Contracts

Partial Class Underwriting_Controls_RequirementsAttachements
    Inherits System.Web.UI.UserControl
    Private Const _UnderwritingSessionTimeOutaspx As String = "~\Underwriting\SessionTimeOut.aspx"

    Public Sub RebindGridView()
        With gvAttachments
            If Not .IsNewRowEditing Then
                .DataBind()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Redirects the page when the session time out
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RedirectOnSessionTimeout()
        If Session("SessionTimeOut") <> "Yes" Then
            If Not Context.Session Is Nothing Then
                If Session.IsNewSession Then
                    Dim cookieHeader = Request.Headers("Cookie")

                    If Not cookieHeader Is Nothing AndAlso cookieHeader.IndexOf("ASP.NET_SessionId") >= 0 Then
                        If Page.IsCallback Then
                            ASPxWebControl.RedirectOnCallback(_UnderwritingSessionTimeOutaspx)
                        Else
                            Response.Redirect(_UnderwritingSessionTimeOutaspx)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub gvAttachments_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs) Handles gvAttachments.CustomColumnDisplayText
        If Not e.Value Is Nothing AndAlso (e.Column.FieldName = "FileName" OrElse e.Column.FieldName = "FileDescription") Then
            Dim fileInfo As New FileInfo(e.Value)
            e.DisplayText = fileInfo.Name
        End If
    End Sub

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)

        'Redirects the page when the session time out
        Me.RedirectOnSessionTimeout()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub txtName_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtName As ASPxTextBox = TryCast(sender, ASPxTextBox)
        If txtName.Text <> "" Then
            Dim fileInfo As New FileInfo(txtName.Text)
            If Not fileInfo Is Nothing AndAlso fileInfo.Exists Then
                txtName.Text = fileInfo.Name
            End If
        End If
    End Sub

    Protected Sub gvAttachments_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles gvAttachments.RowInserting
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        Dim txtDescription As ASPxTextBox = TryCast(grid.FindEditFormTemplateControl("txtDescription"), ASPxTextBox)
        e.NewValues("FileName") = Session("FileName")

        If txtDescription.Text <> "" Then
            e.NewValues("FileDescription") = txtDescription.Text
        Else
            e.NewValues("FileDescription") = Session("FileName")
        End If

        e.NewValues("UploadedDate") = Date.Now
        Session.Remove("FileName")
    End Sub

    Protected Sub uploadFile_FilesUploadComplete(ByVal sender As Object, ByVal e As System.EventArgs)
        If (TryCast(sender, ASPxUploadControl)).UploadedFiles(0).IsValid Then
            Dim serverPath As String = Server.MapPath("~\Uploads") 'ConfigurationManager.AppSettings("Url.WebApplication.Uploads").ToString())
            Dim requirementId As String = String.Empty
            Dim uCaseId As String = String.Empty

            'InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.GetFileNamePrefix(uCaseId, requirementId)

            Dim uploader As ASPxUploadControl = (TryCast(sender, ASPxUploadControl))
            Session("FileName") = String.Format("{0}\{1}",
                                                serverPath,
                                                String.Format("{0}_{1}_{2}", uCaseId, requirementId, uploader.UploadedFiles(0).FileName))
            uploader.UploadedFiles(0).SaveAs(Session("FileName"))
        End If
    End Sub

    Protected Sub gvAttachments_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs) Handles gvAttachments.CustomCallback
        Dim value As String = e.Parameters

        If Not value.ToLower() = "true" Then
            Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
            Dim filePath As String = grid.GetRowValues(Convert.ToInt32(e.Parameters), "FileName")
            Dim fileInfo As New FileInfo(filePath)

            ASPxWebControl.RedirectOnCallback(String.Format("~\Underwriting\Controls\Exporter.aspx?filename={0}", fileInfo.Name))
        Else
            'Redirects the page when the session time out
            Me.RedirectOnSessionTimeout()
        End If
    End Sub

    Protected Sub gvAttachments_HtmlRowCreated(ByVal sender As Object, ByVal e As ASPxGridViewTableRowEventArgs) Handles gvAttachments.HtmlRowCreated
        If e.RowType = GridViewRowType.Data Then
            Dim col As GridViewDataColumn = TryCast(TryCast(sender, ASPxGridView).Columns("colShow"), GridViewDataColumn)
            Dim button As ASPxButton = TryCast(sender, ASPxGridView).FindRowCellTemplateControl(e.VisibleIndex, col, "btnWatch")

            ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(button)

            If Not button Is Nothing Then
                button.ClientSideEvents.Click = String.Format("function(s,e){{ gvAttachments.PerformCallback({0}); }}", e.VisibleIndex)
            End If
        End If
    End Sub

    Protected Sub gvAttachments_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles gvAttachments.RowUpdating
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        Dim txtDescription As ASPxTextBox = TryCast(grid.FindEditFormTemplateControl("txtDescription"), ASPxTextBox)
        e.NewValues("FileDescription") = txtDescription.Text
    End Sub

    Protected Sub txtDescription_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtName As ASPxTextBox = TryCast(sender, ASPxTextBox)
        If txtName.Text <> "" Then
            Dim fileInfo As New FileInfo(txtName.Text)
            If Not fileInfo Is Nothing AndAlso fileInfo.Exists Then
                txtName.Text = fileInfo.Name
            End If
        End If
    End Sub

    Protected Sub gvAttachments_HtmlEditFormCreated(ByVal sender As Object, ByVal e As ASPxGridViewEditFormEventArgs) Handles gvAttachments.HtmlEditFormCreated
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)

        If grid.IsEditing And Not grid.IsNewRowEditing Then
            Dim uploader As ASPxUploadControl = TryCast(grid.FindEditFormTemplateControl("uploadFile"), ASPxUploadControl)
            uploader.Visible = False
        End If
    End Sub

    Protected Sub gvAttachments_RowDeleted(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletedEventArgs) Handles gvAttachments.RowDeleted
        If IO.File.Exists(e.Values("FileName")) Then
            IO.File.Delete(e.Values("FileName"))
        End If
    End Sub

    Protected Sub RequirementsAttachments_gvAttachments_CustomErrorText(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomErrorTextEventArgs) Handles gvAttachments.CustomErrorText

    End Sub

    Protected Sub RequirementsAttachments_gvAttachments_CommandButtonInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCommandButtonEventArgs) Handles gvAttachments.CommandButtonInitialize
        If Not Session("IsEditMode") Then
            e.Visible = False
        End If
    End Sub
End Class
