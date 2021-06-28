#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System
Imports DevExpress.Web.ASPxMenu
Imports InMotionGIT.Common.Proxy
Imports DevExpress.Web.Data
Imports System.Data

#End Region

Partial Class Prototype_CRUD
    Inherits PageBase

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'DevExpress.Web.ASPxClasses.ASPxWebControl.RegisterBaseScript(Me)
        If Not IsCallback And Not IsPostBack Then
            GridViewDirectory.DataBind()
            Session("nUsercode") = 6329
        End If
    End Sub

#End Region

#Region "Controls Events"

    Protected Sub MainMenu_ItemClick(source As Object, e As MenuItemEventArgs) Handles MainMenu.ItemClick
        Select Case e.Item.Name
            Case "AddRegisterItem"
                GridViewDirectory.AddNewRow()

            Case "EditRegisterItem"
                GridViewDirectory.StartEdit(GridViewDirectory.VisibleStartIndex)

            Case "RemoveRegisterItem"
                popupDelete.ShowOnPageLoad = True

            Case "StandardItem"
                e.Item.Parent.Text = "View: Standard"
                MainMenu.Items(0).Enabled = True
                MainMenu.Items(1).Enabled = True

                With GridViewDirectory
                    .DataBind()
                    .Visible = True
                End With

            Case Else
        End Select
    End Sub

#End Region

#Region "GridViewDirectory Events"
    Protected Sub GridViewDirectory_DataBinding(sender As Object, e As EventArgs) Handles GridViewDirectory.DataBinding
        With New DataManagerFactory("SELECT " & _
                                    "       USERID, USERINDICATOR, RELATIONSHIPUSERID, " & _
                                    "       RELATIONSHIPTYPE, ALLOWQUERY, ALLOWCREATE, " & _
                                    "       ALLOWCANCEL, ALLOWCOMPLETED, ALLOWREASSIGN " &
                                    " FROM  " & _
                                    "       FRONTOFFICE.USERRELATIONSHIP",
                                    "USERRELATIONSHIP", "FrontOfficeConnectionString")

            GridViewDirectory.DataSource = .QueryExecuteToTable(True)
        End With

        DirectCast(GridViewDirectory.Columns("RELATIONSHIPUSERID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = InMotionGIT.FrontOffice.Proxy.Helpers.UsersAndGrups.UsersAllByPage(String.Empty, False)
        DirectCast(GridViewDirectory.Columns("USERID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = InMotionGIT.FrontOffice.Proxy.Helpers.UsersAndGrups.UsersAllByPage(String.Empty, False)

    End Sub

    Protected Sub GridViewDirectory_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles GridViewDirectory.CellEditorInitialize
        If GridViewDirectory.IsNewRowEditing Then
            Select Case e.Column.FieldName
                Case "RELATIONSHIPTYPE"
                    With DirectCast(e.Editor, ASPxComboBox)
                        If .SelectedIndex = -1 Then
                            .SelectedIndex = 0
                        End If
                    End With

                Case "RELATIONSHIPUSERID"
                    e.Editor.Focus()
                    With DirectCast(e.Editor, ASPxComboBox)
                        .DataSource = InMotionGIT.FrontOffice.Proxy.Helpers.UsersAndGrups.UsersAllByPage(String.Empty, False)
                        .DataBindItems()
                    End With

                Case "USERID"
                    With DirectCast(e.Editor, ASPxComboBox)
                        .DataSource = InMotionGIT.FrontOffice.Proxy.Helpers.UsersAndGrups.UsersAllByPage(String.Empty, False)
                        .DataBindItems()
                    End With

                Case "ALLOWQUERY"
                    With DirectCast(e.Editor, ASPxCheckBox)
                        .Checked = True
                    End With

                Case Else
            End Select

        Else
            Select Case e.Column.FieldName
                Case "USERID", "USERINDICATOR", "RELATIONSHIPUSERID"
                    e.Editor.Enabled = False

                Case "RELATIONSHIPTYPE"
                    e.Editor.Focus()

                Case Else
            End Select
        End If
    End Sub

    Protected Sub GridViewDirectory_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles GridViewDirectory.RowValidating
        If GridViewDirectory.IsNewRowEditing AndAlso e.NewValues("USERID") = e.NewValues("RELATIONSHIPUSERID") Then

            e.Errors(GridViewDirectory.Columns("USERID")) = "No se puede relacionar consigo mismo"
        End If

        If e.NewValues("ALLOWQUERY") = False AndAlso e.NewValues("ALLOWCREATE") = False AndAlso
           e.NewValues("ALLOWCANCEL") = False AndAlso e.NewValues("ALLOWCOMPLETED") = False AndAlso
           e.NewValues("ALLOWREASSIGN") = False Then

            e.Errors(GridViewDirectory.Columns("ALLOWREASSIGN")) = "Debe permitir por lo menos una opción"
        End If

        If GridViewDirectory.IsNewRowEditing Then
            If RecordExist(e.NewValues("USERID"), e.NewValues("USERINDICATOR"), e.NewValues("RELATIONSHIPUSERID")) Then
                e.Errors(GridViewDirectory.Columns("USERID")) = "Esta relación ya existe"
            End If
        End If

        If e.Errors.Count > 0 Then
            Dim errorMessage As String = "<ol style='font-weight:lighter'>"

            For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                errorMessage += String.Format(CultureInfo.InvariantCulture, "<li>{0}</li>", item.Value)
            Next

            errorMessage += "</ol><ul style='font-weight:bold'>Por favor, debe corregir todos los errores.</ul>"

            e.RowError = errorMessage
        End If
    End Sub

    Protected Sub GridViewDirectory_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles GridViewDirectory.RowInserting
        With New DataManagerFactory(" INSERT INTO " & _
                                    "             FRONTOFFICE.USERRELATIONSHIP " &
                                    "             (USERID, USERINDICATOR, RELATIONSHIPUSERID, " & _
                                    "              RELATIONSHIPTYPE, ALLOWQUERY, ALLOWCREATE, " & _
                                    "              ALLOWCANCEL, ALLOWCOMPLETED, ALLOWREASSIGN, " & _
                                    "              CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) " &
                                    " VALUES " & _
                                    "            (@:USERID, @:USERINDICATOR, @:RELATIONSHIPUSERID, " & _
                                    "             @:RELATIONSHIPTYPE, @:ALLOWQUERY, @:ALLOWCREATE, " & _
                                    "             @:ALLOWCANCEL, @:ALLOWCOMPLETED, @:ALLOWREASSIGN, " & _
                                    "             @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)",
                                    "USERRELATIONSHIP",
                                    "FrontOfficeConnectionString")

            .AddParameter("USERID", DbType.Decimal, 5, False, e.NewValues("USERID"))
            .AddParameter("USERINDICATOR", DbType.Decimal, 1, False, IIf(e.NewValues("USERINDICATOR"), 1, 0))
            .AddParameter("RELATIONSHIPUSERID", DbType.Decimal, 5, False, e.NewValues("RELATIONSHIPUSERID"))
            .AddParameter("RELATIONSHIPTYPE", DbType.Decimal, 5, False, e.NewValues("RELATIONSHIPTYPE"))
            .AddParameter("ALLOWQUERY", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWQUERY"), 1, 0))
            .AddParameter("ALLOWCREATE", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWCREATE"), 1, 0))
            .AddParameter("ALLOWCANCEL", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWCANCEL"), 1, 0))
            .AddParameter("ALLOWCOMPLETED", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWCOMPLETED"), 1, 0))
            .AddParameter("ALLOWREASSIGN", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWREASSIGN"), 1, 0))

            .AddParameter("CREATORUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("NUSERCODE"))
            .AddParameter("UPDATEUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("NUSERCODE"))

            .CommandExecute()
        End With

        e.Cancel = True
        GridViewDirectory.CancelEdit()
        deleteCache()

    End Sub

    Private Sub deleteCache()
        Dim key As String = String.Format("UserAllowScheduler_{0}", UserInfo.UserName)
        InMotionGIT.Common.Helpers.Caching.Remove(key)
    End Sub

    Protected Sub GridViewDirectory_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles GridViewDirectory.RowUpdating
        With New DataManagerFactory("UPDATE " & _
                                    "       FRONTOFFICE.USERRELATIONSHIP " & _
                                    "   SET " &
                                    "       RELATIONSHIPTYPE = @:RELATIONSHIPTYPE, ALLOWQUERY = @:ALLOWQUERY, ALLOWCREATE = @:ALLOWCREATE, " & _
                                    "       ALLOWCANCEL = @:ALLOWCANCEL, ALLOWCOMPLETED = @:ALLOWCOMPLETED, ALLOWREASSIGN = @:ALLOWREASSIGN, " & _
                                    "       UPDATEUSERCODE = @:UPDATEUSERCODE, UPDATEDATE = SYSDATE " &
                                    " WHERE " & _
                                    "       USERID = @:USERID AND USERINDICATOR= @:USERINDICATOR  AND RELATIONSHIPUSERID = @:RELATIONSHIPUSERID",
                                    "USERRELATIONSHIP",
                                    "FrontOfficeConnectionString")

            .AddParameter("RELATIONSHIPTYPE", DbType.Decimal, 5, False, e.NewValues("RELATIONSHIPTYPE"))
            .AddParameter("ALLOWQUERY", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWQUERY"), 1, 0))
            .AddParameter("ALLOWCREATE", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWCREATE"), 1, 0))
            .AddParameter("ALLOWCANCEL", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWCANCEL"), 1, 0))
            .AddParameter("ALLOWCOMPLETED", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWCOMPLETED"), 1, 0))
            .AddParameter("ALLOWREASSIGN", DbType.Decimal, 1, False, IIf(e.NewValues("ALLOWREASSIGN"), 1, 0))

            .AddParameter("UPDATEUSERCODE", DbType.AnsiStringFixedLength, 20, False, Session("NUSERCODE"))

            .AddParameter("USERID", DbType.Decimal, 5, False, e.Keys("USERID"))
            .AddParameter("USERINDICATOR", DbType.Decimal, 1, False, IIf(e.Keys("USERINDICATOR"), 1, 0))
            .AddParameter("RELATIONSHIPUSERID", DbType.Decimal, 5, False, e.Keys("RELATIONSHIPUSERID"))

            .CommandExecute()
        End With

        e.Cancel = True
        GridViewDirectory.CancelEdit()
        deleteCache()
    End Sub

    Protected Sub GridViewDirectory_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridViewDirectory.CustomCallback
        Select Case e.Parameters.ToString.ToLower

            Case "delete"
                Dim KeyUserId As Generic.List(Of Object) = GridViewDirectory.GetSelectedFieldValues("USERID")
                Dim KeyUserIndicator As Generic.List(Of Object) = GridViewDirectory.GetSelectedFieldValues("USERINDICATOR")
                Dim KeyRelationshipUserId As Generic.List(Of Object) = GridViewDirectory.GetSelectedFieldValues("RELATIONSHIPUSERID")

                For index As Integer = 0 To KeyUserId.Count - 1

                    With New DataManagerFactory("DELETE FROM " & _
                                                "            FRONTOFFICE.USERRELATIONSHIP " & _
                                                "      WHERE " & _
                                                "            USERID = @:USERID AND USERINDICATOR = @:USERINDICATOR AND " &
                                                "            RELATIONSHIPUSERID = @:RELATIONSHIPUSERID",
                                                "USERRELATIONSHIP",
                                                "FrontOfficeConnectionString")

                        .AddParameter("USERID", DbType.Decimal, 5, False, KeyUserId(index))
                        .AddParameter("USERINDICATOR", DbType.Decimal, 1, False, IIf(KeyUserIndicator(index), 1, 0))
                        .AddParameter("RELATIONSHIPUSERID", DbType.Decimal, 5, False, KeyRelationshipUserId(index))

                        .CommandExecute()
                    End With
                Next

                GridViewDirectory.DataBind()

                deleteCache()

        End Select
    End Sub

#End Region

#Region "Bussines Logic"

    Private Function RecordExist(userId As Integer, userIndicator As Boolean, relationshipUserId As Integer) As Boolean
        Dim recordCount As Integer = 0

        With New DataManagerFactory("SELECT " & _
                                    "       COUNT(USERID) " & _
                                    "  FROM " & _
                                    "       FRONTOFFICE.USERRELATIONSHIP " &
                                    " WHERE " & _
                                    "       USERID = @:USERID AND USERINDICATOR = @:USERINDICATOR AND RELATIONSHIPUSERID = @:RELATIONSHIPUSERID",
                                    "USERRELATIONSHIP",
                                    "FrontOfficeConnectionString")

            .AddParameter("USERID", DbType.Decimal, 5, False, userId)
            .AddParameter("USERINDICATOR", DbType.Decimal, 1, False, IIf(userIndicator, 1, 0))
            .AddParameter("RELATIONSHIPUSERID", DbType.Decimal, 5, False, relationshipUserId)

            recordCount = .QueryExecuteScalarToInteger
        End With
        If recordCount = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

    'Protected Sub GridViewDirectory_HtmlRowCreated(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridViewDirectory.HtmlRowCreated
    '    e.Row.Attributes.Add("onkeydown", String.Format("OnEnterDown(event,{0})", e.VisibleIndex))
    'End Sub

    'SQLServer:
    '  select nUsercode as code, rtrim(sFirstname) + ' ' + rtrim(sLastName) as description from insudb.gcv_Users where ISNULL(sFirstname, '') <> '' and isnull(sLastName, '') <> '' order by description
    'Oracle:
    ' select nUsercode as code, TRIM(sFirstname) || ' ' ||  TRIM(sLastName) as description from insudb.gcv_Users where NOT sFirstname IS NULL AND NOT sLastName IS NULL order by description

End Class