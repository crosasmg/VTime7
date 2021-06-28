Imports System.Data.Common
Imports DevExpress.Web.ASPxGridView
Imports System.ComponentModel

Partial Class Controls_Tab_Tables
    Inherits System.Web.UI.UserControl

    Protected Sub GridViewTab_Tables_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles GridViewTab_Tables.CustomCallback

        If e.Parameters = "CustomSearch" Then

            If ButtonEditTab_Tables.Text.Trim.Length > 0 Then

                GridViewTab_Tables.JSProperties.Clear()
                Dim Tab_TablesDescript As String = GetTab_TablesFullName(ButtonEditTab_Tables.Text.Trim)
                GridViewTab_Tables.JSProperties.Add("cp_Tab_TablesName", Trim(Tab_TablesDescript))
                GridViewTab_Tables.JSProperties.Add("cp_ExistsTab_Tables", (Tab_TablesDescript.Length > 0))
                LabelTab_Tables.Text = Tab_TablesDescript

            End If

        End If

    End Sub

    Protected Sub GridViewTab_Tables_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridViewTab_Tables.HtmlRowCreated

        If e.RowType = GridViewRowType.Data Then
            e.Row.Attributes("nCode") = e.GetValue("NCODE").ToString()
            e.Row.Attributes("sDescript") = e.GetValue("SDESCRIPT").ToString()
        End If

    End Sub

    Function GetTab_TablesFullName(ByVal nCode As String) As String
        Dim sQuotes As String = String.Empty
        Dim provider As String = Me.DsTab_TablesSeach.ProviderName
        Dim connectionString As String = Me.DsTab_TablesSeach.ConnectionString
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
        GetTab_TablesFullName = dbcmd.ExecuteScalar
        dbcon.Close()

        If IsNothing(GetTab_TablesFullName) Then
            GetTab_TablesFullName = String.Empty
        End If

        Return GetTab_TablesFullName

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
            Return ButtonEditTab_Tables.Text
        End Get
        Set(ByVal value As String)
            ButtonEditTab_Tables.Text = value
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


    Protected Sub ButtonEditTab_Tables_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEditTab_Tables.TextChanged

        If IsPostBack And Not Page.IsCallback Then
            If ButtonEditTab_Tables.Text.Length > 0 Then
                LabelTab_Tables.Text = GetTab_TablesFullName(ButtonEditTab_Tables.Text).Trim
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.DsTab_Tables.SelectCommand = "SELECT " & KeyField & " AS NCODE, SDESCRIPT FROM " & TableName & " WHERE SSTATREGT = '1' ORDER BY SDESCRIPT"
    End Sub

    Protected Sub DsTab_Tables_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DsTab_Tables.Selected
        If Not e.Exception Is Nothing Then
            LabelTab_Tables.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub DsTab_TablesSeach_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles DsTab_TablesSeach.Selected
        If Not e.Exception Is Nothing Then
            LabelTab_Tables.Text = "The BackOffice's database connection is failing. Refresh the page and try again."
            e.ExceptionHandled = True
        End If
    End Sub
End Class
