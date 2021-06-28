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
Imports InMotionGIT.General.Proxy
Imports System.Reflection

#End Region

Partial Public Class Controls_DNEFileControl
    Inherits System.Web.UI.UserControl

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

    Dim fileExtensionsPermited As New List(Of KeyValuePair(Of Integer, List(Of String)))
    Dim gridviewActiveElements As New List(Of ResourceDTO)
    Dim provider As String = GetDNEProvider()


#Region "Fields"
    Private _visible As Boolean = True
    Private _enabled As Boolean = True
    Private _formId As String
    Private _selectMethod As String
    Private _roleName As String
    Private _DNEFileTags As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
    Private _DNEFileTagsFilter As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
    Private _underwritingCaseId As Integer
    Private _DNESequenceId As Integer
    Private _useTemporaryFiles As Boolean = True

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
        lblNoFiles.Visible = Value
        gvActiveResources.Visible = Value
        lblUploadedFiles.Visible = Value
        DNEFilterBox.Visible = Value
        lbtnNewItems.Visible = Value
        lblNoFiles.Visible = Value
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
        lbtnFilter.Enabled = Value
        lbtnNewItems.Enabled = Value
        gvActiveResources.Columns(0).Visible = Value
        lbtnNewItems.Visible = Value
        lbtnFilter.Visible = Value
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

    Public Property DNEFileTags As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
        Get
            Return ViewState(Me.ID.ToString + "DNEFileTags")
        End Get
        Set(ByVal value As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO)
            _DNEFileTags = value
            ViewState(Me.ID.ToString + "DNEFileTags") = value
        End Set
    End Property

    Public Property DNEFileTagsFilter As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
        Get
            Return ViewState(Me.ID.ToString + "DNEFileTagsFilter")
        End Get
        Set(ByVal value As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO)
            _DNEFileTagsFilter = value
            ViewState(Me.ID.ToString + "DNEFileTagsFilter") = value
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

    Public Property UseTemporaryFiles As Boolean
        Get
            Return ViewState(Me.ID.ToString + "UseTemporaryFiles")
        End Get
        Set(ByVal value As Boolean)
            _useTemporaryFiles = value
            ViewState(Me.ID.ToString + "UseTemporaryFiles") = value
        End Set
    End Property

    Public Property selectMethod As String
        Get
            Return ViewState(Me.ID.ToString + "SelectMethod")
        End Get
        Set(ByVal value As String)
            _selectMethod = value
            odsActiveResources.SelectMethod = selectMethod
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
            DeleteNewResourcesFromMemory()
            'DefineSequenceCreation()
            ViewState(Me.ID + "ExtensionArchivosPermitidos") = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetFileExtensionsAllowed(ResponseHelper.GetValidToken(), provider)

        Else
            LoadTagCheckBoxes()
            fileExtensionsPermited = ViewState(Me.ID + "ExtensionArchivosPermitidos")
            odsActiveResources.SelectMethod = ViewState(Me.ID + "SelectMethod")
        End If
    End Sub

    Protected Sub UpdatePanel_Unload(ByVal sender As Object, ByVal e As EventArgs)
        RegisterUpdatePanel(DirectCast(sender, UpdatePanel))    
    End Sub

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
            lbtnNewItems.Visible = ViewState(Me.ID.ToString + "Enabled")
            selectMethod = GetOperationContractMethodToGetResources(ViewState(Me.ID.ToString + "UseTemporaryFiles"), ViewState(Me.ID.ToString + "RoleName"))

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
                    odsActiveResources.SelectMethod = _selectMethod
                    odsActiveResources.SelectParameters.Clear()
                    odsActiveResources.SelectParameters.Add(New Parameter("sequenceId", DbType.Int32, DNESequenceId.ToString()))
                    odsActiveResources.SelectParameters.Add(New Parameter("tags", TypeCode.Object)) ' This variable will be load once the select method gets triggered
                    odsActiveResources.SelectParameters.Add(New Parameter("accessToken", DbType.String, ResponseHelper.GetValidToken()))
                    odsActiveResources.SelectParameters.Add(New Parameter("selectNotesOnly", DbType.Boolean, False.ToString))
                    odsActiveResources.SelectParameters.Add(New Parameter("provider", DbType.String, provider))
                    gvActiveResources.DataSourceID = odsActiveResources.ID
                    odsActiveResources.Select()
                    CreateTagCheckBoxes()
                    CheckFiltersBasedOnUsersInput()
                    RepaintControlBasedOnNumberRows()
                End If
            Else
                ClearContent()
            End If
        End If
    End Sub

    Protected Sub SaveDNESequenceIdInUnderwritingDatabase(uwCase As InMotionGIT.Underwriting.Contracts.UnderwritingCase)
        If uwCase IsNot Nothing Then
            uwCase.DNESequenceId = DNESequenceId
            uwCase.IsDirty = True
            uwCase.SaveIntance()
        End If
    End Sub


    Protected Sub PresentNewResources(sender As Object, e As EventArgs) Handles btnSubmitFileUpload.Click
        HideUserMessage()
        Dim maxResourcesAllowed = 10
        Dim resources As New List(Of ResourceDTO)
        Dim counter As Integer = gvTemporalResources.VisibleRowCount()
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
                    errorMessage = errorMessage + "'" + Path.GetFileName(file.FileName) + "': " + validationErrorMessage
                End If
                counter += 1
            Next
            LoadGridViewNewResources(resources)
        Else
            errorMessage = GetLocalResourceObject("MaxNumberFilesUploadText") + " " + maxResourcesAllowed.ToString
        End If
        ShowMessageToUser(errorMessage)

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
            errorMessage = errorMessage + ";" + stringToConcatenate
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


    Protected Sub LoadGridViewNewResources(listadoNuevosResources As List(Of ResourceDTO))
        Session(Me.ID + "gvTemporalResources") = listadoNuevosResources
        ReloadGridViewNewResources()
    End Sub

    Protected Sub ReloadGridViewNewResources()
        gvTemporalResources.DataSource = Session(Me.ID + "gvTemporalResources")
        gvTemporalResources.DataBind()
    End Sub

    Protected Function GetNewResources() As List(Of ResourceDTO)
        If Not IsNothing(Session(Me.ID + "gvTemporalResources")) Then Return TryCast(Session(Me.ID + "gvTemporalResources"), List(Of ResourceDTO))
        Return New List(Of ResourceDTO)
    End Function

    Protected Sub EndEditionInGridViewNewResources()
        gvTemporalResources.CancelEdit()
    End Sub

    Protected Sub CancelInsertionOfNewResources(sender As Object, e As EventArgs) Handles btnCancelSaveResources.Click
        EndEditionInGridViewNewResources()
        DeleteNewResourcesFromMemory()
        ReloadGridViewNewResources()
        lbtnNewItems.Visible = True
        gvActiveResources.Enabled = True
        lblUserMessage.Text = ""
        NewResources.Visible = False
    End Sub

    Protected Sub DeleteTemporaryResource(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles gvTemporalResources.RowDeleting
        e.Cancel = True
        Dim sequenceIdToDelete As Integer = e.Values("SequenceId")
        Dim consequenceIdToDelete As Integer = e.Values("ConsequenceId")
        Dim resources As List(Of ResourceDTO) = GetNewResources()

        Dim deletedResource As ResourceDTO = (From r In resources Where r.SequenceId = sequenceIdToDelete And r.ConsequenceId = consequenceIdToDelete Select r).FirstOrDefault()

        resources.Remove(deletedResource)
        ReloadGridViewNewResources()
    End Sub

    Protected Sub UpdateNewResource(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles gvTemporalResources.RowUpdating
        e.Cancel = True
        Dim listado As List(Of ResourceDTO) = GetNewResources()
        Dim previousValues As ResourceDTO = (From ant In listado Where ant.SequenceId = CInt(e.Keys("SequenceId")) And ant.ConsequenceId = CInt(e.Keys("ConsequenceId")) Select ant).FirstOrDefault()

        previousValues.Description = e.NewValues("Description")
        'valoresAnterior.ClientAssociatedPerson = e.NewValues("ClientAssociatedPerson")
        'valoresAnterior.ClientAssociatedCompany = e.NewValues("ClientAssociatedCompany")
        'valoresAnterior.LocationId = e.NewValues("LocationId")

        ReloadGridViewNewResources()
        EndEditionInGridViewNewResources()
    End Sub

    Protected Sub DeleteNewResourcesFromMemory()
        Session(Me.ID + "gvTemporalResources") = Nothing
        'TODO Eliminar la variable de session
    End Sub

    Protected Sub btnSaveResources_Click(sender As Object, e As EventArgs) Handles btnSaveResources.Click
        Dim resourcesToBeSaved As List(Of ResourceDTO) = GetNewResources()
        'If ValidateResources(resourcesToBeSaved) Then
        For Each resource In resourcesToBeSaved
            resource.Tags = GenerateTags()
            SaveResource(resource)
        Next
        HideUserMessage()
        DeleteNewResourcesFromMemory()
        ReloadGridViewNewResources()
        ShowPanelWithActiveResources()
        lbtnNewItems.Visible = True
        gvActiveResources.Enabled = True
        'End If
    End Sub

    ''' <summary>
    ''' It generates tags 
    ''' </summary>
    ''' <returns></returns>
    Protected Function GenerateTags() As HashSet(Of TagDTO)
        Dim tagsResult As New HashSet(Of TagDTO)
        tagsResult = GetTagsFromDesigner()
        tagsResult.Add(GenerateFormIdTag())
        Return tagsResult
    End Function

    ''' <summary>
    ''' Gets the tags selected in the Formdesigner and converts them from ShortTag to Tag
    ''' </summary>
    ''' <returns></returns>
    Protected Function GetTagsFromDesigner() As HashSet(Of TagDTO)
        Dim tagsToAdd As New HashSet(Of TagDTO)
        Dim tagsToBeAdded As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.TagListDTO
        tagsToBeAdded = ViewState(Me.ID.ToString + "DNEFileTags")
        If tagsToBeAdded IsNot Nothing Then
            Dim tagDTO As TagDTO
            For Each shortTag As ShortTagDTO In tagsToBeAdded
                tagDTO = New TagDTO
                tagDTO.Content = shortTag.Content
                tagDTO.TagTypeId = shortTag.TagTypeId
                tagsToAdd.Add(tagDTO)
            Next
        End If
        Return tagsToAdd
    End Function

    ''' <summary>
    ''' It generates FormId tag
    ''' </summary>    
    ''' <returns></returns>
    Protected Function GenerateFormIdTag() As TagDTO
        Dim tagDTO As New TagDTO

        tagDTO.Content = ViewState(Me.ID.ToString + "FormId")
        tagDTO.TagTypeId = tagTypeIds("FormId")
        Return tagDTO
    End Function

    ''' <summary>
    ''' It saves a resource in the database with a temporary state. It invoques the web service based on the the file type that is being saved
    ''' </summary>
    ''' <param name="resourceDTO">Resource to be saved</param>
    Private Sub SaveResource(resourceDTO As ResourceDTO)
        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddResourceWithTemporaryState(resourceDTO, ResponseHelper.GetValidToken(), provider)
    End Sub

    Private Function ValidateResources(resourcesToBeSaved As List(Of ResourceDTO)) As Boolean
        Dim errorMessage = ""
        Dim result = True
        Dim count = 1
        For Each resource In resourcesToBeSaved
            If (IsNothing(resource.Description)) OrElse (resource.Description.Equals(String.Empty)) Then
                errorMessage = errorMessage + " Error: Row:" + resource.ConsequenceId.ToString + ", the field description should not be empty." + Environment.NewLine
                result = False
            End If
            If resource.ExpirationDate.Equals(String.Empty) Then
                errorMessage = errorMessage + "Error: Row:" + resource.ConsequenceId.ToString + ", the field description should not be empty." + Environment.NewLine
                result = False
            End If

        Next

        If Not result Then
            ShowMessageToUser(errorMessage)
        End If

        Return result
    End Function

    Private Function GenerateResourceBasedOnFile(file As HttpPostedFile, idConsecutivo As Integer) As ResourceDTO
        Dim fileName As String
        fileName = Path.GetFileName(file.FileName)
        Dim resourceDTO As New ResourceDTO
        resourceDTO.SequenceId = ViewState(Me.ID.ToString + "DNESequenceId")
        resourceDTO.ConsequenceId = idConsecutivo
        resourceDTO.Name = fileName
        resourceDTO.ResourceTypeId = GetFileType(fileName)
        LoadResource(resourceDTO, file)
        'resourceDTO.ExpirationDate = Date.Now 

        'TODO estas dos propiedades tienen que ser puestas como propiedades del control
        resourceDTO.ClientAssociatedCompany = -1
        resourceDTO.ClientAssociatedPerson = -1

        Dim tag As New TagDTO
        tag.TagTypeId = 1
        tag.Content = ViewState(Me.ID.ToString + "FormId")
        resourceDTO.Tags.Add(tag)
        Return resourceDTO
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

    Protected Sub ShowNewResourcesPanel(sender As Object, e As EventArgs) Handles lbtnNewItems.Click
        'lbtnNewItems.Visible = False
        DisableControl()
        NewResources.Visible = True
    End Sub

    Protected Sub DisableControl()
        gvActiveResources.Enabled = False
        lbtnNewItems.Visible = False
        lbtnFilter.Enabled = False
    End Sub

    Protected Sub ShowPanelWithActiveResources()
        gvActiveResources.Visible = True
        lbtnFilter.Enabled = True
        odsActiveResources.Select()
        gvActiveResources.DataBind()
        NewResources.Visible = False
    End Sub

    Protected Sub ShowMessageToUser(mensaje As String)
        lblUserMessage.Visible = True
        lblUserMessage.Text = mensaje
    End Sub

    Protected Sub HideUserMessage()
        lblUserMessage.Visible = False
        lblUserMessage.Text = String.Empty
    End Sub

    Protected Sub gvActiveResources_FocusedRowChanged(sender As Object, e As EventArgs)
        Dim resourceDTO As ResourceDTO = gvActiveResources.GetRow(gvActiveResources.FocusedRowIndex)
        DownloadResource(resourceDTO)
    End Sub

#End Region

#Region "Methods to deal with Active Resources"

    ''' <summary>
    ''' It creates or generates a sequence if there is no sequence related to the CaseId in the DB
    ''' </summary>
    ''' <returns>SequenceId</returns>
    Protected Function CreateSequence() As Integer
        Dim sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.SequenceExistInTags(ViewState(Me.ID.ToString + "UnderwritingCaseId"), ResponseHelper.GetValidToken(), provider)
        If sequenceId = -1 Then
            sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GenerateSequence(ResponseHelper.GetValidToken(), provider)
        End If
        Return sequenceId

        'Dim sequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GenerateSequence(ResponseHelper.GetValidToken(), provider)        
        'Return sequenceId
    End Function

    Private Sub CleanViewStateBasedOnSequenceId(previousSequenceId As Integer, currentSequenceId As Integer)
        If previousSequenceId <> currentSequenceId Then
            ViewState(Me.ID.ToString() + "TagCheckBoxes") = Nothing
            tagFilterContainer.ContentTemplateContainer.Controls.Clear()
        End If
    End Sub

    Private Sub RepaintControlBasedOnNumberRows()
        If gridviewActiveElements.Count > 0 Then
            MakeControlWithElements()
        Else
            MakeControlWithNoElements()
        End If
    End Sub

    Private Sub EnableElements()
        lbtnNewItems.Visible = True
        gvActiveResources.Visible = True
    End Sub

    ''' <summary>
    ''' This method clears the content of the Control when SequenceId is null
    ''' </summary>
    Private Sub ClearContent()
        ViewState(Me.ID.ToString + "SelectMethod") = String.Empty
        Me.tagFilterContainer.ContentTemplateContainer.Controls.Clear()
        gvActiveResources.DataSourceID = String.Empty
        gvActiveResources.DataBind()
        lbtnNewItems.Visible = False
        gvActiveResources.Visible = False
        DNEFilterBox.Visible = False
        lblNoFiles.Visible = True
        lblUploadedFiles.Visible = False
    End Sub

    ''' <summary>
    ''' Create tags based on tags related to the SequenceId Control
    ''' </summary>
    Private Sub CreateTagCheckBoxes()
        If IsNumeric(ViewState(Me.ID.ToString + "DNESequenceId")) Then
            Dim checkBoxes As Dictionary(Of String, Boolean) = GetCheckBoxesFromViewState()
            'Dim tagList As List(Of ShortTagDTO) = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetTagsRelatedToResourceActiveAndTemporarySequence(FormData.@@DNEFilesParameterFullName@@, ResponseHelper.GetValidToken())
            Dim tagList As List(Of TagDTO) = GetTagsFromElementsInGridViewActive()
            'Dim tagListFiltered As List(Of TagDTO) = tagList.Where(Function(x) x.TagTypeId <> tagTypeIds("FormId") And x.TagTypeId <> tagTypeIds("CaseId") And x.TagTypeId <> tagTypeIds("RequirementId")).Distinct.ToList()
            Dim tagListFiltered As List(Of TagDTO) = tagList.Where(Function(x) x.TagTypeId = tagTypeIds("InformativeId")).Distinct.ToList()
            Dim checkBoxId As String
            For Each tag As TagDTO In tagListFiltered
                checkBoxId = Me.ID + "|" + tagTypeIds.ElementAt(tag.TagTypeId - 1).Value.ToString() + "|" + tag.Content.ToString
                If Not CheckBoxExist(checkBoxId) Then
                    Dim checkBox As New CheckBox()
                    checkBox.ID = checkBoxId
                    checkBox.Text = tag.Content.ToString
                    checkBoxes.Add(checkBox.ID, False)
                    checkBox.Enabled = ViewState(Me.ID.ToString + "Enabled")
                    Me.tagFilterContainer.ContentTemplateContainer.Controls.Add(checkBox)
                End If
            Next
            ViewState(Me.ID.ToString + "TagCheckBoxes") = checkBoxes
            Me.tagFilterContainer.Update() 'TODO Updates the panel once new checkboxes gets add
        End If
    End Sub

    ''' <summary>
    ''' Gets the tags involved in gvActiveResources
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
                If Not CheckBoxExist(checkBox.ID) Then
                    Me.tagFilterContainer.ContentTemplateContainer.Controls.Add(checkBox)
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
        taglistDTO = ViewState(Me.ID.ToString + "DNEFileTagsFilter")
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

    Protected Sub FilterButton(sender As Object, e As EventArgs)
        UpdateViewStateTags()
        gvActiveResources.DataBind()
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

    Protected Function GetFiltersChecked() As List(Of ShortTagDTO)
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

    Protected Sub DownloadResource(resourceDTO As ResourceDTO)
        Try
            Dim composedResourceKey As New ComposedResourceKey(resourceDTO.SequenceId, resourceDTO.ConsequenceId)
            Dim resourceContent As Byte() = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetResourceContent(composedResourceKey, ResponseHelper.GetValidToken(), provider)
            SaveFile(resourceContent, resourceDTO.Name)
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

    'Private Function GetOperationContractMethodToGetResources(useOnlyTemporaryResources As Boolean, userRole As String) As String
    '    Dim selectMethod = ""
    '    Dim rolesCliente = ConfigurationManager.AppSettings.Get("Cliente").Split(";")
    '    Dim rolesAdministrador = ConfigurationManager.AppSettings.Get("Administrador").Split(";")
    '    Dim match As List(Of String)

    '    If userRole IsNot Nothing Then
    '        Dim usersRoles = userRole.Split(";")
    '        match = usersRoles.Intersect(rolesAdministrador).ToList
    '        If match.Count > 0 Then
    '            If useOnlyTemporaryResources Then
    '                Return "GetResourceSequenceTemporaryStateOnly"
    '            Else
    '                Return "GetActiveResourceSequenceAndFormTemporals"
    '            End If
    '        Else
    '            match = usersRoles.Intersect(rolesCliente).ToList
    '            If match.Count > 0 Then
    '                If useOnlyTemporaryResources Then
    '                    Return "GetOwnResourceSequenceTemporaryStateOnly"
    '                Else
    '                    Return "GetOwnResourceSequenceActiveAndTemporaryState"
    '                End If
    '            End If
    '        End If
    '    End If

    '    selectMethod = ""   'TODO: Que pasa si es que no es ni administrador, ni cliente
    '    Return selectMethod
    'End Function

    Private Function GetOperationContractMethodToGetResources(useOnlyTemporaryResources As Boolean, userRole As String) As String
        Dim selectMethod = ""
        'Dim rolesCliente = ConfigurationManager.AppSettings.Get("Cliente").Split(";")
        'Dim rolesAdministrador = ConfigurationManager.AppSettings.Get("Administrador").Split(";")
        'Dim match As List(Of String)

        If userRole IsNot Nothing Then
            Dim userRoles = userRole.ToUpper.Split(";")
            If userRoles.Contains("SUSCRIPTOR") Or (userRoles.Contains("DNEADMIN")) Or (userRoles.Contains("DNEGET")) Then
                If useOnlyTemporaryResources Then
                    selectMethod = "GetResourceSequenceTemporaryStateOnly"
                Else
                    selectMethod = "GetActiveResourceSequenceAndFormTemporals"
                End If
                If (userRoles.Contains("DNEGET")) Then
                    Enabled = False
                End If
            Else
                If (userRoles.Contains("DNEOWNER")) Then
                    If useOnlyTemporaryResources Then
                        selectMethod = "GetOwnResourceSequenceTemporaryStateOnly"
                    Else
                        selectMethod = "GetOwnResourceSequenceActiveAndTemporaryState"
                    End If
                End If
            End If
        End If
        Return selectMethod
    End Function

    Private Sub MakeControlWithNoElements()
        lblNoFiles.Visible = True
        gvActiveResources.Visible = False
        lblUploadedFiles.Visible = False
        DNEFilterBox.Visible = False
        'lbtnNewItems.Visible = True
    End Sub

    Private Sub MakeControlWithElements()
        lblNoFiles.Visible = False
        gvActiveResources.Visible = True
        lblUploadedFiles.Visible = True
        DNEFilterBox.Visible = True
        'lbtnNewItems.Visible = True
    End Sub



    Protected Sub odsActiveResources_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsActiveResources.Selecting
        odsActiveResources.SelectMethod = ViewState(Me.ID + "SelectMethod")
        If IsNumeric(ViewState(Me.ID.ToString + "DNESequenceId")) Then
            e.InputParameters.Item("tags") = GenerateTagsForSelecting()
        Else
            e.Cancel = True

        End If
    End Sub

    Protected Sub gvActiveResources_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        odsActiveResources.SelectMethod = ViewState(Me.ID + "SelectMethod")
        Dim composedResourceKey As New ComposedResourceKey(CInt(e.Values("SequenceId")), CInt(e.Values("ConsequenceId")))
        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.DeleteResource(composedResourceKey, ResponseHelper.GetValidToken(), provider)

        e.Cancel = True
        odsActiveResources.Select()
    End Sub

    Protected Sub gvActiveResources_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        odsActiveResources.SelectMethod = ViewState(Me.ID + "SelectMethod")
        'odsActiveResources.SelectMethod = _selectedMethod
        Dim updatedResourceDTO As ResourceDTO = gvActiveResources.GetRow(gvActiveResources.EditingRowVisibleIndex)
        updatedResourceDTO.Description = e.NewValues("Description")
        updatedResourceDTO.ExpirationDate = e.NewValues("ExpirationDate")
        updatedResourceDTO.ClientAssociatedPerson = e.NewValues("ClientAssociatedPerson")
        updatedResourceDTO.ClientAssociatedCompany = e.NewValues("ClientAssociatedCompany")
        updatedResourceDTO.LocationId = e.NewValues("LocationId")

        'In a newer version of this control, we have to deal with this properties, in this version, these properties are left empty 
        updatedResourceDTO.ClientAssociatedPerson = -1
        updatedResourceDTO.ClientAssociatedCompany = -1

        updatedResourceDTO.Tags = New HashSet(Of TagDTO)() From {GenerateFormIdTag()}
        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.UpdateResource(updatedResourceDTO, ResponseHelper.GetValidToken(), provider)

        e.Cancel = True
        gvActiveResources.CancelEdit()
    End Sub

    Protected Sub odsActiveResources_Selected(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles odsActiveResources.Selected
        gridviewActiveElements = e.ReturnValue()
    End Sub

    Protected Sub gvActiveResources_CustomUnboundColumnData(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs)
        If (e.Column.FieldName.Equals("CreatorUserName")) Then
            Dim resourceDTO As ResourceDTO = gvActiveResources.GetRow(e.ListSourceRowIndex)
            e.Value = GetUserName(resourceDTO.CreatorUserCode)
        End If
    End Sub

    Protected Sub gvActiveResources_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewCommandButtonEventArgs) Handles gvActiveResources.CommandButtonInitialize
        e.Enabled = ViewState(Me.ID.ToString + "Enabled")

    End Sub

    Protected Sub gvActiveResources_CustomButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonEventArgs)
        e.Enabled = ViewState(Me.ID.ToString + "Enabled")
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