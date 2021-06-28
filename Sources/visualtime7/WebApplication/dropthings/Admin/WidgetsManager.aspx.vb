Imports GIT.Core
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxUploadControl
Imports DevExpress.Web.ASPxEditors
Imports System.IO
Imports System.Collections.Generic
Imports System.Data
Imports DevExpress.Web.ASPxGridView
Imports InMotionGIT.Core.Configuration
Imports InMotionGIT.Core.Configuration.Enumerations
Imports InMotionGIT.Common
Imports InMotionGIT.Common.Proxy
Imports DevExpress.Web.Data
Imports System.Globalization

Partial Class dropthings_Admin_WidgetsManager
    Inherits PageBase

    Private WidgetTransCollection As List(Of WidgetTrans)
    Private _widgetID As Integer = Integer.MinValue

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GridViewWidgets.DataBind()


            Dim GridViewLanguages As ASPxGridView = GridViewWidgets.FindEditRowCellTemplateControl(GridViewWidgets.Columns("DESCRIPTION"), "GridViewLanguage")

            If Not IsNothing(GridViewLanguages) Then
                If GridViewWidgets.IsEditing Then
                    WidgetTransCollection = ProcessLanguages(GridViewLanguages)
                End If
            End If


        End If


    End Sub

#End Region

#Region "GridViewWidgets Events"

    Protected Sub GridViewWidgets_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs) Handles GridViewWidgets.CellEditorInitialize

        Select Case e.Column.FieldName.ToLower
            Case "id"
                e.Editor.ReadOnly = True
                e.Editor.Enabled = False
            Case "url"
                e.Editor.DataSource = WidgetsFile()
                e.Editor.DataBind()
            Case "iconedit"
                e.Editor.DataSource = WidgetsImage()
                e.Editor.DataBind()
            Case "defaultstate"
                If Not GridViewWidgets.IsNewRowEditing Then
                    Dim specialCharacters As String = e.Editor.Value
                    If Not String.IsNullOrEmpty(specialCharacters) Then
                        specialCharacters = specialCharacters.Replace("<", "[")
                        specialCharacters = specialCharacters.Replace(">", "]")
                        e.Editor.Value = specialCharacters
                    End If
                End If
        End Select

    End Sub

    Protected Sub GridViewWidgets_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridViewWidgets.CustomCallback
        Select Case e.Parameters.ToString.ToLower

            Case "delete"
                Dim KeyValues As Generic.List(Of Object) = GridViewWidgets.GetSelectedFieldValues("ID")
                For Each key As Object In KeyValues
                    WidgetTrans.Delete(key)
                    Widget.Delete(key)
                Next key

                CleanCacheFromWidget()

                GridViewWidgets.DataBind()

            Case "delete_partial"
                Dim KeyValues As Generic.List(Of Object) = GridViewWidgets.GetSelectedFieldValues("ID")
                For Each key As Object In KeyValues
                    WidgetTrans.DeleteWidgetInstanceAndWidgetInstanceTrance(key)
                Next key
        End Select
    End Sub

    Public Sub CleanCacheFromWidget()
        If HttpContext.Current.IsNotEmpty AndAlso HttpContext.Current.Cache.IsNotEmpty Then
            For Each Items As DictionaryEntry In System.Web.HttpContext.Current.Cache
                Dim keyItem As String = Items.Key
                If keyItem.Contains("Widgets") Then
                    System.Web.HttpContext.Current.Cache.Remove(keyItem)
                End If
            Next
        End If
    End Sub

    Protected Sub GridViewWidgets_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewWidgets.DataBinding
        GridViewWidgets.DataSource = Widget.RetrieveAll
    End Sub

    Protected Sub GridViewWidgets_HtmlEditFormCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs) Handles GridViewWidgets.HtmlEditFormCreated

        Dim GridViewLanguage As ASPxGridView = GridViewWidgets.FindEditRowCellTemplateControl(GridViewWidgets.Columns("Description"), "GridViewLanguage")
        If Not IsNothing(GridViewLanguage) Then

            GridViewLanguage.DataSource = RetreiveAllLanguages()
            GridViewLanguage.DataBind()
        End If
    End Sub

    Protected Sub GridViewWidgets_ParseValue(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxParseValueEventArgs) Handles GridViewWidgets.ParseValue
        If e.FieldName.ToLower = "defaultstate" Then
            If Not IsNothing(e.Value) Then
                Dim specialCharacters As String = e.Value
                specialCharacters = specialCharacters.Replace("[", "<")
                specialCharacters = specialCharacters.Replace("]", ">")
                e.Value = specialCharacters
            End If
        End If
    End Sub

    Protected Sub GridViewWidgets_RowInserted(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertedEventArgs) Handles GridViewWidgets.RowInserted
        GridViewWidgets.DataBind()
    End Sub

    Protected Sub GridViewWidgets_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles GridViewWidgets.RowInserting

        Dim WidgetInstance As New Widget

        WidgetInstance.Name = e.NewValues("NAME")
        WidgetInstance.Url = e.NewValues("Url").ToString
        WidgetInstance.Description = e.NewValues("DESCRIPTION")
        WidgetInstance.CreatedDate = Date.Now
        WidgetInstance.LastUpdate = Date.Now
        WidgetInstance.VersionNo = 1
        WidgetInstance.IsDefault = False
        WidgetInstance.DefaultState = e.NewValues("DefaultState")
        WidgetInstance.Icon = e.NewValues("IconEdit")
        WidgetInstance.OrderNo = 0
        WidgetInstance.IsAnonymouAllow = False

        _widgetID = Widget.Insert(WidgetInstance)

        If _widgetID <> Integer.MinValue Then
            SyncronizeLanguages()
        End If

        e.Cancel = True
        GridViewWidgets.CancelEdit()

    End Sub

    Protected Sub GridViewWidgets_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatedEventArgs) Handles GridViewWidgets.RowUpdated
        GridViewWidgets.DataBind()
    End Sub

    Private Sub GridViewWidgets_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles GridViewWidgets.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        Dim isFound As Boolean = False
        If e.IsNewRow Then
            If IsNothing(e.NewValues("Url")) Then
                isFound = True
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold;color:#FF0000'>{0}</ul>", GetLocalResourceObject("UrlValidation").ToString)
            End If
            If IsNothing(e.NewValues("IconEdit")) Then
                isFound = True
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold;color:#FF0000'>{0}</ul>", GetLocalResourceObject("IconEditValidation").ToString)
            End If
            If isFound Then
                e.RowError = errorMessage
            End If
        End If
    End Sub



    Protected Sub GridViewWidgets_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridViewWidgets.RowUpdating

        Dim URL As String = e.NewValues("Url").ToString

        Dim WIDGETINSTANCE As New Widget

        WIDGETINSTANCE.Name = e.NewValues("Name")
        WIDGETINSTANCE.Url = URL
        WIDGETINSTANCE.Description = e.NewValues("Description")
        WIDGETINSTANCE.LastUpdate = Date.Now
        WIDGETINSTANCE.VersionNo = Convert.ToInt32(e.NewValues("VersionNo")) + 1
        WIDGETINSTANCE.DefaultState = e.NewValues("DefaultState")
        WIDGETINSTANCE.Icon = e.NewValues("IconEdit")
        WIDGETINSTANCE.OrderNo = e.NewValues("OrdenNo")

        WIDGETINSTANCE.ID = e.Keys("ID")

        _widgetID = WIDGETINSTANCE.ID

        If Widget.Update(WIDGETINSTANCE) Then
            SyncronizeLanguages()
        End If

        e.Cancel = True
        GridViewWidgets.CancelEdit()

    End Sub

    Protected Sub GridViewWidgets_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles GridViewWidgets.StartRowEditing
        If _widgetID = Integer.MinValue Then
            _widgetID = e.EditingKeyValue
        End If
    End Sub

#End Region

#Region "Page Custom Methods"

    Public Function WidgetsFile() As List(Of WidgetFile)

        Dim sPath As String = ConfigurationManager.AppSettings("WidgetsPath")

        Dim KeyValues As New Generic.List(Of WidgetFile)
        For Each Path As String In Directory.GetFiles(sPath)
            If Path.ToLower.EndsWith("ascx") Then
                Dim _WidgetPath As New WidgetFile
                _WidgetPath.WidgetFileName = System.IO.Path.GetFileName(Path).Trim
                _WidgetPath.WidgetPath = "widgets/" & _WidgetPath.WidgetFileName.Trim
                KeyValues.Add(_WidgetPath)
            End If
        Next

        Return KeyValues
    End Function

    Public Function WidgetsImage() As List(Of WidgetImage)
        Dim sPath As String = ConfigurationManager.AppSettings("WidgetsImagePath")
        Dim backOfficePath As String = ConfigurationManager.AppSettings("WidgetsImageBackOfficePath")
        Dim WebAppplicationPath As String = ConfigurationManager.AppSettings("WebApplicationPath")
        Dim KeyValues As New Generic.List(Of WidgetImage)
        Dim widgetImage As WidgetImage

        For Each path As String In Directory.GetFiles(sPath)
            With path
                If .ToLower.EndsWith(".ico") Or .ToLower.EndsWith(".png") Or
                   .ToLower.EndsWith(".jpg") Or .ToLower.EndsWith(".gif") Then

                    widgetImage = New WidgetImage

                    With widgetImage
                        .ImageName = System.IO.Path.GetFileName(path).Trim
                        .ImagePath = path.Replace(WebAppplicationPath, "~").Trim
                    End With

                    KeyValues.Add(widgetImage)
                End If
            End With
        Next

        For Each officePath As String In Directory.GetFiles(backOfficePath)
            With officePath
                If .ToLower.EndsWith(".ico") Or .ToLower.EndsWith(".png") Or
                   .ToLower.EndsWith(".jpg") Or .ToLower.EndsWith(".gif") Then

                    widgetImage = New WidgetImage

                    With widgetImage
                        .ImageName = System.IO.Path.GetFileName(officePath).Trim
                        .ImagePath = officePath.Replace(WebAppplicationPath, "~").Trim
                    End With
                    KeyValues.Add(widgetImage)
                End If
            End With
        Next

        Return KeyValues
    End Function

    Function RetreiveAllLanguages() As DataTable
        Return WidgetTrans.RetrieveAllLanguages(_widgetID)
    End Function

    Protected Sub ASPxGridView1_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Dim gridView As ASPxGridView = CType(sender, ASPxGridView)

        Dim row = e.Keys(0)
        Dim enumerator As IDictionaryEnumerator = e.NewValues.GetEnumerator()
        enumerator.Reset()
        Do While enumerator.MoveNext()
            row(enumerator.Key.ToString()) = enumerator.Value
        Loop
        gridView.CancelEdit()
        e.Cancel = True
        'If e.DataColumn.FieldName = "title" Then
        '    Dim GridViewLanguage As ASPxGridView = GridViewWidgets.FindEditRowCellTemplateControl(GridViewWidgets.Columns("Description"), "GridViewLanguage")
        '    Dim COLUMNNAME As GridViewDataTextColumn = TryCast(GridViewLanguage.Columns("Name"), GridViewDataTextColumn)
        '    Dim COLUMNDESCRIPTION As GridViewDataTextColumn = TryCast(GridViewLanguage.Columns("Description"), GridViewDataTextColumn)
        '    Dim textBox As ASPxTextBox = TryCast(GridViewLanguage.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "ASPxTextBox1"), ASPxTextBox)
        '    textBox.Text = Convert.ToString(e.CellValue)
        'End If
    End Sub

    Protected Sub GridViewLanguage_RowValidating(sender As Object, e As ASPxDataValidationEventArgs)
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        Dim gridView As ASPxGridView = CType(sender, ASPxGridView)
        Dim enumerator As IDictionaryEnumerator = e.NewValues.GetEnumerator()
        Dim isFound As Boolean = False
        If e.IsNewRow Then
            If IsNothing(e.NewValues("Url")) Then
                isFound = True
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold;color:#FF0000'>{0}</ul>", GetLocalResourceObject("UrlValidation").ToString)
            End If
            If IsNothing(e.NewValues("IconEdit")) Then
                isFound = True
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold;color:#FF0000'>{0}</ul>", GetLocalResourceObject("IconEditValidation").ToString)
            End If
            If isFound Then
                e.RowError = errorMessage
            End If
        End If
    End Sub

    Protected Sub GridViewLanguage_RowUpdated(sender As Object, e As ASPxDataUpdatedEventArgs)

    End Sub

    Function ProcessLanguages(ByRef GridViewLanguage As ASPxGridView) As List(Of WidgetTrans)
        CType(GridViewLanguage.Columns("LanguageCode"), GridViewDataColumn).SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        Dim START As Integer = GridViewLanguage.PageIndex * GridViewLanguage.SettingsPager.PageSize
        Dim [END] As Integer = (GridViewLanguage.PageIndex + 1) * GridViewLanguage.SettingsPager.PageSize
        Dim COLUMNNAME As GridViewDataTextColumn = TryCast(GridViewLanguage.Columns("Name"), GridViewDataTextColumn)
        Dim COLUMNDESCRIPTION As GridViewDataTextColumn = TryCast(GridViewLanguage.Columns("Description"), GridViewDataTextColumn)
        Dim _WIDGETTRANS As New List(Of WidgetTrans)

        Dim source As DataTable = GridViewLanguage.DataSource
        source.DefaultView.Sort = "LanguageCode ASC"

        For i As Integer = START To [END] - 1
            Dim txtBoxName As ASPxTextBox = CType(GridViewLanguage.FindRowCellTemplateControl(i, COLUMNNAME, "Name"), ASPxTextBox)
            Dim txtBoxDescription As ASPxTextBox = CType(GridViewLanguage.FindRowCellTemplateControl(i, COLUMNDESCRIPTION, "Description"), ASPxTextBox)

            If txtBoxName Is Nothing OrElse txtBoxDescription Is Nothing Then
                Continue For
            End If

            Dim LanguageID As Integer = Convert.ToInt32(GridViewLanguage.GetRowValues(i, GridViewLanguage.KeyFieldName))
            Dim LanguageItem As New WidgetTrans
            LanguageItem.LanguageID = LanguageID
            LanguageItem.Name = txtBoxName.Text
            LanguageItem.Description = txtBoxDescription.Text

            LanguageItem.UpdateDate = Date.Now
            LanguageItem.UpdateUserCode = UserInfo.UserName

            _WIDGETTRANS.Add(LanguageItem)

        Next i

        Return _WIDGETTRANS
    End Function

    Function SyncronizeLanguages() As Boolean


        Dim GridViewLanguages As ASPxGridView = GridViewWidgets.FindEditRowCellTemplateControl(GridViewWidgets.Columns("DESCRIPTION"), "GridViewLanguage")

        If Not IsNothing(GridViewLanguages) Then
            If GridViewWidgets.IsEditing Then
                WidgetTransCollection = ProcessLanguages(GridViewLanguages)
            End If
        End If


        For Each item As WidgetTrans In WidgetTransCollection

            item.ID = _widgetID
            Dim Exists As Boolean = WidgetTrans.IsExists(item.ID, item.LanguageID)

            Select Case Exists
                Case True
                    ' Si existe y los campos están vacíos, se marca para borrar
                    If item.Name = String.Empty AndAlso item.Description = String.Empty Then
                        item.isDeleteMarked = True
                    Else
                        item.isDirty = True
                    End If
                Case Else
                    ' Si no existe y los campos estan llenos, se marca para insertar
                    If Not item.Name = String.Empty OrElse Not item.Description = String.Empty Then
                        item.CreationDate = Date.Now
                        item.CreatorUserCode = UserInfo.UserName
                        item.isNew = True
                    End If
            End Select

            ' Se procesa la información
            If item.isNew Then
                WidgetTrans.Insert(item)
            ElseIf item.isDirty Then
                WidgetTrans.Update(item)
            ElseIf item.isDeleteMarked Then
                WidgetTrans.Delete(item.ID, item.LanguageID)
            End If
        Next

    End Function



#End Region

#Region "Class WidgetFile"
    ''' <summary>
    ''' Se usa para llenar el combo FILE
    ''' </summary>
    ''' <remarks></remarks>
    Class WidgetFile
        Private _WidgetFileName As String
        Private _WidgetPath As String

        Property WidgetFileName() As String
            Get
                Return _WidgetFileName
            End Get
            Set(ByVal value As String)
                _WidgetFileName = value
            End Set
        End Property

        Property WidgetPath() As String
            Get
                Return _WidgetPath
            End Get
            Set(ByVal value As String)
                _WidgetPath = value
            End Set
        End Property

    End Class

#End Region

#Region "Class WidgetImage"
    ''' <summary>
    ''' Se utiliza para llenar el combo del ICONO
    ''' </summary>
    ''' <remarks></remarks>
    Class WidgetImage

        Private _ImageName As String = String.Empty
        Private _ImagePath As String = String.Empty

        Property ImageName() As String
            Get
                Return _ImageName
            End Get
            Set(ByVal value As String)
                _ImageName = value
            End Set
        End Property

        Property ImagePath() As String
            Get
                Return _ImagePath
            End Get
            Set(ByVal value As String)
                _ImagePath = value
            End Set
        End Property

    End Class
#End Region

#Region "Class Widget"

    Class Widget

#Region "Private fields, to hold the state of the entity"

        Private _ID As Integer = -1
        Private _Name As String = String.Empty
        Private _Url As String = String.Empty
        Private _Description As String = String.Empty
        Private _CreatedDate As Date = Date.MinValue
        Private _LastUpdate As Date = Date.MinValue
        Private _VersionNo As Integer = -1
        Private _IsDefault As Boolean = False
        Private _DefaultState As String = String.Empty
        Private _Icon As String = String.Empty
        Private _OrderNo As Integer = -1
        Private _IsAnonymouAllow As Boolean = False

        Protected _currentConnection As SqlConnection

        Private Shared _ConnectionString As String = "FrontOfficeConnectionString"

#End Region

#Region "Public properties, to expose the state of the entity"

        Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property

        Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Property Url() As String
            Get
                Return _Url
            End Get
            Set(ByVal value As String)
                _Url = value
            End Set
        End Property

        Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Property CreatedDate() As Date
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As Date)
                _CreatedDate = value
            End Set
        End Property

        Property LastUpdate() As Date
            Get
                Return _LastUpdate
            End Get
            Set(ByVal value As Date)
                _LastUpdate = value
            End Set
        End Property

        Property VersionNo() As Integer
            Get
                Return _VersionNo
            End Get
            Set(ByVal value As Integer)
                _VersionNo = value
            End Set
        End Property

        Property IsDefault() As Boolean
            Get
                Return _IsDefault
            End Get
            Set(ByVal value As Boolean)
                _IsDefault = value
            End Set
        End Property

        Property DefaultState() As String
            Get
                Return _DefaultState
            End Get
            Set(ByVal value As String)
                _DefaultState = value
            End Set
        End Property

        Property Icon() As String
            Get
                Return _Icon
            End Get
            Set(ByVal value As String)
                _Icon = value
            End Set
        End Property

        Property OrderNo() As Integer
            Get
                Return _OrderNo
            End Get
            Set(ByVal value As Integer)
                _OrderNo = value
            End Set
        End Property

        Property IsAnonymouAllow() As Boolean
            Get
                Return _IsAnonymouAllow
            End Get
            Set(ByVal value As Boolean)
                _IsAnonymouAllow = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a Connection that represents a unique session to a Server data source.
        ''' </summary>
        Public Property CurrentConnection() As SqlConnection
            Get
                Return _currentConnection
            End Get
            Set(ByVal value As SqlConnection)
                _currentConnection = value
            End Set
        End Property

#End Region

#Region "Constructors"

        Public Sub New()
            MyBase.New()
        End Sub

#End Region

#Region "Shared methods"

        Public Shared Function RetrieveAll() As DataTable
            Dim UserTemp As New InMotionGIT.Membership.Providers.MemberContext
            Dim result As DataTable = Nothing

            With New DataManagerFactory(String.Format("SELECT WIDGET.ID , WIDGETTRANS.NAME ""Name"", " &
                                                             "WIDGET.URL ""Url"", WIDGETTRANS.DESCRIPTION ""Description"", " &
                                                             "WIDGET.CREATEDDATE ""CreatedDate"", WIDGET.LASTUPDATE ""LastUpdate"", " &
                                                             "WIDGET.VERSIONNO ""VersionNo"", WIDGET.ISDEFAULT ""IsDefault"", " &
                                                             "WIDGET.DEFAULTSTATE ""DefaultState"", WIDGET.ICON ""Icon"", " &
                                                             "WIDGET.ORDERNO ""OrderNo"", WIDGET.ISANONYMOUALLOW ""IsAnonymouallow"", " &
                                                             "WIDGET.ICON ""IconEdit"" " &
                                                        "FROM WIDGET " &
                                                        "LEFT JOIN WIDGETTRANS " &
                                                          "ON WIDGETTRANS.ID = WIDGET.ID AND WIDGETTRANS.LANGUAGEID = {0}", UserTemp.CulturalNameToLanguageId()),
                                                                 "WIDGET", _ConnectionString)
                result = .QueryExecuteToTable
            End With

            Return result
        End Function

        Public Shared Function Insert(ByRef WidgetInstance As Widget) As Integer

            Dim Widget As New Widget
            Insert = Widget.int_Insert(WidgetInstance)
            Widget = Nothing

        End Function

        Public Shared Function Update(ByRef WidgetInstance As Widget) As Boolean

            Dim Widget As New Widget
            Update = Widget.int_Update(WidgetInstance)
            Widget = Nothing

        End Function

        Public Shared Function Delete(ByVal WidgetID As Integer) As Boolean

            Dim Widget As New Widget
            Delete = Widget.int_Delete(WidgetID)
            Widget = Nothing

        End Function

        'Public Shared Function Delete(ByVal WidgetsList As String) As Boolean
        '    Dim Widget As New Widget
        '    Delete = Widget.int_Delete(WidgetsList)
        '    Widget = Nothing
        'End Function

#End Region

#Region "Data access methods, for internal use"

        Function OpenConnection(Optional ByVal connectionString As String = "FrontOfficeConnectionString") As SqlConnection
            Dim currentConfig As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(connectionString)
            Dim connectionInstance As SqlConnection = Nothing

            If Not IsNothing(currentConfig) Then
                connectionInstance = New SqlConnection(currentConfig.ConnectionString)
                connectionInstance.Open()
            End If

            Return connectionInstance
        End Function

        Public Sub CloseConnection()
            _currentConnection.Close()
            _currentConnection = Nothing
        End Sub

        Function int_Insert(ByRef WidgetInstance As Widget) As Integer

            With New DataManagerFactory("SELECT NVL(MAX(ID),0)+1 FROM Widget", "Widget", "FrontOfficeConnectionString")
                WidgetInstance.ID = .QueryExecuteScalarToInteger
            End With

            If IsNothing(WidgetInstance.Name) Then
                WidgetInstance.Name = String.Empty
            End If

            If IsNothing(WidgetInstance.Description) Then
                WidgetInstance.Description = String.Empty
            End If

            If IsNothing(WidgetInstance.DefaultState) Then
                WidgetInstance.DefaultState = String.Empty
            End If

            With New DataManagerFactory("INSERT INTO Widget (ID, " &
                                                            " Name, " &
                                                            " Url, " &
                                                            " Description, " &
                                                            " CreatedDate, " &
                                                            " LastUpdate, " &
                                                            " VersionNo, " &
                                                            " IsDefault," &
                                                            " DefaultState, " &
                                                            " Icon," &
                                                            " OrderNo, " &
                                                            " IsAnonymouAllow) " &
                                                    "VALUES ( @:ID, " &
                                                            " @:Name, " &
                                                            " @:Url," &
                                                            " @:Description, " &
                                                            " @:CreatedDate, " &
                                                            " @:LastUpdate, " &
                                                            "   1, " &
                                                            "   0, " &
                                                            " @:DefaultState, " &
                                                            " @:Icon, " &
                                                            "   1, " &
                                                            " @:IsAnonymouAllow)",
                                        "Widget",
                                        "FrontOfficeConnectionString")

                .AddParameter("ID", DbType.Decimal, 0, False, WidgetInstance.ID)
                .AddParameter("Name", DbType.AnsiString, 255, False, WidgetInstance.Name)
                .AddParameter("Url", DbType.AnsiString, 255, False, WidgetInstance.Url)
                .AddParameter("Description", DbType.AnsiString, 255, False, WidgetInstance.Description)

                .AddParameter("CreatedDate", DbType.DateTime, 8, False, Date.Now)
                .AddParameter("LastUpdate", DbType.DateTime, 8, False, Date.Now)

                .AddParameter("DefaultState", DbType.AnsiStringFixedLength, 1000, False, WidgetInstance.DefaultState)
                .AddParameter("Icon", DbType.AnsiString, 255, False, WidgetInstance.Icon)
                .AddParameter("IsAnonymouAllow", DbType.Decimal, 1, False, IIf(WidgetInstance.IsAnonymouAllow, 1, 0))
                int_Insert = .CommandExecute()
            End With
            int_Insert = WidgetInstance.ID
        End Function

        Function int_Update(ByRef WidgetInstance As Widget) As Boolean
            Try

                With New DataManagerFactory(" UPDATE WIDGET " &
                                        " SET   NAME = @:NAME, " &
                                              " URL = @:URL, " &
                                              " DESCRIPTION = @:DESCRIPTION, " &
                                              " LASTUPDATE = SYSDATE, " &
                                              " VERSIONNO = @:VERSIONNO, " &
                                              " DEFAULTSTATE = @:DEFAULTSTATE, " &
                                              " ICON = @:ICON, " &
                                              " ORDERNO = @:ORDERNO " &
                                        " WHERE " &
                                             " ID = @:ID ",
                                     "WIDGET", "FrontOfficeConnectionString")

                    If IsNothing(WidgetInstance.Name) Then
                        WidgetInstance.Name = String.Empty
                    End If
                    .AddParameter("NAME", DbType.AnsiStringFixedLength, 255, False, WidgetInstance.Name)
                    .AddParameter("URL", DbType.AnsiStringFixedLength, 255, False, WidgetInstance.Url)

                    If IsNothing(WidgetInstance.Description) Then
                        WidgetInstance.Description = String.Empty
                    End If
                    .AddParameter("DESCRIPTION", DbType.AnsiStringFixedLength, 255, False, WidgetInstance.Description)

                    .AddParameter("VERSIONNO", DbType.Decimal, 5, False, WidgetInstance.VersionNo)

                    If IsNothing(WidgetInstance.DefaultState) Then
                        WidgetInstance.DefaultState = String.Empty
                    End If
                    .AddParameter("DEFAULTSTATE", DbType.AnsiStringFixedLength, 510, False, WidgetInstance.DefaultState)

                    .AddParameter("ICON", DbType.AnsiStringFixedLength, 255, False, WidgetInstance.Icon)

                    .AddParameter("ORDERNO", DbType.Decimal, 5, False, WidgetInstance.OrderNo)

                    .AddParameter("ID", DbType.Decimal, 5, False, WidgetInstance.ID)

                    .CommandExecute()
                End With

                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function

        Function int_Delete(ByVal WidgetID As Integer) As Boolean

            With New DataManagerFactory("DELETE FROM  WIDGETINSTANCETRANS WHERE WIDGETID = @:ID ",
                                       "WIDGET", "FrontOfficeConnectionString")
                .AddParameter("ID", DbType.Decimal, 5, False, WidgetID)
                .CommandExecute()
            End With

            With New DataManagerFactory("DELETE FROM WIDGETINSTANCE WHERE WIDGETID = @:ID",
                                     "WIDGETINSTANCE", "FrontOfficeConnectionString")
                .AddParameter("ID", DbType.Decimal, 5, False, WidgetID)
                .CommandExecute()
            End With

            With New DataManagerFactory("DELETE FROM WIDGETSINROLES WHERE WIDGETID = @:ID",
                                  "WIDGETINSTANCE", "FrontOfficeConnectionString")
                .AddParameter("ID", DbType.Decimal, 5, False, WidgetID)
                .CommandExecute()
            End With

            With New DataManagerFactory("DELETE FROM WIDGETTRANS WHERE ID = @:ID",
                                 "WIDGETINSTANCE", "FrontOfficeConnectionString")
                .AddParameter("ID", DbType.Decimal, 5, False, WidgetID)
                .CommandExecute()
            End With

            With New DataManagerFactory("DELETE FROM WIDGET WHERE ID = @:ID",
                               "WIDGETINSTANCE", "FrontOfficeConnectionString")
                .AddParameter("ID", DbType.Decimal, 5, False, WidgetID)
                .CommandExecute()
            End With

            int_Delete = True
        End Function

#End Region

    End Class

#End Region

#Region "Class WidgetTrans"

    Class WidgetTrans

#Region "Private fields, to hold the state of the entity"

        Private _ID As Integer = -1
        Private _LanguageID As Integer = -1
        Private _Name As String = String.Empty
        Private _Description As String = String.Empty
        Private _CreatorUserCode As String = String.Empty
        Private _CreationDate As Date = Date.MinValue
        Private _UpdateUserCode As String = String.Empty
        Private _UpdateDate As Date = Date.MinValue

        Private _isDeleteMarked As Boolean = False
        Private _isNew As Boolean = False
        Private _isDirty As Boolean = False

        Protected _currentConnection As SqlConnection

#End Region

#Region "Public properties, to expose the state of the entity"

        Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property

        Property LanguageID() As Integer
            Get
                Return _LanguageID
            End Get
            Set(ByVal value As Integer)
                _LanguageID = value
            End Set
        End Property

        Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Property CreatorUserCode() As String
            Get
                Return _CreatorUserCode
            End Get
            Set(ByVal value As String)
                _CreatorUserCode = value
            End Set
        End Property

        Property CreationDate() As Date
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Date)
                _CreationDate = value
            End Set
        End Property

        Property UpdateUserCode() As String
            Get
                Return _UpdateUserCode
            End Get
            Set(ByVal value As String)
                _UpdateUserCode = value
            End Set
        End Property

        Property UpdateDate() As Date
            Get
                Return _UpdateDate
            End Get
            Set(ByVal value As Date)
                _UpdateDate = value
            End Set
        End Property

        Property isDeleteMarked() As Boolean
            Get
                Return _isDeleteMarked
            End Get
            Set(ByVal value As Boolean)
                _isDeleteMarked = value
            End Set
        End Property

        Property isNew() As Boolean
            Get
                Return _isNew
            End Get
            Set(ByVal value As Boolean)
                _isNew = value
            End Set
        End Property

        Property isDirty() As Boolean
            Get
                Return _isDirty
            End Get
            Set(ByVal value As Boolean)
                _isDirty = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a Connection that represents a unique session to a Server data source.
        ''' </summary>
        Public Property CurrentConnection() As SqlConnection
            Get
                Return _currentConnection
            End Get
            Set(ByVal value As SqlConnection)
                _currentConnection = value
            End Set
        End Property

#End Region

#Region "Constructors"

        Public Sub New()
            MyBase.New()
        End Sub

#End Region

#Region "Shared methods"

        Public Shared Function RetrieveAll(ByVal ID As Integer) As DataTable
            Dim result As DataTable = Nothing

            With New DataManagerFactory(String.Format("SELECT ID,LANGUAGEID,NAME,DESCRIPTION,CREATORUSERCODE,CREATIONDATE,UPDATEUSERCODE,UPDATEDATE " &
                                                                                 "FROM WIDGETTRANS WHERE ID = {0}", ID), "WIDGETTRANS", "FrontOfficeConnectionString")
                result = .QueryExecuteToTable
            End With

            Return result
        End Function

        Public Shared Function RetrieveAllLanguages(ByVal WidgetID As Integer) As DataTable
            Dim UserTemp As New InMotionGIT.Membership.Providers.MemberContext
            Dim result As DataTable = Nothing
            If WidgetID >= 0 Then
                With New DataManagerFactory(String.Format("SELECT  LOOKUP.CODE  ""LanguageCode"", LOOKUP.DESCRIPTION ""LanguageDescription"", LOOKUP.DESCRIPTION  ""LanguageShortDescription"", " &
                                                                                       "WIDGETTRANS.NAME ""Name"", WIDGETTRANS.DESCRIPTION ""Description"", " &
                                                                                       "CASE  WHEN WIDGETTRANS.NAME = NULL AND  WIDGETTRANS.DESCRIPTION = NULL THEN  '0' ELSE '1'  END AS EXITS " &
                                                                               " FROM LOOKUP  " &
                                                                               " JOIN WIDGETTRANS " &
                                                                               " ON   LOOKUP.CODE = WIDGETTRANS.LANGUAGEID " &
                                                                               "WHERE WIDGETTRANS.ID = {0} AND LOOKUP.LOOKUPID = 1   AND LOOKUP.LANGUAGEID = {1} ORDER BY  LOOKUP.CODE ", WidgetID, UserTemp.CulturalNameToLanguageId), "WIDGETTRANS", "FrontOfficeConnectionString")
                    result = .QueryExecuteToTable
                End With
            Else
                Dim query As String = String.Format("  SELECT TABLANGUAGE.LANGUAGECODE ""LanguageCode"",  " &
                                    "         TRANSLANGUAGE.DESCRIPTION ""LanguageDescription"" " &
                                    "    FROM TABLANGUAGE " &
                                    "         LEFT JOIN TRANSLANGUAGE " &
                                    "            ON     TRANSLANGUAGE.LANGUAGECODE = TABLANGUAGE.LANGUAGECODE " &
                                    "               AND TRANSLANGUAGE.LANGUAGEID = {0} " &
                                    "   WHERE TABLANGUAGE.RECORDSTATUS = 1 " &
                                    " ORDER BY TABLANGUAGE.LANGUAGECODE ", UserTemp.LanguageId)
                With New DataManagerFactory(query, "WIDGETTRANS", "FrontOfficeConnectionString")
                    result = .QueryExecuteToTable(True)
                End With

                With result
                    .Columns.Add(New System.Data.DataColumn With {.ColumnName = "Name", .DataType = GetType(String), .Caption = "Name"})
                    .Columns.Add(New System.Data.DataColumn With {.ColumnName = "Description", .DataType = GetType(String), .Caption = "Description"})
                    .Columns.Add(New System.Data.DataColumn With {.ColumnName = "EXITS", .DataType = GetType(String), .Caption = "EXITS", .DefaultValue = "0"})
                End With
            End If

            Return result
        End Function

        Public Shared Function Insert(ByRef WidgetTransInstance As WidgetTrans) As Boolean

            Dim WidgetTrans As New WidgetTrans
            Insert = WidgetTrans.int_Insert(WidgetTransInstance)
            WidgetTrans = Nothing

        End Function

        Public Shared Function Update(ByRef WidgetTransInstance As WidgetTrans) As Boolean

            Dim WidgetTrans As New WidgetTrans
            Update = WidgetTrans.int_Update(WidgetTransInstance)
            WidgetTrans = Nothing

        End Function

        Public Shared Function Delete(ByVal ID As Integer, ByVal LanguageID As Integer) As Boolean
            Dim WidgetTrans As New WidgetTrans
            Delete = WidgetTrans.int_Delete(ID, LanguageID)
            WidgetTrans = Nothing
        End Function

        Public Shared Function Delete(ByVal ID As Integer) As Boolean
            Dim UserTemp As New InMotionGIT.Membership.Providers.MemberContext
            Dim WidgetTrans As New WidgetTrans
            Delete = WidgetTrans.int_Delete(ID, UserTemp.CulturalNameToLanguageId)
            WidgetTrans = Nothing

        End Function

        Public Shared Function DeleteWidgetInstanceAndWidgetInstanceTrance(ByVal ID As Integer) As Boolean

            Dim WidgetTrans As New WidgetTrans
            DeleteWidgetInstanceAndWidgetInstanceTrance = WidgetTrans.int_DeleteWidgetInstanceAndWidgetInstanceTrans(ID)
            WidgetTrans = Nothing

        End Function

        Public Shared Function IsExists(ByVal ID As Integer, ByVal LanguageID As Integer) As Boolean

            Dim WidgetTrans As New WidgetTrans
            IsExists = WidgetTrans.int_IsExists(ID, LanguageID)
            WidgetTrans = Nothing

        End Function

#End Region

#Region "Data access methods, for internal use"

        Function int_Insert(ByRef WidgetTransInstance As WidgetTrans) As Boolean
            Dim Result As Integer
            With New DataManagerFactory("INSERT INTO WIDGETTRANS ( ID, " &
                                                                 " LANGUAGEID, " &
                                                                 " NAME, " &
                                                                 " DESCRIPTION,  " &
                                                                 " CREATORUSERCODE, " &
                                                                 " CREATIONDATE, " &
                                                                 " UPDATEUSERCODE, " &
                                                                 " UPDATEDATE ) " &
                                                " VALUES " &
                                                            " (   @:ID , @:LANGUAGEID, @:NAME ,@:DESCRIPTION , " &
                                                                " @:CREATORUSERCODE ,SYSDATE ,@:UPDATEUSERCODE , SYSDATE) ",
                                         "WIDGETTRANS", "FrontOfficeConnectionString")
                .AddParameter("ID", DbType.Decimal, 5, False, WidgetTransInstance.ID)
                .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, WidgetTransInstance.LanguageID)
                .AddParameter("NAME", DbType.AnsiStringFixedLength, 255, False, WidgetTransInstance.Name)
                .AddParameter("DESCRIPTION", DbType.AnsiStringFixedLength, 255, False, WidgetTransInstance.Description)
                .AddParameter("CREATORUSERCODE", DbType.AnsiStringFixedLength, 30, False, WidgetTransInstance.CreatorUserCode)
                .AddParameter("UPDATEUSERCODE", DbType.AnsiStringFixedLength, 30, False, WidgetTransInstance.UpdateUserCode)
                .CommandExecute()
            End With
            Return True
        End Function

        Public Function IndexWidget() As Integer
            Dim result As Integer
            With New DataManagerFactory(" SELECT MAX(NVL( ID, 0)) + 1 FROM WIDGET ",
                                        "WIDGET", "FrontOfficeConnectionString")
                result = .QueryExecuteScalarToInteger()
            End With
            Return result
        End Function

        Function int_Update(ByRef WidgetTransInstance As WidgetTrans) As Boolean
            With New DataManagerFactory(" UPDATE WIDGETTRANS " &
                                        " SET NAME = @:NAME, " &
                                            " DESCRIPTION = @:DESCRIPTION, " &
                                            " UPDATEUSERCODE = @:UPDATEUSERCODE, " &
                                            " UPDATEDATE = SYSDATE " &
                                        " WHERE " &
                                            " ID = @:ID " &
                                            " AND LANGUAGEID = @:LANGUAGEID ", "WIDGET", "FrontOfficeConnectionString")
                .AddParameter("NAME", DbType.AnsiStringFixedLength, 255, False, WidgetTransInstance.Name)
                .AddParameter("DESCRIPTION", DbType.AnsiStringFixedLength, 255, False, WidgetTransInstance.Description)
                .AddParameter("UPDATEUSERCODE", DbType.AnsiStringFixedLength, 30, False, WidgetTransInstance.UpdateUserCode)
                .AddParameter("ID", DbType.Decimal, 5, False, WidgetTransInstance.ID)
                .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, WidgetTransInstance.LanguageID)

                .CommandExecute()
            End With
            Return True
        End Function

        Function int_DeleteWidgetInstanceAndWidgetInstanceTrans(ByVal ID As Integer) As Boolean
            Try
                With New DataManagerFactory("DELETE FROM " &
                                                       " WIDGETINSTANCETRANS " &
                                                 " WHERE " &
                                                       " WIDGETINSTANCETRANS.WIDGETID = @:ID",
                                         "Membership", "FrontOfficeConnectionString")

                    .AddParameter("ID", DbType.Decimal, 1, False, ID)
                    .CommandExecute()
                End With

                With New DataManagerFactory("DELETE FROM " &
                                                       " WIDGETINSTANCE " &
                                                 " WHERE " &
                                                       " WIDGETINSTANCE.WIDGETID = @:ID",
                                            "Membership", "FrontOfficeConnectionString")
                    .AddParameter("ID", DbType.Decimal, 1, False, ID)
                    .CommandExecute()
                End With

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function int_Delete(ByVal ID As Integer, ByVal LanguageID As Integer) As Boolean

            Dim result As Integer
            With New DataManagerFactory("DELETE FROM WIDGETTRANS WHERE ID = @:ID", "WIDGETTRANS", "FrontOfficeConnectionString")
                .AddParameter("ID", DbType.Decimal, 5, False, ID)
                .CommandExecute()
            End With
            If result > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function int_IsExists(ByVal ID As Integer, ByVal LanguageID As Integer) As Boolean
            Dim result As Integer
            With New DataManagerFactory(" SELECT  " +
                                        " 	COUNT(*) " +
                                        " FROM " +
                                        " 	WIDGETTRANS " +
                                        " WHERE " +
                                        " 	  ID = @:ID " +
                                        " AND LANGUAGEID = @:LANGUAGEID ",
                                        "WIDGETTRANS",
                                        "FrontOfficeConnectionString")
                .AddParameter("ID", DbType.Decimal, 5, False, ID)
                .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, LanguageID)
                result = .QueryExecuteScalarToInteger()
            End With
            If result > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

#End Region

    End Class

#End Region



End Class