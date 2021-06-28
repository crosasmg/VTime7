#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System
Imports DevExpress.Web.Data
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Common.Helpers.Language
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Data
Imports System.IO
Imports DevExpress.Web.ASPxClasses
Imports System.Data
Imports System.Data.Common
Imports DevExpress.Web.ASPxUploadControl
Imports System.Collections.Generic

#End Region

Partial Class Maintenance_WidgetsInRolesManager
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
            RoleID.DataBind()
        End If

        If Not CurrentState.Contains("LanguageId") Then
            CurrentState.Add("LanguageId", LanguageId)
        End If
    End Sub

#End Region

#Region "Integration Data"

    Private Function IntegrationModel() As Integration.Contracts.Model
        Dim result As New Integration.Contracts.Model("WidgetsInRoles")

        With result
            .ConnectionStringName = "FrontOfficeConnectionString"

            If Not IsNothing(GetLocalResourceObject("PageResource.Title")) Then
                .Title = GetLocalResourceObject("PageResource.Title").ToString
            Else
                .Title = "Widgets In Roles"
            End If

            .Sequence.TableName = "WidgetsInRoles"
            .Sequence.ColumnName = "ID"
            With .AddColumn(1, "ID", DbType.Decimal, 9)
                If Not IsNothing(GetLocalResourceObject("ID.Caption")) Then
                    .Title = GetLocalResourceObject("ID.Caption").ToString
                Else
                    .Title = "Indentificator"
                End If

                If Not IsNothing(GetLocalResourceObject("ID.ToolTip")) Then
                    .Comment = GetLocalResourceObject("ID.ToolTip").ToString
                Else
                    .Comment = "Identificator of the User In Rol"
                End If

                .Scale = 0
                .Sequence = True

            End With
            With .AddColumn(2, "WidgetID", DbType.Decimal, 9)
                If Not IsNothing(GetLocalResourceObject("WidgetID.Caption")) Then
                    .Title = GetLocalResourceObject("WidgetID.Caption").ToString
                Else
                    .Title = "Widget ID"
                End If

                If Not IsNothing(GetLocalResourceObject("WidgetID.ToolTip")) Then
                    .Comment = GetLocalResourceObject("WidgetID.ToolTip").ToString
                Else
                    .Comment = "Identificator of the Widget"
                End If

                .Scale = 0
                .LookUpStatement = "SELECT WIDGET.ID, WIDGETTRANS.DESCRIPTION FROM FRONTOFFICE.WIDGET WIDGET JOIN .WIDGETTRANS WIDGETTRANS ON WIDGETTRANS.ID = WIDGET.ID WHERE LANGUAGEID=1 ORDER BY WIDGETTRANS.DESCRIPTION ASC"

            End With
            With .AddColumn(3, "RoleID", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("RoleID.Caption")) Then
                    .Title = GetLocalResourceObject("RoleID.Caption").ToString
                Else
                    .Title = "Role ID"
                End If

                If Not IsNothing(GetLocalResourceObject("RoleID.ToolTip")) Then
                    .Comment = GetLocalResourceObject("RoleID.ToolTip").ToString
                Else
                    .Comment = "Identificaror of the Role"
                End If

                .Scale = 0
                .LookUpStatement = "SELECT ROLE.ROLEID, ROLE.ROLENAME FROM FRONTOFFICE.ROLE ROLE "

            End With
            With .AddColumn(4, "IsDefault", DbType.Decimal, 1)
                If Not IsNothing(GetLocalResourceObject("IsDefault.Caption")) Then
                    .Title = GetLocalResourceObject("IsDefault.Caption").ToString
                Else
                    .Title = "Is Default"
                End If

                If Not IsNothing(GetLocalResourceObject("IsDefault.ToolTip")) Then
                    .Comment = GetLocalResourceObject("IsDefault.ToolTip").ToString
                Else
                    .Comment = "Is Default "
                End If

                .Scale = 0

            End With
            With .AddColumn(5, "IsEditAllow", DbType.Decimal, 1)
                If Not IsNothing(GetLocalResourceObject("IsEditAllow.Caption")) Then
                    .Title = GetLocalResourceObject("IsEditAllow.Caption").ToString
                Else
                    .Title = "Is Edit Allow"
                End If

                If Not IsNothing(GetLocalResourceObject("IsEditAllow.ToolTip")) Then
                    .Comment = GetLocalResourceObject("IsEditAllow.ToolTip").ToString
                Else
                    .Comment = "Is Edit Allow of Windget in Role"
                End If

                .Scale = 0

            End With
            With .AddColumn(6, "IsAllowedToEditTheTitle", DbType.Decimal, 1)
                If Not IsNothing(GetLocalResourceObject("IsAllowedToEditTheTitle.Caption")) Then
                    .Title = GetLocalResourceObject("IsAllowedToEditTheTitle.Caption").ToString
                Else
                    .Title = "Is Allowed to Edit the Title"
                End If

                If Not IsNothing(GetLocalResourceObject("IsAllowedToEditTheTitle.ToolTip")) Then
                    .Comment = GetLocalResourceObject("IsAllowedToEditTheTitle.ToolTip").ToString
                Else
                    .Comment = "Is Allowed to Edit the Title of the Windget in Role"
                End If

                .Scale = 0

            End With
            With .AddColumn(7, "Sequence", DbType.Decimal, 5)
                If Not IsNothing(GetLocalResourceObject("Sequence.Caption")) Then
                    .Title = GetLocalResourceObject("Sequence.Caption").ToString
                Else
                    .Title = "Sequence"
                End If

                If Not IsNothing(GetLocalResourceObject("Sequence.ToolTip")) Then
                    .Comment = GetLocalResourceObject("Sequence.ToolTip").ToString
                Else
                    .Comment = "Sequence"
                End If

                .Scale = 0

            End With

        End With

        Return result
    End Function

#End Region

#Region "MainMenu Events"

    Protected Sub MainMenu_ItemClick(source As Object, e As DevExpress.Web.ASPxMenu.MenuItemEventArgs) Handles MainMenu.ItemClick

    End Sub

#End Region

#Region "Controls Events"

    Protected Sub ExcelFileUpload_FileUploadComplete(sender As Object, e As FileUploadCompleteEventArgs) Handles ExcelFileUpload.FileUploadComplete
        Dim filePath As String = Server.MapPath("~/temp/" + e.UploadedFile.FileName)

        e.UploadedFile.SaveAs(filePath)
        e.CallbackData = IO.Path.GetFileName(e.UploadedFile.FileName)
    End Sub

    Protected Sub RoleID_DataBinding(sender As Object, e As EventArgs) Handles RoleID.DataBinding
        If Not IsPostBack Then
            Caching.Remove("Role")
        End If
        If Caching.Exist("Role") Then
            RoleID.DataSource = Caching.GetItem("Role")
        Else
            Dim source As DataTable = Nothing

            With New DataManagerFactory("SELECT  ROLE.ROLEID, ROLE.ROLENAME FROM FRONTOFFICE.ROLE ROLE   ", "Role", "FrontOfficeConnectionString")

                source = .QueryExecuteToTable(True)
                RoleID.DataSource = source
            End With

            If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                Caching.SetItem("Role", source)
            End If
        End If
    End Sub

#End Region

#Region "WidgetsInRoles Events"

    Protected Sub WidgetsInRoles_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles WidgetsInRoles.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub WidgetsInRoles_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles WidgetsInRoles.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("WidgetsInRoles")) Or _internalCall Then
            If Not IsNothing(RoleID.SelectedItem) Then

                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  WIDGET.ID, WIDGETTRANS.DESCRIPTION " & _
                                            " FROM FRONTOFFICE.WIDGET WIDGET " & _
                                            " JOIN FRONTOFFICE.WIDGETTRANS WIDGETTRANS " & _
                                              " ON WIDGETTRANS.ID = WIDGET.ID " & _
                                           " WHERE WIDGETTRANS.LANGUAGEID = @:LANGUAGEID" & _
                                        " ORDER BY WIDGETTRANS.DESCRIPTION ASC ",
                                              "Widget", "FrontOfficeConnectionString")

                    .AddParameter("LANGUAGEID", DbType.Int32, 1, False, LanguageId)
                    source = .QueryExecuteToTable(True)
                    DirectCast(WidgetsInRoles.Columns("WIDGETID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("Widget", source)
                End If

                With New DataManagerFactory("SELECT  WIDGETSINROLES.ID, " & _
                                                   " WIDGETTRANS.DESCRIPTION," & _
                                                   " WIDGETSINROLES.WIDGETID, " & _
                                                   " NVL(WIDGETSINROLES.ISDEFAULT, 0) ISDEFAULT, " &
                                                   " NVL(WIDGETSINROLES.ISEDITALLOW, 0) ISEDITALLOW, " & _
                                                   " NVL(WIDGETSINROLES.ISALLOWEDTOEDITTHETITLE, 0) ISALLOWEDTOEDITTHETITLE, " &
                                                   " WIDGETSINROLES.SEQUENCE, " & _
                                                   " WIDGETSINROLES.ROLEID " &
                                            " FROM   FRONTOFFICE.WIDGETSINROLES WIDGETSINROLES " & _
                                      " INNER JOIN   WIDGETTRANS " & _
                                              " ON   WIDGETSINROLES.WIDGETID = WIDGETTRANS.ID " & _
                                           " WHERE   WIDGETSINROLES.ROLEID = @:ROLEID AND " & _
                                                   " WIDGETTRANS.LANGUAGEID = @:LANGUAGEID " & _
                                        " ORDER BY   WIDGETTRANS.DESCRIPTION ASC",
                                           "WIDGETSINROLES", "FrontOfficeConnectionString")

                    .AddParameter("ROLEID", DbType.AnsiString, 255, (RoleID.SelectedItem.Value = String.Empty), RoleID.SelectedItem.Value)
                    .AddParameter("LANGUAGEID", DbType.Int32, 1, False, LanguageId)

                    WidgetsInRoles.DataSource = .QueryExecuteToTable(True)
                End With

            End If
        End If
    End Sub

    Protected Sub WidgetsInRoles_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles WidgetsInRoles.CellEditorInitialize
        If WidgetsInRoles.IsNewRowEditing Then
            Select Case e.Column.FieldName
                Case "ID"
                    With New DataManagerFactory("SELECT NVL(MAX(WIDGETSINROLES.ID),0) + 1 FROM FRONTOFFICE.WidgetsInRoles", "WidgetsInRoles", "FrontOfficeConnectionString")

                        e.Editor.Value = .QueryExecuteScalarToInteger
                        e.Editor.Width = New Unit("50px")
                    End With
                Case Else
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ID"
                    e.Editor.Enabled = False

                Case "WIDGETID"
                    e.Editor.Focus()
            End Select
        End If

        Select Case e.Column.FieldName
            Case "ID"

            Case "WIDGETID"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

        End Select
    End Sub

    Protected Sub WidgetsInRoles_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles WidgetsInRoles.RowInserting
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("INSERT INTO FRONTOFFICE.WidgetsInRoles (ID, WIDGETID, ISDEFAULT, ISEDITALLOW, ISALLOWEDTOEDITTHETITLE, SEQUENCE, ROLEID) VALUES (@:ID, @:WIDGETID, @:ISDEFAULT, @:ISEDITALLOW, @:ISALLOWEDTOEDITTHETITLE, @:SEQUENCE, @:ROLEID)", "WidgetsInRoles", "FrontOfficeConnectionString")

            .AddParameter("ID", DbType.Decimal, 0, False, e.NewValues("ID"))
            .AddParameter("WIDGETID", DbType.Decimal, 0, (e.NewValues("WIDGETID") = 0), e.NewValues("WIDGETID"))
            .AddParameter("ISDEFAULT", DbType.Decimal, 0, (IIf(IsNothing(e.NewValues("ISDEFAULT")), 0, e.NewValues("ISDEFAULT")) = 0), IIf(IsNothing(e.NewValues("ISDEFAULT")), 0, e.NewValues("ISDEFAULT")))
            .AddParameter("ISEDITALLOW", DbType.Decimal, 0, (IIf(IsNothing(e.NewValues("ISEDITALLOW")), 0, e.NewValues("ISEDITALLOW")) = 0), IIf(IsNothing(e.NewValues("ISEDITALLOW")), 0, e.NewValues("ISEDITALLOW")))
            .AddParameter("ISALLOWEDTOEDITTHETITLE", DbType.Decimal, 0, (IIf(IsNothing(e.NewValues("ISALLOWEDTOEDITTHETITLE")), 0, e.NewValues("ISALLOWEDTOEDITTHETITLE")) = 0), IIf(IsNothing(e.NewValues("ISALLOWEDTOEDITTHETITLE")), 0, e.NewValues("ISALLOWEDTOEDITTHETITLE")))
            .AddParameter("SEQUENCE", DbType.Decimal, 0, (e.NewValues("SEQUENCE") = 0), e.NewValues("SEQUENCE"))
            .AddParameter("ROLEID", DbType.AnsiString, 0, (RoleID.SelectedItem.Value = String.Empty), RoleID.SelectedItem.Value)

            .CommandExecute()
        End With

        e.Cancel = True
        WidgetsInRoles.CancelEdit()

        CleanCaching()
    End Sub

    Protected Sub WidgetsInRoles_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles WidgetsInRoles.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE FRONTOFFICE.WidgetsInRoles SET WIDGETID = @:WIDGETID, ISDEFAULT = @:ISDEFAULT, ISEDITALLOW = @:ISEDITALLOW, ISALLOWEDTOEDITTHETITLE = @:ISALLOWEDTOEDITTHETITLE, SEQUENCE = @:SEQUENCE, ROLEID = @:ROLEID WHERE ID = @:ID", "WidgetsInRoles", "FrontOfficeConnectionString")

            .AddParameter("WIDGETID", DbType.Decimal, 0, (e.NewValues("WIDGETID") = 0), e.NewValues("WIDGETID"))
            .AddParameter("ISDEFAULT", DbType.Decimal, 0, (IIf(IsNothing(e.NewValues("ISDEFAULT")), 0, e.NewValues("ISDEFAULT")) = 0), IIf(IsNothing(e.NewValues("ISDEFAULT")), 0, e.NewValues("ISDEFAULT")))
            .AddParameter("ISEDITALLOW", DbType.Decimal, 0, (IIf(IsNothing(e.NewValues("ISEDITALLOW")), 0, e.NewValues("ISEDITALLOW")) = 0), IIf(IsNothing(e.NewValues("ISEDITALLOW")), 0, e.NewValues("ISEDITALLOW")))
            .AddParameter("ISALLOWEDTOEDITTHETITLE", DbType.Decimal, 0, (IIf(IsNothing(e.NewValues("ISALLOWEDTOEDITTHETITLE")), 0, e.NewValues("ISALLOWEDTOEDITTHETITLE")) = 0), IIf(IsNothing(e.NewValues("ISALLOWEDTOEDITTHETITLE")), 0, e.NewValues("ISALLOWEDTOEDITTHETITLE")))
            .AddParameter("SEQUENCE", DbType.Decimal, 0, (e.NewValues("SEQUENCE") = 0), e.NewValues("SEQUENCE"))
            .AddParameter("ROLEID", DbType.AnsiString, 0, (RoleID.SelectedItem.Value = String.Empty), RoleID.SelectedItem.Value)
            .AddParameter("ID", DbType.Decimal, 0, False, e.Keys("ID"))

            .CommandExecute()
        End With

        e.Cancel = True
        WidgetsInRoles.CancelEdit()

        CleanCaching()

    End Sub


    Public Sub CleanCaching()
        InMotionGIT.Common.Helpers.Caching.RemoveStartWith("Widgets")
        Dim cacheKey As String = String.Format("{0}{1}{2}", "Widgets", UserInfo.RoleName, UserInfo.User.LanguageID)
        InMotionGIT.Common.Helpers.Caching.Remove(cacheKey)
    End Sub

    Protected Sub WidgetsInRoles_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles WidgetsInRoles.CustomCallback
        Dim isNullResult As Boolean = True

        Select Case e.Parameters.ToString.ToLower
            Case "delete"
                Dim IDKey As Generic.List(Of Object) = WidgetsInRoles.GetSelectedFieldValues("ID")

                For index As Integer = 0 To IDKey.Count - 1
                    With New DataManagerFactory("DELETE FROM FRONTOFFICE.WidgetsInRoles WHERE ID = @:ID ", "WidgetsInRoles", "FrontOfficeConnectionString")

                        .AddParameter("ID", DbType.Decimal, 0, False, IDKey(index))

                        .CommandExecute()
                    End With

                Next

                WidgetsInRoles.DataBind()

                CleanCaching()

            Case Else
                Dim fileName As String = String.Empty

                If e.Parameters.ToString.ToLower.StartsWith("export") Then
                    Dim extension As String = e.Parameters.ToString.ToLower.Split("_")(1)
                    fileName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", IO.Path.GetRandomFileName, extension)

                    ASPxGridViewExporter.GridViewID = sender.ClientInstanceName

                    Using fs As FileStream = New FileStream(String.Format(CultureInfo.InvariantCulture, "{0}\temp\{1}", Server.MapPath("/"), fileName), FileMode.Create)
                        Select Case extension
                            Case "pdf"
                                ASPxGridViewExporter.WritePdf(fs)
                            Case "xls"
                                ASPxGridViewExporter.WriteXls(fs)
                            Case "xlsx"
                                ASPxGridViewExporter.WriteXlsx(fs)
                            Case "rtf"
                                ASPxGridViewExporter.WriteRtf(fs)
                            Case Else
                        End Select
                    End Using

                    ASPxWebControl.RedirectOnCallback(String.Format(CultureInfo.InvariantCulture, "~/dropthings/download.ashx?Directory=temp&File={0}", fileName))

                ElseIf e.Parameters.ToString.StartsWith("template.") Then
                    Dim path As String = String.Empty
                    Dim values As String = e.Parameters.ToString

                    If Not IsNothing(GetLocalResourceObject("PageResource.Title")) Then
                        fileName = String.Format(CultureInfo.InvariantCulture, "{0}.xlsx", GetLocalResourceObject("PageResource.Title").ToString)
                    Else
                        fileName = String.Format(CultureInfo.InvariantCulture, "{0}.xlsx", "Widgets In Roles")
                    End If

                    path = String.Format(CultureInfo.InvariantCulture, "{0}\{1}", Server.MapPath("/temp"), fileName)
                    Integration.Exports.Excel.DoWork(IntegrationModel, path, values.Split(".")(2), values.Split(".")(1))

                    ASPxWebControl.RedirectOnCallback(String.Format(CultureInfo.InvariantCulture, "/dropthings/download.ashx?Directory=temp&File={0}", fileName))

                ElseIf e.Parameters.ToString.ToLower.StartsWith("import_") Then
                    fileName = String.Format(CultureInfo.InvariantCulture, "{0}\{1}", Server.MapPath("/temp"), e.Parameters.ToString.ToLower.Split("_")(1))

                    Integration.Import.Excel.DoWorkWithDataManager(IntegrationModel, fileName)
                End If
        End Select
    End Sub

    Protected Sub WidgetsInRoles_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles WidgetsInRoles.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        If e.IsNewRow Then
            Dim widGetIdValue As Integer = e.NewValues("WIDGETID")
            Dim roleIdValue As Integer = RoleID.SelectedItem.Value
            Dim widgetExist As Integer
            With New DataManagerFactory("SELECT COUNT(*) " & _
                                         " FROM WIDGETSINROLES " & _
                                        " WHERE WIDGETSINROLES.WIDGETID = @:WIDGETID AND " & _
                                        "       WIDGETSINROLES.ROLEID = @:ROLEID  ",
                                        "WidgetsInRoles", "FrontOfficeConnectionString")
                .AddParameter("WIDGETID", DbType.Decimal, 0, (widGetIdValue = 0), widGetIdValue)
                .AddParameter("ROLEID", DbType.Decimal, 0, (widGetIdValue = 0), roleIdValue)
                widgetExist = .QueryExecuteScalarToInteger
            End With
            If widgetExist <> 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("WidgetIdExistente").ToString)
                e.RowError = errorMessage
            End If

        Else
            If e.Errors.Count > 0 Then
                For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                    errorMessage += String.Format("<li>{0}</li>", item.Value)
                Next

                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("MessageErrorText").ToString)

                e.RowError = errorMessage
            End If
        End If
    End Sub

#End Region

End Class