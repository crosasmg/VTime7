#Region "using"

#End Region

Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Web.Script.Serialization
Imports System.Globalization

Partial Class Underwriting_Controls_GeneralInformation
    Inherits System.Web.UI.UserControl

#Region "Private fields, to hold the state of the entity"
    Private _UnderwritingCaseId As String
#End Region

#Region "Public Properties"

    ''' <summary>
    ''' Gets or sets the underwriting case id selected on the search grid
    ''' </summary>
    Public Property UnderwritingCaseId() As String
        Get
            Return _UnderwritingCaseId
        End Get
        Set(ByVal value As String)
            _UnderwritingCaseId = value
        End Set
    End Property
#End Region

End Class
