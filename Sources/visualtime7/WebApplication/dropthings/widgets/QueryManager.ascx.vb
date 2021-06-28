#Region "using"

Imports System.Convert
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports DevExpress.Data
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Imports Dropthings.Widget.Framework
Imports GIT.EDW
Imports GIT.EDW.Query.Model
Imports GIT.EDW.Query.Model.DataRowTemplate
Imports GIT.EDW.Query.Model.Enumerators
Imports GIT.EDW.Query.Model.Extensions
Imports GIT.EDW.Query.Model.Extensions.Enumerations
Imports GIT.EDW.Query.Model.Helpers
Imports GIT.EDW.Query.Model.Widget
Imports InMotionGIT.Actions
Imports InMotionGIT.Actions.Designer.Helpers.Support
Imports InMotionGIT.Actions.Designer.Helpers.UIQuery
Imports InMotionGIT.Common.Enumerations
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.FrontOffice.Proxy

#End Region

Namespace Dropthings.Widgets

    Partial Class QueryManager
        Inherits UserControl
        Implements IWidget

#Region "Private fields"

        Private WithEvents btnOK As New ASPxButton

        Private _stringBuilder As New System.Text.StringBuilder()
        Private _State As XElement

        Private _GridProcessed As Boolean
        Private _requiredFields As Boolean
        Private _rangesFields As Boolean

        Private _currentLanguage As InMotionGIT.Common.Enumerations.EnumLanguage

        Private _schemaLevel As Integer = 0
        Private _InternalRelease As Integer

        Private _InternalModelId As String
        Private _functionNameList As String
        Private _functionValidationsList As String
        Private _functionValidateRangesList As String

        Private _dateFormatList As Dictionary(Of String, String)

#End Region

#Region "Public properties"

        Private _UserInfo As InMotionGIT.Membership.Providers.MemberContext

        Public Property UserInfo() As InMotionGIT.Membership.Providers.MemberContext
            Get
                If _UserInfo.IsEmpty Then
                    _UserInfo = New InMotionGIT.Membership.Providers.MemberContext
                End If
                Return _UserInfo
            End Get
            Set(ByVal value As InMotionGIT.Membership.Providers.MemberContext)
                _UserInfo = value
            End Set
        End Property

        Public Property _metadata As GIT.EDW.Query.Model.metadata
        Public Property _dateformat As String = String.Empty

        Public Property Release() As Integer
            Get
                Dim value As String = State.Element("Release").Value

                If String.IsNullOrEmpty(value) Then
                    Return -1
                Else
                    Return Integer.Parse(value)
                End If
            End Get

            Set(value As Integer)
                State.Element("Release").Value = value
            End Set
        End Property

        Public Property ModelId() As String
            Get
                Dim value As String = State.Element("ModelId").Value

                If String.IsNullOrEmpty(value) Then
                    Return String.Empty
                Else
                    Return value
                End If
            End Get

            Set(value As String)
                State.Element("ModelId").Value = value
            End Set
        End Property

        Public ReadOnly Property State() As XElement
            Get
                If IsNothing(Host) Then
                    _State = <state>
                                 <Release>
                                     <%= Request.QueryString("Release") %>
                                 </Release>
                                 <ModelId>
                                     <%= Request.QueryString("ModelId") %>
                                 </ModelId>
                             </state>
                Else
                    If (IsNothing(_State)) Then _State = XElement.Parse(Host.GetState())
                End If

                Return _State
            End Get
        End Property

        Public Property Host() As IWidgetHost

        Enum QuerySourceEnum As Integer
            GeneralQuery
            ImageDetail
            NoteDetail
        End Enum

        Public Property notesformat() As String = String.Empty

#End Region

#Region "IWidget Members"

        Public Sub Closed() Implements Widget.Framework.IWidget.Closed

        End Sub

        Public Sub HideSettings() Implements Widget.Framework.IWidget.HideSettings
            SaveState()

            InitializeControls()

            InitForm(Nothing, False, Nothing, True)

            Page_Load(Nothing, Nothing)
        End Sub

        Public Sub Init1(host As Widget.Framework.IWidgetHost) Implements Widget.Framework.IWidget.Init
            'LogManager.Init()
            'LogManager.Begin("Init")
            Me.Host = host
        End Sub

        Public Sub Maximized() Implements Widget.Framework.IWidget.Maximized

        End Sub

        Public Sub Minimized() Implements Widget.Framework.IWidget.Minimized

        End Sub

        Public Sub ShowSettings() Implements Widget.Framework.IWidget.ShowSettings

            pnlSettings.Visible = True
            'EnviromentsASPxComboBox.DataBind()
            'EnviromentsASPxComboBox.SelectedItem = EnviromentsASPxComboBox.Items.FindByValue(_InternalRelease.ToString)

            ' EnviromentsASPxComboBox_SelectedIndexChanged(EnviromentsASPxComboBox, Nothing)
        End Sub

        Private Sub SaveState()
            'With QueryComboBox.Value
            '    ModelId = .Split(",")(0)
            '    Release = .Split(",")(1)
            'End With

            _InternalRelease = Release
            _InternalModelId = ModelId

            Dim title As String = QueryTitle(ModelId, Release, InMotionGIT.Common.Enumerations.EnumLanguage.Spanish)
            Dim Current As GIT.Core.PageBase = TryCast(HttpContext.Current.Handler, GIT.Core.PageBase)
            Dim dashboardFacade As DashboardBusiness.DashboardFacade = New DashboardBusiness.DashboardFacade(Current.UserInfo.UserName)
            dashboardFacade.ChangeWidgetIntanceName(UserInfo.UserName, Host.ID, title)

            DirectCast(DirectCast(Host, UserControl).FindControl("WidgetTitleButton"), LinkButton).Text = title

            Dim Xml = State.Xml()
            Host.SaveState(Xml)
        End Sub

#End Region

#Region "Page Events"

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            'LogManager.Begin("Page_Load")

            _InternalRelease = Release

            If _metadata.IsNotEmpty Then
                _InternalModelId = _metadata.ModelId
                _currentLanguage = GetCurrentLanguage()
            Else

                _InternalModelId = ModelId
            End If

            If TreeViewPostBack("$EditWidgetButton") OrElse TreeViewPostBack("$EnviromentsASPxComboBox") OrElse TreeViewPostBack("$QueryComboBox") Then '
                _InternalModelId = String.Empty

            ElseIf TreeViewPostBack("$CancelEditWidgetButton") Then
                ' Dim current As String = "0" & QueryComboBox.Value
                Dim current As String = "0"

                If current = "0" Then
                    _InternalModelId = String.Empty

                ElseIf current.Contains(",") AndAlso current.Split(",")(0) <> _InternalModelId Then
                    _InternalModelId = current.Split(",")(0)
                End If
            End If

            If Not String.IsNullOrEmpty(_InternalModelId) OrElse IsNothing(_metadata) Then
                LoadRepository()
            End If

            With TreeViewTables
                If Not String.IsNullOrEmpty(_InternalModelId) Then

                    If Not String.IsNullOrEmpty(_metadata.root.InitializeMethod) Then
                        Dim result As Dictionary(Of String, Object) = Extend.InitializeMethod.Initialize(_metadata.root.InitializeMethod, UserInfo,
                                                                                                         InMotionGIT.Common.Helpers.Context.HttpValues)

                        If Not IsNothing(result) Then
                            If Not result.ContainsKey("Result") OrElse result("Result") = EnumBehavior.AccessDenied Then

                                Dim message As String = GetGlobalResourceObject("Resource", "DeniedAccess")

                                If result.ContainsKey("Message") AndAlso Not String.IsNullOrEmpty(result("Message")) Then
                                    message = result("Message")
                                End If

                                If Page.IsCallback Then
                                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/dropthings/Error.aspx?id=GEN9001")
                                Else
                                    Response.Redirect("~/dropthings/Error.aspx?id=GEN9001")
                                End If
                            End If
                        Else
                            If Page.IsCallback Then
                                DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/dropthings/Error.aspx?id=GEN9001")
                            Else
                                Response.Redirect("~/dropthings/Error.aspx?id=GEN9001")
                            End If
                        End If
                    End If

                    If Not IsNothing(Session("nSche_level")) Then
                        _schemaLevel = Session("nSche_level")
                    End If

                    If Not Page.IsPostBack And Not String.IsNullOrEmpty(_InternalModelId) Then
                        If Not IsNothing(_metadata.Parameters) AndAlso _metadata.Parameters.Count > 0 Then
                            CreateUserInputParameterControls()
                        End If

                        If Not String.IsNullOrEmpty(Request.QueryString("accept")) OrElse _metadata.root.AutomaticAccept Then
                            .Nodes.Clear()
                        End If

                        InitForm(Nothing, False, Nothing)
                    Else
                        If .Nodes.Count > 0 AndAlso .Nodes(0).ChildNodes.Count = 0 Then
                            .Nodes(0).Selected = True
                        End If

                        If Not IsNothing(_metadata.Parameters) AndAlso _metadata.Parameters.Count > 0 Then
                            pnlUserInput.Visible = True
                            CreateUserInputParameterControls()
                        End If

                        If (TreeViewPostBack("$TreeViewTables")) AndAlso (Not IsNothing(.SelectedNode)) Then
                            ' TreeViewTables.SelectedNode.Expand()
                            Dim haveParent As Boolean = False
                            Dim parentValue As String = String.Empty

                            If Not IsNothing(.SelectedNode.Parent) Then
                                haveParent = True
                                parentValue = .SelectedNode.Parent.Value
                            End If

                            InitForm(GetTableQuery(_metadata, .SelectedNode.Value, haveParent, parentValue),
                                                   .SelectedNode.Value.StartsWith("P"), .SelectedNode)
                        Else
                            If Not IsNothing(.SelectedNode) OrElse (Not IsNothing(.Nodes) AndAlso .Nodes.Count = 1) Then
                                RefreshGrid()
                            Else
                                With GridViewQueries
                                    .Columns.Clear()
                                    .DataSource = Nothing
                                    .DataBind()
                                End With
                            End If
                        End If
                    End If
                Else
                    If IsNothing(GridViewQueries.DataSource) AndAlso _metadata.IsNotEmpty Then
                        ClearGridViewBehavior(_metadata.root.DetailView)
                        CreateUserInputParameterControls()
                    End If
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("notheader")) Then
                    pnlUserInput.Visible = False
                End If
            End With

            If _stringBuilder.Length > 0 Then
                Dim nameUnique As String = _metadata.ModelId.Replace("-", "").Substring(0, 8)

                btnOK.ClientSideEvents.Init = String.Format(CultureInfo.InvariantCulture, "function(s, e) {{{0}}}", _functionNameList)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), String.Format(CultureInfo.InvariantCulture, "FormData_{0}", nameUnique),
                                                   String.Format(CultureInfo.InvariantCulture, "{0};{1};", _stringBuilder.ToString(), _functionNameList), True)
            Else
                btnOK.ClientSideEvents.Click = "function(s, e) { LoadingPanel.Show(); }"
            End If

            'LogManager.Finish("Page_Load")
        End Sub

        Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
            'LogManager.Finish("Init.unload")
            'LogManager.EmptyLine()
        End Sub

#End Region

#Region "Main Methods"

        Private Sub InitializeControls()
            'LogManager.Begin("InitializeControls")
            TreeViewTables.Nodes.Clear()
            GridViewQueries.DataBind()
            pnlUserInput.Controls.Clear()
            pnlSettings.Visible = False
            'LogManager.Finish("InitializeControls")
        End Sub

        Private Sub InitForm(currentTableQuery As tablequery, isPluralNode As Boolean, rootNode As TreeNode, Optional calledFromSettings As Boolean = False)
            'LogManager.Begin("InitForm")

            Dim vloDataTable As DataTable = Nothing
            Dim parentKey As String = String.Empty
            Dim currentKey As String = String.Empty
            Dim Query As String = String.Empty

            If IsNothing(_metadata) Then
                LoadRepository()
            End If

            If IsNothing(currentTableQuery) Then
                currentTableQuery = _metadata.root
            End If

            If Not IsNothing(_metadata.Parameters) AndAlso _metadata.Parameters.Count > 0 Then
                For Each parameterData As QueryParameters In _metadata.Parameters
                    BuildingEnabledControlDependency(parameterData.Name)
                Next
            End If

            DetailASPxLabel.ClientVisible = False

            With GridViewQueries
                If (IsNothing(_metadata.Parameters) OrElse _metadata.Parameters.Count = 0) OrElse (Page.IsPostBack AndAlso Not calledFromSettings) OrElse
                   Not String.IsNullOrEmpty(Request.QueryString("accept")) OrElse _metadata.root.AutomaticAccept Then

                    If Not IsNothing(rootNode) Then
                        If Not IsNothing(rootNode.Parent) Then
                            parentKey = rootNode.Parent.Value
                        End If
                        currentKey = rootNode.Value
                    End If

                    ErrorMsgASPxLabel.Visible = False

                    If currentTableQuery.EntityType = EnumEntityType.Procedure Then
                        vloDataTable = ExecuteStoredProcedure(currentTableQuery, parentKey, currentKey)
                    Else
                        Dim isRootNode As Boolean = False

                        If Not IsNothing(rootNode) Then
                            If IsNothing(rootNode.Parent) Then
                                isRootNode = True
                            End If
                        Else
                            isRootNode = True
                        End If

                        Query = CreateQuery(currentTableQuery, parentKey, currentKey, False)

                        If Not String.IsNullOrEmpty(Query) Then
                            vloDataTable = ExecuteQuery(currentTableQuery, Query, currentKey,
                                                        String.Format(CultureInfo.InvariantCulture, "{0}{1}", currentKey, parentKey), isRootNode)
                        End If
                    End If

                    If (TreeViewPostBack("$TreeViewTables")) AndAlso (Not IsNothing(TreeViewTables.SelectedNode)) Then

                        If rootNode.ChildNodes.Count > 0 AndAlso rootNode.ChildNodes(0).Text = "empty" Then
                            Dim itemTable As tablequery
                            Dim currentTable As tablequery = Nothing
                            Dim pkvalues As String = String.Empty
                            Dim parentIndex As String = String.Empty
                            Dim parentTable As String = String.Empty
                            Dim haveParent As Boolean = False
                            Dim parentValue As String = String.Empty
                            Dim columnList As String = String.Empty

                            parentTable = GetParentTableQuery(rootNode)

                            If Not IsNothing(rootNode.Parent) Then
                                parentIndex = rootNode.Parent.Value
                                If parentIndex.IndexOf(":Index=") > -1 Then
                                    parentIndex = parentIndex.Substring(parentIndex.IndexOf(":Index="))
                                Else
                                    parentIndex = String.Empty
                                End If
                            End If

                            If Not IsNothing(rootNode.Parent) Then
                                haveParent = True
                                parentValue = rootNode.Parent.Value
                            End If

                            rootNode.Value += ":Loaded:"
                            currentTable = GetTableQuery(_metadata, rootNode.Value & parentIndex, haveParent, parentValue)
                            rootNode.ChildNodes.Clear()

                            If Not IsNothing(currentTableQuery.QueryStatements) AndAlso Not IsNothing(currentTableQuery.QueryStatements.ParameterToChilds) Then
                                columnList = currentTableQuery.QueryStatements.ParameterToChilds

                                If currentTableQuery.EntityType = EnumEntityType.Procedure Then

                                    If isPluralNode Then 'Or IsNothing(currentTableQuery.Parent) Then
                                        columnList = columnList.Split("|")(1)
                                        If String.IsNullOrEmpty(columnList) AndAlso currentTableQuery.childs.Count > 0 Then
                                            columnList = currentTableQuery.QueryStatements.ParameterToChilds.Split("|")(0)
                                        End If
                                    Else
                                        columnList = columnList.Split("|")(0)
                                    End If

                                End If
                            End If

                            Try
                                For Each row As DataRow In vloDataTable.Rows

                                    pkvalues = GetColumnsValues(columnList, vloDataTable, row, False)

                                    For index As Integer = 0 To currentTable.childs.Count - 1
                                        itemTable = currentTable.childs(index)

                                        If _schemaLevel >= itemTable.SecurityLevel Then

                                            If itemTable.EntityType = EnumEntityType.Procedure OrElse
                                                Not itemTable.VerifyHaveRecords OrElse
                                               (itemTable.VerifyHaveRecords AndAlso ChildHaveRecords(itemTable, rootNode.Parent.Value, rootNode.Value)) Then

                                                ' pkvalues = rootNode.Value.Split(":")(2)
                                                rootNode.ChildNodes.Add(New TreeNode(itemTable.pluralcaption.GetValue(_currentLanguage), String.Format(CultureInfo.InvariantCulture, "P:{0}:{1}:Index={2}", itemTable.name, pkvalues, itemTable.Key)))
                                            End If
                                        End If
                                    Next
                                Next

                                rootNode.Expanded = True
                            Catch ex As Exception
                                ErrorMsgASPxLabel.Visible = True

                                If Request.QueryString("debug") = "y" Then
                                    ErrorMsgASPxLabel.Text = String.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3}", GetLocalResourceObject("FailQueryMessage").ToString(), ex.Message, vbCrLf, Query)
                                Else
                                    ErrorMsgASPxLabel.Text = GetLocalResourceObject("FailQueryMessage").ToString()
                                End If
                            End Try

                        End If
                    End If

                    If IsNothing(rootNode) AndAlso Not IsNothing(vloDataTable) AndAlso vloDataTable.Rows.Count > 0 Then
                        If Not IsNothing(vloDataTable) AndAlso vloDataTable.Rows.Count > 1 Then
                            isPluralNode = True
                        End If

                        TreeViewTables.Nodes.Clear()

                        If isPluralNode Then
                            rootNode = New TreeNode(currentTableQuery.pluralcaption.GetValue(_currentLanguage), String.Format(CultureInfo.InvariantCulture, "P:{0}:", currentTableQuery.name))
                        Else
                            rootNode = New TreeNode(currentTableQuery.caption.GetValue(_currentLanguage), String.Format(CultureInfo.InvariantCulture, "S:{0}:", currentTableQuery.name))
                        End If

                        TreeViewTables.ShowCheckBoxes = False
                        rootNode.Selected = True
                        rootNode.Expanded = True
                        TreeViewTables.Nodes.Add(rootNode)
                    End If

                    Try
                        If Not IsNothing(vloDataTable) Then
                            If vloDataTable.Rows.Count > 0 OrElse Not currentTableQuery.VerifyHaveRecords Then

                                vloDataTable.TableName = currentTableQuery.name

                                If currentTableQuery.DetailView = EnumDetailView.DataCard Then
                                    .Visible = False
                                    DataRowsCheckBox.ClientVisible = False
                                    DetailASPxLabel.ClientVisible = False
                                    TableExport.Visible = False

                                    With DataViewQueries
                                        .ItemTemplate = New DataCard(currentTableQuery, SetValuesUserInputParameterControls())
                                        .DataSource = vloDataTable
                                        .Visible = True
                                        .DataBind()

                                        If currentTableQuery.RowsPerPage > 0 Then
                                            .SettingsTableLayout.RowsPerPage = currentTableQuery.RowsPerPage
                                        End If

                                        If currentTableQuery.ColumnCount > 0 Then
                                            .SettingsTableLayout.ColumnCount = currentTableQuery.ColumnCount
                                        End If

                                        If currentTableQuery.Transparent Then
                                            .ItemStyle.BackColor = Color.Transparent
                                        End If

                                        If Not currentTableQuery.WithBorder Then
                                            .ItemStyle.BorderStyle = BorderStyle.None
                                        End If

                                        If currentTableQuery.ItemSpacing > 0 Then
                                            .ItemSpacing = New Unit(String.Format(CultureInfo.InvariantCulture, "{0}px", currentTableQuery.ItemSpacing))
                                        End If
                                    End With
                                Else
                                    DataViewQueries.Visible = False

                                    If Not IsNothing(TreeViewTables.SelectedNode) Then
                                        Dim controlCheckBox As ASPxCheckBox = Nothing
                                        Dim isVisibleLabelAction As Boolean = False
                                        Dim isPlural As Boolean = False
                                        Dim isChecked As Boolean = False

                                        If TreeViewTables.SelectedNode.Value.Contains("P:") Then
                                            isPlural = True
                                        End If

                                        If _metadata.AllowHistoryInfo Then
                                            controlCheckBox = FindControlCheckBox(Me, "_AllowHistoryInfo")

                                            If Not IsNothing(controlCheckBox) AndAlso controlCheckBox.Checked Then
                                                isChecked = True
                                            End If
                                        End If

                                        SetGridViewColumns(GridViewQueries, currentTableQuery, isChecked, _currentLanguage, isPlural,
                                                           vloDataTable.Rows.Count, _schemaLevel, notesformat, isVisibleLabelAction,
                                                           _InternalModelId, _InternalRelease)

                                        ActionMsgASPxLabel.Visible = isVisibleLabelAction
                                    End If

                                    If (vloDataTable.Rows.Count = 1 OrElse Not currentTableQuery.VerifyHaveRecords) AndAlso
                                       (currentTableQuery.DetailView = EnumDetailView.Vertical OrElse
                                        currentTableQuery.DetailView = EnumDetailView.Card OrElse
                                        currentTableQuery.DetailView = EnumDetailView.Layout) Then

                                        If String.IsNullOrEmpty(currentTableQuery.title.GetValue(_currentLanguage)) Then
                                            .Caption = currentTableQuery.caption.GetValue(_currentLanguage)
                                        Else
                                            .Caption = currentTableQuery.title.GetValue(_currentLanguage)
                                        End If

                                        Select Case currentTableQuery.DetailView
                                            Case EnumDetailView.Vertical
                                                .Templates.DataRow = New Vertical(currentTableQuery, _currentLanguage)
                                                .Border.BorderWidth = 1

                                            Case EnumDetailView.Card
                                                .Templates.DataRow = New Card(currentTableQuery.NumberOfColumns)
                                                .Border.BorderWidth = 0

                                            Case EnumDetailView.Layout
                                                .Templates.DataRow = New Layout(currentTableQuery.Layout)
                                                .Border.BorderWidth = 0

                                            Case Else
                                        End Select

                                        .Settings.ShowColumnHeaders = False
                                        .Settings.ShowGroupPanel = False

                                        .Settings.ShowVerticalScrollBar = False
                                        .Settings.ShowHorizontalScrollBar = False
                                        .Settings.ShowFilterRow = False
                                        .Settings.ShowFilterRowMenu = False
                                        .GroupSummary.Clear()
                                        .TotalSummary.Clear()
                                        .Settings.ShowFooter = False

                                        If Not currentTableQuery.VerifyHaveRecords AndAlso
                                           (IsNothing(vloDataTable) OrElse IsNothing(vloDataTable.Rows) OrElse vloDataTable.Rows.Count = 0) Then

                                            .Settings.ShowColumnHeaders = True
                                        End If
                                    Else
                                        .Caption = currentTableQuery.title.GetValue(_currentLanguage)
                                        .Settings.ShowColumnHeaders = True
                                        .Templates.DataRow = Nothing
                                        .Border.BorderWidth = 1
                                        SetGridFooter(vloDataTable, currentTableQuery)
                                    End If

                                    With DataRowsCheckBox
                                        .ClientVisible = currentTableQuery.MultiSelect
                                        .Checked = False
                                    End With

                                    .DataSource = vloDataTable
                                    .Visible = True
                                    .DataBind()

                                    If Not currentTableQuery.HaveSummaryVisibleColumns Then
                                        DetailASPxLabel.ClientVisible = True
                                    End If

                                    If currentTableQuery.MultiSelect Then
                                        .Selection.UnselectAll()
                                    End If
                                End If

                                If (IsNothing(rootNode.Parent) AndAlso rootNode.ChildNodes.Count = 0) OrElse
                                   (((Not currentTableQuery.HaveSummaryVisibleColumns Or
                                     (Not IsNothing(currentTableQuery.childs) AndAlso currentTableQuery.childs.Count > 0)) Or
                                     currentTableQuery.EntityType = EnumEntityType.Procedure) AndAlso
                                    rootNode.ChildNodes.Count = 0) Then

                                    NodeProcess(currentTableQuery, vloDataTable, isPluralNode, rootNode)
                                End If
                            Else
                                .DataSource = Nothing
                                .DataBind()
                            End If
                        Else

                            If Not ErrorMsgASPxLabel.Visible Then
                                ErrorMsgASPxLabel.Visible = True
                                ErrorMsgASPxLabel.Text = GetLocalResourceObject("FailDataBaseMessage").ToString()
                            End If
                        End If
                    Catch ex As Exception
                        ErrorMsgASPxLabel.Visible = True
                        ErrorMsgASPxLabel.Text = ex.Message
                    End Try
                End If

                If (Not IsNothing(_metadata.root.childs) AndAlso _metadata.root.childs.Count > 0) OrElse
                   (_metadata.root.EntityType = EnumEntityType.Procedure AndAlso Not String.IsNullOrEmpty(_metadata.root.DetailName)) Then

                    If IsNothing(.DataSource) AndAlso TreeViewTables.Nodes.Count = 0 Then
                        ClearGridViewBehavior(currentTableQuery.DetailView)
                    Else
                        TreeViewTables.Visible = True
                        TdTreeView.Width = "20%"
                        TdGridView.Width = "80%"
                    End If
                Else
                    If TreeViewTables.Nodes.Count > 0 Then
                        TreeViewTables.Nodes(0).Selected = True
                    End If

                    TreeViewTables.Visible = False
                    TdTreeView.Width = "0%"
                    TdGridView.Width = "100%"
                End If
            End With

            _GridProcessed = True
            'LogManager.Finish("InitForm")
        End Sub

        Private Sub ClearGridViewBehavior(detailView As EnumDetailView)
            If detailView = EnumDetailView.DataCard Then
                With DataViewQueries
                    .ItemTemplate = Nothing
                End With
            Else
                With GridViewQueries
                    With .Settings
                        .ShowColumnHeaders = False
                        .ShowGroupPanel = False
                        .ShowVerticalScrollBar = False
                        .ShowHorizontalScrollBar = False
                        .ShowFilterRow = False
                        .ShowFilterRowMenu = False
                        .ShowFooter = False
                        .ShowColumnHeaders = True
                    End With

                    .Columns.Clear()
                    .Caption = String.Empty
                    .GroupSummary.Clear()
                    .TotalSummary.Clear()
                    .Templates.DataRow = Nothing
                    .Border.BorderWidth = 1
                End With
            End If

            TreeViewTables.Visible = False
            TableExport.Visible = False
            ActionMsgASPxLabel.Visible = False

            TdTreeView.Width = "0%"
            TdGridView.Width = "100%"
        End Sub

#End Region

#Region "Controls Events"

        'Protected Sub EnviromentsASPxComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EnviromentsASPxComboBox.SelectedIndexChanged
        '    Dim SessionRole As String = Session("sSche_code")
        '    Dim comboBox As ASPxComboBox = DirectCast(sender, ASPxComboBox)
        '    Dim config As VisualTIME = CType(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)

        '    If Not IsNothing(comboBox.Value) Then

        '        If String.IsNullOrEmpty(SessionRole) Then
        '            SessionRole = config.Security.DefaultRole
        '        End If

        '        QueryDS.SelectCommand = "SELECT CONVERT(varchar(5), Query.QueryId) + ',' + CONVERT(varchar(2) , QueryVersion.Enviroment) AS 'QueryID', " & _
        '                                "Query.Name, Query.Title, Query.Description " & _
        '                                "FROM [Query] " & _
        '                                "INNER JOIN QueryVersionDetail ON Query.QueryId = QueryVersionDetail.QueryId " & _
        '                                "INNER JOIN QueryVersion ON Query.QueryId = QueryVersion.QueryId " & _
        '                                "WHERE QueryVersion.Enviroment='" + comboBox.Value.ToString + "' AND ((QueryVersionDetail.XmlSpecification.value('(/metadata /roles)[1]', 'varchar(30)') Like '%" & SessionRole & "%') " & _
        '                                 "OR (QueryVersionDetail.XmlSpecification.value('(/metadata /roles)[1]', 'varchar(30)') = 'All'))"
        '        With QueryComboBox
        '            .DataSourceID = "QueryDS"
        '            .TextField = "Name"
        '            .ValueField = "QueryID"
        '            .Enabled = True

        '            .DataBind()
        '            .SelectedItem = .Items.FindByValue(String.Format(CultureInfo.InvariantCulture, "{0},{1}", ModelId, Release))
        '        End With
        '    End If
        'End Sub

        Private Sub btnOk_Click(sender As Object, e As EventArgs)
            'LogManager.Begin("btnOk_Click")
            TreeViewTables.Nodes.Clear()

            InitForm(Nothing, False, Nothing)

            'LogManager.Finish("btnOk_Click")
        End Sub

        Protected Sub cpPopupImagePreview_Callback(source As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cpPopupImagePreview.Callback

            Dim sParameterArray() As String = e.Parameter.Split("-")
            Dim visibleIndex As Integer = sParameterArray(0)
            Dim Caller As String = sParameterArray(1)
            Dim FieldName As String = sParameterArray(2)

            Select Case Caller
                Case "ShowImagePreview"
                    BinaryImagePreview.Value = GridViewQueries.GetRowValues(visibleIndex, FieldName)
                Case "ShowImageFull"
                    BinaryImagePreview.Value = GridViewImageDetail.GetRowValues(visibleIndex, FieldName)
            End Select

        End Sub

        Protected Sub TreeViewTables_TreeNodeExpanded(sender As Object, e As WebControls.TreeNodeEventArgs) Handles TreeViewTables.TreeNodeExpanded
            'LogManager.Begin("TreeViewTables_TreeNodeExpanded")

            Dim itemTable As tablequery
            Dim currentTable As tablequery = Nothing
            Dim pkvalues As String = String.Empty
            Dim parentIndex As String = String.Empty
            Dim parentTable As String = String.Empty
            Dim haveParent As Boolean = False
            Dim parentValue As String = String.Empty

            If e.Node.ChildNodes.Count > 0 AndAlso e.Node.ChildNodes(0).Text = "empty" Then
                parentTable = GetParentTableQuery(e.Node)

                If Not IsNothing(e.Node.Parent) Then
                    parentIndex = e.Node.Parent.Value
                    If parentIndex.IndexOf(":Index=") > -1 Then
                        parentIndex = parentIndex.Substring(parentIndex.IndexOf(":Index="))
                    Else
                        parentIndex = String.Empty
                    End If
                End If

                If Not IsNothing(e.Node.Parent) Then
                    haveParent = True
                    parentValue = e.Node.Parent.Value
                End If

                e.Node.Value += ":Loaded:"
                currentTable = GetTableQuery(_metadata, e.Node.Value & parentIndex, haveParent, parentValue)
                e.Node.ChildNodes.Clear()

                For index As Integer = 0 To currentTable.childs.Count - 1
                    itemTable = currentTable.childs(index)

                    If _schemaLevel >= itemTable.SecurityLevel Then

                        If itemTable.EntityType = EnumEntityType.Procedure OrElse
                           Not itemTable.VerifyHaveRecords OrElse (itemTable.VerifyHaveRecords AndAlso ChildHaveRecords(itemTable, e.Node.Parent.Value, e.Node.Value)) Then

                            pkvalues = e.Node.Value.Split(":")(2)
                            e.Node.ChildNodes.Add(New TreeNode(itemTable.pluralcaption.GetValue(_currentLanguage),
                                                               String.Format(CultureInfo.InvariantCulture, "P:{0}:{1}:Index={2}", itemTable.name, pkvalues, itemTable.Key)))
                        End If
                    End If
                Next

                e.Node.Expanded = True
            End If

            'LogManager.Finish("TreeViewTables_TreeNodeExpanded")
        End Sub

#End Region

#Region "GridViewQueries Events"

        Protected Sub GridViewQueries_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridViewQueries.CustomCallback
            Dim sCaller As String
            Dim nIndexSelected As Integer
            Dim haveParent As Boolean = False
            Dim parentValue As String = String.Empty

            If e.Parameters.StartsWith("Mod:", StringComparison.CurrentCultureIgnoreCase) Then
                Try
                    If e.Parameters.StartsWith("Mod:Exp:", StringComparison.CurrentCultureIgnoreCase) Then
                        DataExport(e.Parameters.Split(":")(2))
                    End If
                Catch ex As Exception
                    Throw New InMotionGIT.Common.Exceptions.InMotionGITException("Ha ocurrido una falla al tratar de exporta la información de la consulta.", ex)
                End Try
            Else
                Try
                    Dim sParameterArray() As String = e.Parameters.Split(",")
                    sCaller = sParameterArray(0)
                    nIndexSelected = sParameterArray(1)

                    _metadata = LoadMetadataRepository(sParameterArray(2))

                    If sCaller.Trim.ToLower = "actions" Then
                        Dim currentTable As tablequery = Nothing

                        With TreeViewTables
                            If Not IsNothing(.SelectedNode) Then
                                If Not IsNothing(.SelectedNode.Parent) Then
                                    haveParent = True
                                    parentValue = .SelectedNode.Parent.Value
                                End If

                                currentTable = GetTableQuery(_metadata, .SelectedNode.Value, haveParent, parentValue)
                            End If
                        End With

                        If IsNothing(currentTable) Then
                            currentTable = _metadata.root
                        End If

                        If Not IsNothing(currentTable) Then
                            Dim totalAction As Integer = currentTable.Actions.Count
                            Dim index As Integer = 0

                            If currentTable.MultiSelect Then
                                Dim selectedRows As List(Of Object) = GridViewQueries.GetSelectedFieldValues("RECORDID")
                                Dim selectedIndexs As New Dictionary(Of Integer, DataRow)
                                Dim source As DataTable = GridViewQueries.DataSource
                                Dim navigationUrls As New Dictionary(Of String, String)
                                Dim countRow As Integer = 0

                                For indexCount As Integer = 0 To selectedRows.Count - 1
                                    countRow = 0

                                    For Each item As DataRow In source.Rows
                                        If String.Equals(item("RECORDID").ToString, selectedRows(indexCount).ToString, StringComparison.CurrentCultureIgnoreCase) Then
                                            selectedIndexs.Add(countRow, item)

                                            Exit For
                                        End If

                                        countRow += 1
                                    Next
                                Next

                                For Each indexData As KeyValuePair(Of Integer, DataRow) In selectedIndexs
                                    index = 0

                                    For Each action As Actions.action In currentTable.Actions
                                        navigationUrls.Add(String.Format(CultureInfo.InvariantCulture,
                                                                         "{0}.{1}", indexData.Key, index), SetActionParameters(action, indexData.Value))
                                        index += 1
                                    Next
                                Next

                                Session("QueryMultiSelected") = navigationUrls
                            End If

                            GridViewQueries.JSProperties.Clear()

                            ' Se indica que tiene acciones para que en el End_CallBack se haga el manejo de agregar los link solo en este caso
                            GridViewQueries.JSProperties.Add("cp_WithActions", True)

                            Dim data As DataTable = GridViewQueries.GetRow(nIndexSelected).Dataview.Table.Clone()
                            Dim addAction As Boolean

                            index = 0

                            For Each action As Actions.action In currentTable.Actions
                                addAction = True

                                If Not IsNothing(action.Conditions) AndAlso action.Conditions.Count > 0 Then
                                    data.ImportRow(GridViewQueries.GetRow(nIndexSelected).row)

                                    If data.Select(GenerateConditionsByAction(action.Conditions)).Count = 0 Then
                                        addAction = False
                                    End If
                                End If

                                If addAction Then
                                    ' Se usa para colocar visible solo el número de acciones definidas
                                    GridViewQueries.JSProperties.Add(String.Format(CultureInfo.InvariantCulture, "cp_Item{0}_Visible", index.ToString()), True)
                                    ' Nombre de la accion
                                    GridViewQueries.JSProperties.Add(String.Format(CultureInfo.InvariantCulture, "cp_Item{0}_Name", index.ToString()), action.caption.GetValue(_currentLanguage))
                                    ' Url
                                    GridViewQueries.JSProperties.Add(String.Format(CultureInfo.InvariantCulture, "cp_Item{0}_Url", index.ToString()), GenerateNavigateUrl(currentTable, action, nIndexSelected, index))

                                    index += 1
                                End If
                            Next

                            ' Se usa para colocar invisibles las acciones no definidas
                            For i As Integer = index To 10
                                GridViewQueries.JSProperties.Add(String.Format(CultureInfo.InvariantCulture, "cp_Item{0}_Visible", index.ToString()), False)
                                index += 1
                            Next
                        End If
                    End If
                Catch ex As Exception
                    Throw New InMotionGIT.Common.Exceptions.InMotionGITException("Ha ocurrido una falla al tratar de cargar las acciones para la consulta.", ex)
                End Try
            End If
        End Sub

        Private Sub DataExport(format As String)
            Dim controlCheckBox As ASPxCheckBox = Nothing
            Dim isChecked As Boolean = False
            Dim isPlural As Boolean = False
            Dim haveParent As Boolean = False
            Dim value As String = String.Empty
            Dim parentValue As String = String.Empty

            If _metadata.AllowHistoryInfo Then
                controlCheckBox = FindControlCheckBox(Me, "_AllowHistoryInfo")

                If Not IsNothing(controlCheckBox) AndAlso controlCheckBox.Checked Then
                    isChecked = True
                End If
            End If

            With TreeViewTables
                If Not IsNothing(.SelectedNode) Then
                    value = .SelectedNode.Value
                Else
                    value = .Nodes(0).Value
                End If

                If value.Contains("P:") Then
                    isPlural = True
                End If

                If Not IsNothing(.SelectedNode) AndAlso Not IsNothing(.SelectedNode.Parent) Then
                    haveParent = True
                    parentValue = .SelectedNode.Parent.Value
                End If
            End With

            Session("DataQuery") = ViewState("Query").ToString()
            Session("StoredProcedureParameters") = ViewState("StoredProcedureParameters")

            ASPxWebControl.RedirectOnCallback(String.Format(CultureInfo.InvariantCulture, "/dropthings/widgets/ExportToFile.aspx?indexExpression={0}&haveParentNode={1}&parentNodeValue={2}&isChecked={3}&language={4}&" &
                                            "isPlural={5}&schemaLevel={6}&format={7}&repository={8}&ModelId={9}&Release={10}",
                                            value, haveParent.ToString, parentValue, isChecked.ToString, _currentLanguage.ToString, isPlural.ToString,
                                            _schemaLevel, format, _metadata.Repository, _InternalModelId, _InternalRelease))
        End Sub

        Protected Sub GridViewQueries_DataBound(sender As Object, e As EventArgs) Handles GridViewQueries.DataBound
            'LogManager.Begin("GridViewQueries_DataBound")

            If GridViewQueries.VisibleRowCount > 0 Then
                TableExport.Visible = True
            Else
                TableExport.Visible = False
            End If

            FindRepositoryMetadata()

            Dim currentTable As tablequery = GetSelectedTableNode(0)

            If Not IsNothing(currentTable) Then
                For Each columnData As columnquery In currentTable.columns
                    If Not IsNothing(columnData.Lookup) AndAlso Not IsNothing(columnData.Lookup.QueryTable) Then

                        If Not IsNothing(GridViewQueries.Columns(columnData.RealName.ToUpper)) Then
                            With DirectCast(GridViewQueries.Columns(columnData.RealName.ToUpper), GridViewDataComboBoxColumn).PropertiesComboBox
                                If IsNothing(.DataSource) Then
                                    .DataSource = Caching.GetItem(String.Format(CultureInfo.InvariantCulture, "{0}_{1}", _InternalModelId, columnData.name))
                                End If
                            End With
                        End If
                    End If
                Next
            End If
            'LogManager.Finish("GridViewQueries_DataBound")
        End Sub

        Protected Sub GridViewQueries_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles GridViewQueries.HtmlDataCellPrepared
            If Not IsNothing(e.DataColumn) Then
                Try

                    Dim currentTable As tablequery = GetSelectedTableNode(e.VisibleIndex)
                    Dim data As DataTable = GridViewQueries.GetRow(e.VisibleIndex).Dataview.Table.Clone()

                    If Not IsNothing(currentTable) Then
                        If Not IsNothing(currentTable.StyleList) AndAlso currentTable.StyleList.Count > 0 Then
                            Dim actionIf As InMotionGIT.Actions.Designer.Actions.IfAction
                            Dim columnName As String

                            For Each action As InMotionGIT.Actions.Designer.Base.ActionBase In currentTable.StyleList
                                actionIf = action

                                If actionIf.Enabled Then
                                    For Each actionStyle As InMotionGIT.Actions.Designer.Actions.StyleAction In actionIf.Childs

                                        With actionStyle
                                            If .Enabled AndAlso Not String.IsNullOrEmpty(.ColumnName) Then
                                                For Each columnData As String In .ColumnName.Split(",")
                                                    columnName = columnData.ToUpper

                                                    If columnName.Contains(".") Then
                                                        columnName = columnName.Split(".")(1)
                                                    Else
                                                        If columnName.Contains("@") Then
                                                            columnName = columnName.Split("@")(1)
                                                        End If
                                                    End If

                                                    If String.Equals(e.DataColumn.FieldName, columnName, StringComparison.CurrentCultureIgnoreCase) OrElse
                                                       SearchColumnDataStyle(currentTable, e.DataColumn.FieldName, columnName) Then

                                                        data.ImportRow(GridViewQueries.GetRow(e.VisibleIndex).row)

                                                        If data.Select(actionIf.GenerateStyleCode).Count <> 0 Then
                                                            If Not String.IsNullOrEmpty(.Font) Then
                                                                e.Cell.Font.Name = .Font
                                                            End If

                                                            If Not String.IsNullOrEmpty(.FontStyle) Then
                                                                Select Case .FontStyle
                                                                    Case "Bold"
                                                                        e.Cell.Font.Bold = True

                                                                    Case "Oblique"
                                                                        e.Cell.Font.Italic = True

                                                                    Case "Bold Oblique"
                                                                        e.Cell.Font.Bold = True
                                                                        e.Cell.Font.Italic = True

                                                                    Case Else
                                                                End Select
                                                            End If

                                                            If .Size <> 0.0 Then
                                                                e.Cell.Font.Size = .Size
                                                            End If

                                                            If .ForegroundColor <> 0 Then
                                                                e.Cell.ForeColor = Color.FromArgb(.ForegroundColor)
                                                            End If

                                                            If .BackgroundColor <> 0 Then
                                                                e.Cell.BackColor = Color.FromArgb(.BackgroundColor)
                                                            End If

                                                            If .Underline Then
                                                                e.Cell.Font.Underline = True
                                                            End If

                                                            If .Strikeout Then
                                                                e.Cell.Font.Strikeout = True
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End With

                                    Next
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    Throw New InMotionGIT.Common.Exceptions.InMotionGITException("Ha ocurrido una falla tratando de desplegar el estilo para una columna", ex)
                End Try

            End If
        End Sub

        Protected Sub GridViewQueries_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles GridViewQueries.HtmlRowPrepared
            If e.RowType = GridViewRowType.Data Then
                Try


                    Dim currentTable As tablequery = GetSelectedTableNode(e.VisibleIndex)
                    Dim data As DataTable = GridViewQueries.GetRow(e.VisibleIndex).Dataview.Table.Clone()

                    If Not IsNothing(currentTable) Then
                        data.ImportRow(GridViewQueries.GetRow(e.VisibleIndex).row)

                        If Not IsNothing(currentTable.StyleList) AndAlso currentTable.StyleList.Count > 0 Then
                            Dim actionIf As InMotionGIT.Actions.Designer.Actions.IfAction

                            For Each action As InMotionGIT.Actions.Designer.Base.ActionBase In currentTable.StyleList
                                actionIf = action

                                If actionIf.Enabled Then
                                    If data.Select(actionIf.GenerateStyleCode).Count <> 0 Then
                                        For Each actionStyle As InMotionGIT.Actions.Designer.Actions.StyleAction In actionIf.Childs

                                            With actionStyle
                                                If .Enabled AndAlso String.IsNullOrEmpty(.ColumnName) Then
                                                    If Not String.IsNullOrEmpty(.Font) Then
                                                        e.Row.Font.Name = .Font
                                                    End If

                                                    If Not String.IsNullOrEmpty(.FontStyle) Then
                                                        Select Case .FontStyle
                                                            Case "Bold"
                                                                e.Row.Font.Bold = True

                                                            Case "Oblique"
                                                                e.Row.Font.Italic = True

                                                            Case "Bold Oblique"
                                                                e.Row.Font.Bold = True
                                                                e.Row.Font.Italic = True

                                                            Case Else
                                                        End Select
                                                    End If

                                                    If .Size <> 0.0 Then
                                                        e.Row.Font.Size = .Size
                                                    End If

                                                    If .ForegroundColor <> 0 Then
                                                        e.Row.ForeColor = Color.FromArgb(.ForegroundColor)
                                                    End If

                                                    If .BackgroundColor <> 0 Then
                                                        e.Row.BackColor = Color.FromArgb(.BackgroundColor)
                                                    End If

                                                    If .Underline Then
                                                        e.Row.Font.Underline = True
                                                    End If

                                                    If .Strikeout Then
                                                        e.Row.Font.Strikeout = True
                                                    End If
                                                End If
                                            End With

                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    Throw New InMotionGIT.Common.Exceptions.InMotionGITException("Ha ocurrido una falla tratando de desplegar el estilo para una fila", ex)
                End Try
            End If
        End Sub

        Protected Sub GridViewQueries_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs) Handles GridViewQueries.CustomColumnDisplayText
            Dim verb As String = String.Empty

            Try
                FindRepositoryMetadata()

                Dim data As DataTable
                Dim rows() As DataRow
                Dim currentTable As tablequery = GetSelectedTableNode(0)
                Dim conditions As String = String.Empty
                Dim codeField As String = String.Empty
                Dim dependencyField As String = String.Empty


                If Not IsNothing(currentTable) Then


                    For Each columnData As columnquery In currentTable.columns
                        With columnData
                            verb = String.Format("column '{0}'", .name)
                            If Not IsNothing(.Lookup) AndAlso Not IsNothing(.Lookup.QueryTable) Then
                                With .Lookup
                                    If Not IsNothing(.Dependency) AndAlso .Dependency.Count > 0 Then

                                        If String.Equals(e.Column.FieldName, columnData.RealName, StringComparison.CurrentCultureIgnoreCase) Then

                                            With DirectCast(GridViewQueries.Columns(e.Column.FieldName.ToUpper), GridViewDataComboBoxColumn).PropertiesComboBox
                                                If IsNothing(.DataSource) Then
                                                    .DataSource = Caching.GetItem(String.Format(CultureInfo.InvariantCulture, "{0}_{1}", _InternalModelId, columnData.name))
                                                End If

                                                data = .DataSource
                                            End With

                                            For Each dependencyData As InMotionGIT.Actions.Designer.DataDependency In .Dependency
                                                With dependencyData

                                                    verb &= String.Format(" dependency codefield '{0}' with controlname '{1}'", .CodeField, .ControlName)

                                                    If .CodeField.Contains("@") Then
                                                        codeField = .CodeField.Split("@")(1).ToUpper
                                                    Else
                                                        codeField = .CodeField.Split(".")(1).ToUpper
                                                    End If

                                                    If conditions.Length > 0 Then
                                                        conditions += " AND "
                                                    End If

                                                    If .ControlName.Contains("@") Then
                                                        dependencyField = .ControlName.Split("@")(1).ToUpper
                                                    Else
                                                        dependencyField = .ControlName.Split(".")(1).ToUpper
                                                    End If

                                                    conditions += String.Format(CultureInfo.InvariantCulture, "{0} = {1}",
                                                                            codeField, GetCodeFieldDbType(.CodeFieldType, e.GetFieldValue(dependencyField)))
                                                End With
                                            Next

                                            rows = data.Select(String.Format(CultureInfo.InvariantCulture, "{0} AND {1} = {2}", conditions, .Code.ToUpper, e.Value))

                                            If rows.Count > 0 Then
                                                e.DisplayText = rows(0)(.Description(0).Name.ToUpper)
                                            Else
                                                e.DisplayText = String.Empty
                                            End If
                                        End If
                                    End If
                                End With
                            End If
                        End With
                    Next
                End If
            Catch ex As Exception
                LogHandler.ErrorLog("CustomColumnDisplayText", verb, ex)
                Throw New InMotionGIT.Common.Exceptions.InMotionGITException("Ha ocurrido una falla tratando de desplegar la descripción para una columna")
            End Try
        End Sub

#End Region

#Region "GridViewImageDetail Events"

        Protected Sub GridViewImageDetail_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridViewImageDetail.CustomCallback
            'ViewState("QueryImageDetail") = "SELECT * FROM IMAGES WHERE NIMAGENUM = " & e.Parameters.ToString
            Session("QueryImageDetail") = "SELECT NIMAGENUM, DCOMPDATE, NCONSEC, SDESCRIPT, IIMAGE, DNULLDATE, NRECTYPE, NUSERCODE FROM {OWNER}IMAGES WHERE NIMAGENUM = " & e.Parameters.ToString
            GridViewImageDetail.DataBind()
        End Sub

        Protected Sub GridViewImageDetail_DataBinding(sender As Object, e As EventArgs) Handles GridViewImageDetail.DataBinding
            Dim haveParent As Boolean = False
            Dim parentValue As String = String.Empty

            If Not IsNothing(TreeViewTables.SelectedNode.Parent) Then
                haveParent = True
                parentValue = TreeViewTables.SelectedNode.Parent.Value
            End If

            GridViewImageDetail.DataSource = ExecuteQuery(GetTableQuery(_metadata, TreeViewTables.SelectedNode.Value, haveParent, parentValue),
                                                          Session("QueryImageDetail"), QuerySourceEnum.ImageDetail, String.Empty, String.Empty, Not haveParent)
        End Sub

#End Region

#Region "GridViewNoteDetail Events"

        Protected Sub GridViewNoteDetail_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridViewNoteDetail.CustomCallback
            Session("QueryNoteDetail") = "SELECT NNOTENUM, DCOMPDATE, NCONSEC, SDESCRIPT, TDS_TEXT, DNULLDATE, NRECTYPE, NUSERCODE, NDEPT_OWNER FROM {OWNER}NOTES WHERE NNOTENUM = " & e.Parameters.ToString
            GridViewNoteDetail.DataBind()
        End Sub

        Protected Sub GridViewNoteDetail_DataBinding(sender As Object, e As EventArgs) Handles GridViewNoteDetail.DataBinding
            Dim haveParent As Boolean = False
            Dim parentValue As String = String.Empty

            If Not IsNothing(TreeViewTables.SelectedNode.Parent) Then
                haveParent = True
                parentValue = TreeViewTables.SelectedNode.Parent.Value
            End If

            GridViewNoteDetail.DataSource = ExecuteQuery(GetTableQuery(_metadata, TreeViewTables.SelectedNode.Value, haveParent, parentValue),
                                                         Session("QueryNoteDetail"), QuerySourceEnum.NoteDetail, String.Empty, String.Empty, Not haveParent)
        End Sub

#End Region

#Region "Query Execution Methods"

        Private Function CreateQuery(tablequery As tablequery, parentKey As String, currentKey As String, isTest As Boolean) As String
            'LogManager.Begin("CreateQuery")

            Dim sql As String = String.Empty
            Dim value As String = String.Empty
            Dim parameterList As String = String.Empty
            Dim currentLocalKey As String = currentKey & parentKey

            SearchDateFormatBySource(tablequery.Source)

            If isTest Then
                sql = tablequery.QueryStatements.IsExist
                parameterList = tablequery.QueryStatements.AllParameters
            Else
                If currentKey.StartsWith("S:") Then
                    sql = tablequery.QueryStatements.RetrieveByKey
                    parameterList = tablequery.QueryStatements.KeyParameters
                Else
                    sql = tablequery.QueryStatements.RetrieveAll
                    parameterList = tablequery.QueryStatements.AllParameters
                End If
            End If
            If Not String.IsNullOrEmpty(parameterList) Then
                For Each parameterItem As String In parameterList.Split(",")
                    Select Case parameterItem.Substring(0, 1)
                        Case "["    'UserInput
                            value = GetParameterValueFromUserControl(parameterItem.Replace("[", "").Replace("]", ""), True, True)
                        Case "#"    'Profile
                            value = GetParameterValueFromUserProfile(parameterItem)
                        Case "{"    'TreeNode.
                            value = GetParameterValueFromKeyNode(parameterItem, currentLocalKey)
                    End Select

                    If value = "NULL" Then
                        sql = String.Empty
                        Exit For
                    End If

                    If value.Contains("TO_DATE('") Then
                        sql = sql.Replace(String.Format(CultureInfo.InvariantCulture, "'{0}'", parameterItem), value)
                    Else
                        sql = sql.Replace(parameterItem, value)
                    End If

                    'TODO: Correccion temporal hasta que ste diponible la nueva version de la extensiones disponible en inmotiongit.common, para el replazo en los string ReplaceIgnoreCase
                    If parameterItem.Equals("#LanguageID#", StringComparison.CurrentCultureIgnoreCase) Then
                        sql = sql.Replace("#LanguageID#", value)
                        sql = sql.Replace("#LANGUAGEID#", value)
                    End If
                Next
            Else
                sql = sql
            End If

            Dim controlCheckBox As ASPxCheckBox = FindControlCheckBox(Me, "_AllowHistoryInfo")

            If Not IsNothing(controlCheckBox) AndAlso controlCheckBox.Checked Then
                Dim beginIndex As Integer = sql.IndexOf("@@BEGIN_HISTORICAL_MODE@@") + 25

                If beginIndex > -1 Then
                    Dim endIndex As Integer = sql.IndexOf("@@END_HISTORICAL_MODE@@", beginIndex)

                    If endIndex > -1 Then
                        Dim condition As String = sql.Substring(beginIndex, endIndex - beginIndex)

                        sql = sql.Replace(condition, String.Empty)

                        sql = sql.Replace(" AND @@BEGIN_HISTORICAL_MODE@@", String.Empty)
                        sql = sql.Replace("@@BEGIN_HISTORICAL_MODE@@", String.Empty)
                        sql = sql.Replace("@@END_HISTORICAL_MODE@@", String.Empty)
                    End If
                End If

            Else
                sql = sql.Replace("@@BEGIN_HISTORICAL_MODE@@", String.Empty)
                sql = sql.Replace("@@END_HISTORICAL_MODE@@", String.Empty)
            End If

            'LogManager.Finish("CreateQuery")
            Return sql
        End Function

        Private Function ExecuteQuery(currentTable As tablequery, query As String, currentKey As String, currentLocalKey As String,
                                      isRoot As Boolean, Optional QuerySource As QuerySourceEnum = QuerySourceEnum.GeneralQuery) As DataTable
            Dim rowCount As Long = 0
            'LogManager.Begin("ExecuteQuery", query)

            Dim result As New DataTable

            Select Case QuerySource
                Case QuerySourceEnum.GeneralQuery
                    ViewState("Query") = query
                Case QuerySourceEnum.ImageDetail
                    ViewState("QueryImageDetail") = query
                Case QuerySourceEnum.NoteDetail
                    ViewState("QueryNoteDetail") = query
            End Select

            Session("StoredProcedureParameters") = Nothing

            Try
                If Request.QueryString("debug") = "y" Then
                    With ErrorMsgASPxLabel
                        .Visible = True
                        .Text = query
                        .ForeColor = Color.Red
                    End With
                End If

                With New InMotionGIT.Common.Proxy.DataManagerFactory(query, "QueryManager", String.Format("Linked.{0}", SetCurrentSourceName(currentTable)))
                    .MaxNumberOfRecord = currentTable.MaxNumberOfRecords
                    result = .QueryExecuteToTable(True)
                    rowCount = .QueryCountResult

                    If Not IsNothing(result) AndAlso (Not IsNothing(result.Rows) AndAlso result.Rows.Count > 0) Then
                        result.ReadOnlyMode(False)
                    End If
                End With

                If result.IsNotEmpty AndAlso result.Rows.IsNotEmpty AndAlso result.Rows.Count <> rowCount Then
                    With ErrorMsgASPxLabel
                        .Visible = True
                        .Text = String.Format(CultureInfo.InvariantCulture, "Solo se pueden mostrar los primeros {0} registros.", currentTable.MaxNumberOfRecords)
                        .ForeColor = Color.Green
                    End With

                Else
                    If Request.QueryString("debug") = "y" Then
                        With ErrorMsgASPxLabel
                            .Visible = True
                            .Text = query
                            .ForeColor = Color.Red
                        End With

                    Else
                        With ErrorMsgASPxLabel
                            .Visible = False
                            .ForeColor = Color.Red
                        End With
                    End If
                End If
            Catch ex As InMotionGIT.Common.Exceptions.InMotionGITException

                ErrorMsgASPxLabel.Visible = True
                ErrorMsgASPxLabel.Text = "The database connection is failing. Refresh the page and try again."
            Catch ex As Exception
                ErrorMsgASPxLabel.Visible = True

                If Request.QueryString("debug") = "y" Then
                    ErrorMsgASPxLabel.Text = "Failed to execute the query. " & ex.Message & vbCrLf & query
                Else
                    ErrorMsgASPxLabel.Text = "Failed to execute the query."
                End If
                'Throw New Exception(ex.Message & query)
            End Try

            '+ Si el campo está encriptado o se le debe aplicar el acuerdo de confidencialidad
            If currentTable.HaveEncryptedColumns Then
                Dim columnName As String = String.Empty

                For Each column As columnquery In currentTable.columns
                    If column.Encrypted Or column.Confidentiality Then

                        For Each row As DataRow In result.Rows
                            columnName = String.Format(CultureInfo.InvariantCulture, "{0}Confidentiality,", column.RealName.ToUpper)

                            If columnName.Length >= 26 Then
                                columnName = columnName.Substring(0, 25)
                            End If

                            If column.Encrypted AndAlso Not String.Equals(currentTable.Source, "LatCombined", StringComparison.CurrentCultureIgnoreCase) Then
                                row(columnName) = DecryptString(row(column.RealName.ToUpper).ToString)
                            End If

                            '+ Si el nivel del esquema de seguridad asociado al usuario es menor que
                            '+ el minimo requerido para consultar el(campo) se aplica el Acuerdo de Confidencialidad
                            If Not IsNothing(column.ConfidentialityAgreement) AndAlso
                               _schemaLevel < column.ConfidentialityAgreement.SecurityLevel Then

                                If column.Confidentiality Then
                                    If Not IsDBNull(row(columnName)) Then
                                        row(columnName) = ApplyConfidentialityAgreement(row(columnName),
                                                                                        column.ConfidentialityAgreement.DisplayRule,
                                                                                        column.ConfidentialityAgreement.NumbersOfPositions)
                                    Else
                                        row(columnName) = String.Empty
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            End If

            'LogManager.Finish("ExecuteQuery")

            If currentTable.EntityType = EnumEntityType.TableQuery AndAlso
              (Not IsNothing(result) AndAlso Not IsNothing(result.Rows) AndAlso result.Rows.Count > 0) Then

                Dim recordIdColumn As New DataColumn
                Dim index As Integer = 1

                With recordIdColumn
                    .DataType = GetType(String)
                    .ColumnName = "RECORDID"
                End With

                result.Columns.Add(recordIdColumn)

                For Each row As DataRow In result.Rows
                    row("RECORDID") = index
                    index += 1
                Next

                If currentKey.StartsWith("S:") AndAlso Not isRoot Then
                    Dim recordID As Integer = GetParameterValueFromKeyNode("RECORDID", currentLocalKey)

                    Dim rows() As DataRow = result.Select(String.Format(CultureInfo.InvariantCulture, "RECORDID = '{0}'", recordID))

                    If rows.Count > 0 Then
                        Dim clonedResult As DataTable = result.Clone

                        With clonedResult
                            .Rows.Clear()
                            .ImportRow(rows(0))
                        End With

                        Return clonedResult
                    End If
                End If
            End If

            Return result
        End Function

        Private Function ExecuteQueryScalar(entity As tablequery, query As String) As Integer
            'LogManager.Begin("ExecuteQueryScalar", query)

            Dim count As Long = 0

            Try
                With New InMotionGIT.Common.Proxy.DataManagerFactory(query, "QueryManager", String.Format("Linked.{0}", SetCurrentSourceName(entity)))
                    count = .QueryExecuteScalarToInteger
                End With
            Catch ex As InMotionGIT.Common.Exceptions.InMotionGITException
                ErrorMsgASPxLabel.Visible = True
                ErrorMsgASPxLabel.Text = "The database connection is failing. Refresh the page and try again."
            Catch ex As Exception
                Throw New Exception(ex.Message & query)
            End Try

            'LogManager.Finish("ExecuteQueryScalar")

            Return count
        End Function

#End Region

#Region "GridView Design Methods"

        Private Sub RefreshGrid()
            'LogManager.Begin("RefreshGrid")
            Dim currentTableQuery As tablequery = Nothing
            Dim vloDataTable As DataTable = Nothing
            Dim currentKey As String = String.Empty

            With TreeViewTables
                Dim haveParent As Boolean = False
                Dim parentValue As String = String.Empty

                If Not IsNothing(.SelectedNode) AndAlso Not IsNothing(.SelectedNode.Parent) Then
                    haveParent = True
                    parentValue = .SelectedNode.Parent.Value
                End If

                If Not IsNothing(.SelectedNode) Then
                    currentKey = .SelectedNode.Value
                Else
                    currentKey = .Nodes(0).Value
                End If

                currentTableQuery = GetTableQuery(_metadata, currentKey, haveParent, parentValue)

                If currentTableQuery.EntityType = EnumEntityType.Procedure Then
                    Dim parentKey As String = String.Empty

                    If Not IsNothing(.SelectedNode) AndAlso Not IsNothing(.SelectedNode.Parent) Then parentKey = .SelectedNode.Parent.Value

                    vloDataTable = ExecuteStoredProcedure(currentTableQuery, parentKey, currentKey)
                Else
                    Dim isRootNode As Boolean = False

                    If Not IsNothing(.SelectedNode) Then
                        If IsNothing(.SelectedNode.Parent) Then
                            isRootNode = True
                        End If
                    Else
                        isRootNode = True
                    End If

                    vloDataTable = ExecuteQuery(GetTableQuery(_metadata, currentKey, haveParent, parentValue), ViewState("Query").ToString, currentKey,
                                                String.Format(CultureInfo.InvariantCulture, "{0}{1}", currentKey, parentValue), isRootNode)
                End If
            End With

            With GridViewQueries
                If Not IsNothing(vloDataTable) Then
                    ErrorMsgASPxLabel.Visible = False

                    If currentTableQuery.DetailView = EnumDetailView.DataCard Then
                        .Visible = False

                        DataRowsCheckBox.ClientVisible = False
                        DetailASPxLabel.ClientVisible = False
                        TableExport.Visible = False

                        With DataViewQueries
                            .DataSource = vloDataTable
                            .Visible = True
                            .ItemTemplate = New DataCard(currentTableQuery, SetValuesUserInputParameterControls())
                            .DataBind()

                            If currentTableQuery.RowsPerPage > 0 Then
                                .SettingsTableLayout.RowsPerPage = currentTableQuery.RowsPerPage
                            End If

                            If currentTableQuery.ColumnCount > 0 Then
                                .SettingsTableLayout.ColumnCount = currentTableQuery.ColumnCount
                            End If

                            If currentTableQuery.Transparent Then
                                .ItemStyle.BackColor = Color.Transparent
                            End If

                            If Not currentTableQuery.WithBorder Then
                                .ItemStyle.BorderStyle = BorderStyle.None
                            End If

                            If currentTableQuery.ItemSpacing > 0 Then
                                .ItemSpacing = New Unit(String.Format(CultureInfo.InvariantCulture, "{0}px", currentTableQuery.ItemSpacing))
                            End If
                        End With
                    Else
                        DataViewQueries.Visible = False

                        If vloDataTable.Rows.Count = 1 AndAlso
                          (currentTableQuery.DetailView = EnumDetailView.Vertical OrElse currentTableQuery.DetailView = EnumDetailView.Card OrElse
                           currentTableQuery.DetailView = EnumDetailView.Layout) Then

                            If String.IsNullOrEmpty(currentTableQuery.title.GetValue(_currentLanguage)) Then
                                .Caption = currentTableQuery.caption.GetValue(_currentLanguage)
                            Else
                                .Caption = currentTableQuery.title.GetValue(_currentLanguage)
                            End If

                            Select Case currentTableQuery.DetailView
                                Case EnumDetailView.Vertical
                                    .Templates.DataRow = New Vertical(currentTableQuery, _currentLanguage)
                                    .Border.BorderWidth = 1

                                Case EnumDetailView.Card
                                    .Templates.DataRow = New Card(currentTableQuery.NumberOfColumns)
                                    .Border.BorderWidth = 0

                                Case EnumDetailView.Layout
                                    .Templates.DataRow = New Layout(currentTableQuery.Layout)
                                    .Border.BorderWidth = 0

                                Case Else
                            End Select

                            .Settings.ShowColumnHeaders = False
                            .Settings.ShowGroupPanel = False

                            .Settings.ShowVerticalScrollBar = False
                            .Settings.ShowHorizontalScrollBar = False
                            .Settings.ShowFilterRow = False
                            .Settings.ShowFilterRowMenu = False
                            .GroupSummary.Clear()
                            .TotalSummary.Clear()
                            .Settings.ShowFooter = False
                        Else
                            .Settings.ShowColumnHeaders = True
                            .Templates.DataRow = Nothing
                            .Border.BorderWidth = 1
                        End If

                        .DataSource = vloDataTable
                        .Visible = True
                        .DataBind()
                    End If
                Else
                    If Not ErrorMsgASPxLabel.Visible Then
                        ErrorMsgASPxLabel.Visible = True
                        ErrorMsgASPxLabel.Text = "The database connection is failing. Refresh the page and try again."
                    End If
                End If
            End With

            'LogManager.Finish("RefreshGrid")
        End Sub

        Private Sub SetGridFooter(vloDataTable As DataTable, currentTableQuery As tablequery)
            'LogManager.Begin("SetGridFooter")

            Dim ShowFooter As Boolean = False
            Dim Column As columnquery = Nothing
            Dim element As String = String.Empty
            Dim Scale As Integer = 0
            Dim Size As Integer = 0

            With GridViewQueries
                If Not IsNothing(.GroupSummary) AndAlso .GroupSummary.Count > 0 Then
                    .GroupSummary.Clear()
                End If

                If Not IsNothing(.TotalSummary) AndAlso .TotalSummary.Count > 0 Then
                    .TotalSummary.Clear()
                End If
            End With

            For index As Integer = 0 To currentTableQuery.columns.Count - 1
                Column = currentTableQuery.columns(index)
                If Column.Aggregate <> enumAggregate.None Then

                    element = Column.name.Split(".")(1).ToUpper

                    ShowFooter = True
                    GridViewQueries.Columns(index).FooterCellStyle.HorizontalAlign = HorizontalAlign.Right

                    Scale = Column.scale
                    Size = Column.precision

                    Select Case Column.Aggregate
                        Case enumAggregate.Average
                            GridViewQueries.TotalSummary.Add(SummaryItemType.Average, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0}}", Column.AggregateLabel.GetValue(_currentLanguage))
                            GridViewQueries.GroupSummary.Add(SummaryItemType.Average, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0}}", Column.AggregateLabel.GetValue(_currentLanguage))

                        Case enumAggregate.Count
                            GridViewQueries.TotalSummary.Add(SummaryItemType.Count, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0}}", Column.AggregateLabel.GetValue(_currentLanguage))
                            GridViewQueries.GroupSummary.Add(SummaryItemType.Count, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0}}", Column.AggregateLabel.GetValue(_currentLanguage))

                        Case enumAggregate.Maximum
                            GridViewQueries.TotalSummary.Add(SummaryItemType.Max, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0:N{1}}}", Column.AggregateLabel.GetValue(_currentLanguage), Scale)
                            GridViewQueries.GroupSummary.Add(SummaryItemType.Max, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0:N{1}}}", Column.AggregateLabel.GetValue(_currentLanguage), Scale)

                        Case enumAggregate.Minimum
                            GridViewQueries.TotalSummary.Add(SummaryItemType.Min, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0:N{1}}}", Column.AggregateLabel.GetValue(_currentLanguage), Scale)
                            GridViewQueries.GroupSummary.Add(SummaryItemType.Min, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0:N{1}}}", Column.AggregateLabel.GetValue(_currentLanguage), Scale)

                        Case Else 'Sum
                            GridViewQueries.TotalSummary.Add(SummaryItemType.Sum, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0:N{1}}}", Column.AggregateLabel.GetValue(_currentLanguage), Scale)
                            GridViewQueries.GroupSummary.Add(SummaryItemType.Sum, element).DisplayFormat = String.Format(CultureInfo.InvariantCulture, "{0} {{0:N{1}}}", Column.AggregateLabel.GetValue(_currentLanguage), Scale)

                    End Select
                End If
            Next

            GridViewQueries.Settings.ShowFooter = ShowFooter
            'LogManager.Finish("SetGridFooter")
        End Sub

#End Region

#Region "TreeView Design Methods"

        Private Function TreeViewPostBack(controlName As String) As Boolean
            'LogManager.Begin("TreeViewPostBack")
            Dim result As Boolean = False
            For index As Integer = 0 To Request.Params.Count - 1
                If Request.Params(index).EndsWith(controlName) Then
                    result = True
                    Exit For
                End If
            Next
            'LogManager.Finish("TreeViewPostBack")
            Return result
        End Function

        Private Sub NodeProcess(currentTableQuery As tablequery, vloDataTable As DataTable, isPluralNode As Boolean, rootNode As TreeNode)
            'LogManager.Begin("NodeProcess")

            Dim pkvalues As String = String.Empty
            Dim IdentifyValues As String = String.Empty
            Dim rowValue As String = String.Empty
            Dim columnName As String = String.Empty
            Dim childNode As TreeNode = Nothing
            Dim item As tablequery = Nothing
            Dim columnItem As columnquery = Nothing
            Dim dDTime As Date = Date.MinValue
            Dim columnList As String = String.Empty
            Dim addRecordValue As Boolean = False

            If currentTableQuery.EntityType = EnumEntityType.TableQuery OrElse
              (currentTableQuery.EntityType = EnumEntityType.Procedure AndAlso currentTableQuery.DetailName.IsEmpty) Then

                addRecordValue = True
            End If

            If Not IsNothing(currentTableQuery.QueryStatements) AndAlso Not IsNothing(currentTableQuery.QueryStatements.ParameterToChilds) Then
                columnList = currentTableQuery.QueryStatements.ParameterToChilds

                If currentTableQuery.EntityType = EnumEntityType.Procedure Then

                    If isPluralNode Then
                        columnList = columnList.Split("|")(1)
                        If String.IsNullOrEmpty(columnList) AndAlso currentTableQuery.childs.Count > 0 Then
                            columnList = currentTableQuery.QueryStatements.ParameterToChilds.Split("|")(0)
                        End If
                    Else
                        columnList = columnList.Split("|")(0)
                    End If

                End If
            End If

            Try
                For Each row As DataRow In vloDataTable.Rows

                    pkvalues = GetColumnsValues(columnList, vloDataTable, row, addRecordValue)

                    If isPluralNode Then
                        IdentifyValues = String.Empty

                        If currentTableQuery.Identify.Length > 0 Then

                            For Each idElement As String In currentTableQuery.Identify.Split(",")
                                If IdentifyValues.Length > 0 Then
                                    IdentifyValues += currentTableQuery.SeparatorIdentify
                                End If

                                If idElement.IndexOf(".") > -1 Then
                                    idElement = idElement.Split(".")(1)

                                ElseIf idElement.IndexOf("@") > -1 Then
                                    idElement = idElement.Split("@")(1)
                                End If

                                columnItem = currentTableQuery.FindColumnByRealName(idElement)

                                If Not IsNothing(columnItem) Then

                                    If Not String.IsNullOrEmpty(columnItem.ColumnSource.RelationshipTable) And
                                       Not String.IsNullOrEmpty(columnItem.ColumnSource.RelationshipColumns) Then

                                        If columnItem.ColumnSource.StyleValue <> enumShownValue.Code Then
                                            idElement += "DESC"
                                        End If
                                    End If

                                    '+ Se ubica el elemento en el DataTable vloDataTableConfidentiality para
                                    '+ que se muestre en el link del treeview la informacion correctamente
                                    '+ en caso que se haya aplicado el Acuerdo de Confidencialidad
                                    If (columnItem.Encrypted OrElse columnItem.Confidentiality) Then

                                        idElement += "Confidentiality"

                                        If idElement.Length >= 26 Then
                                            idElement = idElement.Substring(0, 25)
                                        End If
                                    End If

                                End If
                                IdentifyValues += row.Item(idElement).ToString.Trim
                            Next
                        End If

                        If IdentifyValues.Length > 0 Then
                            IdentifyValues = String.Format(CultureInfo.InvariantCulture, "{0} {1}", currentTableQuery.caption.GetValue(_currentLanguage), IdentifyValues)
                        Else
                            IdentifyValues = currentTableQuery.caption.GetValue(_currentLanguage)
                        End If

                        If currentTableQuery.EntityType = EnumEntityType.Procedure Then
                            If currentTableQuery.childs.Count > 0 Or currentTableQuery.DetailName <> String.Empty Then
                                childNode = New TreeNode(IdentifyValues, String.Format(CultureInfo.InvariantCulture, "S:{0}:({1})", currentTableQuery.name, pkvalues))
                                rootNode.ChildNodes.Add(childNode)
                            End If
                        Else
                            childNode = New TreeNode(IdentifyValues, String.Format(CultureInfo.InvariantCulture, "S:{0}:({1})", currentTableQuery.name, pkvalues))
                            rootNode.ChildNodes.Add(childNode)
                        End If
                    Else
                        childNode = rootNode
                    End If

                    If Not IsNothing(childNode) Then
                        If childNode.ChildNodes.Count = 0 Then

                            If currentTableQuery.name <> _metadata.root.name And currentTableQuery.childs.Count > 0 Then
                                If Not childNode.Value.Contains(":Loaded:") Then
                                    childNode.ChildNodes.Add(New TreeNode("empty"))
                                End If
                            Else

                                If currentTableQuery.EntityType = EnumEntityType.Table OrElse currentTableQuery.EntityType = EnumEntityType.TableQuery OrElse
                                    currentTableQuery.EntityType = EnumEntityType.Procedure Then

                                    For index As Integer = 0 To currentTableQuery.childs.Count - 1
                                        item = currentTableQuery.childs(index)

                                        '+ Solo se muestra el link si el nivel del esquema de seguridad asociado al usuario
                                        '+ es mayor o igual que el requerido en la configuracion de la consulta
                                        If _schemaLevel >= item.SecurityLevel Then

                                            If currentTableQuery.EntityType = EnumEntityType.Procedure OrElse
                                               item.EntityType = EnumEntityType.Procedure Then

                                                childNode.ChildNodes.Add(New TreeNode(item.pluralcaption.GetValue(_currentLanguage), String.Format(CultureInfo.InvariantCulture, "P:{0}:({1}):Index={2}", item.name, pkvalues, item.Key)))
                                            Else
                                                If Not item.VerifyHaveRecords OrElse
                                                  (item.VerifyHaveRecords AndAlso ChildHaveRecords(item, String.Format(CultureInfo.InvariantCulture, "S:{0}:({1})", currentTableQuery.name, pkvalues), String.Format(CultureInfo.InvariantCulture, "P:{0}:({1}):Index={2}", item.name, pkvalues, item.Key))) Then
                                                    childNode.ChildNodes.Add(New TreeNode(item.pluralcaption.GetValue(_currentLanguage), String.Format(CultureInfo.InvariantCulture, "P:{0}:({1}):Index={2}", item.name, pkvalues, item.Key)))
                                                    'childNode.ChildNodes(childNode.ChildNodes.Count - 1).Expanded = True
                                                End If
                                            End If
                                        End If
                                    Next
                                Else
                                    For index As Integer = 0 To currentTableQuery.childs.Count - 1
                                        item = currentTableQuery.childs(index)
                                        If _schemaLevel >= item.SecurityLevel Then
                                            childNode.ChildNodes.Add(New TreeNode(item.pluralcaption.GetValue(_currentLanguage), String.Format(CultureInfo.InvariantCulture, "P:{0}:({1}):Index={2}", item.name, pkvalues, item.Key)))
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If

                Next

                If Not isPluralNode AndAlso Not String.IsNullOrEmpty(pkvalues) Then
                    Dim _key As String = String.Format(CultureInfo.InvariantCulture, "S:{0}:({1})", currentTableQuery.name, pkvalues)

                    If Not rootNode.Value.StartsWith(_key) Then
                        rootNode.Value = _key
                    End If
                End If
            Catch ex As Exception
                ErrorMsgASPxLabel.Visible = True

                If Request.QueryString("debug") = "y" Then
                    ErrorMsgASPxLabel.Text = "Failed to execute the query. " & ex.Message & vbCrLf
                Else
                    ErrorMsgASPxLabel.Text = "Failed to execute the query."
                End If
            End Try

            'LogManager.Finish("NodeProcess")
        End Sub

#End Region

#Region "Search Methods"

        Private Function GetParentTableQuery(node As TreeNode) As String
            'LogManager.Begin("GetParentTableQuery")
            Dim result As String = String.Empty
            Dim TableName As String = String.Empty

            If node.Value.StartsWith("S:") Then
                If Not IsNothing(node.Parent) AndAlso Not IsNothing(node.Parent.Parent) Then
                    TableName = node.Parent.Parent.Value
                End If
            Else

                If Not IsNothing(node.Parent) Then
                    TableName = node.Parent.Value
                End If
            End If

            If Not String.IsNullOrEmpty(TableName) Then
                result = TableName.Split(":")(1)
            End If

            'LogManager.Finish("GetParentTableQuery")
            Return result
        End Function

        Private Function GetParameterValueFromUserControl(elementName As String, isQuery As Boolean, formatDateTime As Boolean) As Object
            'LogManager.Begin("GetParameterValueFromUserControl")

            Dim value As Object = Nothing
            Dim controlType As String = String.Empty
            Dim valueControl As Control = pnlUserInput.FindControl(elementName)
            Dim controlData As QueryParameters = FindParameterControlByName(elementName)

            If Not IsNothing(valueControl) Then
                controlType = valueControl.GetType.ToString

                Select Case controlType.Split(".")(controlType.Split(".").Length - 1)

                    Case "ASPxTextBox"
                        value = DirectCast(valueControl, ASPxTextBox).Text

                        With controlData
                            If Not IsNothing(controlData) Then
                                If .Type = EnumQueryParameterType.Client AndAlso Not .IsAllowSearch AndAlso Not .FillZeros Then
                                    value = value.ToString.PadLeft(14, "0")
                                End If

                                If .Type = EnumQueryParameterType.Text AndAlso Not IsNothing(value) Then
                                    If .TrimBehavior Then
                                        value = value.ToString.Trim
                                    End If

                                    If .UpperCaseBehavior Then
                                        value = value.ToString.ToUpper
                                    End If
                                End If
                            End If
                        End With

                    Case "ASPxComboBox"
                        If Not IsNothing(DirectCast(valueControl, ASPxComboBox).SelectedItem) Then
                            value = DirectCast(valueControl, ASPxComboBox).SelectedItem.Value

                        ElseIf Not IsNothing(DirectCast(valueControl, ASPxComboBox).Value) Then
                            value = DirectCast(valueControl, ASPxComboBox).Value
                        Else
                            value = "0"
                        End If

                    Case "ASPxDateEdit"
                        Dim hourFormat As String = String.Empty

                        value = DirectCast(valueControl, ASPxDateEdit).Value

                        If Not String.IsNullOrEmpty(controlData.BeginRangeControl) Then
                            For Each controlItem As QueryParameters In _metadata.Parameters
                                If String.Equals(controlItem.Name, controlData.BeginRangeControl, StringComparison.CurrentCultureIgnoreCase) Then

                                    If controlItem.Type = EnumQueryParameterType.DatePicker Then
                                        hourFormat = "HH:mm:ss"

                                        Dim newValue As Date = value

                                        newValue = newValue.AddHours(23)
                                        newValue = newValue.AddMinutes(59)
                                        newValue = newValue.AddSeconds(59)

                                        value = newValue
                                    End If

                                    Exit For
                                End If
                            Next
                        End If

                        If formatDateTime Then
                            If isQuery Then
                                If String.IsNullOrEmpty(hourFormat) Then
                                    value = String.Format(CultureInfo.InvariantCulture, "TO_DATE('{0}', '{1}')", System.Convert.ToDateTime(value).ToString(_dateformat), _dateformat)
                                Else
                                    value = String.Format(CultureInfo.InvariantCulture, "TO_DATE('{0}', '{1} HH24:MI:SS')",
                                                          System.Convert.ToDateTime(value).ToString(String.Format(CultureInfo.InvariantCulture,
                                                                                                                  "{0} {1}", _dateformat, hourFormat)), _dateformat)
                                End If
                            End If

                        Else
                            If isQuery Then
                                If String.IsNullOrEmpty(hourFormat) Then
                                    value = System.Convert.ToDateTime(value).ToString(_dateformat)
                                Else
                                    value = System.Convert.ToDateTime(value).ToString(String.Format(CultureInfo.InvariantCulture,
                                                                                                    "{0} {1}", _dateformat, hourFormat))
                                End If
                            End If
                        End If

                    Case "ASPxDropDownEdit"
                        Dim ListBoxItems As ASPxListBox = CType(DirectCast(valueControl, ASPxDropDownEdit).FindControl("lbControl"), ASPxListBox)

                        For Each item As ListEditItem In ListBoxItems.SelectedItems
                            If value.Length > 0 Then value += ","
                            value += item.Value.ToString.Trim
                        Next

                    Case "ASPxCheckBox"
                        If String.Equals(elementName, "_AllowHistoryInfo", StringComparison.CurrentCultureIgnoreCase) Then
                            If DirectCast(valueControl, ASPxCheckBox).Value Then
                                value = 1
                            Else
                                value = 0
                            End If

                        Else
                            value = DirectCast(valueControl, ASPxCheckBox).Value
                        End If

                    Case Else
                        Dim queryUserControl As GIT.EDW.Query.Model.Interfaces.IQueryUserControl = DirectCast(valueControl, GIT.EDW.Query.Model.Interfaces.IQueryUserControl)

                        If Not IsNothing(queryUserControl) Then
                            value = queryUserControl.Value
                        End If
                End Select
            Else
                If String.Equals("RECORDEFFECTIVEDATE", elementName, StringComparison.CurrentCultureIgnoreCase) Then
                    value = String.Format(CultureInfo.InvariantCulture, "{0}", System.Convert.ToDateTime(Today).ToString(_dateformat))
                End If
            End If

            Return value
        End Function

        Private Function GetParameterValueFromUserProfile(ElementName As String) As String
            'LogManager.Begin("GetParameterValueFromUserProfile")
            Dim Current As GIT.Core.PageBase = TryCast(HttpContext.Current.Handler, GIT.Core.PageBase)
            Dim value As String = String.Empty

            Select Case ElementName.ToUpper
                Case "#PRODUCERID#"
                    value = Current.UserInfo.User.ProducerID

                Case "#CLIENTID#"
                    value = Current.UserInfo.User.ClientID

                Case "#USERID#"
                    value = Current.UserInfo.User.UserID

                Case "#LANGUAGEID#"
                    value = _currentLanguage

                Case "#FASI.USERID#"
                    value = Current.UserInfo.UserId

                Case Else
            End Select

            'LogManager.Finish("GetParameterValueFromUserProfile")
            Return value
        End Function

        Private Function GetParameterValueFromKeyNode(Name As String, NodeValue As String) As String
            ''LogManager.Begin("GetParameterValueFromKeyNode")

            Dim Value As String = String.Empty
            Dim beginIndex As Integer = NodeValue.IndexOf(Name)

            If beginIndex > -1 Then
                Dim endIndex As Integer = NodeValue.IndexOf(",", beginIndex)
                If endIndex > beginIndex Then
                    Value = NodeValue.Substring(beginIndex, endIndex - beginIndex).Replace(Name, "").Replace("=", "")

                    If Name.EndsWith(":D}") Then
                        Value = String.Format(CultureInfo.InvariantCulture, "{0}", Date.Parse(Value).ToString(_dateformat))
                    End If
                End If
            End If

            ''LogManager.Finish("GetParameterValueFromKeyNode")
            Return Value
        End Function

        Private Function ChildHaveRecords(child As tablequery, parentKey As String, currentKey As String) As Boolean
            'LogManager.Begin("ChildHaveRecords")

            Dim result As Boolean = False
            Dim sqlStatement As String = String.Empty

            sqlStatement = CreateQuery(child, parentKey, currentKey, True)

            If Not String.IsNullOrEmpty(sqlStatement) Then
                result = (ExecuteQueryScalar(child, sqlStatement) > 0)
            End If

            'LogManager.Finish("ChildHaveRecords")
            Return result
        End Function

        Public Function FindControlCheckBox(Root As Control, Id As String) As ASPxCheckBox
            'LogManager.Begin("FindControlCheckBox")
            Dim result As Control = Nothing
            Dim FoundCtl As Control = Nothing

            If Not IsNothing(Root.ID) AndAlso Root.ID.ToLower = Id.ToLower Then
                result = Root
            Else
                For Each Ctl As Control In Root.Controls
                    FoundCtl = FindControlCheckBox(Ctl, Id)
                    If Not IsNothing(FoundCtl) Then
                        result = FoundCtl
                    End If
                Next
            End If
            'LogManager.Finish("FindControlCheckBox")

            Return result
        End Function

        ''' <summary>
        ''' Find the real repository name of the entity - Devuelve el nombre del fuente de la entidad
        ''' </summary>
        ''' <param name="entity">Çurrent table query - Actual tabla</param>
        ''' <returns>Name of the table's source - Nombre del fuente de la tabla</returns>
        Private Function SetCurrentSourceName(entity As tablequery) As String
            Dim result As String = String.Empty

            If String.IsNullOrEmpty(entity.Source) Then
                result = _metadata.Repository
            Else
                result = entity.Source
            End If

            Return result
        End Function

        Private Sub SearchDateFormatBySource(source As String)
            If IsNothing(_dateFormatList) Then
                _dateFormatList = New Dictionary(Of String, String)
            End If

            If _dateFormatList.ContainsKey(source) Then
                _dateformat = _dateFormatList.Item(source)
            Else
                With New InMotionGIT.Common.Proxy.DataManagerFactory
                    _dateformat = .GetSettingValue(source, "DateFormat")
                End With

                _dateFormatList.Add(source, _dateformat)
            End If
        End Sub

        Private Sub FindRepositoryMetadata()
            If IsNothing(_metadata) Then
                _InternalModelId = ModelId
                _InternalRelease = Release

                If _InternalModelId.IsEmpty Then
                    _InternalModelId = Request.QueryString("ModelId")
                End If

                If _InternalRelease.IsEmpty Then
                    _InternalRelease = Request.QueryString("Release")
                End If

                If _InternalModelId.IsNotEmpty Then
                    _metadata = GIT.EDW.Query.Model.Widget.LoadRepository(_InternalModelId,
                                                                          _InternalRelease,
                                                                         (Request.QueryString("debug") = "y"))
                ElseIf Request.QueryString("Name").IsNotEmpty Then
                    _metadata = GIT.EDW.Query.Model.Widget.LoadRepositoryByName(Request.QueryString("Name"),
                                                                                (Request.QueryString("debug") = "y"))
                End If
            End If
        End Sub

        Private Function FindControlRecursive(Root As Control, controlName As String) As Control
            Dim foundControl As Control = Nothing

            For Each Ctl As Control In Root.Controls
                If Not IsNothing(Ctl.ID) AndAlso String.Equals(Ctl.ID.ToLower, controlName.ToLower, StringComparison.CurrentCultureIgnoreCase) Then
                    foundControl = Ctl
                Else

                    If IsNothing(foundControl) AndAlso Not IsNothing(Ctl.Controls) Then
                        foundControl = FindControlRecursive(Ctl, controlName)
                    End If
                End If

                If Not IsNothing(foundControl) Then
                    Exit For
                End If
            Next

            Return foundControl
        End Function

#End Region

#Region "Others Methods"

        Private Sub LoadRepository()
            'LogManager.Begin("LoadRepository")

            Dim cacheKeyName As String = String.Format(CultureInfo.InvariantCulture, "Query_{0}_{1}", _InternalModelId, _InternalRelease)

            _metadata = Caching.GetItem(cacheKeyName)

            If Request.QueryString("mode") = "refresh" OrElse Request.QueryString("m") = "r" OrElse
               Request.QueryString("cache") = "none" OrElse Request.QueryString("debug") = "y" Then

                Caching.Remove(cacheKeyName)
                _metadata = Nothing
            End If

            If _metadata.IsEmpty AndAlso _InternalModelId.IsNotEmpty Then

                _metadata = LoadMetadataRepository(_InternalModelId)

                If Not IsNothing(_metadata) Then
                    If Request.QueryString("cache") <> "none" Then
                        Caching.SetItem(cacheKeyName, _metadata)

                        If Request.QueryString("mode") <> "refresh" AndAlso Request.QueryString("m") <> "r" AndAlso Request.QueryString("debug") <> "y" Then
                            If TreeViewTables.Nodes.Count > 0 Then
                                TreeViewTables.Nodes.Clear()
                            End If
                        End If
                    End If
                End If
            End If

            If Not IsNothing(_metadata) Then
                _dateformat = ConfigurationManager.AppSettings(String.Format(CultureInfo.InvariantCulture, "Linked.{0}.DateFormat", _metadata.Repository))

                If Not IsNothing(ConfigurationManager.AppSettings(String.Format(CultureInfo.InvariantCulture, "Linked.{0}.NotesFormat", _metadata.Repository))) Then
                    notesformat = ConfigurationManager.AppSettings(String.Format(CultureInfo.InvariantCulture, "Linked.{0}.NotesFormat", _metadata.Repository)).ToUpper(CultureInfo.CurrentCulture)
                End If

                _currentLanguage = GetCurrentLanguage()

                TreeViewTables.ToolTip = String.Format(CultureInfo.InvariantCulture, "Release {0}", _InternalRelease)
            Else
                _InternalModelId = String.Empty
                GridViewQueries.ToolTip = String.Empty

                ErrorMsgASPxLabel.Visible = True
                ErrorMsgASPxLabel.Text = "The xml file not exists, please execute the deploy of the query and try again."
            End If

            'LogManager.Finish("LoadRepository")
        End Sub

        Public Shared Function GetCurrentLanguage() As Integer
            'LogManager.Begin("Get_currentLanguage")
            Dim result As Integer
            If Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower.StartsWith("es") Then
                result = EnumLanguage.Spanish
            Else
                result = EnumLanguage.English
            End If
            'LogManager.Finish("Get_currentLanguage")
            Return result
        End Function

        Private Function DecryptString(Stream As String) As String
            Dim result As String

            Using service As EncryptionService.EncryptionClient = New EncryptionService.EncryptionClient()
                With service
                    result = .Decryption(Stream)
                    .Close()
                End With
            End Using

            Return result
        End Function

        Private Function ApplyConfidentialityAgreement(Stream As String, DisplayRule As String, NumbersOfPositions As String) As String
            'LogManager.Begin("ApplyConfidentialityAgreement")

            Dim VisibleString As String = String.Empty
            Dim MaskedString As String = String.Empty
            Dim MaskedStringLenght As Integer = Stream.Trim.Length - NumbersOfPositions

            Select Case DisplayRule
                Case "1" 'TheLast
                    If MaskedStringLenght < 0 Then
                        VisibleString = Stream.Trim
                        MaskedString = String.Empty
                    Else
                        VisibleString = Stream.Trim.Substring(MaskedStringLenght)
                        MaskedString = Stream.Trim.Substring(0, MaskedStringLenght)
                    End If

                Case "2" 'NoDisplay
                    MaskedString = Stream.Trim

                Case Else '0 TheFirst
                    If MaskedStringLenght < 0 Then
                        VisibleString = Stream.Trim
                        MaskedString = String.Empty
                    Else
                        VisibleString = Stream.Trim.Substring(0, NumbersOfPositions)
                        MaskedString = Stream.Trim.Substring(NumbersOfPositions)
                    End If

            End Select

            For Each Character As Char In MaskedString.Trim
                MaskedString = MaskedString.Trim.Replace(Character, "*")
            Next

            If DisplayRule = "1" Then 'TheLast
                Stream = MaskedString & VisibleString
            Else 'TheFirst & NoDisplay
                Stream = VisibleString & MaskedString
            End If

            'LogManager.Finish("ApplyConfidentialityAgreement")
            Return Stream
        End Function

        Private Function GenerateNavigateUrl(currentTable As tablequery, actionData As Actions.action, nIndexSelected As Integer, actionIndex As Integer) As String
            Dim result As String = String.Empty
            Dim parameters As String = String.Empty
            Dim lastrUrl As String = String.Empty
            Dim target As String = String.Empty
            Dim resizable As String = String.Empty
            Dim scrollbars As String = String.Empty
            Dim name As String
            Dim value As Object = Nothing

            If Not IsNothing(currentTable) AndAlso currentTable.MultiSelect Then
                parameters = String.Format(CultureInfo.InvariantCulture, "QueryMultiSelected={0}", actionIndex)
            End If

            For Each parameterData As Query.Model.parameter In actionData.Parameters
                If parameters.Length > 0 Then parameters += "&"

                If String.Equals(parameterData.Type, "parameter", StringComparison.CurrentCultureIgnoreCase) Then
                    If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                        parameters += "lnk"
                    End If

                    parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name,
                                                GetParameterValueFromUserControl(parameterData.ParameterQuery, True, False))

                ElseIf String.Equals(parameterData.Type, "element", StringComparison.CurrentCultureIgnoreCase) Then
                    ' Si el parametro que se definió no está como una columna de la consulta igualmente se envia pero sin valor

                    Try
                        name = parameterData.Element.Replace(".", "").ToUpper
                        name = parameterData.Element.Replace("@", "").ToUpper

                        value = GridViewQueries.GetRowValues(nIndexSelected, name)

                        If Not IsNothing(value) AndAlso IsDate(value) Then
                            value = DirectCast(value, Date).ToShortDateString
                        End If

                        If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                            parameters += "lnk"
                        End If

                        parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name, value)
                    Catch ex As Exception
                        Try
                            name = parameterData.Element.ToUpper

                            If name.IndexOf(".", StringComparison.CurrentCultureIgnoreCase) > -1 Then
                                name = name.Split(".")(1)
                            End If

                            If name.IndexOf("@", StringComparison.CurrentCultureIgnoreCase) > -1 Then
                                name = name.Split("@")(1)
                            End If

                            value = GridViewQueries.GetRowValues(nIndexSelected, name)

                            If Not IsNothing(value) AndAlso IsDate(value) Then
                                value = DirectCast(value, Date).ToShortDateString
                            End If

                            If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                                parameters += "lnk"
                            End If

                            parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name, value)
                        Catch ex2 As Exception
                            If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                                parameters += "lnk"
                            End If

                            parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name, String.Empty)
                        End Try
                    End Try
                Else
                    value = parameterData.Value

                    If Not IsNothing(value) AndAlso value.ToString.ToLower = "today" Then
                        value = Today.ToShortDateString
                    End If

                    If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                        parameters += "lnk"
                    End If

                    parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name, parameterData.Value)
                End If
            Next

            With actionData
                If .Target = enumTargetType.newWindow Then
                    target = "_blank"
                Else
                    target = "_self"
                End If

                resizable = IIf(.Resizable, "yes", "no")
                scrollbars = IIf(.Scrollbars, "yes", "no")

                If .Transaction.Length > 0 Then

                    If .TransactionSource = Query.Model.EnumTransactionSource.VisualTIME Then

                        Using service As MenuService.MenuClient = New MenuService.MenuClient()
                            With service
                                lastrUrl = .MakeURL(actionData.Transaction, UserInfo.CompanyId)
                                .Close()
                            End With
                        End Using

                        If parameters.Length > 0 Then
                            parameters = String.Format(CultureInfo.InvariantCulture, "&{0}", parameters)
                        End If

                        result = String.Format(CultureInfo.InvariantCulture, "javascript:insGoToQuery(""{0}&LinkFront=1{1}"");", lastrUrl, parameters)

                    ElseIf .TransactionSource = Query.Model.EnumTransactionSource.DesignerForm Then
                        result = String.Format(CultureInfo.InvariantCulture, "javascript:windowOpenUrl('/generated/form/{0}.aspx?{1}','{2}',{3},{4},'{5}','{6}');",
                                            IIf(.ShowAsPopup, String.Format(CultureInfo.InvariantCulture, "{0}Popup", .Transaction), .Transaction),
                                            parameters, target, .Height, .Width, resizable, scrollbars)

                    ElseIf .TransactionSource = Query.Model.EnumTransactionSource.DesignerQuery Then
                        If parameters.Length > 0 Then
                            parameters = String.Format(CultureInfo.InvariantCulture, "&{0}", parameters)
                        End If

                        If Not .ShowAsPopup Then
                            result = String.Format(CultureInfo.InvariantCulture,
                                                   "javascript:windowOpenUrl('/dropthings/GeneralQuery/Page.aspx?ModelId={0}&Release={1}&culture={2}{3}','{4}',{5},{6},'{7}','{8}');",
                                                   .ModelId, .Release, InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(_currentLanguage), parameters, target, .Height, .Width, resizable, scrollbars)
                        Else
                            result = String.Format(CultureInfo.InvariantCulture,
                                                   "javascript:windowOpenUrl('/dropthings/GeneralQuery/Popup.aspx?ModelId={0}&Release={1}&culture={2}{3}','{4}',{5},{6},'{7}','{8}');",
                                                   .ModelId, .Release, InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(_currentLanguage), parameters, target, .Height, .Width, resizable, scrollbars)
                        End If

                    ElseIf .TransactionSource = Query.Model.EnumTransactionSource.WorkflowDesigner Then
                        If parameters.Length > 0 Then parameters += "&"

                        parameters += String.Format(CultureInfo.InvariantCulture,
                                                    "executedMessage={0}&queryName={1}&queryModelId={2}",
                                                    .WorkflowMessage.GetUpValue(_currentLanguage, 1), _metadata.name, _metadata.ModelId)

                        result = String.Format(CultureInfo.InvariantCulture, "javascript:windowOpenUrl('/dropthings/WorkflowExecute.ashx?{0}');", parameters)
                    End If

                ElseIf .Url.Length > 0 Then
                    Dim newUrl As String = .Url

                    If newUrl.Contains("#") Then
                        Dim vector = newUrl.Split("#")
                        Dim tempSetting As String = vector(1)
                        Dim tempOnlySettting As String = tempSetting.Replace("setting.", "")
                        Dim formatValue As String = ConfigurationManager.AppSettings(tempOnlySettting)

                        newUrl = newUrl.Replace(String.Format(CultureInfo.InvariantCulture, "#{0}#", tempSetting), formatValue)
                    End If

                    If newUrl.Contains("[") Then
                        Dim regexData As New Regex(Regex.Escape("[") + "(.*?)" + Regex.Escape("]"))
                        Dim matches As MatchCollection = regexData.Matches(newUrl)

                        If Not IsNothing(matches) AndAlso matches.Count > 0 Then
                            Dim parameterName As String = String.Empty
                            Dim parameterValue As String = String.Empty
                            Dim beginIndex As Integer
                            Dim endIndex As Integer

                            For Each match As Match In matches
                                parameterName = match.Value
                                beginIndex = parameterName.IndexOf("[")

                                If beginIndex > -1 Then
                                    endIndex = parameterName.IndexOf("]", beginIndex)

                                    parameterName = parameterName.Substring(beginIndex + 1, (endIndex - beginIndex) - 1)
                                End If

                                parameterValue = GridViewQueries.GetRowValues(nIndexSelected, parameterName)

                                newUrl = newUrl.Replace(match.Value, parameterValue)
                            Next
                        End If
                    End If

                    If Not newUrl.StartsWith("http://") Then
                        newUrl = String.Format(CultureInfo.InvariantCulture, "http://{0}", newUrl)
                    End If

                    If Not newUrl.Contains("?") Then
                        newUrl = String.Format(CultureInfo.InvariantCulture, "{0}?", newUrl)
                    End If

                    result = String.Format(CultureInfo.InvariantCulture, "javascript:windowOpenUrl('{0}{1}', '{2}',{3},{4},'{5}','{6}');",
                                           newUrl, parameters, target, .Height, .Width, resizable, scrollbars)
                End If
            End With

            Return result
        End Function

        Private Function SetActionParameters(actionData As Actions.action, selectedRow As DataRow) As String
            Dim parameters As String = String.Empty
            Dim parameterName As String
            Dim value As Object = Nothing

            For Each parameterData As parameter In actionData.Parameters
                If parameters.Length > 0 Then parameters += "&"

                If String.Equals(parameterData.Type, "parameter", StringComparison.CurrentCultureIgnoreCase) Then
                    If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                        parameters += "lnk"
                    End If

                    parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name,
                                                GetParameterValueFromUserControl(parameterData.ParameterQuery, True, False))

                ElseIf String.Equals(parameterData.Type, "element", StringComparison.CurrentCultureIgnoreCase) Then
                    Try
                        parameterName = parameterData.Element.Replace(".", "").ToUpper
                        parameterName = parameterData.Element.Replace("@", "").ToUpper

                        value = selectedRow(parameterName)

                        If Not IsNothing(value) AndAlso IsDate(value) Then
                            value = DirectCast(value, Date).ToShortDateString
                        End If

                        If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                            parameters += "lnk"
                        End If

                        parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name, value)
                    Catch ex As Exception
                        Try
                            parameterName = parameterData.Element.ToUpper

                            If parameterName.IndexOf(".", StringComparison.CurrentCultureIgnoreCase) > -1 Then
                                parameterName = parameterName.Split(".")(1)
                            End If

                            If parameterName.IndexOf("@", StringComparison.CurrentCultureIgnoreCase) > -1 Then
                                parameterName = parameterName.Split("@")(1)
                            End If

                            value = selectedRow(parameterName)

                            If Not IsNothing(value) AndAlso IsDate(value) Then
                                value = DirectCast(value, Date).ToShortDateString
                            End If

                            If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                                parameters += "lnk"
                            End If

                            parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name, value)
                        Catch ex2 As Exception
                            If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                                parameters += "lnk"
                            End If

                            parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name, String.Empty)
                        End Try
                    End Try
                Else
                    value = parameterData.Value

                    If Not IsNothing(value) AndAlso value.ToString.ToLower = "today" Then
                        value = Today.ToShortDateString
                    End If

                    If actionData.TransactionSource = Query.Model.EnumTransactionSource.VisualTIME AndAlso String.IsNullOrEmpty(actionData.Url) Then
                        parameters += "lnk"
                    End If

                    parameters += String.Format(CultureInfo.InvariantCulture, "{0}={1}", parameterData.Name, parameterData.Value)
                End If
            Next

            Return parameters
        End Function

        Private Function GetColumnsValues(columnList As String, dataResult As DataTable, row As DataRow, addRecordIdValue As Boolean) As String
            Dim result As String = String.Empty

            If Not String.IsNullOrEmpty(columnList) Then
                Dim columnName As String = String.Empty
                Dim rowValue As String = String.Empty
                Dim dDTime As Date

                For Each column As String In columnList.Split(",")
                    columnName = column.Replace(":D", "")

                    If columnName.Contains(".") Then
                        columnName = columnName.Split(".")(1)
                    End If

                    If dataResult.Columns.Contains(columnName) Then
                        If Not row.Item(columnName) Is DBNull.Value Then
                            rowValue = row.Item(columnName)

                            If column.EndsWith(":D") Then
                                dDTime = Date.Parse(rowValue)
                                rowValue = dDTime.ToString("yyyy-MM-ddTHH:mm:ss")
                            End If

                            result += String.Format(CultureInfo.InvariantCulture, "{{{0}}}={1},", column, rowValue)
                        Else
                            result += String.Format(CultureInfo.InvariantCulture, "{{{0}}}=NULL,", column)
                        End If
                    End If
                Next
            End If

            If addRecordIdValue Then
                If dataResult.Columns.Contains("RECORDID") AndAlso Not IsDBNull(row.Item("RECORDID")) Then
                    result += String.Format(CultureInfo.InvariantCulture, "RECORDID={0},", row.Item("RECORDID"))
                End If
            End If

            Return result
        End Function

        Private Function GetSelectedTableNode(visibleIndex As Integer) As tablequery
            Dim result As tablequery = Nothing
            Dim haveParent As Boolean = False
            Dim parentValue As String = String.Empty

            With TreeViewTables
                If Not IsNothing(.SelectedNode) Then
                    If Not IsNothing(.SelectedNode.Parent) Then
                        haveParent = True
                        parentValue = .SelectedNode.Parent.Value
                    End If

                    result = GetTableQuery(_metadata, .SelectedNode.Value, haveParent, parentValue)
                End If
            End With

            If IsNothing(result) Then
                result = _metadata.root
            End If

            Return result
        End Function

        Private Function GetCodeFieldDbType(kind As String, value As Object) As String
            Dim result As String = String.Empty

            Select Case kind
                Case "String", "Char", "DateTime", "Date"
                    If DBNull.Value.Equals(value) Then
                        result = String.Format(CultureInfo.InvariantCulture, "'{0}'", String.Empty)
                    Else
                        result = String.Format(CultureInfo.InvariantCulture, "'{0}'", value)
                    End If

                Case Else '"Integer", "Int16", "Int32", "Int64", "Numeric", "Decimal", "Double", "Boolean"
                    If DBNull.Value.Equals(value) Then
                        result = String.Format(CultureInfo.InvariantCulture, "{0}", 0)
                    Else
                        result = String.Format(CultureInfo.InvariantCulture, "{0}", value)
                    End If
            End Select

            Return result
        End Function

#End Region

#Region "Template User Input Multiple Selection"

        Partial Class UserInputMultipleSelection
            Implements ITemplate

            Dim ValueField As String
            Dim TextField As String
            Dim Table As String
            Dim Repository As String

            Public Sub New(ConditionColumn As String, Repository As String)
                ValueField = ConditionColumn.Split(",")(4)
                TextField = ConditionColumn.Split(",")(5)
                Table = ConditionColumn.Split(",")(6)
                Repository = Repository
            End Sub

            Public Sub InstantiateIn(container As System.Web.UI.Control) Implements ITemplate.InstantiateIn

                Dim SqlDataSourceUserInput As New SqlDataSource
                ''TODO:Se commento para evitar el error, ademas se debe revisar con detalle
                Dim connectionSetting As ConnectionStringSettings = Nothing  '' GetConnectionString(Repository)

                If Not IsNothing(connectionSetting) Then
                    With SqlDataSourceUserInput
                        .ConnectionString = connectionSetting.ConnectionString
                        .ProviderName = connectionSetting.ProviderName
                        .SelectCommand = String.Format(CultureInfo.InvariantCulture, "SELECT {0}, {1} FROM {2}{3} ORDER BY {4}", ValueField, TextField, ConfigurationManager.AppSettings("BackOfficeConnectionString.Owner"), Table, TextField)
                    End With
                End If

                Dim lbControl As New ASPxListBox
                container.Controls.Add(lbControl)
                lbControl.ID = "lbControl"
                lbControl.ClientInstanceName = "checkListBox"
                lbControl.ClientSideEvents.SelectedIndexChanged = "function(s, e) {OnListBoxSelectionChanged();}"
                lbControl.ValueField = ValueField
                lbControl.TextField = TextField
                lbControl.SelectionMode = ListEditSelectionMode.CheckColumn
                lbControl.DataSource = SqlDataSourceUserInput
                lbControl.DataBind()

            End Sub

        End Class

#End Region

#Region "Execution Stored Procedure Methods"

        Private Function ExecuteStoredProcedure(currentTable As tablequery, parentKey As String, currentKey As String) As DataTable
            'LogManager.Begin("ExecuteStoredProcedure", currentTable.DetailName)
            Dim rowCount As Long = 0
            Dim result As New DataTable
            Dim _isDetail As Boolean = False
            Dim _storedProcedureName As String
            Dim _parameterList As List(Of GIT.EDW.Query.Model.DataType.Parameter) = Nothing
            Dim currentLocalKey As String = currentKey & parentKey

            If currentKey.StartsWith("S:") AndAlso (Not IsNothing(currentTable.Parent) OrElse currentTable.DetailName.IsNotEmpty) Then
                _isDetail = True
                _storedProcedureName = currentTable.DetailName

                If String.IsNullOrEmpty(_storedProcedureName) Then
                    _storedProcedureName = currentTable.name
                    _isDetail = False
                End If
            Else
                _isDetail = False
                _storedProcedureName = currentTable.name
            End If

            If Not IsNothing(currentTable.Parameters) Then
                _parameterList = (From p In currentTable.Parameters.Clone Where p.IsDetail = _isDetail Select p).ToList
            End If

            ViewState("Query") = _storedProcedureName

            Dim parametersList As New Dictionary(Of Query.Model.DataType.Parameter, Object)

            SearchDateFormatBySource(currentTable.Source)

            Try
                Dim value As Object = Nothing

                With New InMotionGIT.Common.Proxy.DataManagerFactory(True, _storedProcedureName, String.Format("Linked.{0}", SetCurrentSourceName(currentTable)))

                    For Each parameterItem As Query.Model.DataType.Parameter In _parameterList
                        If String.Equals(parameterItem.Value, "Today", StringComparison.CurrentCultureIgnoreCase) Then
                            value = Today.ToString
                        Else
                            value = parameterItem.Value
                        End If

                        Select Case parameterItem.ValueType
                            Case Query.Model.Enumerations.enumParameterType.UserInput  '"["    'UserInput
                                value = GetParameterValueFromUserControl(parameterItem.Value.Replace("@", ""), False, False)

                            Case Query.Model.Enumerations.enumParameterType.UserProfile '"#"    'Profile
                                value = GetParameterValueFromUserProfile(String.Format(CultureInfo.InvariantCulture, "{0}{1}{2}",
                                                                                                     "#", parameterItem.Value, "#"))

                            Case Query.Model.Enumerations.enumParameterType.ParentElement '"{"    'TreeNode.
                                value = GetParameterValueFromKeyNode(String.Format(CultureInfo.InvariantCulture, "{0}{1}{2}",
                                                                                                 "{", parameterItem.Value.Replace("@", ""), "}"), currentLocalKey)

                            Case Query.Model.Enumerations.enumParameterType.Constant
                                If (String.Equals(parameterItem.Type.ToLower, "date") OrElse
                                    String.Equals(parameterItem.Type.ToLower, "datetime")) AndAlso
                                    Not String.Equals(parameterItem.Value, "Null", StringComparison.CurrentCultureIgnoreCase) Then

                                    value = System.Convert.ToDateTime(value).ToString(_dateformat).ToString
                                End If

                        End Select

                        .AddParameter(parameterItem.Name, ParameterTypeConvert(parameterItem.Type), parameterItem.Length, False, value)
                        parametersList.Add(parameterItem, value)
                    Next

                    ViewState("StoredProcedureParameters") = parametersList
                    .MaxNumberOfRecord = currentTable.MaxNumberOfRecords
                    result = .ProcedureExecuteToTable(True)
                    rowCount = .QueryCountResult
                End With

                If result.IsNotEmpty AndAlso result.Rows.IsNotEmpty AndAlso result.Rows.Count <> rowCount Then
                    ErrorMsgASPxLabel.Visible = True
                    ErrorMsgASPxLabel.Text = String.Format(CultureInfo.InvariantCulture, "Solo se pueden mostrar los primeros {0} registros.", currentTable.MaxNumberOfRecords)
                    ErrorMsgASPxLabel.ForeColor = Color.Green
                Else
                    ErrorMsgASPxLabel.Visible = False
                    ErrorMsgASPxLabel.ForeColor = Color.Red
                End If

            Catch ex As Exception
                ErrorMsgASPxLabel.Visible = True

                If Request.QueryString("debug") = "y" Then
                    ErrorMsgASPxLabel.Text = ex.Message & vbCrLf
                Else
                    ErrorMsgASPxLabel.Text = "Failed to execute the query. Refresh the page and try again."
                End If
            End Try

            '+ Si el campo está encriptado o se le debe aplicar el acuerdo de confidencialidad
            If currentTable.HaveEncryptedColumns Then
                Dim columnName As String = String.Empty

                For Each column As columnquery In currentTable.columns

                    If column.Encrypted Or column.Confidentiality Then
                        Dim newColumn As New DataColumn
                        columnName = String.Format(CultureInfo.InvariantCulture, "{0}Confidentiality,", column.RealName.Split("@")(1))

                        If columnName.Length >= 26 Then
                            columnName = columnName.Substring(0, 25)
                        End If

                        newColumn.DataType = GetType(String)
                        newColumn.ColumnName = columnName
                        result.Columns.Add(newColumn)

                        For Each row As DataRow In result.Rows

                            If column.Encrypted AndAlso Not String.Equals(currentTable.Source, "LatCombined", StringComparison.CurrentCultureIgnoreCase) Then
                                row(columnName) = DecryptString(row(column.RealName.Split("@")(1)).ToString)
                            End If

                            '+ Si el nivel del esquema de seguridad asociado al usuario es menor que
                            '+ el minimo requerido para consultar el(campo) se aplica el Acuerdo de Confidencialidad
                            If Not IsNothing(column.ConfidentialityAgreement) AndAlso
                               _schemaLevel < column.ConfidentialityAgreement.SecurityLevel Then

                                If column.Confidentiality Then

                                    row(columnName) = ApplyConfidentialityAgreement(row(columnName),
                                                                                    column.ConfidentialityAgreement.DisplayRule,
                                                                                    column.ConfidentialityAgreement.NumbersOfPositions)

                                End If
                            End If
                        Next

                    End If
                Next
            End If

            If Not IsNothing(result) AndAlso Not IsNothing(result.Rows) AndAlso result.Rows.Count > 0 Then
                Dim RecordIdColumn As New DataColumn

                With RecordIdColumn
                    .DataType = GetType(String)
                    .ColumnName = "RECORDID"
                End With

                result.Columns.Add(RecordIdColumn)

                Dim index As Integer = 1

                For Each row As DataRow In result.Rows
                    row("RECORDID") = index
                    index += 1
                Next

                If currentKey.StartsWith("S:") AndAlso currentTable.DetailName.IsEmpty Then
                    Dim recordID As Integer = GetParameterValueFromKeyNode("RECORDID", currentLocalKey)

                    Dim rows() As DataRow = result.Select(String.Format(CultureInfo.InvariantCulture, "RECORDID = '{0}'", recordID))

                    If rows.Count > 0 Then
                        Dim clonedResult As DataTable = result.Clone

                        With clonedResult
                            .Rows.Clear()
                            .ImportRow(rows(0))
                        End With

                        Return clonedResult
                    End If
                End If
            End If

            'LogManager.Finish("ExecuteStoredProcedure")

            Return result
        End Function

#End Region

#Region "Create Input Parameters Methods"

        Private Sub CreateUserInputParameterControls()
            Dim lblCaption As ASPxLabel
            Dim measure As String = String.Empty
            Dim columsWidth As String = String.Empty
            Dim defaultValue As String = String.Empty
            Dim countControl As Integer = 0
            Dim totalCountControl As Integer = 0
            Dim fullcontrolsize As Integer = 0
            Dim currentWidth As Integer = 0
            Dim visibleControlCount As Integer = 0
            Dim skipColumns As Integer = 0
            Dim sizeLabel As Decimal = 0.25
            Dim sizeControl As Decimal = 0.75
            Dim control As Object = Nothing
            Dim isButton As Boolean = False

            If _metadata.NumberOfColumns = 0 Then
                _metadata.NumberOfColumns = 3
            End If

            For Each controlItem As QueryParameters In _metadata.Parameters
                If controlItem.Type = EnumQueryParameterType.Button Then
                    isButton = True
                    Exit For
                End If
            Next

            If Not isButton Then
                Dim newParameterButton As New QueryParameters

                With newParameterButton
                    .Name = "btnOk"
                    .Caption.SetValue(EnumLanguage.English, "OK")
                    .Caption.SetValue(EnumLanguage.Spanish, "OK")

                    .ToolTip.SetValue(EnumLanguage.English, "Accept query")
                    .ToolTip.SetValue(EnumLanguage.Spanish, "Aceptar consulta")

                    .Enabled = True
                    .Type = EnumQueryParameterType.Button
                End With

                _metadata.Parameters.Add(newParameterButton)
            End If

            Dim columns As Integer = 100 / _metadata.NumberOfColumns

            pnlUserInput.Controls.Clear()

            _functionNameList = String.Empty
            _functionValidationsList = String.Format(CultureInfo.InvariantCulture, "var errors = false; {0}", vbCrLf)
            _functionValidateRangesList = String.Format(CultureInfo.InvariantCulture, "var errorsMessage = ''; {0}", vbCrLf)
            _requiredFields = False
            _rangesFields = False
            isButton = False

            For Each controlItem As QueryParameters In _metadata.Parameters

                visibleControlCount += 1
            Next

            For index As Integer = 1 To _metadata.NumberOfColumns
                If index > 1 Then
                    columsWidth &= ","
                End If

                If index = _metadata.NumberOfColumns Then
                    columsWidth &= String.Format(CultureInfo.InvariantCulture, "{0}%", 100 - (columns * (index - 1)))
                Else
                    columsWidth &= String.Format(CultureInfo.InvariantCulture, "{0}%", columns)
                End If
            Next

            With pnlUserInput.Controls
                .Add(New LiteralControl("<table style=""border: 0; width: 100%; margin-top: 7px;margin-bottom: 7px;"">"))
                .Add(New LiteralControl("<tr style=""vertical-align: top;"">"))

                For Each currentControl As QueryParameters In _metadata.Parameters
                    If columsWidth.Split(",")(countControl).EndsWith("%") Then
                        measure = "%"
                    Else
                        measure = "px"
                    End If

                    fullcontrolsize = Convert.ToInt32(columsWidth.Split(",")(countControl).Replace(measure, ""))
                    currentWidth += fullcontrolsize

                    If currentControl.SkipRows > 0 Then
                        If countControl < _metadata.NumberOfColumns Then
                            .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture, "    <td colspan=""{0}"">&nbsp;</td>", (_metadata.NumberOfColumns - countControl) * 2)))
                        End If

                        .Add(New LiteralControl("</tr>"))

                        For index As Integer = 1 To currentControl.SkipRows
                            .Add(New LiteralControl("<tr style=""vertical-align: top;"">"))
                            .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture, "    <td colspan=""{0}"">&nbsp;</td>", _metadata.NumberOfColumns * 2)))
                            .Add(New LiteralControl("</tr>"))
                        Next

                        .Add(New LiteralControl("<tr style=""vertical-align: top;"">"))

                        countControl = 0
                        currentWidth = 0
                    End If

                    If currentControl.SkipColumns > 0 Then
                        skipColumns = currentControl.SkipColumns

                        If (countControl + skipColumns) > _metadata.NumberOfColumns Then
                            skipColumns = _metadata.NumberOfColumns - countControl
                        End If

                        .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture, "    <td style=""width: {1}{2};"" colspan=""{0}"">&nbsp;</td>",
                                                              skipColumns * 2, fullcontrolsize * skipColumns, measure)))
                        countControl += skipColumns
                        totalCountControl += skipColumns
                        currentWidth += (fullcontrolsize * skipColumns)

                        If countControl = _metadata.NumberOfColumns OrElse countControl > visibleControlCount Then
                            .Add(New LiteralControl("</tr>"))
                            .Add(New LiteralControl("<tr style=""vertical-align: top;"">"))

                            countControl = 0
                            currentWidth = 0
                        End If
                    End If

                    If currentControl.Type <> EnumQueryParameterType.CheckBox AndAlso currentControl.Type <> EnumQueryParameterType.Button Then
                        .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture,
                                                              "    <td style=""text-align: left; width: {0}{1};"">", (fullcontrolsize * sizeLabel), measure)))
                        lblCaption = New ASPxLabel

                        With lblCaption
                            .Text = currentControl.Caption.GetValue(_currentLanguage)
                            .ID = String.Format(CultureInfo.InvariantCulture, "{0}label", currentControl.Name)
                            .ClientInstanceName = String.Format(CultureInfo.InvariantCulture, "{0}label", currentControl.Name)
                            .ToolTip = currentControl.ToolTip.GetValue(_currentLanguage)
                            .ClientEnabled = currentControl.Enabled

                            If Not String.IsNullOrEmpty(currentControl.EnableControl) AndAlso Not String.IsNullOrEmpty(currentControl.EnableValue) AndAlso Not Page.IsPostBack Then
                                .ClientEnabled = False
                            End If
                        End With

                        .Add(lblCaption)
                        .Add(New LiteralControl("</td>"))
                    End If

                    If currentControl.Type = EnumQueryParameterType.Button Then
                        .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture,
                                                              "    <td style=""text-align: left; width: {0}{1};"" colspan=""2"">", (fullcontrolsize), measure)))
                    Else
                        .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture,
                                                              "    <td style=""text-align: left; width: {0}{1};"">", (fullcontrolsize * sizeControl), measure)))
                    End If

                    defaultValue = String.Empty

                    If Not String.IsNullOrEmpty(Request.QueryString(currentControl.Name)) Then
                        defaultValue = Request.QueryString(currentControl.Name)
                    End If

                    Select Case currentControl.Type
                        Case EnumQueryParameterType.Text
                            control = CreateTextControl(currentControl, defaultValue)

                        Case EnumQueryParameterType.CheckBox
                            control = CreateCheckBoxControl(currentControl, defaultValue)

                        Case EnumQueryParameterType.DatePicker
                            control = CreateDatePickerControl(currentControl, defaultValue)

                        Case EnumQueryParameterType.Numeric
                            control = CreateNumericControl(currentControl, defaultValue)

                        Case EnumQueryParameterType.DropDown
                            control = CreateDropDownControl(currentControl, defaultValue)

                        Case EnumQueryParameterType.Button
                            isButton = True

                        Case Else
                            If currentControl.Type = EnumQueryParameterType.Client AndAlso Not currentControl.IsAllowSearch Then
                                control = CreateClientTextBox(currentControl, defaultValue)
                            Else
                                control = CreateOtherControl(currentControl, defaultValue)
                            End If
                    End Select

                    If isButton Then
                        With btnOK
                            .Text = currentControl.Caption.GetValue(_currentLanguage)
                            .ToolTip = currentControl.ToolTip.GetValue(_currentLanguage)
                            .ID = "btnOk"
                            .CausesValidation = True
                            .ClientEnabled = currentControl.Enabled
                            .ValidationGroup = String.Format(CultureInfo.InvariantCulture, "{0}_{1}", ModelId, Release)
                        End With

                        AddHandler btnOK.Click, AddressOf btnOk_Click

                        isButton = False
                        .Add(btnOK)
                    Else
                        If currentControl.Type = EnumQueryParameterType.Client AndAlso Not currentControl.IsAllowSearch Then
                            .Add(New LiteralControl("<div style=""display: inline-flex;"">"))
                            .Add(control)

                            If currentControl.ShowCheckDigit Then
                                .Add(New LiteralControl("<div style=""float: left; padding-left: 3px;"">"))
                                .Add(CreateCheckDigitTextBox(currentControl))
                                .Add(New LiteralControl("</div>"))
                            End If

                            If currentControl.ShowClientName Then
                                .Add(New LiteralControl("<div style=""float: left; padding-left: 3px; padding-top: 4px;"">"))
                                .Add(CreateClientLabel(currentControl))
                                .Add(New LiteralControl("</div>"))
                            End If

                            .Add(New LiteralControl("</div>"))
                        Else
                            .Add(control)
                        End If
                    End If

                    .Add(New LiteralControl("</td>"))

                    countControl += 1
                    totalCountControl += 1

                    If countControl = _metadata.NumberOfColumns OrElse countControl > visibleControlCount Then

                        .Add(New LiteralControl("</tr>"))
                        .Add(New LiteralControl("<tr style=""vertical-align: top;"">"))

                        countControl = 0
                        currentWidth = 0
                    End If
                Next

                If _requiredFields Then
                    Dim messageError As String = "You must fill in all fields"

                    If _currentLanguage = 2 Then
                        messageError = "Debe llenar todos los campo requeridos"
                    End If

                    _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "if (errors) {{ {0}", vbCrLf)
                    _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "  alert('{0}'); {1}", messageError, vbCrLf)

                    _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "		e.processOnServer = false; {0}", vbCrLf)
                    _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "}}  else {{{0}", vbCrLf)
                    _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "  LoadingPanel.Show();{0}", vbCrLf)
                    '_functionValidationsList += String.Format(CultureInfo.InvariantCulture, "  s.SetEnabled(false);{0}", vbCrLf)
                    _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "}}{0}", vbCrLf)
                    Dim nameUnique As String = _metadata.ModelId.Replace("-", "").Substring(0, 8)

                    _stringBuilder.Append(String.Format(CultureInfo.InvariantCulture, "{0} function Validations{2}(s,e){{ {0} {1} {0}}}", vbCrLf, _functionValidationsList, nameUnique))

                    If _rangesFields Then
                        BuildingValidateRangesSentence()

                        _stringBuilder.Append(String.Format(CultureInfo.InvariantCulture, "{0} function ValidateRanges{2}(s,e){{ {0} {1} {0}}}", vbCrLf, _functionValidateRangesList, nameUnique))

                        btnOK.ClientSideEvents.Click = "function(s, e) { Validations" + nameUnique + "(s,e); ValidateRanges" + nameUnique + "(s,e); }"
                    Else
                        btnOK.ClientSideEvents.Click = "function(s, e) { Validations" + nameUnique + "(s,e); }"
                    End If

                ElseIf _rangesFields Then
                    Dim nameUnique As String = _metadata.ModelId.Replace("-", "").Substring(0, 8)

                    BuildingValidateRangesSentence()

                    _stringBuilder.Append(String.Format(CultureInfo.InvariantCulture, "{0} function ValidateRanges{2}(s,e){{ {0} {1} {0}}}", vbCrLf, _functionValidateRangesList, nameUnique))
                    btnOK.ClientSideEvents.Click = "function(s, e) { ValidateRanges" + nameUnique + "(s,e); }"
                End If

                If countControl > 0 Then
                    Dim colspan As Integer = (_metadata.NumberOfColumns - countControl)

                    .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture,
                                                          "    <td style=""width: {1}{2};"" colspan=""{0}"">&nbsp;</td>",
                                                          colspan * 2, 100 - currentWidth, measure)))
                End If

                .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture, "   </tr><CrLf>")))
                .Add(New LiteralControl(String.Format(CultureInfo.InvariantCulture, " </table><CrLf>")))
            End With
        End Sub

        Private Function SetValuesUserInputParameterControls() As Dictionary(Of String, Object)
            Dim result As New Dictionary(Of String, Object)

            If Not IsNothing(_metadata.Parameters) AndAlso _metadata.Parameters.Count > 0 Then
                For Each controlItem As QueryParameters In _metadata.Parameters
                    If controlItem.Type <> EnumQueryParameterType.Button Then
                        result.Add(controlItem.Name, GetParameterValueFromUserControl(controlItem.Name, True, False))
                    End If
                Next
            End If

            Return result
        End Function

#End Region

#Region "Controls for Parameters Methods"

        ''' <summary>
        ''' Crea un control web del tipo TextBox asociado a un parámetro
        ''' </summary>
        ''' <param name="item">Información del parametro</param>
        ''' <param name="defaultValue">Valor por default</param>
        ''' <returns>Instancia del control ASPxTextBox</returns>
        Private Function CreateTextControl(item As QueryParameters, defaultValue As String) As ASPxTextBox
            Dim txtControl = New ASPxTextBox

            With txtControl
                .ID = item.Name
                .ClientInstanceName = item.Name
                .Text = IIf(String.IsNullOrEmpty(defaultValue), item.Default, defaultValue)
                .ToolTip = item.ToolTip.GetValue(_currentLanguage)
                .MaxLength = item.Length
                .Size = item.Length
                .ClientEnabled = item.Enabled

                If Not String.IsNullOrEmpty(item.DefaultMethod) Then
                    .Text = Extend.DefaultMethod.DefaultValue(item.DefaultMethod, UserInfo)
                End If

                If Not String.IsNullOrEmpty(item.EnableControl) AndAlso Not String.IsNullOrEmpty(item.EnableValue) AndAlso Not Page.IsPostBack Then
                    .ClientEnabled = False
                End If

                Dim dependencyControl As String = FindDependencyControl(item.Name)
                Dim buildScript As Boolean = HasEnabledControl(item, False, String.Empty)

                If Not String.IsNullOrEmpty(dependencyControl) Then
                    .ClientSideEvents.Init = String.Format(CultureInfo.InvariantCulture,
                                                                   "function(s, e) {{{0}.PerformCallback({1}.GetValue());}}",
                                                                   dependencyControl, item.Name)

                    .ClientSideEvents.ValueChanged = String.Format(CultureInfo.InvariantCulture,
                                                                   "function(s, e) {{{0}.PerformCallback({1}.GetValue());{2} }}",
                                                                   dependencyControl, item.Name,
                                                                   IIf(buildScript, String.Format(CultureInfo.InvariantCulture, "{0}ValueChanged();", item.Name), String.Empty))
                Else
                    If buildScript Then
                        .ClientSideEvents.ValueChanged = String.Format(CultureInfo.InvariantCulture,
                                                                       "function(s, e) {{ {0}ValueChanged(); }}", item.Name)
                    End If
                End If

                If item.Required Then
                    BuildingValidationsBody(item.Name)

                    .ValidationSettings.Display = Display.Dynamic
                    .Paddings.PaddingLeft = New Unit("8px")

                    With .BackgroundImage
                        .HorizontalPosition = "left"
                        .ImageUrl = "/images/generaluse/required.PNG"
                        .Repeat = BackgroundImageRepeat.NoRepeat
                        .VerticalPosition = "center"
                    End With

                    _requiredFields = True
                End If
            End With

            Return txtControl
        End Function

        ''' <summary>
        ''' Crea un control web del tipo DateEdit asociado a un parámetro
        ''' </summary>
        ''' <param name="item">Información del parametro</param>
        ''' <param name="defaultValue">Valor por default</param>
        ''' <returns>Instancia del control ASPxDateEdit</returns>
        Private Function CreateDatePickerControl(item As QueryParameters, defaultValue As String) As ASPxDateEdit
            Dim defaultDateValue As Date = Date.MinValue
            Dim txtControlDate As New ASPxDateEdit

            If item.Default.IsNotEmpty Then
                If item.Default.ToLower.StartsWith("today", StringComparison.CurrentCultureIgnoreCase) Then
                    defaultDateValue = Date.Today.ApplyExtension(item.Default)
                Else
                    defaultDateValue = Date.Parse(item.Default)
                End If
            End If

            If Not String.IsNullOrEmpty(defaultValue) Then
                defaultDateValue = Date.Parse(defaultValue)
            End If

            With txtControlDate
                .ID = item.Name
                .ClientInstanceName = item.Name

                If defaultDateValue <> Date.MinValue Then
                    .Text = defaultDateValue
                End If

                .Width = 100
                .ToolTip = item.ToolTip.GetValue(_currentLanguage)
                .ClientEnabled = item.Enabled

                If Not String.IsNullOrEmpty(item.EnableControl) AndAlso Not String.IsNullOrEmpty(item.EnableValue) AndAlso Not Page.IsPostBack Then
                    .ClientEnabled = False
                End If

                If Not String.IsNullOrEmpty(item.DefaultMethod) Then
                    .Text = Extend.DefaultMethod.DefaultValue(item.DefaultMethod, UserInfo)
                End If

                Dim dependencyControl As String = FindDependencyControl(item.Name)
                Dim buildScript As Boolean = HasEnabledControl(item, False, String.Empty)

                If Not String.IsNullOrEmpty(dependencyControl) Then
                    .ClientSideEvents.Init = String.Format(CultureInfo.InvariantCulture,
                                                                   "function(s, e) {{{0}.PerformCallback({1}.GetDate().toString());}}",
                                                                   dependencyControl, item.Name)

                    .ClientSideEvents.DateChanged = String.Format(CultureInfo.InvariantCulture,
                                                                  "function(s, e) {{{0}.PerformCallback({1}.GetDate().toString());{2} }}",
                                                                  dependencyControl, item.Name,
                                                                  IIf(buildScript, String.Format(CultureInfo.InvariantCulture, "{0}ValueChanged();", item.Name), String.Empty))
                Else
                    If buildScript Then
                        .ClientSideEvents.DateChanged = String.Format(CultureInfo.InvariantCulture,
                                                                       "function(s, e) {{ {0}ValueChanged(); }}", item.Name)
                    End If
                End If

                If item.Required Then
                    BuildingValidationsBody(item.Name)

                    .ValidationSettings.Display = Display.Dynamic

                    .Paddings.PaddingLeft = New Unit("8px")

                    With .BackgroundImage
                        .HorizontalPosition = "left"
                        .ImageUrl = "/images/generaluse/required.PNG"
                        .Repeat = BackgroundImageRepeat.NoRepeat
                        .VerticalPosition = "center"
                    End With

                    _requiredFields = True
                End If

                If Not String.IsNullOrEmpty(item.BeginRangeControl) Then
                    For Each currentControl As QueryParameters In _metadata.Parameters
                        If String.Equals(currentControl.Name, item.BeginRangeControl, StringComparison.CurrentCultureIgnoreCase) Then
                            BuildingValidateRangesBody(currentControl, item)
                            _rangesFields = True
                        End If
                    Next
                End If
            End With

            Return txtControlDate
        End Function

        ''' <summary>
        ''' Crea un control web del tipo TextBox para numeros asociado a un parámetro
        ''' </summary>
        ''' <param name="item">Información del parametro</param>
        ''' <param name="defaultValue">Valor por default</param>
        ''' <returns>Instancia del control ASPxTextBox</returns>
        Private Function CreateNumericControl(item As QueryParameters, defaultValue As String) As ASPxTextBox
            Dim numericControl As New ASPxTextBox
            Dim precision As Integer

            With numericControl
                .ID = item.Name
                .ClientInstanceName = item.Name
                .Text = IIf(String.IsNullOrEmpty(defaultValue), item.Default, defaultValue)
                .HorizontalAlign = HorizontalAlign.Right

                If String.IsNullOrEmpty(.Text) Then
                    .Text = "0"
                End If

                If item.Precision = 0 Then
                    precision = 10
                Else
                    precision = item.Precision
                End If

                .Size = IIf(item.Scale = 0, precision.ToString, (item.Scale + precision + 1).ToString)

                .MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.DecimalSymbol
                .MaskSettings.Mask = FormatSettings.AddMask(False, False, precision, item.Scale, item.AllowNegative, item.ShowDecimalSymbol)
                .ToolTip = item.ToolTip.GetValue(_currentLanguage)
                .ClientEnabled = item.Enabled

                If Not String.IsNullOrEmpty(item.EnableControl) AndAlso Not String.IsNullOrEmpty(item.EnableValue) AndAlso Not Page.IsPostBack Then
                    .ClientEnabled = False
                End If

                If Not String.IsNullOrEmpty(item.DefaultMethod) Then
                    .Text = Extend.DefaultMethod.DefaultValue(item.DefaultMethod, UserInfo)
                End If

                Dim dependencyControl As String = FindDependencyControl(item.Name)
                Dim buildScript As Boolean = HasEnabledControl(item, False, String.Empty)

                If Not String.IsNullOrEmpty(dependencyControl) Then
                    .ClientSideEvents.Init = String.Format(CultureInfo.InvariantCulture,
                                                                   "function(s, e) {{{0}.PerformCallback({1}.GetValue().toString());}}",
                                                                   dependencyControl, item.Name)

                    .ClientSideEvents.ValueChanged = String.Format(CultureInfo.InvariantCulture,
                                                                   "function(s, e) {{{0}.PerformCallback({1}.GetValue().toString());{2} }}",
                                                                   dependencyControl, item.Name,
                                                                   IIf(buildScript, String.Format(CultureInfo.InvariantCulture, "{0}ValueChanged();", item.Name), String.Empty))
                Else
                    If buildScript Then
                        .ClientSideEvents.ValueChanged = String.Format(CultureInfo.InvariantCulture,
                                                                       "function(s, e) {{ {0}ValueChanged(); }}", item.Name)
                    End If
                End If

                If item.Required Then
                    BuildingValidationsBody(item.Name)

                    .ValidationSettings.Display = Display.Dynamic
                    .Paddings.PaddingLeft = New Unit("8px")

                    With .BackgroundImage
                        .HorizontalPosition = "left"
                        .ImageUrl = "/images/generaluse/required.PNG"
                        .Repeat = BackgroundImageRepeat.NoRepeat
                        .VerticalPosition = "center"
                    End With

                    _requiredFields = True
                End If
            End With

            Return numericControl
        End Function

        ''' <summary>
        ''' Crea un control web del tipo CheckBox asociado a un parámetro
        ''' </summary>
        ''' <param name="item">Información del parametro</param>
        ''' <param name="defaultValue">Valor por default</param>
        ''' <returns>Instancia del control ASPxCheckBox</returns>
        Private Function CreateCheckBoxControl(item As QueryParameters, defaultValue As String) As ASPxCheckBox
            Dim controlCheckBox As New ASPxCheckBox
            Dim defaultBooleanValue As Boolean

            If Not String.IsNullOrEmpty(defaultValue) Then
                defaultBooleanValue = Boolean.Parse(defaultValue)
            Else
                defaultBooleanValue = False
            End If

            With controlCheckBox
                .ID = item.Name
                .ClientInstanceName = item.Name
                .Text = item.Caption.GetValue(_currentLanguage)
                .Checked = defaultBooleanValue
                .Width = 100
                .ToolTip = item.ToolTip.GetValue(_currentLanguage)
                .ClientEnabled = item.Enabled

                If Not String.IsNullOrEmpty(item.EnableControl) AndAlso Not String.IsNullOrEmpty(item.EnableValue) AndAlso Not Page.IsPostBack Then
                    .ClientEnabled = False
                End If

                If Not String.IsNullOrEmpty(item.DefaultMethod) Then
                    .Checked = Extend.DefaultMethod.DefaultValue(item.DefaultMethod, UserInfo)
                End If

                Dim buildScript As Boolean = HasEnabledControl(item, False, String.Empty)

                If buildScript Then
                    .ClientSideEvents.ValueChanged = String.Format(CultureInfo.InvariantCulture,
                                                                   "function(s, e) {{ {0}ValueChanged(); }}", item.Name)
                End If

                If item.Required Then
                    BuildingValidationsBody(item.Name)

                    .ValidationSettings.Display = Display.Dynamic

                    With .BackgroundImage
                        .HorizontalPosition = "left"
                        .ImageUrl = "/images/generaluse/required.PNG"
                        .Repeat = BackgroundImageRepeat.NoRepeat
                        .VerticalPosition = "center"
                    End With

                    _requiredFields = True
                End If
            End With

            Return controlCheckBox
        End Function

        ''' <summary>
        ''' Crea un user control o control personalizado asociado a un parámetro
        ''' </summary>
        ''' <param name="item">Información del parametro</param>
        ''' <param name="defaultValue">Valor por default</param>
        ''' <returns>Instancia del control UserControl</returns>
        Private Function CreateOtherControl(item As QueryParameters, defaultValue As String) As UserControl
            Dim newUserControl As New UserControl

            Select Case item.Type
                Case EnumQueryParameterType.Custom
                    newUserControl = LoadControl(String.Format(CultureInfo.InvariantCulture, "~\{0}.ascx", item.CustomName))

                Case Else
                    Select Case item.Type.ToString.ToUpper
                        Case "CLIENT"
                            If Not String.IsNullOrEmpty(defaultValue) Then
                                defaultValue = InMotionGIT.BackOffice.Support.Connection.Client.ExpandClientId(defaultValue)
                            End If
                    End Select

                    newUserControl = LoadControl(String.Format(CultureInfo.InvariantCulture, "~\Controls\{0}Control.ascx", item.Type.ToString))
            End Select

            With DirectCast(newUserControl, GIT.EDW.Query.Model.Interfaces.IQueryUserControl)
                .ControlID = item.Name
                .Value = IIf(String.IsNullOrEmpty(defaultValue), item.Default, defaultValue)
                .Repository = _metadata.Repository
                .ToolTip = item.ToolTip.GetValue(_currentLanguage)
                .Enabled = item.Enabled

                If Not String.IsNullOrEmpty(item.EnableControl) AndAlso Not String.IsNullOrEmpty(item.EnableValue) AndAlso Not Page.IsPostBack Then
                    .Enabled = False
                End If

                If Not String.IsNullOrEmpty(item.DefaultMethod) Then
                    .Value = Extend.DefaultMethod.DefaultValue(item.DefaultMethod, UserInfo)
                End If

                Dim functionScript As String = String.Empty
                Dim buildScript As Boolean = HasEnabledControl(item, True, functionScript)

                If buildScript Then
                    .Script = String.Format(CultureInfo.InvariantCulture, "function(s, e) {{{0}}}", functionScript)
                End If
            End With

            Return newUserControl
        End Function

        ''' <summary>
        ''' Crea un control web del tipo DropDown asociado a un parámetro
        ''' </summary>
        ''' <param name="item">Información del parametro</param>
        ''' <param name="defaultValue">Valor por default</param>
        ''' <remarks>Instancia del control ASPxComboBox</remarks>
        Private Function CreateDropDownControl(item As QueryParameters, defaultValue As String) As ASPxComboBox
            Dim dependencyControl As String = String.Empty
            Dim codeFieldKind As String = String.Empty
            Dim imageColumn As String = String.Empty
            Dim fieldImageColumn As String = String.Empty
            Dim firstPart As String = String.Empty
            Dim secondPart As String = String.Empty
            Dim sqlstatement As String = String.Empty
            Dim codeField As String = String.Empty
            Dim buildScript As Boolean = HasEnabledControl(item, False, String.Empty)
            Dim hasDependency As Boolean = False
            Dim ddlControl As New ASPxComboBox With {.ID = item.Name, .ClientInstanceName = item.Name,
                                                     .ClientEnabled = item.Enabled,
                                                     .ToolTip = item.ToolTip.GetValue(_currentLanguage)}

            If Not String.IsNullOrEmpty(item.EnableControl) AndAlso Not String.IsNullOrEmpty(item.EnableValue) AndAlso Not Page.IsPostBack Then
                ddlControl.ClientEnabled = False
            End If

            If item.Required Then
                BuildingValidationsBody(item.Name)

                ddlControl.Paddings.PaddingLeft = New Unit("8px")
                ddlControl.ValidationSettings.Display = Display.Dynamic

                With ddlControl.BackgroundImage
                    .HorizontalPosition = "left"
                    .ImageUrl = "/images/generaluse/required.PNG"
                    .Repeat = BackgroundImageRepeat.NoRepeat
                    .VerticalPosition = "center"
                End With

                _requiredFields = True
            End If

            If Not IsNothing(item.DataSource) Then
                codeField = item.DataSource.Code

                If codeField.Contains(".") Then
                    codeField = codeField.Split(".")(1)
                End If

                With ddlControl
                    .ValueField = codeField
                    .TextField = item.DataSource.Description(0).Name
                    .IncrementalFilteringMode = IncrementalFilteringMode.Contains

                    If buildScript Then
                        .ClientSideEvents.ValueChanged = String.Format(CultureInfo.InvariantCulture,
                                                                       "function(s, e) {{ {0}ValueChanged(); }}", item.Name)
                    End If
                End With

                With New DataManagerFactory(String.Format(CultureInfo.InvariantCulture, "SELECT {0}, {1} FROM {2}{3} ORDER BY {4}",
                                                          codeField, item.DataSource.Description(0).Name,
                                                          ConfigurationManager.AppSettings("BackOfficeConnectionString.Owner"),
                                                          item.DataSource.Entity, item.DataSource.Description(0).Name),
                                            "QueryManager Input", String.Format("Linked.{0}", item.DataSource.Source))

                    ddlControl.DataSource = .QueryExecuteToTable(True)
                    ddlControl.DataBind()
                End With
            Else
                If Not IsNothing(item.Lookup.ItemsSettings) Then
                    Dim display As String = String.Empty

                    With ddlControl
                        .Items.Clear()

                        For Each itemData As InMotionGIT.Actions.Designer.LookupSettings.Item In item.Lookup.ItemsSettings.Items
                            If item.Lookup.ShownValue = Designer.Enumerations.EnumShownValue.CodeAndDescription Then
                                display = String.Format(CultureInfo.InvariantCulture, "{0} - {1}",
                                                   itemData.Value, itemData.Display.GetUpValue(_currentLanguage, EnumLanguage.English))
                            Else
                                display = itemData.Display.GetUpValue(_currentLanguage, EnumLanguage.English)
                            End If

                            If Not String.IsNullOrEmpty(item.Lookup.ImagePathMask) Then
                                .Items.Add(New ListEditItem With {.Value = itemData.Value, .Text = display,
                                                                 .ImageUrl = String.Format(CultureInfo.InvariantCulture, item.Lookup.ImagePathMask, itemData.Value)})
                            Else
                                .Items.Add(New ListEditItem With {.Value = itemData.Value, .Text = display})
                            End If
                        Next

                        codeFieldKind = item.Lookup.ItemsSettings.BindingType
                        .ValueType = SetValueTypeComboBox(codeFieldKind)

                        dependencyControl = FindDependencyControl(item.Name)

                        If Not String.IsNullOrEmpty(dependencyControl) Then
                            .ClientSideEvents.SelectedIndexChanged = String.Format(CultureInfo.InvariantCulture,
                                                                                  "function(s, e) {{{0}.PerformCallback({1}.GetValue().toString());{2} }}",
                                                                                  dependencyControl, item.Name,
                                                                                  IIf(buildScript, String.Format(CultureInfo.InvariantCulture, "{0}ValueChanged();", item.Name), String.Empty))

                            AddHandler ddlControl.Callback, AddressOf DropDownCallback
                            hasDependency = True
                        Else
                            If buildScript Then
                                .ClientSideEvents.SelectedIndexChanged = String.Format(CultureInfo.InvariantCulture,
                                                                               "function(s, e) {{ {0}ValueChanged(); }}", item.Name)
                            End If
                        End If
                    End With
                Else
                    With ddlControl
                        codeField = item.Lookup.Code

                        If codeField.Contains(".") Then
                            codeField = codeField.Split(".")(1)
                        End If

                        .ValueField = codeField
                        .TextField = item.Lookup.Description(0).Name
                        .IncrementalFilteringMode = IncrementalFilteringMode.Contains
                        .Style.Value = "position: static"

                        codeFieldKind = item.Lookup.CodeField.Split(",")(1)

                        .ValueType = SetValueTypeComboBox(codeFieldKind)

                        If item.Lookup.Description.Count > 1 Then
                            For Each descCol As InMotionGIT.Actions.Designer.DataDescriptionField In item.Lookup.Description
                                .Columns.Add(New ListBoxColumn(descCol.Name, descCol.Caption.GetValue(_currentLanguage)))
                            Next
                        End If

                        .ClientSideEvents.BeginCallback = String.Format(CultureInfo.InvariantCulture,
                                                                        "function (s, e) {{LoadingPanel.ShowInElementByID({0}); LoadingPanel.Show(); }}", .ID)
                        .ClientSideEvents.EndCallback = "function (s, e) { LoadingPanel.Hide(); }"

                        dependencyControl = FindDependencyControl(item.Name)

                        If Not String.IsNullOrEmpty(dependencyControl) Then
                            hasDependency = True

                            .ClientSideEvents.Init = String.Format(CultureInfo.InvariantCulture,
                                                                           "function(s, e) {{{0}.PerformCallback({1}.GetValue());}}",
                                                                           dependencyControl, item.Name)

                            .ClientSideEvents.ValueChanged = String.Format(CultureInfo.InvariantCulture,
                                                                           "function(s, e) {{{0}.PerformCallback({1}.GetValue()); {2} }}",
                                                                           dependencyControl, item.Name,
                                                                           IIf(buildScript, String.Format(CultureInfo.InvariantCulture, "{0}ValueChanged();", item.Name), String.Empty))
                        Else
                            If buildScript Then
                                .ClientSideEvents.SelectedIndexChanged = String.Format(CultureInfo.InvariantCulture,
                                                                                       "function(s, e) {{ {0}ValueChanged(); }}", item.Name)
                            End If
                        End If
                    End With

                    If Not IsNothing(item.Lookup) AndAlso Not IsNothing(item.Lookup.Dependency) AndAlso item.Lookup.Dependency.Count > 0 Then
                        AddHandler ddlControl.Callback, AddressOf DropDownCallback
                        hasDependency = True
                    Else
                        sqlstatement = item.Lookup.SelectStatement

                        If Not String.IsNullOrEmpty(item.Lookup.ImagePathMask) Then
                            SplitImagePathMaskString(item.Lookup.ImagePathMask, firstPart, secondPart)
                            imageColumn = String.Format(CultureInfo.InvariantCulture, "{0}IMAGE", ddlControl.ValueField)

                            ddlControl.ShowImageInEditBox = True
                            ddlControl.ImageUrlField = imageColumn
                            sqlstatement = sqlstatement.Replace(" FROM ", String.Format(CultureInfo.InvariantCulture, ", '{0}' || {1} || '{2}' AS {3} FROM ",
                                                                                        firstPart, item.Lookup.CodeField.Split(",")(0), secondPart, imageColumn))
                        End If

                        If item.Lookup.ShownValue = Designer.Enumerations.EnumShownValue.CodeAndDescription Then
                            sqlstatement = sqlstatement.Replace(" FROM ", String.Format(CultureInfo.InvariantCulture, ", {0} || ' - ' || {1} AS CODE_DESCRIPTION FROM ",
                                                                                        codeField.ToUpper, item.Lookup.Description(0).Name.ToUpper))
                            ddlControl.TextField = "CODE_DESCRIPTION"
                        End If

                        With New DataManagerFactory(ConvertSqlStatement(sqlstatement), "QueryManager Input", String.Format("Linked.{0}", item.Lookup.Source))
                            ddlControl.DataSource = .QueryExecuteToTable(True)
                            ddlControl.DataBind()
                        End With
                    End If
                End If
            End If

            If Not item.Required AndAlso Not hasDependency Then
                ddlControl.Items.Insert(0, New ListEditItem("", "0"))
            End If

            If Not IsNothing(ddlControl.Items) AndAlso ddlControl.Items.Count > 0 Then
                If Not String.IsNullOrEmpty(defaultValue) Then
                    ddlControl.SelectedItem = ddlControl.Items.FindByValue(SetDefaultValueComboBox(codeFieldKind, defaultValue))

                ElseIf Not String.IsNullOrEmpty(item.Default) Then
                    ddlControl.SelectedItem = ddlControl.Items.FindByValue(SetDefaultValueComboBox(codeFieldKind, item.Default))

                ElseIf Not String.IsNullOrEmpty(item.DefaultMethod) Then
                    ddlControl.SelectedItem = ddlControl.Items.FindByValue(Extend.DefaultMethod.DefaultValue(item.DefaultMethod, UserInfo))
                End If
            End If

            Return ddlControl
        End Function

#End Region

#Region "Special Client Controls for Parameters Methods"

        ''' <summary>
        ''' Crea un control web del tipo TextBox para el parámetro Cliente
        ''' </summary>
        ''' <param name="item">Información del parametro</param>
        ''' <param name="defaultValue">Valor por default</param>
        ''' <returns>Instancia del control ASPxTextBox</returns>
        Private Function CreateClientTextBox(item As QueryParameters, defaultValue As String) As ASPxTextBox
            Dim clientTextBox As New ASPxTextBox
            Dim checkDigitControl As String = String.Empty
            Dim clientControl As String = String.Empty

            With clientTextBox
                .ID = item.Name
                .ClientInstanceName = item.Name
                .Text = IIf(String.IsNullOrEmpty(defaultValue), item.Default, defaultValue)
                .ToolTip = item.ToolTip.GetValue(_currentLanguage)
                .Size = 15
                .MaxLength = 14
                .ClientEnabled = item.Enabled
                .ClientIDMode = ClientIDMode.Static

                If Not String.IsNullOrEmpty(item.DefaultMethod) Then
                    .Text = Extend.DefaultMethod.DefaultValue(item.DefaultMethod, UserInfo)
                End If

                If Not String.IsNullOrEmpty(item.EnableControl) AndAlso
                   Not String.IsNullOrEmpty(item.EnableValue) AndAlso Not Page.IsPostBack Then

                    .ClientEnabled = False
                End If

                If item.ShowCheckDigit Then
                    checkDigitControl = String.Format(CultureInfo.CurrentCulture, "{0}CheckDigit", item.Name)
                Else
                    checkDigitControl = "null"
                End If

                If item.ShowClientName Then
                    clientControl = String.Format(CultureInfo.CurrentCulture, "{0}ClientName", item.Name)
                Else
                    clientControl = "null"
                End If

                .ClientSideEvents.Validation = String.Format(CultureInfo.InvariantCulture,
                                                             "function(s, e) {{e.isValid = ClientSupport.CodeAndDigitStep1(s, {0}, {1}, null, false, '{2}', {3}, {4}, {5}, true, '{6}', {7});}}",
                                                             checkDigitControl, clientControl, item.RegularExpressionValidate, "null", "null", item.FillZeros.ToString.ToLower,
                                                             item.AllowClientType.ToString.ToLower, item.OnlyExistingClient.ToString.ToLower)

                .ClientSideEvents.TextChanged = String.Format(CultureInfo.InvariantCulture,
                                                              "function(s, e) {{ClientSupport.CodeAndDigitStep1(s, {0}, {1}, null, false, '{2}', {3}, {4}, {5}, false, '{6}', {7});}}",
                                                              checkDigitControl, clientControl, item.RegularExpressionValidate, "null", "null", item.FillZeros.ToString.ToLower,
                                                              item.AllowClientType.ToString.ToLower, item.OnlyExistingClient.ToString.ToLower)

                BuildingValidationsBody(item.Name)

                With .ValidationSettings
                    .EnableCustomValidation = True
                    .ErrorDisplayMode = ErrorDisplayMode.Text
                    .ErrorTextPosition = ErrorTextPosition.Bottom
                    .Display = Display.Dynamic
                End With

                If item.Required Then
                    .Paddings.PaddingLeft = New Unit("8px")

                    With .BackgroundImage
                        .HorizontalPosition = "left"
                        .ImageUrl = "/images/generaluse/required.PNG"
                        .Repeat = BackgroundImageRepeat.NoRepeat
                        .VerticalPosition = "center"
                    End With

                    _requiredFields = True
                End If
            End With

            Return clientTextBox
        End Function

        ''' <summary>
        ''' Crea un control web del tipo TextBox para el parámetro Cliente con el número digitador
        ''' </summary>
        ''' <param name="controlData">Información del parametro</param>
        ''' <returns>Instancia de un control ASPxTextBox</returns>
        Private Function CreateCheckDigitTextBox(controlData As QueryParameters) As ASPxTextBox
            Dim clientTextBox As New ASPxTextBox
            Dim clientControl As String = String.Empty

            With clientTextBox
                .ID = String.Format(CultureInfo.CurrentCulture, "{0}CheckDigit", controlData.Name)
                .ClientInstanceName = .ID
                .Size = 2
                .MaxLength = 1
                .Width = 24
                .ClientEnabled = controlData.Enabled
                .ClientIDMode = ClientIDMode.Static

                If Not String.IsNullOrEmpty(controlData.DefaultMethod) Then
                    .Text = Extend.DefaultMethod.DefaultValue(controlData.DefaultMethod, UserInfo)
                End If

                If Not String.IsNullOrEmpty(controlData.EnableControl) AndAlso
                   Not String.IsNullOrEmpty(controlData.EnableValue) AndAlso Not Page.IsPostBack Then

                    .ClientEnabled = False
                End If

                If controlData.ShowClientName Then
                    clientControl = String.Format(CultureInfo.CurrentCulture, "{0}ClientName", controlData.Name)
                Else
                    clientControl = "null"
                End If

                .ClientSideEvents.Validation = String.Format(CultureInfo.InvariantCulture,
                                                             "function(s, e) {{e.isValid = ClientSupport.CodeAndDigitStep2({0}, s, {1}, null, false, '{2}', {3}, {4}, {5}, true, '{6}', {7});e.errorText = '';}}",
                                                             controlData.Name, clientControl, controlData.RegularExpressionValidate, "null", "null", controlData.FillZeros.ToString.ToLower,
                                                             controlData.AllowClientType.ToString.ToLower, controlData.OnlyExistingClient.ToString.ToLower)

                .ClientSideEvents.TextChanged = String.Format(CultureInfo.InvariantCulture,
                                                              "function(s, e) {{ClientSupport.CodeAndDigitStep2({0}, s, {1}, null, false, '{2}', {3}, {4}, {5}, false, '{6}', {7});}}",
                                                              controlData.Name, clientControl, controlData.RegularExpressionValidate, "null", "null", controlData.FillZeros.ToString.ToLower,
                                                              controlData.AllowClientType.ToString.ToLower, controlData.OnlyExistingClient.ToString.ToLower)

                With .ValidationSettings
                    .EnableCustomValidation = True
                    .ErrorDisplayMode = ErrorDisplayMode.Text
                    .ErrorTextPosition = ErrorTextPosition.Bottom
                    .Display = Display.Dynamic
                End With

                If controlData.Required Then
                    .Paddings.PaddingLeft = New Unit("8px")

                    With .BackgroundImage
                        .HorizontalPosition = "left"
                        .ImageUrl = "/images/generaluse/required.PNG"
                        .Repeat = BackgroundImageRepeat.NoRepeat
                        .VerticalPosition = "center"
                    End With
                End If
            End With

            Return clientTextBox
        End Function

        ''' <summary>
        ''' Crea un control web del tipo Label para el parámetro Cliente para mostrar el nombre del cliente
        ''' </summary>
        ''' <param name="controlData">Información del parametro</param>
        ''' <returns>Instancia de la etiqueta</returns>
        Private Function CreateClientLabel(controlData As QueryParameters) As ASPxTextBox
            Dim clientLabel As New ASPxTextBox

            With clientLabel
                .ID = String.Format(CultureInfo.CurrentCulture, "{0}ClientName", controlData.Name)
                .ClientInstanceName = .ID
                .EncodeHtml = False
                .ClientEnabled = False
                .ClientVisible = True
                .ClientIDMode = ClientIDMode.Static
                .Border.BorderWidth = 0
                .BackColor = Color.Transparent
            End With

            Return clientLabel
        End Function

#End Region

#Region "Validations for Parameters Methods"

        Private Sub BuildingValidationsBody(controlName As String)
            Dim messageError As String = "The field is required"

            If _currentLanguage = 2 Then
                messageError = "El campo es requerido"
            End If

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "if ({0}.GetEnabled(false)) {{ {1}", controlName, vbCrLf)

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "   var {0}Value = {0}.GetValue();{1}", controlName, vbCrLf)

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "   if ({0}Value == null || {0}Value == '') {{ {1}", controlName, vbCrLf)

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "      {0}.SetIsValid(false); {1}", controlName, vbCrLf)
            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "      {0}.SetErrorText('{1}'); {2}", controlName, messageError, vbCrLf)

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "      errors = true;  {0}", vbCrLf)

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "   }}  {0}", vbCrLf)

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "   else{{  {0}", vbCrLf)

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "       {0}.SetIsValid(true); {1}", controlName, vbCrLf)
            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "       {0}.SetErrorText(''); {1}", controlName, vbCrLf)

            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "   }}  {0}", vbCrLf)
            _functionValidationsList += String.Format(CultureInfo.InvariantCulture, "}}  {0}", vbCrLf)
        End Sub

        Private Sub BuildingValidateRangesBody(currentControl As QueryParameters, rangeControl As QueryParameters)
            Dim messageError As String = String.Format(CultureInfo.InvariantCulture, "The value of {0} must be major at {1}",
                                                     rangeControl.Caption.GetValue(1), currentControl.Caption.GetValue(1))

            If _currentLanguage = 2 Then
                messageError = String.Format(CultureInfo.InvariantCulture, "El valor de {0} debe ser mayor al {1}",
                                             rangeControl.Caption.GetValue(_currentLanguage), currentControl.Caption.GetValue(_currentLanguage))
            End If

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "if ({0}.GetEnabled(true) && {1}.GetEnabled(true)) {{ {2}", currentControl.Name, rangeControl.Name, vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "   var {0}Value = {0}.GetValue();{1}", currentControl.Name, vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "   var {0}Value = {0}.GetValue();{1}", rangeControl.Name, vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "   if (({0}Value != null && {0}Value != '') && ({1}Value != null && {1}Value != '')) {{ {2}", currentControl.Name, rangeControl.Name, vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "       if ({0}Value > {1}Value) {{ {2}", currentControl.Name, rangeControl.Name, vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "          errorsMessage += '{0}\r\n';  {1}", messageError, vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "          {0}.SetIsValid(false); {1}", currentControl.Name, vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "          {0}.SetErrorText('{1}'); {2}", currentControl.Name, messageError, vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "       }}  {0}", vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "       else{{  {0}", vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "         {0}.SetIsValid(true); {1}", currentControl.Name, vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "         {0}.SetErrorText(''); {1}", currentControl.Name, vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "       }}  {0}", vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "   }}  {0}", vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "}}  {0}", vbCrLf)
        End Sub

        Private Sub BuildingValidateRangesSentence()
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "if (errorsMessage != '') {{ {0}", vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "   alert(errorsMessage); {0}", vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "   e.processOnServer = false; {0}", vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "   LoadingPanel.Hide();{0}", vbCrLf)

            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "}}  else {{{0}", vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "  LoadingPanel.Show();{0}", vbCrLf)
            _functionValidateRangesList += String.Format(CultureInfo.InvariantCulture, "}}{0}", vbCrLf)
        End Sub

#End Region

#Region "Behavior Parameters Methods"

        ''' <summary>
        ''' Find the dependency control by name
        ''' </summary>
        ''' <param name="controlName">name of the control</param>
        ''' <returns>True: found</returns>
        Private Function FindDependencyControl(controlName As String) As String
            Dim result As String = String.Empty

            If Not IsNothing(_metadata.Parameters) AndAlso _metadata.Parameters.Count > 0 Then
                For Each parameter As QueryParameters In _metadata.Parameters
                    With parameter

                        If .Type = EnumQueryParameterType.DropDown Then
                            If Not IsNothing(.Lookup) AndAlso
                               Not IsNothing(.Lookup.Dependency) AndAlso .Lookup.Dependency.Count > 0 Then

                                For Each dependency As InMotionGIT.Actions.Designer.DataDependency In .Lookup.Dependency
                                    If String.Equals(dependency.ControlName, controlName, StringComparison.CurrentCultureIgnoreCase) Then
                                        result = parameter.Name

                                        Exit For
                                    End If
                                Next
                            End If
                        End If

                    End With
                Next
            End If

            Return result
        End Function

        Private Sub DropDownCallback(sender As Object, e As CallbackEventArgsBase)
            If Not String.IsNullOrEmpty(e.Parameter) Then
                Dim selectedComboBox As ASPxComboBox = sender
                Dim newValue As Object = Nothing
                Dim dataType As DbType
                Dim codeField As String = String.Empty
                Dim codeFieldKind As String = String.Empty
                Dim dependencyParameterName As String

                For Each parameterData As QueryParameters In _metadata.Parameters
                    If String.Equals(parameterData.Name, selectedComboBox.ID, StringComparison.CurrentCultureIgnoreCase) Then

                        With New DataManagerFactory(BuildingSelectCommand(parameterData.Lookup, selectedComboBox), "QueryManager Input", String.Format("Linked.{0}", parameterData.Lookup.Source))

                            For Each dependency As InMotionGIT.Actions.Designer.DataDependency In parameterData.Lookup.Dependency
                                codeField = dependency.CodeField.Replace(".", String.Empty).Trim
                                dependencyParameterName = String.Format(CultureInfo.InvariantCulture, "{0}Dependency", codeField)

                                If dependencyParameterName.Length >= 26 Then
                                    dependencyParameterName = dependencyParameterName.Substring(0, 25)
                                End If

                                dataType = GetCodeFieldDbType(dependency.CodeFieldType, e.Parameter, newValue)

                                Select Case dataType
                                    Case DbType.Currency
                                        newValue = ToDouble(newValue)

                                    Case DbType.Int32
                                        newValue = ToInt32(newValue)

                                    Case DbType.Decimal
                                        newValue = ToDecimal(newValue)

                                    Case DbType.Date, DbType.DateTime
                                        newValue = ToDateTime(newValue)

                                    Case Else
                                End Select

                                .AddParameter(dependencyParameterName, dataType, 0, False, newValue)
                            Next

                            If parameterData.Lookup.ShownValue = Designer.Enumerations.EnumShownValue.CodeAndDescription Then
                                selectedComboBox.TextField = "CODE_DESCRIPTION"
                            End If

                            selectedComboBox.DataSource = .QueryExecuteToTable(True)
                            selectedComboBox.DataBind()

                            If IsNothing(selectedComboBox.BackgroundImage) OrElse String.IsNullOrEmpty(selectedComboBox.BackgroundImage.ImageUrl) Then
                                selectedComboBox.Items.Insert(0, New ListEditItem("", "0"))
                            End If

                            If Not IsNothing(parameterData.Lookup) Then
                                If Not IsNothing(parameterData.Lookup.ItemsSettings) Then
                                    codeFieldKind = parameterData.Lookup.ItemsSettings.BindingType
                                Else
                                    codeFieldKind = parameterData.Lookup.CodeField.Split(",")(1)
                                End If

                                If Not IsNothing(selectedComboBox.Items) AndAlso selectedComboBox.Items.Count > 0 Then
                                    If Not String.IsNullOrEmpty(parameterData.Default) Then

                                        selectedComboBox.SelectedItem = selectedComboBox.Items.FindByValue(SetDefaultValueComboBox(codeFieldKind, parameterData.Default))

                                    ElseIf Not String.IsNullOrEmpty(parameterData.DefaultMethod) Then
                                        selectedComboBox.SelectedItem = selectedComboBox.Items.FindByValue(Extend.DefaultMethod.DefaultValue(parameterData.DefaultMethod, UserInfo))
                                    End If
                                End If
                            End If
                        End With
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Create the statement 'SELECT' to execute
        ''' </summary>
        ''' <param name="lookup">Contains the information of tables, columns, conditions, etc...</param>
        ''' <remarks>'Select''command for the comboBox</remarks>
        Private Function BuildingSelectCommand(lookup As InMotionGIT.Actions.Designer.Lookup, ByRef selectedComboBox As ASPxComboBox) As String
            Dim descriptionfield As String = String.Empty
            Dim codeFieldList As String = String.Empty
            Dim conditionDependency As String = String.Empty
            Dim codeField As String = String.Empty
            Dim dependencyParameterName As String
            Dim imageColumn As String = String.Empty
            Dim fieldImageColumn As String = String.Empty
            Dim firstPart As String = String.Empty
            Dim secondPart As String = String.Empty
            Dim sqlstatement As String = lookup.SelectStatement

            If Not String.IsNullOrEmpty(lookup.ImagePathMask) Then
                SplitImagePathMaskString(lookup.ImagePathMask, firstPart, secondPart)
                imageColumn = String.Format(CultureInfo.InvariantCulture, "{0}IMAGE", selectedComboBox.ValueField)

                selectedComboBox.ShowImageInEditBox = True
                selectedComboBox.ImageUrlField = imageColumn

                sqlstatement = sqlstatement.Replace(" FROM ", String.Format(CultureInfo.InvariantCulture, ", '{0}' || {1} || '{2}' AS {3} FROM ",
                                                                            firstPart, lookup.CodeField.Split(",")(0), secondPart, imageColumn))
            End If

            If lookup.ShownValue = Designer.Enumerations.EnumShownValue.CodeAndDescription Then
                sqlstatement = sqlstatement.Replace(" FROM ", String.Format(CultureInfo.InvariantCulture, ", {0} || ' - ' || {1} AS CODE_DESCRIPTION FROM ",
                                                                            lookup.Code.ToUpper, lookup.Description(0).Name.ToUpper))
            End If

            sqlstatement = ConvertSqlStatement(sqlstatement)

            If sqlstatement.Contains("ORDER BY") Then
                sqlstatement = sqlstatement.Replace("ORDER BY", " @@DEPENDENCY_FIELD@@ ORDER BY")
            Else
                sqlstatement += " @@DEPENDENCY_FIELD@@"
            End If

            For Each description As InMotionGIT.Actions.Designer.DataDescriptionField In lookup.Description
                If description.Display Then
                    descriptionfield = description.Name.ToUpper

                    Exit For
                End If
            Next

            For Each dependency As InMotionGIT.Actions.Designer.DataDependency In lookup.Dependency
                With dependency
                    If conditionDependency.Length > 0 Then
                        conditionDependency += " AND "
                    End If

                    If codeFieldList.Length > 0 Then
                        codeFieldList += ","
                    End If

                    codeField = .CodeField.Replace(".", String.Empty).Trim
                    codeFieldList += String.Format(CultureInfo.InvariantCulture, "{0}-{1}", codeField, .CodeFieldType)

                    dependencyParameterName = String.Format(CultureInfo.InvariantCulture, "{0}Dependency", codeField)

                    If dependencyParameterName.Length >= 26 Then
                        dependencyParameterName = dependencyParameterName.Substring(0, 25)
                    End If

                    conditionDependency += String.Format(CultureInfo.InvariantCulture, "{0} = @:{1}", .CodeField, dependencyParameterName)
                End With
            Next

            If sqlstatement.Contains("WHERE") Then
                sqlstatement = sqlstatement.Replace("@@DEPENDENCY_FIELD@@",
                                                    String.Format(CultureInfo.CurrentCulture, " AND {0} AND NOT {1} IS NULL", conditionDependency, descriptionfield))
            Else
                sqlstatement = sqlstatement.Replace("@@DEPENDENCY_FIELD@@",
                                                    String.Format(CultureInfo.CurrentCulture, " WHERE {0} AND NOT {1} IS NULL", conditionDependency, descriptionfield))
            End If

            If Not sqlstatement.Contains("ORDER BY") Then
                sqlstatement += String.Format(CultureInfo.CurrentCulture, " ORDER BY {0}", descriptionfield)
            End If

            Return sqlstatement
        End Function

        ''' <summary>
        ''' Return database's type by code field's type
        ''' </summary>
        ''' <param name="kind">Code filed type</param>
        ''' <param name="parameterValue">Selected parameter value</param>
        ''' <param name="newValue">return converted parameter value</param>
        ''' <remarks>Database DbType</remarks>
        Private Function GetCodeFieldDbType(kind As String, parameterValue As String, ByRef newValue As String) As DbType
            Dim result As DbType

            newValue = parameterValue

            Select Case kind
                Case "Integer", "Int16", "Int32", "Int64", "Numeric"
                    result = DbType.Int32

                Case "Decimal"
                    result = DbType.Decimal

                Case "Double"
                    result = DbType.Currency

                Case "String"
                    result = DbType.AnsiString

                Case "Char"
                    result = DbType.AnsiStringFixedLength

                Case "DateTime", "Date"
                    result = DbType.DateTime

                    newValue = ToDateTime(parameterValue).ToString(_dateformat)

                Case "Boolean"
                    result = DbType.Boolean

                    If String.Equals(parameterValue, "True", StringComparison.CurrentCultureIgnoreCase) Then
                        newValue = "1"
                    Else
                        newValue = "0"
                    End If

                Case Else
            End Select

            Return result
        End Function

        Private Function ConvertSqlStatement(query As String) As String
            Dim newQuery As String = query

            If newQuery.Contains("WHERE") Then
                newQuery = newQuery.Replace("ConfigurationManager.AppSettings(""ClientName"")", ConfigurationManager.AppSettings("ClientName"))
                newQuery = newQuery.Replace("ConfigurationManager.AppSettings(""CountryCode"")", ConfigurationManager.AppSettings("CountryCode"))
                newQuery = newQuery.Replace("ConfigurationManager.AppSettings(""CountryName"")", ConfigurationManager.AppSettings("CountryName"))

                newQuery = newQuery.Replace("(Request.QueryString(""readonly"") = ""yes"")", (Request.QueryString("readonly") = "yes"))

                newQuery = newQuery.Replace("Request.QueryString.Item(""Action"")", Request.QueryString.Item("Action"))
                newQuery = newQuery.Replace("Request.QueryString.Item(""nMainAction"")", Request.QueryString.Item("nMainAction"))

                newQuery = newQuery.Replace("Session(""sClient"")", Session("sClient"))
                newQuery = newQuery.Replace("Session(""LanguageId"")", Session("LanguageId"))
                newQuery = newQuery.Replace("Session(""nBranch"")", Session("nBranch"))
                newQuery = newQuery.Replace("Session(""nUserCode"")", Session("nUserCode"))
                newQuery = newQuery.Replace("Session(""nProduct"")", Session("nProduct"))
                newQuery = newQuery.Replace("Session(""AccessToken"")", Session("AccessToken"))

                newQuery = newQuery.Replace("Nothing", Nothing)
                newQuery = newQuery.Replace("Date.Today", Date.Today)
                newQuery = newQuery.Replace("String.Empty", String.Empty)

                Dim profileData As GIT.Core.PageBase = TryCast(HttpContext.Current.Handler, GIT.Core.PageBase)

                If Not IsNothing(profileData) AndAlso Not IsNothing(profileData) Then
                    With profileData.UserInfo
                        newQuery = newQuery.Replace("Profile.ClientID", .ClientID)
                        newQuery = newQuery.Replace("Security.IsAdministrator()", .IsAdministrator())
                        newQuery = newQuery.Replace("Profile.IsAnonymous", .IsAnonymous)
                        newQuery = newQuery.Replace("Profile.IsClient()", .IsClient())
                        newQuery = newQuery.Replace("Profile.IsEmployee()", .IsEmployee)
                        newQuery = newQuery.Replace("Profile.IsProducer()", .IsProducer())
                        newQuery = newQuery.Replace("Profile.LanguageId", .LanguageId)
                        newQuery = newQuery.Replace("Profile.ProducerID", .ProducerID)
                    End With
                End If
            End If

            Return newQuery
        End Function

        Private Function HasEnabledControl(selectedControl As QueryParameters, specialControl As Boolean, ByRef scriptCode As String) As Boolean
            Dim buildScript As Boolean = False
            Dim scriptBody As String = String.Empty

            For Each parameterData As QueryParameters In _metadata.Parameters
                If String.Equals(parameterData.EnableControl, selectedControl.Name, StringComparison.CurrentCultureIgnoreCase) Then

                    With parameterData
                        Select Case .Type
                            Case EnumQueryParameterType.Text, EnumQueryParameterType.Client,
                                 EnumQueryParameterType.Vehicle, EnumQueryParameterType.DatePicker

                                scriptBody += BuildingValuesByType(parameterData, selectedControl.Name, .EnableValue, True, specialControl)

                            Case EnumQueryParameterType.Claim, EnumQueryParameterType.Intermed,
                                 EnumQueryParameterType.Numeric, EnumQueryParameterType.Policy,
                                 EnumQueryParameterType.Premium, EnumQueryParameterType.Provider,
                                 EnumQueryParameterType.CheckBox

                                scriptBody += BuildingValuesByType(parameterData, selectedControl.Name, .EnableValue, False, specialControl)

                            Case EnumQueryParameterType.DropDown
                                With .Lookup
                                    If Not IsNothing(.QueryTable) Then
                                        If Not String.IsNullOrEmpty(.CodeField) AndAlso .CodeField.Contains(",") Then
                                            scriptBody += GetCodeType(parameterData, selectedControl.Name, .CodeField.Split(",")(1), parameterData.EnableValue)
                                        End If

                                    ElseIf Not IsNothing(.ItemsSettings) Then
                                        scriptBody += GetCodeType(parameterData, selectedControl.Name, .ItemsSettings.BindingType, parameterData.EnableValue)

                                    ElseIf Not String.IsNullOrEmpty(.Source) AndAlso .Source.Contains("@") Then
                                        If Not String.IsNullOrEmpty(.CodeField) AndAlso .CodeField.Contains(",") Then
                                            scriptBody += GetCodeType(parameterData, selectedControl.Name, .CodeField.Split(",")(1), parameterData.EnableValue)
                                        End If

                                    ElseIf Not IsNothing(.CatalogSettings) Then
                                        scriptBody += GetCodeType(parameterData, selectedControl.Name, .CatalogSettings.BindingType, parameterData.EnableValue)
                                    Else
                                        scriptBody += BuildingValuesByType(parameterData, selectedControl.Name, parameterData.EnableValue, False, specialControl)
                                    End If
                                End With

                            Case Else
                                scriptBody += BuildingValuesByType(parameterData, selectedControl.Name, .EnableValue, True, specialControl)
                        End Select
                    End With

                    buildScript = True
                End If
            Next

            If buildScript Then
                If specialControl Then
                    scriptCode = scriptBody
                Else
                    _stringBuilder.Append(String.Format(CultureInfo.InvariantCulture, "function {0}ValueChanged(){{ {1} }}", selectedControl.Name, scriptBody))

                    If _functionNameList.Length > 0 Then
                        _functionNameList += ";"
                    End If

                    _functionNameList += String.Format(CultureInfo.InvariantCulture, "{0}ValueChanged()", selectedControl.Name)
                End If
            End If

            Return buildScript
        End Function

        Private Function GetCodeType(parameterData As QueryParameters, functionName As String, kind As String, values As String) As String
            Dim result As String = String.Empty

            If Not String.IsNullOrEmpty(kind) Then
                If kind.Contains(".") Then
                    kind = kind.Split(".")(1)
                End If
            Else
                kind = "Int32"
            End If

            Select Case kind
                Case "Integer", "Int16", "Int32", "Numeric",
                     "Int64", "Decimal", "Double", "Boolean"

                    result = BuildingValuesByType(parameterData, functionName, values, False, False)

                Case "String", "Char", "DateTime", "Date"

                    result = BuildingValuesByType(parameterData, functionName, values, True, False)

                Case Else
            End Select

            Return result
        End Function

        Private Function BuildingValuesByType(parameterData As QueryParameters, functionName As String, values As String, isStringField As Boolean, specialControl As Boolean) As String

            Dim scriptBody As String = String.Format(CultureInfo.InvariantCulture, "var value={0}.GetValue();{1}", functionName, vbCrLf)
            Dim conditions As String = String.Empty

            If values.Contains(",") Then
                For Each value As String In values.Split(",")
                    If conditions.Length > 0 Then
                        conditions += " || "
                    End If

                    If isStringField Then
                        conditions += String.Format(CultureInfo.InvariantCulture, "value == '{0}'", value)
                    Else
                        conditions += String.Format(CultureInfo.InvariantCulture, "value == {0}", value)
                    End If
                Next
            Else
                If isStringField Then
                    conditions += String.Format(CultureInfo.InvariantCulture, "value == '{0}'", values)
                Else
                    conditions += String.Format(CultureInfo.InvariantCulture, "value == {0}", values)
                End If
            End If

            scriptBody += String.Format(CultureInfo.InvariantCulture, "if ({0}) {{ {1}", conditions, vbCrLf)

            If parameterData.Type <> EnumQueryParameterType.CheckBox Then
                scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}label.SetEnabled(true); {1}", parameterData.Name, vbCrLf)
            End If

            If parameterData.Type = EnumQueryParameterType.Client AndAlso Not parameterData.IsAllowSearch Then
                If parameterData.ShowCheckDigit Then
                    scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}CheckDigit.SetEnabled(true); {1}", parameterData.Name, vbCrLf)
                End If
            End If

            scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetEnabled(true); }} else {{ {1}", parameterData.Name, vbCrLf)

            If parameterData.Type <> EnumQueryParameterType.CheckBox Then
                scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}label.SetEnabled(false); {1}", parameterData.Name, vbCrLf)
            End If

            If parameterData.Type = EnumQueryParameterType.Client AndAlso Not parameterData.IsAllowSearch Then
                If parameterData.ShowCheckDigit Then
                    scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}CheckDigit.SetEnabled(false); {1}", parameterData.Name, vbCrLf)
                End If
            End If

            scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetEnabled(false); {1}", parameterData.Name, vbCrLf)

            Select Case parameterData.Type
                Case EnumQueryParameterType.CheckBox
                    scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetValue(false); }} {1}", parameterData.Name, vbCrLf)

                Case EnumQueryParameterType.DropDown
                    scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetValue(null); }} {1}", parameterData.Name, vbCrLf)

                Case EnumQueryParameterType.Numeric
                    scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetValue(0); }} {1}", parameterData.Name, vbCrLf)

                Case EnumQueryParameterType.DatePicker
                    If String.IsNullOrEmpty(parameterData.Default) Then
                        scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetValue(null); }} {1}", parameterData.Name, vbCrLf)
                    Else
                        scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetValue(new Date()); }} {1}", parameterData.Name, vbCrLf)
                    End If

                Case EnumQueryParameterType.Client
                    If Not parameterData.IsAllowSearch Then
                        If parameterData.ShowCheckDigit Then
                            scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}CheckDigit.SetValue(''); {1}", parameterData.Name, vbCrLf)
                        End If

                        If parameterData.ShowClientName Then
                            scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}ClientName.SetValue(''); {1}", parameterData.Name, vbCrLf)
                        End If
                    End If

                    scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetValue(''); }} {1}", parameterData.Name, vbCrLf)

                Case Else
                    scriptBody += String.Format(CultureInfo.InvariantCulture, "{0}.SetValue(''); }} {1}", parameterData.Name, vbCrLf)
            End Select

            Return scriptBody
        End Function

        Private Sub BuildingEnabledControlDependency(controlName As String)
            Dim selectedParameterData As QueryParameters = Nothing

            For Each parameterData As QueryParameters In _metadata.Parameters
                If String.Equals(parameterData.EnableControl, controlName, StringComparison.CurrentCultureIgnoreCase) Then
                    selectedParameterData = parameterData

                    Exit For
                End If
            Next

            If Not IsNothing(selectedParameterData) Then
                Dim currentControl As Object = pnlUserInput.FindControl(selectedParameterData.Name)
                Dim currentLabel As Object = pnlUserInput.FindControl(String.Format(CultureInfo.InvariantCulture, "{0}Label", selectedParameterData.Name))
                Dim value As Object = GetParameterValueFromUserControl(controlName, True, False)

                If BuildingDynamicStatements(value, selectedParameterData.EnableValue) Then
                    If Not IsNothing(currentControl) Then
                        If selectedParameterData.Type = EnumQueryParameterType.Client Then
                            currentControl.Enabled = True
                        Else
                            currentControl.ClientEnabled = True
                        End If
                    End If

                    If Not IsNothing(currentLabel) Then
                        currentLabel.ClientEnabled = True
                    End If
                Else
                    If Not IsNothing(currentControl) Then
                        If selectedParameterData.Type = EnumQueryParameterType.Client Then
                            currentControl.Enabled = False
                        Else
                            currentControl.ClientEnabled = False
                        End If
                    End If

                    If Not IsNothing(currentLabel) Then
                        currentLabel.ClientEnabled = False
                    End If
                End If
            End If
        End Sub

        Private Function BuildingDynamicStatements(controlValue As Object, values As String) As Boolean
            Dim result As Boolean = False

            If values.Contains(",") Then
                For Each value As Object In values.Split(",")
                    If value = controlValue Then
                        result = True

                        Exit For
                    End If
                Next
            Else
                If values = controlValue Then
                    result = True
                End If
            End If

            Return result
        End Function

        Private Function SetValueTypeComboBox(kind As String) As Type
            Dim result As Type

            If kind.Contains(".") Then
                kind = kind.Split(".")(1)
            End If

            Select Case kind
                Case "Integer", "Int16", "Int32", "Numeric", "Double"
                    result = GetType(Int32)

                Case "Int64"
                    result = GetType(Int64)

                Case "Decimal"
                    result = GetType(Decimal)

                Case "Byte"
                    result = GetType(Byte)

                Case "String", "Char"
                    result = GetType(String)

                Case "DateTime", "Date"
                    result = GetType(DateTime)

                Case "Boolean"
                    result = GetType(Boolean)

                Case Else
                    result = GetType(String)
            End Select

            Return result
        End Function

        Private Function SetDefaultValueComboBox(kind As String, defaultValue As String) As Object
            Dim result As Object

            If kind.Contains(".") Then
                kind = kind.Split(".")(1)
            End If

            Select Case kind
                Case "Integer", "Int16", "Int32", "Numeric", "Double"
                    result = Convert.ToInt32(defaultValue)

                Case "Int64"
                    result = Convert.ToInt64(defaultValue)

                Case "Decimal"
                    result = Convert.ToDecimal(defaultValue)

                Case "Byte"
                    result = Convert.ToByte(defaultValue)

                Case "String", "Char"
                    result = Convert.ToString(defaultValue)

                Case "DateTime", "Date"
                    result = Convert.ToDateTime(defaultValue)

                Case "Boolean"
                    result = Convert.ToBoolean(defaultValue)

                Case Else
                    result = defaultValue
            End Select

            Return result
        End Function

#End Region

#Region "Search Data Parameters Methods"

        Private Function FindParameterControlByName(controlName As String) As QueryParameters
            Dim result As QueryParameters = Nothing

            For Each controlData As QueryParameters In _metadata.Parameters
                If String.Equals(controlData.Name, controlName, StringComparison.CurrentCultureIgnoreCase) Then
                    result = controlData

                    Exit For
                End If
            Next

            Return result
        End Function

        Private Function LoadParametersClientLabels() As Dictionary(Of String, String)
            Dim result As New Dictionary(Of String, String)

            If Not IsNothing(pnlUserInput.Controls) AndAlso pnlUserInput.Controls.Count > 0 Then
                Dim controlVB As Control = Nothing

                For Each controlData As QueryParameters In _metadata.Parameters

                    With controlData
                        If .Type = EnumQueryParameterType.Client AndAlso Not .IsAllowSearch AndAlso Not .ShowClientName Then
                            controlVB = pnlUserInput.FindControl(String.Format(CultureInfo.CurrentCulture, "{0}ClientName", controlData.Name))

                            If Not IsNothing(controlVB) Then
                                result.Add(controlVB.ID, DirectCast(controlVB, ASPxLabel).Text)
                            End If
                        End If
                    End With

                Next
            End If

            Return result
        End Function

#End Region

#Region "Style Columns Methods"

        Private Function SearchColumnDataStyle(currentTable As tablequery, fieldName As String, columnName As String) As Boolean
            Dim result As Boolean = False
            Dim columnData As columnquery = currentTable.FindColumnByRealName(columnName)

            If Not IsNothing(columnData) Then
                With columnData
                    If Not IsNothing(.ColumnSource) AndAlso Not String.IsNullOrEmpty(.ColumnSource.RelationshipTable) Then

                        If .ColumnSource.StyleValue <> enumShownValue.Code Then
                            If String.Equals(fieldName, String.Format(CultureInfo.InvariantCulture, "{0}DESC", columnName), StringComparison.CurrentCultureIgnoreCase) Then
                                result = True
                            End If
                        End If

                    End If
                End With
            End If

            Return result
        End Function

#End Region

#Region "Action's Conditions Methods"

        Private Function GenerateConditionsByAction(filters As InMotionGIT.Actions.Designer.Command.ConditionCollection) As String
            Dim conditions As String = String.Empty
            Dim logicalValue As String
            Dim operatorCondition As String
            Dim expression As String
            Dim realName As String
            Dim realKind As String

            For Each conditionItem As InMotionGIT.Actions.Designer.Command.Condition In filters
                With conditionItem
                    logicalValue = String.Empty

                    If Not String.IsNullOrEmpty(.BeginGroup) Then
                        conditions += "("
                    End If

                    If Not String.IsNullOrEmpty(.Name) Then
                        logicalValue = GetLogicalCondition(.LogicalOperator)
                        operatorCondition = GetValidationOperator(.Operator)
                        realName = FindExpressionCondition(Nothing, .Name, String.Empty, True).ToUpper

                        If realName.Contains(".") Then
                            realName = realName.Split(".")(1)

                        ElseIf realName.Contains("@") Then
                            realName = realName.Split("@")(1)
                        End If

                        If Not String.IsNullOrEmpty(operatorCondition) Then
                            expression = FindExpressionCondition(Nothing, .Expression, String.Empty, True)

                            If String.Equals(.Expression, expression, StringComparison.CurrentCultureIgnoreCase) Then
                                realKind = .Type

                                If Not String.IsNullOrEmpty(realKind) Then
                                    If realKind.Contains(".") Then
                                        realKind = .Type.Split(".")(1)
                                    End If

                                    Select Case realKind.ToUpper
                                        Case "STRING", "CHAR", "VARCHAR", "VARCHAR2", "NVARCHAR"

                                            expression = String.Format(CultureInfo.InvariantCulture, "'{0}'", expression)

                                        Case "DATE", "DATETIME"
                                            expression = String.Format(CultureInfo.InvariantCulture, "#{0}#", expression)
                                    End Select
                                End If
                            Else
                                If expression.Contains(".") Then
                                    expression = expression.Split(".")(1)

                                ElseIf expression.Contains("@") Then
                                    expression = expression.Split("@")(1)
                                End If
                            End If

                            conditions += String.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", realName, operatorCondition, expression)
                        Else

                            Select Case .Operator
                                Case InMotionGIT.Actions.Designer.Actions.Enumerations.EnumOperator.IsEmpty
                                    Select Case .Type.ToUpper
                                        Case "SYSTEM.STRING", "STRING", "CHAR", "VARCHAR", "VARCHAR2", "NVARCHAR",
                                             "SYSTEM.DATETIME", "SYSTEM.DATE", "DATE", "DATETIME"

                                            conditions += String.Format(CultureInfo.InvariantCulture, "{0} = ''", realName)

                                        Case Else
                                            conditions += String.Format(CultureInfo.InvariantCulture, "{0} IS NULL", realName)
                                    End Select

                                Case InMotionGIT.Actions.Designer.Actions.Enumerations.EnumOperator.NotEmpty
                                    Select Case .Type.ToUpper
                                        Case "SYSTEM.STRING", "STRING", "CHAR", "VARCHAR", "VARCHAR2", "NVARCHAR",
                                             "SYSTEM.DATETIME", "SYSTEM.DATE", "DATE", "DATETIME"

                                            conditions += String.Format(CultureInfo.InvariantCulture, "{0} <> ''", realName)

                                        Case Else
                                            conditions += String.Format(CultureInfo.InvariantCulture, "{0} IS NOT NULL", realName)
                                    End Select

                                Case Else
                            End Select
                        End If
                    End If

                    If Not String.IsNullOrEmpty(.EndGroup) Then
                        conditions += ")"

                        If String.IsNullOrEmpty(.Name) Then
                            logicalValue = GetLogicalCondition(.LogicalOperator)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(logicalValue) Then
                        conditions += String.Format(CultureInfo.InvariantCulture, " {0} ", logicalValue)
                    End If
                End With
            Next

            Return conditions
        End Function

#End Region

    End Class

End Namespace