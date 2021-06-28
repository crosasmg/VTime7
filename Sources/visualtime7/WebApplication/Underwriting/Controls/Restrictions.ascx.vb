Imports InMotionGIT.Common.Proxy
Imports DevExpress.Web.ASPxGridView
Imports System.Data

Partial Class Underwriting_Controls_Restrictions
    Inherits System.Web.UI.UserControl

#Region "Lookups"

    'Protected Sub RestrictionsGridView_DataBinding(sender As Object, e As System.EventArgs) Handles RestrictionsGridView.DataBinding
    '    With DirectCast(RestrictionsGridView.Columns("ExclusionType"), GridViewDataComboBoxColumn)
    '        .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.ExclusionTypeLkp(Session("LanguageID"), False)
    '    End With
    '    With DirectCast(RestrictionsGridView.Columns("ImpairmentCode"), GridViewDataComboBoxColumn)
    '        .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.IllnessTypeLkp(Session("LanguageID"), False)
    '    End With
    '    With DirectCast(RestrictionsGridView.Columns("RequirementType"), GridViewDataComboBoxColumn)
    '        .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeLkp(Session("LanguageID"), False)
    '    End With
    'End Sub

#End Region

End Class