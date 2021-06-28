#Region "using"

Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxHiddenField
Imports InMotionGIT.Underwriting.Contracts
Imports System.Globalization
Imports System.Web.UI
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports System.Data
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs
Imports InMotionGIT.Seguridad.Proxy

#End Region

Partial Class Underwriting_Controls_Header
    Inherits UserControl

#Region "Private fields, to hold the state of the entity"

    Private _UnderwritingCaseId As String
    'Private _Context As InMotionGIT.Common.Contracts.Context = Nothing
    Private _WorkFlowResponse As IDictionary(Of String, Object) = Nothing

    Dim tagTypeIds As New Dictionary(Of String, Int16)() From {
        {"FormId", 1},
        {"RequirementId", 2},
        {"CaseId", 3},
        {"InformativeId", 4},
        {"RequiretmentTypeId", 5}
    }
    Dim provider As String = ConfigurationManager.AppSettings.Get("DNEProvider")
#End Region

#Region "Public Properties"
    Protected Property DecisionText As String = ""
    Protected Property StatusText As String = ""
    ''' <summary>
    ''' Gets the form view current mode
    ''' </summary>
    Public ReadOnly Property FormViewCurrentMode() As FormViewMode
        Get
            Return Nothing ' fvHeader.CurrentMode
        End Get
    End Property

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

    ''' <summary>
    ''' Gets or sets the underwriting case id selected on the search grid
    ''' </summary>
    Public Property IsEditMode() As Boolean
        Get
            Return Session("IsEditMode")
        End Get
        Set(ByVal value As Boolean)
            Session("IsEditMode") = value
        End Set
    End Property


#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ASPxWebControl.RegisterBaseScript(Page)
    End Sub

#End Region
End Class