Imports DevExpress.Web.ASPxEditors
Imports InMotionGIT.Common.Proxy
Imports GIT.EDW.Query.Model
Imports System.Globalization
Imports System.Threading
Imports DevExpress.Web.ASPxClasses
Imports System.ComponentModel

Partial Class Controls_ClientControl
    Inherits System.Web.UI.UserControl
    Implements Interfaces.IQueryUserControl

#Region "Private fields"

    Private _repositoryName As String = String.Empty

#End Region

#Region "Public Events"

    Public Event GetClientTextChanged()

#End Region

#Region "Public properties for userControl"

    ''' <summary>
    ''' Propiedad publica para colocar el nombre del cliente en el user control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property Text() As String
        Get
            Return ClientIdComboBox.Value
        End Get
        Set(ByVal value As String)
            ClientIdComboBox.Value = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return ClientIdComboBox.ClientEnabled
        End Get
        Set(ByVal value As Boolean)
            ClientIdComboBox.ClientEnabled = value
        End Set
    End Property

    Public Property IsAllowSearch() As Boolean
        Get
            Return ClientIdComboBox.ClientEnabled
        End Get

        Set(ByVal value As Boolean)
            ClientIdComboBox.ClientEnabled = value
        End Set
    End Property

    Public Property NullText() As String
        Get
            Return ClientIdComboBox.NullText
        End Get
        Set(ByVal value As String)
            ClientIdComboBox.NullText = value
        End Set
    End Property

#End Region

#Region "Public properties for validation userControl"

    Public Property PaddingLeft() As Unit
        Get
            Return Nothing
        End Get

        Set(ByVal value As Unit)
        End Set
    End Property

    Public Property HorizontalPositionImage() As String
        Get
            Return String.Empty
        End Get

        Set(ByVal value As String)
        End Set
    End Property

    Public Property ImageUrl() As String
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Property RepeatImage() As BackgroundImageRepeat
        Get
            Return BackgroundImageRepeat.NoRepeat
        End Get
        Set(ByVal value As BackgroundImageRepeat)
        End Set
    End Property

    Public Property VerticalPositionImage() As String
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Property ErrorDisplayMode() As ErrorDisplayMode
        Get
            Return DevExpress.Web.ASPxEditors.ErrorDisplayMode.None
        End Get
        Set(ByVal value As ErrorDisplayMode)
        End Set
    End Property

    Public Property IsRequired() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
        End Set
    End Property

    Public Property ErrorText() As String
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
        End Set
    End Property

#End Region

    Protected Sub ClientIdComboBox_ItemsRequestedByFilterCondition(source As Object, e As DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs) Handles ClientIdComboBox.ItemsRequestedByFilterCondition
        Dim ShortDatePattern As String = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern

        Dim sql As String = String.Format("SELECT SCLIENT, SCLIENAME, SBIRTHDAT" &
                                   " FROM (SELECT SCLIENT, RTRIM(SCLIENAME) AS SCLIENAME, TO_CHAR(DBIRTHDAT, '" & ShortDatePattern & "') AS SBIRTHDAT, ROW_NUMBER() OVER (ORDER BY SCLIENAME) ROW_NUM " &
                                           " FROM INSUDB.CLIENT" &
                                          " WHERE %FILTER%) Result" &
                                  " WHERE Row_Num BETWEEN {0} and {1}", e.BeginIndex + 1, e.EndIndex + 1)
        If String.IsNullOrEmpty(e.Filter) Then
            sql = sql.Replace("%FILTER%", "sCliename IS NOT NULL")
        Else
            Dim Filter As String = e.Filter.Trim
            Dim isNumber As Boolean = IsNumeric(Filter.Replace("%", String.Empty))

            If Filter.IndexOf("%") = -1 Then
                Filter = String.Format("%{0}%", Filter)
            End If
            If isNumber Then
                sql = sql.Replace("%FILTER%", String.Format("(SCLIENT LIKE '{0}')", Filter))
            Else
                sql = sql.Replace("%FILTER%", String.Format("(LOWER(SCLIENAME) LIKE LOWER('%{0}%'))", Filter))
            End If

        End If

        Dim clientCbo As ASPxComboBox = DirectCast(source, ASPxComboBox)
        With New DataManagerFactory(sql, "Client", "BackOfficeConnectionString")
            clientCbo.DataSource = .QueryExecuteToTable(True)
            clientCbo.DataBind()
        End With

    End Sub

    Protected Sub ClientIdComboBox_ItemRequestedByValue(source As Object, e As DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs) Handles ClientIdComboBox.ItemRequestedByValue
        If Not String.IsNullOrEmpty(e.Value) Then
            With DirectCast(source, ASPxComboBox)
                Dim value As String = e.Value
                If value.IsNotEmpty Then
                    Dim result As System.Data.DataTable
                    value = value.PadLeft(14, "0")


                    With New DataManagerFactory(String.Format("SELECT SCLIENT, RTRIM(SCLIENAME) AS SCLIENAME, TO_CHAR(DBIRTHDAT, 'dd/mm/yyyy') SBIRTHDAT FROM insudb.client WHERE SCLIENT='{0}'", value), "Client", "BackOfficeConnectionString")
                        result = .QueryExecuteToTable(True)
                    End With

                    .DataSource = result
                    .DataBind()
                End If
            End With
        End If
    End Sub

#Region "IQueryUserControl Implement"

    Public Property ControlID As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.ControlID
        Get
            Return Me.ID
        End Get
        Set(ByVal value As String)
            Me.ID = value
            ClientIdComboBox.ClientInstanceName = value
        End Set
    End Property

    Public Property Repository As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Repository
        Get
            Return _repositoryName
        End Get
        Set(ByVal value As String)
            _repositoryName = value
        End Set
    End Property

    Public Property ToolTip As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.ToolTip
        Get
            Return ClientIdComboBox.ToolTip
        End Get
        Set(ByVal value As String)
            ClientIdComboBox.ToolTip = value
        End Set
    End Property

    Public Property Value As Object Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Value
        Get
            Return ClientIdComboBox.Value
        End Get
        Set(ByVal value As Object)
            If Not IsNothing(value) AndAlso Not String.IsNullOrEmpty(value) Then
                ClientIdComboBox.Value = value
                Dim x = value
                Dim item As ListEditItem = ClientIdComboBox.Items.FindByValue(value)
                If item.IsNotEmpty Then
                    item.Selected = True
                End If
            End If
        End Set
    End Property

    Public Property Enabled1 As Boolean Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Enabled
        Get
            Return ClientIdComboBox.ClientEnabled
        End Get

        Set(value As Boolean)
            ClientIdComboBox.ClientEnabled = value
        End Set
    End Property

    Public Property Script As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Script
        Get
            Return ClientIdComboBox.ClientSideEvents.SelectedIndexChanged
        End Get
        Set(value As String)
            ClientIdComboBox.ClientSideEvents.SelectedIndexChanged = value
        End Set
    End Property

#End Region


End Class
