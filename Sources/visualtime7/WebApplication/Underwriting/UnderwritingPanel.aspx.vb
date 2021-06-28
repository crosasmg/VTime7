#Region "using"

Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web
Imports InMotionGIT.Underwriting.Contracts
Imports System.Globalization
Imports InMotionGIT.Common
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.FrontOffice.Support
Imports InMotionGIT.Workflow.Support.Runtime
Imports DevExpress.Web.ASPxTabControl
Imports System.Web.Services
Imports System.Web.Script.Services
Imports InMotionGIT.Seguridad.Proxy
Imports System.Net
Imports System
Imports System.Configuration

#End Region

Class Underwriting_UnderwritingPanel
    Inherits GIT.Core.PageBase

    Const FORMID As String = "E5F0E658-00D2-4712-865B-59192DB9F90A"
    Dim provider As String = ConfigurationManager.AppSettings.Get("DNEProvider")
    Dim enableHtml5 As Boolean = IIf(Not IsNothing(ConfigurationManager.AppSettings.Get("NBEnableHTML5")) AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5"), True, False)
    Protected Shared webEnableHtml5 As Boolean = IIf(Not IsNothing(ConfigurationManager.AppSettings.Get("NBEnableHTML5")) AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5"), True, False)

    Protected Property runat As String = ""

#Region "Page Events"


    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (enableHtml5) Then
            runat = "server"
            Me.MasterPageFile = "~/fasi/FASI.master"

        Else
            Me.MasterPageFile = "~/DropthingsMasterPage.master"
        End If
    End Sub

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)

        If Session("SessionTimeOut") <> "Yes" Then
            Try
                Dim isUnderwriter As Boolean
                Dim userRoles As String
                Dim userContext As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser

                Title = GetLocalResourceObject("title")
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()
                'If (ConfigurationManager.AppSettings("UW.UserCodeFromBO") AndAlso userContext.UserCode.IsNotEmpty) Then
                '	Session("nUserCode") = userContext.UserCode
                'Else
                '	Session("nUserCode") = userContext.UserID
                'End If
                Session("UserId") = userContext.UserID
                userRoles = InMotionGIT.Membership.Providers.Helper.RetrivellUserData(userContext.UserName).RoleName.ToLower()
                If (enableHtml5) Then
                    isUnderwriter = userRoles.Split(",").Contains("suscriptor")
                Else
                    isUnderwriter = userRoles.Split(";").Contains("suscriptor")
                End If


                If Not isUnderwriter Then
                    RedirectToDefaultPage()
                Else
                    If IsNothing(Session("nUserName")) Then
                        Session("nUserName") = userContext.UserName
                    End If

                    If IsNothing(Session("LanguageID")) Then
                        Session("LanguageID") = userContext.LanguageID
                    End If

                    If IsNothing(Session("UserRoles")) Then
                        Session("UserRoles") = userRoles
                    End If
                End If
            Catch ex As Exception
                RedirectToDefaultPage()
            End Try
        Else
            RedirectToDefaultPage()
        End If

        If Session("SessionTimeOut") <> "Yes" Then
            If Not Context.Session Is Nothing Then
                If Session.IsNewSession Then
                    Dim cookieHeader = Request.Headers("Cookie")
                    If Not cookieHeader Is Nothing AndAlso cookieHeader.IndexOf("ASP.NET_SessionId") >= 0 Then
                        If IsCallback Then
                            ASPxWebControl.RedirectOnCallback("~\Underwriting\SessionTimeOut.aspx")
                        Else
                            Response.Redirect("~\Underwriting\SessionTimeOut.aspx")
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim closeWindow = Request.QueryString("closeWindow")
        If (closeWindow.IsNotEmpty AndAlso closeWindow = 1) Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", "window.close();", True)
        End If

        If hdnUPanel.Contains("IsSaving") AndAlso hdnUPanel.Get("IsSaving") Then
            If hdnUPanel.Contains("IsSaveAndClose") Then
                'SaveInformation(hdnUPanel.Get("IsSaveAndClose"), Nothing)
            Else
                'SaveInformation(False, Nothing)
            End If
        End If

        If Not IsPostBack Then
            InMotionGIT.Underwriting.Proxy.Helpers.Support.RemoveInstance()

            If Session("SessionTimeOut") = "Yes" Then
                If (enableHtml5) Then
                    popupExpiredHtml5.ShowOnPageLoad = True
                Else
                    popupExpired.ShowOnPageLoad = True
                End If
                Session.Remove("SessionTimeOut")
            End If

            If String.IsNullOrEmpty(Session("UserId")) Then
                If IsCallback Then
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/dropthings/Default.aspx")
                Else
                    Response.Redirect("~/dropthings/Default.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
    End Sub

#End Region

#Region "Main Methods"

    ''' <summary>
    ''' Devuelve una instancia de contexto para ser usada en los workflows.
    ''' </summary>
    ''' <param name="sourceId">Identificador de la forma/planilla que ejecuta el workflow</param>
    ''' <returns>Instancia del context para el workflow</returns>
    Public Shared Function WorkFlowContext(sourceId As String) As InMotionGIT.Common.Contracts.Context
        Dim userId As Integer = 0
        Dim nUsercode As Integer = 0
        Dim securitySchemaCode As String = String.Empty
        Dim roleName As String = String.Empty

        If HttpContext.Current.Request IsNot Nothing Then
            Dim session As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session
            If session.IsNotEmpty Then
                userId = session("UserId")
                nUsercode = session("nUsercode")
                securitySchemaCode = session("sSche_code")
                roleName = session("sSche_code")
            End If
        End If

        Return New InMotionGIT.Common.Contracts.Context(InMotionGIT.FrontOffice.Support.LanguageHelper.CurrentCultureToLanguage,
                                                        sourceId,
                                                        userId,
                                                        nUsercode,
                                                        securitySchemaCode,
                                                        roleName,
                                                        HttpContext.Current.Session("AccessToken"))
    End Function

#End Region

    Protected Sub RedirectToDefaultPage()
        Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
        If IsCallback Then
            If (enableHtml5) Then
                Response.Redirect(String.Format("{0}/fasi/default.aspx", baseUrl))
            Else
                ASPxWebControl.RedirectOnCallback(FormsAuthentication.DefaultUrl())
            End If
        Else
            If (enableHtml5) Then

                Response.Redirect(String.Format("{0}/fasi/default.aspx", baseUrl))
            Else
                Response.Redirect(FormsAuthentication.DefaultUrl(), False)
            End If
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function IsSessionTimeOut() As String
        Dim Response As Boolean = False
        If HttpContext.Current.Session("SessionTimeOut") <> "Yes" Then



            Try
                Dim isUnderwriter As Boolean
                Dim userRoles As String
                Dim userContext As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()
                userRoles = InMotionGIT.Membership.Providers.Helper.RetrivellUserData(userContext.UserName).RoleName.ToLower()
                If (webEnableHtml5) Then
                    isUnderwriter = userRoles.Split(",").Contains("suscriptor")
                Else
                    isUnderwriter = userRoles.Split(";").Contains("suscriptor")
                End If


                If Not isUnderwriter Then
                    Response = True
                End If
            Catch ex As Exception
                Response = True
            End Try
        Else
            Response = True
        End If
        Try
            If Not Response AndAlso HttpContext.Current.Session("SessionTimeOut") <> "Yes" Then
                If Not HttpContext.Current.Session Is Nothing Then
                    If HttpContext.Current.Session.IsNewSession Then
                        Dim cookieHeader = HttpContext.Current.Request.Headers("Cookie")
                        If Not cookieHeader Is Nothing AndAlso cookieHeader.IndexOf("ASP.NET_SessionId") >= 0 Then
                            Response = True
                        End If
                    End If
                End If
            End If
        Catch e As Exception
            Response = True
        End Try
        Return Response
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetEditModeStatus(caseId As Integer) As String
        If (caseId.IsNotEmpty AndAlso Not GetToken().IsEmpty) Then
            Dim uwcase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, GetToken())
            Return uwcase.LockedBy <> 0 AndAlso uwcase.LockedOn <> Date.MinValue AndAlso uwcase.LockedBy = HttpContext.Current.Session("UserId")
        End If

        Return False
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetConsultationMode() As String
        Dim instance = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
        Dim respuesta As String = "False"
        If (isUnderwriter()) Then
            If Not IsNothing(instance) Then
                respuesta = instance.Status = InMotionGIT.Underwriting.Contracts.Enumerations.EnumUnderwritingCaseStatus.Consultation
            End If
        End If
        Return respuesta
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetEditPolicyUrl(caseId As Integer) As String
        Dim policyUrl As String = String.Empty
        If (isUnderwriter()) Then
            Dim selectedCase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, GetToken())
            If (Not IsNothing(selectedCase)) Then
                If selectedCase.LinkRiskInfoEdit.IsNotEmpty AndAlso isExistForm(selectedCase.LinkRiskInfoEdit) Then
                    policyUrl = GetPolicyUrl(selectedCase.LinkRiskInfoEdit, selectedCase.UnderwritingCaseID, True)
                End If
            End If
        End If
        Return policyUrl
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetViewPolicyUrlByUWCaseID(uwCaseID As Integer) As String
        Dim policyUrl As String = String.Empty
        If (isUnderwriter()) Then
            Dim selectedCase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance(uwCaseID)
            If (Not IsNothing(selectedCase)) Then
                policyUrl = GetPolicyUrl(selectedCase.LinkRiskInfoView, selectedCase.UnderwritingCaseID)
            End If
        End If
        Return policyUrl
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetViewPolicyUrl() As String
        Dim policyUrl As String = String.Empty
        If (isUnderwriter()) Then
            Dim selectedCase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            If (Not IsNothing(selectedCase)) Then
                policyUrl = GetPolicyUrl(selectedCase.LinkRiskInfoView, selectedCase.UnderwritingCaseID)
            End If
        End If
        Return policyUrl
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHostAndFormUrl() As String
        Dim finalUrl As String = String.Empty
        If (isUnderwriter()) Then
            Dim hostUrl As String = ConfigurationManager.AppSettings("Url.WebApplication").ToString()
            Dim urlForm As String = ""
            If (webEnableHtml5) Then
                urlForm = ConfigurationManager.AppSettings("Url.Form.HTML5").ToString()
            Else
                urlForm = ConfigurationManager.AppSettings("Url.Form").ToString()
            End If
            finalUrl = hostUrl + urlForm + "/"
        End If
        Return finalUrl
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub SetEmptyCase()
        If (isUnderwriter()) Then
            InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.SelectAll(Int64.MinValue, False, False, TokenHelper.GetValidToken)
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function IsEnableHtml5() As Boolean
        If (isUnderwriter()) Then

            If (webEnableHtml5) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function


    Private Shared Function GetPolicyPopupUrl(linkRiskInfo As String, underWritingCaseID As Long, Optional isTemporal As Boolean = False) As String
        Dim finalUrl As String = String.Empty
        Dim hostUrl As String = ConfigurationManager.AppSettings("Url.WebApplication").ToString()
        Dim urlForm As String = ""
        If (webEnableHtml5) Then
            urlForm = ConfigurationManager.AppSettings("Url.Form.HTML5").ToString()
        Else
            urlForm = ConfigurationManager.AppSettings("Url.Form").ToString()
        End If
        If Not IsNothing(linkRiskInfo) Then
            finalUrl = hostUrl + urlForm + "/" + linkRiskInfo + "Popup" + ".aspx?uwCaseId=" + underWritingCaseID.ToString
        End If
        If (isTemporal) Then
            finalUrl += "&esTemporal=True"
        End If
        Return finalUrl
    End Function

    Private Shared Function GetPolicyUrl(linkRiskInfo As String, underWritingCaseID As Long, Optional isTemporal As Boolean = False) As String
        Dim finalUrl As String = String.Empty
        Dim hostUrl As String = ConfigurationManager.AppSettings("Url.WebApplication").ToString()
        Dim urlForm As String = ""

        If (webEnableHtml5) Then
            urlForm = ConfigurationManager.AppSettings("Url.Form.HTML5").ToString()
        Else
            urlForm = ConfigurationManager.AppSettings("Url.Form").ToString()
        End If
        If Not IsNothing(linkRiskInfo) Then
            finalUrl = hostUrl + urlForm + "/" + linkRiskInfo + ".aspx?uwCaseId=" + underWritingCaseID.ToString
        End If
        If (isTemporal) Then
            finalUrl += "&esTemporal=True"
        End If
        Return finalUrl
    End Function

    ''' <summary>
    ''' Retorna un valor booleano true en caso de que el usuario a validar sea suscriptor, falso en caso de que no lo sea.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function isUnderwriter() As Boolean
        Dim Response As Boolean = False
        If HttpContext.Current.Session("SessionTimeOut") <> "Yes" Then
            Try
                Dim userRoles As String
                Dim userContext As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()
                userRoles = InMotionGIT.Membership.Providers.Helper.RetrivellUserData(userContext.UserName).RoleName.ToLower()
                If (webEnableHtml5) Then
                    Response = userRoles.Split(",").Contains("suscriptor")
                Else
                    Response = userRoles.Split(";").Contains("suscriptor")
                End If
            Catch ex As Exception
                Response = False
            End Try
        End If
        Return Response
    End Function

    ''' <summary>
    ''' Verifica que la forma exista en el directorio.
    ''' </summary>
    ''' <param name="formName">Nombre de la forma.</param>
    ''' <returns>Valor booleano que representa si existe o no la forma.</returns>
    Private Shared Function isExistForm(formName As String) As Boolean
        Try
            Dim pathForm As String = ""
            If (webEnableHtml5) Then
                pathForm = String.Format("{0}\forms\{1}.aspx", ConfigurationManager.AppSettings("GeneratePathHTML5").ToString(), formName)
            Else
                pathForm = String.Format("{0}\form\{1}.aspx", ConfigurationManager.AppSettings("GeneratePath").ToString(), formName)
            End If
            Return System.IO.File.Exists(pathForm)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Shared Function GetToken() As String
        Try
            Return TokenHelper.GetValidToken()
        Catch ex As Exception
            ResponseHelper.ErrorToClient(ex, HttpContext.Current, 401)
        End Try
        Return String.Empty
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUserName(userCode As Integer) As String
        Dim lstUser = InMotionGIT.Membership.Providers.Helpers.User.UserLkp()
        Dim userName = (From x In lstUser Where x.Code = userCode Select x.Description).FirstOrDefault()
        If (IsNothing(userName) OrElse userName.IsEmpty()) Then ' It means that the user is located in Security
            Return InMotionGIT.General.Proxy.Security.UserName(userCode)
        Else
            Return GetFrontOfficeUserName(userCode)
        End If
    End Function

    ''' <summary>
    ''' Obtiene username del FrontOFfice en base al usercode
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Private Shared Function GetFrontOfficeUserName(userCode As Integer) As String
        Dim securityServer = ConfigurationManager.AppSettings.Get("STS.URL")
        Dim securityService = securityServer + "/api/users/GetFirstnameLastname"
        Dim uri As String = "?usercode=" + userCode.ToString()
        Dim userName As String = ""
        Using client As New WebClient()
            Try
                client.Encoding = Encoding.UTF8
                client.Headers(HttpRequestHeader.ContentType) = "application/json"
                userName = Newtonsoft.Json.JsonConvert.DeserializeObject(Of String)(client.DownloadString(securityService & uri))
            Catch ex As Exception
                Return ""
            End Try
        End Using
        Return userName
    End Function

End Class
