Imports System
Imports System.Configuration
Imports DevExpress.Web.ASPxClasses
Partial Class Underwriting_TabUnderwritingRules
    Inherits GIT.Core.PageBase

    Protected Property UnderwritingRuleId As String = ""
    Protected Property EffectDate As String = ""

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (Not IsNothing(ConfigurationManager.AppSettings.Get("NBEnableHTML5")) AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5")) Then
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

                Title = GetLocalResourceObject("PageTitle")
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()
                userRoles = InMotionGIT.Membership.Providers.Helper.RetrivellUserData(userContext.UserName).RoleName.ToLower()
                If (Not IsNothing(ConfigurationManager.AppSettings.Get("NBEnableHTML5")) AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5")) Then
                    isUnderwriter = userRoles.Split(",").Contains("suscriptor")
                Else
                    isUnderwriter = userRoles.Split(";").Contains("suscriptor")
                End If

                If Not isUnderwriter Then
                    RedirectToDefaultPage()
                Else
                    'If (ConfigurationManager.AppSettings("UW.UserCodeFromBO") AndAlso userContext.UserCode.IsNotEmpty) Then
                    '    Session("nUserCode") = userContext.UserCode
                    'Else
                    '    Session("nUserCode") = userContext.UserID
                    'End If
                    Session("UserId") = userContext.UserID
                    Session("nUserName") = userContext.UserName
                    Session("LanguageID") = userContext.LanguageID
                    Session("UserRoles") = userRoles
                    HttpContext.Current.Session("TabAlarms") = Nothing
                    HttpContext.Current.Session("TabOriginalAlarms") = Nothing
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        UnderwritingRuleId = Request.QueryString("id")
        If (IsNothing(UnderwritingRuleId) OrElse UnderwritingRuleId.IsEmpty) Then
            UnderwritingRuleId = Request.Form("id")
        End If
        EffectDate = Request.QueryString("effectdate")
        If (IsNothing(EffectDate) OrElse EffectDate.IsEmpty) Then
            EffectDate = Request.Form("effectdate")
        End If
    End Sub
#End Region



    Protected Sub RedirectToDefaultPage()
        Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
        If (Not IsNothing(ConfigurationManager.AppSettings.Get("NBEnableHTML5")) AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5")) Then
            Response.Redirect(String.Format("{0}/fasi/default.aspx", baseUrl))
        Else
            Response.Redirect(FormsAuthentication.DefaultUrl(), False)
        End If
    End Sub

End Class
