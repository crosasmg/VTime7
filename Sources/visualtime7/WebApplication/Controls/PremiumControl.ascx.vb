Imports System.Data.Common
Imports DevExpress.Web.ASPxGridView
Imports System.ComponentModel
Imports GIT.Core.Helpers.DataAccess
Imports GIT.EDW.Query.Model

Partial Class Controls_PremiumControl
    Inherits System.Web.UI.UserControl
    Implements Interfaces.IQueryUserControl

#Region "Private fields"

    Private _repositoryName As String = String.Empty

#End Region

#Region "Public properties"

    ''' <summary>
    ''' Propiedad publica para colocar la descripción del recibo en el user control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property Text() As String
        Get
            Return ButtonEditPremium.Text
        End Get
        Set(ByVal value As String)
            ButtonEditPremium.Text = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If IsNothing(DevExpress.Xpo.XpoDefault.DataLayer) Then
            GIT.EDW.Query.Model.XpoSupport.Init()
        End If
        XpoPremiumControl.Session = New DevExpress.Xpo.Session
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Throw New NotImplementedException
        'If Not IsPostBack Or String.IsNullOrEmpty(DsPremiumSeach.ConnectionString) Then

        '    Dim connectionSetting As ConnectionStringSettings = GetConnectionString(_repositoryName)

        '    If Not IsNothing(connectionSetting) Then
        '        With DsPremiumSeach
        '            .ConnectionString = connectionSetting.ConnectionString
        '            .ProviderName = connectionSetting.ProviderName
        '        End With
        '    End If
        'End If
    End Sub

#End Region

#Region "GridView Events"

    Protected Sub GridViewPremium_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridViewPremium.CustomCallback

        GridViewPremium.JSProperties.Clear()

        If e.Parameters = "CustomSearch" Then

            If ButtonEditPremium.Text.Trim.Length > 0 Then
                GridViewPremium.JSProperties.Clear()
                GridViewPremium.JSProperties.Add("cp_Receipt", ButtonEditPremium.Text.Trim())
                GridViewPremium.JSProperties.Add("cp_ExistsClient", Me.IsExist(ButtonEditPremium.Text.Trim()))
            End If

        End If

    End Sub

    Protected Sub GridViewClient_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridViewPremium.HtmlRowCreated
        If e.RowType = GridViewRowType.Data Then
            e.Row.Attributes("sCertype") = e.GetValue("SCERTYPE")
            e.Row.Attributes("sReceipt") = e.GetValue("NRECEIPT")
            e.Row.Attributes("sBranch") = e.GetValue("NBRANCH")
            e.Row.Attributes("sProduct") = e.GetValue("NPRODUCT")
            e.Row.Attributes("sDigit") = e.GetValue("NDIGIT")
            e.Row.Attributes("sPaynumbe") = e.GetValue("NPAYNUMBE")
        End If
    End Sub

#End Region

#Region "Controls Events"

    Protected Sub ButtonEditPremium_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEditPremium.TextChanged
        If IsPostBack And Not Page.IsCallback Then
            If ButtonEditPremium.Text.Length > 0 Then
                LabelPremium.Text = ""
            End If
        End If
    End Sub

    Protected Sub DsPremiumSeach_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DsPremiumSeach.Selected
        If Not e.Exception Is Nothing Then
            LabelPremium.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub

#End Region

#Region "Methods"

    Private Function IsExist(ByVal nReceipt As String) As Boolean
        Dim _isExists As Boolean = False
        Dim provider As String = DsPremiumSeach.ProviderName
        Dim connectionString As String = DsPremiumSeach.ConnectionString
        Dim dbpf As DbProviderFactory = DbProviderFactories.GetFactory(provider)
        Dim dbcon As DbConnection = dbpf.CreateConnection()
        Dim dbcmd As DbCommand = dbpf.CreateCommand
        Dim _reader As DbDataReader = Nothing
        dbcon.ConnectionString = connectionString
        dbcon.Open()
        dbcmd.Connection = dbcon
        dbcmd.CommandText = "select * from INSUDB.GCV_PREMIUMCONTROL prem " + _
                            "where PREM.NRECEIPT = " + nReceipt
        _reader = dbcmd.ExecuteReader()

        If _reader.Read() Then
            _isExists = True
        End If

        dbcon.Close()

        Return _isExists
    End Function

#End Region

#Region "IQueryUserControl Implement"

    Public Property ControlID As String Implements Interfaces.IQueryUserControl.ControlID
        Get
            Return Me.ID
        End Get
        Set(ByVal value As String)
            Me.ID = value
            ButtonEditPremium.ClientInstanceName = value
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
            Return ButtonEditPremium.ToolTip
        End Get
        Set(ByVal value As String)
            ButtonEditPremium.ToolTip = value
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
            Return ButtonEditPremium.ClientEnabled
        End Get

        Set(value As Boolean)
            ButtonEditPremium.ClientEnabled = value
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
