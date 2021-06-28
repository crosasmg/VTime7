Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Imports System.Xml
Imports InMotionGIT.Common.Proxy

Partial Class Underwriting_Controls_SuscriptionRulesInformation
    Inherits System.Web.UI.UserControl

    Protected Sub cmbAlarmType_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAlarmType.Load

    End Sub

    Protected Sub cmbAlarmType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAlarmType.DataBound
        Dim combo As ASPxComboBox = DirectCast(sender, ASPxComboBox)

        If combo.Value = 4 Then
            rpFlatExtraPremium.Visible = True
            rpExclusion.Visible = False
        ElseIf combo.Value = 5 Then
            rpFlatExtraPremium.Visible = False
            rpExclusion.Visible = True
        Else
            rpFlatExtraPremium.Visible = False
            rpExclusion.Visible = False
        End If

        combo.Enabled = False
    End Sub

    Protected Sub txtWaitingPeriodDays_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWaitingPeriodDays.DataBound
        DirectCast(sender, ASPxTextBox).Enabled = False
    End Sub

    Protected Sub cmbFlatPeriodType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFlatPeriodType.DataBound
        DirectCast(sender, ASPxComboBox).Enabled = False
    End Sub

    Protected Sub txtDurationOfExtraPremiumYears_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDurationOfExtraPremiumYears.DataBound
        DirectCast(sender, ASPxTextBox).Enabled = False
    End Sub

    Protected Sub txtDurationOfExtraPremiumMonths_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDurationOfExtraPremiumMonths.DataBound
        DirectCast(sender, ASPxTextBox).Enabled = False
    End Sub

    Protected Sub txtDurationOfExtraPremiumDays_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDurationOfExtraPremiumDays.DataBound
        DirectCast(sender, ASPxTextBox).Enabled = False
    End Sub

    Protected Sub cmbExclusionType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbExclusionType.DataBound
        DirectCast(sender, ASPxComboBox).Enabled = False
    End Sub

    Protected Sub cmbExclusionPeriodType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbExclusionPeriodType.DataBound
        DirectCast(sender, ASPxComboBox).Enabled = False
    End Sub

    Protected Sub cmbCover_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCover.DataBound
        DirectCast(sender, ASPxComboBox).Enabled = False
    End Sub

    Protected Sub cmbIllness_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIllness.DataBound
        DirectCast(sender, ASPxComboBox).Enabled = False
    End Sub

    Protected Sub txtWaitingPeriodYears_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWaitingPeriodYears.DataBound
        DirectCast(sender, ASPxTextBox).Enabled = False
    End Sub

    Protected Sub txtWaitingPeriodMonths_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWaitingPeriodMonths.DataBound
        DirectCast(sender, ASPxTextBox).Enabled = False
    End Sub


End Class
