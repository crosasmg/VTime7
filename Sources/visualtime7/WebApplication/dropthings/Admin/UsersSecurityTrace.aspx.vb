#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System
Imports DevExpress.Web.Data
Imports InMotionGIT.Common
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Common.Helpers.Language
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Data
Imports System.IO
Imports DevExpress.Web.ASPxClasses
Imports System.Data
Imports System.Data.Common
Imports DevExpress.Web.ASPxUploadControl
Imports DevExpress.Web.ASPxPopupControl

#End Region

Partial Class dropthings_Admin_UsersSecurityTrace
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
        Dim result As New Integration.Contracts.Model("UsersSecurityTrace")

        With result
            .ConnectionStringName = "FrontOfficeConnectionString"

            If Not IsNothing(GetLocalResourceObject("PageResource.Title")) Then
                .Title = GetLocalResourceObject("PageResource.Title").ToString
            Else
                .Title = "Users Security Trace"
            End If

            .Sequence.TableName = "UsersSecurityTrace"
            .Sequence.ColumnName = "ID"
            With .AddColumn(1, "ID", DbType.Decimal, 9)
                If Not IsNothing(GetLocalResourceObject("ID.Caption")) Then
                    .Title = GetLocalResourceObject("ID.Caption").ToString
                Else
                    .Title = "ID"
                End If

                If Not IsNothing(GetLocalResourceObject("ID.ToolTip")) Then
                    .Comment = GetLocalResourceObject("ID.ToolTip").ToString
                Else
                    .Comment = "Identifier"
                End If

                .Scale = 0
                .Sequence = True

            End With
            With .AddColumn(2, "IPAddress", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("IPAddress.Caption")) Then
                    .Title = GetLocalResourceObject("IPAddress.Caption").ToString
                Else
                    .Title = "IP Address"
                End If

                If Not IsNothing(GetLocalResourceObject("IPAddress.ToolTip")) Then
                    .Comment = GetLocalResourceObject("IPAddress.ToolTip").ToString
                Else
                    .Comment = "IP Address"
                End If

                .Scale = 0

            End With
            With .AddColumn(3, "Email", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("Email.Caption")) Then
                    .Title = GetLocalResourceObject("Email.Caption").ToString
                Else
                    .Title = "Email"
                End If

                If Not IsNothing(GetLocalResourceObject("Email.ToolTip")) Then
                    .Comment = GetLocalResourceObject("Email.ToolTip").ToString
                Else
                    .Comment = "Email"
                End If

                .Scale = 0

            End With
            With .AddColumn(4, "State", DbType.Decimal, 5)
                If Not IsNothing(GetLocalResourceObject("State.Caption")) Then
                    .Title = GetLocalResourceObject("State.Caption").ToString
                Else
                    .Title = "State"
                End If

                If Not IsNothing(GetLocalResourceObject("State.ToolTip")) Then
                    .Comment = GetLocalResourceObject("State.ToolTip").ToString
                Else
                    .Comment = "State"
                End If

                .Scale = 0
                .LookUpStatement = "SELECT LOOKUP.CODE, LOOKUP.DESCRIPTION FROM FRONTOFFICE.LOOKUP LOOKUP WHERE LOOKUPID=8 AND RECORDSTATUS=1 AND LANGUAGEID=User.LanguageId ORDER BY LookUp.QueryOrder"

            End With
            With .AddColumn(5, "Result", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("Result.Caption")) Then
                    .Title = GetLocalResourceObject("Result.Caption").ToString
                Else
                    .Title = "Result"
                End If

                If Not IsNothing(GetLocalResourceObject("Result.ToolTip")) Then
                    .Comment = GetLocalResourceObject("Result.ToolTip").ToString
                Else
                    .Comment = "Result"
                End If

                .Scale = 0

            End With
            With .AddColumn(6, "EffectDate", DbType.DateTime, 8)
                If Not IsNothing(GetLocalResourceObject("EffectDate.Caption")) Then
                    .Title = GetLocalResourceObject("EffectDate.Caption").ToString
                Else
                    .Title = "Effect Date"
                End If

                If Not IsNothing(GetLocalResourceObject("EffectDate.ToolTip")) Then
                    .Comment = GetLocalResourceObject("EffectDate.ToolTip").ToString
                Else
                    .Comment = "Effect Date"
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

#Region "UsersSecurityTrace Events"


    Protected Sub DateTo_Init(sender As Object, e As EventArgs)
        DateTo.Date = Date.Now
    End Sub
    Protected Sub DateFrom_Init(sender As Object, e As EventArgs)
        DateFrom.Date = Date.Now
    End Sub



    Protected Sub UsersSecurityTrace_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles UsersSecurityTrace.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub UsersSecurityTrace_DataBinding(sender As Object, e As EventArgs) Handles UsersSecurityTrace.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("UsersSecurityTrace")) Or _internalCall Then

            With New DataManagerFactory("SELECT LOOKUP.CODE, LOOKUP.DESCRIPTION " & _
                                         " FROM FRONTOFFICE.LOOKUP LOOKUP " & _
                                        " WHERE LOOKUP.LOOKUPID = 8 " & _
                                              " AND LOOKUP.RECORDSTATUS = 1 " & _
                                              " AND LOOKUP.LANGUAGEID = @:LANGUAGEID ",
                                        "Lookup", "FrontOfficeConnectionString")

                .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, UserInfo.CulturalNameToLanguageId)

                DirectCast(UsersSecurityTrace.Columns("STATE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = .QueryExecuteToTable(True)
            End With

            Dim tempDateTo As Date = Convert.ToDateTime(DateTo.Value)
            Dim tempDateFrom As Date = Convert.ToDateTime(DateFrom.Value)
            tempDateTo = tempDateTo.AddDays(1)
            With New DataManagerFactory(String.Format("SELECT USERSSECURITYTRACE.ID, USERSSECURITYTRACE.IPADDRESS, " & _
                                         " USERSSECURITYTRACE.EMAIL, USERSSECURITYTRACE.STATE, " & _
                                         " USERSSECURITYTRACE.RESULT, USERSSECURITYTRACE.EFFECTDATE " & _
                                    " FROM USERSSECURITYTRACE USERSSECURITYTRACE  " & _
                                   " WHERE EFFECTDATE between to_date ('{0}', 'yyyy/mm/dd') AND to_date ('{1}', 'yyyy/mm/dd') " & _
                                   "  ORDER BY USERSSECURITYTRACE.EFFECTDATE DESC ", tempDateFrom.ToString("yyyy/MM/dd"), tempDateTo.ToString("yyyy/MM/dd")),
                                   "USERSSECURITYTRACE", "FrontOfficeConnectionString")

                UsersSecurityTrace.DataSource = .QueryExecuteToTable(True)
            End With

        End If
    End Sub

    Protected Sub UsersSecurityTrace_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles UsersSecurityTrace.CellEditorInitialize
        If UsersSecurityTrace.IsNewRowEditing Then
            Select Case e.Column.FieldName
                Case "ID"
                    e.Editor.Focus()
            End Select

        Else
            Select Case e.Column.FieldName
                Case "ID"
                    e.Editor.Enabled = False

                Case "EFFECTDATE"
                    e.Editor.Focus()
            End Select
        End If

        Select Case e.Column.FieldName
            Case "ID"

            Case "STATE"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

        End Select
    End Sub

    Protected Sub UsersSecurityTrace_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles UsersSecurityTrace.RowInserting
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("INSERT INTO UsersSecurityTrace (ID, IPADDRESS, EMAIL, STATE, RESULT, EFFECTDATE) VALUES (@:ID, @:IPADDRESS, @:EMAIL, @:STATE, @:RESULT, @:EFFECTDATE)",
                                    "UsersSecurityTrace", "FrontOfficeConnectionString")

            .AddParameter("ID", DbType.Decimal, 0, False, e.NewValues("ID"))
            .AddParameter("IPADDRESS", DbType.AnsiString, 0, (e.NewValues("IPADDRESS") = String.Empty), e.NewValues("IPADDRESS"))
            .AddParameter("EMAIL", DbType.AnsiString, 0, (e.NewValues("EMAIL") = String.Empty), e.NewValues("EMAIL"))
            .AddParameter("STATE", DbType.Decimal, 0, (e.NewValues("STATE") = 0), e.NewValues("STATE"))
            .AddParameter("RESULT", DbType.AnsiString, 0, (e.NewValues("RESULT") = String.Empty), e.NewValues("RESULT"))
            .AddParameter("EFFECTDATE", DbType.DateTime, 0, (e.NewValues("EFFECTDATE") = Date.MinValue), e.NewValues("EFFECTDATE"))

            .CommandExecute()
        End With

        e.Cancel = True
        UsersSecurityTrace.CancelEdit()
    End Sub

    Protected Sub UsersSecurityTrace_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles UsersSecurityTrace.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE UsersSecurityTrace SET IPADDRESS = @:IPADDRESS, EMAIL = @:EMAIL, STATE = @:STATE, RESULT = @:RESULT, EFFECTDATE = @:EFFECTDATE WHERE ID = @:ID",
                                    "UsersSecurityTrace", "FrontOfficeConnectionString")

            .AddParameter("IPADDRESS", DbType.AnsiString, 0, (e.NewValues("IPADDRESS") = String.Empty), e.NewValues("IPADDRESS"))
            .AddParameter("EMAIL", DbType.AnsiString, 0, (e.NewValues("EMAIL") = String.Empty), e.NewValues("EMAIL"))
            .AddParameter("STATE", DbType.Decimal, 0, (e.NewValues("STATE") = 0), e.NewValues("STATE"))
            .AddParameter("RESULT", DbType.AnsiString, 0, (e.NewValues("RESULT") = String.Empty), e.NewValues("RESULT"))
            .AddParameter("EFFECTDATE", DbType.DateTime, 0, (e.NewValues("EFFECTDATE") = Date.MinValue), e.NewValues("EFFECTDATE"))
            .AddParameter("ID", DbType.Decimal, 0, False, e.Keys("ID"))

            .CommandExecute()
        End With

        e.Cancel = True
        UsersSecurityTrace.CancelEdit()
    End Sub

    Protected Sub UsersSecurityTrace_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles UsersSecurityTrace.CustomCallback
        Dim isNullResult As Boolean = True

        Select Case e.Parameters.ToString.ToLower
            Case "delete"
                Dim IDKey As Generic.List(Of Object) = UsersSecurityTrace.GetSelectedFieldValues("ID")

                For index As Integer = 0 To IDKey.Count - 1
                    With New DataManagerFactory("DELETE FROM UsersSecurityTrace WHERE ID = @:ID ", "UsersSecurityTrace", "FrontOfficeConnectionString")

                        .AddParameter("ID", DbType.Decimal, 0, False, IDKey(index))

                        .CommandExecute()
                    End With

                Next

                UsersSecurityTrace.DataBind()

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
                        fileName = String.Format(CultureInfo.InvariantCulture, "{0}.xlsx", "Users Security Trace")
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

    Protected Sub UsersSecurityTrace_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles UsersSecurityTrace.RowValidating
        If e.Errors.Count > 0 Then
            Dim errorMessage As String = "<ol style='font-weight:lighter'>"

            For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                errorMessage += String.Format("<li>{0}</li>", item.Value)
            Next

            errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("MessageErrorText").ToString)

            e.RowError = errorMessage
        End If

    End Sub

#End Region

End Class