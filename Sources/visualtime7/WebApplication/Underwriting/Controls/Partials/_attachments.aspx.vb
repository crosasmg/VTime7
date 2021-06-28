Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.Modelo
Imports InMotionGIT.Underwriting.Contracts
Imports DevExpress.Web.ASPxEditors
Imports System.IO
Imports DevExpress.Web
Imports System.Data
Imports System.Net
Imports InMotionGIT.Seguridad.Proxy
Partial Class Underwriting_Controls_Partials_attachments
    Inherits GIT.Core.PageBase
    'Inherits System.Web.UI.Page

    Dim resourceTypes As New Dictionary(Of String, Int16)() From {
        {"Image", 1},
        {"Video", 2},
        {"Audio", 3},
        {"Document", 4}
    }

    Dim tagTypeIds As New Dictionary(Of String, Int16)() From {
        {"FormId", 1},
        {"RequirementId", 2},
        {"CaseId", 3},
        {"InformativeId", 4},
        {"RUT", 5},
        {"RUTFigure", 6},
        {"RUTFigureId", 7},
        {"Proposal", 8},
        {"DocumentDescription", 9},
        {"DocumentId", 10},
        {"RequirementDescription", 11},
        {"RequirementTypeId", 12},
        {"RequirementTypeDescription", 13},
        {"RequirementCreationDate", 14},
        {"PolicyId", 15}
    }

    Public Property uwCaseId() As Integer
        Get
            Return ViewState("uwCaseId")
        End Get
        Set(ByVal value As Integer)
            ViewState("uwCaseId") = value
        End Set
    End Property

    Public Property listadoNuevosResources As List(Of ResourceDTO)
        Get
            Return ViewState("newResources")
        End Get
        Set(ByVal value As List(Of ResourceDTO))
            ViewState("newResources") = value
        End Set
    End Property

    Public Property DneSequenceId As Integer
        Get
            Return ViewState("DneSequenceId")
        End Get
        Set(ByVal value As Integer)
            ViewState("DneSequenceId") = value
        End Set
    End Property


    Dim fileExtensionsPermited As New List(Of KeyValuePair(Of Integer, List(Of String)))
    Dim gridviewActiveElements As New List(Of ResourceDTO)

    Dim provider As String = GetDNEProvider()
    Private _formData As New Form1ParameterFiles

    Private Function GetDNEProvider() As String
        Dim provider = ConfigurationManager.AppSettings.Get("DNEProvider")
        If (provider IsNot Nothing) Then
            Return provider.ToUpper()
        End If
        Return "DNE"
    End Function

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
    End Sub

    Protected Overrides Sub InitializeCulture()
        If HttpContext.Current.Session("App_CultureInfoCode") IsNot Nothing Then
            Dim language = HttpContext.Current.Session("App_CultureInfoCode").ToString()
            UICulture = language
            Culture = language
            MyBase.InitializeCulture()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim token = GetToken()
        Try
            If Not IsPostBack Then
                uwCaseId = Request.QueryString("caseId").IfEmpty(0)
                Dim uwCase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
                If (Not IsNothing(uwCase)) Then
                    DneSequenceId = uwCase.DNESequenceId
                End If
                DeleteNewResourcesFromMemory()
                DefineSequenceCreation()
                ViewState("extensionArchivosPermitidos") = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetFileExtensionsAllowed(GetToken(), provider)
            Else
                LoadTagCheckBoxes()
                fileExtensionsPermited = ViewState("extensionArchivosPermitidos")
                ObjectDataSourceActiveResources.SelectMethod = ViewState("DNEFilesSelectMethod")
            End If
            ActivateButtonsBasedOnPageState()
        Catch ex As Exception
            If (ConfigurationManager.AppSettings("FrontOffice.Debug").ToLower.Equals("false")) Then
                Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
                Response.Write(String.Format("<script type=text/javascript> window.parent.location.href ='{0}/dropthings/Error.aspx?id={1}' </script>", baseUrl, Resources.Resource.DeniedAccess))
            Else
                Throw ex
            End If
        End Try
    End Sub

#Region "Mapping methods"
    Protected Sub DefineSequenceCreation()
        Dim useOnlyTemporaryResources = False ' Esta variable viene desde una propiedad del control en el FormDesigner el cual define  si se van a mostrar Recursos notas temporales(True). Si useOnlyTemporaryResources=False, se van a mostrar Recursos temporales y activos
        Dim DNEFilesSelectMethod As String = GetOperationContractMethodToGetResources(useOnlyTemporaryResources, Session("UserRoles")) 'TOBEREPLACED 
        Dim instance = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        If instance IsNot Nothing _
            AndAlso IsNumeric(instance.UnderwritingCaseID) AndAlso Not DNEFilesSelectMethod.Equals("") Then
            LinkButtonNewItemsId.Visible = True
            If (DneSequenceId = 0) Then
                'Dim sequenceDTO As New SequenceDTO
                'sequenceDTO.BlockedToExternalUsers = "0"
                'sequenceDTO.AdditionalAuthorizedUsers = "0"
                '_formData.SequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddSequence(sequenceDTO, ResponseHelper.GetValidToken())
                DneSequenceId = CreateSequence()
                instance.DNESequenceId = DneSequenceId
                InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(uwCaseId, instance, GetToken())
            End If
            'TODO se tiene que adicionar una nueva columna al grid especifique si un Recurso es Temporal o Activa
            If (Not IsNothing(DNEFilesSelectMethod)) Then
                'GridViewActiveResources.Enabled = (Not IsNothing(instance.LockedOn) AndAlso (instance.LockedBy = Session("nUserCode") OrElse instance.LockedBy = Session("UserId"))) 'Session("IsEditMode")
                ObjectDataSourceActiveResources.SelectMethod = DNEFilesSelectMethod
                ObjectDataSourceActiveResources.SelectParameters.Clear()
                ObjectDataSourceActiveResources.SelectParameters.Add(New Parameter("sequenceId", DbType.Int32, DneSequenceId.ToString()))
                ObjectDataSourceActiveResources.SelectParameters.Add(New Parameter("tags", TypeCode.Object)) ' This variable will be load once the select method gets triggered 
                ObjectDataSourceActiveResources.SelectParameters.Add(New Parameter("accessToken", DbType.String, GetToken()))
                ObjectDataSourceActiveResources.SelectParameters.Add(New Parameter("selectNotesOnly", DbType.Boolean, False.ToString))
                ObjectDataSourceActiveResources.SelectParameters.Add(New Parameter("provider", DbType.String, provider))
                GridViewActiveResources.DataSourceID = ObjectDataSourceActiveResources.ID
                ObjectDataSourceActiveResources.DataBind()
                CreateTagCheckBoxes()
                ViewState("DNEFilesSelectMethod") = ObjectDataSourceActiveResources.SelectMethod
                Dim x As New Parameter()
            End If
        Else
            InitializeComponents()
        End If
    End Sub

    ''' <summary>
    ''' It creates or generates a sequence if there is no sequence related to the CaseId in the DB
    ''' </summary>
    ''' <returns>SequenceId</returns>
    Protected Function CreateSequence() As Integer
        Dim sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.SequenceExistInTags(uwCaseId, GetToken(), provider)
        If sequenceId = -1 Then
            sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GenerateSequence(GetToken(), provider)
        End If
        Return sequenceId
    End Function
#End Region

#Region "Methods to deal with Active Resources"

    ''' <summary>
    ''' Creates tags based on which tab made the request. There are two tabs, one that has its own tab and the other that is indented in requirements 
    ''' </summary>
    ''' <returns>It returns a list of tags</returns>
    Private Function GenerateRequiredTagsForInserting() As List(Of ShortTagDTO)
        Dim instance = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        Dim tags As List(Of ShortTagDTO) = GetFiltersChecked()
        AddRequirementTags(tags)
        If Not IsNothing(instance) Then
            tags.Add(GenerateTagsCaseId(instance))
        End If
        Return tags
    End Function

    ''' <summary>
    ''' Creates tags based on which tab made the request. There are two tabs, one that has its own tab and the other that is indented in requirements 
    ''' </summary>
    ''' <returns>It returns a list of tags</returns>
    Private Function GenerateTagsForSelecting() As List(Of ShortTagDTO)
        Dim tagsForSelecting As List(Of ShortTagDTO) = GetFiltersChecked()
        'tagsForSelecting.AddRange(GetInvisibleFilterTags())
        Dim uwCase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        Dim requirementTag As ShortTagDTO = GetRequirementId(Request.QueryString("SentFromRequirementTab"), uwCase)
        If requirementTag IsNot Nothing Then
            tagsForSelecting.Add(requirementTag)
        End If
        Return tagsForSelecting
    End Function


    '''' <summary>
    '''' This method adds the tags that are not being shown in the Form. These tags could be Requirement, Form, and CaseId.
    '''' </summary>
    '''' <returns></returns>
    'Protected Function GetInvisibleFilterTags() As List(Of ShortTagDTO)
    '    Dim tagList As TagListDTO = ViewState(Me.ID + "DNEFileTagsFilter")
    '    Dim listInvisibleTags As New List(Of ShortTagDTO)
    '    If tagList IsNot Nothing Then
    '        listInvisibleTags = tagList.Where(Function(x) x.TagTypeId = tagTypeIds("FormId") Or x.TagTypeId = tagTypeIds("CaseId") Or x.TagTypeId = tagTypeIds("RequirementId")).Distinct.ToList()
    '    End If
    '    Return listInvisibleTags
    'End Function

    Private Sub AddRequirementTags(ByRef tags As List(Of ShortTagDTO))
        Dim sentFromRequirementTab As String = Request.QueryString("SentFromRequirementTab")
        Dim uwCase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())

        Dim requirementIdTag = GetRequirementId(sentFromRequirementTab, uwCase)
        If requirementIdTag IsNot Nothing Then
            tags.Add(requirementIdTag)
            TryToAddTag(tags, GetRequirementTypeId(uwCase))
            TryToAddTag(tags, GetRequirementCreationDateTag(uwCase))

            Dim RUTTag As ShortTagDTO = GetRutTag(sentFromRequirementTab, uwCase)
            If RUTTag IsNot Nothing Then
                tags.Add(RUTTag)
                TryToAddTag(tags, GetRUTFigure(RUTTag.Content, uwCase))
                TryToAddTag(tags, GetFigureRUTId(RUTTag.Content, uwCase))
            End If

            TryToAddTag(tags, GetPolicyTag(uwCase))
            TryToAddTag(tags, GetProposalTag(uwCase))  'Cotización
        End If
    End Sub

    Private Sub TryToAddTag(ByRef tags As List(Of ShortTagDTO), newTag As ShortTagDTO)
        If newTag IsNot Nothing Then
            tags.Add(newTag)
        End If
    End Sub

    Private Function GetRutTag(sentFromRequirementTab As String, uwCase As UnderwritingCase) As ShortTagDTO
        Dim requirementId = uwCase.CurrentRequirementID
        If sentFromRequirementTab IsNot Nothing And requirementId IsNot Nothing Then ' If was not sent from maintab, it means that it was sent through requirement attachments tab           
            Dim RUTCliente As String = uwCase.Requirements.Find(Function(x) x.RequirementID = uwCase.CurrentRequirementID).ClientId
            Dim RUTClientTag As New ShortTagDTO
            RUTClientTag.TagTypeId = tagTypeIds("RUT")
            RUTClientTag.Content = RUTCliente
            Return RUTClientTag
        End If
        Return Nothing
    End Function

    Private Function GetRUTFigure(RUT As String, uwCase As UnderwritingCase) As ShortTagDTO
        Dim roleInCase = uwCase.RolesInCase.Find(Function(x) x.ClientID.TrimStart("0").Equals(RUT.TrimStart("0")))
        If (roleInCase IsNot Nothing) Then
            Dim figureRutTag As New ShortTagDTO
            Dim figuraRut = roleInCase.RoleDescription
            If figuraRut IsNot Nothing AndAlso Not figuraRut.Equals("") Then
                figureRutTag.TagTypeId = tagTypeIds("RUTFigure")
                figureRutTag.Content = figuraRut
                Return figureRutTag
            End If
        End If
        Return Nothing
    End Function

    Private Function GetFigureRUTId(RUT As String, uwCase As UnderwritingCase) As ShortTagDTO
        Dim roleInCase = uwCase.RolesInCase.Find(Function(x) x.ClientID.TrimStart("0").Equals(RUT.TrimStart("0")))
        If roleInCase IsNot Nothing Then
            Dim role = roleInCase.Role
            Dim RutFigureIdTag As New ShortTagDTO
            RutFigureIdTag.TagTypeId = tagTypeIds("RUTFigureId")
            RutFigureIdTag.Content = role
            Return RutFigureIdTag
        End If
        Return Nothing
    End Function

    Private Function GetRequirementId(sentFromRequirementTab As String, uwCase As UnderwritingCase) As ShortTagDTO
        Dim requirementId = uwCase.CurrentRequirementID
        If sentFromRequirementTab IsNot Nothing And requirementId IsNot Nothing Then ' If was not sent from maintab, it means that it was sent through requirement attachments tab
            Dim requirementTag As New ShortTagDTO
            requirementTag.TagTypeId = tagTypeIds("RequirementId")
            requirementTag.Content = requirementId
            Return requirementTag
        End If
        Return Nothing
    End Function

    Private Function GetProposalTag(uwCase As UnderwritingCase) As ShortTagDTO
        Dim proposalId = uwCase.ProposalID
        Dim figureRutTag As New ShortTagDTO
        figureRutTag.TagTypeId = tagTypeIds("Proposal")
        figureRutTag.Content = proposalId
        Return figureRutTag
    End Function

    Private Function GetRequirementDescriptionTag(uwCase As UnderwritingCase) As ShortTagDTO
        Dim requirementId = uwCase.CurrentRequirementID
        Dim requirementDescription = uwCase.Requirements.Find(Function(x) x.RequirementID = uwCase.CurrentRequirementID).Description
        If requirementDescription IsNot Nothing AndAlso Not requirementDescription.Equals("") Then
            Dim requirementDescriptionTag As New ShortTagDTO
            requirementDescriptionTag.TagTypeId = tagTypeIds("RequirementDescription")
            requirementDescriptionTag.Content = requirementDescription
            Return requirementDescriptionTag
        End If
        Return Nothing
    End Function

    Private Function GetRequirementTypeId(uwCase As UnderwritingCase) As ShortTagDTO
        Dim requirementId = uwCase.CurrentRequirementID
        If requirementId IsNot Nothing Then ' If was not sent from maintab, it means that it was sent through requirement attachments tab
            Dim currentRequirementType As Integer = uwCase.Requirements.Find(Function(x) x.RequirementID = uwCase.CurrentRequirementID).RequirementType
            Dim requirementTypeIdTag As New ShortTagDTO
            requirementTypeIdTag.TagTypeId = tagTypeIds("RequirementTypeId")
            requirementTypeIdTag.Content = currentRequirementType
            Return requirementTypeIdTag
        End If
        Return Nothing
    End Function

    Private Function GetRequirementTypeDescription(uwCase As UnderwritingCase) As ShortTagDTO
        Dim requirementId = uwCase.CurrentRequirementID
        If requirementId IsNot Nothing Then ' If was not sent from maintab, it means that it was sent through requirement attachments tab
            Dim currentRequirementTypeDescription As Integer = uwCase.Requirements.Find(Function(x) x.RequirementID = uwCase.CurrentRequirementID).RequirementTypeDescription
            Dim requirementTypeDescriptionTag As New ShortTagDTO
            requirementTypeDescriptionTag.TagTypeId = tagTypeIds("RequirementTypeDescription")
            requirementTypeDescriptionTag.Content = currentRequirementTypeDescription
            Return requirementTypeDescriptionTag
        End If
        Return Nothing
    End Function

    Private Function GetRequirementCreationDateTag(uwCase As UnderwritingCase) As ShortTagDTO
        Dim requirementId = uwCase.CurrentRequirementID
        Dim requirementCreationDate = uwCase.Requirements.Find(Function(x) x.RequirementID = uwCase.CurrentRequirementID).CreationDate
        Dim requirementCreationDateTag As New ShortTagDTO
        requirementCreationDateTag.TagTypeId = tagTypeIds("RequirementCreationDate")
        requirementCreationDateTag.Content = requirementCreationDate
        Return requirementCreationDateTag
    End Function

    Private Function GetPolicyTag(uwCase As UnderwritingCase) As ShortTagDTO
        Dim policyId = uwCase.PolicyID
        Dim figureRutTag As New ShortTagDTO
        figureRutTag.TagTypeId = tagTypeIds("PolicyId")
        figureRutTag.Content = policyId
        Return figureRutTag
    End Function

    ''' <summary>
    ''' Create tags based on tags related to the SequenceId Control
    ''' </summary>
    Private Sub CreateTagCheckBoxes()
        If IsNumeric(DneSequenceId) Then
            Dim checkBoxes As Dictionary(Of String, Boolean) = GetCheckBoxesFromViewState()
            'Dim tagList As List(Of ShortTagDTO) = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetTagsRelatedToResourceActiveAndTemporarySequence(_formData.SequenceId, ResponseHelper.GetValidToken())
            Dim tagList As List(Of TagDTO) = GetTagsFromElementsInGridViewActive()

            Dim tagListFiltered As List(Of TagDTO) = tagList.Where(Function(x) x.TagTypeId <> tagTypeIds("FormId") And x.TagTypeId <> tagTypeIds("CaseId") And x.TagTypeId <> tagTypeIds("RequirementId")).Distinct.ToList()
            Dim checkBoxId As String
            For Each tag As TagDTO In tagListFiltered
                checkBoxId = "Files" + "|" + tagTypeIds.ElementAt(tag.TagTypeId - 1).Value.ToString() + "|" + tag.Content.ToString
                If Not CheckBoxExist(checkBoxId) Then
                    Dim checkBox As New CheckBox()
                    checkBox.ID = checkBoxId
                    checkBox.Text = tag.Content.ToString
                    checkBoxes.Add(checkBox.ID, False)
                    checkBox.Enabled = Session("IsEditMode")
                    Me.tagFilterContainer.Controls.Add(checkBox)
                End If
            Next
            ViewState("DNEFilesTagCheckBoxes") = checkBoxes
            'Me.tagFilterContainerfiles0.Update() 'TODO Updates the panel once new checkboxes gets add
        End If
    End Sub

    ''' <summary>
    ''' Gets the tags involved in GridViewActiveResources
    ''' </summary>
    ''' <returns></returns>
    Private Function GetTagsFromElementsInGridViewActive() As List(Of TagDTO)
        Dim tagsInActiveGridView As New List(Of TagDTO)
        For Each resource In gridviewActiveElements
            If resource.Tags IsNot Nothing Then
                tagsInActiveGridView.AddRange(resource.Tags)
            End If
        Next
        Return tagsInActiveGridView
    End Function

    'Private Function GetTagsFromElementsInGridViewActive(tagList As List(Of TagDTO)) As List(Of TagDTO)
    '    Dim tagsFiltered As New List(Of HashSet(Of TagDTO))
    '    For Each tagList1 In tagList
    '        Dim tagListFiltered As List(Of TagDTO) = tagList1.Where(Function(x) x.TagTypeId <> tagTypeIds("FormId") And x.TagTypeId <> tagTypeIds("CaseId") And x.TagTypeId <> tagTypeIds("RequirementId")).Distinct.ToList()
    '    Next
    '    Return tagsFiltered
    'End Function

    ''' <summary>
    ''' It obtains the elements from the viewState("TagCheckBoxes") 
    ''' </summary>
    ''' <returns></returns>
    Private Function GetCheckBoxesFromViewState() As Dictionary(Of String, Boolean)
        Dim checkBoxes As New Dictionary(Of String, Boolean)
        If ViewState("DNEFilesTagCheckBoxes") IsNot Nothing Then
            For Each checkbox As KeyValuePair(Of String, Boolean) In ViewState("DNEFilesTagCheckBoxes")
                checkBoxes.Add(checkbox.Key, checkbox.Value)
            Next
        End If
        Return checkBoxes
    End Function

    ''' <summary>
    ''' create the checkboxes elements based on the elements saved in ViewState
    ''' </summary>
    Private Sub LoadTagCheckBoxes()
        If ViewState("DNEFilesTagCheckBoxes") IsNot Nothing Then
            For Each checkBoxElement As KeyValuePair(Of String, Boolean) In ViewState("DNEFilesTagCheckBoxes")
                Dim checkBox As New CheckBox()
                checkBox.ID = checkBoxElement.Key
                checkBox.Text = checkBoxElement.Key.Split("|")(2)
                checkBox.Checked = checkBoxElement.Value
                If Not CheckBoxExist(checkBox.ID) Then
                    Me.tagFilterContainer.Controls.Add(checkBox)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Verifies whether tagFilterContainer panel contains a checkbox with the given ID
    ''' </summary>
    ''' <param name="checkBoxId"></param>
    ''' <returns></returns>
    Private Function CheckBoxExist(checkBoxId As String) As Boolean
        For Each checkBox As CheckBox In Me.tagFilterContainer.Controls.OfType(Of CheckBox)
            If checkBox.ID = checkBoxId Then
                Return True
            End If
        Next
        Return False
    End Function


    Protected Sub FilterButton(sender As Object, e As EventArgs)
        UpdateViewStateTags()
        GridViewActiveResources.DataBind()
    End Sub

    ''' <summary>
    ''' Updates the variable TagCheckBoxes in Viewstate based on the checkboxes checked. 
    ''' </summary>
    Private Sub UpdateViewStateTags()
        Dim checkBoxes As Dictionary(Of String, Boolean) = ViewState("DNEFilesTagCheckBoxes")
        For Each checkBox As CheckBox In Me.tagFilterContainer.Controls.OfType(Of CheckBox)
            checkBoxes(checkBox.ID) = checkBox.Checked
        Next
        ViewState("DNEFilesTagCheckBoxes") = checkBoxes
    End Sub

    Protected Function GetFiltersChecked() As List(Of ShortTagDTO)
        Dim tagList As New List(Of ShortTagDTO)
        Dim shortTag As ShortTagDTO
        For Each panelElement As Control In tagFilterContainer.Controls
            If panelElement.GetType Is GetType(CheckBox) Then
                Dim elementCheckBox As CheckBox = DirectCast(panelElement, CheckBox)
                If elementCheckBox.Checked Then
                    shortTag = New ShortTagDTO
                    shortTag.TagTypeId = elementCheckBox.ID
                    shortTag.Content = elementCheckBox.Text
                    tagList.Add(shortTag)
                End If
            End If
        Next
        Return tagList
    End Function

    Protected Sub DownloadResource(resourceDTO As ResourceDTO)
        Dim token = GetToken()
        Try
            Dim composedResourceKey As New ComposedResourceKey(resourceDTO.SequenceId, resourceDTO.ConsequenceId)
            Dim resourceContent As Byte() = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetResourceContent(composedResourceKey, GetToken(), provider)
            SaveFile(resourceContent, resourceDTO.Name)

            '    Select Case resourceDTO.ResourceTypeId
            '        Case resourceTypes("Image")
            '            Dim imageDTO As Byte() = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetOriginalSizeImage(composedResourceKey, ResponseHelper.GetValidToken(), provider)
            '            SaveFile(imageDTO.OriginalImage, resourceDTO.Name)
            '        Case resourceTypes("Video")
            '            Dim videoDTO As VideoDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetVideo(composedResourceKey, ResponseHelper.GetValidToken(), provider)
            '            SaveFile(videoDTO.Video, resourceDTO.Name)
            '        Case resourceTypes("Audio")
            '            Dim audioDTO As AudioDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetAudio(composedResourceKey, ResponseHelper.GetValidToken(), provider)
            '            SaveFile(audioDTO.Sound, resourceDTO.Name)
            '        Case resourceTypes("Document")
            '            Dim documentDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetDocument(composedResourceKey, ResponseHelper.GetValidToken(), provider)
            '            SaveFile(documentDTO.Document, resourceDTO.Name)
            '    End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub SaveFile(bytesContent As Byte(), fileName As String)
        Try
            Response.ClearContent()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.ContentType = "application/octet-stream"
            Response.BinaryWrite(bytesContent)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Gets the method that will be used to populate the main Grid
    ''' </summary>
    ''' <param name="useOnlyTemporaryResources">Variable that defines whether only temporary Resources will be shown</param>
    ''' <param name="userRoles">User's roles</param>
    ''' <returns>The method that will be used to populate the Grid</returns>
    Private Function GetOperationContractMethodToGetResources(useOnlyTemporaryResources As Boolean, userRoles As String) As String
        If userRoles IsNot Nothing AndAlso Not userRoles.Equals("") Then
            If userRoles.Split(";").Contains("suscriptor") Then
                If useOnlyTemporaryResources Then
                    Return "GetResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetActiveResourceSequenceAndMyTemporals"
                End If
            ElseIf userRoles.Split(";").Contains("cliente") Then ' if userRole is Client
                If useOnlyTemporaryResources Then
                    Return "GetOwnResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetOwnResourceSequenceActiveAndTemporaryState"
                End If
            ElseIf userRoles.Split(",").Contains("suscriptor") Then
                If useOnlyTemporaryResources Then
                    Return "GetResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetActiveResourceSequenceAndMyTemporals"
                End If
            ElseIf userRoles.Split(",").Contains("cliente") Then ' if userRole is Client
                If useOnlyTemporaryResources Then
                    Return "GetOwnResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetOwnResourceSequenceActiveAndTemporaryState"
                End If
            End If
        End If
        Return ""   'Si el usuario no es cliente, tampoco es administrador, no debería ver nada         
    End Function

    Protected Sub ObjectDataSourceActiveResources_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles ObjectDataSourceActiveResources.Selecting
        If InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken()) IsNot Nothing _
            AndAlso IsNumeric(InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken()).UnderwritingCaseID) Then ' Si existe un caso seleccionado llenar el Grid
            e.InputParameters.Item("tags") = GenerateTagsForSelecting()
        Else
            e.Cancel = True   ' si aun no existe un caso de suscripción cargado, no llamar al método de select del ObjectDatasource
        End If
    End Sub

    ''' <summary>
    ''' This method clears the content of the Control when SequenceId is null
    ''' </summary>
    Private Sub InitializeComponents()
        ViewState("DNEFilesSelectMethod") = String.Empty
        Me.tagFilterContainer.Controls.Clear()
        GridViewActiveResources.DataSourceID = String.Empty
        GridViewActiveResources.DataBind()
        GridViewActiveResources.Enabled = False
        LinkButtonNewItemsId.Visible = False
    End Sub

    ''' <summary>
    ''' Enables or disables buttons to add new Notes based on Form's state
    ''' </summary>
    Private Sub ActivateButtonsBasedOnPageState()
        If (uwCaseId.IsNotEmpty) Then
            Dim selectedCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
            If (Not IsNothing(selectedCase) AndAlso selectedCase.Status = InMotionGIT.Underwriting.Contracts.Enumerations.EnumUnderwritingCaseStatus.Consultation) Then
                LabelNewItem.Visible = False
                LinkButtonNewItemsId.Enabled = False
                LinkButtonNewItemsId.Visible = False
                ButtonFilter.Enabled = False
            ElseIf (Not IsNothing(selectedCase)) Then
                LabelNewItem.Visible = (Not IsNothing(selectedCase.LockedOn) AndAlso (selectedCase.LockedBy = Session("nUserCode") OrElse selectedCase.LockedBy = Session("UserId")))
                LinkButtonNewItemsId.Enabled = (Not IsNothing(selectedCase.LockedOn) AndAlso (selectedCase.LockedBy = Session("nUserCode") OrElse selectedCase.LockedBy = Session("UserId")))
                LinkButtonNewItemsId.Visible = (Not IsNothing(selectedCase.LockedOn) AndAlso (selectedCase.LockedBy = Session("nUserCode") OrElse selectedCase.LockedBy = Session("UserId")))
                ButtonFilter.Enabled = (Not IsNothing(selectedCase.LockedOn) AndAlso (selectedCase.LockedBy = Session("nUserCode") OrElse selectedCase.LockedBy = Session("UserId")))
            Else
                LabelNewItem.Visible = False
                LinkButtonNewItemsId.Enabled = False
                LinkButtonNewItemsId.Visible = False
                ButtonFilter.Enabled = False
            End If
        End If
    End Sub

    Protected Sub GridviewRecursosActivos_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        Dim composedResourceKey As New ComposedResourceKey(CInt(e.Values("SequenceId")), CInt(e.Values("ConsequenceId")))
        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.DeleteResourceTemporarily(composedResourceKey, GetToken(), provider)

        e.Cancel = True
        GridViewActiveResources.CancelEdit()
    End Sub

    Protected Sub GridviewRecursosActivos_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Dim updatedResourceDTO As ResourceDTO = GridViewActiveResources.GetRow(GridViewActiveResources.EditingRowVisibleIndex)
        updatedResourceDTO.Description = e.NewValues("Description")
        updatedResourceDTO.ExpirationDate = e.NewValues("ExpirationDate")
        updatedResourceDTO.ClientAssociatedPerson = e.NewValues("ClientAssociatedPerson")
        updatedResourceDTO.ClientAssociatedCompany = e.NewValues("ClientAssociatedCompany")
        updatedResourceDTO.LocationId = e.NewValues("LocationId")

        'In a newer version of this control, we have to deal with this properties, in this version, these properties are left empty 
        updatedResourceDTO.ClientAssociatedPerson = -1
        updatedResourceDTO.ClientAssociatedCompany = -1

        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.UpdateResource(updatedResourceDTO, GetToken(), provider)

        e.Cancel = True
        GridViewActiveResources.CancelEdit()
    End Sub

#End Region


#Region "Methods to deal with temporal Resources"

    Protected Sub FileUploaderfiles0_FileUploadComplete(sender As Object, e As EventArgs) Handles FileUploaderfiles0.FileUploadComplete
        Dim maxResourcesAllowed = 10
        Dim resources As New List(Of ResourceDTO)
        Dim counter As Integer = GridViewTemporalResources.VisibleRowCount()
        resources = GetNewResources()
        Dim errorMessage = ""
        Dim validationErrorMessage As String = ""
        If Request.Files.Count > 0 And Request.Files.Count - 1 <= maxResourcesAllowed And counter <= maxResourcesAllowed Then   'Counter <maxResourcesAllowed, no mas de 10 elementos pueden adicionarse al grid            
            For index As Integer = 1 To Request.Files.Count - 1
                Dim file As HttpPostedFile = Request.Files(index)
                validationErrorMessage = ValidateFile(file)
                If validationErrorMessage.Equals("") Then
                    resources.Add(GenerateResourceBasedOnFile(file, counter))
                Else
                    errorMessage = String.Format("{0} {1}: {2}", errorMessage, Path.GetFileName(file.FileName), validationErrorMessage)
                End If
                counter += 1
            Next
            LoadGridViewNewResources(resources)
            Dim resourcesToBeSaved As List(Of ResourceDTO) = GetNewResources()
            If ValidateResources(resourcesToBeSaved) Then
                For Each resource As ResourceDTO In resourcesToBeSaved
                    'resource.Tags = GenerateTags(resource.SequenceId, resource.ConsequenceId)
                    SaveResource(resource)
                Next
                DeleteNewResourcesFromMemory()
                ReloadGridViewNewResources()
                EndEditionInGridViewNewResources()
                ShowPanelWithActiveResources()
                ButtonFilter.Enabled = True
                GridViewActiveResources.Enabled = True
                LinkButtonNewItemsId.Visible = True
            End If
        Else
            errorMessage = GetLocalResourceObject("MaxNumberFilesUploadText") + " " + maxResourcesAllowed.ToString
        End If
        If errorMessage.Length > 0 Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "showMessage('" + errorMessage + "');", True)
        End If
    End Sub

    Protected Sub PresentNewResources(sender As Object, e As EventArgs) Handles ButtonSubmitFileUpload.Click
        Dim maxResourcesAllowed = 10
        Dim resources As New List(Of ResourceDTO)
        Dim counter As Integer = GridViewTemporalResources.VisibleRowCount()
        resources = GetNewResources()
        Dim errorMessage = ""
        Dim validationErrorMessage As String = ""

        If Request.Files.Count > 0 And Request.Files.Count - 1 <= maxResourcesAllowed And counter <= maxResourcesAllowed Then   'Counter <maxResourcesAllowed, no mas de 10 elementos pueden adicionarse al grid            
            For index As Integer = 1 To Request.Files.Count - 1
                Dim file As HttpPostedFile = Request.Files(index)
                validationErrorMessage = ValidateFile(file)
                If validationErrorMessage.Equals("") Then
                    resources.Add(GenerateResourceBasedOnFile(file, counter))
                Else
                    errorMessage = String.Format("{0} {1}: {2}", errorMessage, Path.GetFileName(file.FileName), validationErrorMessage)
                End If
                counter += 1
            Next
            LoadGridViewNewResources(resources)
        Else
            errorMessage = GetLocalResourceObject("MaxNumberFilesUploadText") + " " + maxResourcesAllowed.ToString
        End If
        If errorMessage.Length > 0 Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "showMessage('" + errorMessage + "');", True)
        End If
    End Sub

    Protected Function ValidateFile(file As HttpPostedFile) As String
        Dim errorMessage As String = ""
        Dim fileName As String
        fileName = Path.GetFileName(file.FileName)
        errorMessage = ValidateFileExtension(fileName)
        errorMessage = Concatenate(errorMessage, ValidateFileSize(file))
        errorMessage = Concatenate(errorMessage, ValidateFileNameLenght(fileName))
        Return errorMessage
    End Function

    Private Function Concatenate(errorMessage As String, stringToConcatenate As String) As String
        If errorMessage.Equals("") Then
            errorMessage = stringToConcatenate
        Else
            errorMessage = String.Format("{0} {1}", errorMessage, stringToConcatenate)
        End If

        Return errorMessage
    End Function

    Protected Function ValidateFileExtension(fileName As String) As String
        Dim result = True
        Dim extension As String
        Dim fileType = GetFileType(fileName)
        If fileType <> -1 Then
            extension = fileName.Substring(fileName.LastIndexOf(".")).ToUpper
            Select Case fileType
                Case resourceTypes("Image")
                    result = (fileExtensionsPermited(resourceTypes("Image") - 1).Value.Contains(extension))
                Case resourceTypes("Video")
                    result = (fileExtensionsPermited(resourceTypes("Video") - 1).Value.Contains(extension))
                Case resourceTypes("Audio")
                    result = (fileExtensionsPermited(resourceTypes("Audio") - 1).Value.Contains(extension))
                Case resourceTypes("Document")
                    result = (fileExtensionsPermited(resourceTypes("Document") - 1).Value.Contains(extension))
            End Select
            If result Then
                Return ""
            End If
        End If
        Return GetLocalResourceObject("FileExtensionNotAllowedText")
    End Function

    Protected Function ValidateFileNameLenght(fileName As String) As String
        Dim maxFileNameLenghtExtension As Integer = 100
        fileName = Path.GetFileName(fileName)
        If fileName.Length <= maxFileNameLenghtExtension Then
            Return ""
        End If
        Return GetLocalResourceObject("FileNameExceededText") + " " + maxFileNameLenghtExtension.ToString
    End Function

    Protected Function ValidateFileSize(file As HttpPostedFile) As String
        Dim result = True
        Dim fileType = GetFileType(file.FileName)
        Dim contentLength As Decimal = file.ContentLength / 1024 / 1024  ' the length of the file is in bytes, so we have to convert it to MB
        Select Case fileType
            Case resourceTypes("Image")
                result = (fileExtensionsPermited(resourceTypes("Image") - 1).Key > contentLength)
            Case resourceTypes("Video")
                result = (fileExtensionsPermited(resourceTypes("Video") - 1).Key > contentLength)
            Case resourceTypes("Audio")
                result = (fileExtensionsPermited(resourceTypes("Audio") - 1).Key > contentLength)
            Case resourceTypes("Document")
                result = (fileExtensionsPermited(resourceTypes("Document") - 1).Key > contentLength)
        End Select

        If (result) Then
            Return ""
        End If

        Return GetLocalResourceObject("FileSizeExceededText") + " " + fileExtensionsPermited(fileType - 1).Key.ToString + " MB."
    End Function


    Protected Sub LoadGridViewNewResources(listadoResources As List(Of ResourceDTO))
        'Session("GridViewTemporalResources") = listadoNuevosResources
        listadoNuevosResources = listadoResources
        ReloadGridViewNewResources()
    End Sub

    Protected Sub ReloadGridViewNewResources()
        'GridViewTemporalResources.DataSource = Session("GridViewTemporalResources")
        GridViewTemporalResources.DataSource = listadoNuevosResources
        GridViewTemporalResources.DataBind()
    End Sub

    Protected Function GetNewResources() As List(Of ResourceDTO)
        If Not IsNothing(listadoNuevosResources) Then Return listadoNuevosResources
        Return New List(Of ResourceDTO)
    End Function

    Protected Sub EndEditionInGridViewNewResources()
        GridViewTemporalResources.CancelEdit()
    End Sub

    Protected Sub CancelInsertionOfNewResources(sender As Object, e As EventArgs) Handles ButtonCancelSaveResources.Click
        NewResources.Visible = False
        EndEditionInGridViewNewResources()
        DeleteNewResourcesFromMemory()
        ReloadGridViewNewResources()
        LinkButtonNewItemsId.Visible = True
        GridViewActiveResources.Enabled = True
    End Sub

    Protected Sub DeleteTemporaryResource(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles GridViewTemporalResources.RowDeleting
        e.Cancel = True
        Dim sequenceIdToDelete As Integer = e.Values("SequenceId")
        Dim consequenceIdToDelete As Integer = e.Values("ConsequenceId")
        Dim resources As List(Of ResourceDTO) = GetNewResources()

        Dim deletedResource As ResourceDTO = (From r In resources Where r.SequenceId = sequenceIdToDelete And r.ConsequenceId = consequenceIdToDelete Select r).FirstOrDefault()

        resources.Remove(deletedResource)
        ReloadGridViewNewResources()
    End Sub

    Protected Sub UpdateNewResource(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridViewTemporalResources.RowUpdating
        e.Cancel = True
        Dim listado As List(Of ResourceDTO) = GetNewResources()
        Dim previousValues As ResourceDTO = (From ant In listado Where ant.SequenceId = CInt(e.Keys("SequenceId")) And ant.ConsequenceId = CInt(e.Keys("ConsequenceId")) Select ant).FirstOrDefault()

        previousValues.Description = e.NewValues("Description")

        ReloadGridViewNewResources()
        EndEditionInGridViewNewResources()
    End Sub

    Protected Sub DeleteNewResourcesFromMemory()
        'Session("GridViewTemporalResources") = Nothing
        listadoNuevosResources = Nothing
        'TODO Eliminar la variable de session
    End Sub




    ''' <summary>
    ''' It generates tags 
    ''' </summary>
    ''' <param name="sequenceId">SequenceId</param>
    ''' <param name="consequenceId">ConsequenceId</param>
    ''' <returns></returns>
    Protected Function GenerateTags(sequenceId As Integer, consequenceId As Integer) As HashSet(Of TagDTO)
        Dim sentFromRequirementTab As String = Request.QueryString("SentFromRequirementTab")
        Dim uwCase As UnderwritingCase = DirectCast((Session("UnderwritingCaseID")), InMotionGIT.Underwriting.Contracts.UnderwritingCase)
        Dim tagsResult As New HashSet(Of TagDTO)

        Dim caseIdTag = GenerateCaseIdTag(sequenceId, consequenceId, sentFromRequirementTab, uwCase)
        If caseIdTag IsNot Nothing Then
            tagsResult.Add(caseIdTag)
        End If

        Dim requirementId = GenerateRequirementId(sequenceId, consequenceId, sentFromRequirementTab, uwCase)
        If requirementId IsNot Nothing Then
            tagsResult.Add(requirementId)
        End If
        Return tagsResult
    End Function

    ''' <summary>
    ''' It generates tags 
    ''' </summary>
    ''' <param name="uwCase">Underwriting Case</param>
    ''' <returns></returns>
    Protected Function GenerateTagsCaseId(uwCase As UnderwritingCase) As ShortTagDTO
        Dim tagsResult As New ShortTagDTO
        tagsResult.Content = uwCase.UnderwritingCaseID
        tagsResult.TagTypeId = tagTypeIds("CaseId")
        Return tagsResult
    End Function

    Private Function GenerateRequirementId(sequenceId As Integer, consequenceId As Integer, sentFromRequirementTab As String, uwCase As UnderwritingCase) As TagDTO
        Dim requirementId As ShortTagDTO = GetRequirementId(sentFromRequirementTab, uwCase)
        If requirementId IsNot Nothing Then
            Dim tagDTO As New TagDTO
            tagDTO.SequenceId = sequenceId
            tagDTO.ConsequenceId = consequenceId
            tagDTO.TagTypeId = requirementId.TagTypeId
            tagDTO.Content = requirementId.Content
            Return tagDTO
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' It generates tagId Case
    ''' </summary>
    ''' <param name="sequenceId">SequenceId</param>
    ''' <param name="consequenceId">ConsequenceId</param>
    ''' <returns></returns>
    Protected Function GenerateCaseIdTag(sequenceId As Integer, consequenceId As Integer, sentFromRequirementTab As String, uwCase As UnderwritingCase) As TagDTO
        Dim tagDTO As New TagDTO
        tagDTO.SequenceId = sequenceId
        tagDTO.ConsequenceId = consequenceId
        If uwCase.IsNotEmpty Then
            tagDTO.Content = uwCase.UnderwritingCaseID
        End If

        tagDTO.TagTypeId = tagTypeIds("CaseId")
        Return tagDTO
    End Function

    ''' <summary>
    ''' It saves a resource in the database with a temporary state. It invoques the web service based on the the file type that is being saved
    ''' </summary>
    ''' <param name="resourceDTO">Resource to be saved</param>
    Private Sub SaveResource(resourceDTO As ResourceDTO)
        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddResourceWithTemporaryState(resourceDTO, GetToken(), provider)
        'Select Case resourceDTO.ResourceTypeId
        '    Case 1
        '        Dim imageDTO As New ImageDTO
        '        'imageDTO = resourceDTO.Image
        '        'resourceDTO.Image = Nothing
        '        'imageDTO.Resource = resourceDTO
        '        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddResourceWithTemporaryState(resourceDTO, ResponseHelper.GetValidToken(), provider)
        '    Case 2
        '        Dim videoDTO As New VideoDTO
        '        videoDTO = resourceDTO.Video
        '        resourceDTO.Video = Nothing
        '        videoDTO.Resource = resourceDTO
        '        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddVideoWithTemporaryState(videoDTO, ResponseHelper.GetValidToken(), provider)
        '    Case 3
        '        Dim audioDTO As New AudioDTO
        '        audioDTO = resourceDTO.Sound
        '        resourceDTO.Sound = Nothing
        '        audioDTO.Resource = resourceDTO
        '        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddAudioWithTemporaryState(audioDTO, ResponseHelper.GetValidToken(), provider)

        '    Case 4
        '        Dim documentDTO As New DocumentDTO
        '        documentDTO = resourceDTO.Document
        '        resourceDTO.Document = Nothing
        '        documentDTO.Resource = resourceDTO
        '        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddDocumentWithTemporaryState(documentDTO, ResponseHelper.GetValidToken(), provider)
        'End Select

    End Sub

    Private Function ValidateResources(resourcesToBeSaved As List(Of ResourceDTO)) As Boolean
        Dim errorMessage = ""
        Dim result = True
        Dim count = 1
        'For Each resource As ResourceDTO In resourcesToBeSaved
        '    If (IsNothing(resource.Description)) OrElse (resource.Description.Equals(String.Empty)) Then
        '        errorMessage = errorMessage + " Error: Row:" + resource.ConsequenceId.ToString + ", the field description should not be empty." + Environment.NewLine
        '        result = False
        '    End If
        '    If resource.ExpirationDate.Equals(String.Empty) Then
        '        errorMessage = errorMessage + "Error: Row:" + resource.ConsequenceId.ToString + ", the field expiration date should not be empty." + Environment.NewLine
        '        result = False
        '    End If

        'Next

        If Not result Then

        End If

        Return result
    End Function

    ''' <summary>
    ''' It generates a Resource item based on a posted file.
    ''' </summary>
    ''' <param name="file">HttpPostedfile</param>
    ''' <param name="idConsecutivo">IdConsecutivo</param>
    ''' <returns></returns>
    Private Function GenerateResourceBasedOnFile(file As HttpPostedFile, idConsecutivo As Integer) As ResourceDTO
        Dim fileName As String
        fileName = Path.GetFileName(file.FileName)
        Dim resourceDTO As New ResourceDTO
        resourceDTO.SequenceId = DneSequenceId
        resourceDTO.ConsequenceId = idConsecutivo
        resourceDTO.Name = fileName
        resourceDTO.ResourceTypeId = GetFileType(fileName)
        LoadResource(resourceDTO, file)
        'resourceDTO.ExpirationDate = Date.Now

        'TODO estas dos propiedades tienen que ser puestas como propiedades del control
        resourceDTO.ClientAssociatedCompany = 1
        resourceDTO.ClientAssociatedPerson = 1

        Dim tag As New List(Of ShortTagDTO)
        tag = GenerateRequiredTagsForInserting()

        'tag.TagTypeId = 1
        'tag.Content = "_FormID.Text" '_FormID.Text 'Gets the GUID to be saved in the 
        'resourceDTO.Tags = tag.[Select](Function(x) New TagDTO() With {TagTypeId = x.Content}).ToList()
        resourceDTO.Tags = New HashSet(Of TagDTO)(tag.ConvertAll(New Converter(Of ShortTagDTO, TagDTO)(AddressOf ConvertirShortTagDTOATagDTO)))
        Return resourceDTO
    End Function

    Private Function ConvertirShortTagDTOATagDTO(ByVal shorTagDTO As ShortTagDTO) As TagDTO
        Dim tagDTO As New TagDTO()
        tagDTO.Content = shorTagDTO.Content
        tagDTO.TagTypeId = shorTagDTO.TagTypeId
        Return tagDTO
    End Function

    ''' <summary>
    ''' Loads the resource selected by the user and keeps it in the resource object
    ''' </summary>
    ''' <param name="resourceDTO">The object that will keep the resource</param>
    ''' <param name="file">File uploaded by the user</param>
    Private Sub LoadResource(ByRef resourceDTO As ResourceDTO, file As HttpPostedFile)
        Dim binaryReader As New BinaryReader(file.InputStream)
        Select Case resourceDTO.ResourceTypeId
            Case 1
                resourceDTO.Image = New ImageDTO
                resourceDTO.Image.SequenceId = resourceDTO.SequenceId
                resourceDTO.Image.OriginalImage = binaryReader.ReadBytes(file.ContentLength)
            Case 2
                resourceDTO.Video = New VideoDTO
                resourceDTO.Video.SequenceId = resourceDTO.SequenceId
                resourceDTO.Video.Video = binaryReader.ReadBytes(file.ContentLength)
            Case 3
                resourceDTO.Audio = New AudioDTO
                resourceDTO.Audio.SequenceId = resourceDTO.SequenceId
                resourceDTO.Audio.Sound = binaryReader.ReadBytes(file.ContentLength)

            Case 4
                resourceDTO.Document = New DocumentDTO
                resourceDTO.Document.SequenceId = resourceDTO.SequenceId
                resourceDTO.Document.Document = binaryReader.ReadBytes(file.ContentLength)

        End Select
    End Sub

    ''' <summary>
    ''' Gets File type given a filename
    ''' </summary>
    ''' <param name="fileName">File name</param>
    ''' <returns></returns>
    Private Function GetFileType(fileName As String) As Integer
        Dim extension As String
        If fileName.Contains(".") Then
            extension = fileName.Substring(fileName.LastIndexOf(".")).ToUpper()

            Dim imageAcceptedExtensions = fileExtensionsPermited(0).Value
            Dim videoAcceptedExtensions = fileExtensionsPermited(1).Value
            Dim audioAcceptedExtensions = fileExtensionsPermited(2).Value
            Dim documentAcceptedExtensions = fileExtensionsPermited(3).Value

            If imageAcceptedExtensions.Contains(extension) Then
                Return resourceTypes("Image")
            ElseIf videoAcceptedExtensions.Contains(extension) Then
                Return resourceTypes("Video")
            ElseIf audioAcceptedExtensions.Contains(extension) Then
                Return resourceTypes("Audio")
            ElseIf documentAcceptedExtensions.Contains(extension) Then
                Return resourceTypes("Document")
            End If
        End If
        Return -1          'Si la extension recibida no esta en ninguno de las extensiones permitidas, retorna -1 
    End Function

    Protected Sub ShowNewResourcesPanel(sender As Object, e As EventArgs) Handles LinkButtonNewItemsId.Click
        'LinkButtonNewItemsId.Visible = False
        Dim resources As List(Of ResourceDTO) = GetNewResources()
        resources.Clear()
        ReloadGridViewNewResources()
        DisableControl()
        NewResources.Visible = True
    End Sub

    Protected Sub DisableControl()
        GridViewActiveResources.Enabled = False
        LinkButtonNewItemsId.Visible = False
        ButtonFilter.Enabled = False
        'DNEFilterBox.Style("Background-Color") = "#6E6E6E"
        'ActiveResources.Style("Background-Color") = "#6E6E6E"
    End Sub

    Protected Sub ShowPanelWithActiveResources()
        GridViewActiveResources.Visible = True
        GridViewActiveResources.DataBind()
        NewResources.Visible = False
    End Sub

    Protected Sub GridviewRecursosActivos_FocusedRowChanged(sender As Object, e As EventArgs)
        Dim resourceDTO As ResourceDTO = GridViewActiveResources.GetRow(GridViewActiveResources.FocusedRowIndex)
        DownloadResource(resourceDTO)
    End Sub

    Protected Sub GridViewActiveResources_CustomUnboundColumnData(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs)
        If (e.Column.FieldName.Equals("CreatorUserNameUW")) Then
            Dim resourceDTO As ResourceDTO = GridViewActiveResources.GetRow(e.ListSourceRowIndex)
            e.Value = GetUserName(resourceDTO.CreatorUserCode)
        End If
    End Sub

    Private Function GetUserName(userCode As Integer) As String
        Dim lstUser = InMotionGIT.Membership.Providers.Helpers.User.UserLkp()
        Dim userName = (From x In lstUser Where x.Code = userCode Select x.Description).FirstOrDefault()
        If (IsNothing(userName) OrElse userName.IsEmpty()) Then ' It means that the user is located in Security
            Return InMotionGIT.General.Proxy.Security.UserName(userCode)
        Else
            Return userName
        End If
    End Function

    Protected Function GetFrontOfficeUserName(userCode As Integer) As String
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

#End Region


    Protected Sub ObjectDataSourceActiveResources_Selected(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles ObjectDataSourceActiveResources.Selected
        gridviewActiveElements = e.ReturnValue()
    End Sub

    Protected Sub GridViewActiveResources_CommandButtonInitialize(sender As Object, e As ASPxGridView.ASPxGridViewCommandButtonEventArgs) Handles GridViewActiveResources.CommandButtonInitialize
        Dim selectedCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        If (Not IsNothing(selectedCase) AndAlso selectedCase.Status = InMotionGIT.Underwriting.Contracts.Enumerations.EnumUnderwritingCaseStatus.Consultation) Then
            e.Enabled = False
        Else
            If (Not IsNothing(selectedCase) AndAlso Not IsNothing(selectedCase.LockedOn) AndAlso (selectedCase.LockedBy = Session("nUserCode") OrElse selectedCase.LockedBy = Session("UserId"))) Then
                e.Enabled = True
            Else
                e.Enabled = False
            End If
        End If
    End Sub
    Protected Sub GridViewActiveResources_CustomButtonInitialize(sender As Object, e As ASPxGridView.ASPxGridViewCustomButtonEventArgs)
        Dim selectedCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        If (Not IsNothing(selectedCase) AndAlso selectedCase.Status = InMotionGIT.Underwriting.Contracts.Enumerations.EnumUnderwritingCaseStatus.Consultation) Then
            e.Enabled = False
        Else
            If (Not IsNothing(selectedCase) AndAlso Not IsNothing(selectedCase.LockedOn) AndAlso (selectedCase.LockedBy = Session("nUserCode") OrElse selectedCase.LockedBy = Session("UserId"))) Then
                e.Enabled = True
            Else
                e.Enabled = False
            End If
        End If
    End Sub

    Protected Function GetToken() As String
        Try
            Return TokenHelper.GetValidToken()
        Catch ex As Exception
            Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
            Response.Write(String.Format("<script type=text/javascript> window.parent.location.href ='{0}/dropthings/Default.aspx?SessionTimeOut=Yes' </script>", baseUrl))
            Response.Flush()
            Response.End()
        End Try
        Return String.Empty
    End Function
End Class


<Serializable()>
Public Class Form1ParameterFiles

    ' Methods
    Public Sub New()
        'Tags utilizados de ejemplo
        MyBase.New()
        'tagListDTO = New List(Of ShortTagDTO)
        'TagWithEnumDTO = New ShortTagDTO
        'TagWithEnumDTO.Content = "IdFormulario"
        'TagWithEnumDTO.TagTypeId = 1
        'tagListDTO.Add(TagWithEnumDTO)

        'TagWithEnumDTO = New ShortTagDTO
        'TagWithEnumDTO.Content = "1234"
        'TagWithEnumDTO.TagTypeId = 2
        'tagListDTO.Add(TagWithEnumDTO)

        'Parameter2 = 138
    End Sub


    ' Properties
    Public Property SequenceId As String
    Public Property tagListDTO As List(Of InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ShortTagDTO)
    Public Property TagWithEnumDTO As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ShortTagDTO


End Class