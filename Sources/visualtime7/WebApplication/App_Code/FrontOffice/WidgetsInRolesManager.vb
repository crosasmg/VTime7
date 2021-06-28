Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Public Class WidgetsInRolesManager
    
End Class

''' <summary>
''' Summary description for Class1
''' </summary>
Public Class Record

    Private id_Renamed As Integer
    Private WidgetId_Renamed As Integer
    Private RoleId_Renamed As String
    Private IsDefault_Renamed As Boolean
    Private IsEditAllow_Renamed As Boolean
    Private IsChecked_Renamed As Boolean
    Private IsAllowedToEditTheTitle_Renamed As Boolean
    Private Sequence_Renamed As Integer


    Public Sub New(id As Integer, WidgetId As Integer, RoleId As String, IsDefault As Boolean,
                   IsEditAllow As Boolean, IsChecked As Boolean, IsAllowedToEditTheTitle As Boolean, Sequence As Integer)

        id_Renamed = id
        WidgetId_Renamed = WidgetId
        RoleId_Renamed = RoleId
        IsDefault_Renamed = IsDefault
        IsEditAllow_Renamed = IsEditAllow
        IsChecked_Renamed = IsChecked
        IsAllowedToEditTheTitle_Renamed = IsAllowedToEditTheTitle
        Sequence_Renamed = Sequence
    End Sub

    Public ReadOnly Property Id() As Integer
        Get
            Return id_Renamed
        End Get
    End Property

    Public ReadOnly Property WidgetId() As Integer
        Get
            Return WidgetId_Renamed
        End Get
    End Property

    Public ReadOnly Property RoleId() As String
        Get
            Return RoleId_Renamed
        End Get
    End Property


    Public ReadOnly Property IsDefault() As Boolean
        Get
            Return IsDefault_Renamed
        End Get
    End Property

    Public ReadOnly Property IsEditAllow() As Boolean
        Get
            Return IsEditAllow_Renamed
        End Get
    End Property

    Public ReadOnly Property IsChecked() As Boolean
        Get
            Return IsChecked_Renamed
        End Get
    End Property

    Public ReadOnly Property IsAllowedToEditTheTitle() As Boolean
        Get
            Return IsAllowedToEditTheTitle_Renamed
        End Get
    End Property

    Public ReadOnly Property Sequence() As Integer
        Get
            Return Sequence_Renamed
        End Get
    End Property
End Class
