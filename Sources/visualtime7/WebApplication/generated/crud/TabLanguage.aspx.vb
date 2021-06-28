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

Partial Class Maintenance_TabLanguage
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
            Dim newItem As DevExpress.Web.ASPxMenu.MenuItem

            For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary

                newItem = New DevExpress.Web.ASPxMenu.MenuItem

                With newItem
                    .Name = String.Format(CultureInfo.InvariantCulture, "{0}Item", languageItem.Value)
                    .Text = languageItem.Value
                    .Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Value.ToLower)

                   If languageItem.Key = LanguageId Then
                        MainMenu.Items(4).Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), languageItem.Value)
                        MainMenu.Items(4).Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Value.ToLower)

                        .Visible = False
                    Else
                        .Visible = True
                    End If
                End With

                MainMenu.Items(4).Items.Add(newItem)
            Next

        End If

        If Not CurrentState.Contains("LanguageId") Then
            CurrentState.Add("LanguageId", LanguageId)
        End If
    End Sub

#End Region

#Region "MainMenu Events"

    Protected Sub MainMenu_ItemClick(source As Object, e As DevExpress.Web.ASPxMenu.MenuItemEventArgs) Handles MainMenu.ItemClick
        If String.Equals(e.Item.Parent.Name, "LanguageItem", StringComparison.CurrentCultureIgnoreCase) Then
            e.Item.Parent.Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), e.Item.Text)
            e.Item.Parent.Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", e.Item.Text.ToLower)

            e.Item.Visible = False

            For Each item As DevExpress.Web.ASPxMenu.MenuItem In e.Item.Parent.Items
                If Not String.Equals(item.Text, e.Item.Text, StringComparison.CurrentCultureIgnoreCase) Then
                    item.Visible = True
                End If
            Next

            CurrentState.Set("LanguageId", InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext())
            _internalCall = True

            TabLanguage_Grid.DataBind()
        End If
    End Sub

#End Region

#Region "Controls Events"

#End Region

#Region "TabLanguage_Grid Events"

    Protected Sub TabLanguage_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabLanguage_Grid.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub TabLanguage_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabLanguage_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabLanguage_Grid")) Or _internalCall Then
            If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabLanguage_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))

                    source = .QueryExecuteToTable(True)
                    DirectCast(TabLanguage_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If

            With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.LANGUAGEID, TRANSLANGUAGE.DESCRIPTION, TRANSLANGUAGE.SHORTDESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TRANSLANGUAGE.LANGUAGEID = @:LANGUAGEID", "TabLanguage", "Linked.Common")

                .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))

                TabLanguage_Grid.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub TabLanguage_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabLanguage_Grid.CellEditorInitialize
        If TabLanguage_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName

                Case "LANGUAGEID"
                    e.Editor.Focus()
            End Select

        Else

            Select Case e.Column.FieldName
                Case "LANGUAGEID"
                    e.Editor.Enabled = False

                Case "RECORDSTATUS"
                    e.Editor.Focus()
            End Select
        End If

        Select Case e.Column.FieldName
            Case "LANGUAGEID"

            Case "RECORDSTATUS"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

        End Select
    End Sub

    Protected Sub TabLanguage_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabLanguage_Grid.RowInserting
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("INSERT INTO COMMON.TabLanguage (LANGUAGEID, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:LANGUAGEID, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabLanguage", "Linked.Common")

            .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.NewValues("LANGUAGEID"))
            .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
            .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, (Session("nUsercode") = 0), Session("nUsercode"))
            .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, (Session("nUsercode") = 0), Session("nUsercode"))

            .CommandExecute()
        End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary

            With New DataManagerFactory("INSERT INTO COMMON.TransLanguage (LANGUAGECODEID, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:LANGUAGECODEID, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TransLanguage", "Linked.Common")

                .AddParameter("LANGUAGECODEID", DbType.Decimal, 0, False, e.NewValues("LANGUAGEID"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

        e.Cancel = True
        TabLanguage_Grid.CancelEdit()
    End Sub

    Protected Sub TabLanguage_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabLanguage_Grid.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE COMMON.TabLanguage SET RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE LANGUAGEID = @:LANGUAGEID", "TabLanguage", "Linked.Common")

            .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
            .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, (Session("nUsercode") = 0), Session("nUsercode"))
            .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))

            .CommandExecute()
        End With
        With New DataManagerFactory("UPDATE COMMON.TransLanguage SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE LANGUAGECODEID = @:LANGUAGECODEID AND LANGUAGEID = @:LANGUAGEID", "TransLanguage", "Linked.Common")

            .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
            .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
            .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            .AddParameter("LANGUAGECODEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))

            .CommandExecute()
        End With

        e.Cancel = True
        TabLanguage_Grid.CancelEdit()
    End Sub

    Protected Sub TabLanguage_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabLanguage_Grid.CustomCallback
        Dim isNullResult As Boolean = True

        Select Case e.Parameters.ToString.ToLower
            Case "delete"
                Dim LANGUAGEIDKey As Generic.List(Of Object) = TabLanguage_Grid.GetSelectedFieldValues("LANGUAGEID")

                For index As Integer = 0 To LANGUAGEIDKey.Count - 1
                    With New DataManagerFactory("DELETE FROM COMMON.TransLanguage WHERE LANGUAGECODEID = @:LANGUAGECODEID ", "TransLanguage", "Linked.Common")

                        .AddParameter("LANGUAGECODEID", DbType.Decimal, 0, False, LANGUAGEIDKey(index))

                        .CommandExecute()
                    End With
                    With New DataManagerFactory("DELETE FROM COMMON.TabLanguage WHERE LANGUAGEID = @:LANGUAGEID ", "TabLanguage", "Linked.Common")

                        .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, LANGUAGEIDKey(index))

                        .CommandExecute()
                    End With

                Next

                TabLanguage_Grid.DataBind()

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

                End If
        End Select
    End Sub

    Protected Sub TabLanguage_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabLanguage_Grid.RowValidating

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

#Region "TransLanguage Events"

    Protected Sub TransLanguage_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransLanguage.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub TransLanguage_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransLanguage.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransLanguage")) Or _internalCall Then
            If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransLanguage.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))

                    source = .QueryExecuteToTable(True)
                    DirectCast(TransLanguage.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If

            With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.LANGUAGEID, TRANSLANGUAGE.DESCRIPTION, TRANSLANGUAGE.SHORTDESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  ", "TabLanguage", "Linked.Common")

                TransLanguage.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub TransLanguage_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransLanguage.CellEditorInitialize
        If TransLanguage.IsNewRowEditing Then
            Select Case e.Column.FieldName

                Case "LANGUAGEID"
                    e.Editor.Focus()
            End Select

        Else

            Select Case e.Column.FieldName
                Case "LANGUAGEID"
                    e.Editor.Enabled = False
                Case "LANGUAGECODEID"
                    e.Editor.Enabled = False

                Case "RECORDSTATUS"
                    e.Editor.Focus()
            End Select
        End If

        Select Case e.Column.FieldName
            Case "LANGUAGEID"

            Case "RECORDSTATUS"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

        End Select
    End Sub

    Protected Sub TransLanguage_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransLanguage.RowInserting
        Dim isNullResult As Boolean = True

        e.Cancel = True
        TransLanguage.CancelEdit()
    End Sub

    Protected Sub TransLanguage_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransLanguage.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE COMMON.TransLanguage SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE LANGUAGECODEID = @:LANGUAGECODEID AND LANGUAGEID = @:LANGUAGEID", "TransLanguage", "Linked.Common")

            .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
            .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
            .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            .AddParameter("LANGUAGECODEID", DbType.Decimal, 0, False, e.Keys("LANGUAGECODEID"))
            .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))

            .CommandExecute()
        End With

        e.Cancel = True
        TransLanguage.CancelEdit()
    End Sub

    Protected Sub TransLanguage_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransLanguage.CustomCallback
        Dim isNullResult As Boolean = True

        Select Case e.Parameters.ToString.ToLower
            Case "delete"

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

                End If
        End Select
    End Sub

    Protected Sub TransLanguage_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransLanguage.RowValidating

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