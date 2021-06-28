Imports DevExpress.Web.ASPxGridView
Imports InMotionGIT.Common.Proxy

Partial Class Underwriting_Controls_History
    Inherits System.Web.UI.UserControl

    '    Public Sub RebindGridView()
    '        gvCaseHistory.DataBind()
    '    End Sub

    '    Protected Sub gvCaseHistory_CommandButtonInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCommandButtonEventArgs) Handles gvCaseHistory.CommandButtonInitialize
    '        If Not Session("IsEditMode") Then
    '            e.Visible = False
    '        End If
    '    End Sub

    '    Protected Sub gvCaseHistory_CustomButtonInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonEventArgs) Handles gvCaseHistory.CustomButtonInitialize
    '        If Not Session("IsEditMode") Then
    '            e.Visible = False
    '        End If
    '    End Sub

    '#Region "Lookups"

    '    Protected Sub gvCaseHistory_DataBinding(sender As Object, e As System.EventArgs) Handles gvCaseHistory.DataBinding
    '        With DirectCast(gvCaseHistory.Columns("ManualOrAutomatic"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.ManualOrAutomaticTypeLkp(Session("LanguageID"), False)
    '        End With
    '        With DirectCast(gvCaseHistory.Columns("AlarmType"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.AlarmTypeLkp(Session("LanguageID"), False)
    '        End With
    '        With DirectCast(gvCaseHistory.Columns("EntryType"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.EntryTypeLkp(Session("LanguageID"), False)
    '        End With
    '        With DirectCast(gvCaseHistory.Columns("RequirementType"), GridViewDataComboBoxColumn)
    '            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeLkp(Session("LanguageID"), False)
    '        End With
    '    End Sub

    '#End Region

End Class