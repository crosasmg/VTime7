Imports System.Data.Common
Imports DevExpress.Web.ASPxGridView
Imports System.ComponentModel
Imports GIT.EDW.Query.Model

Partial Class Controls_IntermedControl
    Inherits System.Web.UI.UserControl
    Implements Interfaces.IQueryUserControl

#Region "Private fields"

    Private _repositoryName As String = String.Empty

#End Region

#Region "Public properties"

    ''' <summary>
    ''' Propiedad publica para colocar el nombre del Intermediario en el user control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property Text() As String
        Get
            Return ButtonEditIntermed.Text
        End Get
        Set(ByVal value As String)
            ButtonEditIntermed.Text = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If IsNothing(DevExpress.Xpo.XpoDefault.DataLayer) Then
            GIT.EDW.Query.Model.XpoSupport.Init()
        End If
        XpoProducerControl.Session = New DevExpress.Xpo.Session
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Or String.IsNullOrEmpty(DsIntermedSeach.ConnectionString) Then
            'TODO:Don nelson levanta la tarea para migrar los sqlDatasource a datafactory

            Throw New NotImplementedException()

            'Dim connectionSetting As ConnectionStringSettings = GetConnectionString(_repositoryName)

            'If Not IsNothing(connectionSetting) Then
            '    With DsIntermedSeach
            '        .ConnectionString = connectionSetting.ConnectionString
            '        .ProviderName = connectionSetting.ProviderName
            '    End With
            'End If
        End If
    End Sub

#End Region

#Region "GridView Events"

    Protected Sub GridViewIntermed_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridViewIntermed.CustomCallback

        GridViewIntermed.JSProperties.Clear()
        GridViewIntermed.JSProperties.Add("cp_Bind_GridView", False)

        If e.Parameters = "CustomSearch" Then

            If ButtonEditIntermed.Text.Trim.Length > 0 Then

                GridViewIntermed.JSProperties.Clear()
                Dim IntermedDescript As String = GetIntermedFullName(ButtonEditIntermed.Text.Trim)
                GridViewIntermed.JSProperties.Add("cp_IntermedName", Trim(IntermedDescript))
                GridViewIntermed.JSProperties.Add("cp_ExistsIntermed", (IntermedDescript.Length > 0))
                LabelIntermed.Text = IntermedDescript

            End If

        End If

    End Sub

    Protected Sub GridViewIntermed_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridViewIntermed.HtmlRowCreated

        If e.RowType = GridViewRowType.Data Then
            e.Row.Attributes("nIntermed") = e.GetValue("NINTERMED") '.ToString()
            e.Row.Attributes("sCliename") = e.GetValue("SCLIENAME") '.ToString()
        End If

    End Sub

#End Region

#Region "Controls Events"

    Protected Sub ButtonEditIntermed_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEditIntermed.TextChanged

        If IsPostBack And Not Page.IsCallback Then
            If ButtonEditIntermed.Text.Length > 0 Then
                LabelIntermed.Text = GetIntermedFullName(ButtonEditIntermed.Text).Trim
            End If
        End If

    End Sub

    Protected Sub DsIntermedSeach_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DsIntermedSeach.Selected
        If Not e.Exception Is Nothing Then
            LabelIntermed.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub

#End Region

#Region "Methods"

    Function GetIntermedFullName(ByVal nIntermed As String) As String

        Dim provider As String = Me.DsIntermedSeach.ProviderName
        Dim connectionString As String = Me.DsIntermedSeach.ConnectionString
        Dim dbpf As DbProviderFactory = DbProviderFactories.GetFactory(provider)
        Dim dbcon As DbConnection = dbpf.CreateConnection()
        Dim dbcmd As DbCommand = dbpf.CreateCommand
        dbcon.ConnectionString = connectionString
        dbcon.Open()
        dbcmd.Connection = dbcon
        dbcmd.CommandText = Me.DsIntermedSeach.SelectCommand.Replace(":NINTERMED", nIntermed)
        GetIntermedFullName = dbcmd.ExecuteScalar
        dbcon.Close()

        If IsNothing(GetIntermedFullName) Then
            GetIntermedFullName = String.Empty
        End If

        Return GetIntermedFullName

    End Function

#End Region

#Region "IQueryUserControl Implement"

    Public Property ControlID As String Implements Interfaces.IQueryUserControl.ControlID
        Get
            Return Me.ID
        End Get
        Set(ByVal value As String)
            Me.ID = value
            ButtonEditIntermed.ClientInstanceName = value
        End Set
    End Property

    Public Property Repository As String Implements Interfaces.IQueryUserControl.Repository
        Get
            Return _repositoryName
        End Get
        Set(ByVal value As String)
            _repositoryName = value
        End Set
    End Property

    Public Property ToolTip As String Implements Interfaces.IQueryUserControl.ToolTip
        Get
            Return ButtonEditIntermed.ToolTip
        End Get
        Set(ByVal value As String)
            ButtonEditIntermed.ToolTip = value
        End Set
    End Property

    Public Property Value As Object Implements Interfaces.IQueryUserControl.Value
        Get
            Return Me.Text
        End Get
        Set(ByVal value As Object)
            Me.Text = value
        End Set
    End Property

    Public Property Enabled1 As Boolean Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Enabled
        Get
            Return ButtonEditIntermed.ClientEnabled
        End Get

        Set(value As Boolean)
            ButtonEditIntermed.ClientEnabled = value
        End Set
    End Property

    Public Property Script As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Script
        Get
            Return String.Empty
        End Get
        Set(value As String)

        End Set
    End Property

#End Region

End Class
