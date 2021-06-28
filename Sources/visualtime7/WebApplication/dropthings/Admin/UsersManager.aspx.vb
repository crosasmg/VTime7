#Region "using"

Imports System.Data
Imports System.Globalization
Imports System.Net
Imports System.Threading
Imports System.Web.Script.Services
Imports System.Web.Services
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.Data
Imports GIT.Core
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Support.User

#End Region

Partial Class dropthings_Admin_UsersManager
    Inherits PageBase

#Region "WebMethod"

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function IsRoleBackOffice(roleName As String, isCheck As Boolean) As Object

        Dim isRoleBackOfficeState As Boolean = False
        Dim Message As String = String.Empty

        Dim Query = "SELECT COUNT(ROLENAME) FROM ROLE " &
                    " WHERE ISBACKOFFICESOURCE = 1 AND ROLENAME=@:ROLENAME"

        With New DataManagerFactory(Query, "ROLE", "FrontOfficeConnectionString")
            .AddParameter("ROLENAME", DbType.StringFixedLength, 255, False, roleName)
            .Cache = InMotionGIT.Common.Enumerations.EnumCache.CacheWithFullParameters
            Dim countRole = .QueryExecuteScalarToInteger()

            If countRole <> 0 Then
                isRoleBackOfficeState = True
                Dim temporalCultureInfoCode As String = HttpContext.Current.Session("App_CultureInfoCode")
                If temporalCultureInfoCode.IsEmpty Then
                    temporalCultureInfoCode = "es"
                End If
                Message = HttpContext.GetLocalResourceObject("/dropthings/Admin/UsersManager.aspx", "RoleIsBackOffice", New CultureInfo(temporalCultureInfoCode)).ToString.Replace("RoleName", roleName)
            End If

        End With
        Dim Result = New With {Key .IsRoleBackOffice = isRoleBackOfficeState, Key .Message = Message}
        Return Result
    End Function

#End Region

#Region "Fields"

    Public IsEditing As Boolean = False
    Dim initialItemCount As Integer = 5

#End Region

#Region "Page Methods"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RegisterRequiresControlState(userPager)
        If Not IsCallback AndAlso Not IsPostBack Then
            RemoveUsers()
            userPager.ItemCount = initialItemCount
            GridViewUsers.DataBind()
        End If

    End Sub

#End Region

#Region "Controls Events"

    Protected Sub userPager_PageIndexChanged(sender As Object, e As System.EventArgs) Handles userPager.PageIndexChanged
        GridViewUsers.DataBind()
    End Sub

    Protected Sub btnSeach_Click(sender As Object, e As System.EventArgs) Handles btnSeach.Click
        GridViewUsers.DataBind()
    End Sub

    Protected Sub userPager_PageSizeChanged(sender As Object, e As System.EventArgs) Handles userPager.PageSizeChanged
        GridViewUsers.DataBind()
    End Sub

    Protected Sub callbackPanel_Callback(sender As Object, e As CallbackEventArgsBase) Handles callbackPanel.Callback
        Dim VisibleIndex As Integer = e.Parameter

        With GridViewUsers
            CheckIsLockedOut.Checked = .GetRowValues(VisibleIndex, "ISLOCKEDOUT")
            CheckIsOnline.Checked = .GetRowValues(VisibleIndex, "ISONLINE")
            TextCreationDate.Text = .GetRowValues(VisibleIndex, "CREATIONDATE")

            If .GetRowValues(VisibleIndex, "LASTLOGINDATE") Is DBNull.Value Then
                TextLastLoginDate.Text = String.Empty
            Else
                TextLastLoginDate.Text = IIf(Date.MinValue = .GetRowValues(VisibleIndex, "LASTLOGINDATE"), "", .GetRowValues(VisibleIndex, "LASTLOGINDATE"))
            End If

            If .GetRowValues(VisibleIndex, "LASTLOCKEDOUTDATE") Is DBNull.Value Then
                TextLastLockoutDate.Text = String.Empty
            Else
                TextLastLockoutDate.Text = IIf(Date.MinValue = .GetRowValues(VisibleIndex, "LASTLOCKEDOUTDATE"), "", .GetRowValues(VisibleIndex, "LASTLOCKEDOUTDATE"))
            End If

            If .GetRowValues(VisibleIndex, "LASTPWDCHANGEDDATE") Is DBNull.Value Then
                TextLastPasswordChanged.Text = String.Empty
            Else
                TextLastPasswordChanged.Text = IIf(Date.MinValue = .GetRowValues(VisibleIndex, "LASTPWDCHANGEDDATE"), "", .GetRowValues(VisibleIndex, "LASTPWDCHANGEDDATE"))
            End If

            UserNameTextBox.Text = .GetRowValues(VisibleIndex, "USERNAME")

            TextRolesAssigned.Text = .GetRowValues(VisibleIndex, "ROLESASSIGNED")

            If ConfigurationManager.AppSettings("FrontOffice.Security.ChangeEmailRecovery").IsNotEmpty() AndAlso
                ConfigurationManager.AppSettings("FrontOffice.Security.ChangeEmailRecovery").ToString().ToLower().Equals("false") Then
                EmailTextBox.ReadOnly = True
                RestorePasswordCheckBox.ReadOnly = True
                RestorePasswordCheckBox.Checked = True

            End If

            EmailTextBox.Text = .GetRowValues(VisibleIndex, "EMAIL")

            If TryCast(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME).Security.Mode = Enumerations.EnumSecurityMode.ActiveDirectory Then
                GetRecoverPasswordButton.Enabled = False
            End If

        End With
    End Sub

    Protected Sub GetRecoverPasswordButton_Click(sender As Object, e As EventArgs) Handles GetRecoverPasswordButton.Click
        Using securityServices As UserService.UsersClient = New UserService.UsersClient()
            securityServices.GetRecoverPassword(UserNameTextBox.Text, EmailTextBox.Text,
                                                RestorePasswordCheckBox.Checked, InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(UserInfo.LanguageId))
            InMotionGIT.Common.Helpers.LogHandler.WarningLog("UserManager", String.Format("El usuario {0} con el codigo: {1}, recobro el password del usuario {2}",
                                                                                                 Profile.UserName, UserCode, UserNameTextBox.Text))
        End Using
    End Sub

#End Region

#Region "GridViewUsers Events"

    Protected Sub GridViewUsers_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles GridViewUsers.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "USERNAME"
                e.Editor.ReadOnly = True
            Case "ROLESASSIGNED"
                Dim GRIDUSER As ASPxGridView = DirectCast(sender, ASPxGridView)

                Dim temporalProperties As ASPxDropDownEdit = DirectCast(e.Editor, ASPxDropDownEdit)

                Dim temporalListRole As ASPxCheckBoxList = temporalProperties.FindControl("DropDownEditColumnListRole")

                Dim roleSelected As String = GRIDUSER.GetRowValues(GRIDUSER.FocusedRowIndex, {"ROLESASSIGNED"})

                LoadRoleByUser(temporalListRole, roleSelected)

            Case Else
        End Select
    End Sub

    Public Sub LoadRoleByUser(control As ASPxCheckBoxList, roleSelected As String)
        InMotionGIT.Common.Helpers.LogHandler.TraceLog("Role asginado", String.Format("'{0}'", roleSelected))
        Dim query As String = String.Empty
        Dim roledSelectedList As New List(Of String)
        Dim sourceList As New List(Of InMotionGIT.Common.DataType.LookUpValue)
        If roleSelected.Contains(",") Then
            For Each ItemRole In roleSelected.Split(",")
                roledSelectedList.Add(ItemRole)
            Next
        Else
            roledSelectedList.Add(roleSelected)
        End If

        Dim keyRoleFromFrontOffice As String = "LoadRoleByUserRoleFrontOffice"

        query = "SELECT DISTINCT  ROLEID," +
                "                 ROLENAME," +
                "                 NVL (ISBACKOFFICESOURCE, 0)," +
                "                 NVL (SECURITYLEVEL, 9)" +
                " FROM   ROLE" +
                " WHERE  ROLE .ISBACKOFFICESOURCE = 0" +
                " ORDER  BY ROLE .ROLENAME ASC "

        With New DataManagerFactory(query, "ROLE", "FrontOfficeConnectionString")
            Dim dataResult = .QueryExecuteToTable(True)
            If dataResult.IsNotEmpty AndAlso dataResult.Rows.Count <> 0 Then
                For Each ItemRow As DataRow In dataResult.Rows
                    With sourceList
                        .Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = ItemRow.StringValue("ROLENAME"),
                                                                               .Description = ItemRow.StringValue("ROLENAME")})
                    End With
                Next
            End If
        End With

        Dim newSource As New List(Of InMotionGIT.Common.DataType.LookUpValue)

        For Each Item In sourceList
            newSource.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = Item.Code,
                                                                                .Description = Item.Description})
        Next

        For Each item In roledSelectedList
            Dim itemFound As String = (From itemInternal In newSource
                                       Where itemInternal.Code.ToLower.Equals(item.ToLower)
                                       Select itemInternal.Code).FirstOrDefault

            If itemFound.IsEmpty Then
                With newSource
                    .Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = item,
                                                                               .Description = item})
                End With
            End If

        Next

        control.Items.Clear()
        control.DataSource = newSource
        control.DataBind()

        For Each Item As ListEditItem In control.Items
            If roledSelectedList.Contains(Item.Text.Trim) Then
                Item.Selected = True
            End If
        Next
    End Sub

    Public Function UserCode() As String
        Dim Resul As String = ""
        If Not IsNothing(Session("nUsercode")) Then
            UserCode = Session("nUsercode").ToString
        End If
        Return Resul
    End Function

    Protected Sub ChannelDropDownColumnList_Init(sender As Object, e As EventArgs)
        Dim listBox As ASPxCheckBoxList = DirectCast(sender, ASPxCheckBoxList)

        Dim roleSelected As String
        roleSelected = GridViewUsers.GetRowValues(GridViewUsers.FocusedRowIndex, {"ROLESASSIGNED"})

        Dim query As String = String.Format("SELECT *" +
                            " FROM   ROLE" +
                            " WHERE  ROLE.ISBACKOFFICESOURCE = 0" +
                            " UNION" +
                            " SELECT *" +
                            " FROM   ROLE" +
                            " WHERE  ROLE.ROLENAME = 'EASE1' ", roleSelected)

        With New DataManagerFactory(query, "ROLE", "FrontOfficeConnectionString")
            listBox.Items.Clear()
            listBox.DataSource = .QueryExecuteToTable(True)
            listBox.DataBind()
        End With
    End Sub

    Protected Sub GridViewUsers_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridViewUsers.CustomCallback
        Select Case e.Parameters.ToLower
            Case "delete"
                Dim KeyValues As Generic.List(Of Object) = GridViewUsers.GetSelectedFieldValues(New String() {"USERNAME", "EMAIL"})

                For Each key As Object In KeyValues
                    UserManager("delete", key(0), key(0), String.Empty, String.Empty, String.Empty, InMotionGIT.Membership.Providers.Enumerations.enumUserType.User, String.Empty,
                                String.Empty, Nothing, False, True)
                    InMotionGIT.Common.Helpers.LogHandler.WarningLog("UserManager", String.Format("El usuario {0} con el codigo: {1}, elimino el usuario {2}",
                                                                                                      Profile.UserName, UserCode, key(0)))

                    InMotionGIT.FrontOffice.Support.Integrations.STS.STS_UsersDelete(key(1))
                Next key

                RemoveUsers()
                GridViewUsers.DataBind()
        End Select
    End Sub

    ''' <summary>
    ''' Elimina un usuario del STST por medio de la dirección de correo electrónico
    ''' </summary>
    ''' <param name="email">Correo electrónico del usuario</param>
    Private Shared Sub STS_UsersDelete(email As String)
        Dim address As String = String.Concat(ConfigurationManager.AppSettings("API.SecurityUsers.URL"), "/Users/Delete?email=", email)
        Dim result = String.Empty

        If (Not Convert.ToBoolean(ConfigurationManager.AppSettings("STS.UseOfValidCertificate"))) Then ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications

        Try

            Using client As New WebClient()
                client.Encoding = Encoding.UTF8
                client.Headers(HttpRequestHeader.ContentType) = "application/json"
                client.Headers(HttpRequestHeader.Authorization) = String.Concat("Bearer ", HttpContext.Current.Session("AccessToken").ToString)
                result = client.UploadString(address, "DELETE", "")
            End Using
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Bypass the cert validation.
    ''' </summary>
    Private Shared Function AcceptAllCertifications(ByVal sender As Object, ByVal certification As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Sub ProcessDataSource(ByRef data As DataTable)
        If data.IsNotEmpty AndAlso data.Rows.Count <> 0 Then
            Dim rows = data.Select("LASTLOCKEDOUTDATE IS NULL")
            If rows.IsNotEmpty AndAlso rows.Count <> 0 Then
                For Each ItemRow As DataRow In rows
                    ItemRow("LASTLOCKEDOUTDATE") = Date.MinValue
                Next
            End If

            rows = data.Select("LASTPWDCHANGEDDATE IS NULL")
            If rows.IsNotEmpty AndAlso rows.Count <> 0 Then
                For Each ItemRow As DataRow In rows
                    ItemRow("LASTPWDCHANGEDDATE") = Date.MinValue
                Next
            End If

            rows = data.Select("ROLESASSIGNED LIKE 'Empty'")
            If rows.IsNotEmpty AndAlso rows.Count <> 0 Then
                For Each ItemRow As DataRow In rows
                    ItemRow("ROLESASSIGNED") = GetLocalResourceObject("RoleAssegnedEmpty").ToString
                Next
            End If

        End If
    End Sub

    Public Sub RemoveUsers()
        Dim startIndex As Integer = userPager.PageIndex * userPager.ItemsPerPage
        Dim endIndex As Integer = (userPager.PageIndex + 1) * userPager.ItemsPerPage
        Dim filter As String = String.Empty
        Dim allowScheduler As Boolean

        Dim key As String = String.Format("pageUser{0}_{1}_{2}_{3}", startIndex.ToString, endIndex.ToString, IIf(filter.IsEmpty, "empty", filter.Trim), allowScheduler.ToString)

        Caching.Remove(key)

    End Sub

    Protected Sub GridViewUsers_DataBinding(sender As Object, e As EventArgs) Handles GridViewUsers.DataBinding

        Dim startIndex As Integer = userPager.PageIndex * userPager.ItemsPerPage
        Dim endIndex As Integer = (userPager.PageIndex + 1) * userPager.ItemsPerPage
        Dim filter As String = txtSearch.Text.Trim
        Dim allowScheduler As Boolean

        Dim client As New InMotionGIT.Membership.Providers.FrontOfficeMembershipProvider
        Dim source = client.UserAllByRawPage(startIndex, endIndex, filter, allowScheduler)

        ProcessDataSource(source)

        userPager.ItemCount = client.UserAllByRawCountPage(filter, allowScheduler)
        GridViewUsers.DataSource = source

    End Sub

    Protected Sub GridViewUsers_RowUpdated(sender As Object, e As ASPxDataUpdatedEventArgs) Handles GridViewUsers.RowUpdated
        GridViewUsers.DataBind()
    End Sub

    Protected Sub GridViewUsers_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles GridViewUsers.RowUpdating
        Dim rolNameSelected As String = e.NewValues("ROLESASSIGNED")
        Dim roleListNew As New List(Of String)
        Dim roleListOld As New List(Of String)
        Dim userNameUse As String = String.Empty

        If e.OldValues("USERNAME").ToString.ToLower.Equals(e.NewValues("USERNAME").ToString.ToLower) Then
            userNameUse = e.NewValues("USERNAME").ToString
        Else
            userNameUse = e.OldValues("USERNAME").ToString
        End If

        If rolNameSelected.Contains(",") Then
            For Each ItemRole In rolNameSelected.Split(",")
                roleListNew.Add(ItemRole)
            Next
        Else
            roleListNew.Add(rolNameSelected)
        End If

        Dim roleListAssigned As String() = Web.Security.Roles.GetRolesForUser(userNameUse)

        For Each ItemRole In Web.Security.Roles.GetRolesForUser(userNameUse)
            roleListOld.Add(ItemRole)
        Next

        Dim listRoleRemove = (From item In roleListOld Where Not roleListNew.Contains(item) Select item).ToList

        Dim listRoleAdd = (From item In roleListNew Where Not roleListOld.Contains(item) Select item).ToList

        If listRoleRemove.Count <> 0 Then
            Dim vectorReleRemove(listRoleRemove.Count - 1) As String

            Dim index As Integer = 0

            For Each Item In listRoleRemove
                vectorReleRemove(index) = Item
                index = index + 1
            Next

            Web.Security.Roles.RemoveUserFromRoles(userNameUse, vectorReleRemove)
        End If

        If listRoleAdd.Count <> 0 Then
            Dim vectorRoleAdd(listRoleAdd.Count - 1) As String

            Dim index As Integer = 0

            For Each item In listRoleAdd
                vectorRoleAdd(index) = item
                index = index + 1
            Next

            Web.Security.Roles.AddUserToRoles(userNameUse, vectorRoleAdd)
        End If

        Dim User As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser = Membership.GetUser(userNameUse)
        Dim isLockedOut As Boolean = False

        User.Email = e.NewValues("EMAIL")

        If Not User.IsLockedOut AndAlso IIf(e.NewValues("ISLOCKEDOUT"), True, False) Then
            LockedUser(User)
            isLockedOut = True
            InMotionGIT.Common.Helpers.LogHandler.WarningLog("UserManager",
                                                             String.Format("El usuario {0} con el código: {1}, blockeo el usuario {2}",
                                                                                               Profile.UserName, UserCode, User.UserName))
        End If

        Dim isInactive As Boolean = False

        If IsNothing(e.NewValues("ISINACTIVE")) Then
            isInactive = False
        Else
            If e.NewValues("ISINACTIVE").ToString.ToLower.Equals("1") Then
                isInactive = True
            Else
                isInactive = False
            End If
        End If

        If Not User.IsInactive AndAlso isInactive Then
            InactiveUser(User)

        ElseIf Not isLockedOut Then
            User.UnlockUser()
        End If

        Dim PasswordNeverExpires As Boolean = False

        If IsNothing(e.NewValues("PASSWORDNEVEREXPIRES")) Then
            PasswordNeverExpires = False
        Else
            If e.NewValues("PASSWORDNEVEREXPIRES").ToString.ToLower.Equals("1") Then
                PasswordNeverExpires = True
            Else
                PasswordNeverExpires = False
            End If
        End If

        User.PasswordNeverExpires = PasswordNeverExpires

        User.ClientID = e.NewValues("CLIENTID")
        User.ProducerID = e.NewValues("PRODUCERID")

        User.IsInactive = isInactive

        User.IsAdministrator = IIf(e.NewValues("ISADMINISTRATOR").ToString.ToLower.Equals("1"), True, False)
        User.AllowScheduler = IIf(e.NewValues("ALLOWSCHEDULER").ToString.ToLower.Equals("1"), True, False)
        User.IsEmployee = IIf(e.NewValues("ISEMPLOYEE").ToString.ToLower.Equals("1"), True, False)

        Dim securityLevel As ASPxTrackBar = CType(GridViewUsers.FindEditRowCellTemplateControl(CType(GridViewUsers.Columns(14), GridViewDataColumn), "tkbrSecurityLevel"), ASPxTrackBar)
        User.SecurityLevel = securityLevel.Value

        InMotionGIT.Common.Helpers.LogHandler.TraceLog("UserManager.aspx", String.Format("El usuario {0} con el código: {1}, actualizo el perfil del usuario {2}",
                                                                                                    UserInfo.UserName, UserCode, User.UserName))
        System.Web.Security.Membership.UpdateUser(User)

        If Not e.OldValues("USERNAME").ToString.ToLower.Equals(e.NewValues("USERNAME").ToString.ToLower) Then
            InMotionGIT.Membership.Providers.Helpers.User.ChangeUserName(User.ProviderUserKey, e.NewValues("USERNAME").ToString)
        End If

        InMotionGIT.Common.Helpers.LogHandler.WarningLog("UserManager", String.Format("El usuario {0} con el código: {1}, actualizo el perfil del usuario {2}",
                                                                                        Profile.UserName, UserCode, User.UserName))

        e.Cancel = True

        InMotionGIT.Common.Helpers.Caching.Remove(String.Format("PropertyUser_{0}", User.UserName))

        RemoveUsers()
        GridViewUsers.CancelEdit()
        Dim _configurations = InMotionGIT.Core.Configuration.VisualTIME.Configuration()
        roleListAssigned = Web.Security.Roles.GetRolesForUser(User.UserName)
        Dim roleId As Integer
        Dim query = "SELECT ROLEID from ROLE where lower(ROLENAME) = lower('Administrador')"
        With New DataManagerFactory(query,
                                    "FrontOfficeConnectionString")

            roleId = .QueryExecuteScalarToInteger()
        End With
        If User.IsAdministrator Then
            query = String.Format("INSERT INTO USERMEMBERROLE (USERID, ROLEID) VALUES ('{0}', '{1}')", User.UserID, roleId)
            Dim foundRole = (From itemDb In roleListAssigned Where itemDb.Equals("Administrador") Select itemDb).FirstOrDefault()
            If foundRole.IsEmpty Then
                With New DataManagerFactory(query,
                                    "FrontOfficeConnectionString")
                    .CommandExecute()
                End With
            End If
        Else
            Dim foundRole = (From itemDb In roleListAssigned Where itemDb.Equals("Administrador") Select itemDb).FirstOrDefault()
            If foundRole.IsNotEmpty Then
                query = String.Format("DELETE from USERMEMBERROLE where USERID = {0} and ROLEID = {1}", User.UserID, roleId)
                With New DataManagerFactory(query,
                                   "FrontOfficeConnectionString")
                    .CommandExecute()
                End With
            End If
        End If
        roleListAssigned = Web.Security.Roles.GetRolesForUser(User.UserName)
        Dim roleListAssignedId As Integer() = InMotionGIT.FASI.Support.Members.RoleByIdByNames(roleListAssigned).ToArray()
        InMotionGIT.FASI.Support.Members.UpdateRoles(User.Email, String.Join(";", roleListAssigned), String.Join(";", roleListAssignedId))
    End Sub

    Private Sub UserEmailValidation(sender As Object, e As ASPxDataValidationEventArgs)
        Dim gridUser As ASPxGridView = DirectCast(sender, ASPxGridView)
        Dim DataSource As DataTable = gridUser.DataSource
        Dim EMAILCHANGE = e.NewValues("EMAIL")
        Dim USERNAME = gridUser.GetRowValues(gridUser.FocusedRowIndex, {"USERNAME"})
        Dim FOUNDEMAIL As String = e.OldValues("EMAIL")

        If Not FOUNDEMAIL.Equals(EMAILCHANGE) Then
            Dim FOUNDDUPLICATED As DataRow() = DataSource.Select(String.Format("OLDEMAIL ='{0}' OR EMAIL='{0}'", EMAILCHANGE))
            If FOUNDDUPLICATED.IsNotEmpty AndAlso FOUNDDUPLICATED.Count <> 0 Then
                e.Errors(GridViewUsers.Columns("EMAIL")) = GetLocalResourceObject("EMAILCHANGEMESSAGEERRORRESOURCE").ToString
            End If
        End If
    End Sub

    Private Sub UserNameValidation(sender As Object, e As ASPxDataValidationEventArgs)
        Dim gridUser As ASPxGridView = DirectCast(sender, ASPxGridView)
        Dim DataSource As DataTable = gridUser.DataSource
        Dim USERNAME = e.NewValues("USERNAME").ToString.ToLower
        Dim result As Integer
        With New DataManagerFactory("SELECT " +
                                    "       COUNT (USERNAMELOW) " +
                                    "FROM " +
                                    "	USERMEMBER " +
                                    "WHERE " +
                                    "	USERNAMELOW = @:USERNAMELOW",
                                    "USERMEMBER",
                                    "FrontOfficeConnectionString")
            .AddParameter("USERNAMELOW", DbType.AnsiString, 256, False, USERNAME)
            result = .QueryExecuteScalarToInteger()
        End With
        If result <> 0 Then
            e.Errors(GridViewUsers.Columns("USERNAME")) = GetLocalResourceObject("USERCHANGEMESSAGEERRORRESOURCE").ToString
        End If
    End Sub

    Protected Sub GridViewUsers_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles GridViewUsers.RowValidating
        If e.Errors.Count = 0 Then

            UserEmailValidation(sender, e)

            If Not e.OldValues("USERNAME").Equals(e.NewValues("USERNAME")) Then
                UserNameValidation(sender, e)
            End If

            Dim roles As String = e.NewValues("ROLESASSIGNED")

            If Not String.IsNullOrEmpty(roles) Then
                Dim config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)
                roles = roles.ToLower

                If roles.Contains(config.Security.ClientRole.ToLower) Then
                    If String.IsNullOrEmpty(e.NewValues("CLIENTID")) Then
                        e.Errors(GridViewUsers.Columns("CLIENTID")) = GetLocalResourceObject("ClientIDMessageErrorResource").ToString
                    End If
                End If

                If roles.Contains(config.Security.ProducerRole.ToLower) Then
                    If String.IsNullOrEmpty(e.NewValues("PRODUCERID")) OrElse e.NewValues("PRODUCERID") = 0 Then
                        e.Errors(GridViewUsers.Columns("PRODUCERID")) = GetLocalResourceObject("ProducerIDMessageErrorResource").ToString
                    End If
                End If

            End If
        End If
    End Sub

#End Region

#Region "Custom Methods"

    Private Sub LockedUser(selectedUser As MembershipUser)

        With New DataManagerFactory("UPDATE USERMEMBER  " &
                                      "SET ISLOCKEDOUT = @:ISLOCKEDOUT, LASTLOCKEDOUTDATE = @:LASTLOCKEDOUTDATE " &
                                      "WHERE USERID = @:USERID  ",
                                      "USERMEMBER", "FrontOfficeConnectionString")

            .AddParameter("ISLOCKEDOUT", DbType.Decimal, 1, False, 1)
            .AddParameter("LASTLOCKEDOUTDATE", DbType.DateTime, 8, False, Now)
            .AddParameter("USERID", DbType.Decimal, 16, False, DirectCast(selectedUser.ProviderUserKey, Integer))
            .CommandExecute()

        End With

    End Sub

    Private Sub InactiveUser(selectedUser As MembershipUser)

        With New DataManagerFactory("UPDATE USERMEMBER  " &
                                      "SET ISINACTIVE = @:ISINACTIVE " &
                                      "WHERE USERID = @:USERID  ",
                                      "USERMEMBER", "FrontOfficeConnectionString")

            .AddParameter("ISINACTIVE", DbType.Decimal, 1, False, 1)
            .AddParameter("USERID", DbType.Decimal, 16, False, DirectCast(selectedUser.ProviderUserKey, Integer))
            .CommandExecute()

        End With

    End Sub

#End Region

#Region "ClientID Requested"

    Protected Sub ClientID_OnItemsRequestedByFilterCondition(ByVal source As Object, ByVal e As ListEditItemsRequestedByFilterConditionEventArgs)
        Dim clientCbo As ASPxComboBox = DirectCast(source, ASPxComboBox)

        clientCbo.Enabled = True
        Dim sql As String = String.Format("SELECT SCLIENT, SCLIENAME , SBIRTHDAT " &
                                           " FROM (SELECT SCLIENT, RTRIM(SCLIENAME) AS SCLIENAME, TO_CHAR(DBIRTHDAT, 'dd/mm/yyyy') AS SBIRTHDAT ,  ROW_NUMBER() OVER (ORDER BY SCLIENAME) ROW_NUM " &
                                                   " FROM Client" &
                                                  " WHERE %FILTER%) Result" &
                                          " WHERE Row_Num BETWEEN {0} and {1}", e.BeginIndex + 1, e.EndIndex + 1)
        If String.IsNullOrEmpty(e.Filter) Then
            sql = sql.Replace("%FILTER%", "sCliename IS NOT NULL")
        Else
            Dim Filter As String = e.Filter.Trim
            If Filter.IndexOf("%") = -1 Then
                Filter = String.Format("%{0}%", Filter)
            End If
            sql = sql.Replace("%FILTER%", String.Format("(SCLIENT LIKE '{0}' OR SCLIENAME LIKE '{0}')", Filter))
        End If

        With New DataManagerFactory(sql, "Client", "BackOfficeConnectionString")
            clientCbo.DataSource = .QueryExecuteToTable(True)
            clientCbo.DataBind()
        End With
    End Sub

    Protected Sub ClientID_OnItemRequestedByValue(ByVal source As Object, ByVal e As ListEditItemRequestedByValueEventArgs)

        If Not String.IsNullOrEmpty(e.Value) Then
            With DirectCast(source, ASPxComboBox)
                Dim value As String = e.Value
                If value.IsNotEmpty Then
                    .DataSource = GetClientIdDesription(value)
                    .DataBind()
                End If
            End With
        End If

    End Sub

    Private Function GetClientIdDesription(SCLIENT As String) As System.Data.DataTable
        Dim result As System.Data.DataTable

        With New DataManagerFactory(String.Format("SELECT SCLIENT,RTRIM(SCLIENAME) AS SCLIENAME, TO_CHAR(DBIRTHDAT, 'dd/mm/yyyy') AS SBIRTHDAT FROM client WHERE (SCLIENT = '{0}') ORDER BY SCLIENAME", SCLIENT), "Client", "BackOfficeConnectionString")
            result = .QueryExecuteToTable(True)

            InMotionGIT.Common.Helpers.LogHandler.TraceLog("Inicialization.aspx", String.Format("El usuario {0} con el codigo: {1}, consulto usuario con el codigo {2}",
                                                                                                Profile.UserName, UserCode, SCLIENT))

        End With

        Return result
    End Function

#End Region

#Region "RoleID Requested"

    Protected Sub RoleName_OnItemsRequestedByFilterCondition(ByVal source As Object, ByVal e As ListEditItemsRequestedByFilterConditionEventArgs)
        Dim ShortDatePattern As String = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern

        Dim sql As String = "SELECT ROLENAME FROM ROLE " &
                                          " WHERE ROLENAME LIKE '%FILTER%' AND ISBACKOFFICESOURCE = 1"
        If String.IsNullOrEmpty(e.Filter) Then
            sql = sql.Replace("FILTER", "")
        Else
            sql = sql.Replace("FILTER", e.Filter)
        End If

        Dim clientCbo As ASPxComboBox = DirectCast(source, ASPxComboBox)

        With New DataManagerFactory(sql, "ROLE", "FrontOfficeConnectionString")
            clientCbo.Items.Clear()
            clientCbo.DataSource = .QueryExecuteToTable(True)
            clientCbo.DataBind()
        End With
    End Sub

    Protected Sub RoleName_OnItemRequestedByValue(ByVal source As Object, ByVal e As ListEditItemRequestedByValueEventArgs)
        If Not String.IsNullOrEmpty(e.Value) Then
            With DirectCast(source, ASPxComboBox)
                Dim value As String = e.Value

                If value.IsNotEmpty Then
                    Dim result As System.Data.DataTable

                    With New DataManagerFactory(String.Format("SELECT ROLENAME FROM ROLE " &
                                                              " WHERE ROLENAME LIKE '{0}' AND ISBACKOFFICESOURCE = 1 ", value), "ROLE", "FrontOfficeConnectionString")
                        result = .QueryExecuteToTable(True)
                    End With
                    .Items.Clear()
                    .DataSource = result
                    .DataBind()
                End If
            End With
        End If
    End Sub

#End Region

#Region "ProducerID Requested"

    Protected Sub ProducerID_OnItemsRequestedByFilterCondition(ByVal source As Object, ByVal e As ListEditItemsRequestedByFilterConditionEventArgs)
        Dim sql As String = String.Format("SELECT NINTERMED AS PRODUCERID, SCLIENAME" &
                                               " FROM (SELECT INTERMEDIA.NINTERMED, RTRIM(CLIENT.SCLIENAME) AS SCLIENAME, ROW_NUMBER() OVER (ORDER BY CLIENT.SCLIENAME) ROW_NUM" &
                                                       " FROM INTERMEDIA INTERMEDIA" &
                                                      " INNER JOIN CLIENT CLIENT" &
                                                              " ON INTERMEDIA.SCLIENT = CLIENT.SCLIENT" &
                                                      " WHERE %FILTER%) Result" &
                                              " WHERE Row_Num BETWEEN {0} and {1}", e.BeginIndex + 1, e.EndIndex + 1)
        If String.IsNullOrEmpty(e.Filter) Then
            sql = sql.Replace("%FILTER%", "CLIENT.SCLIENAME IS NOT NULL")
        Else
            Dim Filter As String = e.Filter.Trim
            If Filter.IndexOf("%") = -1 Then
                Filter = String.Format("%{0}%", Filter)
            End If
            sql = sql.Replace("%FILTER%", String.Format("(NINTERMED LIKE '{0}' OR SCLIENAME LIKE '{0}')", Filter))
        End If

        Dim producerCbo As ASPxComboBox = DirectCast(source, ASPxComboBox)
        With New DataManagerFactory(sql, "INTERMEDIA", "BackOfficeConnectionString")
            producerCbo.DataSource = .QueryExecuteToTable(True)
            producerCbo.DataBind()
        End With
    End Sub

    Protected Sub ProducerID_OnItemRequestedByValue(ByVal source As Object, ByVal e As ListEditItemRequestedByValueEventArgs)
        If Not String.IsNullOrEmpty(e.Value) AndAlso IsNumeric(e.Value) Then
            With DirectCast(source, ASPxComboBox)
                Dim value As Integer = e.Value

                If value.IsNotEmpty Then
                    .DataSource = GetProducerIdDesription(value)
                    .DataBindItems()
                End If
            End With
        End If
    End Sub

    Private Function GetProducerIdDesription(NINTERMED As Integer) As System.Data.DataTable
        Dim result As System.Data.DataTable

        With New DataManagerFactory(String.Format("SELECT INTERMEDIA.NINTERMED AS PRODUCERID, RTRIM(CLIENT.SCLIENAME) AS SCLIENAME FROM INTERMEDIA INTERMEDIA INNER JOIN CLIENT CLIENT ON INTERMEDIA.SCLIENT = CLIENT.SCLIENT WHERE (NINTERMED = {0}) ORDER BY SCLIENAME", NINTERMED), "INTERMEDIA", "BackOfficeConnectionString")
            result = .QueryExecuteToTable(True)
        End With

        Return result
    End Function

#End Region

End Class