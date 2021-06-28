Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs
Imports InMotionGIT.DatosNoEstruct.Proxy.DNE
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.Modelo
Imports InMotionGIT.Underwriting.Contracts
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web
Imports System.Data
Imports System.Net
Imports InMotionGIT.Seguridad.Proxy

Partial Class Underwriting_Controls_Partials_notes
    Inherits GIT.Core.PageBase
    'Inherits System.Web.UI.Page

    Dim sequenceDTO As New SequenceDTO
    Dim DNEClient As OperationContracts

    Dim tagTypeIds As New Dictionary(Of String, Int16)() From {
        {"FormId", 1},
        {"RequirementId", 2},
        {"CaseId", 3},
        {"InformativeId", 4},
        {"RequiretmentTypeId", 5},
        {"TypeOfNote", 16}
    }
    Public Property uwCaseId() As Integer
        Get
            Return ViewState("uwCaseId")
        End Get
        Set(ByVal value As Integer)
            ViewState("uwCaseId") = value
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

    Dim gridViewNoteElements As New List(Of ResourceDTO)

    Private _formData As New ParameterNotes
    Dim provider As String = GetDNEProvider()

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        '_formData.SequenceId = Session("DNESequenceId")
    End Sub

    Private Function GetDNEProvider() As String
        Dim provider = ConfigurationManager.AppSettings.Get("DNEProvider")
        If (provider IsNot Nothing) Then
            Return provider.ToUpper()
        End If
        Return "DNE"
    End Function

    Protected Overrides Sub InitializeCulture()
        If HttpContext.Current.Session("App_CultureInfoCode") IsNot Nothing Then
            Dim language = HttpContext.Current.Session("App_CultureInfoCode").ToString()
            UICulture = language
            Culture = language
            MyBase.InitializeCulture()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            uwCaseId = Request.QueryString("caseId").IfEmpty(0)
            MapClassToPage()
        Else
            LoadTagCheckBoxes()
            DataSourceNotes.SelectMethod = ViewState("DNENotesSelectMethod")
        End If
        ActivateButtonsBasedOnPageState()
    End Sub


    Protected Sub MapClassToPage()
        Dim useOnlyTemporaryNotes = False ' Esta variable viene desde una propiedad del control en el FormDesigner el cual define  si se van a mostrar Notas temporales(True). Si useOnlyTemporaryResources=False, se van a mostrar Notas temporales y activos
        Dim DNENotesSelectMethod = GetOperationContractMethodToGetNotes(useOnlyTemporaryNotes, Session("UserRoles")) 'TOBEREPLACED
        Dim instance = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        If instance IsNot Nothing _
            AndAlso IsNumeric(instance.UnderwritingCaseID) AndAlso Not DNENotesSelectMethod.Equals("") Then
            DneSequenceId = instance.DNESequenceId
            If (DneSequenceId = 0) Then
                'Dim sequenceDTO As New SequenceDTO
                'sequenceDTO.BlockedToExternalUsers = "0"
                'sequenceDTO.AdditionalAuthorizedUsers = "0"
                '_formData.SequenceId = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddSequence(sequenceDTO, ResponseHelper.GetValidToken())
                '_formData.SequenceId = CreateSequence()
                DneSequenceId = CreateSequence()
                instance.DNESequenceId = DneSequenceId
                InMotionGIT.Underwriting.Proxy.Helpers.Support.StorageInstance(uwCaseId, instance, GetToken())
                'Session("DNESequenceId") = _formData.SequenceId
            End If
            'TODO se tiene que adicionar una nueva columna al grid especifique si una nota es Temporal o Activa
            GridViewNotes.Enabled = (Not IsNothing(instance.LockedOn) AndAlso (instance.LockedBy = Session("nUserCode") OrElse instance.LockedBy = Session("UserId")))
            AspxHyperLinkNew.Visible = (Not IsNothing(instance.LockedOn) AndAlso (instance.LockedBy = Session("nUserCode") OrElse instance.LockedBy = Session("UserId")))
            If (instance IsNot Nothing AndAlso instance.Status = InMotionGIT.Underwriting.Contracts.Enumerations.EnumUnderwritingCaseStatus.Consultation) Then
                GridViewNotes.Enabled = False
                AspxHyperLinkNew.Visible = False
            End If
            DataSourceNotes.SelectMethod = DNENotesSelectMethod
            DataSourceNotes.SelectParameters.Clear()
            'DataSourceNotes.SelectParameters.Add(New Parameter("sequenceId", DbType.Int32, _formData.SequenceId.ToString()))
            DataSourceNotes.SelectParameters.Add(New Parameter("sequenceId", DbType.Int32, DneSequenceId.ToString()))
            DataSourceNotes.SelectParameters.Add(New Parameter("tags", TypeCode.Object)) ' This variable will be load once the select method gets triggered 
            DataSourceNotes.SelectParameters.Add(New Parameter("accessToken", DbType.String, GetToken()))
            DataSourceNotes.SelectParameters.Add(New Parameter("selectNotesOnly", DbType.Boolean, True.ToString))
            DataSourceNotes.SelectParameters.Add(New Parameter("provider", DbType.String, provider))
            GridViewNotes.DataSourceID = DataSourceNotes.ID
            DataSourceNotes.Select()
            CreateTagCheckBoxes()
            ViewState("DNENotesSelectMethod") = DataSourceNotes.SelectMethod
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

#Region "DNENotes GridViewNotes Methods"

    Protected Sub notes_Callback(sender As Object, e As EventArgs)
        If (Request.Browser.Browser <> "InternetExplorer") Then
            Dim editor As ASPxHtmlEditor.ASPxHtmlEditor = TryCast(sender, ASPxHtmlEditor.ASPxHtmlEditor)
            editor.Settings.AllowDesignView = True
        End If
    End Sub

    ''' <summary>
    ''' This method clears the content of the Control when SequenceId is null
    ''' </summary>
    Private Sub InitializeComponents()
        ViewState("DNENotesSelectMethod") = String.Empty
        Me.tagFilterContainer.Controls.Clear()
        GridViewNotes.DataSourceID = String.Empty
        GridViewNotes.DataBind()
        GridViewNotes.Enabled = False
    End Sub

    ''' <summary>
    ''' Enables or disables buttons to add new Notes based on Form's state
    ''' </summary>
    Private Sub ActivateButtonsBasedOnPageState()
        Dim instance = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        If (Not IsNothing(instance)) Then
            AspxHyperLinkNew.Enabled = (Not IsNothing(instance.LockedOn) AndAlso (instance.LockedBy = Session("nUserCode") OrElse instance.LockedBy = Session("UserId")))
            AspxHyperLinkNew.Visible = (Not IsNothing(instance.LockedOn) AndAlso (instance.LockedBy = Session("nUserCode") OrElse instance.LockedBy = Session("UserId")))
            ButtonFilter.Enabled = (Not IsNothing(instance.LockedOn) AndAlso (instance.LockedBy = Session("nUserCode") OrElse instance.LockedBy = Session("UserId")))
        Else
            AspxHyperLinkNew.Enabled = False
            AspxHyperLinkNew.Visible = False
            ButtonFilter.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Create tags based on tags related to the SequenceId Control
    ''' </summary>
    Private Sub CreateTagCheckBoxes()
        Dim instance = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        '_formData.tagListDTO = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetTagsRelatedToNoteActiveAndTemporarySequence(_formData.SequenceId, ResponseHelper.GetValidToken())
        If IsNumeric(DneSequenceId) Then
            Dim checkBoxes As Dictionary(Of String, Boolean) = GetCheckBoxesFromViewState()
            'Dim tagList As List(Of ShortTagDTO) = InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.GetTagsRelatedToNoteActiveAndTemporarySequence(_formData.SequenceId, ResponseHelper.GetValidToken())
            Dim tagList As List(Of TagDTO) = GetTagsFromElementsInGridViewActive()
            Dim tagListFiltered As List(Of TagDTO) = tagList.Where(Function(x) x.TagTypeId <> tagTypeIds("FormId") And x.TagTypeId <> tagTypeIds("CaseId") And x.TagTypeId <> tagTypeIds("TypeOfNote")).Distinct.ToList()
            Dim checkBoxId As String
            For Each tag As TagDTO In tagListFiltered
                checkBoxId = "Notes" + "|" + tagTypeIds.ElementAt(tag.TagTypeId - 1).Value.ToString() + "|" + tag.Content.ToString
                If Not CheckBoxExist(checkBoxId) Then
                    Dim checkBox As New CheckBox()
                    checkBox.ID = checkBoxId
                    checkBox.Text = tag.Content.ToString
                    checkBoxes.Add(checkBox.ID, False)
                    If (Not IsNothing(instance)) Then
                        checkBox.Enabled = (Not IsNothing(instance.LockedOn) AndAlso (instance.LockedBy = Session("nUserCode") OrElse instance.LockedBy = Session("UserId")))
                    Else
                        checkBox.Enabled = False
                    End If
                    Me.tagFilterContainer.Controls.Add(checkBox)
                End If
            Next
            ViewState("DNENotesTagCheckBoxes") = checkBoxes
            'Me.tagFilterContainer.Update() 'TODO Updates the panel once new checkboxes gets add
        End If
    End Sub

    ''' <summary>
    ''' Gets the tags involved in GridViewActiveResources
    ''' </summary>
    ''' <returns></returns>
    Private Function GetTagsFromElementsInGridViewActive() As List(Of TagDTO)
        Dim tagsInActiveGridView As New List(Of TagDTO)
        For Each resource In gridViewNoteElements
            If resource.Tags IsNot Nothing Then
                tagsInActiveGridView.AddRange(resource.Tags)
            End If
        Next
        Return tagsInActiveGridView
    End Function

    Protected Sub GridViewNotes_CustomUnboundColumnData(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs)
        Dim resourceDTO As ResourceDTO = GridViewNotes.GetRow(e.ListSourceRowIndex)
        Dim creatorUserName As String = ""
        Dim updateUserName As String = ""
        getCreatorAndUpdateUserName(resourceDTO.CreatorUserCode, resourceDTO.UpdateUserCode, creatorUserName, updateUserName)
        If (e.Column.FieldName.Equals("CreatorUserNameUW")) Then
            e.Value = creatorUserName
        End If

        If (e.Column.FieldName.Equals("UpdateUserNameUW")) Then
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
        If (IsNothing(userName) OrElse userName.IsEmpty()) Then ' It means that the userhttp://localhost:47115/Underwriting/Controls/Partials/_notes.aspx.vb is located in Security
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

    ''' <summary>
    ''' It obtains the elements from the viewState("TagCheckBoxesParameter1") 
    ''' </summary>
    ''' <returns></returns>
    Private Function GetCheckBoxesFromViewState() As Dictionary(Of String, Boolean)
        Dim checkBoxes As New Dictionary(Of String, Boolean)
        If ViewState("DNENotesTagCheckBoxes") IsNot Nothing Then
            For Each checkbox As KeyValuePair(Of String, Boolean) In ViewState("DNENotesTagCheckBoxes")
                checkBoxes.Add(checkbox.Key, checkbox.Value)
            Next
        End If
        Return checkBoxes
    End Function

    ''' <summary>
    ''' create the checkboxes elements based on the elements saved in ViewState
    ''' </summary>
    Private Sub LoadTagCheckBoxes()
        If ViewState("DNENotesTagCheckBoxes") IsNot Nothing Then
            For Each checkBoxElement As KeyValuePair(Of String, Boolean) In ViewState("DNENotesTagCheckBoxes")
                Dim checkBox As New CheckBox()
                checkBox.ID = checkBoxElement.Key
                checkBox.Text = checkBoxElement.Key.Split("|")(2)
                checkBox.Checked = checkBoxElement.Value
                If Not Me.tagFilterContainer.Controls.OfType(Of CheckBox).Contains(checkBox) Then
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

    Protected Sub GridViewNotes_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        Dim composedResourceKey As New ComposedResourceKey(CInt(e.Values("SequenceId")), CInt(e.Values("ConsequenceId")))

        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.DeleteResourceTemporarily(composedResourceKey, GetToken(), provider)

        e.Cancel = True
        GridViewNotes.CancelEdit()
    End Sub

    Protected Sub GridViewNotes_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)

        Dim nameTextBoxValue As String = TryCast(GridViewNotes.FindEditFormTemplateControl("TextBoxNameId"), ASPxTextBox).Text
        'Dim expirationDateTextBoxValue As DateTime = DirectCast(TryCast(GridViewNotes.FindEditFormTemplateControl("ExpirationDateTextId"), ASPxDateEdit).Value, DateTime)
        'Dim expirationDateTextBox = GridViewParameter1.FindEditFormTemplateControl("ExpirationDateTextId")

        Dim expirationDate As New Nullable(Of DateTime)
        expirationDate = GetExpirationDateValue()

        Dim resourceDTO As New ResourceDTO()
        resourceDTO.Note = New NoteDTO()
        resourceDTO.Note.SequenceId = DneSequenceId
        resourceDTO.Note.Content = DirectCast(e.NewValues("Note.Content"), [String])

        resourceDTO.SequenceId = DneSequenceId
        resourceDTO.Name = nameTextBoxValue
        resourceDTO.ExpirationDate = expirationDate
        resourceDTO.Description = "No Description"
        resourceDTO.ClientAssociatedCompany = "1"
        resourceDTO.ClientAssociatedPerson = "1"



        resourceDTO.Tags = GenerateTags(resourceDTO.SequenceId, resourceDTO.ConsequenceId)

        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddResourceWithTemporaryState(resourceDTO, GetToken(), provider)
        e.Cancel = True
        GridViewNotes.CancelEdit()

        'Other VERSION
        'Dim nameTextBoxValue As String = TryCast(GridViewNotes.FindEditFormTemplateControl("TextBoxNameId"), ASPxTextBox).Text
        ''Dim expirationDateTextBoxValue As DateTime = DirectCast(TryCast(GridViewNotes.FindEditFormTemplateControl("ExpirationDateTextId"), ASPxDateEdit).Value, DateTime)
        ''Dim expirationDateTextBox = GridViewParameter1.FindEditFormTemplateControl("ExpirationDateTextId")

        'Dim expirationDate As New Nullable(Of DateTime)
        'expirationDate = GetExpirationDateValue()

        'Dim noteDTO As New NoteDTO()
        'noteDTO.SequenceId = _formData.SequenceId

        'noteDTO.Resource.SequenceId = _formData.SequenceId
        'noteDTO.Resource.Name = nameTextBoxValue
        'noteDTO.Resource.ExpirationDate = expirationDate
        'noteDTO.Resource.Description = "No Description"
        'noteDTO.Resource.ClientAssociatedCompany = "1"
        'noteDTO.Resource.ClientAssociatedPerson = "1"

        'noteDTO.Content = DirectCast(e.NewValues("Note.Content"), [String])

        'noteDTO.Resource.Tags = GenerateTags(noteDTO.Resource.SequenceId, noteDTO.Resource.ConsequenceId)

        'InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.AddNoteWithTemporaryState(noteDTO, ResponseHelper.GetValidToken(), provider)

        'e.Cancel = True
        'GridViewNotes.CancelEdit()

    End Sub

    Private Function GetExpirationDateValue() As Nullable(Of DateTime)
        Dim expirationDate As Nullable(Of DateTime)
        Dim expirationDateTextBox = TryCast(GridViewNotes.FindEditFormTemplateControl("ExpirationDateTextId"), ASPxDateEdit).Value
        If expirationDateTextBox IsNot Nothing Then
            expirationDate = DirectCast(expirationDateTextBox, Nullable(Of DateTime))
        End If
        Return expirationDate
    End Function


    Protected Sub GridViewNotes_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Dim updatedResourceDTO As ResourceDTO = GridViewNotes.GetRow(GridViewNotes.EditingRowVisibleIndex)

        Dim nameTextTextBoxValue As String = TryCast(GridViewNotes.FindEditFormTemplateControl("TextBoxNameId"), ASPxTextBox).Value
        Dim expirationDateEditValue As Nullable(Of DateTime) = TryCast(GridViewNotes.FindEditFormTemplateControl("ExpirationDateTextId"), ASPxDateEdit).Value

        updatedResourceDTO.Name = nameTextTextBoxValue
        updatedResourceDTO.ExpirationDate = expirationDateEditValue
        updatedResourceDTO.Note.Content = DirectCast(e.NewValues("Note.Content"), [String])

        updatedResourceDTO.Tags = New HashSet(Of TagDTO)() From {GenerateCaseIdTag(updatedResourceDTO.SequenceId, updatedResourceDTO.ConsequenceId)}

        InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.UpdateResource(updatedResourceDTO, GetToken(), provider)

        e.Cancel = True
        GridViewNotes.CancelEdit()
    End Sub

    'Protected Sub GridViewNotes_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
    '    Dim updatedNoteDTO As NoteDTO = GridViewNotes.GetRow(GridViewNotes.EditingRowVisibleIndex)
    '    updatedNoteDTO.Content = DirectCast(e.NewValues("Content"), [String])

    '    Dim nameTextTextBoxValue As String = TryCast(GridViewNotes.FindEditFormTemplateControl("TextBoxNameId"), ASPxTextBox).Value
    '    Dim expirationDateEditValue As Nullable(Of DateTime) = TryCast(GridViewNotes.FindEditFormTemplateControl("ExpirationDateTextId"), ASPxDateEdit).Value

    '    Dim resourceDTO As New ResourceDTO

    '    resourceDTO.Name = nameTextTextBoxValue
    '    resourceDTO.ExpirationDate = expirationDateEditValue
    '    resourceDTO.Note = updatedNoteDTO

    '    InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts.UpdateNote1(resourceDTO, ResponseHelper.GetValidToken())

    '    e.Cancel = True
    '    GridViewNotes.CancelEdit()
    'End Sub

    ''' <summary>
    ''' It generates tags 
    ''' </summary>    
    ''' <returns></returns>
    Protected Function GenerateTags(sequenceId As Integer, consequenceId As Integer) As HashSet(Of TagDTO)
        Dim tagsResult As New HashSet(Of TagDTO)
        tagsResult.Add(GenerateTypeOfNoteTag(sequenceId, consequenceId))
        Return tagsResult
    End Function

    ''' <summary>
    ''' It generates tagId Case
    ''' </summary>
    ''' <param name="sequenceId">SequenceId</param>
    ''' <param name="consequenceId">ConsequenceId</param>
    ''' <returns></returns>
    Protected Function GenerateCaseIdTag(sequenceId As Integer, consequenceId As Integer) As TagDTO
        Dim tagDTO As New TagDTO
        Dim uwcase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance()
        tagDTO.SequenceId = sequenceId
        tagDTO.ConsequenceId = consequenceId
        If uwcase.IsNotEmpty Then
            tagDTO.Content = uwcase.UnderwritingCaseID
        End If

        tagDTO.TagTypeId = tagTypeIds("CaseId")
        Return tagDTO
    End Function

    ''' <summary>
    ''' It generates tagId Case
    ''' </summary>
    ''' <param name="sequenceId">SequenceId</param>
    ''' <param name="consequenceId">ConsequenceId</param>
    ''' <returns></returns>
    Protected Function GenerateTypeOfNoteTag(sequenceId As Integer, consequenceId As Integer) As TagDTO
        Dim tagDTO As New TagDTO
        Dim uwcase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstanceWithAccessToken(uwCaseId, GetToken())
        tagDTO.SequenceId = sequenceId
        tagDTO.ConsequenceId = consequenceId
        If uwcase.IsNotEmpty Then
            tagDTO.Content = 5 'Indica tipo de nota en el BO "Notas de la póliza/ comentarios/ endosos"
        End If

        tagDTO.TagTypeId = tagTypeIds("TypeOfNote")
        Return tagDTO
    End Function

    ''' <summary>
    ''' It generates tagId Case
    ''' </summary>
    ''' <returns></returns>
    Protected Function GenerateFormIdTag() As TagDTO
        Dim tagDTO As New TagDTO
        tagDTO.TagTypeId = tagTypeIds("FormId")
        tagDTO.Content = "_FormID.Text"
        Return tagDTO
    End Function



    ''' <summary>
    ''' Gets the method that will be used to populate the main Grid
    ''' </summary>
    ''' <param name="useOnlyTemporaryNotes">Variable that defines whether only temporary Notes will be shown</param>
    ''' <param name="userRoles">User's role</param>
    ''' <returns>The method that will be used to populate the Grid</returns>
    Private Function GetOperationContractMethodToGetNotes(useOnlyTemporaryNotes As Boolean, userRoles As String) As String
        If userRoles IsNot Nothing AndAlso Not userRoles.Equals("") Then
            If userRoles.Split(";").Contains("suscriptor") Then
                If useOnlyTemporaryNotes Then
                    Return "GetResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetActiveResourceSequenceAndMyTemporals"
                End If
            ElseIf userRoles.Split(";").Contains("cliente") Then ' if userRole is Client
                If useOnlyTemporaryNotes Then
                    Return "GetOwnResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetOwnResourceSequenceActiveAndTemporaryState"
                End If
            ElseIf userRoles.Split(",").Contains("suscriptor") Then
                If useOnlyTemporaryNotes Then
                    Return "GetResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetActiveResourceSequenceAndMyTemporals"
                End If
            ElseIf userRoles.Split(",").Contains("cliente") Then ' if userRole is Client
                If useOnlyTemporaryNotes Then
                    Return "GetOwnResourceSequenceTemporaryStateOnly"
                Else
                    Return "GetOwnResourceSequenceActiveAndTemporaryState"
                End If
            End If
        End If
        Return ""   'TODO: Que pasa si es que no es ni administrador, ni cliente
    End Function
    Protected Sub DataSourceNotes_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles DataSourceNotes.Selecting
        If IsNumeric(DneSequenceId) Then
            e.InputParameters.Item("tags") = GetFiltersChecked()
        Else
            e.Cancel = True
        End If
    End Sub

    Protected Function GetFiltersChecked() As List(Of ShortTagDTO)
        Dim tagList As New List(Of ShortTagDTO)
        Dim shortTag As ShortTagDTO
        For Each checkBox As CheckBox In Me.tagFilterContainer.Controls.OfType(Of CheckBox)
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
        GridViewNotes.DataBind()
    End Sub

    ''' <summary>
    ''' Updates the variable TagCheckBoxes in Viewstate based on the checkboxes checked. 
    ''' </summary>
    Private Sub UpdateViewStateTags()
        Dim checkBoxes As Dictionary(Of String, Boolean) = ViewState("DNENotesTagCheckBoxes")
        For Each checkBox As CheckBox In Me.tagFilterContainer.Controls.OfType(Of CheckBox)
            checkBoxes(checkBox.ID) = checkBox.Checked
        Next
        ViewState("DNENotesTagCheckBoxes") = checkBoxes
    End Sub

#End Region


    Protected Sub DataSourceNotes_Selected(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles DataSourceNotes.Selected
        gridViewNoteElements = e.ReturnValue()
    End Sub

    'Protected Sub GridViewNotes_CellEditorInitialize(sender As Object, e As ASPxGridView.ASPxGridViewEditorEventArgs) Handles GridViewNotes.CellEditorInitialize
    '    If e.Column.FieldName.Equals("ExpirationDateTextId") Then
    '        TryCast(e.Editor, ASPxDateEdit).MinDate = DateTime.Now.Subtract(TimeSpan.FromDays(-1))
    '    End If
    'End Sub

    Protected Function GetToken() As String
        Try
            Return ResponseHelper.GetValidToken()
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
Public Class ParameterNotes

    ' Methods
    Public Sub New()
        MyBase.New()
    End Sub


    ' Properties
    Public Property SequenceId As String
    Public Property tagListDTO As List(Of InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ShortTagDTO)
    Public Property TagWithEnumDTO As InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs.ShortTagDTO


End Class
