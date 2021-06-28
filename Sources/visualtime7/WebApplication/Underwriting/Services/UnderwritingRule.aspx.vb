Imports System.Web.Services
Imports System.Web.Script.Services
Imports InMotionGIT.Seguridad.Proxy
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.Modelo
Imports System.Configuration
Imports System.Web
Imports System
Imports System.Globalization

Partial Class UnderwritingAsync_Services_UnderwritingRule
    Inherits GIT.Core.PageBase

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllUnderwritingRules(caseId As Integer, requirementId As Integer) As List(Of InMotionGIT.Underwriting.Contracts.UnderwritingRule)
        Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.UnderwritingRule)
        If (isUnderwriter()) Then
            Try
                listado = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.SelectAll(caseId, requirementId, HttpContext.Current.Session("LanguageID"), TokenHelper.GetValidToken)
                For Each Rule As InMotionGIT.Underwriting.Contracts.UnderwritingRule In listado
                    Rule.EncodedExplanation = HttpUtility.JavaScriptStringEncode(Rule.Explanation)
                Next
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return listado
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUnderwritingRulesByUnderRuleId(caseId As Integer, requirementId As Integer, UnderRuleId As String) As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule
        Dim tabUnderwritingRule As New InMotionGIT.Underwriting.Contracts.TabUnderwritingRule
        If (isUnderwriter()) Then
            Try
                tabUnderwritingRule = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.SelectByUnderRuleId(caseId, requirementId, UnderRuleId, (HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.StorageRuleAlarmInstance(caseId, tabUnderwritingRule.RuleAlarms, TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return tabUnderwritingRule
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub RemoveUnderwritingRule(caseId As Integer, requirementId As Integer, UnderRuleId As String, consequenceId As Integer)
        If (isUnderwriter()) Then
            Try
                If Not IsNothing(UnderRuleId) Then
                    Dim uwCaseInstance = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, TokenHelper.GetValidToken, True)
                    InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.DeleteOnCache(caseId, requirementId, UnderRuleId, True, HttpContext.Current.Session("UserId"))
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    '<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    '   Public Shared Sub AddUnderwritingRule(newUnderwritingRule As InMotionGIT.Underwriting.Contracts.UnderwritingRule)
    '       If (isUnderwriter()) Then
    '           Try
    '               If Not IsNothing(newUnderwritingRule) Then
    '				InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.InsertOnCache(newUnderwritingRule, HttpContext.Current.Session("LanguageID"), HttpContext.Current.Session("UserId"))
    '			End If
    '           Catch ex As Exception
    '               ResponseHelper.ErrorToClient(ex, HttpContext.Current)
    '           End Try
    '       End If
    '   End Sub

    '<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    '   Public Shared Sub EditUnderwritingRule(newUnderwritingRule As InMotionGIT.Underwriting.Contracts.UnderwritingRule,
    '                                          originalQuestionId As Integer,
    '                                          originalUnderRuleId As Integer,
    '                                          originalUnderwritingRuleId As Integer)
    '       If (isUnderwriter()) Then
    '           Try
    '               If Not IsNothing(newUnderwritingRule) Then InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.UpdateOnCache(newUnderwritingRule, originalQuestionId, originalUnderRuleId, originalUnderwritingRuleId)
    '           Catch ex As Exception
    '               ResponseHelper.ErrorToClient(ex, HttpContext.Current)
    '           End Try
    '       End If
    '   End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValues() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return New List(Of String) From {
                Resources._requirementSuscriptionRules.UnderRuleId,
                Resources._requirementSuscriptionRules.RuleDescription,
                Resources._requirementSuscriptionRules.QuestionId,
                Resources._requirementSuscriptionRules.QuestionIdDescription,
                Resources._requirementSuscriptionRules.UnderwritingArea,
                Resources._requirementSuscriptionRules.UnderwritingAreaDescription,
                Resources._requirementSuscriptionRules.Explanation,
                Resources._requirementSuscriptionRules.IsManualRule,
                Resources._requirementSuscriptionRules.Answer,
                Resources._requirementSuscriptionRules.Answer
            }
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUnderwriter() As String
        Dim managerClient As New InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient
        Dim underwriters As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim underwriter As String = String.Empty
        If (isUnderwriter()) Then
            If IsNothing(HttpContext.Current.Session("suscriptores")) Then HttpContext.Current.Session("suscriptores") = managerClient.UserLkp()
            underwriters = HttpContext.Current.Session("suscriptores")
            If Not IsNothing(underwriters) Then underwriter = String.Format("{0} | {1}", HttpContext.Current.Session("UserId").ToString, (From s In underwriters Where s.Code = HttpContext.Current.Session("UserId") Select s.Description).FirstOrDefault())
        End If
        Return underwriter
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderAlarmValues(editMode As Boolean) As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            If (editMode) Then
                Return New List(Of String) From {
                    Resources._addRule.AlarmType,
                    Resources._addRule.AlarmTypeDescriptionUpdate
                }
            Else
                Return New List(Of String) From {
                    Resources._addRule.AlarmType,
                    Resources._addRule.AlarmTypeDescription
                }
            End If
        Else
            Return Nothing
        End If
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AddAlarm(caseId As Integer, AlarmType As String, AlarmTypeDescription As String) As String
        Dim result As String = String.Empty
        If (isUnderwriter()) Then
            Try
                If Not IsNothing(AlarmType) AndAlso caseId > 0 Then
                    Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RetrieveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
                    Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, TokenHelper.GetValidToken)
                    If Not IsNothing(listAlarms) Then
                        If (Not IsNothing((From list In listAlarms Where list.AlarmType = AlarmType Select list).SingleOrDefault)) Then Return "La alarma ya existe"
                    Else
                        listAlarms = New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
                    End If
                    Dim alarm As New InMotionGIT.Underwriting.Contracts.RuleAlarms
                    alarm.AlarmType = AlarmType
                    alarm.AlarmTypeDescription = AlarmTypeDescription
                    alarm.Product = uwcase.Product
                    alarm.LineOfBusiness = uwcase.LineOfBusiness
                    listAlarms.Add(alarm)
                    InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.StorageRuleAlarmInstance(caseId, listAlarms, TokenHelper.GetValidToken)
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return ex.Message
            End Try
        Else
            result = "No es suscriptor"
        End If
        Return result
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub RemoveAlarm(caseId As Integer, requirementId As Integer, alarmType As String)
        If (isUnderwriter()) Then
            Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RetrieveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
            Try
                If Not IsNothing(alarmType) AndAlso caseId > 0 Then
                    Dim currentAlarm = listAlarms.Find(Function(x) x.AlarmType = alarmType)
                    If IsNothing(currentAlarm.RuleRestrictions) Then
                        listAlarms.RemoveAll(Function(x) x.AlarmType = alarmType)
                    Else
                        For i As Integer = 0 To currentAlarm.RuleRestrictions.Count - 1
                            InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.DeleteOnCache(caseId, requirementId, currentAlarm.RuleRestrictions(i).RestrictionId, False, TokenHelper.GetValidToken, 0)
                        Next
                        listAlarms.RemoveAll(Function(x) x.AlarmType = alarmType)
                    End If
                    InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.StorageRuleAlarmInstance(caseId, listAlarms, TokenHelper.GetValidToken)
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllAlarm(caseId As Integer) As List(Of InMotionGIT.Underwriting.Contracts.RuleAlarms)
        Dim listAlarms As New List(Of InMotionGIT.Underwriting.Contracts.RuleAlarms)
        If (isUnderwriter()) Then
            Try
                listAlarms = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RetrieveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return listAlarms
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub CleanAllAlarm(caseId As Integer)
        If (isUnderwriter()) Then
            Try
                InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RemoveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderRestrictionValues(AlarmType As String) As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Select Case (AlarmType)
                Case 5
                    Return New List(Of String) From {
                        Resources._addRule.RestrictionId,
                        Resources._addRule.Discountorextrapremiumcode,
                        Resources._addRule.DiscountOrExtraPremiumDescription,
                        Resources._addRule.DiscountorExtraPremiumType,
                        Resources._addRule.DiscountorExtraPremiumTypeDescription,
                        Resources._addRule.CurrencyCode,
                        Resources._addRule.CurrencyCodeDescription,
                        Resources._addRule.ExtraPremiumPercentage,
                        Resources._addRule.FlatExtraPremium,
                        Resources._addRule.XPremiumDiscountOnlyInsured,
                        Resources._addRule.ProductModule,
                        Resources._addRule.ProductModuleDescription,
                        Resources._addRule.CoverageCode,
                        Resources._addRule.CoverageDescription,
                        Resources._addRule.ExclusionPeriodType,
                        Resources._addRule.ExclusionPeriodTypeDescription,
                        Resources._addRule.DOfFlatExtraPremiumDays,
                        Resources._addRule.DOfFlatExtraPremiumMonths,
                        Resources._addRule.DOfFlatExtraPremiumYears,
                        Resources._addRule.IsNew
                    }
                Case 6
                    Return New List(Of String) From {
                        Resources._addRule.RestrictionId,
                        Resources._addRule.ExclusionType,
                        Resources._addRule.ExclusionTypeDescription,
                        Resources._addRule.RatingTable,
                        Resources._addRule.RatingTableDescription,
                        Resources._addRule.ProductModule,
                        Resources._addRule.ProductModuleDescription,
                        Resources._addRule.CoverageCode,
                        Resources._addRule.CoverageDescription,
                        Resources._addRule.ExclusionClientID,
                        Resources._addRule.ExclusionClientName,
                        Resources._addRule.ImpairmentCode,
                        Resources._addRule.ImpairmentCodeDescription,
                        Resources._addRule.Cause,
                        Resources._addRule.CauseDescription,
                        Resources._addRule.ExclusionPeriodType,
                        Resources._addRule.ExclusionPeriodTypeDescription,
                        Resources._addRule.DOfFlatExtraPremiumDays,
                        Resources._addRule.DOfFlatExtraPremiumMonths,
                        Resources._addRule.DOfFlatExtraPremiumYears,
                        Resources._addRule.IsNew
                    }
                Case 8
                    Return New List(Of String) From {
                        Resources._addRule.RestrictionId,
                        Resources._addRule.ProductModule,
                        Resources._addRule.ProductModuleDescription,
                        Resources._addRule.CoverageCode,
                        Resources._addRule.CoverageDescription,
                        Resources._addRule.MaximumInsuredAmount,
                        Resources._addRule.IsNew
                    }
                Case Else
                    Return New List(Of String) From {}
            End Select
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderExclusionIllnessInsured() As String
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return Resources._addRule.ExclusionIllnessInsured
        Else
            Return Nothing
        End If
    End Function


    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllRestriction(caseId As Integer, AlarmType As String) As List(Of InMotionGIT.Underwriting.Contracts.RuleRestrictions)
        If (isUnderwriter()) Then
            Dim listRestriction As New List(Of InMotionGIT.Underwriting.Contracts.RuleRestrictions)
            Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RetrieveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
            Try
                If Not IsNothing(listAlarms) Then
                    listRestriction = listAlarms.Find(Function(x) x.AlarmType = AlarmType).RuleRestrictions
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
            If (IsNothing(listRestriction)) Then
                Return listRestriction
            Else
                Return (From list In listRestriction Select list Order By list.RestrictionId Ascending).ToList()
            End If
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AddRestriction(caseId As Integer, requirementId As Integer, newRestriction As InMotionGIT.Underwriting.Contracts.RuleRestrictions) As String
        If (isUnderwriter()) Then
            Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RetrieveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
            Dim alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, TokenHelper.GetValidToken)
            Dim rules As InMotionGIT.Underwriting.Contracts.UnderwritingRuleCollection = uwcase.Requirements.Find(Function(x) x.RequirementID = requirementId).UnderwritingRules
            Try
                If Not IsNothing(newRestriction) Then
                    If Not IsNothing(listAlarms) Then
                        alarm = listAlarms.FirstOrDefault(Function(x) x.AlarmType = newRestriction.AlarmType)
                        If Not IsNothing(alarm.RuleRestrictions) Then
                            Select Case newRestriction.RestrictionType
                                Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.Exclusion
                                    Select Case newRestriction.ExclusionType
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeCoverage
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = newRestriction.ExclusionType And x.ProductModule = newRestriction.ProductModule And x.CoverageCode = newRestriction.CoverageCode).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairment
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = newRestriction.ExclusionType And x.ImpairmentCode = newRestriction.ImpairmentCode And x.RatingTable.IsEmpty And x.ProductModule.IsEmpty And x.CoverageCode.IsEmpty).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairmentByTariff
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = newRestriction.ExclusionType And x.ImpairmentCode = newRestriction.ImpairmentCode And x.RatingTable = newRestriction.RatingTable).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairmentUnderAnSpecificCoverage
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = newRestriction.ExclusionType And x.ImpairmentCode = newRestriction.ImpairmentCode And x.CoverageCode = newRestriction.CoverageCode And x.ProductModule = newRestriction.ProductModule).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                        Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeInsured
                                            If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = newRestriction.ExclusionType And x.ExclusionClientID = newRestriction.ExclusionClientID).Count > 0 Then Return "Ya existe una restricción de este tipo"
                                    End Select
                                Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.FlatExtraPremium
                                    If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = newRestriction.ExclusionType And x.Discountorextrapremiumcode = newRestriction.Discountorextrapremiumcode And x.DiscountorExtraPremiumType = newRestriction.DiscountorExtraPremiumType).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.MaximumSumInsured
                                    If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = newRestriction.ExclusionType And x.ProductModule = newRestriction.ProductModule And x.CoverageCode = newRestriction.CoverageCode And x.CurrencyCode = newRestriction.CurrencyCode).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                            End Select
                        Else
                            alarm.RuleRestrictions = New InMotionGIT.Underwriting.Contracts.RuleRestrictionsCollection
                        End If
                        newRestriction.RestrictionId = GetLastIdRestriccion(rules, listAlarms)
                        alarm.RuleRestrictions.Add(newRestriction)
                        InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.StorageRuleAlarmInstance(caseId, listAlarms, TokenHelper.GetValidToken)
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

    Private Shared Function GetLastIdRestriccion(rules As InMotionGIT.Underwriting.Contracts.UnderwritingRuleCollection, listAlarm As List(Of InMotionGIT.Underwriting.Contracts.RuleAlarms)) As Integer
        Dim result As Integer = 0
        For i As Integer = 0 To listAlarm.Count - 1
            If Not IsNothing(listAlarm(i).RuleRestrictions) AndAlso listAlarm(i).RuleRestrictions.Count > 0 Then
                If result < listAlarm(i).RuleRestrictions.Max(Function(x) x.RestrictionId) Then
                    result = listAlarm(i).RuleRestrictions.Max(Function(x) x.RestrictionId)
                End If
            End If
        Next
        If (IsNothing(rules) OrElse rules.Count = 0) Then
            Return result + 1
        Else
            If rules.Max(Function(x) x.UnderRuleId) > result Then
                Return rules.Max(Function(x) x.UnderRuleId) + 1
            Else
                Return result + 1
            End If
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function EditRestriction(caseId As Integer, ediRestriction As InMotionGIT.Underwriting.Contracts.RuleRestrictions) As String
        If (isUnderwriter()) Then
            Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RetrieveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
            Dim alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms
            Try
                If Not IsNothing(ediRestriction) Then
                    If Not IsNothing(listAlarms) Then
                        ediRestriction.IsDirty = True
                        alarm = listAlarms.FirstOrDefault(Function(x) x.AlarmType = ediRestriction.AlarmType)
                        Select Case ediRestriction.RestrictionType
                            Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.Exclusion
                                Select Case ediRestriction.ExclusionType
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeCoverage
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = ediRestriction.ExclusionType And x.ProductModule = ediRestriction.ProductModule And x.CoverageCode = ediRestriction.CoverageCode And x.RestrictionId <> ediRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairment
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = ediRestriction.ExclusionType And x.ImpairmentCode = ediRestriction.ImpairmentCode And x.RatingTable.IsEmpty And x.ProductModule.IsEmpty And x.CoverageCode.IsEmpty And x.RestrictionId <> ediRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairmentByTariff
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = ediRestriction.ExclusionType And x.ImpairmentCode = ediRestriction.ImpairmentCode And x.RatingTable = ediRestriction.RatingTable And x.RestrictionId <> ediRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeImpairmentUnderAnSpecificCoverage
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = ediRestriction.ExclusionType And x.ImpairmentCode = ediRestriction.ImpairmentCode And x.CoverageCode = ediRestriction.CoverageCode And x.ProductModule = ediRestriction.ProductModule And x.RestrictionId <> ediRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                                    Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumExclusionType.ExcludeInsured
                                        If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = ediRestriction.ExclusionType And x.ExclusionClientID = ediRestriction.ExclusionClientID).Count > 0 Then Return "Ya existe una restricción de este tipo"
                                End Select
                            Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.FlatExtraPremium
                                If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = ediRestriction.ExclusionType And x.Discountorextrapremiumcode = ediRestriction.Discountorextrapremiumcode And x.DiscountorExtraPremiumType = ediRestriction.DiscountorExtraPremiumType And x.RestrictionId <> ediRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                            Case InMotionGIT.Underwriting.Contracts.Enumerations.EnumRestrictionType.MaximumSumInsured
                                If alarm.RuleRestrictions.FindAll(Function(x) x.ExclusionType = ediRestriction.ExclusionType And x.ProductModule = ediRestriction.ProductModule And x.CoverageCode = ediRestriction.CoverageCode And x.CurrencyCode = ediRestriction.CurrencyCode And x.RestrictionId <> ediRestriction.RestrictionId).Count > 0 Then Return "Ya existe una restriccion de este tipo"
                        End Select
                        alarm.RuleRestrictions.RemoveAll(Function(x) x.RestrictionId = ediRestriction.RestrictionId)
                        alarm.RuleRestrictions.Add(ediRestriction)
                        InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.StorageRuleAlarmInstance(caseId, listAlarms, TokenHelper.GetValidToken)
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
    Public Shared Sub DeleteRestriction(caseId As Integer, requirementId As Integer, alarmType As String, restrictionId As String)
        If (isUnderwriter()) Then
            Dim listAlarms As InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RetrieveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
            Dim alarm As InMotionGIT.Underwriting.Contracts.RuleAlarms
            Try
                If Not IsNothing(alarmType) AndAlso Not IsNothing(restrictionId) Then
                    If Not IsNothing(listAlarms) Then
                        alarm = listAlarms.FirstOrDefault(Function(x) x.AlarmType = alarmType)
                        InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.DeleteOnCache(caseId, requirementId, restrictionId, False, TokenHelper.GetValidToken)
                        alarm.RuleRestrictions.RemoveAll(Function(x) x.RestrictionId = restrictionId)
                        InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.StorageRuleAlarmInstance(caseId, listAlarms, TokenHelper.GetValidToken)
                    End If
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub SaveRule(newRule As InMotionGIT.Underwriting.Contracts.TabUnderwritingRule, clientId As String, caseId As Integer)
        If (isUnderwriter()) Then
            Dim listAlarms As List(Of InMotionGIT.Underwriting.Contracts.RuleAlarms) = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RetrieveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, TokenHelper.GetValidToken)
            Try
                If Not IsNothing(newRule) Then
                    If Not IsNothing(listAlarms) Then
                        listAlarms = (From alarms In listAlarms Where alarms.RuleRestrictions.IsNotEmpty Select alarms).ToList()
                        newRule.CreatorUserCode = HttpContext.Current.Session("UserId").ToString
                        newRule.UpdateUserCode = HttpContext.Current.Session("UserId").ToString
                        newRule.RuleAlarms = New InMotionGIT.Underwriting.Contracts.RuleAlarmsCollection
                        newRule.RuleAlarms.AddRange(listAlarms)
                        newRule.EffectiveDate = Today
                        newRule.LineOfBusiness = uwcase.LineOfBusiness
                        newRule.UnderwritingCaseType = uwcase.UnderwritingCaseType
                        newRule.IsManualRule = True
                        'newRule.TokenToDNE = TokenHelper.GetValidToken
                        newRule.UpdateOnlyAssociatedRisk = True
                        newRule.RecordStatus = InMotionGIT.Common.Contracts.Enumerations.EnumRecordStatus.Active
                        newRule.UnderwritingRuleStatus = 1
                        If newRule.ImpairmentCode = "0" Then newRule.ImpairmentCode = Nothing
                        uwcase = InMotionGIT.Underwriting.Proxy.ApplyRule.Apply(uwcase, newRule, clientId, Date.Now(), TokenHelper.GetValidToken, HttpContext.Current.Session("UserId")).UnderwritingCase
                        uwcase.NewVersionOfRiskInformation = True
                        uwcase.NewVersionDescription = "Información del riesgo modificada por el suscriptor."
                        InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(caseId, uwcase, TokenHelper.GetValidToken)
                        newRule.IsDirty = True
                        InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCaseLocked.Synchonize(uwcase, HttpContext.Current.Session("UserId"), Date.Now(), TokenHelper.GetValidToken, "", False)
                    End If
                End If
            Catch ex As Exception
                InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule.RemoveRuleAlarmInstance(caseId, TokenHelper.GetValidToken)
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CaseIsAllowed(caseId As Integer) As Boolean
        If (isUnderwriter()) Then
            Try
                Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, TokenHelper.GetValidToken())
                If (uwcase.Status <> 4 And uwcase.Status <> 7 And uwcase.Decision <> 5) Then
                    Return False
                End If
                Return True
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTextDescription() As Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Return New Dictionary(Of String, String) From {
                {"AddRule", Resources._requirementSuscriptionRules.AddRule},
                {"ViewRule", Resources._requirementSuscriptionRules.ViewRule},
                {"UpdateRule", Resources._requirementSuscriptionRules.UpdateRule},
                {"AppliedRule", Resources._requirementSuscriptionRules.AppliedRule},
                {"NoManualRule", Resources._requirementSuscriptionRules.NoManualRule},
                {"DeniedUpdate", Resources._requirementSuscriptionRules.DeniedUpdate},
                {"DeniedUnderwritingRule", Resources._requirementSuscriptionRules.DeniedUnderwritingRule}
            }
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetListValidationText() As Dictionary(Of String, String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo
            Return New Dictionary(Of String, String) From {
                {"validationTemporal", Resources._addRule.ValidationTemporal},
                {"validationDescription", Resources._addRule.ValidationDescription},
                {"validationPeriod", Resources._addRule.ValidationPeriod},
                {"validationFactor", Resources._addRule.ValidationFactor},
                {"validationExclusion", Resources._addRule.ValidationExclusion},
                {"validationIllnessCause", Resources._addRule.ValidationIllnessCause},
                {"validationModuleCoverage", Resources._addRule.ValidationModuleCoverage},
                {"validationModCovIllCause", Resources._addRule.ValidationModCovIllCause},
                {"validationIllCauseRate", Resources._addRule.ValidationIllCauseRate},
                {"validationFieldRequired", Resources._addRule.ValidationFieldRequired},
                {"validationAlarms", Resources._addRule.ValidationAlarms},
                {"validationRestrictions", Resources._addRule.ValidationRestrictions},
                {"validationModule", Resources._addRule.ValidationModule},
                {"validationCoverage", Resources._addRule.ValidationCoverage},
                {"validationInsured", Resources._addRule.ValidationInsured}
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
