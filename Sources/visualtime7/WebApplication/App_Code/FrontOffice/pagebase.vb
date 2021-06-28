#Region "Copyright (c) 2007, Global Insurance Technology"

'************************************************************************************
'
' Copyright (c) 2007, Global Insurance Technology
'
'***********************************************************************************

#End Region

#Region "Imports directive"

Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports DevExpress.Web.ASPxGridView
Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.Core.Configuration.Enumerations
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Proxy.UserService.AuthenticationInformation
Imports InMotionGIT.Membership.Providers

#End Region

Namespace GIT.Core

    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class PageBase
        Inherits System.Web.UI.Page

        '     Private startTime As Integer

        Public Event WithErrors(ByVal sender As Object, ByVal e As EventArgs, ByRef errors As InMotionGIT.Common.Contracts.Errors.ErrorCollection)

#Region "Properties"

        Private config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)

        Public Property LanguageId As Integer

        Public Property IsFirstTime() As Boolean
            Get
                Return ViewState("IsFirstTime")
            End Get
            Set(ByVal value As Boolean)
                ViewState("IsFirstTime") = value
            End Set
        End Property

        Public Property SavedPassword() As String
            Get
                Return ViewState("SavedPassword")
            End Get
            Set(ByVal value As String)
                ViewState("SavedPassword") = value
            End Set
        End Property

        Private _UserInfo As InMotionGIT.Membership.Providers.MemberContext

        Public Property UserInfo() As InMotionGIT.Membership.Providers.MemberContext
            Get
                If _UserInfo.IsEmpty Then
                    _UserInfo = New InMotionGIT.Membership.Providers.MemberContext
                End If
                Return _UserInfo
            End Get
            Set(ByVal value As InMotionGIT.Membership.Providers.MemberContext)
                _UserInfo = value
            End Set
        End Property

#End Region

        Public Sub New()

        End Sub

#Region "Page Events"

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim FolderName As String = String.Empty

            If AppRelativeVirtualPath.ToLower().Split(".")(0).Split("/").Length > 2 Then
                FolderName = AppRelativeVirtualPath.ToLower().Split(".")(0).Split("/")(2)
            End If

            If FolderName = "admin" AndAlso
               Not DashboardBusiness.Helpers.Security.IsAdministrator Then
                If IsCallback Then
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/dropthings/Error.aspx?id=GEN9001")
                Else
                    Response.Redirect("~/dropthings/Error.aspx?id=GEN9001")
                End If

            End If

            If Not IsPostBack Then
                Dim strPage As String = Page.ToString().Replace("ASP.", "").Replace("_", ".")
                If Not strPage.Equals("dropthings.error.aspx") Then
                    If UserInfo.UserName.IsNotEmpty Then
                        ValidationUser()
                    End If

                End If
            End If

        End Sub

        Public Sub ValidationUser()
            Dim config As VisualTIME = TryCast(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)

            If config.Security.Mode = EnumSecurityMode.Windows Or config.Security.Mode = EnumSecurityMode.HeaderAuthentication Then
                Dim userInformation = Nothing
                Dim IsTicketSecirityExistAndValid As Boolean

                IsTicketSecirityExistAndValid = InMotionGIT.FrontOffice.Support.AuthenticationHelper.IsTicketSecurityExistAndValid(UserInfo.UserName.ToLower)

                Select Case config.Security.Mode
                    Case EnumSecurityMode.Windows
                        If IsTicketSecirityExistAndValid Then
                            If IsNothing(Session("nUsercode")) Then
                                IsTicketSecirityExistAndValid = False
                            End If
                        End If
                        Using securityServices As UserService.UsersClient = New UserService.UsersClient()
                            userInformation = securityServices.PortalAuthentication(UserInfo.UserName, "", 1, InMotionGIT.Common.Helpers.Connection.GetIPRequest())
                        End Using

                    Case EnumSecurityMode.HeaderAuthentication
                        Using securityServices As UserService.UsersClient = New UserService.UsersClient()
                            userInformation = securityServices.PortalAuthentication(UserInfo.UserName.ToLower, "", 1, InMotionGIT.Common.Helpers.Connection.GetIPRequest())
                        End Using
                End Select

                With userInformation
                    InMotionGIT.FrontOffice.Support.Helpers.Language.UpdateLanguage(userInformation)
                    Select Case .Status
                        Case EnumAuthenticationStatus.UserValid
                            If .User.IsEmployee Then
                                If Not IsNothing(.SessionEnviroment) AndAlso .SessionEnviroment.Count > 0 Then
                                    For Each item In .SessionEnviroment

                                        With item
                                            If .Key.StartsWith("Application.") Then
                                                Application.Lock()
                                                Application.Add(.Key.Replace("Application.", ""), .Value)
                                                Application.UnLock()
                                            Else
                                                Session.Add(.Key, .Value)
                                            End If
                                        End With
                                    Next

                                    Session.Add("SessionID", Session.SessionID)
                                    Session.Add("sAccesswoCon", .SessionEnviroment.Item("sAccesswoCon"))
                                    Session.Add("sInitialsCon", .SessionEnviroment.Item("sInitialsCon"))
                                    If .SessionEnviroment.ContainsKey("CompanyId") Then
                                        Session.Add("CompanyId", .SessionEnviroment.Item("CompanyId"))
                                    End If
                                End If
                            Else
                                If config.Security.Mode = EnumSecurityMode.Windows Then
                                    Session.Add("nUsercode", 0)
                                End If
                            End If

                            Dim securityLevel As Integer = userInformation.User.SecurityLevel

                            If securityLevel = 9 Then
                                Dim userAsigne As String = userInformation.User.RolesAssigned
                                securityLevel = InMotionGIT.Membership.Providers.Helper.RoleSecurityLavel(userAsigne, "FrontOfficeConnectionString")
                            End If

                            Session.Add("SecurityLevel", securityLevel)

                            InMotionGIT.FrontOffice.Support.AuthenticationHelper.ValidateFormsAuthenticationTicket(.User.UserName.ToLower, True, "ClientRedirect", Me, "LogInPopupControl")

                            Dim _Theme As String = String.Empty
                            Dim _Language As String = String.Empty

                            If String.IsNullOrEmpty(userInformation.User.Theme) Then
                                _Theme = config.General.DefaultTheme
                            Else
                                _Theme = userInformation.User.Theme
                            End If

                            If String.IsNullOrEmpty(userInformation.User.Language) Then
                                _Language = config.General.DefaultLanguage
                            Else
                                _Language = userInformation.User.Language
                            End If

                            Session("UserPageSetup") = Nothing

                            InMotionGIT.Membership.Providers.Helper.InformationUserLoad(.User.UserName, New FrontOfficeMembershipInfo With {.IsAdministrator = userInformation.User.IsAdministrator,
                                                                                                                                           .IsEmployee = userInformation.User.IsEmployee,
                                                                                                                                           .IsClient = userInformation.User.IsClient,
                                                                                                                                           .IsProducer = userInformation.User.IsProducer,
                                                                                                                                           .Theme = _Theme,
                                                                                                                                           .Language = _Language,
                                                                                                                                           .UserId = userInformation.User.UserID,
                                                                                                                                           .UserName = userInformation.User.UserName,
                                                                                                                                           .AllowScheduler = userInformation.User.AllowScheduler,
                                                                                                                                           .CurrentPageId = userInformation.User.CurrentPageId,
                                                                                                                                           .ProducerID = userInformation.User.ProducerID,
                                                                                                                                           .ClientID = userInformation.User.ClientID,
                                                                                                                                           .RoleName = userInformation.User.RolesAssigned,
                                                                                                                                           .LanguageId = _LanguageId,
                                                                                                                                            .PasswordNeverExpires = userInformation.User.PasswordNeverExpires})

                        Case Else

                            InMotionGIT.Common.Helpers.LogHandler.TraceLog("ValidateUser", "Not valid " & UserInfo.UserName & " " & userInformation.ToString)
                            Dim message As String = String.Empty

                            If config.Security.Mode = EnumSecurityMode.Windows Then
                                message = Server.UrlEncode("The user is not authorized to use this application")
                            ElseIf config.Security.Mode = EnumSecurityMode.HeaderAuthentication Then
                                message = Server.UrlEncode(String.Format("The {0} is not authorized to use this application", UserInfo.UserName))
                            End If
                            If message.IsNotEmpty Then
                                If IsCallback Then
                                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(String.Format("~/dropthings/Error.aspx?id={0}", message))
                                Else
                                    Response.Redirect(String.Format("~/dropthings/Error.aspx?id={0}", message))
                                End If
                            End If
                    End Select
                End With

            End If
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            'startTime = Environment.TickCount
            'LogManager.Begin("default")

        End Sub

        Public Sub SetUpSetting(userInformation)
            Dim securityLevel As Integer = userInformation.User.SecurityLevel

            If securityLevel = 9 Then
                Dim userAsigne As String = userInformation.User.RolesAssigned
                securityLevel = InMotionGIT.Membership.Providers.Helper.RoleSecurityLavel(userAsigne, "FrontOfficeConnectionString")
            End If

            Session.Add("SecurityLevel", securityLevel)
        End Sub

        Protected Overrides Sub OnPreInit(ByVal e As EventArgs)
            MyBase.OnPreInit(e)

            If Request.QueryString("resetUser").IsNotEmpty() Then
                UserInfo = New InMotionGIT.Membership.Providers.MemberContext(True)
            End If

            If (UserInfo.Theme <> String.Empty) And (UserInfo.Theme <> config.General.DefaultTheme) Then
                If Directory.Exists(Request.PhysicalApplicationPath + "App_Themes\" + UserInfo.Theme) Then
                    Theme = UserInfo.Theme
                Else
                    Theme = config.General.DefaultTheme
                End If
            Else
                Theme = config.General.DefaultTheme
            End If
        End Sub

        Private Sub Page_Error(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Error
            If Not System.Diagnostics.Debugger.IsAttached Then
                Dim lasterror As Exception = Server.GetLastError().GetBaseException()
                InMotionGIT.Common.Helpers.LogHandler.ErrorLog("PageBase", "page", lasterror)

                If (ConfigurationManager.AppSettings("GeneralExceptionUnhandled").ToLower = "true") Then

                    Session("LastError") = lasterror
                    Server.Transfer("/HandlerPageError.aspx")
                End If
            End If
        End Sub

        Protected Overrides Sub OnInit(ByVal e As EventArgs)

            MyBase.OnInit(e)

            If Request.QueryString("SessionTimeOut") <> "Yes" Then
                If Not IsNothing(Context.Session) Then

                    If (Session.IsNewSession) Then
                        Dim szCookieHeader As String = Request.Headers("Cookie")

                        If (szCookieHeader <> String.Empty) Then
                            If (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0) Then

                                If Not UserInfo.IsAnonymous Then
                                    FormsAuthentication.SignOut()

                                    If IsCallback Then
                                        DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/dropthings/Default.aspx?SessionTimeOut=Yes")
                                    Else
                                        Response.Redirect("~/dropthings/Default.aspx?SessionTimeOut=Yes")
                                    End If

                                End If
                            End If
                        End If
                    End If

                    Session("VT_Theme") = UserInfo.Theme  '"Esto es una session creada en OnInit de PageBase"
                End If
            End If
        End Sub

        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
            'Dim endTime As Integer = Environment.TickCount
            'Dim executionTime As Double = (endTime - startTime) / 1000.0
            'Response.Write("Page execution is " + executionTime.ToString + " secords. ")
            'LogManager.Finish("default", "PreRender")
        End Sub

        Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
            'LogManager.Finish("default", "Unload")
        End Sub

#End Region

#Region "Methods"

#Region "Find Controls Methods"

        Public Sub BehaviorShowControls(ByVal behaviorcontrols As String)
            Dim controlname As String = String.Empty
            Dim userControlname As String = String.Empty
            Dim showproperty As String = String.Empty
            Dim controlFound As Control

            For Each control As String In behaviorcontrols.Split(";")
                controlname = control.Split(",")(0)
                showproperty = control.Split(",")(1)
                If controlname.Contains(".") Then
                    controlname = controlname.Split(".")(0)
                End If

                userControlname = AppRelativeVirtualPath.Split("/")(AppRelativeVirtualPath.Split("/").Length - 1)
                userControlname = userControlname.Replace(".aspx", "")

                controlFound = FindControlRecursive(Me, String.Format("{0}{1}UC1", userControlname, controlname))

                If Not IsNothing(controlFound) Then
                    FindValueProperty(controlFound, showproperty)
                Else

                    controlFound = FindControlRecursive(Me, controlname)

                    If Not IsNothing(controlFound) Then
                        If control.Split(",")(0).Contains(".") Then
                            controlname = control.Split(",")(0).Split(".")(1)
                            FindColumGrid(controlFound, controlname, showproperty)
                        Else
                            FindValueProperty(controlFound, showproperty)

                            controlFound = FindControlRecursive(Me, controlname & "Label")

                            If Not IsNothing(controlFound) Then
                                FindValueProperty(controlFound, showproperty)
                            End If

                            controlFound = FindControlRecursive(Me, controlname & "MeasureLabel")

                            If Not IsNothing(controlFound) Then
                                FindValueProperty(controlFound, showproperty)
                            End If

                            controlFound = FindControlRecursive(Me, controlname & "LegendLabel")

                            If Not IsNothing(controlFound) Then
                                FindValueProperty(controlFound, showproperty)
                            End If
                        End If
                    End If
                End If
            Next
        End Sub

        Public Function FindControls(ByVal controlName As String) As Control
            Dim controlFound As Control
            Dim name As String = controlName
            Dim userControlname As String = AppRelativeVirtualPath.Split("/")(AppRelativeVirtualPath.Split("/").Length - 1)

            If name.Contains(".") Then
                name = name.Split(".")(0)
            End If

            userControlname = userControlname.Replace(".aspx", "")

            controlFound = FindControlRecursive(Me, String.Format("{0}{1}UC1", userControlname, name))

            If IsNothing(controlFound) Then
                controlFound = FindControlRecursive(Me, name)

                If IsNothing(controlFound) Then
                    controlFound = FindControlRecursive(Me, String.Format("{0}Label", controlName))
                End If
            End If

            Return controlFound
        End Function

        Public Function FindControlRecursive(ByVal Root As Control, ByVal Id As String) As Control
            Dim FoundCtl As Control = Nothing

            If Not IsNothing(Root.ID) AndAlso Root.ID.ToLower = Id.ToLower Then
                Return Root
            End If

            For Each Ctl As Control In Root.Controls
                FoundCtl = FindControlRecursive(Ctl, Id)
                If Not IsNothing(FoundCtl) Then
                    Return FoundCtl
                End If
            Next
            Return Nothing
        End Function

        Private Shared Sub FindColumGrid(ByVal gridcontrol As ASPxGridView, ByVal columnname As String, ByVal showproperty As String)

            For Each Ctl As GridViewColumn In gridcontrol.Columns
                If Ctl.Name = columnname Then
                    Select Case showproperty
                        Case "Hidden"
                            Ctl.Visible = False
                        Case "Enabled"
                            Throw New Exception("Not implements")
                        Case "Disabled"
                            Throw New Exception("Not implements")
                        Case Else 'Or "Visible"
                            Ctl.Visible = True
                    End Select
                End If
            Next
        End Sub

        Private Shared Sub FindValueProperty(ByVal control As Control, ByVal showproperty As String)
            Select Case showproperty
                Case "Hidden"
                    control.Visible = False
                Case "Enabled"
                    InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("Enabled", control, True)
                Case "Disabled"
                    InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("Enabled", control, False)
                Case Else 'Or "Visible"
                    control.Visible = True
            End Select
        End Sub

#End Region

        Protected Overrides Sub InitializeCulture()
            Dim cultureName As String = String.Empty
            Dim resetUser As Boolean = False
            If Not String.IsNullOrEmpty(Request.QueryString("resetUser")) Then
                InMotionGIT.Common.Helpers.Caching.Remove(String.Format("PropertyUser_{0}", UserInfo.UserName))
                UserInfo = New InMotionGIT.Membership.Providers.MemberContext(True)
                If Not String.IsNullOrEmpty(Request.QueryString("Parameter0")) Then
                    LanguageId = Request.QueryString("Parameter0")
                    cultureName = InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(LanguageId)
                End If
                resetUser = True
            End If

            'Si se preestablece el lenague por medio del parametro del url 'culture'
            If Not String.IsNullOrEmpty(Request.QueryString("culture")) Then
                cultureName = Request.QueryString("culture")
            End If

            'Si no hya ningun lenaguje seleccionado, entonces se estable el lenaguaje pre establecido por el usuario
            If cultureName = String.Empty AndAlso UserInfo.LanguageId <> 0 Then
                cultureName = InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(UserInfo.LanguageId)
            End If

            'Si no hya ningun lenaguje seleccionado, entonces se toma el default para el portal
            If cultureName = String.Empty AndAlso UserInfo.IsAnonymous Then
                cultureName = config.General.DefaultLanguage
            End If

            'Si no hya ningun lenaguje seleccionado, entonces se toma el languaje por default del browser
            If cultureName = String.Empty Then
                Dim contextLanguages As String() = HttpContext.Current.Request.UserLanguages
                If contextLanguages.IsNotEmpty AndAlso contextLanguages.Length > 0 Then
                    cultureName = contextLanguages(0)
                Else
                    cultureName = config.General.DefaultLanguage
                End If
            End If

            'Establece una cualtura por default en caso de no tratarse de alguna de la cultura manejadas
            If Not cultureName.ToLower.StartsWith("es") AndAlso
               Not cultureName.ToLower.StartsWith("pt") AndAlso
               Not cultureName.ToLower.StartsWith("en") Then
                cultureName = "en"
            End If

            'Manejo del error 'CultureNotFoundException', el cual solo ha sido generado en ADS, donde falla por un codigo de
            ' cultura es-419 el cual no es valido.
            If config.General.DefaultLanguage.Split("-").Length >= 1 AndAlso config.General.DefaultLanguage.ToLower().Contains(cultureName.ToLower()) Then
                cultureName = config.General.DefaultLanguage.ToLower()
            End If
            Try
                UICulture = cultureName
                Culture = cultureName
            Catch ex As CultureNotFoundException
                InMotionGIT.Common.Helpers.LogHandler.WarningLog("PageBase", String.Format("InitializeCulture => CultureNotFoundException({0}) fixed", cultureName))
                cultureName = config.General.DefaultLanguage
                UICulture = cultureName
                Culture = cultureName
            End Try

            Dim currentCultureInfo As CultureInfo = New CultureInfo(cultureName)
            Dim ShortDatePattern As String = currentCultureInfo.DateTimeFormat.ShortDatePattern

            If ShortDatePattern.Length < 10 Then
                ShortDatePattern = ShortDatePattern.Replace("M", "MM")
                ShortDatePattern = ShortDatePattern.Replace("MMMM", "MM")
                ShortDatePattern = ShortDatePattern.Replace("d", "dd")
                ShortDatePattern = ShortDatePattern.Replace("dddd", "dd")
                ShortDatePattern = ShortDatePattern.Replace("yy", "yyyy")
                ShortDatePattern = ShortDatePattern.Replace("yyyyyyyy", "yyyy")
                currentCultureInfo.DateTimeFormat.ShortDatePattern = ShortDatePattern
            End If
            Thread.CurrentThread.CurrentCulture = currentCultureInfo
            Thread.CurrentThread.CurrentUICulture = New CultureInfo(cultureName)
            MyBase.InitializeCulture()

            If UserInfo.LanguageId = 0 Then
                UserInfo.LanguageId = InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext(cultureName)
                UserInfo.Language = InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(UserInfo.LanguageId)
                UserInfo.UpdateInformation(UserInfo.UserName, UserInfo.LanguageId, UserInfo.Language)
                LanguageId = InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext(currentCultureInfo.Name)
            ElseIf resetUser Then
                UserInfo.LanguageId = LanguageId
                UserInfo.Language = InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(UserInfo.LanguageId)
                UserInfo.UpdateInformation(UserInfo.UserName, UserInfo.LanguageId, UserInfo.Language)
            Else
                LanguageId = UserInfo.LanguageId
            End If
            Session.Add("App_CultureInfoCode", Thread.CurrentThread.CurrentCulture.Name)
            Session.Add("App_LanguageId", LanguageId)
        End Sub

        Protected Overrides Sub CreateChildControls()
            Try
                Dim mymaster As MasterBase = TryCast(MyBase.Master, MasterBase)
                If Not IsNothing(mymaster) Then
                    With mymaster
                        .LoadUserPageSetup(False)
                        .SetupTabs()
                    End With
                End If
            Catch ex As Exception
                InMotionGIT.Common.Helpers.LogHandler.ErrorLog("CreateChildControls", ex.Message, ex)
            End Try
        End Sub

#End Region

        Public Shared Sub DumpSetup(prefix As String, _setup As InMotionGIT.FrontOffice.Contracts.UserPageSetup, UserInfo As MemberContext)
            If _setup.UserSetting.UserId = 5188 Then


                Dim out As New System.Text.StringBuilder()
                out.AppendLine()

                out.AppendFormat("  UserInfo.UserId {1}<br>", prefix, UserInfo.UserId)
                out.AppendFormat("  UserInfo.UserName {1}<br>", prefix, UserInfo.UserName)
                out.AppendFormat("  UserInfo.LanguageId {1}<br>", prefix, UserInfo.LanguageId)
                out.AppendFormat("  UserInfo.RoleName {1}<br>", prefix, UserInfo.RoleName)

                out.AppendFormat("  UserPageSetup.UserSetting.UserId {1}<br>", prefix, _setup.UserSetting.UserId)
                out.AppendFormat("  UserPageSetup.UserSetting.CurrentPageId {1}<br>", prefix, _setup.CurrentPage.ID)

                If IsNothing(_setup.Pages) Then
                    out.AppendFormat("  UserPageSetup.Page.Count {1}<br>", prefix, "IsNothing")
                Else
                    out.AppendFormat("  UserPageSetup.Page.Count {1}<br>", prefix, _setup.Pages.Count)
                End If
                If IsNothing(_setup.WidgetInstances) Then
                    out.AppendFormat("  UserPageSetup.WidgetInstances.Count {1}<br>", prefix, "IsNothing")
                Else
                    out.AppendFormat("  UserPageSetup.WidgetInstances.Count {1}<br>", prefix, _setup.WidgetInstances.Count)
                    Dim index As Integer = 0
                    For Each item As InMotionGIT.FrontOffice.Contracts.WidgetInstance In _setup.WidgetInstances
                        out.AppendFormat("  UserPageSetup.WidgetInstances {1}.{2}={3}<br>", prefix, index, "PageId", item.PageId)
                        out.AppendFormat("  UserPageSetup.WidgetInstances {1}.{2}={3}<br>", prefix, index, "WidgetId", item.WidgetId)
                        out.AppendFormat("  UserPageSetup.WidgetInstances {1}.{2}={3}<br>", prefix, index, "Title", item.Title)

                        index += 1
                    Next
                End If
                out.Replace("<br>", Microsoft.VisualBasic.Constants.vbCrLf)

                InMotionGIT.Common.Helpers.LogHandler.TraceLog(prefix, out.ToString, "MXTEST13B")
            End If

        End Sub

    End Class

End Namespace