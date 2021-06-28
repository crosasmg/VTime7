#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System
Imports DevExpress.Web.Data
Imports InMotionGIT.Common.Helpers.Language
Imports System.IO
Imports DevExpress.Web.ASPxClasses
Imports InMotionGIT.Common.Proxy
Imports DevExpress.Web.ASPxMenu
Imports System.Data

#End Region

Partial Class Maintenance_UsersByGroupManager
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then

        End If

        Session("nUsercode") = 777
    End Sub

#End Region

#Region "Controls Events"

    Protected Sub MainMenu_ItemClick(source As Object, e As MenuItemEventArgs) Handles MainMenu.ItemClick

    End Sub

#End Region

#Region "UsersbyGroup_Grid Events"

    Protected Sub UsersbyGroup_Grid_DataBinding(sender As Object, e As EventArgs) Handles UsersbyGroup_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("UsersbyGroup_Grid")) Or _internalCall Then

            DirectCast(UsersbyGroup_Grid.Columns("USERID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = InMotionGIT.FrontOffice.Proxy.Helpers.UsersAndGrups.UsersAllByPage(String.Empty, False)

            With New DataManagerFactory("SELECT USERGROUPS.GROUPID, USERGROUPS.DESCRIPTION FROM USERGROUPS USERGROUPS",
                                        "USERGROUPS", "FrontOfficeConnectionString")

                DirectCast(UsersbyGroup_Grid.Columns("GROUPID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = .QueryExecuteToTable(True)
            End With

            With New DataManagerFactory("SELECT GROUPMEMBERS.USERID, GROUPMEMBERS.GROUPID FROM GROUPMEMBERS GROUPMEMBERS",
                                        "GROUPMEMBERS", "FrontOfficeConnectionString")

                UsersbyGroup_Grid.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub UsersbyGroup_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles UsersbyGroup_Grid.CellEditorInitialize
        If UsersbyGroup_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                Case "USERID"
                    e.Editor.Focus()

                Case Else
            End Select

        Else
            Select Case e.Column.FieldName
                Case "USERID"
                    e.Editor.Enabled = False

                Case "GROUPID"
                    e.Editor.Enabled = False

                Case ""
                    e.Editor.Focus()

                Case Else
            End Select
        End If

        Select Case e.Column.FieldName
            Case "USERID"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

            Case "GROUPID"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

            Case Else
        End Select
    End Sub

    Protected Sub UsersbyGroup_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles UsersbyGroup_Grid.RowInserting
        With New DataManagerFactory("INSERT INTO GROUPMEMBERS " &
                                    "(USERID, GROUPID, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) " &
                                    "VALUES (@:USERID, @:GROUPID, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)",
                                    "GROUPMEMBERS", "FrontOfficeConnectionString")

            .AddParameter("USERID", DbType.Decimal, 5, False, e.NewValues("USERID"))
            .AddParameter("GROUPID", DbType.Decimal, 5, False, e.NewValues("GROUPID"))

            .AddParameter("CREATORUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("nUsercode"))
            .AddParameter("UPDATEUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("nUsercode"))

            .CommandExecute()
        End With

        e.Cancel = True
        UsersbyGroup_Grid.CancelEdit()
    End Sub

    Protected Sub UsersbyGroup_Grid_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles UsersbyGroup_Grid.RowUpdating
        With New DataManagerFactory("UPDATE GROUPMEMBERS SET " &
                                    "UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE " &
                                    "WHERE USERID = @:USERID AND GROUPID = @:GROUPID", "USERGROUPS", "FrontOfficeConnectionString")

            .AddParameter("UPDATEUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("nUsercode"))

            .AddParameter("USERID", DbType.Decimal, 5, False, e.Keys("USERID"))
            .AddParameter("GROUPID", DbType.Decimal, 5, False, e.Keys("GROUPID"))

            .CommandExecute()
        End With

        e.Cancel = True
        UsersbyGroup_Grid.CancelEdit()
    End Sub

    Protected Sub UsersbyGroup_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles UsersbyGroup_Grid.CustomCallback
        Select Case e.Parameters.ToString.ToLower
            Case "delete"
                Dim USERIDKey As Generic.List(Of Object) = UsersbyGroup_Grid.GetSelectedFieldValues("USERID")
                Dim GROUPIDKey As Generic.List(Of Object) = UsersbyGroup_Grid.GetSelectedFieldValues("GROUPID")

                For index As Integer = 0 To USERIDKey.Count - 1
                    With New DataManagerFactory("DELETE FROM GROUPMEMBERS WHERE USERID = @:USERID AND GROUPID = @:GROUPID",
                                                "GROUPMEMBERS", "FrontOfficeConnectionString")

                        .AddParameter("USERID", DbType.Decimal, 5, False, USERIDKey(index))
                        .AddParameter("GROUPID", DbType.Decimal, 5, False, GROUPIDKey(index))

                        .CommandExecute()
                    End With
                Next

                UsersbyGroup_Grid.DataBind()

            Case Else
                If e.Parameters.ToString.ToLower.StartsWith("export") Then
                    Dim extension As String = e.Parameters.ToString.ToLower.Split("_")(1)
                    Dim filename As String = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", IO.Path.GetRandomFileName, extension)

                    ASPxGridViewExporter.GridViewID = sender.ClientInstanceName

                    Using fs As FileStream = New FileStream(String.Format(CultureInfo.InvariantCulture, "{0}\temp\{1}", Server.MapPath("/"), filename), FileMode.Create)
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

                    ASPxWebControl.RedirectOnCallback(String.Format(CultureInfo.InvariantCulture, "~/dropthings/download.ashx?Directory=temp&File={0}", filename))
                End If
        End Select
    End Sub

    Protected Sub UsersbyGroup_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles UsersbyGroup_Grid.RowValidating

        If e.Errors.Count = 0 AndAlso UsersbyGroup_Grid.IsNewRowEditing Then
            With New DataManagerFactory("SELECT COUNT(USERID) FROM GROUPMEMBERS WHERE USERID = @:USERID AND GROUPID = @:GROUPID",
                                        "GROUPMEMBERS", "FrontOfficeConnectionString")

                .AddParameter("USERID", DbType.Decimal, 5, False, e.NewValues("USERID"))
                .AddParameter("GROUPID", DbType.Decimal, 5, False, e.NewValues("GROUPID"))

                If .QueryExecuteScalarToInteger > 0 Then
                    e.Errors(UsersbyGroup_Grid.Columns("USERID")) = GetLocalResourceObject("UsersbyGroup_GridMessageErrorTableKeyDuplicate0Resource").ToString
                End If
            End With
        End If

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