#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System
Imports DevExpress.Web.Data
Imports InMotionGIT.Common
Imports InMotionGIT.Common.Helpers.Language
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Data
Imports System.IO
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxMenu
Imports InMotionGIT.Common.Proxy
Imports System.Data
Imports System.Data.Common
Imports DevExpress.Web.ASPxUploadControl

#End Region

Partial Class dropthings_Admin_EmailTrace
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
        Dim result As New Integration.Contracts.Model("Jobs")

        With result
            .ConnectionStringName = "FrontOfficeConnectionString"

            If Not IsNothing(GetLocalResourceObject("PageResource.Title")) Then
                .Title = GetLocalResourceObject("PageResource.Title").ToString
            Else
                .Title = "E-mail Trace"
            End If


            With .AddColumn(1, "JOBID", DbType.AnsiString, 36)
                If Not IsNothing(GetLocalResourceObject("JOBID.Caption")) Then
                    .Title = GetLocalResourceObject("JOBID.Caption").ToString
                Else
                    .Title = "ID"
                End If

                If Not IsNothing(GetLocalResourceObject("JOBID.ToolTip")) Then
                    .Comment = GetLocalResourceObject("JOBID.ToolTip").ToString
                Else
                    .Comment = "Id"
                End If

                .Scale = 0

            End With
            With .AddColumn(2, "EMAIL", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("EMAIL.Caption")) Then
                    .Title = GetLocalResourceObject("EMAIL.Caption").ToString
                Else
                    .Title = "E-Mail"
                End If

                If Not IsNothing(GetLocalResourceObject("EMAIL.ToolTip")) Then
                    .Comment = GetLocalResourceObject("EMAIL.ToolTip").ToString
                Else
                    .Comment = "E-Mail"
                End If

                .Scale = 0

            End With
            With .AddColumn(3, "SUBJECT", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("SUBJECT.Caption")) Then
                    .Title = GetLocalResourceObject("SUBJECT.Caption").ToString
                Else
                    .Title = "Subject"
                End If

                If Not IsNothing(GetLocalResourceObject("SUBJECT.ToolTip")) Then
                    .Comment = GetLocalResourceObject("SUBJECT.ToolTip").ToString
                Else
                    .Comment = "Message Subject"
                End If

                .Scale = 0

            End With
            With .AddColumn(4, "BODY", DbType.AnsiString, 1000)
                If Not IsNothing(GetLocalResourceObject("BODY.Caption")) Then
                    .Title = GetLocalResourceObject("BODY.Caption").ToString
                Else
                    .Title = "Body"
                End If

                If Not IsNothing(GetLocalResourceObject("BODY.ToolTip")) Then
                    .Comment = GetLocalResourceObject("BODY.ToolTip").ToString
                Else
                    .Comment = "Message Body"
                End If

                .Scale = 0

            End With
            With .AddColumn(5, "FAIL", DbType.AnsiString, 1000)
                If Not IsNothing(GetLocalResourceObject("FAIL.Caption")) Then
                    .Title = GetLocalResourceObject("FAIL.Caption").ToString
                Else
                    .Title = "Fail"
                End If

                If Not IsNothing(GetLocalResourceObject("FAIL.ToolTip")) Then
                    .Comment = GetLocalResourceObject("FAIL.ToolTip").ToString
                Else
                    .Comment = "System of Fail"
                End If

                .Scale = 0

            End With
            With .AddColumn(6, "STATUS", DbType.Decimal, 8)
                If Not IsNothing(GetLocalResourceObject("STATUS.Caption")) Then
                    .Title = GetLocalResourceObject("STATUS.Caption").ToString
                Else
                    .Title = "Status"
                End If

                If Not IsNothing(GetLocalResourceObject("STATUS.ToolTip")) Then
                    .Comment = GetLocalResourceObject("STATUS.ToolTip").ToString
                Else
                    .Comment = "Message Status"
                End If

                .Scale = 0
                .LookUpStatement = "SELECT LOOKUP.CODE, LOOKUP.DESCRIPTION FROM FRONTOFFICE.LOOKUP LOOKUP WHERE LOOKUPID=5 AND RECORDSTATUS=1 AND LANGUAGEID=User.LanguageId ORDER BY LookUp.QueryOrder"

            End With
            With .AddColumn(7, "CREATEDBY", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("CREATEDBY.Caption")) Then
                    .Title = GetLocalResourceObject("CREATEDBY.Caption").ToString
                Else
                    .Title = "Created By"
                End If

                If Not IsNothing(GetLocalResourceObject("CREATEDBY.ToolTip")) Then
                    .Comment = GetLocalResourceObject("CREATEDBY.ToolTip").ToString
                Else
                    .Comment = "Created By"
                End If

                .Scale = 0
                .Visible = False
                .Audit = True
                .Constant = Session("nUserCode")

            End With
            With .AddColumn(8, "CREATEDON", DbType.DateTime, 8)
                If Not IsNothing(GetLocalResourceObject("CREATEDON.Caption")) Then
                    .Title = GetLocalResourceObject("CREATEDON.Caption").ToString
                Else
                    .Title = "Created On"
                End If

                If Not IsNothing(GetLocalResourceObject("CREATEDON.ToolTip")) Then
                    .Comment = GetLocalResourceObject("CREATEDON.ToolTip").ToString
                Else
                    .Comment = "Created On"
                End If

                .Scale = 0
                .Visible = False
                .Audit = True
                .Constant = "SYSDATE"

            End With
            With .AddColumn(9, "LASTUPDATEDBY", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("LASTUPDATEDBY.Caption")) Then
                    .Title = GetLocalResourceObject("LASTUPDATEDBY.Caption").ToString
                Else
                    .Title = "Last Update By"
                End If

                If Not IsNothing(GetLocalResourceObject("LASTUPDATEDBY.ToolTip")) Then
                    .Comment = GetLocalResourceObject("LASTUPDATEDBY.ToolTip").ToString
                Else
                    .Comment = "Last Update By"
                End If

                .Scale = 0
                .Visible = False
                .Audit = True
                .Constant = Session("nUserCode")

            End With
            With .AddColumn(10, "LASTUPDATEDON", DbType.DateTime, 8)
                If Not IsNothing(GetLocalResourceObject("LASTUPDATEDON.Caption")) Then
                    .Title = GetLocalResourceObject("LASTUPDATEDON.Caption").ToString
                Else
                    .Title = "Last Update On"
                End If

                If Not IsNothing(GetLocalResourceObject("LASTUPDATEDON.ToolTip")) Then
                    .Comment = GetLocalResourceObject("LASTUPDATEDON.ToolTip").ToString
                Else
                    .Comment = "Last Update On"
                End If

                .Scale = 0
                .Visible = False
                .Audit = True
                .Constant = "SYSDATE"

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

#Region "Jobs Events"

    Protected Sub DateTo_Init(sender As Object, e As EventArgs)
        DateTo.Date = Date.Now
    End Sub
    Protected Sub DateFrom_Init(sender As Object, e As EventArgs)
        DateFrom.Date = Date.Now
    End Sub

    Protected Sub Jobs_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles Jobs.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub Jobs_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles Jobs.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("Jobs")) Or _internalCall Then
            If Caching.Exist("Lookup") Then
                DirectCast(Jobs.Columns("STATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("Lookup")

            Else
                Dim source As DataTable = Nothing
                With New DataManagerFactory("SELECT " & _
                                            "	    LOOKUP.CODE, " & _
                                            "	    LOOKUP.DESCRIPTION " & _
                                            " FROM " & _
                                            "	    LOOKUP LOOKUP " & _
                                            " INNER " & _
                                            "  JOIN LOOKUPMASTER LookUpMaster " & _
                                            "    ON LOOKUP.LOOKUPID =  LookUpMaster.LOOKUPID " & _
                                            " WHERE " & _
                                            "	    LOOKUPMASTER.KEY = @:KEY " & _
                                            "       AND LOOKUP.RECORDSTATUS = 1 " & _
                                            "       AND LOOKUP.LANGUAGEID =@:LANGUAGEID ", "Lookup", "FrontOfficeConnectionString")
                    .AddParameter("KEY", DbType.String, 50, False, "MailStatus")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, UserInfo.User.LanguageID)

                    source = .QueryExecuteToTable(True)
                    DirectCast(Jobs.Columns("STATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("Lookup", source)
                End If
            End If


              Dim tempDateTo As Date = Convert.ToDateTime(DateTo.Value)
            Dim tempDateFrom As Date = Convert.ToDateTime(DateFrom.Value)
            tempDateTo = tempDateTo.AddDays(1)


            With New DataManagerFactory("SELECT " & _
                                                    "	 JOBS.JobId, " & _
                                                    "	 JOBS.EMAIL, " & _
                                                    "	 JOBS.SUBJECT, " & _
                                                    "	 JOBS.BODY, " & _
                                                    "	 JOBS.FAIL, " & _
                                                    "	 JOBS.STATUS, " & _
                                                    "	 JOBS.LASTUPDATEDON, " & _
                                                    "    JOBS.VIEWEDDATE " & _
                                                    " FROM " & _
                                                    "	 JOBS " & _
                                                    " WHERE " & _
                                                    "		JOBS.LASTUPDATEDON " & _
                                                    "       BETWEEN @:DATEFROM " & _
                                                    "       AND @:DATETO " & _
                                                    " ORDER BY JOBS.LASTUPDATEDON DESC ", "JOBS", "FrontOfficeConnectionString")
                .AddParameter("DATEFROM", DbType.DateTimeOffset, 10, False, tempDateFrom)
                .AddParameter("DATETO", DbType.DateTimeOffset, 10, False, tempDateTo)
                Jobs.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub Jobs_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles Jobs.CellEditorInitialize
        If Jobs.IsNewRowEditing Then
            Select Case e.Column.FieldName



                Case "JOBID"
                    e.Editor.Focus()
            End Select

        Else

            Select Case e.Column.FieldName
                Case "JOBID"
                    e.Editor.Enabled = False



                Case "LASTUPDATEDON"
                    e.Editor.Focus()
            End Select
        End If

        Select Case e.Column.FieldName
            Case "JOBID"


            Case "STATUS"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

        End Select
    End Sub

    Protected Sub Jobs_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles Jobs.RowInserting
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("INSERT INTO Jobs (JOBID, EMAIL, SUBJECT, BODY, FAIL, STATUS, CREATEDBY, CREATEDON, LASTUPDATEDBY, LASTUPDATEDON) VALUES (@:JOBID, @:EMAIL, @:SUBJECT, @:BODY, @:FAIL, @:STATUS, @:CREATEDBY, SYSDATE, @:LASTUPDATEDBY, SYSDATE)", "Jobs", "FrontOfficeConnectionString")

            .AddParameter("JOBID", DbType.AnsiString, 0, False, e.NewValues("JOBID"))
            .AddParameter("EMAIL", DbType.AnsiString, 0, (e.NewValues("EMAIL") = String.Empty), e.NewValues("EMAIL"))
            .AddParameter("SUBJECT", DbType.AnsiString, 0, (e.NewValues("SUBJECT") = String.Empty), e.NewValues("SUBJECT"))
            .AddParameter("BODY", DbType.AnsiString, 0, (e.NewValues("BODY") = String.Empty), e.NewValues("BODY"))
            .AddParameter("FAIL", DbType.AnsiString, 0, (e.NewValues("FAIL") = String.Empty), e.NewValues("FAIL"))
            .AddParameter("STATUS", DbType.Decimal, 0, (e.NewValues("STATUS") = 0), e.NewValues("STATUS"))
            .AddParameter("CREATEDBY", DbType.AnsiString, 0, (Session("nUsercode").ToString = String.Empty), Session("nUsercode").ToString)
            .AddParameter("LASTUPDATEDBY", DbType.AnsiString, 0, (Session("nUsercode").ToString = String.Empty), Session("nUsercode").ToString)

            .CommandExecute()
        End With

        e.Cancel = True
        Jobs.CancelEdit()
    End Sub

    Protected Sub Jobs_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles Jobs.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE Jobs SET EMAIL = @:EMAIL, SUBJECT = @:SUBJECT, BODY = @:BODY, FAIL = @:FAIL, STATUS = @:STATUS, LASTUPDATEDBY = @:LASTUPDATEDBY, LASTUPDATEDON = SYSDATE WHERE JOBID = @:JOBID", "Jobs", "FrontOfficeConnectionString")

            .AddParameter("EMAIL", DbType.AnsiString, 0, (e.NewValues("EMAIL") = String.Empty), e.NewValues("EMAIL"))
            .AddParameter("SUBJECT", DbType.AnsiString, 0, (e.NewValues("SUBJECT") = String.Empty), e.NewValues("SUBJECT"))
            .AddParameter("BODY", DbType.AnsiString, 0, (e.NewValues("BODY") = String.Empty), e.NewValues("BODY"))
            .AddParameter("FAIL", DbType.AnsiString, 0, (e.NewValues("FAIL") = String.Empty), e.NewValues("FAIL"))
            .AddParameter("STATUS", DbType.Decimal, 0, (e.NewValues("STATUS") = 0), e.NewValues("STATUS"))
            .AddParameter("LASTUPDATEDBY", DbType.AnsiString, 0, (Session("nUsercode").ToString = String.Empty), Session("nUsercode").ToString)
            .AddParameter("JOBID", DbType.AnsiString, 0, False, e.Keys("JOBID"))

            .CommandExecute()
        End With

        e.Cancel = True
        Jobs.CancelEdit()
    End Sub

    Protected Sub Jobs_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Jobs.CustomCallback
        Dim isNullResult As Boolean = True

        Select Case e.Parameters.ToString.ToLower
            Case "delete"
                Dim JOBIDKey As Generic.List(Of Object) = Jobs.GetSelectedFieldValues("JOBID")

                For index As Integer = 0 To JOBIDKey.Count - 1
                    With New DataManagerFactory("DELETE FROM Jobs WHERE JOBID = @:JOBID ", "Jobs", "FrontOfficeConnectionString")

                        .AddParameter("JOBID", DbType.AnsiString, 0, False, JOBIDKey(index))

                        .CommandExecute()
                    End With

                Next

                Jobs.DataBind()

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
                        fileName = String.Format(CultureInfo.InvariantCulture, "{0}.xlsx", "E-mail Trace")
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

    Protected Sub Jobs_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles Jobs.RowValidating


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