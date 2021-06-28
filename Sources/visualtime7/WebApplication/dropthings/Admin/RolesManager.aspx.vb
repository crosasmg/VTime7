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
Imports System.Data
Imports System.Data.Common
Imports DevExpress.Web.ASPxUploadControl
Imports InMotionGIT.Common.Proxy

#End Region

Partial Class Maintenance_RolesManager
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
        Dim result As New Integration.Contracts.Model("Role")

        With result
            .ConnectionStringName = "FrontOfficeConnectionString"

            If Not IsNothing(GetLocalResourceObject("PageResource.Title")) Then
                .Title = GetLocalResourceObject("PageResource.Title").ToString
            Else
                .Title = "Administracion de Roles"
            End If

            .Sequence.TableName = "Role"
            .Sequence.ColumnName = "RoleId"
            With .AddColumn(1, "RoleId", DbType.Decimal, 9)
                If Not IsNothing(GetLocalResourceObject("RoleId.Caption")) Then
                    .Title = GetLocalResourceObject("RoleId.Caption").ToString
                Else
                    .Title = "ID Rol"
                End If

                If Not IsNothing(GetLocalResourceObject("RoleId.ToolTip")) Then
                    .Comment = GetLocalResourceObject("RoleId.ToolTip").ToString
                Else
                    .Comment = "Identificación Rol"
                End If

                .Scale = 0
                .Sequence = True

            End With
            With .AddColumn(2, "roleName", DbType.AnsiString, 255)
                If Not IsNothing(GetLocalResourceObject("roleName.Caption")) Then
                    .Title = GetLocalResourceObject("roleName.Caption").ToString
                Else
                    .Title = "Nombre Rol"
                End If

                If Not IsNothing(GetLocalResourceObject("roleName.ToolTip")) Then
                    .Comment = GetLocalResourceObject("roleName.ToolTip").ToString
                Else
                    .Comment = "Nombre del rol"
                End If

                .Scale = 0

            End With
            With .AddColumn(3, "SecurityLevel", DbType.Decimal, 5)
                If Not IsNothing(GetLocalResourceObject("SecurityLevel.Caption")) Then
                    .Title = GetLocalResourceObject("SecurityLevel.Caption").ToString
                Else
                    .Title = "Nivel Seguridad"

                End If

                If Not IsNothing(GetLocalResourceObject("SecurityLevel.ToolTip")) Then
                    .Comment = GetLocalResourceObject("SecurityLevel.ToolTip").ToString
                Else
                    .Comment = "Nivel de Seguridad"
                End If
                .Value = 9
                .Scale = 0

            End With
            With .AddColumn(4, "IsBackOfficeSource", DbType.Decimal, 1)
                If Not IsNothing(GetLocalResourceObject("IsBackOfficeSource.Caption")) Then
                    .Title = GetLocalResourceObject("IsBackOfficeSource.Caption").ToString
                Else
                    .Title = "Rol de Backoffice"
                End If

                If Not IsNothing(GetLocalResourceObject("IsBackOfficeSource.ToolTip")) Then
                    .Comment = GetLocalResourceObject("IsBackOfficeSource.ToolTip").ToString
                Else
                    .Comment = "Es Rol de Backoffice"
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

#Region "Role Events"

    Protected Sub Role_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles Role.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow

        Select Case e.Column.FieldName

            Case Else
        End Select
    End Sub

    Protected Sub Role_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles Role.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("Role")) Or _internalCall Then

            With New DataManagerFactory("SELECT ROLE.ROLEID, ROLE.ROLENAME, NVL(ROLE.ISBACKOFFICESOURCE ,0) ISBACKOFFICESOURCE , NVL(ROLE.SECURITYLEVEL, 9) SECURITYLEVEL " &
                                         " FROM FRONTOFFICE.ROLE ROLE " &
                                      "ORDER BY ROLE.ROLENAME ", "Role", "FrontOfficeConnectionString")

                Role.DataSource = .QueryExecuteToTable(True)
            End With
        End If
    End Sub

    Protected Sub Role_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles Role.CellEditorInitialize
        If Role.IsNewRowEditing Then
            Select Case e.Column.FieldName
                Case "ROLEID"
                    e.Editor.Focus()
            End Select
        Else
            Select Case e.Column.FieldName
                Case "ROLEID"
                    e.Editor.Enabled = False

                Case "ROLENAME"
                    e.Editor.Focus()

            End Select
        End If

        Select Case e.Column.FieldName
            Case "ROLEID"

        End Select

    End Sub

    Protected Sub Role_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles Role.InitNewRow
        e.NewValues("SecurityLevel") = 9
        Dim securityLevel As ASPxTrackBar = CType(Role.FindEditRowCellTemplateControl(CType(Role.Columns(3), GridViewDataColumn), "tkbrSecurityLevel"), ASPxTrackBar)
        securityLevel.Value = 9
    End Sub

    Protected Sub Role_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles Role.RowInserting
        Dim isNullResult As Boolean = True
        Dim roleId As Integer = 0
        With New DataManagerFactory(" SELECT NVL(MAX(ROLEID) , 0) + 1 " &
                                    " FROM ROLE", "ROLE", "FrontOfficeConnectionString")
            roleId = .QueryExecuteScalarToInteger()
        End With

        Dim securityLevel As ASPxTrackBar = CType(Role.FindEditRowCellTemplateControl(CType(Role.Columns(3), GridViewDataColumn), "tkbrSecurityLevel"), ASPxTrackBar)
        Dim tempSecurity As Integer = securityLevel.Value

        With New DataManagerFactory("INSERT INTO " &
                                          " FRONTOFFICE.Role (ROLEID, ROLENAME, ISBACKOFFICESOURCE , SECURITYLEVEL ) " &
                                          " VALUES (@:ROLEID, @:ROLENAME, @:ISBACKOFFICESOURCE , @:SECURITYLEVEL)", "Role", "FrontOfficeConnectionString")

            .AddParameter("ROLEID", DbType.Decimal, 0, False, roleId)
            .AddParameter("ROLENAME", DbType.AnsiString, 0, False, e.NewValues("ROLENAME"))
            .AddParameter("ISBACKOFFICESOURCE", DbType.Decimal, 0, False, 0)
            .AddParameter("SECURITYLEVEL", DbType.AnsiString, 0, False, tempSecurity)
            .CommandExecute()
        End With

        e.Cancel = True
        Role.CancelEdit()
    End Sub

    Protected Sub Role_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles Role.RowUpdating
        Dim isNullResult As Boolean = True

        With New DataManagerFactory("UPDATE FRONTOFFICE.Role SET ROLENAME = @:ROLENAME, ISBACKOFFICESOURCE = @:ISBACKOFFICESOURCE , SECURITYLEVEL =@:SECURITYLEVEL  WHERE ROLEID = @:ROLEID", "Role", "FrontOfficeConnectionString")
            Dim securityLevel As ASPxTrackBar = CType(Role.FindEditRowCellTemplateControl(CType(Role.Columns(3), GridViewDataColumn), "tkbrSecurityLevel"), ASPxTrackBar)
            Dim tempSecurity As Integer = securityLevel.Value
            .AddParameter("ROLENAME", DbType.AnsiString, 0, False, e.NewValues("ROLENAME"))
            .AddParameter("ISBACKOFFICESOURCE", DbType.Decimal, 0, (IIf(IsNothing(e.NewValues("ISBACKOFFICESOURCE")), 0, e.NewValues("ISBACKOFFICESOURCE")) = 0), IIf(IsNothing(e.NewValues("ISBACKOFFICESOURCE")), 0, e.NewValues("ISBACKOFFICESOURCE")))
            .AddParameter("SECURITYLEVEL", DbType.AnsiString, 0, False, tempSecurity)
            .AddParameter("ROLEID", DbType.Decimal, 0, False, e.Keys("ROLEID"))

            .CommandExecute()
        End With

        e.Cancel = True
        Role.CancelEdit()
    End Sub

    Protected Sub Role_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Role.CustomCallback
        Dim isNullResult As Boolean = True

        Select Case e.Parameters.ToString.ToLower
            Case "delete"
                Dim message As StringBuilder
                Dim roleIdKeys As Generic.List(Of Object) = Role.GetSelectedFieldValues("ROLEID")
                For index As Integer = 0 To roleIdKeys.Count - 1
                        Dim exist As Integer
                        With New DataManagerFactory(String.Format(" SELECT " +
                                                                  " 	COUNT (USERMEMBERROLE.USERID) " +
                                                                  " FROM " +
                                                                  " 	USERMEMBERROLE " +
                                                                  " INNER JOIN ROLE ON USERMEMBERROLE.ROLEID = ROLE .ROLEID " +
                                                                  " WHERE " +
                                                                  " 	ROLE.ROLEID = {0} ", roleIdKeys(index)),
                                                                  "USERMEMBERROLE", "FrontOfficeConnectionString")
                            exist = .QueryExecuteScalarToInteger
                        End With
                    If exist = 0 Then
                        With New DataManagerFactory("DELETE FROM FRONTOFFICE.Role WHERE ROLEID = @:ROLEID ", "Role", "FrontOfficeConnectionString")
                            .AddParameter("ROLEID", DbType.Decimal, 0, False, roleIdKeys(index))
                            .CommandExecute()
                        End With
                    Else
                        If message.IsEmpty Then
                                message = New StringBuilder
                            End If
                            Dim roleName As String = String.Empty

                            With New DataManagerFactory(String.Format("SELECT ROLENAME FROM FRONTOFFICE.ROLE WHERE ROLEID = {0} ", roleIdKeys(index)), "Role", "FrontOfficeConnectionString")
                                roleName = .QueryExecuteScalarToString()
                            End With

                            message.AppendLine(String.Format(GetLocalResourceObject("RoleExistInUserMember").ToString, roleName))
                        End If
                    Next

                If message.IsNotEmpty Then
                    Role.JSProperties.Add("cp_error", "true")
                    Role.JSProperties.Add("cp_error_Message", message.ToString)
                End If


                Role.DataBind()

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
                        fileName = String.Format(CultureInfo.InvariantCulture, "{0}.xlsx", "Administracion de Roles")
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

    Protected Sub Role_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles Role.RowValidating
        If e.IsNewRow Then

            Dim errorMessage As String = "<ol style='font-weight:lighter'>"

            Dim roleName As String = e.NewValues("ROLENAME").ToString.Trim
            Dim roleExist As Integer
            With New DataManagerFactory("SELECT COUNT (*) " &
                                    "  FROM ROLE " &
                                    " WHERE LOWER(ROLENAME) = @:NAME ",
                                    "ROLE",
                                    "FrontOfficeConnectionString")
                .AddParameter("NAME", DbType.AnsiStringFixedLength, 80, False, roleName.ToLower)
                roleExist = .QueryExecuteScalarToInteger
            End With
            If roleExist <> 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("RoleExist").ToString)
                e.RowError = errorMessage
            End If

            If e.Errors.Count > 0 Then

                For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                    errorMessage += String.Format("<li>{0}</li>", item.Value)
                Next

                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("MessageErrorText").ToString)

                e.RowError = errorMessage
            End If

        End If

    End Sub

    'Private Sub Role_CallbackError(sender As Object, e As EventArgs) Handles Role.CallbackError

    'End Sub

#End Region

End Class