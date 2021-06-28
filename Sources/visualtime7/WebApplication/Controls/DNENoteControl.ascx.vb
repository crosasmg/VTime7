#Region "Imports"

Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.Modelo
Imports System.IO
Imports InMotionGIT.FrontOffice.Support
Imports System.Data
Imports System.Globalization
Imports System.Net
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxPanel
Imports System.ComponentModel
Imports DevExpress.Web
Imports System.Reflection

#End Region

Partial Public Class Controls_DNENoteControl
    Inherits System.Web.UI.UserControl

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

    Dim gvNoteElements As New List(Of ResourceDTO)
    Dim provider As String = GetDNEProvider()


#Region "Fields"
    Private _visible As Boolean = True
    Private _enabled As Boolean = True
    Private _formId As String
    Private _selectMethod As String
    Private _roleName As String
    Private _DNENoteTags As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
    Private _DNENoteTagsFilter As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
    Private _underwritingCaseId As Integer
    Private _DNESequenceId As Integer
    Private _useTemporaryNotes As Boolean = True

#End Region


    ''' <summary>
    ''' Propiedad publica para colocar el Id DNE Secuencia en el user control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>    
    Public Property DNESequenceId As Integer
        Get
            Return ViewState(Me.ID.ToString + "DNESequenceId")
        End Get
        Set(ByVal value As Integer)
            _DNESequenceId = value
            ViewState(Me.ID.ToString + "DNESequenceId") = value
        End Set
    End Property


    ''' <summary>
    '''Property that makes the controls visible or invisible/Propiedad que hace visible o invisible los controls
    ''' </summary>
    ''' <value>Property condition visible / hidden. /Estado de la propiedad visible/oculto</value>
    ''' <returns>Property condition or not available</returns>
    ''' <remarks></remarks>
    Public Overrides Property Visible As Boolean
        Get
            Return _visible
        End Get
        Set(value As Boolean)
            SetVisible(value)
            _visible = value
            ViewState(Me.ID.ToString + "Visible") = value
        End Set
    End Property

    ''' <summary>
    '''Allocation method embodying the controls to make visible or not /Método que realiza la asignación a los controles para hacer visible o no
    ''' </summary>
    ''' <param name="Value">State of the control</param>
    ''' <remarks></remarks>
    Sub SetVisible(Value As Boolean)
        Notes.Visible = Value
    End Sub

    ''' <summary>
    '''Property that enables or disables the controls/Propiedad que habilita o deshabilita los controls
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Enabled As Boolean
        Get
            Return _enabled
        End Get
        Set(value As Boolean)
            SetEnable(value)
            _enabled = value
            ViewState(Me.ID.ToString + "Enabled") = value
        End Set
    End Property

    ''' <summary>
    '''Allocation method that performs the checks for available or not/Método que realiza la asignación a los controles para hacer disponible o no
    ''' </summary>
    ''' <param name="Value">State of the control</param>
    ''' <remarks></remarks>
    Sub SetEnable(Value As Boolean)
        btnNewNote.Enabled = Value
        gvNotes.Columns(0).Visible = Value
    End Sub

    Public Property FormId As String
        Get
            Return ViewState(Me.ID.ToString + "FormId")
        End Get
        Set(ByVal value As String)
            _formId = value
            ViewState(Me.ID.ToString + "FormId") = value
        End Set
    End Property

    Public Property RoleName As String
        Get
            Return ViewState(Me.ID.ToString + "RoleName")
        End Get
        Set(ByVal value As String)
            _roleName = value
            ViewState(Me.ID.ToString + "RoleName") = value
        End Set
    End Property

    Public Property DNENoteTags As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
        Get
            Return ViewState(Me.ID.ToString + "DNENoteTags")
        End Get
        Set(ByVal value As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO)
            _DNENoteTags = value
            ViewState(Me.ID.ToString + "DNENoteTags") = value
        End Set
    End Property

    Public Property DNENoteTagsFilter As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
        Get
            Return ViewState(Me.ID.ToString + "DNENoteTagsFilter")
        End Get
        Set(ByVal value As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO)
            _DNENoteTagsFilter = value
            ViewState(Me.ID.ToString + "DNENoteTagsFilter") = value
        End Set
    End Property

    Public Property UnderwritingCaseId As Integer
        Get
            Return ViewState(Me.ID.ToString + "UnderwritingCaseId")
        End Get
        Set(ByVal value As Integer)
            _underwritingCaseId = value
            ViewState(Me.ID.ToString + "UnderwritingCaseId") = value
        End Set
    End Property

    Public Property UseTemporaryNotes As Boolean
        Get
            Return ViewState(Me.ID.ToString + "UseTemporaryNotes")
        End Get
        Set(ByVal value As Boolean)
            _useTemporaryNotes = value
            ViewState(Me.ID.ToString + "UseTemporaryNotes") = value
        End Set
    End Property

    Public Property selectMethod As String
        Get
            Return ViewState(Me.ID.ToString + "SelectMethod")
        End Get
        Set(ByVal value As String)
            _selectMethod = value
            odsNotes.SelectMethod = selectMethod
            ViewState(Me.ID.ToString + "SelectMethod") = value
        End Set
    End Property



#Region "Methods"


#End Region

#Region "Configurations"

#End Region

#Region "Methods to deal with temporal Resources"
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.Page.EnableEventValidation = False
    End Sub

    Private Function GetDNEProvider() As String
        Dim provider = ConfigurationManager.AppSettings.Get("DNEProvider")
        If (provider IsNot Nothing) Then
            Return provider.ToUpper()
        End If
        Return "DNE"
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'DeleteNewResourcesFromMemory()
            'DefineSequenceCreation()
        Else
            LoadTagCheckBoxes()
            odsNotes.SelectMethod = ViewState(Me.ID + "SelectMethod")
        End If
    End Sub

    Protected Sub UpdatePanel_Unload(ByVal sender As Object, ByVal e As EventArgs)
        RegisterUpdatePanel(DirectCast(sender, UpdatePanel))
    End Sub

    'Protected Sub AddNewRow(sender As Object, e As EventArgs) Handles btnSubmitFileUpload.Click
    '    gvNotes.AddNewRow()
    'End Sub

    Protected Sub RegisterUpdatePanel(ByVal panel As UpdatePanel)
        Dim sType = GetType(ScriptManager)
        Dim mInfo = sType.GetMethod("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel", BindingFlags.NonPublic Or BindingFlags.Instance)
        If mInfo IsNot Nothing Then
            mInfo.Invoke(ScriptManager.GetCurrent(Page), New Object() {panel})
        End If
    End Sub

    Public Sub DefineSequenceCreation()
        If ViewState(Me.ID.ToString + "Visible") Then
            LoadTagCheckBoxes()
            btnNewNote.Visible = ViewState(Me.ID.ToString + "Enabled")
            selectMethod = GetOperationContractMethodToGetNotes(ViewState(Me.ID.ToString + "UseTemporaryNotes"), ViewState(Me.ID.ToString + "RoleName"))

            Dim underWritingCase = InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.SelectAll(ViewState(Me.ID.ToString + "UnderwritingCaseId"), False, False)
            If (IsNumeric(DNESequenceId)) AndAlso Not selectMethod.Equals("") AndAlso Not IsNothing(underWritingCase) AndAlso underWritingCase.UnderwritingCaseID > 0 Then
                'If (IsNumeric(ParameterFullName)) AndAlso Not selectedMethod.Equals("") Then
                CleanViewStateBasedOnSequenceId(ViewState(Me.ID.ToString + "DNESequenceId"), DNESequenceId) 'Cuando cambia el Numero del IdSecuencia
                ViewState(Me.ID.ToString + "DNESequenceId") = DNESequenceId
                If (DNESequenceId = 0) Then
                    DNESequenceId = CreateSequence()
                    'SaveDNESequenceIdInUnderwritingDatabase(underWritingCase)
                End If
                If Not selectMethod.Equals("") Then   ' Si existe un SelectedMethod
                    odsNotes.SelectMethod = _selectMethod
                    odsNotes.SelectParameters.Clear()
                    odsNotes.SelectParameters.Add(New Parameter("sequenceId", DbType.Int32, DNESequenceId.ToString()))
                    odsNotes.SelectParameters.Add(New Parameter("tags", TypeCode.Object)) ' This variable will be load once the select method gets triggered
                    odsNotes.SelectParameters.Add(New Parameter("accessToken", DbType.String, ResponseHelper.GetValidToken()))
                    odsNotes.SelectParameters.Add(New Parameter("selectNotesOnly", DbType.Boolean, True.ToString))
                    odsNotes.SelectParameters.Add(New Parameter("provider", DbType.String, provider))
                    gvNotes.DataSourceID = odsNotes.ID
                    odsNotes.Select()
                    CreateTagCheckBoxes()
                    CheckFiltersBasedOnUsersInput()
                    'RepaintControlBasedOnNumberRows()
                End If
            Else
                ClearContent()
            End If
        End If
    End Sub

    Protected Sub Notes_Callback(sender As Object, e As EventArgs)
        If (Request.Browser.Browser <> "InternetExplorer") Then
            Dim editor As ASPxHtmlEditor.ASPxHtmlEditor = TryCast(sender, ASPxHtmlEditor.ASPxHtmlEditor)
            editor.Settings.AllowDesignView = True
        End If
    End Sub

    ''' <summary>
    ''' It creates or generates a sequence if there is no sequence releated to the CaseId in the DB
    ''' </summary>
    ''' <returns>SequenceId</returns>
    Protected Function CreateSequence() As Integer
        Dim sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.SequenceExistInTags(ViewState(Me.ID.ToString + "UnderwritingCaseId"), ResponseHelper.GetValidToken(), provider)
        If sequenceId = -1 Then
            sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GenerateSequence(ResponseHelper.GetValidToken(), provider)
        End If
        Return sequenceId
        'Dim sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GenerateSequence(Session("AccessToken"), provider)        
        'Return sequenceId
    End Function


    Private Sub CleanViewStateBasedOnSequenceId(previousSequenceId As Integer, currentSequenceId As Integer)
        If previousSequenceId <> currentSequenceId Then
            ViewState(Me.ID.ToString() + "TagCheckBoxes") = Nothing
            tagFilterContainer.ContentTemplateContainer.Controls.Clear()
        End If
    End Sub

    'Private Sub RepaintControlBasedOnNumberRows()
    '    If gridviewActiveElements.Count > 0 Then
    '        MakeControlWithElements()
    '    Else
    '        MakeControlWithNoElements()
    '    End If
    'End Sub

    ''' <summary>
    ''' This method clears the content of the Control when SequenceId is null
    ''' </summary>
    Private Sub ClearContent()
        ViewState(Me.ID.ToString + "SelectMethod") = String.Empty
        Me.tagFilterContainer.ContentTemplateContainer.Controls.Clear()
        gvNotes.DataSourceID = String.Empty
        gvNotes.DataBind()
        'gvNotes.Enabled = False
        btnNewNote.Visible = False
    End Sub

    Private Sub CreateTagCheckBoxes()
        If IsNumeric(ViewState(Me.ID.ToString + "DNESequenceId")) Then
            Dim checkBoxes As Dictionary(Of String, Boolean) = GetCheckBoxesFromViewState()
            'Dim tagList As List(Of ShortTagDTO) = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetTagsRelatedToNoteActiveAndTemporarySequence(FormData.UnderwritingCase., ResponseHelper.GetValidToken())
            Dim tagList As List(Of TagDTO) = GetTagsFromElementsInGridViewActive()
            'Dim tagListFiltered As List(Of TagDTO) = tagList.Where(Function(x) x.TagTypeId <> tagTypeIds("FormId") And x.TagTypeId <> tagTypeIds("CaseId")).Distinct.ToList()
            Dim tagListFiltered As List(Of TagDTO) = tagList.Where(Function(x) x.TagTypeId <> tagTypeIds("FormId") And x.TagTypeId <> tagTypeIds("CaseId") And x.TagTypeId <> tagTypeIds("RequirementId")).Distinct.ToList()
            Dim checkBoxId As String
            For Each tag As TagDTO In tagListFiltered
                checkBoxId = Me.ID + "|" + tagTypeIds.ElementAt(tag.TagTypeId - 1).Value.ToString() + "|" + tag.Content.ToString
                If Not CheckBoxExist(checkBoxId) Then
                    Dim checkBox As New CheckBox()
                    checkBox.ID = checkBoxId
                    checkBox.Text = tag.Content.ToString
                    checkBoxes.Add(checkBox.ID, False)
                    checkBox.Enabled = True
                    Me.tagFilterContainer.ContentTemplateContainer.Controls.Add(checkBox)
                End If
            Next
            ViewState(Me.ID.ToString + "TagCheckBoxes") = checkBoxes
            Me.tagFilterContainer.Update() 'TODO Updates the panel once new checkboxes gets add
        End If
    End Sub

    ''' <summary>
    ''' Gets the tags involved in GridViewActiveResources
    ''' </summary>
    ''' <returns></returns>
    Private Function GetTagsFromElementsInGridViewActive() As List(Of TagDTO)
        Dim tagsInActiveGridView As New List(Of TagDTO)
        For Each resource In gvNoteElements
            If resource.Tags IsNot Nothing Then
                tagsInActiveGridView.AddRange(resource.Tags)
            End If
        Next
        Return tagsInActiveGridView
    End Function

    ''' <summary>
    ''' It obtains the elements from the viewState("TagCheckBoxes") 
    ''' </summary>
    ''' <returns></returns>
    Private Function GetCheckBoxesFromViewState() As Dictionary(Of String, Boolean)
        Dim checkBoxes As New Dictionary(Of String, Boolean)
        If ViewState(Me.ID.ToString + "TagCheckBoxes") IsNot Nothing Then
            For Each checkbox As KeyValuePair(Of String, Boolean) In ViewState(Me.ID.ToString + "TagCheckBoxes")
                checkBoxes.Add(checkbox.Key, checkbox.Value)
            Next
        End If
        Return checkBoxes
    End Function

    ''' <summary>
    ''' create the checkboxes elements based on the elements saved in ViewState
    ''' </summary>
    Private Sub LoadTagCheckBoxes()
        If ViewState(Me.ID.ToString + "TagCheckBoxes") IsNot Nothing Then
            For Each checkBoxElement As KeyValuePair(Of String, Boolean) In ViewState(Me.ID.ToString + "TagCheckBoxes")
                Dim checkBox As New CheckBox()
                checkBox.ID = checkBoxElement.Key
                checkBox.Text = checkBoxElement.Key.Split("|")(2)
                checkBox.Checked = checkBoxElement.Value
                'If Not Me.tagFilterContainer.ContentTemplateContainer.Controls.OfType(Of CheckBox).Contains(checkBox) Then
                If Not CheckBoxExist(checkBox.ID) Then
                    Me.tagFilterContainer.ContentTemplateContainer.Controls.Add(checkBox)
                End If
            Next
        End If'
    End Sub

    ''' <summary>
    ''' Verifies whether tagFilterContainer panel contains a checkbox with the given ID
    ''' </summary>
    ''' <param name="checkBoxId"></param>
    ''' <returns></returns>
    Private Function CheckBoxExist(checkBoxId As String) As Boolean
        For Each checkBox As CheckBox In Me.tagFilterContainer.ContentTemplateContainer.Controls.OfType(Of CheckBox)
            If checkBox.ID = checkBoxId Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' The user creates tag filters in Design time, this filters gets checked in the Page
    ''' </summary>
    Private Sub CheckFiltersBasedOnUsersInput()
        Dim taglistDTO As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
        taglistDTO = ViewState(Me.ID.ToString + "DNENoteTagsFilter")
        If taglistDTO IsNot Nothing Then
            For Each tag As ShortTagDTO In taglistDTO
                For Each checkBox As CheckBox In Me.tagFilterContainer.ContentTemplateContainer.Controls.OfType(Of CheckBox)
                    If checkBox.ID.Equals(Me.ID + "|" + tagTypeIds(tag.TagTypeId.ToString).ToString + "|" + tag.Content) Then
                        checkBox.Checked = True
                    End If
                Next
            Next
        End If
    End Sub

    '''' <summary>
    '''' Gets the value of a property if exists, otherwise it returns null
    '''' </summary>
    '''' <param name="objectt"></param>
    '''' <param name="propertyy"></param>
    '''' <returns></returns>
    'Private Function GetProperty(ByVal objectt As Object, ByVal propertyy As String)
    '    Dim type As Type = objectt.GetType
    '    If type.GetProperty(propertyy) IsNot Nothing Then
    '        Return type.GetProperty(propertyy).GetValue(objectt, Nothing)    'This returns the property info        
    '    End If
    '    Return Nothing
    'End Function

    ''' <summary>
    ''' It generates tags 
    ''' </summary>    
    ''' <returns></returns>
    Protected Function GenerateTags(sequenceId As Integer, consequenceId As Integer) As HashSet(Of TagDTO)
        Dim tagsResult As New HashSet(Of TagDTO)
        tagsResult = GetTagsFromDesigner(sequenceId, consequenceId)
        tagsResult.Add(GenerateFormIdTag())
        Return tagsResult
    End Function

    ''' <summary>
    ''' Gets the tags selected in the Formdesigner and converts them from ShortTag to Tag
    ''' </summary>
    ''' <returns></returns>
    Protected Function GetTagsFromDesigner(sequenceId As Integer, consequenceId As Integer) As HashSet(Of TagDTO)
        Dim tagsToAdd As New HashSet(Of TagDTO)
        Dim tagsToBeAdded As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
        tagsToBeAdded = ViewState(Me.ID.ToString + "DNENoteTags")
        If tagsToBeAdded IsNot Nothing Then
            Dim tagDTO As TagDTO
            For Each shortTag As ShortTagDTO In tagsToBeAdded
                tagDTO = New TagDTO
                tagDTO.SequenceId = sequenceId
                tagDTO.ConsequenceId = consequenceId
                tagDTO.Content = shortTag.Content
                tagDTO.TagTypeId = shortTag.TagTypeId
                tagsToAdd.Add(tagDTO)
            Next
        End If
        Return tagsToAdd
    End Function

    ''' <summary>
    ''' It generates generates FormId tag
    ''' </summary>
    ''' <returns></returns>
    Protected Function GenerateFormIdTag() As TagDTO
        Dim tagDTO As New TagDTO
        tagDTO.TagTypeId = tagTypeIds("FormId")
        tagDTO.Content = ViewState(Me.ID.ToString + "FormId")
        Return tagDTO
    End Function

    Protected Sub odsNotes_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsNotes.Selecting
        odsNotes.SelectCountMethod = ViewState(Me.ID + "SelectMethod")
        If IsNumeric(ViewState(Me.ID.ToString + "DNESequenceId")) Then
            e.InputParameters.Item("tags") = GenerateTagsForSelecting()
        Else
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' It generates tags based on the filters checked and the FormId
    ''' </summary>
    ''' <returns></returns>
    Protected Function GenerateTagsForSelecting() As List(Of ShortTagDTO)
        Dim tagsForSelecting As New List(Of ShortTagDTO)
        tagsForSelecting = GetFiltersChecked()
        tagsForSelecting.AddRange(GetInvisibleFilterTags())
        tagsForSelecting.Add(GenerateFormIdTagForSelecting())
        Return tagsForSelecting
    End Function

    ''' <summary>
    ''' This method adds the tags that are not being shown in the Form. These tags could be Requirement, Form, and CaseId.
    ''' </summary>
    ''' <returns></returns>
    Protected Function GetInvisibleFilterTags() As List(Of ShortTagDTO)
        Dim tagList As TagListDTO = ViewState(Me.ID + "DNEFileTagsFilter")
        Dim listInvisibleTags As New List(Of ShortTagDTO)
        If tagList IsNot Nothing Then
            listInvisibleTags = tagList.Where(Function(x) x.TagTypeId = tagTypeIds("FormId") Or x.TagTypeId = tagTypeIds("CaseId") Or x.TagTypeId = tagTypeIds("RequirementId")).Distinct.ToList()
        End If
        Return listInvisibleTags
    End Function


    ''' <summary>
    ''' It generates FormId as ShortTagDTO 
    ''' </summary>    
    ''' <returns></returns>
    Protected Function GenerateFormIdTagForSelecting() As ShortTagDTO
        Dim formIDShortTag As New ShortTagDTO

        formIDShortTag.Content = ViewState(Me.ID.ToString + "FormId")
        formIDShortTag.TagTypeId = tagTypeIds("FormId")
        Return formIDShortTag
    End Function

    ''' <summary>
    ''' It returns the Tags (checkboxes) that are checked.
    ''' </summary>
    ''' <returns></returns>
    Private Function GetFiltersChecked() As List(Of ShortTagDTO)
        Dim tagList As New List(Of ShortTagDTO)
        Dim shortTag As ShortTagDTO
        For Each checkBox As CheckBox In Me.tagFilterContainer.ContentTemplateContainer.Controls.OfType(Of CheckBox)
            If checkBox.Checked Then
                shortTag = New ShortTagDTO
                shortTag.TagTypeId = checkBox.ID.Split("|")(1)
                shortTag.Content = checkBox.Text
                tagList.Add(shortTag)
            End If
        Next
        Return tagList
    End Function

    Protected Sub FilterButton(sender As Object, e As EventArgs)
        UpdateViewStateTags()
        gvNotes.DataBind()
    End Sub

    ''' <summary>
    ''' Updates the variable TagCheckBoxes in Viewstate based on the checkboxes checked. 
    ''' </summary>
    Private Sub UpdateViewStateTags()
        Dim checkBoxes As Dictionary(Of String, Boolean) = ViewState(Me.ID.ToString + "TagCheckBoxes")
        For Each checkBox As CheckBox In Me.tagFilterContainer.ContentTemplateContainer.Controls.OfType(Of CheckBox)
            checkBoxes(checkBox.ID) = checkBox.Checked
        Next
        ViewState(Me.ID.ToString + "TagCheckBoxes") = checkBoxes
    End Sub

    Private Function GetOperationContractMethodToGetNotes(useOnlyTemporaryNotes As Boolean, userRole As String) As String
        Dim selectMethod = ""
        'Dim rolesCliente = ConfigurationManager.AppSettings.Get("Cliente").Split(";")
        'Dim rolesAdministrador = ConfigurationManager.AppSettings.Get("Administrador").Split(";")
        'Dim match As List(Of String)

        If userRole IsNot Nothing Then
            Dim userRoles = userRole.ToUpper.Split(";")
            If userRoles.Contains("SUSCRIPTOR") Or (userRoles.Contains("DNEADMIN")) Or (userRoles.Contains("DNEGET")) Then
                If useOnlyTemporaryNotes Then
                    selectMethod = "GetResourceSequenceTemporaryStateOnly"
                Else
                    selectMethod = "GetActiveResourceSequenceAndFormTemporals"
                End If
                If (userRoles.Contains("DNEGET")) Then
                    Enabled = False
                End If
            Else
                If (userRoles.Contains("DNEOWNER")) Then
                    If useOnlyTemporaryNotes Then
                        selectMethod = "GetOwnResourceSequenceTemporaryStateOnly"
                    Else
                        selectMethod = "GetOwnResourceSequenceActiveAndTemporaryState"
                    End If
                End If
            End If
        End If
        Return selectMethod
    End Function

    Protected Sub GridView_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        Dim composedResourceKey As New ComposedResourceKey(CInt(e.Values("SequenceId")), CInt(e.Values("ConsequenceId")))
        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.DeleteResource(composedResourceKey, ResponseHelper.GetValidToken(), provider)

        e.Cancel = True
        gvNotes.CancelEdit()
    End Sub

    Protected Sub GridView_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        Dim nameTextBoxValue As String = TryCast(gvNotes.FindEditFormTemplateControl("tbName"), ASPxTextBox).Text
        Dim expirationDate As New Nullable(Of DateTime)
        expirationDate = GetExpirationDateValue()

        Dim resourceDTO As New ResourceDTO()
        resourceDTO.Note = New NoteDTO()
        resourceDTO.Note.SequenceId = ViewState(Me.ID.ToString + "DNESequenceId")
        resourceDTO.Note.Content = DirectCast(e.NewValues("Note.Content"), [String])

        resourceDTO.SequenceId = ViewState(Me.ID.ToString + "DNESequenceId")
        resourceDTO.Name = nameTextBoxValue
        resourceDTO.ExpirationDate = expirationDate
        resourceDTO.Description = "No Description"
        resourceDTO.ClientAssociatedCompany = "1"
        resourceDTO.ClientAssociatedPerson = "1"

        resourceDTO.Tags = GenerateTags(resourceDTO.SequenceId, resourceDTO.ConsequenceId)

        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddResourceWithTemporaryState(resourceDTO, ResponseHelper.GetValidToken(), provider)

        e.Cancel = True
        gvNotes.CancelEdit()
        '      Dim nameTextBoxValue As String = TryCast(GridView.FindEditFormTemplateControl("TextBoxName"), ASPxTextBox).Text
        '      'Dim expirationDateTextBoxValue As DateTime = DirectCast(TryCast(GridView.FindEditFormTemplateControl("ExpirationDateText"), ASPxDateEdit).Value, DateTime)

        'Dim expirationDate As New Nullable(Of DateTime)
        '      expirationDate = GetExpirationDateValue()

        '      Dim noteDTO As New NoteDTO()
        '      noteDTO.SequenceId = FormData.UnderwritingCase.

        '      noteDTO.Resource.SequenceId = FormData.UnderwritingCase.
        '      noteDTO.Resource.Name = nameTextBoxValue
        '      noteDTO.Resource.ExpirationDate = expirationDate
        '      noteDTO.Resource.Description = "No Description"
        '      noteDTO.Resource.ClientAssociatedCompany = "1"
        '      noteDTO.Resource.ClientAssociatedPerson = "1"

        '      noteDTO.Content = DirectCast(e.NewValues("Note.Content"), [String])

        '      noteDTO.Resource.Tags = GenerateTags()		               

        '      InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddNoteWithTemporaryState(noteDTO, ResponseHelper.GetValidToken(), provider)

        '      e.Cancel = True
        '      GridView.CancelEdit()
        'CreateTagCheckBoxes()	' Once the new item gets created, if the element has new Tag inserted, this should be shown as well in the TagsContainer Panel
    End Sub

    Private Function GetExpirationDateValue() As Nullable(Of DateTime)
        Dim expirationDate As Nullable(Of DateTime)
        Dim expirationDateTextBox = TryCast(gvNotes.FindEditFormTemplateControl("deExpirationDate"), ASPxDateEdit).Value
        If expirationDateTextBox IsNot Nothing Then
            expirationDate = DirectCast(expirationDateTextBox, Nullable(Of DateTime))
        End If
        Return expirationDate
    End Function

    Protected Sub GridView_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Dim updatedResourceDTO As ResourceDTO = gvNotes.GetRow(gvNotes.EditingRowVisibleIndex)

        Dim nameTextTextBoxValue As String = TryCast(gvNotes.FindEditFormTemplateControl("tbName"), ASPxTextBox).Value
        Dim expirationDateEditValue As Nullable(Of DateTime) = TryCast(gvNotes.FindEditFormTemplateControl("deExpirationDate"), ASPxDateEdit).Value

        updatedResourceDTO.Name = nameTextTextBoxValue
        updatedResourceDTO.ExpirationDate = expirationDateEditValue
        updatedResourceDTO.Note.Content = DirectCast(e.NewValues("Note.Content"), [String])

        updatedResourceDTO.Tags = New HashSet(Of TagDTO)() From {GenerateFormIdTag()}

        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.UpdateResource(updatedResourceDTO, ResponseHelper.GetValidToken(), provider)

        e.Cancel = True
        gvNotes.CancelEdit()
    End Sub

    Protected Sub odsNotes_Selected(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles odsNotes.Selected
        gvNoteElements = e.ReturnValue()
    End Sub

    Protected Sub GridView_CustomUnboundColumnData(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs)
        Dim resourceDTO As ResourceDTO = gvNotes.GetRow(e.ListSourceRowIndex)
        Dim creatorUserName As String = ""
        Dim updateUserName As String = ""
        getCreatorAndUpdateUserName(resourceDTO.CreatorUserCode, resourceDTO.UpdateUserCode, creatorUserName, updateUserName)
        If (e.Column.FieldName.Equals("CreatorUserName")) Then
            e.Value = creatorUserName
        End If

        If (e.Column.FieldName.Equals("UpdateUserName")) Then
            e.Value = updateUserName
        End If
    End Sub

    Private Sub getCreatorAndUpdateUserName(creatorUserCode As Integer, updateUserCode As Integer, ByRef creatorUserName As String, ByRef updateUserName As String)
        creatorUserName = GetUserName(creatorUserCode)
        If creatorUserCode = updateUserCode Then
            updateUserName = creatorUserName
        Else
            updateUserName = GetUserName(updateUserCode)
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

End Class