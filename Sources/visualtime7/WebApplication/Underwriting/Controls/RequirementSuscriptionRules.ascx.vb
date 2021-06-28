#Region "using"

Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxRoundPanel
Imports InMotionGIT.Underwriting.Contracts
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Common.Helpers


#End Region

Partial Class Underwriting_Controls_RequirementSuscriptionRules
    Inherits System.Web.UI.UserControl

#Region "Private fields, to hold the state of the entity"

    Private _masterKeyValue As String

    Public WriteOnly Property IsEditingMode() As Boolean
        Set(ByVal value As Boolean)
            hdnIsEditingMode.Set("IsEditingMode", value)
        End Set
    End Property

#End Region

#Region "Controls Events"

    Protected Sub gQuestionForRequirement_DataBinding(sender As Object, e As EventArgs)
        Dim gQuestionForReq As ASPxGridView = TryCast(sender, ASPxGridView)
        gQuestionForReq.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.QuestionsFromRequirementLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub gRules_DataBinding(sender As Object, e As EventArgs)
        Dim gRulesGrid As ASPxGridView = TryCast(sender, ASPxGridView)
        gRulesGrid.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingRulesTypeLkp(Session("LanguageID"), False)
    End Sub


    Protected Sub btnSave_Load(sender As Object, e As System.EventArgs)
        Dim button As ASPxButton = DirectCast(sender, ASPxButton)

        If Not Session("IsEditMode") Is Nothing Then
            DirectCast(sender, ASPxButton).Visible = Session("IsEditMode")
        Else
            DirectCast(sender, ASPxButton).Visible = False
        End If
    End Sub

    Protected Sub ddeQuestion_Validation(sender As Object, e As DevExpress.Web.ASPxEditors.ValidationEventArgs)
        If e.Value = Nothing OrElse e.Value = "" OrElse e.Value = 0 Then
            e.IsValid = True
            e.ErrorText = "El valor es obligatorio"
        Else
            e.IsValid = False
            e.ErrorText = String.Empty
        End If
    End Sub

    Protected Sub txtManualRule_Validation(sender As Object, e As DevExpress.Web.ASPxEditors.ValidationEventArgs)
        Dim grid As ASPxGridView = DirectCast(DirectCast(sender, ASPxTextBox).Parent.NamingContainer, ASPxGridView)

        'txtManualRule
        Dim chkManualRule As ASPxCheckBox = DirectCast(grid.FindEditFormTemplateControl("chkManualRule"), ASPxCheckBox)
        If chkManualRule.Checked And DirectCast(sender, ASPxTextBox).Text = "" Then
            e.IsValid = True
            e.ErrorText = "El valor es obligatorio"
        Else
            e.IsValid = False
            e.ErrorText = String.Empty
        End If
    End Sub

    Protected Sub ddeRule_Validation(sender As Object, e As DevExpress.Web.ASPxEditors.ValidationEventArgs)
        Dim grid As ASPxGridView = DirectCast(DirectCast(sender, ASPxDropDownEdit).Parent.NamingContainer, ASPxGridView)
        Dim chkManualRule As ASPxCheckBox = DirectCast(grid.FindEditFormTemplateControl("chkManualRule"), ASPxCheckBox)
        If Not chkManualRule.Checked And (DirectCast(sender, ASPxDropDownEdit).Value = Nothing OrElse _
                                          DirectCast(sender, ASPxDropDownEdit).Value = "" OrElse _
                                          DirectCast(sender, ASPxDropDownEdit).Value = 0) Then
            e.IsValid = True
            e.ErrorText = "El valor es obligatorio"
        Else
            e.IsValid = False
            e.ErrorText = String.Empty
        End If
    End Sub

    Protected Sub dsUnderwritingRules_Inserting(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles dsUnderwritingRules.Inserting
        e.InputParameters("languageId") = Session("LanguageID")
    End Sub

    Protected Sub dsUnderwritingRules_Selecting(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles dsUnderwritingRules.Selecting
        e.InputParameters("languageId") = Session("LanguageID")
    End Sub

    ''' <summary>
    ''' Sets the question id values to a javascript var that will be set to the grid control properties
    ''' </summary>
    ''' <param name="sender">The object that represents the grid control</param>
    ''' <param name="e">The parameters from the grid control event</param>
    Protected Sub gQuestionForRequirement_CustomJSProperties(sender As Object, e As ASPxGridViewClientJSPropertiesEventArgs)
        Dim ddeQuestion As ASPxDropDownEdit = DirectCast(UnderwritingGridView.FindEditFormTemplateControl("ddeQuestion"), ASPxDropDownEdit)
        Dim grid As ASPxGridView = CType(ddeQuestion.FindControl("gQuestionForRequirement"), ASPxGridView)
        Dim keyValues(grid.VisibleRowCount - 1) As Object
        Dim questionDescriptions(grid.VisibleRowCount - 1) As Object
        For i As Integer = 0 To grid.VisibleRowCount - 1
            keyValues(i) = grid.GetRowValues(i, "Code")
            questionDescriptions(i) = grid.GetRowValues(i, "Description")
        Next i

        e.Properties("cpQuestionDescription") = questionDescriptions
        e.Properties("cpKeyValues") = keyValues
    End Sub

    ''' <summary>
    ''' Synchronize the grid control to set the focused row
    ''' </summary>
    ''' <param name="sender">The object that represents the grid control</param>
    ''' <param name="e">The parameters from the grid control event</param>
    Protected Sub gQuestionForRequirement_AfterPerformCallback(sender As Object, e As ASPxGridViewAfterPerformCallbackEventArgs)
        SynchronizeFocusedRow("gQuestionForRequirement", "ddeQuestion")
    End Sub

    ''' <summary>
    ''' Sets the question id values to a javascript var that will be set to the grid control properties
    ''' </summary>
    ''' <param name="sender">The object that represents the grid control</param>
    ''' <param name="e">The parameters from the grid control event</param>
    Protected Sub gRules_CustomJSProperties(sender As Object, e As ASPxGridViewClientJSPropertiesEventArgs)
        Dim ddeRule As ASPxDropDownEdit = DirectCast(UnderwritingGridView.FindEditFormTemplateControl("ddeRule"), ASPxDropDownEdit)
        Dim grid As ASPxGridView = CType(ddeRule.FindControl("gRules"), ASPxGridView)
        Dim keyValues(grid.VisibleRowCount - 1) As Object
        Dim ruleDescriptions(grid.VisibleRowCount - 1) As Object
        For i As Integer = 0 To grid.VisibleRowCount - 1
            keyValues(i) = grid.GetRowValues(i, "Code")
            ruleDescriptions(i) = grid.GetRowValues(i, "Description")
        Next i

        e.Properties("cpRulesDescription") = ruleDescriptions
        e.Properties("cpKeyValues") = keyValues
    End Sub

    ''' <summary>
    ''' Synchronize the grid control to set the focused row
    ''' </summary>
    ''' <param name="sender">The object that represents the grid control</param>
    ''' <param name="e">The parameters from the grid control event</param>
    Protected Sub gRules_AfterPerformCallback(sender As Object, e As ASPxGridViewAfterPerformCallbackEventArgs)
        SynchronizeFocusedRow("gRules", "ddeRule")
    End Sub

#End Region

#Region "UnderwritingGridView Events"

    Protected Sub UnderwritingGridView_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles UnderwritingGridView.RowInserting
        Dim selectedCase As UnderwritingCase = InMotionGIT.Underwriting.Proxy.Helpers.Support.RetrieveInstance

        If Not IsNothing(selectedCase) Then
            With selectedCase
                If Not IsNothing(.Requirements) AndAlso .Requirements.Count > 0 Then
                    For Each requirement As InMotionGIT.Underwriting.Contracts.Requirement In .Requirements
                        If requirement.RequirementID = .CurrentRequirementID Then
                            With requirement
                                If Not IsNothing(.UnderwritingRules) AndAlso .UnderwritingRules.Count > 0 Then
                                    _masterKeyValue = .UnderwritingRules.Count.ToString
                                Else
                                    _masterKeyValue = "1"
                                End If
                            End With

                            Exit For
                        End If
                    Next

                Else
                    _masterKeyValue = "1"
                End If
            End With

            e.NewValues("UnderRuleId") = _masterKeyValue

            Dim cmbAlarmType As ASPxComboBox = DirectCast(DirectCast(sender, ASPxGridView).FindEditFormTemplateControl("cmbAlarmType"), ASPxComboBox)
            Dim isFlatValid As Boolean = True
            Dim isExclusionValid As Boolean = True

            If Not cmbAlarmType.SelectedItem Is Nothing Then
                If cmbAlarmType.SelectedItem.Value = 4 Then
                    Dim rpFlatExtraPremium As ASPxRoundPanel = DirectCast(DirectCast(sender, ASPxGridView).FindEditFormTemplateControl("rpFlatExtraPremium"), ASPxRoundPanel)
                    Dim cmbFlatPeriodType As ASPxComboBox = DirectCast(rpFlatExtraPremium.FindControl("cmbFlatPeriodType"), ASPxComboBox)

                    If cmbFlatPeriodType.SelectedItem.Value = 1 Then
                        Dim txtDurationOfFlatExtraPremiumYears As ASPxTextBox = DirectCast(rpFlatExtraPremium.FindControl("txtDurationOfExtraPremiumYears"), ASPxTextBox)
                        Dim txtDurationOfFlatExtraPremiumMonths As ASPxTextBox = DirectCast(rpFlatExtraPremium.FindControl("txtDurationOfExtraPremiumMonths"), ASPxTextBox)
                        Dim txtDurationOfFlatExtraPremiumDays As ASPxTextBox = DirectCast(rpFlatExtraPremium.FindControl("txtDurationOfExtraPremiumDays"), ASPxTextBox)

                        If (txtDurationOfFlatExtraPremiumYears.Text = String.Empty OrElse Convert.ToInt32(txtDurationOfFlatExtraPremiumYears.Text) <= 0) AndAlso _
                            (txtDurationOfFlatExtraPremiumMonths.Text = String.Empty OrElse Convert.ToInt32(txtDurationOfFlatExtraPremiumMonths.Text) <= 0) AndAlso _
                            (txtDurationOfFlatExtraPremiumDays.Text = String.Empty OrElse Convert.ToInt32(txtDurationOfFlatExtraPremiumDays.Text) <= 0) Then
                            isFlatValid = False
                        Else
                            Dim txtExtraFlatPremium As ASPxTextBox = DirectCast(rpFlatExtraPremium.FindControl("txtExtraFlatPremium"), ASPxTextBox)

                            e.NewValues("FlatExtraPremium") = txtExtraFlatPremium.Text
                            e.NewValues("ExclusionPeriodType") = cmbFlatPeriodType.Value
                            e.NewValues("DurationOfFlatExtraPremiumYears") = Convert.ToInt32(txtDurationOfFlatExtraPremiumYears.Text)
                            e.NewValues("DurationOfFlatExtraPremiumMonths") = Convert.ToInt32(txtDurationOfFlatExtraPremiumMonths.Text)
                            e.NewValues("DurationOfFlatExtraPremiumDays") = Convert.ToInt32(txtDurationOfFlatExtraPremiumDays.Text)

                            e.NewValues("ExclusionType") = 0
                            e.NewValues("Coverage") = 0
                            e.NewValues("ImpairmentCode") = 0
                            e.NewValues("WaitingPeriodYears") = 0
                            e.NewValues("WaitingPeriodMonths") = 0
                            e.NewValues("WaitingPeriodDays") = 0
                        End If
                    End If

                ElseIf cmbAlarmType.SelectedItem.Value = 5 Then
                    Dim rpExclusion As ASPxRoundPanel = DirectCast(DirectCast(sender, ASPxGridView).FindEditFormTemplateControl("rpExclusion"), ASPxRoundPanel)

                    Dim cmbExclusionPeriodType As ASPxComboBox = DirectCast(rpExclusion.FindControl("cmbExclusionPeriodType"), ASPxComboBox)
                    If cmbExclusionPeriodType.SelectedItem.Value = 1 Then
                        Dim txtWaitingPeriodYears As ASPxTextBox = DirectCast(rpExclusion.FindControl("txtWaitingPeriodYears"), ASPxTextBox)
                        Dim txtWaitingPeriodMonths As ASPxTextBox = DirectCast(rpExclusion.FindControl("txtWaitingPeriodMonths"), ASPxTextBox)
                        Dim txtWaitingPeriodDays As ASPxTextBox = DirectCast(rpExclusion.FindControl("txtWaitingPeriodDays"), ASPxTextBox)

                        If (txtWaitingPeriodYears.Text = String.Empty OrElse Convert.ToInt32(txtWaitingPeriodYears.Text) <= 0) AndAlso _
                            (txtWaitingPeriodMonths.Text = String.Empty OrElse Convert.ToInt32(txtWaitingPeriodMonths.Text) <= 0) AndAlso _
                            (txtWaitingPeriodDays.Text = String.Empty OrElse Convert.ToInt32(txtWaitingPeriodDays.Text) <= 0) Then
                            isExclusionValid = False
                        Else
                            Dim cmbExclusionType As ASPxComboBox = DirectCast(rpExclusion.FindControl("cmbExclusionType"), ASPxComboBox)
                            Dim cmbCover As ASPxComboBox = DirectCast(rpExclusion.FindControl("cmbCover"), ASPxComboBox)
                            Dim cmbIllness As ASPxComboBox = DirectCast(rpExclusion.FindControl("cmbIllness"), ASPxComboBox)

                            e.NewValues("ExclusionType") = cmbExclusionType.Value
                            e.NewValues("ExclusionPeriodType") = cmbExclusionPeriodType.Value
                            e.NewValues("Coverage") = cmbCover.Value
                            e.NewValues("ImpairmentCode") = cmbIllness.Value
                            e.NewValues("WaitingPeriodYears") = Convert.ToInt32(txtWaitingPeriodYears.Text)
                            e.NewValues("WaitingPeriodMonths") = Convert.ToInt32(txtWaitingPeriodMonths.Text)
                            e.NewValues("WaitingPeriodDays") = Convert.ToInt32(txtWaitingPeriodDays.Text)

                            e.NewValues("FlatExtraPremium") = 0
                            e.NewValues("DurationOfFlatExtraPremiumYears") = 0
                            e.NewValues("DurationOfFlatExtraPremiumMonths") = 0
                            e.NewValues("DurationOfFlatExtraPremiumDays") = 0
                        End If
                    End If
                End If

                If Not isExclusionValid Then
                    e.Cancel = True
                    Throw New ArgumentException("Al menos uno de los valores de 'Duración Plazo' debe contener un valor")
                ElseIf Not isFlatValid Then
                    e.Cancel = True
                    Throw New ArgumentException("Al menos uno de los valores de 'Duración Flat Extra Prima' debe contener un valor")
                Else
                    Dim memo As ASPxMemo = DirectCast(DirectCast(sender, ASPxGridView).FindEditFormTemplateControl("ppEditExplainRule").FindControl("mmExplainRule"), ASPxMemo)
                    e.NewValues("Explanation") = memo.Text
                End If
            End If
        End If
    End Sub

    Protected Sub UnderwritingGridView_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles UnderwritingGridView.RowUpdating
        Dim cmbAlarmType As ASPxComboBox = DirectCast(DirectCast(sender, ASPxGridView).FindEditFormTemplateControl("cmbAlarmType"), ASPxComboBox)
        Dim isFlatValid As Boolean = True
        Dim isExclusionValid As Boolean = True

        If Not cmbAlarmType.SelectedItem Is Nothing Then
            If cmbAlarmType.SelectedItem.Value = 4 Then
                Dim rpFlatExtraPremium As ASPxRoundPanel = DirectCast(DirectCast(sender, ASPxGridView).FindEditFormTemplateControl("rpFlatExtraPremium"), ASPxRoundPanel)
                Dim cmbFlatPeriodType As ASPxComboBox = DirectCast(rpFlatExtraPremium.FindControl("cmbFlatPeriodType"), ASPxComboBox)

                If cmbFlatPeriodType.SelectedItem.Value = 1 Then
                    Dim txtDurationOfFlatExtraPremiumYears As ASPxTextBox = DirectCast(rpFlatExtraPremium.FindControl("txtDurationOfExtraPremiumYears"), ASPxTextBox)
                    Dim txtDurationOfFlatExtraPremiumMonths As ASPxTextBox = DirectCast(rpFlatExtraPremium.FindControl("txtDurationOfExtraPremiumMonths"), ASPxTextBox)
                    Dim txtDurationOfFlatExtraPremiumDays As ASPxTextBox = DirectCast(rpFlatExtraPremium.FindControl("txtDurationOfExtraPremiumDays"), ASPxTextBox)

                    If (txtDurationOfFlatExtraPremiumYears.Text = String.Empty OrElse Convert.ToInt32(txtDurationOfFlatExtraPremiumYears.Text) <= 0) AndAlso _
                        (txtDurationOfFlatExtraPremiumMonths.Text = String.Empty OrElse Convert.ToInt32(txtDurationOfFlatExtraPremiumMonths.Text) <= 0) AndAlso _
                        (txtDurationOfFlatExtraPremiumDays.Text = String.Empty OrElse Convert.ToInt32(txtDurationOfFlatExtraPremiumDays.Text) <= 0) Then
                        isFlatValid = False
                    Else
                        Dim txtExtraFlatPremium As ASPxTextBox = DirectCast(rpFlatExtraPremium.FindControl("txtExtraFlatPremium"), ASPxTextBox)

                        e.NewValues("FlatExtraPremium") = txtExtraFlatPremium.Text
                        e.NewValues("ExclusionPeriodType") = cmbFlatPeriodType.Value
                        e.NewValues("DurationOfFlatExtraPremiumYears") = Convert.ToInt32(txtDurationOfFlatExtraPremiumYears.Text)
                        e.NewValues("DurationOfFlatExtraPremiumMonths") = Convert.ToInt32(txtDurationOfFlatExtraPremiumMonths.Text)
                        e.NewValues("DurationOfFlatExtraPremiumDays") = Convert.ToInt32(txtDurationOfFlatExtraPremiumDays.Text)

                        e.NewValues("ExclusionType") = 0
                        e.NewValues("ExclusionPeriodType") = 0
                        e.NewValues("Coverage") = 0
                        e.NewValues("ImpairmentCode") = 0
                        e.NewValues("WaitingPeriodYears") = 0
                        e.NewValues("WaitingPeriodMonths") = 0
                        e.NewValues("WaitingPeriodDays") = 0
                    End If
                End If

            ElseIf cmbAlarmType.SelectedItem.Value = 5 Then
                Dim rpExclusion As ASPxRoundPanel = DirectCast(DirectCast(sender, ASPxGridView).FindEditFormTemplateControl("rpExclusion"), ASPxRoundPanel)

                Dim cmbExclusionPeriodType As ASPxComboBox = DirectCast(rpExclusion.FindControl("cmbExclusionPeriodType"), ASPxComboBox)
                If cmbExclusionPeriodType.SelectedItem.Value = 1 Then
                    Dim txtWaitingPeriodYears As ASPxTextBox = DirectCast(rpExclusion.FindControl("txtWaitingPeriodYears"), ASPxTextBox)
                    Dim txtWaitingPeriodMonths As ASPxTextBox = DirectCast(rpExclusion.FindControl("txtWaitingPeriodMonths"), ASPxTextBox)
                    Dim txtWaitingPeriodDays As ASPxTextBox = DirectCast(rpExclusion.FindControl("txtWaitingPeriodDays"), ASPxTextBox)

                    If (txtWaitingPeriodYears.Text = String.Empty OrElse Convert.ToInt32(txtWaitingPeriodYears.Text) <= 0) AndAlso _
                        (txtWaitingPeriodMonths.Text = String.Empty OrElse Convert.ToInt32(txtWaitingPeriodMonths.Text) <= 0) AndAlso _
                        (txtWaitingPeriodDays.Text = String.Empty OrElse Convert.ToInt32(txtWaitingPeriodDays.Text) <= 0) Then
                        isExclusionValid = False
                    Else
                        Dim cmbExclusionType As ASPxComboBox = DirectCast(rpExclusion.FindControl("cmbExclusionType"), ASPxComboBox)
                        Dim cmbCover As ASPxComboBox = DirectCast(rpExclusion.FindControl("cmbCover"), ASPxComboBox)
                        Dim cmbIllness As ASPxComboBox = DirectCast(rpExclusion.FindControl("cmbIllness"), ASPxComboBox)

                        e.NewValues("ExclusionType") = cmbExclusionType.Value
                        e.NewValues("ExclusionPeriodType") = cmbExclusionPeriodType.Value
                        e.NewValues("Coverage") = cmbCover.Value
                        e.NewValues("ImpairmentCode") = cmbIllness.Value
                        e.NewValues("WaitingPeriodYears") = Convert.ToInt32(txtWaitingPeriodYears.Text)
                        e.NewValues("WaitingPeriodMonths") = Convert.ToInt32(txtWaitingPeriodMonths.Text)
                        e.NewValues("WaitingPeriodDays") = Convert.ToInt32(txtWaitingPeriodDays.Text)

                        e.NewValues("FlatExtraPremium") = 0
                        e.NewValues("ExclusionPeriodType") = 0
                        e.NewValues("DurationOfFlatExtraPremiumYears") = 0
                        e.NewValues("DurationOfFlatExtraPremiumMonths") = 0
                        e.NewValues("DurationOfFlatExtraPremiumDays") = 0
                    End If
                End If
            End If

            If Not isExclusionValid Then
                e.Cancel = True
                Throw New ArgumentException("Al menos uno de los valores de 'Duración Plazo' debe contener un valor")
            ElseIf Not isFlatValid Then
                e.Cancel = True
                Throw New ArgumentException("Al menos uno de los valores de 'Duración Flat Extra Prima' debe contener un valor")
            Else
                Dim memo As ASPxMemo = DirectCast(DirectCast(sender, ASPxGridView).FindEditFormTemplateControl("ppEditExplainRule").FindControl("mmExplainRule"), ASPxMemo)
                e.NewValues("Explanation") = memo.Text
            End If
        End If
    End Sub

    Protected Sub UnderwritingGridView_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles UnderwritingGridView.StartRowEditing
        DirectCast(sender, ASPxGridView).DetailRows.CollapseRowByKey(e.EditingKeyValue)
    End Sub

    Protected Sub UnderwritingGridView_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles UnderwritingGridView.InitNewRow
        DirectCast(sender, ASPxGridView).DetailRows.CollapseAllRows()
    End Sub

    Protected Sub UnderwritingGridView_CancelRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles UnderwritingGridView.CancelRowEditing
        If Not DirectCast(sender, ASPxGridView).IsNewRowEditing Then
            DirectCast(sender, ASPxGridView).DetailRows.ExpandRowByKey(e.EditingKeyValue)
        End If
    End Sub

    Protected Sub UnderwritingGridView_RowUpdated(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatedEventArgs) Handles UnderwritingGridView.RowUpdated
        DirectCast(sender, ASPxGridView).DetailRows.ExpandRowByKey(e.Keys(0))
    End Sub

    Protected Sub UnderwritingGridView_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles UnderwritingGridView.CustomErrorText
        If Not e.Exception.InnerException Is Nothing Then
            e.ErrorText = e.Exception.InnerException.Message
        End If
    End Sub

    Protected Sub UnderwritingGridView_DataBound(sender As Object, e As System.EventArgs) Handles UnderwritingGridView.DataBound
        If Not String.IsNullOrEmpty(_masterKeyValue) Then
            Dim grid As ASPxGridView = DirectCast(sender, ASPxGridView)
            grid.FocusedRowIndex = grid.FindVisibleIndexByKeyValue(_masterKeyValue)
            If grid.FocusedRowIndex <> -1 Then
                grid.DetailRows.ExpandRow(grid.FocusedRowIndex)
            End If

            _masterKeyValue = String.Empty
        End If
    End Sub

    Protected Sub UnderwritingGridView_HtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles UnderwritingGridView.HtmlRowCreated
        If e.RowType = GridViewRowType.Data Then
            Dim col As GridViewDataColumn = TryCast(TryCast(sender, ASPxGridView).Columns("Explanation"), GridViewDataColumn)
            Dim button As ASPxButton = TryCast(sender, ASPxGridView).FindRowCellTemplateControl(e.VisibleIndex, col, "btnLinkToWorkflow")

            ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(button)

            If Not TryCast(sender, ASPxGridView).GetRowValues(e.VisibleIndex, "Explanation") Is Nothing AndAlso
                TryCast(sender, ASPxGridView).GetRowValues(e.VisibleIndex, "Explanation").ToString() <> "" Then
                If Not button Is Nothing Then
                    button.ClientSideEvents.Click = String.Format("function(s,e){{ mmGridExplainRule.SetText('{0}'); ppGridEditExplainRule.Show(); }}",
                                                                  TryCast(sender, ASPxGridView).GetRowValues(e.VisibleIndex, "Explanation"))
                    mmGridExplainRule.ReadOnly = True
                End If
            Else
                button.Cursor = "auto"
                button.Enabled = False
            End If
        End If
    End Sub

    Protected Sub cmbStatus_DataBinding(sender As Object, e As System.EventArgs)
        Dim bbStatus As ASPxComboBox = TryCast(sender, ASPxComboBox)
        'TODO Falta metodo lkp para los estados de la regla
        bbStatus.DataSource = Nothing 'InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingAreaTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbUnderwritingArea_DataBinding(sender As Object, e As System.EventArgs)
        Dim bbUnderwritingArea As ASPxComboBox = TryCast(sender, ASPxComboBox)
        bbUnderwritingArea.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.UnderwritingAreaTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbExclusionType_DataBinding(sender As Object, e As System.EventArgs)
        Dim bbExclusionType As ASPxComboBox = TryCast(sender, ASPxComboBox)
        bbExclusionType.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.ExclusionTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbIllness_DataBinding(sender As Object, e As System.EventArgs)
        Dim bbIllness As ASPxComboBox = TryCast(sender, ASPxComboBox)
        bbIllness.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.IllnessTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbExclusionPeriodType_DataBinding(sender As Object, e As System.EventArgs)
        Dim bbExclusionPeriodType As ASPxComboBox = TryCast(sender, ASPxComboBox)
        bbExclusionPeriodType.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.ExclusionPeriodTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbAlarmType_DataBinding(sender As Object, e As System.EventArgs)
        Dim bbAlarmType As ASPxComboBox = TryCast(sender, ASPxComboBox)
        bbAlarmType.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.AlarmTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub cmbFlatPeriodType_DataBinding(sender As Object, e As System.EventArgs)
        Dim bbbFlatPeriodType As ASPxComboBox = TryCast(sender, ASPxComboBox)
        bbbFlatPeriodType.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.ExclusionPeriodTypeLkp(Session("LanguageID"), False)
    End Sub

    Protected Sub UnderwritingGridView_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewCommandButtonEventArgs) Handles UnderwritingGridView.CommandButtonInitialize
        If Not Session("IsEditMode") Then
            e.Visible = False
        End If
    End Sub

    Protected Sub UnderwritingGridView_BeforePerformDataSelect(sender As Object, e As System.EventArgs) Handles UnderwritingGridView.BeforePerformDataSelect
        Dim rowIndex As Integer = DirectCast(DirectCast(sender, ASPxGridView).NamingContainer.NamingContainer.NamingContainer.NamingContainer, ASPxGridView).FocusedRowIndex
        Dim keyFieldValue As Object = DirectCast(DirectCast(sender, ASPxGridView).NamingContainer.NamingContainer.NamingContainer.NamingContainer, ASPxGridView).GetRowValues(rowIndex, "RequirementID")

        InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingCase.SetCurrentRequirementID(keyFieldValue)
    End Sub

#End Region

#Region "Methods"

    Public Sub RebindGridView()
        UnderwritingGridView.DataBind()
    End Sub

    ''' <summary>
    ''' Adds errors descriptions to the errors collection
    ''' </summary>
    ''' <param name="errors">Sets the specific error collection</param>
    ''' <param name="text">Sets the textbox control</param>
    ''' <param name="errorText">Sets the error text</param>
    ''' <remarks></remarks>
    Private Sub AddError(ByVal errors As Dictionary(Of ASPxTextBox, String), text As ASPxTextBox, errorText As String)
        If Not errors.ContainsKey(text) Then
            errors(text) = errorText
        End If
    End Sub

    ''' <summary>
    ''' Adds errors descriptions to the errors collection
    ''' </summary>
    ''' <param name="errors">Sets the specific error collection</param>
    ''' <param name="combo">Sets the combobox control</param>
    ''' <param name="errorText">Sets the error text</param>
    ''' <remarks></remarks>
    Private Sub AddError(ByVal errors As Dictionary(Of ASPxComboBox, String), combo As ASPxComboBox, errorText As String)
        If Not errors.ContainsKey(combo) Then
            errors(combo) = errorText
        End If
    End Sub

    ''' <summary>
    ''' Adds errors descriptions to the errors collection
    ''' </summary>
    ''' <param name="errors">Sets the specific error collection</param>
    ''' <param name="dropDown">Sets the dropdown edit control</param>
    ''' <param name="errorText">Sets the error text</param>
    ''' <remarks></remarks>
    Private Sub AddError(ByVal errors As Dictionary(Of ASPxDropDownEdit, String), dropDown As ASPxDropDownEdit, errorText As String)
        If Not errors.ContainsKey(dropDown) Then
            errors(dropDown) = errorText
        End If
    End Sub

    ''' <summary>
    ''' Sets the grid focused row after the call back is called
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SynchronizeFocusedRow(gridName As String, dropDownName As String)
        Dim dropDown As ASPxDropDownEdit = DirectCast(UnderwritingGridView.FindEditFormTemplateControl(dropDownName), ASPxDropDownEdit)
        Dim grid As ASPxGridView = CType(dropDown.FindControl(gridName), ASPxGridView)
        Dim lookupKeyValue As Object = dropDown.KeyValue
        grid.FocusedRowIndex = grid.FindVisibleIndexByKeyValue(lookupKeyValue)
    End Sub

#End Region
End Class
