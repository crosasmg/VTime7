Imports System.Data.Common
Imports DevExpress.Web.ASPxGridView
Imports System.ComponentModel
Imports GIT.EDW.Query.Model

Partial Class Controls_RiskAddressControl
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
            Return ButtonEditRiskAddress.Text
        End Get
        Set(ByVal value As String)
            ButtonEditRiskAddress.Text = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If IsNothing(DevExpress.Xpo.XpoDefault.DataLayer) Then
            GIT.EDW.Query.Model.XpoSupport.Init()
        End If
        XpoRiskAddressControl.Session = New DevExpress.Xpo.Session
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Or String.IsNullOrEmpty(DSRiskAddressSearch.ConnectionString) Then

            'TODO:Don nelson levanta la tarea para migrar los sqlDatasource a data factory

            Throw New NotImplementedException()

            'Dim connectionSetting As ConnectionStringSettings = GetConnectionString(_repositoryName)

            'If Not IsNothing(connectionSetting) Then
            '    With DSRiskAddressSearch
            '        .ConnectionString = connectionSetting.ConnectionString
            '        .ProviderName = connectionSetting.ProviderName
            '    End With
            'End If
        End If
    End Sub

#End Region

#Region "GridView Events"

    Protected Sub GridViewRiskAddress_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridViewRiskAddress.CustomCallback

        If e.Parameters = "CustomSearch" Then

            If ButtonEditRiskAddress.Text.Trim.Length > 0 Then

                GridViewRiskAddress.JSProperties.Clear()
                Dim RiskAddress As String = GetRiskAddress(ButtonEditRiskAddress.Text.Trim)
                GridViewRiskAddress.JSProperties.Add("cp_RiskAddress", Trim(RiskAddress))
                GridViewRiskAddress.JSProperties.Add("cp_ExistsRiskAddress", (RiskAddress.Length > 0))

            End If

        End If

    End Sub

    Protected Sub GridViewRiskAddress_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridViewRiskAddress.HtmlRowCreated

        If e.RowType = GridViewRowType.Data Then
            e.Row.Attributes("nPolicy") = e.GetValue("NPOLICY")
            e.Row.Attributes("sDescAdd") = e.GetValue("SDESCADD")
        End If

    End Sub

#End Region

#Region "Controls Events"

    Protected Sub ButtonEditRiskAddress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEditRiskAddress.TextChanged

        If IsPostBack And Not Page.IsCallback Then
            If ButtonEditRiskAddress.Text.Length > 0 Then
                LabelRiskAddress.Text = GetRiskAddress(ButtonEditRiskAddress.Text).Trim
            End If
        End If

    End Sub

    Protected Sub DSRiskAddressSearch_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DSRiskAddressSearch.Selected
        If Not e.Exception Is Nothing Then
            LabelRiskAddress.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub

#End Region

#Region "Methods"

    Function GetRiskAddress(ByVal nPolicy As Long) As String

        Dim provider As String = DSRiskAddressSearch.ProviderName
        Dim connectionString As String = DSRiskAddressSearch.ConnectionString
        Dim dbpf As DbProviderFactory = DbProviderFactories.GetFactory(provider)
        Dim dbcon As DbConnection = dbpf.CreateConnection()
        Dim dbcmd As DbCommand = dbpf.CreateCommand
        dbcon.ConnectionString = connectionString
        dbcon.Open()
        dbcmd.Connection = dbcon
        dbcmd.CommandText = DSRiskAddressSearch.SelectCommand.Replace(":NPOLICY", nPolicy)
        GetRiskAddress = dbcmd.ExecuteScalar
        dbcon.Close()

        If IsNothing(GetRiskAddress) Then
            GetRiskAddress = String.Empty
        End If

        Return GetRiskAddress

    End Function

#End Region

#Region "IQueryUserControl Implement"

    Public Property ControlID As String Implements Interfaces.IQueryUserControl.ControlID
        Get
            Return Me.ID
        End Get
        Set(ByVal value As String)
            Me.ID = value
            ButtonEditRiskAddress.ClientInstanceName = value
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
            Return ButtonEditRiskAddress.ToolTip
        End Get
        Set(ByVal value As String)
            ButtonEditRiskAddress.ToolTip = value
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
            Return ButtonEditRiskAddress.ClientEnabled
        End Get

        Set(value As Boolean)
            ButtonEditRiskAddress.ClientEnabled = value
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
