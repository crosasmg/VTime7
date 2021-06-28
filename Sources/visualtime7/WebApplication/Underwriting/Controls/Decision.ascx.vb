#Region "using"

Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxTabControl
Imports InMotionGIT.Underwriting.Contracts
Imports InMotionGIT.Common.Proxy
Imports System.Data
Imports DevExpress.Web.ASPxGridView

#End Region

Partial Class Underwriting_Controls_Decision
    Inherits System.Web.UI.UserControl

    '#Region "Page Events"

    '    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '        Dim controlContainer As ASPxPageControl = DirectCast(Me.NamingContainer, ASPxPageControl)

    '        If Session("IsEditMode") Then
    '            '   FormView1.ChangeMode(FormViewMode.Edit)
    '        End If
    '    End Sub

    '#End Region

    '#Region "Controls Events"

    '    Protected Sub UnderwritingData_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles UnderwritingData.Selecting
    '        'If UnderwritingCase.CurrentCaseId = String.Empty Then
    '        '    e.InputParameters("UNDERWRITINGCASEID") = UnderwritingCase.CurrentCaseId
    '        '    e.InputParameters("isSelectingOnCache") = Session("IsEditMode")
    '        '    If hdnIsCaseTakenDecision.Contains("IsTaken") AndAlso Not hdnIsCaseTakenDecision.Get("IsTaken") Is Nothing AndAlso hdnIsCaseTakenDecision.Get("IsTaken") = "T" Then
    '        '        e.InputParameters("wasLocked") = True

    '        '    End If
    '        'End If
    '    End Sub

    '    Protected Sub RequirementGridView_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles RequirementGridView.CustomColumnDisplayText
    '        If IsNumeric(e.Value) AndAlso e.Value = -1 Then
    '            e.DisplayText = ""
    '        End If
    '    End Sub

    '#End Region


    '#Region "Lookups"

    '    Protected Sub RequirementGridView_DataBinding(sender As Object, e As System.EventArgs) Handles RequirementGridView.DataBinding
    '        With DirectCast(RequirementGridView.Columns("AlarmType"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.AlarmTypeLkp(Session("LanguageID"), False)
    '        End With
    '        With DirectCast(RequirementGridView.Columns("RequirementType"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeLkp(Session("LanguageID"), False)
    '        End With
    '        With DirectCast(RequirementGridView.Columns("UnderwritingArea"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingAreaTypeLkp(Session("LanguageID"), False)
    '        End With
    '        With DirectCast(RequirementGridView.Columns("Status"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.RequirementStatusTypeLkp(Session("LanguageID"), False)
    '        End With
    '        With DirectCast(RequirementGridView.Columns("QuestionId"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.QuestionLkp(Session("LanguageID"), False)
    '        End With
    '    End Sub


    '#End Region


End Class
