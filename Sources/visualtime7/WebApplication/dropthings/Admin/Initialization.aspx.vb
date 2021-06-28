#Region "Imports"

Imports System.Data
Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.ServiceModel.Dispatcher
Imports DevExpress.Web.ASPxEditors
Imports GIT.Core
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.FrontOffice.Contracts
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Support
Imports InMotionGIT.FrontOffice.Support.User

#End Region

Partial Class dropthings_Admin_Initialization
    Inherits PageBase

#Region "Properties"

    Public Shared fasiURL As String = ConfigurationManager.AppSettings("API.FASI.URL")
    Public Shared ConectionStringName As String = "FrontOfficeConnectionString"
    Private config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)
    Shared _configurations As InMotionGIT.Core.Configuration.FASIConfiguration = InMotionGIT.Core.Configuration.FASIConfiguration.Configuration()

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack AndAlso Not IsCallback Then
            FillOneUsersInitComboBox()
            FillFrontOfficeUsersComboBox()
            FillSchemaBackOffice()
        End If
    End Sub

#End Region
#Region "WebMethods"
    <System.Web.Services.WebMethod(EnableSession:=True)>
    <Script.Services.ScriptMethod(UseHttpGet:=True, ResponseFormat:=Script.Services.ResponseFormat.Json)>
    Public Shared Function GetAccessToken() As String
        If HttpContext.Current.Session("AccessToken") = Nothing Then
            Return String.Empty
        Else
            Return HttpContext.Current.Session("AccessToken").ToString()
        End If
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <Script.Services.ScriptMethod(UseHttpGet:=True, ResponseFormat:=Script.Services.ResponseFormat.Json)>
    Public Shared Function InitialCleaning(cleanWorkflows As Boolean, cleanRoleAndUsers As Boolean, cleanPendingDoc As Boolean, cleanNavigationDirectory As Boolean, cleanAnonimousUsers As Boolean, cleanTasks As Boolean) As String
        Dim token As String = GetAccessToken()
        Dim params = New With {cleanWorkflows, cleanRoleAndUsers, cleanPendingDoc, cleanNavigationDirectory, cleanAnonimousUsers, cleanTasks}
        'Dim converter = New QueryStringConverter()

        Dim callParameters = "cleanWorkflows=" + cleanWorkflows.ToString
        callParameters += "&cleanRoleAndUsers=" + cleanRoleAndUsers.ToString
        callParameters += "&cleanPendingDoc=" + cleanPendingDoc.ToString
        callParameters += "&cleanNavigationDirectory=" + cleanNavigationDirectory.ToString
        callParameters += "&cleanAnonimousUsers=" + cleanAnonimousUsers.ToString
        callParameters += "&cleanTasks=" + cleanTasks.ToString

        Try
            If Not String.IsNullOrEmpty(token) Or isLocal(HttpContext.Current.Request.UserHostAddress) Then
                Using client As HttpClient = New HttpClient()
                    client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", token)
                    Using response As HttpResponseMessage = client.DeleteAsync(fasiURL + "/api/initialization/v1/InitialCleaning?" + callParameters).Result
                        Using content As HttpContent = response.Content
                            Dim result As String = content.ReadAsStringAsync().Result
                            Return result
                        End Using
                    End Using
                End Using
            End If
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(ex)
        End Try

        Return New With {.Successfully = False, .Reason = InMotionGIT.FASI.Contracts.Properties.Resources.UnknownError}.ToString
    End Function

    Private Shared Function isLocal(host As String) As Boolean
        Try
            Dim clientIPs = Dns.GetHostAddresses(host)
            Dim localIPs = Dns.GetHostAddresses(Dns.GetHostName())

            For Each hostIP In clientIPs
                If IPAddress.IsLoopback(hostIP) Then Return True
                For Each localIP In localIPs
                    If hostIP.Equals(localIP) Then Return True
                Next
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

#End Region

#Region "Controls Events"

    Protected Sub RedirectCallback_Callback(ByVal source As Object, ByVal e As DevExpress.Web.ASPxCallback.CallbackEventArgs) Handles RedirectCallback.Callback
        RedirectLogonPage()
    End Sub


#End Region

#Region "Generate Data Methods"
    Public Function UserEmail() As String
        Dim Resul As String = ""
        If Not IsNothing(Session("email")) Then
            Resul = Session("email").ToString
        End If
        Return Resul
    End Function

#End Region

#Region "Fill Data Methods"

    Private Sub FillOneUsersInitComboBox()
        Dim ListBoxUsers As ASPxListBox = CType(OneUsersInitDropDownEdit.FindControl("OneUsersInitListBox"), ASPxListBox)

        If Not IsNothing(ListBoxUsers) Then
            With ListBoxUsers
                Dim dataSourceTable As DataTable = GetUsersBackOfficeLookup()
                Dim removeUser As Boolean = True
                Dim userName As String = String.Empty

                While removeUser
                    removeUser = False

                    For Each user As DataRow In dataSourceTable.Rows
                        If String.Equals(userName, user("SINITIALS").ToString.Trim, StringComparison.CurrentCultureIgnoreCase) Then
                            dataSourceTable.Rows.Remove(user)
                            userName = String.Empty

                            removeUser = True
                            Exit For
                        End If

                        userName = user("SINITIALS").ToString.Trim
                    Next
                End While

                .DataSource = dataSourceTable
                .DataBind()
            End With
        End If
    End Sub

    Private Sub FillFrontOfficeUsersComboBox()
        Dim users = RetrieveDataUsersFrontOffice()

        With CopyUserConfigurationComboBox
            .DataSource = users
            .DataBind()
        End With

        Dim chkBoxlistTargetUserInter As ASPxListBox = CType(chkBoxlistTargetUser.FindControl("chkBoxlistTargetUserInter"), ASPxListBox)
        With chkBoxlistTargetUserInter
            .DataSource = (From user In users Where user.IsAdministrator = False).ToList()
            .DataBind()
        End With
    End Sub

    Private Sub FillSchemaBackOffice()
        Dim ListBoxUsers As ASPxListBox = CType(chkBoxlistRoleBackOffice.FindControl("chkBoxlistRoleBackOfficeInter"), ASPxListBox)
        With ListBoxUsers
            .DataSource = SecuritySche()
            .DataBind()
        End With
    End Sub



    Protected Sub CopyUserConfigurationCallbackPanel_Callback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles CopyUserConfigurationCallbackPanel.Callback
        FillFrontOfficeUsersComboBox()
    End Sub
#End Region


#Region "Custom Methods"

    Private Sub RedirectLogonPage()

        ' Se elimina el usuario actual y se envia a la página de login
        'If Not UserInfo.UserName.ToLower.Equals(config.Security.AdministratorUser.ToLower) Then
        '    UserManager("delete", UserInfo.UserName, UserInfo.UserName, String.Empty, String.Empty, String.Empty, InMotionGIT.Membership.Providers.Enumerations.enumUserType.User, String.Empty,
        '                String.Empty, Nothing, False, True)
        '    InMotionGIT.Common.Helpers.LogHandler.WarningLog("Initialization", String.Format("El usuario {0} con el codigo: {1}, elimina el usuario {2}",
        '                                                                                      UserInfo.UserName, UserCode, UserInfo.UserName))
        'End If

        FormsAuthentication.SignOut()

        If IsCallback Then
            DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/dropthings/Default.aspx")
        Else
            Response.Redirect("~/dropthings/Default.aspx")
        End If

    End Sub


    Public Shared Function SecuritySche() As DataTable
        Dim result As DataTable = Nothing

        Dim settingService As SettingsService.SettingsClient = New SettingsService.SettingsClient()
        Dim companies As InMotionGIT.FrontOffice.Proxy.SettingsService.MultiCompany() = settingService.MultiCompanyList
        For Each itemCompnay In companies
            With New DataManagerFactory("SELECT SSCHE_CODE FROM SECUR_SCHE WHERE SSTATREGT = '1'", "SSCHE_CODE", "BackOfficeConnectionString")
                .CompanyId = itemCompnay.Identification
                .Cache = InMotionGIT.Common.Enumerations.EnumCache.CacheWithFullParameters
                If result Is Nothing Then
                    result = .QueryExecuteToTable(True)
                Else
                    Dim resultSecuritySchem = .QueryExecuteToTable(True)
                    For Each row As DataRow In resultSecuritySchem.Rows
                        Dim rows = result.Select(String.Format("SSCHE_CODE='{0}'", row.StringValue("SSCHE_CODE")))
                        If rows.Length = 0 Then
                            result.ImportRow(row)
                        End If
                    Next
                End If
            End With
        Next

        Dim dv As DataView = result.DefaultView
        dv.Sort = "SSCHE_CODE ASC"
        result = dv.ToTable()

        Return result
    End Function

    Private Shared Function RetrieveDataUsersFrontOffice() As List(Of Object)
        Dim result As New List(Of Object)
        Dim VTSchemesList As DataTable = Nothing
        Dim SchemesList As New List(Of String)
        Dim filter As String = String.Empty

        VTSchemesList = SecuritySche()

        For Each row As DataRow In VTSchemesList.Rows
            SchemesList.Add(row.StringValue("SSCHE_CODE"))
        Next

        SchemesList = (From item In SchemesList Select String.Format("'{0}'", item)).ToList
        SchemesList.Add(String.Format("'{0}'", _configurations.Security.AdministratorRole))

        filter = String.Join(",", SchemesList)
        Dim query As String = String.Format("	SELECT " +
                                                "		USERMEMBER.USERID, " +
                                                "		USERMEMBER.USERNAME, " +
                                                "		( " +
                                                "			SELECT " +
                                                "				COUNT (*) " +
                                                "			FROM " +
                                                "				ROLE " +
                                                "			WHERE " +
                                                "				ROLEINTERNAL.ROLEID = ROLE.ROLEID " +
                                                "			AND LOWER(ROLE.ROLENAME) = LOWER('{1}') " +
                                                "		) ADMINBYROLE, " +
                                                "		USERMEMBER.ISADMINISTRATOR ADMINBYPROPERTY " +
                                                "	FROM " +
                                                "		USERMEMBER " +
                                                "	INNER JOIN USERMEMBERROLE ON USERMEMBER.USERID = USERMEMBERROLE.USERID " +
                                                "	INNER JOIN ROLE ROLEINTERNAL ON USERMEMBERROLE.ROLEID = ROLEINTERNAL.ROLEID " +
                                                "	WHERE " +
                                                "		USERMEMBER.ISANONYMOUS = 0 " +
                                                "	AND ROLEINTERNAL.ROLENAME IN ({0}) " +
                                                "	ORDER BY " +
                                                "		USERNAME ASC  ", filter, _configurations.Security.AdministratorRole)
        With New DataManagerFactory(query,
                                    "USERMEMBER", ConectionStringName)
            Dim resulQuery = .QueryExecuteToTable(True)
            If resulQuery.IsNotEmpty AndAlso resulQuery.Rows.Count <> 0 Then
                For Each ItemRow As DataRow In resulQuery.Rows
                    Dim ISADMINISTRATOR = False
                    If ItemRow.IntegerValue("ADMINBYPROPERTY") <> 0 Or ItemRow.IntegerValue("ADMINBYROLE") <> 0 Then
                        ISADMINISTRATOR = True
                    End If
                    result.Add(New With {Key .UserName = ItemRow.StringValue("USERNAME"),
                                         Key .ProviderUserKey = ItemRow.IntegerValue("USERID"),
                                         Key .IsAdministrator = ISADMINISTRATOR
                               })
                Next
            End If
        End With

        Return result

    End Function

    Shared Function GetSchemes() As String
        Dim VTSchemesList As DataTable = SecuritySche()

        Dim VTSchemesString As String = String.Empty

        For Each row As DataRow In VTSchemesList.Rows
            If VTSchemesString.Length > 0 Then VTSchemesString += ","
            VTSchemesString += row.Item("SSCHE_CODE").ToString().Trim
        Next

        Return VTSchemesString
    End Function

#End Region
End Class