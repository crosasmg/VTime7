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

Partial Class Maintenance_TabNetworkType
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

            TabNetworkType_Grid.DataBind()
        End If
    End Sub

#End Region

#Region "Controls Events"

#End Region

#Region "TabNetworkType_Grid Events"

    Protected Sub TabNetworkType_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabNetworkType_Grid.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub TabNetworkType_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabNetworkType_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabNetworkType_Grid")) Or _internalCall Then
            If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabNetworkType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))

                    source = .QueryExecuteToTable(True)
                    DirectCast(TabNetworkType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If

            With New DataManagerFactory("SELECT  TABNETWORKTYPE.NETWORKTYPE, TABNETWORKTYPE.RECORDSTATUS, TABNETWORKTYPE.DSTINDICATOR, TRANSNETWORKTYPE.NETWORKTYPE, TRANSNETWORKTYPE.LANGUAGEID, TRANSNETWORKTYPE.DESCRIPTION, TRANSNETWORKTYPE.SHORTDESCRIPTION FROM ADDRESS.TABNETWORKTYPE TABNETWORKTYPE JOIN ADDRESS.TRANSNETWORKTYPE TRANSNETWORKTYPE ON TRANSNETWORKTYPE.NETWORKTYPE = TABNETWORKTYPE.NETWORKTYPE  WHERE TRANSNETWORKTYPE.LANGUAGEID = @:LANGUAGEID", "TabNetworkType", "Linked.Address")

                .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))

                TabNetworkType_Grid.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub TabNetworkType_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabNetworkType_Grid.CellEditorInitialize
        If TabNetworkType_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName

                Case "NETWORKTYPE"
                    e.Editor.Focus()
            End Select

        Else

            Select Case e.Column.FieldName
                Case "NETWORKTYPE"
                    e.Editor.Enabled = False

                Case "RECORDSTATUS"
                    e.Editor.Focus()
            End Select
        End If

        Select Case e.Column.FieldName
            Case "NETWORKTYPE"

            Case "RECORDSTATUS"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

        End Select
    End Sub

    Protected Sub TabNetworkType_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabNetworkType_Grid.RowInserting
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("INSERT INTO ADDRESS.TabNetworkType (NETWORKTYPE, RECORDSTATUS, DSTINDICATOR, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:NETWORKTYPE, @:RECORDSTATUS, @:DSTINDICATOR, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabNetworkType", "Linked.Address")

            .AddParameter("NETWORKTYPE", DbType.Decimal, 0, False, e.NewValues("NETWORKTYPE"))
            .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
            .AddParameter("DSTINDICATOR", DbType.AnsiString, 0, (e.NewValues("DSTINDICATOR") = String.Empty), e.NewValues("DSTINDICATOR"))
            .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

            .CommandExecute()
        End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary

            With New DataManagerFactory("INSERT INTO ADDRESS.TransNetworkType (NETWORKTYPE, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:NETWORKTYPE, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TransNetworkType", "Linked.Address")

                .AddParameter("NETWORKTYPE", DbType.Decimal, 0, False, e.NewValues("NETWORKTYPE"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

        e.Cancel = True
        TabNetworkType_Grid.CancelEdit()
    End Sub

    Protected Sub TabNetworkType_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabNetworkType_Grid.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE ADDRESS.TabNetworkType SET RECORDSTATUS = @:RECORDSTATUS, DSTINDICATOR = @:DSTINDICATOR, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE NETWORKTYPE = @:NETWORKTYPE", "TabNetworkType", "Linked.Address")

            .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
            .AddParameter("DSTINDICATOR", DbType.AnsiString, 0, (e.NewValues("DSTINDICATOR") = String.Empty), e.NewValues("DSTINDICATOR"))
            .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            .AddParameter("NETWORKTYPE", DbType.Decimal, 0, False, e.Keys("NETWORKTYPE"))

            .CommandExecute()
        End With
        With New DataManagerFactory("UPDATE ADDRESS.TransNetworkType SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE NETWORKTYPE = @:NETWORKTYPE AND LANGUAGEID = @:LANGUAGEID", "TransNetworkType", "Linked.Address")

            .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
            .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
            .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            .AddParameter("NETWORKTYPE", DbType.Decimal, 0, False, e.Keys("NETWORKTYPE"))
            .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))

            .CommandExecute()
        End With

        e.Cancel = True
        TabNetworkType_Grid.CancelEdit()
    End Sub

    Protected Sub TabNetworkType_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabNetworkType_Grid.CustomCallback
        Dim isNullResult As Boolean = True

        Select Case e.Parameters.ToString.ToLower
            Case "delete"
                Dim NETWORKTYPEKey As Generic.List(Of Object) = TabNetworkType_Grid.GetSelectedFieldValues("NETWORKTYPE")

                For index As Integer = 0 To NETWORKTYPEKey.Count - 1
                    With New DataManagerFactory("DELETE FROM ADDRESS.TransNetworkType WHERE NETWORKTYPE = @:NETWORKTYPE ", "TransNetworkType", "Linked.Address")

                        .AddParameter("NETWORKTYPE", DbType.Decimal, 0, False, NETWORKTYPEKey(index))

                        .CommandExecute()
                    End With
                    With New DataManagerFactory("DELETE FROM ADDRESS.TabNetworkType WHERE NETWORKTYPE = @:NETWORKTYPE ", "TabNetworkType", "Linked.Address")

                        .AddParameter("NETWORKTYPE", DbType.Decimal, 0, False, NETWORKTYPEKey(index))

                        .CommandExecute()
                    End With

                Next

                TabNetworkType_Grid.DataBind()

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

    Protected Sub TabNetworkType_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabNetworkType_Grid.RowValidating

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

#Region "TransNetworkType Events"

    Protected Sub TransNetworkType_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransNetworkType.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub TransNetworkType_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransNetworkType.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransNetworkType")) Or _internalCall Then
            If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransNetworkType.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))

                    source = .QueryExecuteToTable(True)
                    DirectCast(TransNetworkType.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If
            If Caching.Exist("TabLanguage") Then
                DirectCast(TransNetworkType.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGECODEID = @:LANGUAGECODEID ", "TabLanguage", "Linked.Address")
                    .AddParameter("LANGUAGECODEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))

                    source = .QueryExecuteToTable(True)
                    DirectCast(TransNetworkType.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If

            With New DataManagerFactory("SELECT  TABNETWORKTYPE.NETWORKTYPE, TABNETWORKTYPE.RECORDSTATUS, TABNETWORKTYPE.DSTINDICATOR, TRANSNETWORKTYPE.NETWORKTYPE, TRANSNETWORKTYPE.LANGUAGEID, TRANSNETWORKTYPE.DESCRIPTION, TRANSNETWORKTYPE.SHORTDESCRIPTION FROM ADDRESS.TABNETWORKTYPE TABNETWORKTYPE JOIN ADDRESS.TRANSNETWORKTYPE TRANSNETWORKTYPE ON TRANSNETWORKTYPE.NETWORKTYPE = TABNETWORKTYPE.NETWORKTYPE  ", "TabNetworkType", "Linked.Address")

                TransNetworkType.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub TransNetworkType_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransNetworkType.CellEditorInitialize
        If TransNetworkType.IsNewRowEditing Then
            Select Case e.Column.FieldName

                Case "NETWORKTYPE"
                    e.Editor.Focus()
            End Select

        Else

            Select Case e.Column.FieldName
                Case "NETWORKTYPE"
                    e.Editor.Enabled = False
                Case "LANGUAGEID"
                    e.Editor.Enabled = False

                Case "RECORDSTATUS"
                    e.Editor.Focus()
            End Select
        End If

        Select Case e.Column.FieldName
            Case "NETWORKTYPE"

            Case "RECORDSTATUS"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()
            Case "LANGUAGEID"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

        End Select
    End Sub

    Protected Sub TransNetworkType_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransNetworkType.RowInserting
        Dim isNullResult As Boolean = True

        e.Cancel = True
        TransNetworkType.CancelEdit()
    End Sub

    Protected Sub TransNetworkType_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransNetworkType.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE ADDRESS.TransNetworkType SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE NETWORKTYPE = @:NETWORKTYPE AND LANGUAGEID = @:LANGUAGEID", "TransNetworkType", "Linked.Address")

            .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
            .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
            .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            .AddParameter("NETWORKTYPE", DbType.Decimal, 0, False, e.Keys("NETWORKTYPE"))
            .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))

            .CommandExecute()
        End With

        e.Cancel = True
        TransNetworkType.CancelEdit()
    End Sub

    Protected Sub TransNetworkType_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransNetworkType.CustomCallback
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

    Protected Sub TransNetworkType_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransNetworkType.RowValidating

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