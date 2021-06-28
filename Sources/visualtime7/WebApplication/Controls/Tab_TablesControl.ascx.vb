Imports System.Data.Common
Imports DevExpress.Web.ASPxGridView
Imports System.ComponentModel

Partial Class Controls_Tab_TablesControl
    Inherits System.Web.UI.UserControl

    Protected Sub GridViewTab_TablesControl_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridViewTab_TablesControl.CustomCallback

        If e.Parameters = "CustomSearch" Then

            If ButtonEditTab_TablesControl.Text.Trim.Length > 0 Then

                GridViewTab_TablesControl.JSProperties.Clear()
                Dim Tab_TablesControlDescript As String = GetTab_TablesControlFullName(ButtonEditTab_TablesControl.Text.Trim)
                GridViewTab_TablesControl.JSProperties.Add("cp_Tab_TablesControlName", Trim(Tab_TablesControlDescript))
                GridViewTab_TablesControl.JSProperties.Add("cp_ExistsTab_TablesControl", (Tab_TablesControlDescript.Length > 0))
                LabelTab_TablesControl.Text = Tab_TablesControlDescript

            End If

        End If

    End Sub

    Protected Sub GridViewTab_TablesControl_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridViewTab_TablesControl.HtmlRowCreated

        If e.RowType = GridViewRowType.Data Then
            e.Row.Attributes("nCode") = e.GetValue("NCODE").ToString()
            e.Row.Attributes("sDescript") = e.GetValue("SDESCRIPT").ToString()
        End If

    End Sub

    Function GetTab_TablesControlFullName(ByVal nCode As String) As String
        Dim sQuotes As String = String.Empty
        Dim provider As String = Me.DsTab_TablesControlSeach.ProviderName
        Dim connectionString As String = Me.DsTab_TablesControlSeach.ConnectionString
        Dim dbpf As DbProviderFactory = DbProviderFactories.GetFactory(provider)
        Dim dbcon As DbConnection = dbpf.CreateConnection()
        Dim dbcmd As DbCommand = dbpf.CreateCommand
        dbcon.ConnectionString = connectionString
        dbcon.Open()
        dbcmd.Connection = dbcon
        If KeyField.ToUpper.StartsWith("S") Then
            sQuotes = "'"
        End If
        dbcmd.CommandText = "SELECT SDESCRIPT FROM " & TableName & " WHERE " & KeyField & " = " & sQuotes & nCode & sQuotes
        GetTab_TablesControlFullName = dbcmd.ExecuteScalar
        dbcon.Close()

        If IsNothing(GetTab_TablesControlFullName) Then
            GetTab_TablesControlFullName = String.Empty
        End If

        Return GetTab_TablesControlFullName

    End Function

    ''' <summary>
    ''' Propiedad publica para colocar la descripcion del registro en el user control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property Text() As String
        Get
            Return ButtonEditTab_TablesControl.Text
        End Get
        Set(ByVal value As String)
            ButtonEditTab_TablesControl.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad publica para colocar el nombre de la tabla a usar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property TableName() As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad publica para colocar el nombre del campo clave de la tabla a usar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property KeyField() As String
        Get
            Return _KeyField
        End Get
        Set(ByVal value As String)
            _KeyField = value
        End Set
    End Property

    Private _KeyField As String
    Private _TableName As String


    Protected Sub ButtonEditTab_TablesControl_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEditTab_TablesControl.TextChanged

        If IsPostBack And Not Page.IsCallback Then
            If ButtonEditTab_TablesControl.Text.Length > 0 Then
                LabelTab_TablesControl.Text = GetTab_TablesControlFullName(ButtonEditTab_TablesControl.Text).Trim
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.DsTab_TablesControl.SelectCommand = "SELECT " & KeyField & " AS NCODE, SDESCRIPT FROM " & TableName & " WHERE SSTATREGT = '1' ORDER BY SDESCRIPT"
    End Sub

    Protected Sub DsTab_TablesControl_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DsTab_TablesControl.Selected
        If Not e.Exception Is Nothing Then
            LabelTab_TablesControl.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub DsTab_TablesControlSeach_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DsTab_TablesControlSeach.Selected
        If Not e.Exception Is Nothing Then
            LabelTab_TablesControl.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub
End Class
