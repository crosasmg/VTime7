Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports GIT.Core
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler.Localization
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.Common.Extensions



Partial Public Class AppointmentGITForm
    Inherits SchedulerFormControl

    Public ReadOnly Property CanShowReminders() As Boolean
        Get
            Return (CType(Parent, AppointmentFormTemplateContainer)).Control.Storage.EnableReminders
        End Get
    End Property

    Public ReadOnly Property IsTask() As Boolean
        Get
            Return CType(Parent, SchedulerContainer).Appointment.CustomFields("RecordType") = 1 ' enumRecordType.Task
        End Get
    End Property

    Public ReadOnly Property ResourceSharing() As Boolean
        Get
            Return (CType(Parent, AppointmentFormTemplateContainer)).Control.Storage.ResourceSharing
        End Get
    End Property

    Public ReadOnly Property ResourceDataSource() As IEnumerable
        Get
            Return (CType(Parent, AppointmentFormTemplateContainer)).ResourceDataSource
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        PrepareChildControls()
        tbSubject.Focus()
    End Sub

    Private Function CompanyIdSelect() As Integer
        Dim Result As Integer = 1
        If Not IsNothing(ConfigurationManager.AppSettings("BackOffice.IsMultiCompany")) AndAlso
           (Boolean.Parse(ConfigurationManager.AppSettings("BackOffice.IsMultiCompany").ToString) = True) Then
            If Not IsNothing(HttpContext.Current) Then
                If Not IsNothing(HttpContext.Current.Session) Then
                    Result = HttpContext.Current.Session("CompanyId")
                End If
            End If
        End If
        Return Result
    End Function

    Public Overrides Sub DataBind()
        MyBase.DataBind()

        Dim container As SchedulerContainer = CType(Parent, SchedulerContainer)

        Dim apt As Appointment = container.Appointment
        Dim Row As Object = apt.GetRow((CType(Parent, AppointmentFormTemplateContainer)).Control.Storage)
        Dim TaskID As String = String.Empty
        Dim TaskInstance As InMotionGIT.Agenda.Contracts.Task = Nothing

        PopulateResourceEditors(apt, container)

        If Not Row Is Nothing Then
            TaskID = Row.Item("TaskId").ToString
        End If

        If Not String.IsNullOrEmpty(TaskID) Then
            TaskInstance = InMotionGIT.Agenda.Proxy.Manager.RetrieveTaskByTaskId(TaskID)
        End If

        AppointmentRecurrenceForm1.Visible = container.ShouldShowRecurrence

        'VisualTimeTransaction
        cbTransaction.SelectedItem = cbTransaction.Items.FindByValue(apt.CustomFields("VisualTimeTransaction"))

        'Priority
        If Not String.IsNullOrEmpty(apt.CustomFields("Priority")) Then
            cbPriority.SelectedItem = cbPriority.Items.FindByValue(apt.CustomFields("Priority").ToString)
        End If

        'TaskStatus
        If Not String.IsNullOrEmpty(apt.CustomFields("Status")) Then
            cbTaskStatus.SelectedItem = cbTaskStatus.Items.FindByValue(apt.CustomFields("Status").ToString)
        Else
            cbTaskStatus.ClientEnabled = False
            cbTaskStatus.SelectedItem = cbTaskStatus.Items.FindByValue("1") 'TaskManagerService.EnumStatus.Pending
        End If

        'RecordType
        If Not String.IsNullOrEmpty(apt.CustomFields("RecordType")) Then
            cbRecordType.SelectedItem = cbRecordType.Items.FindByValue(apt.CustomFields("RecordType").ToString)
        Else
            If Session("IsNewToDoTask") = True Then
                'enumRecordType.Task
                cbRecordType.SelectedItem = cbRecordType.Items.FindByValue("1")
            Else
                'enumRecordType.Event
                cbRecordType.SelectedItem = cbRecordType.Items.FindByValue("2")
            End If
        End If

        btnOk.ClientSideEvents.Click = container.SaveHandler
        btnCancel.ClientSideEvents.Click = container.CancelHandler
        btnDelete.ClientSideEvents.Click = container.DeleteHandler
        btnCallVT.Enabled = False

        '+ Cuando es una tarea (TODO) no se muestran los controles de las fechas y la seccion de la Alarma
        If Session("IsNewToDoTask") = True Then
            edtStartDate.Date = Date.MinValue
            edtEndDate.Date = Date.MinValue
        End If

        '+ Se evalúa si la tarea de la agenda debe ser completada mediante una transacción de VisualTime
        If apt.CustomFields("VisualTimeTransaction") IsNot Nothing AndAlso Not apt.CustomFields("VisualTimeTransaction").GetType.Name.Equals("DBNull") Then

            '+ Se genera el URL para la transacción indicada
            Dim lstrUrl As String

            Using service As MenuService.MenuClient = New MenuService.MenuClient()
                With service
                    lstrUrl = .MakeURL(apt.CustomFields("VisualTimeTransaction"), CompanyIdSelect)
                    .Close()
                End With
            End Using

            Dim TransactionSource As GIT.EDW.Query.Model.EnumTransactionSource = GIT.EDW.Query.Model.EnumTransactionSource.VisualTIME
            If String.IsNullOrEmpty(lstrUrl) Then TransactionSource = GIT.EDW.Query.Model.EnumTransactionSource.DesignerForm

            '+ Se arma la cadena con los parametros necesarios para el llamado de la transacción
            Dim Params As String = String.Empty

            Dim taskElementCollection As List(Of InMotionGIT.Agenda.Contracts.TaskElement) = InMotionGIT.Agenda.Proxy.Manager.RetrieveTaskElements(TaskID)

            For Each taskElementInstance As InMotionGIT.Agenda.Contracts.TaskElement In taskElementCollection
                If Params.Length > 0 Then Params += "&"
                Params += taskElementInstance.ElementName.ToString + "=" + taskElementInstance.ElementValue.ToString
            Next

            If TransactionSource = GIT.EDW.Query.Model.EnumTransactionSource.VisualTIME Then
                '+ Se coloca el llamado a la función que abre la ventana de VisualTime
                btnCallVT.ClientSideEvents.Click = "function() { insGoTo('" & lstrUrl & "&TaskID=" & TaskID & "&" + Params & "'); }"
            Else
                '+ Se coloca el llamado a la función que abre la ventana del DesignerForm
                lstrUrl = "/generated/" & apt.CustomFields("VisualTimeTransaction") & ".aspx?TaskID=" + TaskID & "&Action=" & TaskInstance.CompletedAction.ToString & "&" & Params & "&Comment=y"
                btnCallVT.ClientSideEvents.Click = "function() { insGoToDesignerForm('" & lstrUrl & "'); }"
            End If

            btnCallVT.Enabled = True

        End If

        '+ Se hace el manejo para el llamado de la ventana que muestra la imagen del WorkFlow
        '+ en caso de que sea necesario
        If Not IsNothing(TaskInstance) Then
            imgWorkFlow.Visible = (TaskInstance.OriginTypeEnum = InMotionGIT.Agenda.Contracts.Enumerations.EnumOriginType.Workflow)
        Else
            imgWorkFlow.Visible = False
        End If


        If Not IsNothing(TaskInstance) Then
            If TaskInstance.OriginTypeEnum = InMotionGIT.Agenda.Contracts.Enumerations.EnumOriginType.Workflow Then
                Dim ParameterWorkFlowArray() As String = TaskInstance.OriginatedBy.ToString.Split("(")
                If ParameterWorkFlowArray.Length = 2 Then
                    imgWorkFlow.ClientSideEvents.Click = "function() { showWorkflow('" & ParameterWorkFlowArray(0) + "','" + ParameterWorkFlowArray(1).Replace(")", "") & "'); }"
                End If
            End If

            ' TODO: MULTIPLES RESOURCES
            If TaskInstance.TaskOwners.Count > 0 Then
                edtResource.Value = TaskInstance.TaskOwners(0).UserId.ToString
            End If
        End If

        edtLabel.SelectedIndex = apt.LabelId
        edtStatus.SelectedIndex = apt.StatusId
        tbLocation.Text = apt.Location

        'WarningWhenCompleted
        If Not String.IsNullOrEmpty(apt.CustomFields("WarningWhenCompleted")) Then
            WarningWhenCompleted.Checked = apt.CustomFields("WarningWhenCompleted")
        End If

        'PercentageCompleted
        If Not String.IsNullOrEmpty(apt.CustomFields("PercentageCompleted")) Then
            tbPerCompleted.Value = apt.CustomFields("PercentageCompleted")
        End If

        'RepeatActive
        If Not String.IsNullOrEmpty(apt.CustomFields("RepeatActive")) Then
            RepeatActive.Checked = apt.CustomFields("RepeatActive")
        End If

        'RepeatStartingDate
        If Not String.IsNullOrEmpty(apt.CustomFields("RepeatStartingDate")) Then
            edtRepeatStatingDate.Value = apt.CustomFields("RepeatStartingDate")
        End If

        'RepeatEndingDate
        If Not String.IsNullOrEmpty(apt.CustomFields("RepeatEndingDate")) Then
            edtRepeatEndingDate.Value = apt.CustomFields("RepeatEndingDate")
        End If

        'RepeatFrequency
        If Not String.IsNullOrEmpty(apt.CustomFields("RepeatFrequency")) Then
            tbRepeatFrecuency.SelectedItem = tbRepeatFrecuency.Items.FindByValue(apt.CustomFields("RepeatFrequency").ToString)
        End If

        'RepeatTimes
        If Not String.IsNullOrEmpty(apt.CustomFields("RepeatTimes")) Then
            tbRepeatTimes.Value = apt.CustomFields("RepeatTimes")
        End If

        If (Not Object.Equals(apt.ResourceId, Resource.Empty.Id)) Then
            edtResource.Value = apt.ResourceId.ToString()
        Else
            edtResource.Value = SchedulerIdHelper.EmptyResourceId
        End If

        If container.Appointment.HasReminder Then
            cbReminder.Value = container.Appointment.Reminder.TimeBeforeStart.ToString()
            chkReminder.Checked = True
        Else
            cbReminder.ClientEnabled = False
        End If

    End Sub

    Protected Overrides Sub PrepareChildControls()
        Dim container As AppointmentFormTemplateContainer = CType(Parent, AppointmentFormTemplateContainer)
        Dim control As ASPxScheduler = container.Control

        AppointmentRecurrenceForm1.EditorsInfo = New EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons)
        MyBase.PrepareChildControls()
    End Sub
    Protected Overrides Function GetChildEditors() As ASPxEditBase()
        'Dim edits() As ASPxEditBase = {lblSubject, tbSubject, lblStartDate, edtStartDate, lblEndDate, edtEndDate, chkAllDay, tbDescription, lblTransaction, cbTransaction, lblPriority, cbPriority, lblTaskStatus, cbTaskStatus, lblPercentageCompleted, tbPercentageCompleted}
        Dim edits() As ASPxEditBase = {lblSubject, tbSubject, lblStartDate, edtStartDate, lblEndDate, edtEndDate, lblLocation, tbLocation, lblLabel, edtLabel, lblStatus, edtStatus, lblResource, edtResource, cbReminder, chkAllDay, tbDescription, lblTransaction, cbTransaction, lblPriority, cbPriority, lblTaskStatus, cbTaskStatus, lblPerCompleted, tbPerCompleted, lblRepeatStatingDate, edtRepeatStatingDate, lblRepeatStatingDate, edtRepeatEndingDate, lblRepeatFrecuency, tbRepeatFrecuency, lblRepeatTimes, tbRepeatTimes, lblVisualTimeTransactionAction, cbVisualTimeTransactionAction, IndividualTaskIndicator, WarningWhenCompleted, cbRecordType}
        Return edits
    End Function
    Protected Overrides Function GetChildButtons() As ASPxButton()
        Dim buttons() As ASPxButton = {btnOk, btnCancel, btnDelete, btnCallVT}
        Return buttons
    End Function

    Private Sub PopulateResourceEditors(ByVal apt As Appointment, ByVal container As AppointmentFormTemplateContainer)
        If ResourceSharing Then
            ddResource.JSProperties.Clear()
            Dim edtMultiResource As ASPxListBox = TryCast(ddResource.FindControl("edtMultiResource"), ASPxListBox)
            If edtMultiResource Is Nothing Then
                Return
            End If
            SetListBoxSelectedValues(edtMultiResource, apt.ResourceIds)
            Dim multiResourceString As List(Of String) = GetListBoxSeletedItemsText(edtMultiResource)
            Dim stringResourceNone As String = SchedulerLocalizer.GetString(SchedulerStringId.Caption_ResourceNone)
            ddResource.Value = stringResourceNone
            If multiResourceString.Count > 0 Then
                ddResource.Value = String.Join(", ", multiResourceString.ToArray())
            End If
            ddResource.JSProperties.Add("cp_Caption_ResourceNone", stringResourceNone)
        Else
            If (Not Object.Equals(apt.ResourceId, Resource.Empty.Id)) Then
                edtResource.Value = apt.ResourceId.ToString()
            Else
                edtResource.Value = SchedulerIdHelper.EmptyResourceId
            End If
        End If
    End Sub

    Private Function GetListBoxSeletedItemsText(ByVal listBox As ASPxListBox) As List(Of String)
        Dim result As New List(Of String)()
        For Each editItem As ListEditItem In listBox.Items
            If editItem.Selected Then
                result.Add(editItem.Text)
            End If
        Next editItem
        Return result
    End Function
    Private Sub SetListBoxSelectedValues(ByVal listBox As ASPxListBox, ByVal values As IEnumerable)
        listBox.Value = Nothing

        Dim count As Integer = 0
        Dim selected As Boolean = False
        Dim item As ListEditItem = Nothing
        For Each value As Object In values
            count += 1
            item = listBox.Items.FindByValue(value.ToString())
            If item IsNot Nothing Then
                item.Selected = True
                selected = True
            End If
        Next value
        If count = 1 AndAlso Not selected Then
            item = listBox.Items.FindByValue((New InMotionGIT.Membership.Providers.MemberContext).User.ProviderUserKey.ToString)
            If item IsNot Nothing Then
                item.Selected = True
                selected = True
            End If
        End If
    End Sub

End Class
