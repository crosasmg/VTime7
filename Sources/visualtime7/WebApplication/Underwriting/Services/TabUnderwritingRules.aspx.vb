Imports System.Web.Services
Imports System.Web.Script.Services
Imports InMotionGIT.Seguridad.Proxy
Imports System.Globalization
Imports System.Web
Imports System

Partial Class UnderwritingAsync_Services_TabUnderwritingRules
    Inherits GIT.Core.PageBase 'System.Web.UI.Page

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
    Public Shared Function GetTabHeaderRulesValues() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            'Dim page = DirectCast(HttpContext.Current.CurrentHandler, Page)
            'Dim urlRelativePath As String = page.AppRelativeVirtualPath.Replace("~", "")
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"

            Return New List(Of String) From {
                HttpContext.GetLocalResourceObject(urlRelativePath, "UnderwritingRuleId"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "UnderwritingRuleIdDescription"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "EffectiveDate"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "UnderwritingRuleStatus"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "ImpairmentCode"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "UnderwritingCaseType"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "RequirementType"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "UnderwritingArea"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "LineOfBusinessDescription"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "LineOfBusinessDescription")
            }
        Else
            Return New List(Of String) From {}
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllTabUnderwritingRules() As List(Of InMotionGIT.Underwriting.Contracts.TabUnderwritingRule)
        Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.TabUnderwritingRule)
        If (isUnderwriter()) Then
            Try
                listado = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRuleLkpTyped(HttpContext.Current.Session("LanguageID"), True, TokenHelper.GetValidToken(), True) _
                            .OrderBy(Function(x) x.UnderwritingRuleId).ToList()
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return listado
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDialogButtonsText() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
            Dim arr() As String = {"btnSave", "btnDelete", "btnCancel"}

            For Each i In arr
                dict.Add(i, HttpContext.GetLocalResourceObject(urlRelativePath, i))
            Next
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTUWRMessages() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
            Dim arr() As String = {"requiredFieldsText", "invalidEffectDate", "greaterOrEqualThanToday"}

            For Each i In arr
                dict.Add(i, HttpContext.GetLocalResourceObject(urlRelativePath, i))
            Next
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function getbtnLanguageText() As List(Of String)
        Dim list As New List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
            Dim arr() As String = {"btnLanguage"}

            For Each i In arr
                list.Add(HttpContext.GetLocalResourceObject(urlRelativePath, i))
            Next
        End If
        Return list
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UpdateTabUnderwritingRule(ByVal updRule As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule, ByVal lstTransRule As List(Of InMotionGIT.Underwriting.Contracts.TransUnderwritingRule)) As String
        If (isUnderwriter()) Then
            Dim clientmanagerRule = InMotionGIT.Underwriting.Proxy.Helpers.Support.NewManagerClientInstance()
            Dim userContext As New InMotionGIT.Membership.Providers.FrontOfficeMembershipUser()
            Dim currentRule = InMotionGIT.Underwriting.Contracts.TabUnderwritingRule.CreateNewTabUnderwritingRule()
            Dim currentTUWR As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule = DirectCast(HttpContext.Current.Session("currentTabUndewritingRule"), InMotionGIT.Underwriting.Contracts.TabUnderwritingRule)
            Dim dbTUWR As New InMotionGIT.Underwriting.Contracts.TabUnderwritingRule
            Dim codeRule As Integer = 0
            Dim isNew = False
            Try
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()

                With currentTUWR
                    dbTUWR = InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.RetrieveTabUnderwritingRuleInstanceByRuleId(.UnderwritingRuleId, .EffectiveDate, HttpContext.Current.Session("LanguageID"), TokenHelper.GetValidToken())
                End With
                updRule.UpdateUserCode = userContext.UserID
                With dbTUWR
                    .IsNew = False
                    .IsDeletedMark = False
                    .IsDirty = True
                    If .EffectiveDate <> updRule.EffectiveDate Then
                        isNew = True
                        .CancellationDate = updRule.EffectiveDate
                        If .RuleAlarms IsNot Nothing Then
                            For Each alarm In .RuleAlarms
                                If alarm.RuleRestrictions IsNot Nothing Then
                                    For Each restriction In alarm.RuleRestrictions
                                        restriction.CancellationDate = updRule.EffectiveDate
                                    Next
                                End If
                                alarm.CancellationDate = updRule.EffectiveDate
                            Next
                        End If
                        InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.SynchronizeWithAccessToken(dbTUWR, userContext.UserID, dbTUWR.EffectiveDate, TokenHelper.GetValidToken)
                    Else
                        UpdateTransUnderwritingRule(lstTransRule, dbTUWR)
                        updRule.IsDirty = True
                        updRule.TRANSUNDERWRITINGRULEs = dbTUWR.TRANSUNDERWRITINGRULEs
                        If Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                            Dim listAlarm = HttpContext.Current.Session("TabAlarms")
                            updRule.RuleAlarms = listAlarm
                        End If
                        InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.SynchronizeWithAccessToken(updRule, userContext.UserID, dbTUWR.EffectiveDate, TokenHelper.GetValidToken())
                    End If
                End With
                updRule.CancellationDate = Nothing
                Dim listRuleAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = Nothing
                If Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                    listRuleAlarm = HttpContext.Current.Session("TabAlarms")
                End If

                updRule.RuleAlarms = listRuleAlarm
                'Return message
                If isNew Then
                    dbTUWR.IsDeletedMark = False
                    updRule.UnderwritingRuleId = dbTUWR.UnderwritingRuleId
                    updRule.TRANSUNDERWRITINGRULEs = Nothing
                    updRule.RecordStatus = 1
                    'Return "updRule ->" & updRule.UnderwritingRuleId & " Fecha Efecto -> " & updRule.EffectiveDate & " Length ->"
                    updRule.IsNew = True
                    dbTUWR.IsDirty = False
                    UpdateTransUnderwritingRule(lstTransRule, dbTUWR)
                    With updRule
                        .TRANSUNDERWRITINGRULEs = New InMotionGIT.Underwriting.Contracts.TransUnderwritingRuleCollection
                        dbTUWR.TRANSUNDERWRITINGRULEs.OrderByDescending(Function(x) x.EffectiveDate)
                        dbTUWR.TRANSUNDERWRITINGRULEs.ForEach(
                            Sub(x)
                                If (IsNothing(.TRANSUNDERWRITINGRULEs.Find(Function(z) z.LanguageId = x.LanguageId))) Then
                                    x.IsNew = True
                                    x.IsDeletedMark = False
                                    x.IsDirty = False
                                    x.EffectiveDate = updRule.EffectiveDate
                                    .TRANSUNDERWRITINGRULEs.Add(x)

                                End If
                            End Sub
                        )
                    End With
                    'Return "--->" + message + "<---"
                    codeRule = InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.SynchronizeWithAccessToken(updRule, userContext.UserID, updRule.EffectiveDate, TokenHelper.GetValidToken())
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
            HttpContext.Current.Session("currentTabUndewritingRule") = updRule
            Return ""
        Else
            Return ""
        End If
    End Function


    Private Shared Sub UpdateTransUnderwritingRule(lstTransRule As List(Of InMotionGIT.Underwriting.Contracts.TransUnderwritingRule), ByRef dbTUWR As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule)
        If (Not IsNothing(lstTransRule) AndAlso lstTransRule.Count > 0) Then
            Dim transUnderwritingRuleBuffer = lstTransRule.First()
            For Each transUnderwritingRuleInObject As InMotionGIT.Underwriting.Contracts.TransUnderwritingRule In dbTUWR.TRANSUNDERWRITINGRULEs
                If (transUnderwritingRuleInObject.LanguageId = transUnderwritingRuleBuffer.LanguageId) Then
                    If (Not IsNothing(transUnderwritingRuleBuffer.Description) AndAlso transUnderwritingRuleBuffer.Description <> "") Then
                        transUnderwritingRuleInObject.Description = transUnderwritingRuleBuffer.Description
                    End If
                    transUnderwritingRuleInObject.Explanation = transUnderwritingRuleBuffer.Explanation
                    transUnderwritingRuleInObject.IsDirty = True
                End If
            Next
        End If
    End Sub

    Private Shared Function UpdateRuleRestriction(ByRef listRuleAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection) As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
        Dim listRestrictionBuffer As New InMotionGIT.Underwriting.Contracts.RuleRestrictionsCollection

        If (Not IsNothing(listRuleAlarm)) Then
            For Each alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms In listRuleAlarm
                If (Not IsNothing(alarm.RuleRestrictions)) Then
                    For Each restriction As InMotionGIT.Underwriting.Contracts.RuleRestrictions In alarm.RuleRestrictions
                        If (Not IsNothing(restriction.ImpairmentCode) AndAlso restriction.ImpairmentCode <> "") Then
                            restriction.ImpairmentCode = restriction.ImpairmentCode.Split("|").First.Trim()
                        End If
                        If (restriction.RestrictionType = InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.Exclusion _
                                      AndAlso restriction.IsDirty AndAlso Not restriction.IsNew AndAlso Not restriction.IsDeletedMark _
                                      AndAlso (restriction.FlatExtraPremium = 0 OrElse restriction.ExtraPremiumPercentage = 0)) Then
                            Dim dataRestriction As New InMotionGIT.Underwriting.Contracts.RuleRestrictions
                            dataRestriction = restriction.Clone()
                            listRestrictionBuffer.Add(dataRestriction)
                            restriction.IsDirty = False
                            restriction.IsNew = False
                            restriction.IsDeletedMark = True
                        End If
                    Next
                    For Each restriction As InMotionGIT.Underwriting.Contracts.RuleRestrictions In listRestrictionBuffer
                        restriction.IsNew = True
                        restriction.IsDirty = False
                        restriction.IsDeletedMark = False
                        'restriction.RestrictionId = GetLastIdRestriccion(Nothing, listRuleAlarm)
                        alarm.RuleRestrictions.Add(restriction)
                    Next
                End If
            Next
        End If
        Return listRuleAlarm
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CreateTabUnderwritingRule(ByVal newRule As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule, ByVal lstTransRule As List(Of InMotionGIT.Underwriting.Contracts.TransUnderwritingRule)) As String
        Dim clientmanagerRule = InMotionGIT.Underwriting.Proxy.Helpers.Support.NewManagerClientInstance()
        Dim userContext As New InMotionGIT.Membership.Providers.FrontOfficeMembershipUser()
        Dim currentRule = InMotionGIT.Underwriting.Contracts.TabUnderwritingRule.CreateNewTabUnderwritingRule()
        Dim createdRule As Integer = 0
        If (isUnderwriter()) Then
            Try
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()

                'If newRule.EffectiveDate = Date.MinValue Then newRule.EffectiveDate = Date.Today
                newRule.RecordStatus = 1
                newRule.IsNew = True
                newRule.IsDeletedMark = False
                newRule.IsDirty = False
                newRule.UnderwritingRuleId = 0

                If IsNothing(newRule.TRANSUNDERWRITINGRULEs) Then newRule.TRANSUNDERWRITINGRULEs = New InMotionGIT.Underwriting.Contracts.TransUnderwritingRuleCollection()

                For nLanguage = 1 To 2
                    Dim nLanguage_inner = nLanguage
                    lstTransRule.ForEach(Sub(x)
                                             Dim tmp As InMotionGIT.Underwriting.Contracts.TransUnderwritingRule = x.Clone()
                                             'tmp.UnderwritingRuleId = created_rule
                                             tmp.EffectiveDate = newRule.EffectiveDate
                                             tmp.LanguageId = nLanguage_inner
                                             tmp.IsNew = True
                                             tmp.IsDeletedMark = False
                                             tmp.IsDirty = False
                                             newRule.TRANSUNDERWRITINGRULEs.Add(tmp)
                                         End Sub
                    )
                Next
                Dim listRuleAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = Nothing
                If Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                    listRuleAlarm = HttpContext.Current.Session("TabAlarms")
                End If
                newRule.RuleAlarms = listRuleAlarm
                If (Not IsNothing(newRule.RuleAlarms)) Then
                    For Each alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms In newRule.RuleAlarms
                        If (Not IsNothing(alarm.RuleRestrictions)) Then
                            For Each restriction As InMotionGIT.Underwriting.Contracts.RuleRestrictions In alarm.RuleRestrictions
                                If (Not IsNothing(restriction.ImpairmentCode) AndAlso restriction.ImpairmentCode <> "") Then
                                    restriction.ImpairmentCode = restriction.ImpairmentCode.Split("|").First.ToString.Trim
                                End If
                            Next
                        End If
                    Next
                End If
                createdRule = InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.SynchronizeWithAccessToken(newRule, userContext.UserID, newRule.EffectiveDate, TokenHelper.GetValidToken())
            Catch ex As Exception
                'ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
        End If
        Return createdRule
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function DeleteTabUnderwritingRule(ByVal delRule As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule) As String
        Dim userContext As New InMotionGIT.Membership.Providers.FrontOfficeMembershipUser()
        Dim deleted_rule As Integer = 0
        If (isUnderwriter()) Then
            Dim dbTUWR As New InMotionGIT.Underwriting.Contracts.TabUnderwritingRule
            Dim currentTUWR As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule = DirectCast(HttpContext.Current.Session("currentTabUndewritingRule"), InMotionGIT.Underwriting.Contracts.TabUnderwritingRule)

            Try
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()

                If (Not InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.IsTabUnderwritingRuleInUse(currentTUWR.UnderwritingRuleId, TokenHelper.GetValidToken())) Then

                    Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.TabUnderwritingRule)

                    listado = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRuleLkpTyped(HttpContext.Current.Session("LanguageID"), True, TokenHelper.GetValidToken(), True)
                    listado = listado.FindAll(Function(x) x.UnderwritingRuleId = currentTUWR.UnderwritingRuleId).ToList()

                    For Each tempTUWR As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule In listado

                        With tempTUWR
                            dbTUWR = InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.RetrieveTabUnderwritingRuleInstanceByRuleId(.UnderwritingRuleId, .EffectiveDate, HttpContext.Current.Session("LanguageID"), TokenHelper.GetValidToken())
                        End With

                        dbTUWR.IsNew = False
                        dbTUWR.IsDeletedMark = True
                        dbTUWR.IsDirty = False

                        deleted_rule = InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.SynchronizeWithAccessToken(dbTUWR, userContext.UserID, Date.Today(), TokenHelper.GetValidToken)
                    Next
                Else
                    Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
                    System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

                    Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
                    Return HttpContext.GetLocalResourceObject(urlRelativePath, "msgDeletedDenied")
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
        End If
        Return ""
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateRecordHistories(underwritingRuleId As Integer) As String
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"

            Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.TabUnderwritingRule)

            listado = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRuleLkpTyped(HttpContext.Current.Session("LanguageID"), True, TokenHelper.GetValidToken(), True)
            listado = listado.FindAll(Function(x) x.UnderwritingRuleId = underwritingRuleId).ToList()

            If (listado.Count > 1) Then
                Return HttpContext.GetLocalResourceObject(urlRelativePath, "TbUwrHistory")
            Else
                Return HttpContext.GetLocalResourceObject(urlRelativePath, "ConfirmChanges")
            End If
        Else
            Return ""
        End If
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValuesRuleAlarms() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"

            Return New List(Of String) From {
                HttpContext.GetLocalResourceObject(urlRelativePath, "TbUwRProduct"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "TbUwRAlarmType"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "TbUwRUpdOnlyRisk"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "TbUwRCaseStage"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "TbUwRCaseStatus"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "TbUwRCaseDecision"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "TbUwRComment")
            }
        Else
            Return New List(Of String) From {}
        End If
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTabUnderwritingRuleAlarms(idRule As Long, effectDate As Date, idLanguage As Integer) As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
        If (isUnderwriter()) Then
            Dim listAlarm = New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            Dim listAlarmResult = New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            Dim listOriginalAlarm = New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            If IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                If (idRule > 0) Then
                    Dim tabUnderwritingRule = InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.RetrieveTabUnderwritingRuleInstanceByRuleId(idRule, effectDate, idLanguage, TokenHelper.GetValidToken())
                    CleanNothingRecords(tabUnderwritingRule, effectDate)
                    listAlarm = tabUnderwritingRule.RuleAlarms
                    If (IsNothing(listAlarm)) Then
                        listAlarm = New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
                    End If
                    listOriginalAlarm = tabUnderwritingRule.RuleAlarms
                    HttpContext.Current.Session("TabAlarms") = listAlarm
                    HttpContext.Current.Session("TabOriginalAlarms") = listOriginalAlarm
                End If
            Else
                listAlarm = HttpContext.Current.Session("TabAlarms")
            End If
            'listAlarmResult = listAlarm.FindAll(Function(x) Not (x.IsDeletedMark))
            'listAlarm.RemoveAll(Function(x) x.IsDeletedMark)
            Return listAlarm
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Limpia registros que tengan fecha 01/01/0001 que no pudieron ser eliminados en la consulta
    ''' </summary>
    ''' <param name="tabUnderwritingRule"></param>
    ''' <param name="effectDate"></param>
    Private Shared Sub CleanNothingRecords(ByRef tabUnderwritingRule As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule, effectDate As Date)
        Dim secondCompareDate As Date = DateTime.MinValue
        secondCompareDate = secondCompareDate.AddYears(2000)
        If (Not IsNothing(tabUnderwritingRule.RuleAlarms) AndAlso effectDate > DateTime.MinValue AndAlso effectDate <> secondCompareDate) Then
            For Each RuleAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms In tabUnderwritingRule.RuleAlarms
                If (Not IsNothing(RuleAlarm.RuleRestrictions)) Then
                    RuleAlarm.RuleRestrictions.RemoveAll(Function(x) IsNothing(x.RecordEffectiveDate) OrElse x.RecordEffectiveDate = DateTime.MinValue)
                End If
            Next
            tabUnderwritingRule.RuleAlarms.RemoveAll(Function(x) IsNothing(x.EffectiveDate) OrElse x.EffectiveDate = DateTime.MinValue)
        End If
    End Sub


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCultureInfoName() As String
        Return CultureInfo.CurrentCulture.Name
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDateTimeFormat() As String
        Return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.Replace("dd", "d").Replace("MM", "m").Replace("yyyy", "Y")
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function getDateTimeFormatShortPattern() As String
        Return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CompareMinorDate(date1 As String, date2 As String) As String

        Dim DateFormated1 As DateTime = Convert.ToDateTime(date1)
        Dim DateFormated2 As DateTime = Convert.ToDateTime(date2)

        If (DateFormated1 < DateFormated2) Then
            Return "True"
        Else
            Return "False"
        End If

    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function formatDate(DateToFormat As Date) As String
        If (Not IsNothing(DateToFormat)) Then
            Return DateToFormat.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture)
        Else
            Return New Date().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture)
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValuesRuleAlarmsExclusions() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"

            Return New List(Of String) From {
                "RestrictionId",
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclExclusionType"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclRatingTable"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclProductModule"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclCoverage"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclImpairmentCode"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclCause"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclExclusionPeriodType"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclWaitingPeriodDays"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclWaitingPeriodMonths"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblExclWaitingPeriodYears")
            }
        Else
            Return New List(Of String) From {}
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RetrieveTabUnderwritingRule(underwritingRuleId As Integer, effectDate As Date, languageId As Integer) As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule
        Dim ret = InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.RetrieveTabUnderwritingRuleInstanceByRuleId(underwritingRuleId, effectDate, languageId, TokenHelper.GetValidToken())
        Return ret
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValuesRuleAlarmsDiscoexprem() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"

            Return New List(Of String) From {
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExDiscountorextrapremiumcode"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExDiscountorExtraPremiumType"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExExtraPremiumPercentage"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExFlatExtraPremium"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExCurrencyCode"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExXPremiumDiscountOnlyInsured"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExTypeOfUnit"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExExclusionPeriodType"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExDOfFlatExtraPremiumDays"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExDOfFlatExtraPremiumMonths"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblDisExDOfFlatExtraPremiumYears")
            }
        Else
            Return New List(Of String) From {}
        End If
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValuesRuleAlarmsMaxInsuredSum() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"

            Return New List(Of String) From {
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblMaxInsSumProductModule"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblMaxInsSumCoverageCode"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblMaxInsSumMaximumInsuredAmount"),
                HttpContext.GetLocalResourceObject(urlRelativePath, "tblMaxInsSumCurrencyCode")
            }
        Else
            Return New List(Of String) From {}
        End If
    End Function


    '*********************************** Inicio Ajustes ***************************************'

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub CleanListAlarms()
        If (isUnderwriter()) Then
            Dim listAlarm = Nothing
            If Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                HttpContext.Current.Session("TabAlarms") = Nothing
            End If
            If Not IsNothing(HttpContext.Current.Session("TabOriginalAlarms")) Then
                HttpContext.Current.Session("TabOriginalAlarms") = Nothing
            End If
        End If
    End Sub



    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub AddAlarm(newAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms)
        If (isUnderwriter()) Then
            Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            'Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Try
                If Not IsNothing(newAlarm) Then
                    If IsNothing(HttpContext.Current.Session("TabAlarms")) Then HttpContext.Current.Session("TabAlarms") = New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
                    listAlarms = HttpContext.Current.Session("TabAlarms")
                    If (Not IsNothing((From list In listAlarms Where list.AlarmType = newAlarm.AlarmType AndAlso list.Product = newAlarm.Product AndAlso list.IsDeletedMark = False Select list).SingleOrDefault)) Then Throw New Exception("La alarma ya existe")
                    newAlarm.IsNew = True
                    newAlarm.IsDirty = False
                    newAlarm.IsDeletedMark = False
                    listAlarms.Add(newAlarm)
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RemoveAlarm(selectedAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms) As String
        If (isUnderwriter()) Then
            Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            Try
                If Not IsNothing(selectedAlarm) AndAlso Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                    listAlarms = HttpContext.Current.Session("TabAlarms")
                    Dim currentAlarm = listAlarms.FindLast(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product)
                    'If IsNothing(currentAlarm.RuleRestrictions) Then
                    If (Not IsNothing(HttpContext.Current.Session("TabOriginalAlarms"))) Then
                        Dim listAlarmBuffer As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = HttpContext.Current.Session("TabOriginalAlarms")
                        Dim alarmBuffer = listAlarmBuffer.Find(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product)
                        If (Not IsNothing(alarmBuffer)) Then
                            selectedAlarm.IsDirty = False
                            selectedAlarm.IsNew = False
                            selectedAlarm.IsDeletedMark = True
                            selectedAlarm.RuleRestrictions = currentAlarm.RuleRestrictions
                            If (Not IsNothing(selectedAlarm.RuleRestrictions)) Then
                                For Each ruleRestriction In selectedAlarm.RuleRestrictions
                                    ruleRestriction.IsDirty = False
                                    ruleRestriction.IsNew = False
                                    ruleRestriction.IsDeletedMark = True
                                Next
                            End If
                            listAlarms.RemoveAll(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product)
                            listAlarms.Add(selectedAlarm)
                        Else
                            listAlarms.RemoveAll(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product)
                        End If
                    Else
                        listAlarms.RemoveAll(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product)
                    End If
                    HttpContext.Current.Session("TabAlarms") = listAlarms
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
        End If
        Return ""
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UpdateAlarm(newAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms) As String
        If (isUnderwriter()) Then
            Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            Try
                If Not IsNothing(newAlarm) AndAlso Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                    listAlarms = HttpContext.Current.Session("TabAlarms")
                    Dim currentAlarm = listAlarms.FindLast(Function(x) x.AlarmType = newAlarm.AlarmType AndAlso x.Product = newAlarm.Product)
                    If (Not IsNothing(currentAlarm) AndAlso Not IsNothing(currentAlarm.RuleRestrictions)) Then
                        newAlarm.RuleRestrictions = currentAlarm.RuleRestrictions
                    End If
                    'If IsNothing(currentAlarm.RuleRestrictions) Then                  
                    If (Not IsNothing(HttpContext.Current.Session("TabOriginalAlarms"))) Then
                        Dim listAlarmBuffer As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = HttpContext.Current.Session("TabOriginalAlarms")
                        Dim alarmBuffer = listAlarmBuffer.Find(Function(x) x.AlarmType = newAlarm.AlarmType AndAlso x.Product = newAlarm.Product)
                        If (Not IsNothing(alarmBuffer)) Then
                            newAlarm.IsDirty = True
                            newAlarm.IsNew = False
                            newAlarm.IsDeletedMark = False
                        Else
                        End If
                    End If
                    listAlarms.RemoveAll(Function(x) x.AlarmType = newAlarm.AlarmType AndAlso x.Product = newAlarm.Product)
                    listAlarms.Add(newAlarm)
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
        End If
        Return ""
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RetrieveTabUnderwritingRuleAlarm(
                               selectedAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms,
                               languageId As Integer) As List(Of Object)
        Dim vret As New List(Of Object)
        Dim listAlarmBuffer As List(Of InMotionGIT.Underwriting.Contracts.RuleAlarms)
        If (isUnderwriter()) Then
            Dim listAlarm As New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            If Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                listAlarm = HttpContext.Current.Session("TabAlarms")
                listAlarmBuffer = listAlarm.FindAll(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product AndAlso x.IsDeletedMark = False).ToList()
                If (Not IsNothing(listAlarmBuffer)) Then
                    For Each ruleAlarm In listAlarmBuffer
                        If (ruleAlarm.Product = selectedAlarm.Product AndAlso Not IsNothing(ruleAlarm.RuleRestrictions)) Then
                            For Each ruleRestriction In ruleAlarm.RuleRestrictions
                                If (Not (ruleRestriction.IsDeletedMark)) Then
                                    vret.Add(ruleRestriction)
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        End If
        If (IsNothing(vret)) Then
            Return vret
        Else
            Return vret
        End If

    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AddRestriction(selectedAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms,
                               dataRestriction As InMotionGIT.Underwriting.Contracts.RuleRestrictions) As String
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"

            Dim listAlarms As New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            Dim alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms
            Try
                If Not IsNothing(dataRestriction) Then
                    If Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                        listAlarms = HttpContext.Current.Session("TabAlarms")
                        alarm = listAlarms.FirstOrDefault(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product AndAlso x.IsDeletedMark = False)
                        If Not IsNothing(alarm.RuleRestrictions) Then
                            Select Case dataRestriction.RestrictionType
                                Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.Exclusion
                                    Select Case dataRestriction.ExclusionType
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeCoverage
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ProductModule = dataRestriction.ProductModule And x.CoverageCode = dataRestriction.CoverageCode And x.IsDeletedMark = False).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairment
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ImpairmentCode = dataRestriction.ImpairmentCode And x.IsDeletedMark = False).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairmentByTariff
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ImpairmentCode = dataRestriction.ImpairmentCode And x.RatingTable = dataRestriction.RatingTable And x.IsDeletedMark = False).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairmentUnderAnSpecificCoverage
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ImpairmentCode = dataRestriction.ImpairmentCode And x.CoverageCode = dataRestriction.CoverageCode And x.ProductModule = dataRestriction.ProductModule And x.IsDeletedMark = False).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                                    End Select
                                Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.FlatExtraPremium
                                    If alarm.RuleRestrictions.FindAll(Function(x) x.Discountorextrapremiumcode = dataRestriction.Discountorextrapremiumcode).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                                Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.MaximumSumInsured
                                    If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ProductModule = dataRestriction.ProductModule And x.CoverageCode = dataRestriction.CoverageCode And x.CurrencyCode = dataRestriction.CurrencyCode And x.IsDeletedMark = False).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                            End Select
                            Select Case selectedAlarm.AlarmType
                                Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumAlarmType.InsuredAmountLimit
                                    If alarm.RuleRestrictions.FindAll(Function(x) x.AlarmType = selectedAlarm.AlarmType And x.Product = selectedAlarm.Product And x.IsDeletedMark = False).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                                    'Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumAlarmType.AddExclusion
                                    '    If alarm.RuleRestrictions.FindAll(Function(x) x.AlarmType = selectedAlarm.AlarmType And x.Product = selectedAlarm.Product And x.IsDeletedMark = False).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                                    'Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumAlarmType.AddFlatExtraPremium
                                    '    If alarm.RuleRestrictions.FindAll(Function(x) x.AlarmType = selectedAlarm.AlarmType And x.Product = selectedAlarm.Product And x.IsDeletedMark = False).Count > 0 Then Return HttpContext.GetLocalResourceObject(urlRelativePath, "ExistRestriction")
                            End Select
                        Else
                            alarm.RuleRestrictions = New InMotionGIT.Underwriting.Contracts.RuleRestrictionsCollection
                        End If
                        dataRestriction.Product = selectedAlarm.Product
                        dataRestriction.AlarmType = selectedAlarm.AlarmType
                        dataRestriction.IsNew = True
                        dataRestriction.IsDeletedMark = False
                        dataRestriction.IsDirty = False
                        dataRestriction.RestrictionId = GetLastIdRestriccion(Nothing, listAlarms)
                        alarm.RuleRestrictions.Add(dataRestriction)
                    End If
                End If
                Return String.Empty
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
        Else
            Return String.Empty
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function EditRestriction(selectedAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms, dataRestriction As InMotionGIT.Underwriting.Contracts.RuleRestrictions) As String
        If (isUnderwriter()) Then
            Dim listAlarms As New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
            Dim alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms
            Try
                If Not IsNothing(dataRestriction) Then
                    If Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                        listAlarms = HttpContext.Current.Session("TabAlarms")
                        alarm = listAlarms.FindLast(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product)
                        Select Case dataRestriction.RestrictionType
                            Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.Exclusion
                                Select Case dataRestriction.ExclusionType
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeCoverage
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ProductModule = dataRestriction.ProductModule And x.CoverageCode = dataRestriction.CoverageCode And x.RestrictionId <> dataRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairment
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ImpairmentCode = dataRestriction.ImpairmentCode And x.RatingTable.IsEmpty And x.ProductModule.IsEmpty And x.CoverageCode.IsEmpty And x.RestrictionId <> dataRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairmentByTariff
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ImpairmentCode = dataRestriction.ImpairmentCode And x.RatingTable = dataRestriction.RatingTable And x.RestrictionId <> dataRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairmentUnderAnSpecificCoverage
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ImpairmentCode = dataRestriction.ImpairmentCode And x.CoverageCode = dataRestriction.CoverageCode And x.ProductModule = dataRestriction.ProductModule And x.RestrictionId <> dataRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                End Select
                            Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.FlatExtraPremium
                                If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.Discountorextrapremiumcode = dataRestriction.Discountorextrapremiumcode And x.DiscountorExtraPremiumType = dataRestriction.DiscountorExtraPremiumType And x.RestrictionId <> dataRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                            Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.MaximumSumInsured
                                If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = dataRestriction.ExclusionType And x.ProductModule = dataRestriction.ProductModule And x.CoverageCode = dataRestriction.CoverageCode And x.CurrencyCode = dataRestriction.CurrencyCode And x.RestrictionId <> dataRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                        End Select
                        dataRestriction.Product = selectedAlarm.Product
                        dataRestriction.AlarmType = selectedAlarm.AlarmType
                        If (Not IsNothing(HttpContext.Current.Session("TabOriginalAlarms"))) Then
                            Dim listAlarmBuffer As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = HttpContext.Current.Session("TabOriginalAlarms")
                            Dim alarmBuffer = listAlarmBuffer.Find(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product AndAlso x.IsDeletedMark = False)
                            If (Not IsNothing(alarmBuffer) AndAlso Not IsNothing(alarmBuffer.RuleRestrictions)) Then
                                Dim listRestrictionBuffer As InMotionGIT.Underwriting.Contracts.RuleRestrictionsCollection = alarmBuffer.RuleRestrictions
                                Dim restrictionBuffer = listRestrictionBuffer.Find(Function(x) x.RestrictionId = dataRestriction.RestrictionId)

                                If (Not IsNothing(restrictionBuffer)) Then
                                    dataRestriction.IsDirty = True
                                    dataRestriction.IsNew = False
                                    dataRestriction.IsDeletedMark = False
                                End If
                            End If
                        End If
                        alarm.RuleRestrictions.RemoveAll(Function(x) x.RestrictionId = dataRestriction.RestrictionId)
                        Dim dataRestrictionBuffer As InMotionGIT.Underwriting.Contracts.RuleRestrictions = Nothing
                        If (dataRestriction.RestrictionType = InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.Exclusion _
                             AndAlso (dataRestriction.FlatExtraPremium = 0 OrElse dataRestriction.ExtraPremiumPercentage = 0)) Then
                            dataRestrictionBuffer = dataRestriction.Clone()
                            dataRestriction.IsDirty = False
                            dataRestriction.IsNew = False
                            dataRestriction.IsDeletedMark = True
                        End If
                        alarm.RuleRestrictions.Add(dataRestriction)
                        If (Not IsNothing(dataRestrictionBuffer)) Then
                            dataRestrictionBuffer.IsDirty = False
                            dataRestrictionBuffer.IsNew = True
                            dataRestrictionBuffer.IsDeletedMark = False
                            alarm.RuleRestrictions.Add(dataRestrictionBuffer)
                        End If
                    End If
                End If
                Return String.Empty
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
        Else
            Return String.Empty
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function DeleteRestriction(selectedAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms,
                           dataRestriction As InMotionGIT.Underwriting.Contracts.RuleRestrictions) As String
        If (isUnderwriter()) Then
            Dim listAlarms As New List(Of InMotionGIT.Underwriting.Contracts.RuleAlarms)
            Dim listRestrictions As New InMotionGIT.Underwriting.Contracts.RuleRestrictionsCollection
            Dim alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms
            Dim existRestriction = False
            Try
                If Not IsNothing(dataRestriction.AlarmType) AndAlso Not IsNothing(dataRestriction.RestrictionId) Then
                    If Not IsNothing(HttpContext.Current.Session("TabAlarms")) Then
                        listAlarms = HttpContext.Current.Session("TabAlarms")
                        alarm = listAlarms.FirstOrDefault(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product)

                        If (Not IsNothing(HttpContext.Current.Session("TabOriginalAlarms"))) Then
                            Dim listAlarmBuffer As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = HttpContext.Current.Session("TabOriginalAlarms")
                            Dim alarmBuffer = listAlarmBuffer.Find(Function(x) x.AlarmType = selectedAlarm.AlarmType AndAlso x.Product = selectedAlarm.Product AndAlso x.IsDeletedMark = False)
                            If (Not IsNothing(alarmBuffer) AndAlso Not IsNothing(alarmBuffer.RuleRestrictions)) Then
                                Dim listRestrictionBuffer As InMotionGIT.Underwriting.Contracts.RuleRestrictionsCollection = alarmBuffer.RuleRestrictions
                                Dim restrictionBuffer = listRestrictionBuffer.Find(Function(x) x.RestrictionId = dataRestriction.RestrictionId)
                                If (Not IsNothing(restrictionBuffer)) Then
                                    dataRestriction.IsDirty = False
                                    dataRestriction.IsNew = False
                                    dataRestriction.IsDeletedMark = True
                                    alarm.RuleRestrictions.RemoveAll(Function(x) x.RestrictionId = dataRestriction.RestrictionId)
                                    alarm.RuleRestrictions.Add(dataRestriction)
                                    existRestriction = True
                                End If
                            End If
                        End If
                        If (Not existRestriction) Then
                            alarm.RuleRestrictions.RemoveAll(Function(x) x.RestrictionId = dataRestriction.RestrictionId)
                        End If
                    End If
                End If
                If (Not IsNothing(listAlarms)) Then
                    Return ""
                Else
                    Return ""
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
        End If
    End Function

    Private Shared Function GetLastIdRestriccion(rules As InMotionGIT.Underwriting.Contracts.UnderwritingRuleCollection, listAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection) As Integer
        Dim result As Integer = 0

        For Each alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms In listAlarm
            'For i As Integer = 0 To listAlarm.Count - 1
            If Not IsNothing(alarm.RuleRestrictions) AndAlso alarm.RuleRestrictions.Count > 0 Then
                result = alarm.RuleRestrictions.Max(Function(x) x.RestrictionId) + 1
            End If
        Next
        'If (IsNothing(rules) OrElse rules.Count = 0) Then
        Return result

        'Else
        '    If rules.Max(Function(x) x.UnderRuleId) > result Then
        '        Return rules.Max(Function(x) x.UnderRuleId) + 1
        '    Else
        '        Return result + 1
        'End If
        'End If
    End Function

    '*********************************** Fin Ajustes ***************************************'

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function DeleteTabUnderwritingRuleAlarm(selectedAlarm As InMotionGIT.Underwriting.Contracts.RuleAlarms) As Integer
        If (isUnderwriter()) Then
            Dim currentTUWR As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule = DirectCast(HttpContext.Current.Session("currentTabUndewritingRule"), InMotionGIT.Underwriting.Contracts.TabUnderwritingRule)
            Dim dbTUWR As New InMotionGIT.Underwriting.Contracts.TabUnderwritingRule

            With currentTUWR
                dbTUWR = InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.RetrieveTabUnderwritingRuleInstanceByRuleId(.UnderwritingRuleId, .EffectiveDate, HttpContext.Current.Session("LanguageID"), TokenHelper.GetValidToken())
            End With

            If IsNothing(dbTUWR.RuleAlarms) Then dbTUWR.RuleAlarms = New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection()

            Dim ra = dbTUWR.RuleAlarms.Where(Function(x) x.AlarmType = selectedAlarm.AlarmType And
                                               x.Decision = selectedAlarm.Decision And
                                               x.Product = selectedAlarm.Product And
                                               x.Stage = selectedAlarm.Stage And
                                               x.UpdateOnlyAssociatedRisk = selectedAlarm.UpdateOnlyAssociatedRisk And
                                               x.Status = selectedAlarm.Status).First()
            'If ra.RuleRestrictions IsNot Nothing AndAlso ra.RuleRestrictions.Count > 0 Then
            '    ra.RuleRestrictions.ForEach(Sub(x)
            '                                    x.IsDeletedMark = True
            '                                    x.IsDirty = False
            '                                End Sub)
            'End If
            ra.IsNew = False
            ra.IsDeletedMark = True
            ra.IsDirty = False

            Dim userContext As New InMotionGIT.Membership.Providers.FrontOfficeMembershipUser()

            Return InMotionGIT.Underwriting.Proxy.Helpers.TabUnderwritingRule.SynchronizeWithAccessToken(dbTUWR, userContext.UserID, Date.Today(), TokenHelper.GetValidToken)
        Else
            Return Nothing
        End If
    End Function



    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmMessages() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
            Dim arr() As String = {"msgreqAlarmType", "msgreqDecision", "msgreqProduct", "msgreqStage", "msgErrAlarmExist", "msgFillAlarm", "msgFillExclusion", "msgFillMaxInsuredSum", "msgFillDescoexprem", "msgSuccess", "msgSuccessEdit", "msgSuccessDelete", "msgFailed", "msgInfoRestriction", "msgSuccessfull", "msgSelectBranch", "msgDeletedDenied"}

            For Each i In arr
                dict.Add(i, HttpContext.GetLocalResourceObject(urlRelativePath, i))
            Next
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmExclusionsMessages() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
            Dim arr() As String = {"msgreqExclusionType", "msgreqRatingTable", "msgreqProductModule", "msgreqRoleCode", "msgreqCoverageCode", "msgreqImpairmentCode", "msgreqExclusionPeriodType", "msgreqPeriod"}

            For Each i In arr
                dict.Add(i, HttpContext.GetLocalResourceObject(urlRelativePath, i))
            Next
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmDiscoexpremMessages() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
            Dim arr() As String = {"msgreqDiscountorextrapremiumcode", "msgreqDiscountorExtraPremiumType", "msgreqExclusionPeriodTypeDisco", "msgreqExtraPremiumPercentage", "msgreqPeriod"}

            For Each i In arr
                dict.Add(i, HttpContext.GetLocalResourceObject(urlRelativePath, i))
            Next
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmMaxInsuredSumMessages() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
            Dim arr() As String = {"msgreqProductModule", "msgreqCoverageCode", "msgreqMaximumInsuredAmount"}

            For Each i In arr
                dict.Add(i, HttpContext.GetLocalResourceObject(urlRelativePath, i))
            Next
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTabUnderwritingRulesByIdRule(IdRule As Integer, effectDate As Date, languageID As Integer) As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule
        Dim retval As New InMotionGIT.Underwriting.Contracts.TabUnderwritingRule
        If (isUnderwriter()) Then
            Try
                If IdRule > 0 Then
                    retval = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRuleLookupById(IdRule, effectDate, languageID, True, TokenHelper.GetValidToken())
                End If
                HttpContext.Current.Session("currentTabUndewritingRule") = retval
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return retval
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmToolTips() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim arr() As String = {
                "ttipProduct", "ttipAlarmType", "ttipStage", "ttipStatus",
                "ttipDecision", "ttipDecisionComplement"
            }
            dict = GetResourceValues(arr)
        End If
        Return dict
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmDiscoExpremToolTips() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim arr() As String = {
                "ttipDiscountorextrapremiumcode", "ttipDiscountorExtraPremiumType", "ttipExtraPremiumPercentage",
                "ttipFlatExtraPremium", "ttipCurrencyCode", "ttipXPremiumDiscountOnlyInsured", "ttipTypeOfUnit", "ttipExclusionPeriodType",
                "ttipDOfFlatExtraPremiumDays", "ttipDOfFlatExtraPremiumMonths", "ttipDOfFlatExtraPremiumYears"
            }
            dict = GetResourceValues(arr)
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmExclusionToolTips() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim arr() As String = {
                "ttipExclusionType", "ttipRatingTable", "ttipProductModule",
                "ttipCoverageCode", "ttipImpairmentCode",
                "ttipCause", "ttipExclusionPeriodType_ex", "ttipWaitingPeriodDays",
                "ttipWaitingPeriodMonths", "ttipWaitingPeriodYears"
            }
            dict = GetResourceValues(arr)
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmMaxInsuredSumToolTips() As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim arr() As String = {
                "ttipProductModule_mis", "ttipCoverageCode_mis", "ttipMaximumInsuredAmount",
                "ttipCurrencyCode_mis"}
            dict = GetResourceValues(arr)
        End If
        Return dict
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetIllnessDescription(illness As String) As String
        Dim description As String = ""
        If (isUnderwriter()) Then
            description = InMotionGIT.Underwriting.Proxy.Lookups.IllnessTypeDescription(illness, HttpContext.Current.Session("LanguageID"), TokenHelper.GetValidToken())
        End If
        Return description
    End Function

    Protected Shared Function GetResourceValues(arr() As String) As Dictionary(Of String, String) ' List(Of String)
        Dim dict As New Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Dim urlRelativePath As String = "/Underwriting/Services/TabUnderwritingRules.aspx"
            For Each i In arr
                dict.Add(i, HttpContext.GetLocalResourceObject(urlRelativePath, i))
            Next
        End If
        Return dict
    End Function
End Class
