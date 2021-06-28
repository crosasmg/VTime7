Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports InMotionGIT.Seguridad.Proxy

Partial Class Underwriting_Services_PolicyHistory
    Inherits GIT.Core.PageBase

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetPremiumHistory(caseId As Integer) As List(Of InMotionGIT.Underwriting.Contracts.UnderwritingCaseRisk)
        Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.UnderwritingCaseRisk)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Try
                listado = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCaseRisk.SelectAllWithoutRiskData(caseId, TokenHelper.GetValidToken)
                For Each UnderwritingCaseRisk As InMotionGIT.Underwriting.Contracts.UnderwritingCaseRisk In listado
                    If UnderwritingCaseRisk.Release > 1 Then
                        UnderwritingCaseRisk.Description = Resources.HistoryPremium.UpdateRiskInformation
                    Else
                        UnderwritingCaseRisk.Description = Resources.HistoryPremium.NewRiskInformation
                    End If
                Next
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return listado
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValues() As List(Of String)
        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
        If (isUnderwriter()) Then
            Return New List(Of String) From {
                Resources.HistoryPremium.UnderwritingCaseID,
                Resources.HistoryPremium.Release1,
                Resources.HistoryPremium.ReleaseDate2,
                Resources.HistoryPremium.RequirementId3,
                Resources.HistoryPremium.Description4,
                Resources.HistoryPremium.DetailBtn5
            }
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetButtonValue() As String
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return Resources.HistoryPremium.DetailBtnTitle
        Else
            Return Nothing
        End If
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
                If (Not IsNothing(ConfigurationManager.AppSettings.Get("NBEnableHTML5")) AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5")) Then
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
End Class
