Imports System.Web.Services
Imports System.Web.Script.Services
Imports InMotionGIT.Seguridad.Proxy

Partial Class UnderwritingAsync_Services_Restriction
    Inherits GIT.Core.PageBase

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRestrictions(caseId As Integer) As List(Of InMotionGIT.Underwriting.Contracts.UnderwritingRule)
        If (isUnderwriter()) Then
            Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.UnderwritingRule)
            Dim listadoResult As New List(Of InMotionGIT.Underwriting.Contracts.UnderwritingRule)
            Try
                listado = InMotionGIT.Underwriting.Proxy.Helpers.Restriction.SelectAll(caseId, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                For Each restriction As InMotionGIT.Underwriting.Contracts.UnderwritingRule In listado
                    If restriction.AlarmType = InMotionGIT.Underwriting.Contracts.Enumerations.EnumAlarmType.InsuredAmountLimit _
                    Or restriction.AlarmType = InMotionGIT.Underwriting.Contracts.Enumerations.EnumAlarmType.AddFlatExtraPremium _
                    Or restriction.AlarmType = InMotionGIT.Underwriting.Contracts.Enumerations.EnumAlarmType.AddExclusion Then
                        listadoResult.Add(restriction)
                    End If
                Next
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
            Return listadoResult
        Else
            Return New List(Of InMotionGIT.Underwriting.Contracts.UnderwritingRule)
        End If

    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRestrictionByUnderwritingRuleId(caseId As Integer, requirementId As Integer, UnderwritingRuleId As Integer, AlarmType As Integer) As InMotionGIT.Underwriting.Contracts.UnderwritingRule
        If (isUnderwriter()) Then
            Dim restriction As New InMotionGIT.Underwriting.Contracts.UnderwritingRule
            Try
                restriction = InMotionGIT.Underwriting.Proxy.Helpers.Restriction.SelectByUnderwritingRuleId(caseId, requirementId, UnderwritingRuleId, AlarmType, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
            Return restriction
        Else
            Return New InMotionGIT.Underwriting.Contracts.UnderwritingRule
        End If

    End Function
    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValues() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return New List(Of String) From {
                Resources.Restrictions.RequirementType1,
                Resources.Restrictions.RestrictionType,
                Resources.Restrictions.RequirementTypeByLanguage2,
                Resources.Restrictions.RestrictionDescription,
                Resources.Restrictions.FlatExtraPremium3,
                Resources.Restrictions.DurationOfFlatExtraPremiumYears4,
                Resources.Restrictions.DurationOfFlatExtraPremiumMonths5,
                Resources.Restrictions.DurationOfFlatExtraPremiumDays6,
                Resources.Restrictions.Recharge,
                Resources.Restrictions.ExclusionTypeEnumText7,
                Resources.Restrictions.ExclusionTypeByLanguage8,
                Resources.Restrictions.Module,
                Resources.Restrictions.Coverage9,
                Resources.Restrictions.ImpairmentCode10,
                Resources.Restrictions.Currency,
                Resources.Restrictions.WaitingPeriodDays,
                Resources.Restrictions.WaitingPeriodMonths,
                Resources.Restrictions.WaitingPeriodYear,
                Resources.Restrictions.ExclusionPeriodTypeEnumText,
                Resources.Restrictions.WaitingPeriodMonths,
                Resources.Restrictions.WaitingPeriodMonths,
                Resources.Restrictions.WaitingPeriodMonths,
                Resources.Restrictions.WaitingPeriodMonths,
                Resources.Restrictions.WaitingPeriodMonths
            }
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
