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

#End Region

Partial Class Maintenance_Authorized
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then

        End If

        If Not CurrentState.Contains("LanguageId") Then
            CurrentState.Add("LanguageId", LanguageId)
        End If
    End Sub

#End Region

#Region "Integration Data"

    Private Function IntegrationModel() As Integration.Contracts.Model
        Dim result As New Integration.Contracts.Model("Authorized")

        With result
            .ConnectionStringName = "FrontOfficeConnectionString"

            If Not IsNothing(GetLocalResourceObject("PageResource.Title")) Then
                .Title = GetLocalResourceObject("PageResource.Title").ToString
            Else
                .Title = "Administradores de EDW."
            End If

            With .AddColumn(1, "Name", DbType.AnsiString, 80)
                If Not IsNothing(GetLocalResourceObject("Name.Caption")) Then
                    .Title = GetLocalResourceObject("Name.Caption").ToString
                Else
                    .Title = "Nombre Usuario"
                End If

                If Not IsNothing(GetLocalResourceObject("Name.ToolTip")) Then
                    .Comment = GetLocalResourceObject("Name.ToolTip").ToString
                Else
                    .Comment = "Username or machine."
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

#End Region

#Region "Authorized Events"

    Protected Sub Authorized_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles Authorized.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub Authorized_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles Authorized.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("Authorized")) Or _internalCall Then

            With New DataManagerFactory("SELECT  AUTHORIZED.NAME FROM AUTHORIZED  ", "Authorized", "FrontOfficeConnectionString")

                Authorized.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub Authorized_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles Authorized.CellEditorInitialize
        If Authorized.IsNewRowEditing Then
            Select Case e.Column.FieldName

                Case "NAME"
                    e.Editor.Focus()
            End Select

        Else

            Select Case e.Column.FieldName
                Case "NAME"
                    e.Editor.Enabled = False

                Case ""
                    e.Editor.Focus()
            End Select
        End If

        Select Case e.Column.FieldName
            Case "NAME"

        End Select
    End Sub

    Protected Sub Authorized_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles Authorized.RowInserting
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("INSERT INTO AUTHORIZED (NAME) VALUES (@:NAME)", "Authorized", "FrontOfficeConnectionString")

            .AddParameter("NAME", DbType.AnsiString, 0, False, e.NewValues("NAME"))

            .CommandExecute()
        End With

        e.Cancel = True
        Authorized.CancelEdit()
    End Sub

    Protected Sub Authorized_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles Authorized.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE AUTHORIZED SET  WHERE NAME = @:NAME", "Authorized", "FrontOfficeConnectionString")

            .AddParameter("NAME", DbType.AnsiString, 0, False, e.Keys("NAME"))

            .CommandExecute()
        End With

        e.Cancel = True
        Authorized.CancelEdit()
    End Sub

    Protected Sub Authorized_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Authorized.CustomCallback
        Dim isNullResult As Boolean = True

        Select Case e.Parameters.ToString.ToLower
            Case "delete"
                Dim NAMEKey As Generic.List(Of Object) = Authorized.GetSelectedFieldValues("NAME")

                For index As Integer = 0 To NAMEKey.Count - 1
                    With New DataManagerFactory("DELETE FROM AUTHORIZED WHERE NAME = @:NAME ", "Authorized", "FrontOfficeConnectionString")

                        .AddParameter("NAME", DbType.AnsiString, 0, False, NAMEKey(index))

                        .CommandExecute()
                    End With

                Next

                Authorized.DataBind()

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
                        fileName = String.Format(CultureInfo.InvariantCulture, "{0}.xlsx", "Administradores de EDW.")
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

    Protected Sub Authorized_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles Authorized.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"

        If e.IsNewRow Then
            Dim AuthrorizedName As String = e.NewValues("NAME").ToString.Trim
            Dim autorizizedExist As Integer
            With New DataManagerFactory("SELECT COUNT (*) " & _
                                        "  FROM AUTHORIZED " & _
                                        " WHERE NAME = @:NAME ",
                                        "AUTHORIZED",
                                        "FrontOfficeConnectionString")
                .AddParameter("NAME", DbType.AnsiStringFixedLength, 80, False, AuthrorizedName)
                autorizizedExist = .QueryExecuteScalarToInteger
            End With
            If autorizizedExist <> 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("AuthrorizedExistente").ToString)
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