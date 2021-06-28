Imports GIT.EDW.Query.Model

Partial Class Controls_CustomSampleUserControl
    Inherits System.Web.UI.UserControl
    Implements Interfaces.IQueryUserControl

    Public Property ControlID As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.ControlID
        Get
            Return Me.ID
        End Get
        Set(ByVal value As String)
            Me.ID = value
            ASPxComboBox1.ClientInstanceName = value
        End Set
    End Property

    Public Property Repository As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Repository
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property ToolTip As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.ToolTip
        Get
            Return ASPxComboBox1.ToolTip
        End Get
        Set(ByVal value As String)
            ASPxComboBox1.ToolTip = value
        End Set
    End Property

    Public Property Value As Object Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Value
        Get
            Return ASPxComboBox1.SelectedItem.Text
        End Get
        Set(ByVal value As Object)
            If Not IsNothing(ASPxComboBox1.Items.FindByText(value)) Then
                ASPxComboBox1.Items.FindByText(value).Selected = True
            End If
        End Set
    End Property

    Public Property Enabled1 As Boolean Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Enabled
        Get
            Return ASPxComboBox1.ClientEnabled
        End Get

        Set(value As Boolean)
            ASPxComboBox1.ClientEnabled = value
        End Set
    End Property

    Public Property Script As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Script
        Get
            Return ASPxComboBox1.ClientSideEvents.SelectedIndexChanged
        End Get
        Set(value As String)
            ASPxComboBox1.ClientSideEvents.SelectedIndexChanged = value
        End Set
    End Property

End Class
