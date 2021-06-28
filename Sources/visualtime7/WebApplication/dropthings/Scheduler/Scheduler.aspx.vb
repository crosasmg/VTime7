#Region "using"

Imports System.IO
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxUploadControl
Imports DevExpress.XtraScheduler.iCalendar
Imports GIT.Core
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Support

#End Region

Partial Class Scheduler
    Inherits PageBase

    'Protected Sub MainScheduler_AppointmentInserting(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.PersistentObjectCancelEventArgs) Handles MainScheduler.AppointmentInserting
    '    Dim storage As ASPxSchedulerStorage = DirectCast(sender, ASPxSchedulerStorage)
    '    Dim apt As Appointment = DirectCast(e.Object, Appointment)

    '    storage.SetAppointmentId(apt, Guid.NewGuid().ToString) ' apt.GetHashCode()
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If UserInfo.User.AllowScheduler Then

            SetupMappings()
            ResourceFiller.FillResources(MainScheduler.Storage, 3)

            MainScheduler.AppointmentDataSource = EventsDataSource
            MainScheduler.DataBind()

            InitialSetup()

            If Request.QueryString.Count = 1 AndAlso Request.QueryString(0) = "Scheduler.ics" AndAlso MainScheduler.Storage.Appointments.Count > 0 Then
                PostCalendarFile()
            ElseIf Request.QueryString.Count = 1 AndAlso Request.QueryString(0) = "Scheduler.ics" AndAlso Not MainScheduler.Storage.Appointments.Count > 0 Then
                lblMessage.Text = Me.GetLocalResourceObject("NoAppointmentsMessage").ToString()
                popupMessage.ShowOnPageLoad = True
            End If

            If Not IsCallback And Not IsPostBack Then
                UserFilter.DataSource = InMotionGIT.FrontOffice.Proxy.Helpers.UsersAndGrups.RetrieveUsersAndGroupsList
                UserFilter2.DataSource = InMotionGIT.FrontOffice.Proxy.Helpers.UsersAndGrups.RetrieveUsersAndGroupsList
                StatusFilter.DataSource = InMotionGIT.Agenda.Proxy.Manager.RetrieveTaskStatusLookUp(LanguageId)
                StatusFilter2.DataSource = InMotionGIT.Agenda.Proxy.Manager.RetrieveTaskStatusLookUp(LanguageId)

                StatusFilter.DataBind()
                StatusFilter.Items.FindByValue(2).Selected = True
                UserFilter.DataBind()

                StatusFilter2.DataBind()
                StatusFilter2.Items.FindByValue(2).Selected = True
                UserFilter2.DataBind()
            End If
        Else
            If IsCallback Then
                DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/dropthings/Error.aspx?id=GEN9001")
            Else
                Response.Redirect("~/dropthings/Error.aspx?id=GEN9001")
            End If
        End If

    End Sub

    Private Sub PostCalendarFile()
        Dim exporter As New iCalendarExporter(MainScheduler.Storage)
        Dim memoryStream As New MemoryStream()
        exporter.Export(memoryStream)
        memoryStream.WriteTo(Response.OutputStream)
        Response.ContentType = "text/calendar"
        Response.AddHeader("Content-Disposition", "attachment; filename=Scheduler.ics")
        Response.End()
    End Sub

    Protected Sub MainScheduler_AfterExecuteCallbackCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs) Handles MainScheduler.AfterExecuteCallbackCommand

        Dim IsNewToDoTask As Boolean = False

        Try
            IsNewToDoTask = HiddenTODO("NewTask")
        Catch ex As Exception
            IsNewToDoTask = False
        End Try

        Session("IsNewToDoTask") = IsNewToDoTask

        'Si se esta salvado una tarea (TODO) se debe refrescar el control de calendario
        If (e.CommandId = SchedulerCallbackCommandId.AppointmentSave Or
            e.CommandId = SchedulerCallbackCommandId.AppointmentDelete) And
            IsNewToDoTask Then
            MainScheduler.JSProperties.Add("cp_CommandSave", True)
            'Inicializo la variable de sesion
            Session("IsNewToDoTask") = False
            MainScheduler.DataBind()
        End If
    End Sub

    Protected Sub MainScheduler_AppointmentFormShowing(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.AppointmentFormEventArgs) Handles MainScheduler.AppointmentFormShowing
        e.Container = New SchedulerContainer(CType(sender, ASPxScheduler))
        If Session("IsNewToDoTask") Then
            e.Container.Caption = GetLocalResourceObject("TaskCaption").ToString()
        Else
            e.Container.Caption = GetLocalResourceObject("AppointmentCaption").ToString()
        End If
    End Sub

    Protected Sub MainScheduler_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs) Handles MainScheduler.BeforeExecuteCallbackCommand

        Select Case e.CommandId
            Case SchedulerCallbackCommandId.AppointmentSave
                e.Command = New SchedulerSaveCallbackCommand(CType(sender, ASPxScheduler))
            Case ImportAppointmentsCallbackCommand.CommandId
                e.Command = New ImportAppointmentsCallbackCommand(CType(sender, ASPxScheduler))
        End Select

    End Sub

    Private Function CompanyIdSelect() As Integer
        Dim Result As Integer = 1
        If ConfigurationManager.AppSettings("BackOffice.CompanyDefault").IsNotEmpty() Then
            Result = ConfigurationManager.AppSettings("BackOffice.CompanyDefault")
        End If
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

    Protected Function link_Load(ByVal VisibleIndex As Integer) As String
        Dim VisualTimeTransaction As String = String.Empty
        Try

            TaskGridView.JSProperties.Clear()

            Dim TaskID As String = TaskGridView.GetRowValues(VisibleIndex, "TaskId")
            VisualTimeTransaction = TaskGridView.GetRowValues(VisibleIndex, "VisualTimeTransaction")
            Dim CompletedAction As String = TaskGridView.GetRowValues(VisibleIndex, "CompletedAction")
            Dim sLink As String = String.Empty
            Dim TransactionSource As GIT.EDW.Query.Model.EnumTransactionSource = GIT.EDW.Query.Model.EnumTransactionSource.VisualTIME
            Dim lstrUrl As String = String.Empty
            Dim Params As String = String.Empty

            '+ Se evalua si la tarea de la agenda debe ser completada mediante una transacción de VisualTime
            If Not String.IsNullOrEmpty(VisualTimeTransaction) Then

                '+ Se genera el URL para la transacción indicada
                Using service As MenuService.MenuClient = New MenuService.MenuClient()
                    With service
                        lstrUrl = .MakeURL(VisualTimeTransaction, CompanyIdSelect)
                        .Close()
                    End With
                End Using

                '+ Si la variable lstrUrl está vacía significa que la transacción no es de VisualTime
                If String.IsNullOrEmpty(lstrUrl) Then TransactionSource = GIT.EDW.Query.Model.EnumTransactionSource.DesignerForm

                '+ Se arma la cadena con los parámetros necesarios para el llamado de la transacción
                Dim taskElementCollection As List(Of InMotionGIT.Agenda.Contracts.TaskElement) = InMotionGIT.Agenda.Proxy.Manager.RetrieveTaskElements(TaskID)

                For Each taskElementInstance As InMotionGIT.Agenda.Contracts.TaskElement In taskElementCollection
                    Params += "&"
                    If TransactionSource = GIT.EDW.Query.Model.EnumTransactionSource.VisualTIME Then
                        Params += "lnk"
                    End If
                    Params += taskElementInstance.ElementName.ToString + "=" + taskElementInstance.ElementValue.ToString
                Next

                If TransactionSource = GIT.EDW.Query.Model.EnumTransactionSource.VisualTIME Then
                    '+ Se coloca el llamado a la función que abre la ventana de VisualTime
                    sLink = "javascript:insGoTo('" & ConfigurationManager.AppSettings("Url.BackOffice") & lstrUrl & "&LinkFront=1&TaskID=" & TaskID & Params & "'); "
                    Dim modules As MenuService.MenuInformationList

                    Using service As MenuService.MenuClient = New MenuService.MenuClient()
                        With service
                            modules = .FullWindowsList(VisualTimeTransaction, Session("sSche_Code"), Session("CompanyId"))
                            .Close()
                        End With
                    End Using
                    If Not IsNothing(modules) Then
                        VisualTimeTransaction = modules(0).Description
                    End If
                Else
                    '+ Se coloca el llamado a la función que abre la ventana del EDW
                    Dim name As String = String.Empty
                    Dim title As String = String.Empty

                    InMotionGIT.Workbench.Deploy.DeploySupport.GetLocalModelInformation(VisualTimeTransaction, "form", LanguageId, name, title)

                    If Not String.IsNullOrEmpty(name) Then
                        lstrUrl = "/generated/form/" & name & ".aspx?TaskID=" + TaskID & "&Action=" & CompletedAction & Params & "&Comment=y"
                        VisualTimeTransaction = title
                    Else
                        lstrUrl = String.Format("Form Id {0} not found", VisualTimeTransaction)
                    End If

                    sLink = lstrUrl
                End If

                TaskGridView.JSProperties.Add("cp_VisualTimeTransaction", VisualTimeTransaction)
            Else
                TaskGridView.JSProperties.Add("cp_VisualTimeTransaction", GetLocalResourceObject("NoneMessageResource").ToString())
                TaskGridView.JSProperties.Add("cp_Message", GetLocalResourceObject("TransactionMessageResource").ToString())
                sLink = "javascript:popupMessageShow();"
            End If

            link_Load = sLink

            Select Case CompletedAction
                Case "2"
                    ReviewBtn.Visible = True
                Case "3"
                    AcceptBtn.Visible = True
                    DenyBtn.Visible = True
                Case "4"
                    AcceptButton.Visible = True
                    CancelBtn.Visible = True
            End Select
        Catch ex As InMotionGIT.Common.Exceptions.InMotionGITException
            TaskGridView.JSProperties.Add("cp_VisualTimeTransaction", ex.Message)
        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Scheduler", "link_Load - Transaction(" & VisualTimeTransaction & ")", ex)
            TaskGridView.JSProperties.Add("cp_VisualTimeTransaction", "Technical error loading the options")
        End Try
    End Function

    Private Sub UpdateTaskStatus(ByVal VisibleIndex As Integer,
                                 ByVal status As InMotionGIT.Agenda.Contracts.Enumerations.EnumStatus,
                                 ByVal Response As String)
        Dim TaskID As String = TaskGridView.GetRowValues(VisibleIndex, "TaskId")

        Dim taskItem As InMotionGIT.Agenda.Contracts.Task = InMotionGIT.Agenda.Proxy.Manager.UpdateTaskStatus(TaskID, status, UserInfo.User.ProviderUserKey.ToString, LanguageHelper.CurrentCultureToLanguage)

        If taskItem.IsNotEmpty AndAlso
           taskItem.OriginTypeEnum = InMotionGIT.Agenda.Contracts.Enumerations.EnumOriginType.Workflow AndAlso
           status = InMotionGIT.Agenda.Contracts.Enumerations.EnumStatus.Completed Then
            InMotionGIT.Workflow.Support.Runtime.Resume(taskItem.OriginatedBy, Response)
        End If

        TaskGridView.DataBind()
    End Sub

    Private Sub DeleteTask(ByVal VisibleIndex As Integer)

        Dim TaskID As String = TaskGridView.GetRowValues(VisibleIndex, "TaskId")
        InMotionGIT.Agenda.Proxy.Manager.DeleteTask(TaskID, UserInfo.User.ProviderUserKey.ToString)
        TaskGridView.DataBind()

    End Sub

    Protected Sub TaskGridView_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles TaskGridView.CustomCallback

        Dim sParameterArray() As String = e.Parameters.ToString.Split("-")

        Select Case sParameterArray(0)
            Case "Status"
                UpdateTaskStatus(sParameterArray(1), sParameterArray(2), sParameterArray(3))

            Case "Link"
                TaskGridView.JSProperties.Add("cp_link", link_Load(sParameterArray(1)))

            Case "DataBind"
                TaskGridView.DataBind()

            Case "Delete"
                DeleteTask(sParameterArray(1))
            Case Else
                Return
        End Select

    End Sub

    Protected Sub EventsDataSource_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles EventsDataSource.Selected
        If Not e.Exception Is Nothing Then
            ErrorMsgLabel.Text = "The Agenda's database connection is failing. Refresh the page and try again."
            ErrorMsgLabel.Visible = True
            e.ExceptionHandled = True
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Scheduler_EventsDataSource:", "EventsDataSource_Selected", e.Exception)
        Else
            ErrorMsgLabel.Visible = False
        End If
    End Sub

    Protected Sub TaskDataSource_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles TaskDataSource.Selected
        If Not e.Exception Is Nothing Then
            ErrorMsgLabel.Text = "The Agenda's database connection is failing. Refresh the page and try again."
            ErrorMsgLabel.Visible = True
            e.ExceptionHandled = True
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Scheduler_TaskDataSource:", "TaskDataSource_Selected", e.Exception)
        Else
            ErrorMsgLabel.Visible = False
        End If
    End Sub

    Protected Sub MainScheduler_PreparePopupMenu(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.PreparePopupMenuEventArgs) Handles MainScheduler.PreparePopupMenu
        'Dim menu As ASPxSchedulerPopupMenu = e.Menu
        'If menu.Id.Equals(SchedulerMenuItemId.AppointmentMenu) Then
        '    Dim item As New DevExpress.Web.ASPxMenu.MenuItem("Export", "ExportAppointment")
        '    e.Menu.Items.Insert(1, item)
        '    e.Menu.ClientSideEvents.ItemClick = "function(s, e) { OnMenuClick(s,e); }"
        'End If
    End Sub

    Protected Sub ucUploadCalendar_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs) Handles ucUploadCalendar.FileUploadComplete
        Dim uploadControl As ASPxUploadControl = CType(sender, ASPxUploadControl)
        Dim uploadedFile As UploadedFile = uploadControl.UploadedFiles(0)
        If (Not IsFileNameCorrect(uploadedFile.FileName)) Then
            e.IsValid = False
            e.ErrorText = Me.GetLocalResourceObject("InvalidFileTypeMessage").ToString()
            Return
        End If
        If uploadedFile.IsValid Then
            Session("UploadedFile") = GetBytes(uploadedFile.FileContent)
        Else
            Session("UploadedFile") = Nothing
        End If
    End Sub

    Private Function GetBytes(ByVal stream As Stream) As Byte()
        stream.Position = 0
        Dim buf(stream.Length - 1) As Byte
        stream.Read(buf, 0, CInt(Fix(stream.Length)))
        Return buf
    End Function

    Private Function IsFileNameCorrect(ByVal fileName As String) As Boolean
        Dim fileInfo As New FileInfo(fileName)
        Return fileInfo.Extension = ".ics"
    End Function

    Private Class ImportAppointmentsCallbackCommand
        Inherits SchedulerCallbackCommand
        Public Const CommandId As String = "IMPRTAPT"

        Public Sub New(ByVal control As ASPxScheduler)
            MyBase.New(control)
        End Sub

        Public Overrides ReadOnly Property Id() As String
            Get
                Return CommandId
            End Get
        End Property

        Protected Overrides Sub ParseParameters(ByVal parameters As String)
            ' do nothing
        End Sub

        Protected Overrides Sub ExecuteCore()
            Dim stream As Stream = GetStream()
            If stream Is Nothing Then
                Return
            End If
            Dim page As Scheduler = TryCast(Control.Page, Scheduler)
            If page IsNot Nothing AndAlso page.chkDelete.Checked Then
                Control.Storage.Appointments.Clear()
            End If
            Dim importer As New iCalendarImporter(Control.Storage)
            importer.Import(stream)
            Control.Page.Session("UploadedFile") = Nothing
        End Sub

        Private Function GetStream() As Stream
            Dim buf() As Byte = TryCast(Control.Page.Session("UploadedFile"), Byte())
            If Not buf Is Nothing Then
                Dim stream As Stream = New MemoryStream(buf)
                Return stream
            End If
            Return Nothing
        End Function

    End Class

    Protected Sub MainScheduler_CustomErrorText(ByVal handler As Object, ByVal e As DevExpress.Web.ASPxScheduler.ASPxSchedulerCustomErrorTextEventArgs) Handles MainScheduler.CustomErrorText
        If Not e.Exception.InnerException Is Nothing Then
            e.ErrorText = PrepareMessage("Please contact Technical Support", e.Exception.InnerException.Message, True)
        End If
    End Sub

#Region "#errortexthelper"

    ' Public Class ErrorTextHelper
    Private Shared Function NewLinesToBr(ByVal text As String) As String
        text = text.Replace(Constants.vbCr, String.Empty)
        Return text.Replace(Constants.vbLf, "<br/>")
    End Function

    Public Shared Function PrepareMessage(ByVal subjectText As String, ByVal detailInfoText As String, ByVal showDetailedErrorInfo As Boolean) As String
        Dim subject As String = String.Format("{0}" & Constants.vbLf, subjectText)
        Dim detailInfo As String = String.Format("Detailed information is included below." & Constants.vbLf + Constants.vbLf & "- {0}", detailInfoText)
        If (Not showDetailedErrorInfo) Then
            detailInfo = String.Empty
        End If
        subject = NewLinesToBr(HttpUtility.HtmlEncode(subject))
        detailInfo = NewLinesToBr(HttpUtility.HtmlEncode(detailInfo))
        Return String.Format("{0},{1}|{2}{3}", subject.Length, detailInfo.Length, subject, detailInfo)
    End Function

    'End Class

#End Region ' #errortexthelper

    Private Sub SetupMappings()

        Dim mappings As ASPxAppointmentMappingInfo = MainScheduler.Storage.Appointments.Mappings
        MainScheduler.Storage.BeginUpdate()

        Try
            mappings.AppointmentId = "TaskId"
            mappings.Start = "StartingDateTime"
            mappings.End = "EndingDateTime"
            mappings.Subject = "TaskShortDescription"
            mappings.AllDay = "AllDayActivity"
            mappings.Description = "TaskLongDescription"
            mappings.Label = "Label"
            mappings.Location = "Location"
            mappings.RecurrenceInfo = "RecurrenceInfo"
            mappings.ReminderInfo = "ReminderInfo"
            mappings.ResourceId = "ResourceInfo"
            mappings.Status = "ShowTimeAs"
            'mappings.Type = "" ' "EventType"
        Finally
            MainScheduler.Storage.EndUpdate()
        End Try

        Dim custommappings As ASPxAppointmentCustomFieldMappingCollection = MainScheduler.Storage.Appointments.CustomFieldMappings
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("VisualTimeTransaction", "VisualTimeTransaction"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("IndividualTaskIndicator", "IndividualTaskIndicator"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("Priority", "Priority"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("Status", "Status"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("RecordType", "RecordType"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("WarningWhenCompleted", "WarningWhenCompleted"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("PercentageCompleted", "PercentageCompleted"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("RepeatActive", "RepeatActive"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("RepeatStartingDate", "RepeatStartingDate"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("RepeatEndingDate", "RepeatEndingDate"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("RepeatFrequency", "RepeatFrequency"))
        custommappings.Add(New ASPxAppointmentCustomFieldMapping("RepeatTimes", "RepeatTimes"))

    End Sub

    Private Sub InitialSetup()

        Dim vBeginWorkTime As String() = ConfigurationManager.AppSettings("BeginWorkingTime").ToString().Split(":")
        Dim vEndWorkTime As String() = ConfigurationManager.AppSettings("EndWorkingTime").ToString().Split(":")

        Dim startWorkingTime As New TimeSpan(Convert.ToInt32(vBeginWorkTime(0)), Convert.ToInt32(vBeginWorkTime(1)), Convert.ToInt32(vBeginWorkTime(2)))
        Dim endWorkingTime As New TimeSpan(Convert.ToInt32(vEndWorkTime(0)), Convert.ToInt32(vEndWorkTime(1)), Convert.ToInt32(vEndWorkTime(2)))

        MainScheduler.Views.DayView.WorkTime.Start = startWorkingTime
        MainScheduler.Views.DayView.WorkTime.End = endWorkingTime
        MainScheduler.Views.TimelineView.WorkTime.Start = startWorkingTime
        MainScheduler.Views.TimelineView.WorkTime.End = endWorkingTime
        MainScheduler.Views.WorkWeekView.WorkTime.Start = startWorkingTime
        MainScheduler.Views.WorkWeekView.WorkTime.End = endWorkingTime

        Dim vBeginVisibleTime As String() = ConfigurationManager.AppSettings("BeginVisibleTime").ToString().Split(":")
        Dim vEndVisibleTime As String() = ConfigurationManager.AppSettings("EndVisibleTime").ToString().Split(":")

        Dim startVisibleTime As New TimeSpan(Convert.ToInt32(vBeginVisibleTime(0)), Convert.ToInt32(vBeginVisibleTime(1)), Convert.ToInt32(vBeginVisibleTime(2)))
        Dim endVisibleTime As New TimeSpan(Convert.ToInt32(vEndVisibleTime(0)), Convert.ToInt32(vEndVisibleTime(1)), Convert.ToInt32(vEndVisibleTime(2)))

        MainScheduler.Views.DayView.VisibleTime.Start = startVisibleTime
        MainScheduler.Views.DayView.VisibleTime.End = endVisibleTime
        MainScheduler.Views.WorkWeekView.VisibleTime.Start = startVisibleTime
        MainScheduler.Views.WorkWeekView.VisibleTime.End = endVisibleTime

        If Not IsPostBack Then
            MainScheduler.Start = Date.Now
        End If

    End Sub

#Region "Relationship Filter Control Events"

    Protected Sub ShowPreview_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowPreview.CheckedChanged
        RelationshipTaskGridView.Settings.ShowPreview = ShowPreview.Checked
        RelationshipTaskGridView.DataBind()
    End Sub

    Protected Sub UserFilter_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles UserFilter.DataBound
        If IsNothing(UserFilter.Items.FindByValue(0)) Then
            UserFilter.Items.Insert(0, New DevExpress.Web.ASPxEditors.ListEditItem("", 0))
        End If
    End Sub

    Protected Sub StatusFilter_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusFilter.DataBound
        StatusFilter.Items.Insert(0, New DevExpress.Web.ASPxEditors.ListEditItem("", 0))
    End Sub

#End Region

#Region "Relationship  RelationshipTaskGridView Events"

    Protected Sub RelationshipTaskGridView_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles RelationshipTaskGridView.CustomCallback

        Select Case e.Parameters
            Case "Bind"
                RelationshipTaskGridView.DataBind()
        End Select
    End Sub

    Protected Sub RelationshipTaskGridView_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles RelationshipTaskGridView.DataBinding
        Dim UserfilterValue As Long = 0
        Dim statusFilterValue As Integer = 0

        If Not IsNothing(UserFilter.SelectedItem) AndAlso
           UserFilter.SelectedItem.Value > 0 Then
            UserfilterValue = UserFilter.SelectedItem.Value
        End If

        If Not IsNothing(StatusFilter.SelectedItem) AndAlso
           StatusFilter.SelectedItem.Value > 0 Then
            statusFilterValue = StatusFilter2.SelectedItem.Value
        End If

        RelationshipTaskGridView.DataSource = InMotionGIT.Agenda.Proxy.Manager.RetrieveRelationshipTask(UserInfo.User.ProviderUserKey.ToString, LanguageId, UserfilterValue, statusFilterValue)

    End Sub

#End Region

#Region "Ownwer Filter Control Events"

    Protected Sub ShowPreview2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowPreview2.CheckedChanged
        RelationshipTaskGridView2.Settings.ShowPreview = ShowPreview.Checked
        RelationshipTaskGridView2.DataBind()
    End Sub

    Protected Sub UserFilter2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles UserFilter2.DataBound
        If IsNothing(UserFilter2.Items.FindByValue(0)) Then
            UserFilter2.Items.Insert(0, New DevExpress.Web.ASPxEditors.ListEditItem("", 0))
        End If
    End Sub

    Protected Sub StatusFilter2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusFilter2.DataBound
        StatusFilter2.Items.Insert(0, New DevExpress.Web.ASPxEditors.ListEditItem("", 0))
    End Sub

#End Region

#Region "Owner RelationshipTaskGridView Events"

    Protected Sub RelationshipTaskGridView2_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles RelationshipTaskGridView2.CustomCallback

        Select Case e.Parameters
            Case "Bind"
                RelationshipTaskGridView2.DataBind()
        End Select
    End Sub

    Protected Sub RelationshipTaskGridView2_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles RelationshipTaskGridView2.DataBinding
        Dim UserfilterValue As Long = 0
        Dim statusFilterValue As Integer = 0

        If Not IsNothing(UserFilter2.SelectedItem) AndAlso
           UserFilter2.SelectedItem.Value > 0 Then
            UserfilterValue = UserFilter2.SelectedItem.Value
        End If

        If Not IsNothing(StatusFilter2.SelectedItem) AndAlso
           StatusFilter2.SelectedItem.Value > 0 Then
            statusFilterValue = StatusFilter2.SelectedItem.Value
        End If

        RelationshipTaskGridView2.DataSource = InMotionGIT.Agenda.Proxy.Manager.RetrieveOwnerTasks(UserInfo.User.ProviderUserKey.ToString, LanguageId, UserfilterValue, statusFilterValue)

    End Sub

#End Region

End Class