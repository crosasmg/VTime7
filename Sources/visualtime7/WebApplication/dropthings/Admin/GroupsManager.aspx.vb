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
Imports DevExpress.Web.ASPxMenu
Imports InMotionGIT.Common.Proxy
Imports System.Data

#End Region

Partial Class Maintenance_GroupsManager
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

#Region "Groups_Grid Events"

    Protected Sub Groups_Grid_DataBinding(sender As Object, e As EventArgs) Handles Groups_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("Groups_Grid")) Or _internalCall Then
            Dim userMemberListAllowScheduler As List(Of InMotionGIT.Membership.Providers.FrontOfficeMembershipUser) = Nothing
            Dim userList As New DataTable

            With userList
                .Columns.Add("NUSERCODE", GetType(String))
                .Columns.Add("SFIRSTNAME", GetType(String))
                .Columns.Add("SLASTNAME", GetType(String))
            End With

            Dim userMemberList As MembershipUserCollection = System.Web.Security.Membership.GetAllUsers

            If userMemberList.IsNotEmpty AndAlso userMemberList.Count <> 0 Then
                userMemberListAllowScheduler = (From intemUser As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser In userMemberList
                                         Where intemUser.AllowScheduler = True
                                         Select intemUser).ToList
            End If
     

            If userMemberListAllowScheduler.IsNotEmpty AndAlso userMemberListAllowScheduler.Count <> 0 Then
                For Each itemUser As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser In userMemberListAllowScheduler
                    userList.Rows.Add(itemUser.ProviderUserKey.ToString.Trim, itemUser.FirstName.ToString.Trim, itemUser.LastName.ToString.Trim)
                Next
                DirectCast(Groups_Grid.Columns("OWNERID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = userList
            End If

            With New DataManagerFactory("SELECT USERGROUPS.GROUPID, USERGROUPS.OWNERID, USERGROUPS.DESCRIPTION FROM USERGROUPS USERGROUPS",
                                        "USERGROUPS", "FrontOfficeConnectionString")

                Groups_Grid.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub Groups_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles Groups_Grid.CellEditorInitialize
        If Groups_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                Case "GROUPID"
                    e.Editor.Focus()

                Case Else
            End Select

        Else
            Select Case e.Column.FieldName
                Case "GROUPID"
                    e.Editor.Enabled = False

                Case "OWNERID"
                    e.Editor.Focus()

                Case Else
            End Select
        End If

        Select Case e.Column.FieldName
            Case "GROUPID"

            Case "OWNERID"
                DirectCast(e.Editor, ASPxComboBox).DataBindItems()

            Case Else
        End Select
    End Sub

    Protected Sub Groups_Grid_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles Groups_Grid.RowInserting
        With New DataManagerFactory("INSERT INTO USERGROUPS " &
                                    "(GROUPID, OWNERID, DESCRIPTION, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) " &
                                    "VALUES (@:GROUPID, @:OWNERID, @:DESCRIPTION, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)",
                                    "USERGROUPS", "FrontOfficeConnectionString")

            .AddParameter("GROUPID", DbType.Decimal, 5, False, e.NewValues("GROUPID"))
            .AddParameter("OWNERID", DbType.Decimal, 5, False, e.NewValues("OWNERID"))
            .AddParameter("DESCRIPTION", DbType.AnsiString, 40, False, e.NewValues("DESCRIPTION"))
            .AddParameter("CREATORUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("nUsercode"))
            .AddParameter("UPDATEUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("nUsercode"))

            .CommandExecute()
        End With

        e.Cancel = True
        Groups_Grid.CancelEdit()
    End Sub

    Protected Sub Groups_Grid_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles Groups_Grid.RowUpdating
        With New DataManagerFactory("UPDATE USERGROUPS SET " &
                                    "OWNERID = @:OWNERID, DESCRIPTION = @:DESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE " &
                                    "WHERE GROUPID = @:GROUPID", "USERGROUPS", "FrontOfficeConnectionString")

            .AddParameter("OWNERID", DbType.Decimal, 5, False, e.NewValues("OWNERID"))
            .AddParameter("DESCRIPTION", DbType.AnsiString, 40, False, e.NewValues("DESCRIPTION"))

            .AddParameter("UPDATEUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("nUsercode"))

            .AddParameter("GROUPID", DbType.Decimal, 5, False, e.Keys("GROUPID"))

            .CommandExecute()
        End With

        e.Cancel = True
        Groups_Grid.CancelEdit()
    End Sub

    Protected Sub Groups_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Groups_Grid.CustomCallback
        Select Case e.Parameters.ToString.ToLower

            Case "delete"
                Dim GROUPIDKey As Generic.List(Of Object) = Groups_Grid.GetSelectedFieldValues("GROUPID")

                For index As Integer = 0 To GROUPIDKey.Count - 1
                    With New DataManagerFactory("DELETE FROM USERGROUPS WHERE GROUPID = @:GROUPID", "USERGROUPS", "FrontOfficeConnectionString")

                        .AddParameter("GROUPID", DbType.Decimal, 5, False, GROUPIDKey(index))

                        .CommandExecute()
                    End With
                Next

                Groups_Grid.DataBind()

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

    Protected Sub Groups_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles Groups_Grid.RowValidating
        If e.Errors.Count = 0 AndAlso Groups_Grid.IsNewRowEditing Then
            With New DataManagerFactory("SELECT COUNT(GROUPID) FROM USERGROUPS WHERE GROUPID = @:GROUPID", "USERGROUPS", "FrontOfficeConnectionString")

                .AddParameter("GROUPID", DbType.Decimal, 5, False, e.NewValues("GROUPID"))

                If .QueryExecuteScalarToInteger > 0 Then
                    e.Errors(Groups_Grid.Columns("GROUPID")) = GetLocalResourceObject("Groups_GridMessageErrorTableKeyDuplicate0Resource").ToString
                End If
            End With
        End If

        If e.Errors.Count > 0 Then
            Dim errorMessage As String = "<ol style='font-weight:lighter'>"

            For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                errorMessage += String.Format("<li>{0}</li>", item.Value)
            Next

            errorMessage += String.Format(CultureInfo.InvariantCulture,
                                          "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("MessageErrorText").ToString)
            e.RowError = errorMessage
        End If
    End Sub

#End Region

End Class