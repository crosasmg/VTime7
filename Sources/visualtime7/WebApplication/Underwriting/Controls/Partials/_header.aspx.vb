
Imports System.Web.Script.Services
Imports System.Web.Services
Imports InMotionGIT.Seguridad.Proxy
Imports InMotionGIT.Common.Contracts
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs
Imports InMotionGIT.Underwriting.Contracts
Imports InMotionGIT.Workflow.Support.Runtime
Imports System.Configuration
Imports System.Collections.Generic

Partial Class Underwriting_Controls_Partials_header
    Inherits System.Web.UI.Page

    Const FORMID As String = "E5F0E658-00D2-4712-865B-59192DB9F90A"
    Private Shared provider As String = ConfigurationManager.AppSettings.Get("DNEProvider")
    Private Shared tagTypeIds As New Dictionary(Of String, System.Int16)() From {
        {"FormId", 1},
        {"RequirementId", 2},
        {"CaseId", 3},
        {"InformativeId", 4},
        {"RequiretmentTypeId", 5}
    }

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function LoadUnderwritingCase(caseId As Integer) As Object

        Dim token = GetToken()

        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

        Dim userContext As New InMotionGIT.Membership.Providers.FrontOfficeMembershipUser
        Dim uwcase As UnderwritingCase = Nothing
        Dim caseStatusTypeList As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim decisionTypeList As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim stageList As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim userCode As Integer
        Dim caseStatusText = String.Empty
        Dim decisionText = String.Empty
        Dim wFProgressText = String.Empty
        Dim wFProgressToolTip = String.Empty
        Dim roles As Integer() = GetRolesUser()

        If IsNothing(HttpContext.Current.Session("LanguageID")) Then
            userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()
            HttpContext.Current.Session("LanguageID") = userContext.LanguageID
        End If

        If IsNothing(HttpContext.Current.Session("caseStatus")) Then
            HttpContext.Current.Session("caseStatus") = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingCaseStatusTypeLkp(HttpContext.Current.Session("LanguageID"), False, token)
        End If

        If IsNothing(HttpContext.Current.Session("decisionType")) Then
            HttpContext.Current.Session("decisionType") = InMotionGIT.Underwriting.Proxy.Lookups.DecisionTypeLkp(HttpContext.Current.Session("LanguageID"), False, token)
        End If

        If IsNothing(HttpContext.Current.Session("stage")) Then
            HttpContext.Current.Session("stage") = InMotionGIT.Underwriting.Proxy.Lookups.StageLkp(HttpContext.Current.Session("LanguageID"), False, token)
        End If

        caseStatusTypeList = HttpContext.Current.Session("caseStatus")
        decisionTypeList = HttpContext.Current.Session("decisionType")
        stageList = HttpContext.Current.Session("stage")
        userCode = HttpContext.Current.Session("UserId")


        Try
            InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.SelectAll(caseId, False, False, TokenHelper.GetValidToken)
            uwcase = GetUnderwritingCase(caseId)

            If (Not IsNothing(uwcase) AndAlso uwcase.LockedBy <> userCode) Then
                If uwcase.LockedBy <> userCode Then
                    uwcase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, token, True)
                    InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(caseId, uwcase, token)
                End If
            End If

            If (uwcase IsNot Nothing) Then
                Dim UwCaseBackup As InMotionGIT.Underwriting.Contracts.UnderwritingCase = InMotionGIT.Underwriting.Proxy.Manager.RetrieveWithAccessToken(uwcase.UnderwritingCaseID, Today, False, True, True, token)
                If (Not IsNothing(UwCaseBackup) AndAlso UwCaseBackup.Status <> uwcase.Status) Then
                    uwcase.Status = UwCaseBackup.Status
                    uwcase.StatusEnum = UwCaseBackup.StatusEnum
                    uwcase.StatusDescription = UwCaseBackup.StatusDescription
                    uwcase.LockedBy = 0
                    uwcase.Decision = UwCaseBackup.Decision
                    InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(caseId, uwcase, token)
                End If
            End If

            If Not IsNothing(uwcase) Then
                With uwcase

                    caseStatusText = (From caseStatus In caseStatusTypeList Where caseStatus.Code = .Status Select caseStatus.Description).FirstOrDefault()
                    decisionText = (From decision In decisionTypeList Where decision.Code = .Decision Select decision.Description).FirstOrDefault()

                    If ((.LockedBy <> 0 AndAlso .LockedBy = userCode AndAlso .Status <> InMotionGIT.Underwriting.Contracts.Enumerations.EnumUnderwritingCaseStatus.Consultation) OrElse .Status <> InMotionGIT.Underwriting.Contracts.Enumerations.EnumUnderwritingCaseStatus.Decided) Then
                        .StageDescription = (From stage In stageList Where stage.Code = .Stage Select stage.Description).FirstOrDefault()
                    Else
                        Dim underwritingCaseInBD = InMotionGIT.Underwriting.Proxy.Manager.RetrieveWithAccessToken(uwcase.UnderwritingCaseID, Today, False, False, False, token)
                        .Stage = underwritingCaseInBD.Stage
                        .StageDescription = (From stage In stageList Where stage.Code = underwritingCaseInBD.Stage Select stage.Description).FirstOrDefault()
                    End If

                    If (Not IsNothing(.WFInProgress) AndAlso .WFInProgress = 1) Then
                        wFProgressText = Resources.Header.WFProgressPendingText
                        wFProgressToolTip = Resources.Header.WFProgressPendingText
                    ElseIf (Not IsNothing(.WFInProgress) AndAlso .WFInProgress = 2) Then
                        wFProgressText = Resources.Header.WFProgressApprovedText
                        wFProgressToolTip = Resources.Header.WFProgressApprovedText
                    End If
                End With
            End If
        Catch ex As Exception
            ResponseHelper.ErrorToClient(ex, HttpContext.Current)
        End Try

        If (Not IsNothing(uwcase) AndAlso uwcase.IsNotEmpty AndAlso Not IsNothing(roles)) Then
            stageList = InMotionGIT.Underwriting.Proxy.Lookups.StageByRoleFilterWithAccessTokenLkp(uwcase.LineOfBusiness, uwcase.Product, uwcase.UnderwritingCaseType, HttpContext.Current.Session("LanguageID"), roles, False, uwcase.UnderwritingCaseID, TokenHelper.GetValidToken)
        End If

        Return New Dictionary(Of String, Object) From {
            {"stageList", stageList},
            {"caseStatusText", caseStatusText},
            {"decisionText", decisionText},
            {"wFProgressText", wFProgressText},
            {"wFProgressToolTip", wFProgressToolTip},
            {"selectedCase", uwcase},
            {"userId", userCode}
        }
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function EditInformation(caseId As Integer) As UnderwritingCase
        Dim token = GetToken()
        Dim uwcase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Manager.CaseLockForEditing(caseId, String.Empty, HttpContext.Current.Session("UserId"), token)
        Return uwcase
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CancelEditInformation(caseId As Integer) As UnderwritingCase
        Dim token = GetToken()
        Dim uwcase As UnderwritingCase = GetUnderwritingCase(caseId)

        If (Not IsNothing(uwcase.UnderwritingCaseRisk)) Then
            InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.RevertInsertionOfPreviousTemporaryStateItems(uwcase.DNESequenceId, token, provider)
            InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.RevertDeleteTemporaryStateItems(uwcase.DNESequenceId, token, provider)
        End If
        InMotionGIT.Underwriting.Proxy.Manager.CaseUnLock(uwcase.UnderwritingCaseID, String.Empty, HttpContext.Current.Session("UserId"), token)
        InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.ResetRiskClassification()
        Return uwcase
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetStageFilter(caseId As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim token = GetToken()
        Dim stageList As New List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim uwcase As UnderwritingCase = GetUnderwritingCase(caseId)
        Dim roles As Integer() = GetRolesUser()

        If (Not IsNothing(uwcase) AndAlso uwcase.IsNotEmpty AndAlso Not IsNothing(roles)) Then
            stageList = InMotionGIT.Underwriting.Proxy.Lookups.StageByRoleFilterWithAccessTokenLkp(uwcase.LineOfBusiness, uwcase.Product, uwcase.UnderwritingCaseType, HttpContext.Current.Session("LanguageID"), roles, True, uwcase.UnderwritingCaseID, TokenHelper.GetValidToken)
        End If

        Return stageList
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveCase(caseId As Integer, stage As Integer) As UnderwritingCase
        Dim token = GetToken()
        Dim uwcase = GetUnderwritingCase(caseId)
        SaveUnderwritingCase(uwcase, stage)
        InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.UpdateLockedObjectInDB(caseId, HttpContext.Current.Session("UserId"), HttpContext.Current.Session("LanguageID"), token)
        Return uwcase
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveAndCloseCase(caseId As Integer, stage As Integer) As Object
        Dim token = GetToken()
        Dim uwcase = GetUnderwritingCase(caseId)
        SaveUnderwritingCase(uwcase, stage)
        Dim workFlowResponse As IDictionary(Of String, Object) = Nothing

        'Se realiza llamado a Workflow para validación de requerimientos.
        If (IsNothing(uwcase.RiskInformation)) Then
            Dim UnderwritingCaseRisk As InMotionGIT.Underwriting.Contracts.UnderwritingCaseRisk = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveUnderwritingCaseByRelease(uwcase.UnderwritingCaseID, uwcase.LastRelease, TokenHelper.GetValidToken)
            If (Not IsNothing(UnderwritingCaseRisk)) Then
                uwcase.RiskInformation = InMotionGIT.Common.Helpers.Serialize.Deserialize(Of InMotionGIT.Policy.Entity.Contracts.RiskInformation)(InMotionGIT.Underwriting.Proxy.Helpers.Support.BytesToString(UnderwritingCaseRisk.RiskInformationData))
            End If
        End If
        Try
            workFlowResponse = DoWorkFromForm("SaveAndClose",
                        "8a8a4cbc-ba60-44f4-8963-84a326297acc",
                        0,
                        New Dictionary(Of String, Object) From {{"uwcaseid", uwcase.UnderwritingCaseID},
                                                                {"email", uwcase.PrimaryInsured.Emailaddress},
                                                                {"MasaCorporal", 0},
                                                                {"UsingVT", True},
                                                                {"RiskInformation", uwcase.RiskInformation},
                                                                {"ProductMaster", New InMotionGIT.Product.Entity.Contracts.ProductMaster},
                                                                {"context", WorkFlowContext(FORMID)},
                                                                {"SendMailIndicator", True},
                                                                {"MailToProducerIndicator", True},
                                                                {"OnLinePrintIndicator", False},
                                                                {"PlanType", 0},
                                                                {"PreviousPayment", True},
                                                                {"UnderwritingCaseUpdated", uwcase},
                                                                {"AuditUserIdentify", Integer.Parse(HttpContext.Current.Session("UserId"))},
                                                                {"Language", Integer.Parse(HttpContext.Current.Session("LanguageID"))},
                                                                {"DNESequenceID", uwcase.DNESequenceId.IfEmpty(0)},
                                                                {"Token", token},
                                                                {"Provider", provider}},
                        True,
                        True,
                        GetPath(),
                        FORMID)

            If CType(workFlowResponse.Item("context"), InMotionGIT.Common.Contracts.Context).Errors.Count > 0 Then
                Dim context As InMotionGIT.Common.Contracts.Context = CType(workFlowResponse.Item("context"), InMotionGIT.Common.Contracts.Context)
                context.Errors = New InMotionGIT.Common.Contracts.Errors.ErrorCollection
            End If
        Catch e As Exception
            ParseExceptionToCommonError(workFlowResponse, e)
        End Try
        UpdateWithoutErrors(caseId, workFlowResponse)

        Return showInformationWorkflow(caseId, workFlowResponse)
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AcceptCase(caseId As Integer) As Dictionary(Of String, String)
        Dim token = GetToken()
        Dim workFlowResponse As IDictionary(Of String, Object) = Nothing

        If (IsNothing(workFlowResponse)) Then
            ApplyOperationCase(caseId, "MakeDecisionRequestByUnderwritingPanel", String.Empty, workFlowResponse, 0)
            UpdateWithoutErrors(caseId, workFlowResponse)
        End If

        Return showInformationWorkflow(caseId, workFlowResponse, 1)
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ReopenCase(caseId As Integer) As Object
        Dim token = GetToken()
        Dim workFlowResponse As IDictionary(Of String, Object) = Nothing
        If (IsNothing(workFlowResponse)) Then
            ApplyOperationCase(caseId, "ChangeStatusCaseToOpen", String.Empty, workFlowResponse, 0)
            UpdateWithoutErrors(caseId, workFlowResponse)
        End If
        Return showInformationWorkflow(caseId, workFlowResponse, 3)
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUnderwritingCasesSearch(page As Integer, filter As String, sidx As String, sord As String) As Object
        Dim token = GetToken()
        Dim conditionFilterProduct = ""
        Dim uwcases As New List(Of Object)
        Dim ramos As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim productos As List(Of InMotionGIT.Product.Entity.Contracts.Lookups.Product)
        Dim managerClient As New InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient
        Dim subscribers As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim limit As Integer = 10
        Dim totalPages As Integer = 0

        ' Convertir filter de queryString a Dictionary
        Dim parseFilter = HttpUtility.ParseQueryString(filter)
        Dim searchConditions As Dictionary(Of String, String) = parseFilter.AllKeys.ToDictionary(Function(k As String) k, Function(k As String) parseFilter(k))

        If IsNothing(HttpContext.Current.Session("ramos")) Then
            HttpContext.Current.Session("ramos") = InMotionGIT.Product.Proxy.Lookups.LineOfBussinesLkp(HttpContext.Current.Session("LanguageID"), False)
        End If

        ramos = HttpContext.Current.Session("ramos")

        If searchConditions.ContainsKey("LineOfBusiness") Then
            Dim lineOfBusinessCodes = ramos.
                FindAll(Function(ramo) ramo.Description.ToUpper.Contains(searchConditions("LineOfBusiness").Trim.ToUpper)).
                Select(Function(x) x.Code)
            If lineOfBusinessCodes.IsNotEmpty AndAlso lineOfBusinessCodes.Count > 0 Then
                searchConditions("LineOfBusiness") = String.Join(",", lineOfBusinessCodes)
            Else
                searchConditions("LineOfBusiness") = String.Join(",", 0)
            End If
        End If

        Dim products = InMotionGIT.Product.Proxy.Lookups.ProductLkp(HttpContext.Current.Session("LanguageID"), False)

        If searchConditions.ContainsKey("Product") Then
            Dim productIds = products.
                FindAll(Function(product) product.ProductDescription.Trim.ToUpper.Contains(searchConditions("Product").Trim.ToUpper)).
                Select(Function(x) x.ProductCode)

            If productIds.IsNotEmpty Then
                conditionFilterProduct = searchConditions("Product")
                searchConditions("Product") = String.Join(",", productIds)
            End If
        End If

        If IsNothing(HttpContext.Current.Session("subscribers")) Then
            HttpContext.Current.Session("subscribers") = managerClient.UserLkp()
        End If

        subscribers = HttpContext.Current.Session("subscribers")

        If searchConditions.ContainsKey("UnderwriterID") Then
            If IsNumeric(searchConditions("UnderwriterID")) Then
                searchConditions("UnderwriterID") = searchConditions("UnderwriterID")
            Else
                Dim subscribersId = subscribers.
                    FindAll(Function(subscriber) subscriber.Description.ToUpper.Contains(searchConditions("UnderwriterID").Trim.ToUpper)).
                    Select(Function(x) x.Code)

                If subscribersId.IsNotEmpty Then
                    searchConditions("UnderwriterID") = String.Join(",", subscribersId)
                End If
            End If
        End If

        If searchConditions.ContainsKey("LockedBy") Then
            If IsNumeric(searchConditions("LockedBy")) Then
                searchConditions("LockedBy") = searchConditions("LockedBy")
            Else
                Dim subscribersId = subscribers.
                    FindAll(Function(subscriber) subscriber.Description.ToUpper.Contains(searchConditions("LockedBy").Trim.ToUpper)).
                    Select(Function(x) x.Code)

                If subscribersId.IsNotEmpty Then
                    searchConditions("LockedBy") = String.Join(",", subscribersId)
                End If
            End If
        End If

        ' return filter a queryString
        filter = String.Join("&", searchConditions.Select(Function(kvp) String.Format("{0}={1}", kvp.Key, kvp.Value)))
        Dim count As Integer = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingCaseLookupByFilterCount(HttpContext.Current.Session("LanguageID"), filter, sidx, token)
        If count > 0 Then
            totalPages = Math.Ceiling(count / limit)
        End If

        page = If(page > totalPages, totalPages, page)

        Dim start As Integer = limit * page - limit

        start = If(start < 0, 0, start)

        If sidx.IsEmpty Then
            sidx = "UnderwritingCaseID"
        End If

        Dim underwritingCaseLookup = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingCaseLookupByFilter(HttpContext.Current.Session("LanguageID"), filter, sidx, sord, start, limit, token)

        Try
            For Each uwcase As Lookups.UnderwritingCase In underwritingCaseLookup
                Dim ramo As Integer = 0
                Dim lineOfBusinessDescription As String
                Dim productDesc As String = String.Empty
                Dim compositeKey As String
                Dim underwriterText As String = GetSubscriberText(subscribers, uwcase.UnderwriterID)
                Dim lockedByText As String = GetSubscriberText(subscribers, uwcase.LockedBy)

                ramo = uwcase.LineOfBusiness

                If IsNothing(HttpContext.Current.Session("productos")) Then
                    HttpContext.Current.Session("productos") = InMotionGIT.Product.Proxy.Lookups.ProductByLineOfBusinessLkp(ramo, HttpContext.Current.Session("LanguageID"), False)
                End If

                If IsNothing(HttpContext.Current.Session("productos")) Or HttpContext.Current.Session("ultimoRamo") <> ramo Then
                    HttpContext.Current.Session("productos") = InMotionGIT.Product.Proxy.Lookups.ProductByLineOfBusinessLkp(ramo, HttpContext.Current.Session("LanguageID"), False)
                    HttpContext.Current.Session("ultimoRamo") = ramo
                End If

                If Not IsNothing(ramos) Then
                    lineOfBusinessDescription = (From r In ramos Where r.Code = ramo Select r.Description).FirstOrDefault()
                Else
                    lineOfBusinessDescription = String.Empty
                End If

                productos = HttpContext.Current.Session("productos")
                HttpContext.Current.Session("ultimoProducto") = uwcase.Product

                If Not IsNothing(productos) Then
                    productDesc = (From p In productos Where p.ProductCode = uwcase.Product Select p.ProductDescription).FirstOrDefault()
                End If

                compositeKey = String.Format("{0}-{1}", uwcase.UnderwritingCaseID, uwcase.ClientID)

                Dim isValidProductFilter = True
                If searchConditions.ContainsKey("Product") AndAlso Not productDesc.Trim.ToUpper.Contains(conditionFilterProduct.Trim.ToUpper) Then
                    isValidProductFilter = False
                    If (count > 0) Then
                        count = count - 1
                    End If
                End If

                If (isValidProductFilter) Then
                    Dim caseReturn = New Dictionary(Of String, Object) From {
                       {"UnderwritingCaseID", uwcase.UnderwritingCaseID},
                       {"OpenDate", uwcase.OpenDate},
                       {"UnderwriterID", underwriterText},
                       {"Role", uwcase.Role},
                       {"ClientID", uwcase.ClientID},
                       {"ClientName", uwcase.ClientName},
                       {"Decision", uwcase.Decision},
                       {"LineOfBusiness", lineOfBusinessDescription},
                       {"Product", productDesc},
                       {"FullProposalId", uwcase.FullProposalId},
                       {"BatchNumber", uwcase.BatchNumber},
                       {"PolicyID", uwcase.PolicyID},
                       {"FaceAmount", uwcase.FaceAmount},
                       {"LockedBy", lockedByText},
                       {"IsLocked", uwcase.IsLocked},
                       {"CompositeKey", compositeKey},
                       {"TypeOfLineOfBusiness", uwcase.TypeOfLineOfBusiness},
                       {"ManualOrAutomatic", uwcase.ManualOrAutomatic},
                       {"Status", uwcase.Status}
                    }
                    uwcases.Add(caseReturn)
                End If
            Next


        Catch ex As Exception
            ResponseHelper.ErrorToClient(ex, HttpContext.Current)
        End Try

        If IsNothing(subscribers) Then
            HttpContext.Current.Session("subscribers") = subscribers
        End If



        Return New Dictionary(Of String, Object) From {
            {"rows", uwcases},
            {"page", page},
            {"total", totalPages},
            {"records", count}
        }
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValues() As List(Of String)

        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

        Return New List(Of String) From {
            Resources.Header.GridColumnCaseId,
            Resources.Header.GridColumnDate,
            Resources.Header.GridColumnUnderwriter,
            Resources.Header.GridColumnRole,
            Resources.Header.GridColumnClient,
            Resources.Header.GridColumnName,
            Resources.Header.GridColumnDecision,
            Resources.Header.GridColumnLineOfBusiness,
            Resources.Header.GridColumnProduct,
            Resources.Header.GridColumnApplicationQuotation,
            Resources.Header.GridColumnResourceBatchNumber,
            Resources.Header.GridColumnPolicy,
            Resources.Header.GridColumnInsuredAmount,
            Resources.Header.GridColumnUnderwriterEditingCase,
            Resources.Header.GridColumnIsLocked,
            Resources.Header.GridColumnCompositeKey,
            Resources.Header.GridColumnTypeOfLineOfBusiness,
            Resources.Header.GridColumnManualOrAutomatic,
            Resources.Header.GridColumnStatus
            }
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function DeclineCase(caseId As Integer, rejectionReason As Integer, rejectionReasonText As String, freeTextReason As String) As Dictionary(Of String, String)
        Dim token = GetToken()
        Dim uwcase As UnderwritingCase = GetUnderwritingCase(caseId)
        Dim workFlowResponse As IDictionary(Of String, Object) = Nothing
        Dim sequenceId = uwcase.DNESequenceId

        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

        If (Not IsNothing(rejectionReasonText)) Then

            If (IsNothing(workFlowResponse)) Then
                ApplyOperationCase(caseId, "ChangeStatusCaseToClose", If(rejectionReason = 5, Resources.Header.RemarkRejection, rejectionReasonText), workFlowResponse, rejectionReason)
                UpdateWithoutErrors(caseId, workFlowResponse)
            End If

            If freeTextReason.Length > 0 Then
                If (sequenceId = 0) Then
                    sequenceId = CreateSequence(caseId)
                End If

                Dim resourceDTO As New ResourceDTO()
                resourceDTO.Note = New NoteDTO()
                resourceDTO.Note.SequenceId = sequenceId
                resourceDTO.Note.Content = DirectCast(freeTextReason, [String])

                resourceDTO.SequenceId = sequenceId
                resourceDTO.Name = Resources.Header.DescriptionRejectionCase
                resourceDTO.Description = "No Description"
                resourceDTO.ClientAssociatedCompany = "1"
                resourceDTO.ClientAssociatedPerson = "1"
                resourceDTO.Tags = GenerateTags(caseId, resourceDTO.SequenceId, resourceDTO.ConsequenceId)

                InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddResource(resourceDTO, token, provider)
            End If
            Return showInformationWorkflow(caseId, workFlowResponse, 2)
        End If
        Return Nothing
    End Function

    Private Shared Function GetSubscriberText(ByRef subscribers As List(Of InMotionGIT.Common.DataType.LookUpValue), underwriterID As Integer) As String
        Dim underwriterText As String = String.Empty

        Try
            Dim name As String = String.Empty

            If underwriterID <> 0 Then
                If subscribers.Exists(Function(x) x.Code = underwriterID) Then
                    name = (From s In subscribers Where s.Code = underwriterID Select s.Description).FirstOrDefault()
                Else
                    name = InMotionGIT.General.Proxy.Security.UserName(underwriterID)
                    subscribers.Add(New InMotionGIT.Common.DataType.LookUpValue(underwriterID, name))
                End If
            End If

            underwriterText = underwriterID.ToString + " " + name

        Catch ex As Exception
            underwriterText = underwriterID
        End Try

        Return underwriterText

    End Function


    ''' <summary>
    ''' Cambia el estado del caso de suscripción visible actualmente ene el panel.
    ''' </summary>
    ''' <param name="status">Identificación del estado al cual se debe cambian el caso de suscripción.</param>
    Private Shared Sub ApplyOperationCase(caseId As Integer, status As String, remarks As String, ByRef workFlowResponse As IDictionary(Of String, Object), rejectionReason As Integer)
        Dim uwcase As UnderwritingCase = GetUnderwritingCase(caseId)

        If uwcase.IsNotEmpty AndAlso IsNothing(workFlowResponse) Then
            Try
                Select Case status
                    Case "ChangeStatusCaseToOpen"
                        workFlowResponse = DoWorkFromForm("ChangeStatusCaseToOpen",
                                       "b5562cdc-ee5e-46d8-9642-0d101ab5abfb",
                                       0,
                                       New Dictionary(Of String, Object) From {{"context", WorkFlowContext(FORMID)},
                                                                               {"uwcaseid", uwcase.UnderwritingCaseID},
                                                                               {"remarks", String.Empty}},
                                       True,
                                       True,
                                       GetPath,
                                       FORMID)
                    Case "ChangeStatusCaseToClose" 'CloseCase or CloseEndosement
                        workFlowResponse = DoWorkFromForm("ChangeStatusCaseToClose",
                                           "b8fc252c-c5a5-40c4-9b55-2de1a01b8f3b",
                                           0,
                                           New Dictionary(Of String, Object) From {{"context", WorkFlowContext(FORMID)},
                                                                                   {"uwcaseid", uwcase.UnderwritingCaseID},
                                                                                   {"remarks", remarks},
                                                                                   {"UnderwritingCaseType", uwcase.UnderwritingCaseType},
                                                                                   {"RejectionReason", rejectionReason}},
                                           True,
                                           True,
                                           GetPath,
                                           FORMID)

                    Case "MakeDecisionRequestByUnderwritingPanel"   'AcceptCase or AcceptEndorsement
                        'If uwcase.RiskInformation.IsNotEmpty Then
                        workFlowResponse = DoWorkFromForm("AcceptCaseFromPanel",
                                       "bdd639d8-3563-4337-84ce-91b26a8c60c1",
                                       0,
                                       New Dictionary(Of String, Object) From {{"context", WorkFlowContext(FORMID)},
                                                                               {"uwcaseid", uwcase.UnderwritingCaseID},
                                                                               {"remarks", remarks},
                                                                               {"UnderwritingCaseType", uwcase.UnderwritingCaseType},
                                                                               {"PolicyId", uwcase.PolicyID},
                                                                               {"ProposalId", uwcase.ProposalID},
                                                                               {"IsRiskInformationNotEmpty", uwcase.RiskInformation.IsNotEmpty}},
                                       True,
                                       True,
                                       GetPath,
                                       FORMID)
                        'End If
                        'Para eliminar los mensajes que genera el CA Policy Setup solo si se logro generar la póliza.
                        If workFlowResponse.ContainsKey("PolicyId") AndAlso CType(workFlowResponse.Item("PolicyId"), Long) > 0 And CType(workFlowResponse.Item("context"), InMotionGIT.Common.Contracts.Context).Errors.Count > 0 Then
                            Dim context As InMotionGIT.Common.Contracts.Context = CType(workFlowResponse.Item("context"), InMotionGIT.Common.Contracts.Context)
                            context.Errors = New InMotionGIT.Common.Contracts.Errors.ErrorCollection
                        End If
                End Select
            Catch e As Exception
                ParseExceptionToCommonError(workFlowResponse, e)
            End Try
        End If
    End Sub

    Private Shared Sub SaveUnderwritingCase(uwcase As UnderwritingCase, stageId As Integer)
        Dim token = GetToken()
        Dim stageList As List(Of InMotionGIT.Common.DataType.LookUpValue)

        If IsNothing(HttpContext.Current.Session("stage")) Then HttpContext.Current.Session("stage") = InMotionGIT.Underwriting.Proxy.Lookups.StageLkp(HttpContext.Current.Session("LanguageID"), False, token)
        stageList = HttpContext.Current.Session("stage")

        If Not IsNothing(uwcase) Then
            With uwcase

                If Not IsNothing(stageId) Then
                    .Stage = stageId
                    .StageDescription = (From stage In stageList Where stage.Code = stageId Select stage.Description).FirstOrDefault()
                End If
            End With
        End If
        InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(uwcase.UnderwritingCaseID, uwcase, TokenHelper.GetValidToken)
    End Sub


    Private Shared Sub ParseExceptionToCommonError(ByRef workFlowResponse As IDictionary(Of String, Object), exception As Exception)
        Dim localContext As InMotionGIT.Common.Contracts.Context = Nothing
        If (IsNothing(workFlowResponse)) Then
            localContext = New InMotionGIT.Common.Contracts.Context
            localContext.Errors.AddError(
                exception.Source,
                0,
                exception.Message)
        End If
        workFlowResponse = New Dictionary(Of String, Object)
        workFlowResponse.Add("context", localContext)
    End Sub


    Private Shared Sub UpdateWithoutErrors(caseId As Integer, ByRef workFlowResponse As IDictionary(Of String, Object))
        If (Not IsNothing(workFlowResponse) AndAlso CType(workFlowResponse.Item("context"), InMotionGIT.Common.Contracts.Context).Errors.Count = 0) Then
            UpdateUnderwritingInstance(caseId)
        End If
    End Sub


    Private Shared Sub UpdateUnderwritingInstance(caseId As Integer)

        Dim uwcase As UnderwritingCase = GetUnderwritingCase(caseId)

        If (Not IsNothing(uwcase)) Then
            Dim uwcaseInBd As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Manager.RetrieveWithAccessToken(uwcase.UnderwritingCaseID, Today, False, True, True, TokenHelper.GetValidToken)
            If (Not IsNothing(uwcase)) Then
                uwcase.CaseHistory = uwcaseInBd.CaseHistory
                uwcase.DecisionEnum = uwcaseInBd.DecisionEnum
                uwcase.Decision = uwcaseInBd.Decision
                uwcase.CloseDate = uwcaseInBd.CloseDate
                uwcase.OpenDate = uwcaseInBd.OpenDate
                uwcase.Status = uwcaseInBd.Status
                uwcase.Reason = uwcaseInBd.Reason
                uwcase.RolesInCase = uwcaseInBd.RolesInCase
                uwcase.CertificateID = uwcaseInBd.CertificateID
                uwcase.PolicyID = uwcaseInBd.PolicyID
                InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(uwcase.UnderwritingCaseID, uwcase, TokenHelper.GetValidToken)
            End If
        End If
    End Sub


    Private Shared Function showInformationWorkflow(caseId As Integer, ByRef workFlowResponse As IDictionary(Of String, Object), Optional ByVal Operation As Integer = 0) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        If (Not IsNothing(workFlowResponse)) Then
            result = setWorkflowInformation(caseId, CType(workFlowResponse.Item("context"), InMotionGIT.Common.Contracts.Context).Errors, If(workFlowResponse.ContainsKey("PolicyId"), CType(workFlowResponse.Item("PolicyId"), String), String.Empty), Operation)
        End If
        ClearErrorsWorkflowResponse(workFlowResponse)
        Return result
    End Function


    Private Shared Function setWorkflowInformation(caseId As Integer, ByRef Errors As InMotionGIT.Common.Contracts.Errors.ErrorCollection, Optional ByVal Policy As String = "", Optional ByVal Operation As Integer = 0) As Dictionary(Of String, String)

        Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

        Dim uwCase As UnderwritingCase = GetUnderwritingCase(caseId)
        Dim workflowStackTrace As String = String.Empty
        Dim workflowInformation As String = String.Empty
        Dim PolicyId As String = uwCase.PolicyID.ToString & " / " & uwCase.CertificateID.ToString
        If (Not IsNothing(Errors) AndAlso Errors.Count > 0) Then
            Dim StringError = GetStringErrors(Errors)
            workflowStackTrace = StringError
        Else
            Select Case Operation
                Case 0
                    workflowInformation = Resources.Header.SuccessfulOperation
                Case 1 'Accept Case
                    workflowInformation = String.Format(Resources.Header.SuccessfulOperationAcceptCase, PolicyId, uwCase.FullProposalId)
                Case 2 'Decline Case
                    workflowInformation = String.Format(Resources.Header.SuccessfulOperationDeclineCase, uwCase.UnderwritingCaseID, uwCase.FullProposalId)
                Case 3 'ReOpen Case
                    workflowInformation = String.Format(Resources.Header.SuccessfulOperationReOpenCase, uwCase.UnderwritingCaseID, uwCase.FullProposalId)
            End Select
        End If

        Errors = Nothing

        Return New Dictionary(Of String, String) From {
            {"workflowStackTrace", workflowStackTrace},
            {"workflowInformation", workflowInformation}
        }
    End Function

    Private Shared Sub ClearErrorsWorkflowResponse(ByRef workFlowResponse As IDictionary(Of String, Object))
        Dim localContext = CType(workFlowResponse.Item("context"), InMotionGIT.Common.Contracts.Context)
        If (localContext IsNot Nothing) Then
            localContext.Errors = New InMotionGIT.Common.Contracts.Errors.ErrorCollection
            workFlowResponse.Remove("context")
            workFlowResponse.Add("context", localContext)
        End If
    End Sub


    Private Shared Function GetUnderwritingCase(caseId As Integer) As UnderwritingCase
        Return InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(caseId, GetToken())
    End Function

    Private Shared Function GetStringErrors(Errors As InMotionGIT.Common.Contracts.Errors.ErrorCollection) As String

        Dim StringError As String = ""

        For Each ErrorItem As InMotionGIT.Common.Contracts.Errors.Error In Errors
            StringError &= " - "
            StringError &= ErrorItem.Message
            StringError &= "</br>"
        Next

        Return StringError
    End Function

    Public Shared Function GetPath() As String
        Dim page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            Return page.AppRelativeVirtualPath
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Retorna un arreglo con los id de los roles del usuario.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetRolesUser() As Integer()
        If HttpContext.Current.Session("SessionTimeOut") <> "Yes" Then
            Try
                Dim userRoles As String
                Dim userContext As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()
                userRoles = InMotionGIT.Membership.Providers.Helper.RetrivellUserData(userContext.UserName).RoleName.ToLower()
                Dim roles() As String = userRoles.Split(";")
                Dim result(roles.Length) As Integer
                For i As Integer = 0 To roles.Length - 1
                    result(i) = InMotionGIT.Membership.Providers.FrontOfficeRoleProvider.RoleByName(roles(i)).Id
                Next
                Return result
            Catch ex As Exception
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

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
        Dim token As String = String.Empty

        If HttpContext.Current.Request IsNot Nothing Then
            Dim session As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session
            If session.IsNotEmpty Then
                userId = session("UserId")
                nUsercode = session("nUsercode")
                securitySchemaCode = session("sSche_code")
                roleName = session("sSche_code")
                token = TokenHelper.GetValidToken
            End If
        End If

        Return New InMotionGIT.Common.Contracts.Context(InMotionGIT.FrontOffice.Support.LanguageHelper.CurrentCultureToLanguage,
                                                        sourceId,
                                                        userId,
                                                        nUsercode,
                                                        securitySchemaCode,
                                                        roleName,
                                                        token)
    End Function


    ''' <summary>
    ''' It creates or generates a sequence if there is no sequence related to the CaseId in the DB
    ''' </summary>
    ''' <returns>SequenceId</returns>
    Protected Shared Function CreateSequence(caseId As Integer) As Integer
        Dim token = GetToken()
        Try
            Dim sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.SequenceExistInTags(caseId, token, provider)
            If sequenceId = -1 Then
                sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GenerateSequence(token, provider)
            End If
            Return sequenceId
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' It generates tagId Case
    ''' </summary>
    ''' <param name="sequenceId">SequenceId</param>
    ''' <param name="consequenceId">ConsequenceId</param>
    ''' <returns></returns>
    Protected Shared Function GenerateCaseIdTag(caseId As Integer, sequenceId As Integer, consequenceId As Integer) As TagDTO
        Dim tagDTO As New TagDTO
        Dim uwcase As UnderwritingCase = GetUnderwritingCase(caseId)
        tagDTO.SequenceId = sequenceId
        tagDTO.ConsequenceId = consequenceId
        If uwcase.IsNotEmpty Then
            tagDTO.Content = uwcase.UnderwritingCaseID
        End If

        tagDTO.TagTypeId = tagTypeIds("CaseId")
        Return tagDTO
    End Function

    Protected Shared Function GenerateTags(caseId As Integer, sequenceId As Integer, consequenceId As Integer) As HashSet(Of TagDTO)
        Dim tagsResult As New HashSet(Of TagDTO)
        tagsResult.Add(GenerateCaseIdTag(caseId, sequenceId, consequenceId))
        Return tagsResult
    End Function

    Protected Shared Function GetToken() As String
        Try
            Return TokenHelper.GetValidToken()
        Catch ex As Exception
            ResponseHelper.ErrorToClient(ex, HttpContext.Current, 401)
        End Try
        Return String.Empty
    End Function
End Class
