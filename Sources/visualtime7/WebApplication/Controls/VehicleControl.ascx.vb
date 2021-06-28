Imports DevExpress.Web.ASPxGridView
Imports System.Data.Common
Imports System.ComponentModel
Imports GIT.Core.Helpers.DataAccess
Imports GIT.EDW.Query.Model

Partial Class Controls_VehicleControl
    Inherits System.Web.UI.UserControl
    Implements Interfaces.IQueryUserControl

#Region "Private fields"

    Private _repositoryName As String = String.Empty

#End Region

#Region "Public properties"

    ''' <summary>
    ''' Propiedad publica para colocar el nombre del cliente en el user control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property Text() As String
        Get
            Return ButtonEditAuto.Text
        End Get
        Set(ByVal value As String)
            ButtonEditAuto.Text = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If IsNothing(DevExpress.Xpo.XpoDefault.DataLayer) Then
            GIT.EDW.Query.Model.XpoSupport.Init()
        End If
        XpoAutoControl.Session = New DevExpress.Xpo.Session
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Or String.IsNullOrEmpty(DsAutoSearch.ConnectionString) Then


            'TODO:Don nelson levanta la tarea para migrar los sqlDatasource a data factory

            Throw New NotImplementedException()

            'Dim connectionSetting As ConnectionStringSettings = GetConnectionString(_repositoryName)

            'If Not IsNothing(connectionSetting) Then
            '    With DsAutoSearch
            '        .ConnectionString = connectionSetting.ConnectionString
            '        .ProviderName = connectionSetting.ProviderName
            '    End With
            'End If
        End If
    End Sub

#End Region

#Region "GridView Events"

    Protected Sub GridViewAuto_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridViewAuto.CustomCallback

        If e.Parameters = "CustomSearch" Then

            If ButtonEditAuto.Text.Trim.Length > 0 Then

                GridViewAuto.JSProperties.Clear()
                Dim AutoDescript As String = GetAutoDescript(ButtonEditAuto.Text.Trim)
                GridViewAuto.JSProperties.Add("cp_Auto", Trim(AutoDescript))
                GridViewAuto.JSProperties.Add("cp_ExistsAuto", (AutoDescript.Length > 0))

            End If

        End If

    End Sub

    Protected Sub GridViewAuto_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridViewAuto.HtmlRowCreated

        If e.RowType = GridViewRowType.Data Then
            e.Row.Attributes("SREGIST") = e.GetValue("SREGIST")
            e.Row.Attributes("AUTODESCRIPT") = e.GetValue("AUTODESCRIPT")
        End If

    End Sub

#End Region

#Region "Controls Events"

    Protected Sub ButtonEditAuto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEditAuto.TextChanged

        If IsPostBack And Not Page.IsCallback Then
            If ButtonEditAuto.Text.Length > 0 Then
                LabelAutoDescript.Text = GetAutoDescript(ButtonEditAuto.Text.Trim).Trim
            End If
        End If

    End Sub

    Protected Sub DsAutoSearch_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DsAutoSearch.Selected
        If Not e.Exception Is Nothing Then
            LabelAutoDescript.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub

#End Region

#Region "Methods"

    Function GetAutoDescript(ByVal sRegist As String) As String

        Dim provider As String = Me.DsAutoSearch.ProviderName
        Dim connectionString As String = Me.DsAutoSearch.ConnectionString
        Dim dbpf As DbProviderFactory = DbProviderFactories.GetFactory(provider)
        Dim dbcon As DbConnection = dbpf.CreateConnection()
        Dim dbcmd As DbCommand = dbpf.CreateCommand
        dbcon.ConnectionString = connectionString
        dbcon.Open()
        dbcmd.Connection = dbcon
        dbcmd.CommandText = DsAutoSearch.SelectCommand.Replace(":SREGIST", String.Format("'{0}'", sRegist))
        GetAutoDescript = dbcmd.ExecuteScalar
        dbcon.Close()

        If IsNothing(GetAutoDescript) Then
            GetAutoDescript = String.Empty
        End If

        Return GetAutoDescript

    End Function

#End Region

#Region "IQueryUserControl Implement"

    Public Property ControlID As String Implements Interfaces.IQueryUserControl.ControlID
        Get
            Return Me.ID
        End Get
        Set(ByVal value As String)
            Me.ID = value
            ButtonEditAuto.ClientInstanceName = value
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
            Return ButtonEditAuto.ToolTip
        End Get
        Set(ByVal value As String)
            ButtonEditAuto.ToolTip = value
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
            Return ButtonEditAuto.ClientEnabled
        End Get

        Set(value As Boolean)
            ButtonEditAuto.ClientEnabled = value
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
