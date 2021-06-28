Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Imports InMotionGIT.Seguridad.Proxy
Imports InMotionGIT.Product.Entity.Contracts
Imports System.Web
Imports System
Imports InMotionGIT.Product.Entity.Contracts.Lookups
Imports InMotionGIT.Underwriting.Contracts
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Policy.Entity.Contracts
Imports InMotionGIT.Underwriting.Proxy

Partial Class UnderwritingAsync_Services_LookUps
    Inherits GIT.Core.PageBase

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRequirementType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeLkp(HttpContext.Current.Session("LanguageID"), False).OrderBy(Function(x) x.Description).ToList
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRequirementTypeByType(requirementType As Integer) As InMotionGIT.Underwriting.Contracts.TabRequirementType
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeLkpByIdTyped(requirementType, HttpContext.Current.Session("LanguageID"))
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRequirementTypeByProduct() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Dim listOfRequirementsTypeAssignedToProduct As New List(Of InMotionGIT.Common.DataType.LookUpValue)
            If Not (IsNothing(uwcase)) Then
                'Se obtiene la lista de Requerimientos que esten asignados a un producto y ramo.
                listOfRequirementsTypeAssignedToProduct = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeByProductLkp(uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"), True)
                'en caso de que efectivamente esta lista contenga registros, se retorna la misma, en caso contrario se obtienen todos los tipos de requerimientos activos.
                If listOfRequirementsTypeAssignedToProduct.Count > 0 Then
                    listOfRequirementsTypeAssignedToProduct.Insert(0, New InMotionGIT.Common.DataType.LookUpValue("", ""))
                End If
            End If
            Return listOfRequirementsTypeAssignedToProduct
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAlarmType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.AlarmTypeLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUnderwritingAreaType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim listOfUnderwritingAreaTypes = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingAreaTypeLkp(HttpContext.Current.Session("LanguageID"), False)
            Return (From listUnder In listOfUnderwritingAreaTypes Where listUnder.Code <> String.Empty Select listUnder).OrderBy(Function(x) x.Description).ToList
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUnderwritingAreaTypeActive() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim listOfUnderwritingAreaTypes = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingAreaTypeActiveLkp(HttpContext.Current.Session("LanguageID"), False)
            Return (From listUnder In listOfUnderwritingAreaTypes Where listUnder.Code <> String.Empty Select listUnder).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetProcessType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim listOfProcessType = InMotionGIT.Underwriting.Proxy.Lookups.ProcessTypeLkp(HttpContext.Current.Session("LanguageID"), False)
            If listOfProcessType.Count > 0 AndAlso listOfProcessType.Item(0).Code <> "" Then
                listOfProcessType.Insert(0, New InMotionGIT.Common.DataType.LookUpValue("", ""))
            End If
            Return listOfProcessType
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRequirementStatusType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim listOfRequirementStatus = InMotionGIT.Underwriting.Proxy.Lookups.RequirementStatusTypeLkp(HttpContext.Current.Session("LanguageID"), False)
            If listOfRequirementStatus.Count > 0 AndAlso listOfRequirementStatus.Item(0).Code <> "" AndAlso listOfRequirementStatus.Item(0).Code <> "0" Then
                listOfRequirementStatus.Insert(0, New InMotionGIT.Common.DataType.LookUpValue("", ""))
            End If
            Return listOfRequirementStatus.OrderBy(Function(x) x.Description).ToList
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetPayableByType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim listOfPayables = InMotionGIT.Underwriting.Proxy.Lookups.PayableByTypeLkp(HttpContext.Current.Session("LanguageID"), False)
            If listOfPayables.Count > 0 AndAlso listOfPayables.Item(0).Code <> "" Then
                listOfPayables.Insert(0, New InMotionGIT.Common.DataType.LookUpValue("", ""))
            End If
            Return listOfPayables
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRolesTypes() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.RolesLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRejectionReasonTypes() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.RejectionReasonLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRiskClassType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.RiskClassTypeLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUnderwritingCaseStatusType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingCaseStatusTypeLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDecisionType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.DecisionTypeLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUnderwritingCaseType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingCaseTypeLkp(HttpContext.Current.Session("LanguageID"), False).OrderBy(Function(x) x.Description).ToList
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCurrencyLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.CurrencyLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetGenders() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.GenderLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetQuestionsFromRequirement() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim questionList As List(Of InMotionGIT.Common.DataType.LookUpValue) = InMotionGIT.Underwriting.Proxy.Lookups.QuestionsFromRequirementLkp(HttpContext.Current.Session("LanguageID"), False).OrderBy(Function(x) x.Description).ToList
            Return questionList
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetQuestionsFromRequirementFilterLkp(filter As String) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.QuestionsFromRequirementFilterLkp(HttpContext.Current.Session("LanguageID"), filter, 0, 100)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllProvidersInCaseLkp(caseId As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim listOfProvidersInCase = InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.SelectProviders(caseId, Convert.ToInt32(HttpContext.Current.Session("LanguageID")))
            If listOfProvidersInCase.Count > 0 AndAlso listOfProvidersInCase.Item(0).Code <> "" Then
                listOfProvidersInCase.Insert(0, New InMotionGIT.Common.DataType.LookUpValue("", ""))
            End If
            Return listOfProvidersInCase
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllUnderwritingRuleType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRuleByFilterLkp(HttpContext.Current.Session("LanguageID"), String.Empty, 1, 1000)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllUnderwritingRuleStatusType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRulesTypeLkp(HttpContext.Current.Session("LanguageID"), False).OrderBy(Function(x) x.Description).ToList
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllExclusionType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Underwriting.Proxy.Lookups.ExclusionTypeLkp(HttpContext.Current.Session("LanguageID"), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllExclusionPeriodType() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Underwriting.Proxy.Lookups.ExclusionPeriodTypeLkp(HttpContext.Current.Session("LanguageID"), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllIllnessTypeLkp(filter As String) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Underwriting.Proxy.Lookups.IllnessTypeByFilterLkp(HttpContext.Current.Session("LanguageID"), filter, 0, 100) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetIllnessTypeLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.IllnessTypeLkp(HttpContext.Current.Session("LanguageID"), True)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetIllnessByFilterAndProductLkp(filter As String) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Return (From u In InMotionGIT.Product.Proxy.Lookups.IllnessByFilterAndProductLkp(uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"), Date.Now, filter, 0, 100) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetIllnessByProductFilterLkp(LineOfBusiness As Integer, productCode As Integer, languageID As Integer, filter As String, regini As Integer, regend As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim tmpList As New List(Of InMotionGIT.Common.DataType.LookUpValue)
            Dim partialList As New List(Of InMotionGIT.Common.DataType.LookUpValue)
            Dim npage As Integer = regini
            Dim length As Integer = regend
            Dim fetchmore As Boolean = True

            While fetchmore
                partialList = New List(Of InMotionGIT.Common.DataType.LookUpValue)
                partialList = (From u In InMotionGIT.Product.Proxy.Lookups.IllnessByFilterAndProductLkp(LineOfBusiness, productCode, languageID, Date.Now, filter, npage, length) Select u Order By u.Description Ascending).ToList()
                If partialList.Count > 0 Then tmpList.AddRange(partialList) Else fetchmore = False
                npage = length + 1
                length += length
            End While

            Dim dctValues As New Dictionary(Of Integer, InMotionGIT.Common.DataType.LookUpValue)
            tmpList.ForEach(Sub(x) If Not dctValues.Keys.Contains(x.Code) Then dctValues.Add(x.Code, x))

            Dim retValue As New List(Of InMotionGIT.Common.DataType.LookUpValue)
            retValue.AddRange(dctValues.Select(Function(x) x.Value))

            Return retValue
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllCoverageByProductLkp() As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Return (From u In InMotionGIT.Product.Proxy.Lookups.CoverageByProductLkp(uwcase.LineOfBusiness, uwcase.Product, 0, HttpContext.Current.Session("LanguageID"), Date.Now(), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllDegreeLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Underwriting.Proxy.Lookups.DegreeLkp(HttpContext.Current.Session("LanguageID"), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllDiscountOrExtraPremiumLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Underwriting.Proxy.Lookups.DiscountOrExtraPremiumLkp(HttpContext.Current.Session("LanguageID")) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllDiscountOrExtraPremiumByProductLkp() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Return (From u In InMotionGIT.Product.Proxy.Lookups.DiscountExtraPremiumTaxByProductLkp(uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"), Date.Now(), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllDiscountOrExtraPremiumInfoByProductsLkp() As List(Of InMotionGIT.Product.Entity.Contracts.DiscountExtraPremiumTaxOfProduct)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Return (From u In InMotionGIT.Product.Proxy.Lookups.DiscountExtraPremiumTaxByProductLookup(uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"), Date.Now(), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllDiscoExPremByProductsLkp(LineOfBusiness As Integer, productCode As Integer, languageID As Integer) As List(Of Object)
        Dim vret As New List(Of Object)
        If (isUnderwriter()) Then
            InMotionGIT.Product.Proxy.Lookups.DiscountExtraPremiumTaxByProductLookup(LineOfBusiness, productCode, languageID, Date.Now(), False).ForEach(
            Sub(x) vret.Add(New With {
                            .Code = x.ExtraPremiumDiscountOrTaxCode,
                            .Description = x.Description,
                            .TypeOfItem = x.TypeOfItem,
                            .Currency = x.Currency,
                            .ExtraPremiumPercentage = x.ExtraPremiumPercentage,
                            .FixedAmount = x.FixedAmount
                            }))
        End If
        Return vret
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllModuleByProductLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Return (From u In InMotionGIT.Product.Proxy.Lookups.ModuleByProduct(uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"), Date.Now(), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetModuleByProductLkp(LineOfBusiness As Integer, productCode As Integer, languageID As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Product.Proxy.Lookups.ModuleByProduct(LineOfBusiness, productCode, languageID, Date.Now(), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllRatingByProductLkp() As List(Of InMotionGIT.Product.Entity.Contracts.BasicRatingTableForHealthProduct)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Return (From u In InMotionGIT.Product.Proxy.Lookups.BasicRatingTableByProduct(uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"), Date.Now(), False) Select u Order By u.RateDescription Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllBasicRatingByProductLkp(LineOfBusiness As Integer, productCode As Integer, languageID As Integer) As List(Of InMotionGIT.Product.Entity.Contracts.BasicRatingTableForHealthProduct)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Product.Proxy.Lookups.BasicRatingTableByProduct(LineOfBusiness, productCode, languageID, Date.Now(), False) Select u Order By u.RateDescription Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRoleByProductLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Return (From u In InMotionGIT.Product.Proxy.Lookups.RoleByProduct(uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"), Date.Now(), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetModuleByRiskInformation() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Return (From u In InMotionGIT.Underwriting.Proxy.Lookups.GetListModuleByRiskInformation(uwcase.UnderwritingCaseID, uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID")) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetModulesLkp(LineOfBusiness As Integer, productCode As Integer, languageID As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Product.Proxy.Lookups.ModuleByProduct(LineOfBusiness, productCode, languageID, Date.Now(), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllCoverageByRiskInformation(uwCaseId As Integer) As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage)
        Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance(uwCaseId)
        Dim listLkp As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage) = InMotionGIT.Product.Proxy.Lookups.CoverageByProductLkp(uwcase.LineOfBusiness, uwcase.Product, 0, HttpContext.Current.Session("LanguageID"), Date.Now(), False)
        Dim listResult As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage) = New List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage)
        For Each coverage As InMotionGIT.Policy.Entity.Contracts.CoverageWithCalculatedPremium In uwcase.RiskInformation.Policy.CoveragesWithCalculatedPremium.ToList()
            If uwcase.RiskInformation.Modules.IsNotEmpty AndAlso uwcase.RiskInformation.Modules.Count <> 0 Then
                For Each modules As InMotionGIT.Policy.Entity.Contracts.Module In uwcase.RiskInformation.Modules.ToList()
                    listResult.AddRange(listLkp.FindAll(Function(x) x.Code = coverage.CoverageCode And x.Module = modules.CoverageModule))
                Next
            Else
                listResult.AddRange(listLkp.FindAll(Function(x) x.Code = coverage.CoverageCode))
            End If
        Next
        Return listResult
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCoverageByRiskInformation(uwCaseId As Integer) As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage)
        Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance(uwCaseId)
        Dim listLkp As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage) = InMotionGIT.Product.Proxy.Lookups.CoverageByProductLkp(uwcase.LineOfBusiness, uwcase.Product, 0, HttpContext.Current.Session("LanguageID"), Date.Now(), False)
        Dim listResult As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage) = New List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage)
        For Each coverage As InMotionGIT.Policy.Entity.Contracts.CoverageWithCalculatedPremium In uwcase.RiskInformation.Policy.CoveragesWithCalculatedPremium.ToList()
            If coverage.Required <> "1" Then
                If uwcase.RiskInformation.Modules.IsNotEmpty AndAlso uwcase.RiskInformation.Modules.Count <> 0 Then
                    For Each modules As InMotionGIT.Policy.Entity.Contracts.Module In uwcase.RiskInformation.Modules.ToList()
                        listResult.AddRange(listLkp.FindAll(Function(x) x.Code = coverage.CoverageCode And x.Module = modules.CoverageModule))
                    Next
                Else
                    listResult.AddRange(listLkp.FindAll(Function(x) x.Code = coverage.CoverageCode))
                End If
            End If
        Next
        Return listResult
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCoverageLkp(LineOfBusiness As Integer, productCode As Integer, coverageModule As Integer, languageID As Integer) As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Coverage)
        If (isUnderwriter()) Then
            Return InMotionGIT.Product.Proxy.Lookups.CoverageByProductLkp(LineOfBusiness, productCode, coverageModule, languageID, Today, True)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetReasonForExclusionOfIllnessLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Product.Proxy.Lookups.ReasonForExclusionOfIllnessLkp(HttpContext.Current.Session("LanguageID"), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetLineOfBussinesLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Product.Proxy.Lookups.LineOfBussinesLkp(HttpContext.Current.Session("LanguageID"), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllUnderwritingRuleLkpTyped() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRuleLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllTabUnderwritingRuleFilterLkp(filter As String) As List(Of InMotionGIT.Underwriting.Contracts.TabUnderwritingRule) ' List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            filter = filter.ToLower()
            Return InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRuleLkpTyped(HttpContext.Current.Session("LanguageID"), True, True) _
                            .Where(Function(x) (x.UnderwritingRuleId.ToString() + x.Description).ToLower().Contains(filter)).ToList() _
                            .OrderBy(Function(x) x.UnderwritingRuleId).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetProductLkp() As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Product)
        If (isUnderwriter()) Then
            Return InMotionGIT.Product.Proxy.Lookups.ProductLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetStageLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.StageLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDecisionTypeLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Underwriting.Proxy.Lookups.DecisionTypeLkp(HttpContext.Current.Session("LanguageID"), False)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllAllRestrictionTypeLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Underwriting.Proxy.Lookups.RestrictionTypeLkp(HttpContext.Current.Session("LanguageID"), False) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllDiscoExPremTaxByProductLkp(LineOfBusiness As Integer, productCode As Integer, languageID As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Product.Proxy.Lookups.DiscountExtraPremiumTaxByProductLkp(LineOfBusiness, productCode, languageID, Date.Now(), True) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllRolesByProductLkp(LineOfBusiness As Integer, productCode As Integer, languageID As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return (From u In InMotionGIT.Product.Proxy.Lookups.RoleByProduct(LineOfBusiness, productCode, languageID, Date.Now(), True) Select u Order By u.Description Ascending).ToList()
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRoleAllowedByProductCoverage(LineOfBusiness As Integer, productCode As Integer, languageID As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Return InMotionGIT.Product.Proxy.Lookups.RoleAllowedByProductCoverage(LineOfBusiness, productCode, languageID, Today, True)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetClientByUnderwritingCase() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            uwcase.RolesInCase.ForEach(Sub(x) result.Add(New InMotionGIT.Common.DataType.LookUpValue With
                                                         {.Code = x.ClientID,
                                                         .Description = String.Format("{0} | {1}", x.ClientID, x.ClientName)
                                                        }))
        End If
        Return result
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetClientForExclusionLkp(editMode As Boolean, isExcludeInsured As Boolean) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim listRole As Integer() = Nothing
        listRole = If(isExcludeInsured, {7, 21, 22, 23, 24, 27, 28, 29, 30, 60, 67, 68}, {2, 7, 21, 22, 23, 24, 27, 28, 29, 30, 60, 67, 68})
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
            Dim listClient As List(Of RoleInCase) = uwcase.RolesInCase.Where(Function(x) listRole.Contains(x.Role)).ToList()
            If Not editMode Then
                listClient = listClient.Where(Function(x) x.ExclusionDate.IsEmpty).ToList()
            End If
            listClient.ForEach(Sub(x) result.Add(New InMotionGIT.Common.DataType.LookUpValue With
                                                             {.Code = x.ClientID,
                                                             .Description = String.Format("{0} | {1}", x.ClientID, x.ClientName)
                                                            }))
        End If
        Return result
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
    Public Shared Function GetRequirementTypeActivesLkp() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim listRequirementActives = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeActivesLkp(HttpContext.Current.Session("LanguageID"), False).OrderBy(Function(x) x.Description).ToList
            listRequirementActives.RemoveAll(Function(x) x.Description = "null" OrElse x.Description.IsEmpty OrElse x.Description = "")
            Return listRequirementActives
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRequirementTypeActivesByRolesLkp(caseId As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim listRequirementTypes As New List(Of InMotionGIT.Common.DataType.LookUpValue)
        If (isUnderwriter()) Then
            Dim uwcase As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, "")
            If Not IsNothing(uwcase) Then
                listRequirementTypes = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeActivesByRolesLkp(caseId, uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"))
                If (IsNothing(listRequirementTypes) OrElse listRequirementTypes.Count = 0) Then
                    listRequirementTypes = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeByProductLkp(uwcase.LineOfBusiness, uwcase.Product, HttpContext.Current.Session("LanguageID"), False)
                End If
                If listRequirementTypes.Count > 0 AndAlso listRequirementTypes.Item(0).Code <> "" Then
                    listRequirementTypes.Insert(0, New InMotionGIT.Common.DataType.LookUpValue("", ""))
                End If
                listRequirementTypes = removeDuplicatesLookUpValue(listRequirementTypes)
            End If
        End If
        Return listRequirementTypes
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRequirementTypeActivesByRoleLkp(RoleCode As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim listRequirementTypes As New List(Of InMotionGIT.Common.DataType.LookUpValue)

        If (isUnderwriter()) Then
            listRequirementTypes = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeActivesByRoleLkp(RoleCode, HttpContext.Current.Session("LanguageID"))
            If (IsNothing(listRequirementTypes) OrElse listRequirementTypes.Count = 0) Then
                listRequirementTypes = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeActivesLkp(HttpContext.Current.Session("LanguageID"), False)
            Else
            End If
            listRequirementTypes = listRequirementTypes.OrderBy(Function(x) x.Description).ToList
        End If
        Return listRequirementTypes
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRequirementTypeActivesByClientRoleLkp(ClientID As String) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim listRequirementTypes As New List(Of InMotionGIT.Common.DataType.LookUpValue)

        If (isUnderwriter()) Then
            If (ClientID.Trim.Length > 0) Then
                listRequirementTypes = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeActivesByClientRoleLkp(ClientID, HttpContext.Current.Session("LanguageID"))
            End If
            If (IsNothing(listRequirementTypes) OrElse listRequirementTypes.Count = 0) Then
                listRequirementTypes = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeActivesLkp(HttpContext.Current.Session("LanguageID"), False)
            End If
            If listRequirementTypes.Count > 0 AndAlso listRequirementTypes.Item(0).Code <> "" Then
                listRequirementTypes.Insert(0, New InMotionGIT.Common.DataType.LookUpValue("", ""))
            End If
            listRequirementTypes = listRequirementTypes.OrderBy(Function(x) x.Description).ToList
        End If
        Return listRequirementTypes
    End Function

    Public Shared Function removeDuplicatesLookUpValue(inputList As List(Of InMotionGIT.Common.DataType.LookUpValue)) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim uniqueStore As New Dictionary(Of String, Integer)
        Dim finalList As New List(Of InMotionGIT.Common.DataType.LookUpValue)
        For Each item In inputList
            If Not uniqueStore.ContainsKey(item.Code) Then
                uniqueStore.Add(item.Code, 0)
                finalList.Add(item)
            End If
        Next
        Return finalList
    End Function
End Class

