Imports DevExpress.Web.ASPxEditors
Imports InMotionGIT.Underwriting.Contracts
Imports InMotionGIT.Underwriting.Proxy
Imports DevExpress.Web.ASPxGridView
Imports InMotionGIT.Common.Proxy

Partial Class Underwriting_Controls_RequirementsGeneralInformation
    Inherits System.Web.UI.UserControl

#Region "Event Controls"

    Protected Sub cmbClientID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientID.SelectedIndexChanged
        'If Not cmbClientID.SelectedItem Is Nothing AndAlso InMotionGIT.Underwriting.Proxy.Helpers.Requirement.IsEqualToProvider(cmbClientID.SelectedItem.Value) Then
        '    cmbProviderID.Value = cmbClientID.SelectedItem.Value
        'End If
    End Sub

    Protected Sub cmbRequirementType_DataBinding(sender As Object, e As System.EventArgs) Handles cmbRequirementType.DataBinding
        cmbRequirementType.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.RequirementTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbAlarmType_DataBinding(sender As Object, e As System.EventArgs) Handles cmbAlarmType.DataBinding
        cmbAlarmType.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.AlarmTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbUnderwritingArea_DataBinding(sender As Object, e As System.EventArgs) Handles cmbUnderwritingArea.DataBinding
        cmbUnderwritingArea.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingAreaTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbProcessType_DataBinding(sender As Object, e As System.EventArgs) Handles cmbProcessType.DataBinding
        cmbProcessType.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.ProcessTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbStatus_DataBinding(sender As Object, e As System.EventArgs) Handles cmbStatus.DataBinding
        cmbStatus.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.RequirementStatusTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbPayer_DataBinding(sender As Object, e As System.EventArgs) Handles cmbPayer.DataBinding
        cmbPayer.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.PayableByTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbRequirementType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRequirementType.DataBound
        If TryCast(sender, ASPxComboBox).Value = -1 Then
            TryCast(sender, ASPxComboBox).Text = ""

        Else
            With btnCompleteInformation
                If TryCast(sender, ASPxComboBox).Value = 1 Then
                    .Enabled = False
                Else
                    .Enabled = True
                End If
            End With
        End If
    End Sub

    Protected Sub cmbClientID_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientID.DataBound
        If TryCast(sender, ASPxComboBox).Value = "-1" Then
            TryCast(sender, ASPxComboBox).Text = ""
        End If
    End Sub

    Protected Sub cmbProcessType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProcessType.DataBound
        If TryCast(sender, ASPxComboBox).Value = -1 Then
            TryCast(sender, ASPxComboBox).Text = ""
        End If
    End Sub

    Protected Sub cmbUnderwritingArea_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnderwritingArea.DataBound
        If TryCast(sender, ASPxComboBox).Value = -1 Then
            TryCast(sender, ASPxComboBox).Text = ""
        End If
    End Sub

    Protected Sub cmbStatus_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatus.DataBound
        If TryCast(sender, ASPxComboBox).Value = -1 Then
            TryCast(sender, ASPxComboBox).Text = ""
        End If
    End Sub

    Protected Sub cmbAlarmType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAlarmType.DataBound
        If TryCast(sender, ASPxComboBox).Value = -1 Then
            TryCast(sender, ASPxComboBox).Text = ""
        End If
    End Sub

    Protected Sub txtDebits_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDebits.DataBound
        If TryCast(sender, ASPxTextBox).Value = -1 Then
            TryCast(sender, ASPxTextBox).Text = ""
        End If
    End Sub

    Protected Sub txtCredits_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCredits.DataBound
        If TryCast(sender, ASPxTextBox).Value = -1 Then
            TryCast(sender, ASPxTextBox).Text = ""
        End If
    End Sub

    Protected Sub txtBalance_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBalance.DataBound
        If TryCast(sender, ASPxTextBox).Value = -1 Then
            TryCast(sender, ASPxTextBox).Text = ""
        End If
    End Sub

    Protected Sub cmbProviderID_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProviderID.DataBound
        If TryCast(sender, ASPxComboBox).Value = -1 Then
            TryCast(sender, ASPxComboBox).Text = ""
        End If
    End Sub

    Protected Sub cmbPayer_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPayer.DataBound
        If TryCast(sender, ASPxComboBox).Value = -1 Then
            TryCast(sender, ASPxComboBox).Text = ""
        End If
    End Sub

    Protected Sub txtCost_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCost.DataBound
        If TryCast(sender, ASPxTextBox).Value = -1 Then
            TryCast(sender, ASPxTextBox).Text = ""
        End If
    End Sub

    Protected Sub txtCostDueAmount_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostDueAmount.DataBound
        If TryCast(sender, ASPxTextBox).Value = -1 Then
            TryCast(sender, ASPxTextBox).Text = ""
        End If
    End Sub

    Protected Sub txtAccordCode_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccordCode.DataBound
        If TryCast(sender, ASPxTextBox).Value = -1 Then
            TryCast(sender, ASPxTextBox).Text = ""
        End If
    End Sub

#End Region

End Class