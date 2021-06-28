Imports System.Data.Common
Imports DevExpress.Web.ASPxGridView
Imports System.ComponentModel
Imports GIT.Core.Helpers.DataAccess
Imports GIT.EDW.Query.Model

Partial Class Controls_ProviderControl
    Inherits System.Web.UI.UserControl
    Implements Interfaces.IQueryUserControl

#Region "Private fields"

    Private _repositoryName As String = String.Empty

#End Region

#Region "Public properties"

    ''' <summary>
    ''' Propiedad publica para colocar el nombre del Provideriario en el user control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property Text() As String
        Get
            Return ButtonEditProviderControl.Text
        End Get
        Set(ByVal value As String)
            ButtonEditProviderControl.Text = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If IsNothing(DevExpress.Xpo.XpoDefault.DataLayer) Then
            GIT.EDW.Query.Model.XpoSupport.Init()
        End If
        XpoProviderControl.Session = New DevExpress.Xpo.Session
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Throw New NotImplementedException

        'If Not IsPostBack Or String.IsNullOrEmpty(DsProviderSeach.ConnectionString) Then

            
        '    Dim connectionSetting As ConnectionStringSettings = GetConnectionString(_repositoryName)

        '    If Not IsNothing(connectionSetting) Then
        '        With DsProviderSeach
        '            .ConnectionString = connectionSetting.ConnectionString
        '            .ProviderName = connectionSetting.ProviderName
        '        End With
        '    End If
        'End If
    End Sub

#End Region

#Region "GridView Events"

    Protected Sub GridViewProviderControl_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridViewProviderControl.CustomCallback

        If e.Parameters = "CustomSearch" Then

            If ButtonEditProviderControl.Text.Trim.Length > 0 Then

                GridViewProviderControl.JSProperties.Clear()
                Dim ProviderDescript As String = GetProviderFullName(ButtonEditProviderControl.Text.Trim)
                GridViewProviderControl.JSProperties.Add("cp_ProviderName", Trim(ProviderDescript))
                GridViewProviderControl.JSProperties.Add("cp_ExistsProvider", (ProviderDescript.Length > 0))
                LabelProviderControl.Text = ProviderDescript

            End If

        End If

    End Sub

    Protected Sub GridViewProviderControl_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridViewProviderControl.HtmlRowCreated

        If e.RowType = GridViewRowType.Data Then
            e.Row.Attributes("nProvider") = e.GetValue("NPROVIDER")
            e.Row.Attributes("sCliename") = e.GetValue("SCLIENAME")
        End If

    End Sub

#End Region

#Region "Controls Events"

    Protected Sub ButtonEditProviderControl_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEditProviderControl.TextChanged

        If IsPostBack And Not Page.IsCallback Then
            If ButtonEditProviderControl.Text.Length > 0 Then
                LabelProviderControl.Text = GetProviderFullName(ButtonEditProviderControl.Text).Trim
            End If
        End If

    End Sub

    Protected Sub DsProviderSeach_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DsProviderSeach.Selected
        If Not e.Exception Is Nothing Then
            LabelProviderControl.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub

#End Region

#Region "Methods"

    Function GetProviderFullName(ByVal nProvider As String) As String

        Dim provider As String = Me.DsProviderSeach.ProviderName
        Dim connectionString As String = Me.DsProviderSeach.ConnectionString
        Dim dbpf As DbProviderFactory = DbProviderFactories.GetFactory(provider)
        Dim dbcon As DbConnection = dbpf.CreateConnection()
        Dim dbcmd As DbCommand = dbpf.CreateCommand
        dbcon.ConnectionString = connectionString
        dbcon.Open()
        dbcmd.Connection = dbcon
        dbcmd.CommandText = Me.DsProviderSeach.SelectCommand.Replace(":NProvider", nProvider)
        GetProviderFullName = dbcmd.ExecuteScalar
        dbcon.Close()

        If IsNothing(GetProviderFullName) Then
            GetProviderFullName = String.Empty
        End If

        Return GetProviderFullName

    End Function

#End Region

#Region "IQueryUserControl Implement"

    Public Property ControlID As String Implements Interfaces.IQueryUserControl.ControlID
        Get
            Return Me.ID
        End Get
        Set(ByVal value As String)
            Me.ID = value
            ButtonEditProviderControl.ClientInstanceName = value
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
            Return ButtonEditProviderControl.ToolTip
        End Get
        Set(ByVal value As String)
            ButtonEditProviderControl.ToolTip = value
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
            Return ButtonEditProviderControl.ClientEnabled
        End Get

        Set(value As Boolean)
            ButtonEditProviderControl.ClientEnabled = value
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
