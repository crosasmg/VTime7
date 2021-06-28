Imports System.Web.Services
Imports System.Web.Script.Services
Imports InMotionGIT.Seguridad.Proxy
Partial Class UnderwritingAsync_Services_UnderwritingCase
    Inherits GIT.Core.PageBase

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUnderwritingCase(underwritingCaseID As Integer, wasLocked As Boolean, isEditing As Boolean) As InMotionGIT.Underwriting.Contracts.UnderwritingCase
        Dim underwritingcase As New InMotionGIT.Underwriting.Contracts.UnderwritingCase
        If (isUnderwriter()) Then
            Try
                underwritingcase = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.SelectAll(underwritingCaseID, wasLocked, isEditing, TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return underwritingcase
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllUnderwritingCases() As List(Of InMotionGIT.Underwriting.Contracts.Lookups.UnderwritingCase)
        Dim underwritingcase As New List(Of InMotionGIT.Underwriting.Contracts.Lookups.UnderwritingCase)
        If (isUnderwriter()) Then
            Try
                underwritingcase = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingCaseLookup(HttpContext.Current.Session("LanguageID"), False)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
            Return underwritingcase
        Else
            Return Nothing
        End If
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SetCurrentRequirementID(caseId As Integer, requirementID As Integer) As Boolean
        If (isUnderwriter()) Then
            Try
                InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.SetCurrentRequirementID(caseId, requirementID, TokenHelper.GetValidToken())
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
            Return True
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

    ''' <summary>
    ''' Retorna información del caso.
    ''' </summary>
    ''' <returns></returns>
    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetInformationCase(caseId As Integer) As Dictionary(Of String, Object)
        Dim result As Dictionary(Of String, Object) = Nothing
        If caseId > 0 Then
            result = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.SelectCaseInfo(caseId, HttpContext.Current.Session("LanguageID"), TokenHelper.GetValidToken)
        End If
        Return result
    End Function

    ''' <summary>
    ''' Guarda la clasificacion del riesgo.
    ''' </summary>
    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub SaveRiskClasification(value As Integer)
        Dim selectedCase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance
        selectedCase.RiskClassification = value
        InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(selectedCase)
        InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(selectedCase.UnderwritingCaseID, selectedCase, TokenHelper.GetValidToken)
    End Sub
End Class
