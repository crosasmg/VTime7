Imports System.Web.Services
Imports System.Web.Script.Services
Imports InMotionGIT.Seguridad.Proxy

Partial Class UnderwritingAsync_Services_Requirement
    Inherits GIT.Core.PageBase

    Private Const FORMID As String = "E5F0E658-00D2-4712-865B-59192DB9F90A"

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllRequirements(caseId As Integer) As List(Of InMotionGIT.Underwriting.Contracts.Requirement)
        If (isUnderwriter()) Then
            Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.Requirement)
            Try
                listado = InMotionGIT.Underwriting.Proxy.Helpers.Requirement.SelectAll(caseId, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                For i As Integer = 0 To listado.Count - 1
                    Dim requirement = listado.ElementAt(i)
                    If (requirement.IsNew And Not requirement.CreatorUserCode = Convert.ToInt32(HttpContext.Current.Session("UserId"))) Then
                        listado.Remove(requirement)
                    End If
                Next

            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
            Return listado
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function EditRequirement(requirement As InMotionGIT.Underwriting.Contracts.Requirement) As List(Of InMotionGIT.Underwriting.Contracts.Requirement)
        InMotionGIT.Common.Helpers.LogHandler.WarningLog("Requirement.aspx.vb - EditRequirement", String.Format("caseId: {0}", requirement.UnderwritingCaseID))
        If (isUnderwriter()) Then
            Try
                'ResponseHelper.VerifyEditMode()
                Dim userCode As Integer
                If Not IsNothing(HttpContext.Current.Session("UserId")) Then
                    userCode = Integer.Parse(HttpContext.Current.Session("UserId"))
                End If
                requirement.UpdateUserCode = userCode
                requirement.UpdateDate = Today
                requirement.ManualOrAutomatic = InMotionGIT.Underwriting.Contracts.Enumerations.EnumManualOrAutomatic.Manual
                InMotionGIT.Underwriting.Proxy.Helpers.Requirement.UpdateOnCache(requirement, TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
            Return GetAllRequirements(0)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub RemoveRequirement(caseId As Integer, requirementId As String)
        If (isUnderwriter()) Then
            Try
                'ResponseHelper.VerifyEditMode()
                If Not IsNothing(requirementId) Then InMotionGIT.Underwriting.Proxy.Helpers.Requirement.DeleteOnCache(caseId, requirementId, TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub AddRequirementParameters(AlarmTypeEnum As Integer,
                                                Balance As Integer,
                                                Cost As Decimal,
                                                CostDueAmount As Decimal,
                                                PayerEnum As Integer,
                                                ProcessTypeEnum As Integer,
                                                ReceptionDate As String,
                                                ClientId As String,
                                                RequirementDate As String,
                                                RequirementType As Integer,
                                                Status As Integer,
                                                TotalCredits As Long,
                                                TotalDebits As Long,
                                                UnderwritingAreaEnum As Integer,
                                                AcordRequirementCode As Integer,
                                                ProviderId As String,
                                                id As String,
                                                oper As String,
                                                caseId As Integer)
        If (isUnderwriter()) Then
            Try
                'ResponseHelper.VerifyEditMode()
                InMotionGIT.Underwriting.Proxy.Helpers.Requirement.InsertOnCache(RequirementType,
                                                                             ClientId,
                                                                             ProcessTypeEnum,
                                                                             UnderwritingAreaEnum,
                                                                             IIf(RequirementDate = "", Date.MinValue, RequirementDate),
                                                                             IIf(ReceptionDate = "", Date.MinValue, ReceptionDate),
                                                                             Status,
                                                                             0,
                                                                             ProviderId,
                                                                             PayerEnum,
                                                                             0,
                                                                             Cost,
                                                                             CostDueAmount,
                                                                             TotalCredits,
                                                                             TotalDebits,
                                                                             Balance,
                                                                             AcordRequirementCode,
                                                                             Convert.ToInt32(HttpContext.Current.Session("UserId")),
                                                                             String.Empty,
                                                                             Convert.ToInt32(HttpContext.Current.Session("LanguageID")),
                                                                             caseId,
                                                                                 TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllDecisions(caseId As Integer) As List(Of InMotionGIT.Underwriting.Contracts.Requirement)
        Dim listado As New InMotionGIT.Underwriting.Contracts.RequirementCollection
        If (isUnderwriter()) Then
            Try
                listado = InMotionGIT.Underwriting.Proxy.Helpers.Requirement.SelectDecisions(caseId, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                If (Not IsNothing(listado) AndAlso listado.Count > 0) Then

                    For Each Decision As InMotionGIT.Underwriting.Contracts.Requirement In listado
                        Dim ClientName = GetRoleInCaseClientName(Decision.ClientId)
                        If (ClientName.Length > 0) Then
                            Decision.ClientId += " | " & ClientName
                        End If
                        If Decision.ExclusionClientID.IsNotEmpty Then
                            Dim ExcludedClientName = GetRoleInCaseClientName(Decision.ExclusionClientID)
                            If (ExcludedClientName.Length > 0) Then
                                Decision.ExclusionClientDescription = Decision.ExclusionClientID & " | " & ExcludedClientName
                            Else
                                Decision.ExclusionClientDescription = ""
                            End If
                        End If
                        Decision.EncodedExplanation = HttpUtility.JavaScriptStringEncode(Decision.Explanation)
                        Decision.EncodedCommentary = HttpUtility.JavaScriptStringEncode(Decision.Commentary)
                    Next
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return listado
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValues() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return New List(Of String) From {
                Resources.Requirements.RequirementID,
                Resources.Requirements.RequirementType,
                Resources.Requirements.ProcessTypeEnum,
                Resources.Requirements.AlarmTypeEnum,
                Resources.Requirements.PayerEnum,
                Resources.Requirements.UnderwritingAreaEnum,
                Resources.Requirements.RequestedTo,
                Resources.Requirements.ClientId,
                Resources.Requirements.Cost,
                Resources.Requirements.Status,
                Resources.Requirements.StatusByLanguage,
                Resources.Requirements.RequirementTypeEnumText,
                Resources.Requirements.RequirementTypeByLanguage,
                Resources.Requirements.RequestedToByLanguage,
                Resources.Requirements.RequirementDate,
                Resources.Requirements.ReceptionDate,
                Resources.Requirements.AlarmTypeEnumText,
                Resources.Requirements.AlarmTypeByLanguage,
                Resources.Requirements.TotalDebits,
                Resources.Requirements.TotalCredits,
                Resources.Requirements.Balance,
                Resources.Requirements.CostDueAmount,
                Resources.Requirements.AcordRequirementCode,
                Resources.Requirements.ProviderId,
                Resources.Requirements.Link,
                Resources.Requirements.AllowViewRequirement,
                Resources.Requirements.AllowLoadRequirement
            }
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetToolTipValues() As Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return New Dictionary(Of String, String) From {
                {"RequirementType_ToolTip", Resources.Requirements.RequirementType_ToolTip},
                {"ProcessTypeEnum_ToolTip", Resources.Requirements.ProcessTypeEnum_ToolTip},
                {"AlarmTypeEnum_ToolTip", Resources.Requirements.AlarmTypeEnum_ToolTip},
                {"PayerEnum_ToolTip", Resources.Requirements.PayerEnum_ToolTip},
                {"UnderwritingAreaEnum_ToolTip", Resources.Requirements.UnderwritingAreaEnum_ToolTip},
                {"RequestedTo_ToolTip", Resources.Requirements.RequestedTo_ToolTip},
                {"ReceptionDate_ToolTip", Resources.Requirements.ReceptionDate_ToolTip},
                {"Cost_ToolTip", Resources.Requirements.Cost_ToolTip},
                {"Status_ToolTip", Resources.Requirements.Status_ToolTip},
                {"RequirementDate_ToolTip", Resources.Requirements.RequirementDate_ToolTip},
                {"TotalDebits_ToolTip", Resources.Requirements.TotalDebits_ToolTip},
                {"TotalCredits_ToolTip", Resources.Requirements.TotalCredits_ToolTip},
                {"Balance_ToolTip", Resources.Requirements.Balance_ToolTip},
                {"CostDueAmount_ToolTip", Resources.Requirements.CostDueAmount_ToolTip},
                {"AcordRequirementCode_ToolTip", Resources.Requirements.AcordRequirementCode_ToolTip},
                {"ProviderId_ToolTip", Resources.Requirements.ProviderId_ToolTip}
            }
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValuesDecision() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return New List(Of String) From {
            Resources.Decision.CreationDate,
            Resources.Decision.AlarmTypeEnumText,
            Resources.Decision.AlarmTypeID,
            Resources.Decision.AlarmTypeByLanguage,
            Resources.Decision.RequirementID,
            Resources.Decision.RequirementTypeEnumText,
            Resources.Decision.RequirementTypeByLanguage,
            Resources.Decision.QuestionId,
            Resources.Decision.QuestionTextByLanguage,
            Resources.Decision.Answer,
            Resources.Decision.Explanation,
            Resources.Decision.UnderwritingAreaEnumText,
            Resources.Decision.UnderwritingAreaByLanguage,
            Resources.Decision.TotalCredits,
            Resources.Decision.TotalDebits,
            Resources.Decision.Balance,
            Resources.Decision.Commentary,
            Resources.Decision.Commentary,
            Resources.Decision.Status,
            Resources.Decision.StatusByLanguage,
            Resources.Decision.UnderwritingRuleID,
            Resources.Decision.ClientID,
            Resources.Decision.ManualOrAutomatic,
            Resources.Decision.ManualOrAutomatic,
            Resources.Decision.Explanation,
            Resources.Decision.CreatorUserCode,
            Resources.Decision.ExclusionClientID
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

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetExplanationMessage() As String
        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
        Return Resources.Decision.Explanation
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCommentaryTitle() As String
        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
        Return Resources.Decision.Commentary
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetReceptionMaxDateValidation() As String
        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
        Return Resources.Requirements.ReceptionMaxDateValidation
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetReceptionMinDateValidation() As String
        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
        Return Resources.Requirements.ReceptionMinDateValidation
    End Function

    Private Shared Function GetRoleInCaseClientName(ClientId As String) As String
        Dim underwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance
        If (Not IsNothing(underwritingCase)) Then
            Dim RoleInCaseObject As InMotionGIT.Underwriting.Contracts.RoleInCase = underwritingCase.RolesInCase.Find(Function(x) x.ClientID = ClientId)
            If (Not IsNothing(RoleInCaseObject)) Then
                Return RoleInCaseObject.ClientName
            End If
        End If
        Return ""
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UpdateCaseInstanceAfterAddRequirement(caseId As Integer) As Boolean
        Try
            Dim ejemplo As String = ""
            InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.SelectAll(caseId, False, False, TokenHelper.GetValidToken)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

End Class
