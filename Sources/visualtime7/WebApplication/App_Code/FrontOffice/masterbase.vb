Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Xml.Linq
Imports Page = InMotionGIT.FrontOffice.Contracts.Page
Imports Dropthings.Web.Util
Imports Dropthings.Web.UI
Imports DashboardBusiness
Imports Microsoft.VisualBasic
Imports InMotionGIT.Core.Configuration.Enumerations
Imports InMotionGIT.Core.Configuration
Imports System.Collections.Generic
Imports InMotionGIT.Common.Extensions

Namespace GIT.Core

    Public Class MasterBase
        Inherits MasterPage

#Region "Private Fields"

        Private config As VisualTIME = DirectCast(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)
        Private _ProfileInfo As InMotionGIT.Membership.Providers.MemberContext
        Private keyAnonymousPageSetup As String = "AnonymousPageSetup"

#End Region

#Region "Public Properties"

        Public Property ProfileInfo() As InMotionGIT.Membership.Providers.MemberContext
            Get
                If _ProfileInfo.IsEmpty Then
                    _ProfileInfo = New InMotionGIT.Membership.Providers.MemberContext
                End If
                Return _ProfileInfo
            End Get
            Set(ByVal value As InMotionGIT.Membership.Providers.MemberContext)
                _ProfileInfo = value
            End Set
        End Property

        Public Property IsFirstTime() As Boolean
            Get
                Return ViewState("IsFirstTime")
            End Get
            Set(ByVal value As Boolean)
                ViewState("IsFirstTime") = value
            End Set
        End Property

        Protected Property _Setup() As InMotionGIT.FrontOffice.Contracts.UserPageSetup
            Get
                Return CType(Context.Items(GetType(InMotionGIT.FrontOffice.Contracts.UserPageSetup)), InMotionGIT.FrontOffice.Contracts.UserPageSetup)
            End Get
            Set(ByVal value As InMotionGIT.FrontOffice.Contracts.UserPageSetup)
                Context.Items(GetType(InMotionGIT.FrontOffice.Contracts.UserPageSetup)) = value
            End Set
        End Property

#End Region

        Private Sub ChangePageCurrent(ByRef userPageSetup As InMotionGIT.FrontOffice.Contracts.UserPageSetup, pageId As Integer)
            _Setup.UserSetting.CurrentPageId = pageId
        End Sub

        Public Sub LoadUserPageSetup(noCache As Boolean)
            Dim pageName As String = Session("PageName")
            Dim IsFirst As Boolean = True
            Dim pageId As Integer
            Dim strPage As String = Page.ToString().Replace("ASP.", "").Replace("_", ".")
            Dim userInformation As InMotionGIT.Membership.Providers.FrontOfficeMembershipInfo = InMotionGIT.Membership.Providers.Helper.RetrivellUserData(ProfileInfo.UserName)
            _Setup = Session("UserPageSetup")

            If IsNothing(Session("IsFirst")) Then
                IsFirst = True
            Else
                IsFirst = Session("IsFirst")
            End If

            If _Setup.IsEmpty Then
                Try
                    If Not ProfileInfo.IsAnonymous Then
                        Using client As New InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient
                            _Setup = client.UserVisit(ProfileInfo.UserId, ProfileInfo.CurrentPageId, ProfileInfo.LanguageId, ProfileInfo.RoleName)
                            Session("UserPageSetup") = _Setup
                        End Using
                    Else
                        If InMotionGIT.Common.Helpers.Caching.NotExist(keyAnonymousPageSetup) Then
                            Using client As New InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient
                                _Setup = client.UserVisit(ProfileInfo.UserId, ProfileInfo.CurrentPageId, ProfileInfo.LanguageId, ProfileInfo.RoleName)
                                Session("UserPageSetup") = _Setup
                                InMotionGIT.Common.Helpers.Caching.SetItem(keyAnonymousPageSetup, _Setup)
                            End Using
                        Else
                            Session("UserPageSetup") = InMotionGIT.Common.Helpers.Caching.GetItem(keyAnonymousPageSetup)
                            _Setup = InMotionGIT.Common.Helpers.Caching.GetItem(keyAnonymousPageSetup)
                        End If
                    End If
                Catch ex As Exception
                    InMotionGIT.Common.Helpers.LogHandler.ErrorLog("LoadUserPageSetup", ex.Message, ex)
                    Dim value = System.Runtime.InteropServices.Marshal.GetHRForException(ex)
                    If value = -2146233087 Then
                        Dim collecctionCookieName As New List(Of String)
                        For Each cookieName As String In Context.Request.Cookies
                            If Not String.Equals("EmailAddress", cookieName) Then
                                collecctionCookieName.Add(cookieName)
                            End If
                        Next
                        For Each Item In collecctionCookieName
                            Dim cookie As HttpCookie = Context.Request.Cookies(Item)
                            cookie.Expires = DateTime.Today.AddYears(-1)
                            Context.Response.Cookies.Set(cookie)
                        Next
                        Response.Redirect(String.Format("{0}/{1}", ConfigurationManager.AppSettings("Url.WebApplication").ToString(), "dropthings/LogOff.aspx"))
                    End If
                End Try
            End If

            If strPage.Equals("dropthings.default.aspx") Then

                If IsFirst Then
                    Session("IsFirst") = False
                    pageName = _Setup.CurrentPage.Title
                    pageId = InMotionGIT.FrontOffice.Support.Helpers.Page.GetPageIdByPageTitle(_Setup, pageName)
                    ChangePageCurrent(_Setup, pageId)
                    ProfileInfo.UpdateInformation(ProfileInfo.UserName, pageId)
                Else
                    If Server.UrlDecode(Request.Url.Query.TrimStart("?")).IsNotEmpty Then
                        pageName = Server.UrlDecode(Request.Url.Query.TrimStart("?"))
                        pageId = InMotionGIT.FrontOffice.Support.Helpers.Page.GetPageIdByPageTitle(_Setup, pageName)
                        If _Setup.UserSetting.CurrentPageId <> pageId Then
                            InMotionGIT.FrontOffice.Proxy.Helpers.UserSetting.UserSettingUpdate(ProfileInfo.UserId, pageId)
                        End If
                        ProfileInfo.UpdateInformation(ProfileInfo.UserName, pageId)
                        ChangePageCurrent(_Setup, pageId)
                    Else
                        pageName = _Setup.CurrentPage.Title
                        pageId = InMotionGIT.FrontOffice.Support.Helpers.Page.GetPageIdByPageTitle(_Setup, pageName)
                        If _Setup.UserSetting.CurrentPageId <> pageId Then
                            InMotionGIT.FrontOffice.Proxy.Helpers.UserSetting.UserSettingUpdate(ProfileInfo.UserId, pageId)
                        End If
                        ChangePageCurrent(_Setup, pageId)
                        ProfileInfo.UpdateInformation(ProfileInfo.UserName, pageId)
                    End If
                End If

            End If

        End Sub

        Public Shared Function PageBelongUser(listPage As List(Of InMotionGIT.FrontOffice.Contracts.Page), pageIdCurrent As Integer) As Boolean
            Dim resul As Boolean = False

            If Not IsNothing(listPage) AndAlso listPage.Count <> 0 Then
                For Each Item In listPage
                    If Item.ID = pageIdCurrent Then
                        resul = True
                        Exit For
                    End If
                Next
            End If
            Return resul
        End Function

        Public Sub SetupTabs()
            Try
                Dim strPage As String = Page.ToString().Replace("ASP.", "").Replace("_", ".")
                If Not strPage.Equals("dropthings.error.aspx") Then

                    Dim tabList As HtmlGenericControl

                    tabList = TryCast(Me.FindControl("tabList"), HtmlGenericControl)

                    tabList.Controls.Clear()
                    Dim ID = _Setup.CurrentPage.ID

                    If Not Request.FilePath.ToLower.EndsWith("default.aspx") Then
                        If Request.FilePath.ToLower.EndsWith("operations.aspx") Then
                            ID = -600
                        ElseIf Request.FilePath.ToLower.EndsWith("scheduler.aspx") Then
                            ID = -601
                        Else
                            ID = -1
                        End If
                    End If

                    For Each page As InMotionGIT.FrontOffice.Contracts.Page In _Setup.Pages
                        Dim li = New HtmlGenericControl("li")
                        li.ID = "Tab" + page.ID.ToString()
                        li.Attributes("class") = "tab " + IIf(page.ID = ID, "activetab", "inactivetab")
                        Dim liWrapper = New HtmlGenericControl("div")
                        li.Controls.Add(liWrapper)
                        liWrapper.Attributes("class") = "tab_wrapper"
                        If page.ID = ID Then
                            Dim tabTextDiv = New HtmlGenericControl("span")
                            tabTextDiv.InnerText = InMotionGIT.FrontOffice.Support.Helpers.Page.GetTitle(page)
                            liWrapper.Controls.Add(tabTextDiv)
                        Else
                            Dim tabLink = New HyperLink()
                            With tabLink
                                .Text = InMotionGIT.FrontOffice.Support.Helpers.Page.GetTitle(page)
                                .NavigateUrl = String.Format("{0}/{1}{2}", ConfigurationManager.AppSettings("Url.WebApplication"), "dropthings/?", Server.UrlEncode(page.Title.Replace(" ", "_")))
                            End With
                            liWrapper.Controls.Add(tabLink)
                        End If
                        tabList.Controls.Add(li)
                    Next

                    If (DashboardBusiness.Helpers.Security.IsAdministrator()) Then setupTabAdmin(ID, tabList)
                    If (DashboardBusiness.Helpers.Security.IsEmployee()) Then setupTabEmployee(ID, tabList)

                    If ID = -1 Then
                        setCurrentOtherTab(tabList)
                    End If

                    If Not ProfileInfo.IsAnonymous Then
                        Dim addNewTabLinkButton As LinkButton = New LinkButton()
                        addNewTabLinkButton.ID = "AddNewPage"
                        addNewTabLinkButton.Text = Dropthings.Web.Util.ResourceManager.getResource("NewTab")
                        AddHandler addNewTabLinkButton.Click, AddressOf addNewTabLinkButton_Click

                        Dim li2 = New HtmlGenericControl("li")
                        li2.Attributes("class") = "newtab"
                        li2.Controls.Add(addNewTabLinkButton)
                        tabList.Controls.Add(li2)
                    End If

                End If
            Catch ex As Exception
                InMotionGIT.Common.Helpers.LogHandler.ErrorLog("SetupTabs", ex.Message, ex)
            End Try
        End Sub

        Private Sub addNewTabLinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)

            Dim defaultTitle As List(Of InMotionGIT.Common.DataType.LookUpValue) = Dropthings.Web.Util.ResourceManager.getListResourceAllSource("NewTab")

            Dim page = (New DashboardFacade()).AddNewPage(Integer.Parse(ProfileInfo.User.ProviderUserKey), InMotionGIT.Membership.Providers.Helper.ConvertList(defaultTitle))
            If Session("UserPageSetup") IsNot Nothing Then
                _Setup.Pages.Add(page)
                _Setup.UserSetting.CurrentPageId = page.ID
                Session("UserPageSetup") = _Setup
            End If

            RedirectToTab(page)
        End Sub

        Public Sub RedirectToTab(ByVal page As InMotionGIT.FrontOffice.Contracts.Page)
            Response.Redirect(String.Format("{0}/{1}{2}", ConfigurationManager.AppSettings("Url.WebApplication"), "dropthings/?", page.Title.Replace(" ", "_")))
        End Sub

        Private Sub setupTabAdmin(ByVal ID As Integer, ByVal tabList As HtmlGenericControl)
            Dim li = New HtmlGenericControl("li")
            li.ID = "TabAdmin"
            li.Attributes("class") = "tab " + IIf(ID = -600, "activetab", "inactivetab")

            Dim liWrapper = New HtmlGenericControl("div")
            li.Controls.Add(liWrapper)
            liWrapper.Attributes("class") = "tab_wrapper"

            If ID = -600 Then
                Dim tabTextDiv = New HtmlGenericControl("span")
                tabTextDiv.InnerText = Dropthings.Web.Util.ResourceManager.getResource("AdminTab")
                liWrapper.Controls.Add(tabTextDiv)
            Else
                Dim tabLink = New HyperLink()
                With tabLink
                    .Text = Dropthings.Web.Util.ResourceManager.getResource("AdminTab")
                    .NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings("Url.WebApplication"), "dropthings/Admin/operations.aspx")
                End With
                liWrapper.Controls.Add(tabLink)
            End If
            tabList.Controls.Add(li)
        End Sub

        Private Sub setupTabEmployee(ByVal ID As Integer, ByVal tabList As HtmlGenericControl)
            Dim li = New HtmlGenericControl("li")
            li.ID = "TabEmployee"
            li.Attributes("class") = "tab " + IIf(ID = -601, "activetab", "inactivetab")

            Dim liWrapper = New HtmlGenericControl("div")
            li.Controls.Add(liWrapper)
            liWrapper.Attributes("class") = "tab_wrapper"

            If ID = -601 Then
                Dim tabTextDiv = New HtmlGenericControl("span")
                tabTextDiv.InnerText = Dropthings.Web.Util.ResourceManager.getResource("SchedulerTab")
                liWrapper.Controls.Add(tabTextDiv)
            Else
                Dim tabLink = New HyperLink()
                With tabLink
                    .Text = Dropthings.Web.Util.ResourceManager.getResource("SchedulerTab")
                    .NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings("Url.WebApplication"), "dropthings/Scheduler/Scheduler.aspx")
                End With
                liWrapper.Controls.Add(tabLink)
            End If
            tabList.Controls.Add(li)
        End Sub

        Private Sub setCurrentOtherTab(ByVal tabList As HtmlGenericControl)
            Dim li = New HtmlGenericControl("li")
            li.ID = "currentTab"
            li.Attributes("class") = "tab activetab"

            Dim liWrapper = New HtmlGenericControl("div")
            li.Controls.Add(liWrapper)
            liWrapper.Attributes("class") = "tab_wrapper"

            Dim tabTextDiv = New HtmlGenericControl("span")
            tabTextDiv.InnerText = Me.Page.Title
            liWrapper.Controls.Add(tabTextDiv)

            tabList.Controls.Add(li)
        End Sub

        Private Sub MasterBase_Load(sender As Object, e As EventArgs) Handles Me.Load
            If ProfileInfo.IsAnonymous Then
                Dim url = Page.AppRelativeTemplateSourceDirectory
                Dim message As String = String.Empty
                If url.ToLower.Contains("generated") Or url.ToLower.Contains("generalquery") Or url.ToLower.Contains("dropthings") Then
                    If InMotionGIT.FrontOffice.Proxy.Helpers.Authentication.ExistCrendencialInForm Then
                        If InMotionGIT.FrontOffice.Proxy.Helpers.Authentication.ValidateAcccesUserLogin(True, message) Then
                            ProfileInfo = New InMotionGIT.Membership.Providers.MemberContext(True)
                            Dim pageBase = TryCast(Page, PageBase)
                            If pageBase.IsNotEmpty Then
                                With pageBase
                                    .UserInfo = ProfileInfo
                                End With
                            End If
                        Else
                            If Page.IsCallback Then
                                DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(String.Format("~/dropthings/Error.aspx?id='{0}'", Server.UrlEncode(message)))
                            Else
                                Response.Redirect(String.Format("~/dropthings/Error.aspx?id='{0}'", Server.UrlEncode(message)))
                            End If
                        End If
                    ElseIf InMotionGIT.FrontOffice.Proxy.Helpers.Authentication.ExistTokenInQueryString Then
                        If InMotionGIT.FrontOffice.Proxy.Helpers.Authentication.ValidateAcccesByToken(True, message) Then
                            ProfileInfo = New InMotionGIT.Membership.Providers.MemberContext(True)

                            Dim pageBase = TryCast(Page, PageBase)
                            If pageBase.IsNotEmpty Then
                                With pageBase
                                    .UserInfo = ProfileInfo
                                End With
                            End If
                        Else
                            If Not message.Contains("@@") Then
                                If Page.IsCallback Then
                                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(String.Format("~/dropthings/Error.aspx?id='{0}'", Server.UrlEncode(message)))
                                Else
                                    Response.Redirect(String.Format("~/dropthings/Error.aspx?id='{0}'", Server.UrlEncode(message)))
                                End If
                            Else
                                If Page.IsCallback Then
                                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(String.Format("~/dropthings/LogOff.aspx?id='{0}'", Server.UrlEncode(message)))
                                Else
                                    Response.Redirect(String.Format("~/dropthings/LogOff.aspx?id='{0}'", Server.UrlEncode(message)))
                                End If
                            End If
                        End If
                    End If

                End If
            End If
        End Sub

    End Class

End Namespace