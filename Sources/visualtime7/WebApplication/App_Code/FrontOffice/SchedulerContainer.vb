Imports Microsoft.VisualBasic
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxEditors
Imports System.Data
Imports InMotionGIT.FrontOffice.Proxy

Public Class SchedulerContainer
    Inherits AppointmentFormTemplateContainer

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.new(control)
    End Sub

    Private _VisualTimeTransaction As String
    Private _IndividualTaskIndicator As Boolean
    Private _Priority As Integer
    Private _Status As Integer
    Private _RecordType As Integer

    Private _WarningWhenCompleted As Boolean
    Private _PercentageCompleted As Integer
    Private _RepeatActive As Boolean
    Private _RepeatStartingDate As Date
    Private _RepeatEndingDate As Date
    Private _RepeatFrequency As Integer
    Private _RepeatTimes As Integer

    Public ReadOnly Property VisualTimeTransaction() As String
        Get
            Dim val As Object = Appointment.CustomFields("VisualTimeTransaction")
            If IsNothing(val) Then
                Return ""
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property IndividualTaskIndicator() As Boolean
        Get
            Dim val As Object = Appointment.CustomFields("IndividualTaskIndicator")
            If IsNothing(val) Then
                Return False
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property Priority() As Integer
        Get
            Dim val As Object = Appointment.CustomFields("Priority")
            If IsNothing(val) Then
                Return ""
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property RecordType() As Integer
        Get
            Dim val As Object = Appointment.CustomFields("RecordType")
            If IsNothing(val) Then
                Return 2
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property Status() As Integer
        Get
            Dim val As Object = Appointment.CustomFields("Status")
            If IsNothing(val) Then
                Return Integer.MinValue
            Else
                Return val
            End If
        End Get
    End Property


    Public ReadOnly Property WarningWhenCompleted() As Boolean
        Get
            Dim val As Object = Appointment.CustomFields("WarningWhenCompleted")
            If IsNothing(val) Then
                Return False
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property PercentageCompleted() As Integer
        Get
            Dim val As Object = Appointment.CustomFields("PercentageCompleted")
            If IsNothing(val) Then
                Return Integer.MinValue
            Else
                Return val
            End If
        End Get
    End Property


    Public ReadOnly Property RepeatActive() As Boolean
        Get
            Dim val As Object = Appointment.CustomFields("RepeatActive")
            If IsNothing(val) Then
                Return False
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property RepeatStartingDate() As Date
        Get
            Dim val As Object = Appointment.CustomFields("RepeatStartingDate")
            If IsNothing(val) Then
                Return Date.MinValue
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property RepeatEndingDate() As Date
        Get
            Dim val As Object = Appointment.CustomFields("RepeatEndingDate")
            If IsNothing(val) Then
                Return Date.MinValue
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property RepeatFrequency() As Integer
        Get
            Dim val As Object = Appointment.CustomFields("RepeatFrequency")
            If IsNothing(val) Then
                Return Integer.MinValue
            Else
                Return val
            End If
        End Get
    End Property

    Public ReadOnly Property RepeatTimes() As Integer
        Get
            Dim val As Object = Appointment.CustomFields("RepeatTimes")
            If IsNothing(val) Then
                Return Integer.MinValue
            Else
                Return val
            End If
        End Get
    End Property

End Class

Public Class SchedulerSaveCallbackCommand
    Inherits AppointmentFormSaveCallbackCommand

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub

    Protected Friend Shadows ReadOnly Property Controller() As SchedulerFormController
        Get
            Return CType(MyBase.Controller, SchedulerFormController)
        End Get
    End Property
    Protected Overrides Sub AssignControllerValues()

        Dim tbField1 As ASPxComboBox = CType(FindControlByID("cbTransaction"), ASPxComboBox)
        Dim IndividualTaskIndicator As ASPxCheckBox = CType(FindControlByID("IndividualTaskIndicator"), ASPxCheckBox)
        Dim Priority As ASPxComboBox = CType(FindControlByID("cbPriority"), ASPxComboBox)
        Dim Status As ASPxComboBox = CType(FindControlByID("cbTaskStatus"), ASPxComboBox)
        Dim RecordType As ASPxComboBox = CType(FindControlByID("cbRecordType"), ASPxComboBox)

        Dim WarningWhenCompleted As ASPxCheckBox = CType(FindControlByID("WarningWhenCompleted"), ASPxCheckBox)
        Dim PercentageCompleted As ASPxTextBox = CType(FindControlByID("tbPerCompleted"), ASPxTextBox)
        Dim RepeatActive As ASPxCheckBox = CType(FindControlByID("RepeatActive"), ASPxCheckBox)
        Dim RepeatStartingDate As ASPxDateEdit = CType(FindControlByID("edtRepeatStatingDate"), ASPxDateEdit)
        Dim RepeatEndingDate As ASPxDateEdit = CType(FindControlByID("edtRepeatEndingDate"), ASPxDateEdit)
        Dim RepeatFrequency As ASPxComboBox = CType(FindControlByID("tbRepeatFrecuency"), ASPxComboBox)
        Dim RepeatTimes As ASPxTextBox = CType(FindControlByID("tbRepeatTimes"), ASPxTextBox)

        Try
            Controller.VisualTimeTransaction = tbField1.SelectedItem.Value
        Catch ex As Exception
            Controller.VisualTimeTransaction = String.Empty
        End Try

        Try
            Controller.IndividualTaskIndicator = IndividualTaskIndicator.Checked
        Catch ex As Exception
            Controller.IndividualTaskIndicator = False
        End Try

        Try
            Controller.Priority = Priority.SelectedItem.Value
        Catch ex As Exception
            Controller.Priority = 0
        End Try

        Try
            Controller.Status = Status.SelectedItem.Value
        Catch ex As Exception
            Controller.Status = 0
        End Try

        Try
            Controller.RecordType = RecordType.SelectedItem.Value
        Catch ex As Exception
            Controller.RecordType = 2
        End Try

        Try
            Controller.WarningWhenCompleted = WarningWhenCompleted.Checked
        Catch ex As Exception
            Controller.WarningWhenCompleted = False
        End Try

        Try
            Controller.PercentageCompleted = PercentageCompleted.Value
        Catch ex As Exception
            Controller.PercentageCompleted = 0
        End Try

        Try
            Controller.RepeatActive = RepeatActive.Checked
        Catch ex As Exception
            Controller.RepeatActive = False
        End Try

        Try
            Controller.RepeatStartingDate = RepeatStartingDate.Value
        Catch ex As Exception
            Controller.RepeatStartingDate = Date.MinValue
        End Try

        Try
            Controller.RepeatEndingDate = RepeatEndingDate.Value
        Catch ex As Exception
            Controller.RepeatEndingDate = Date.MinValue
        End Try

        Try
            Controller.RepeatFrequency = RepeatFrequency.SelectedItem.Value
        Catch ex As Exception
            Controller.RepeatFrequency = 0
        End Try

        Try
            Controller.RepeatTimes = RepeatTimes.Value
        Catch ex As Exception
            Controller.RepeatTimes = 0
        End Try

        MyBase.AssignControllerValues()

    End Sub

    Protected Overrides Function CreateAppointmentFormController(ByVal apt As Appointment) As AppointmentFormController
        Return New SchedulerFormController(Control, apt)
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Public Class SchedulerFormController
    Inherits AppointmentFormController

    Public Sub New(ByVal control As ASPxScheduler, ByVal apt As Appointment)
        MyBase.New(control, apt)
    End Sub

    Public Property VisualTimeTransaction() As String
        Get
            Return EditedAppointmentCopy.CustomFields("VisualTimeTransaction")
        End Get
        Set(ByVal value As String)
            EditedAppointmentCopy.CustomFields("VisualTimeTransaction") = value
        End Set
    End Property

    Public Property IndividualTaskIndicator() As Boolean
        Get
            Return EditedAppointmentCopy.CustomFields("IndividualTaskIndicator")
        End Get
        Set(ByVal value As Boolean)
            EditedAppointmentCopy.CustomFields("IndividualTaskIndicator") = value
        End Set
    End Property

    Public Property Priority() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("Priority")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("Priority") = value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("Status")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("Status") = value
        End Set
    End Property

    Public Property RecordType() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("RecordType")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("RecordType") = value
        End Set
    End Property

    Public Property WarningWhenCompleted() As Boolean
        Get
            Return EditedAppointmentCopy.CustomFields("WarningWhenCompleted")
        End Get
        Set(ByVal value As Boolean)
            EditedAppointmentCopy.CustomFields("WarningWhenCompleted") = value
        End Set
    End Property

    Public Property PercentageCompleted() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("PercentageCompleted")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("PercentageCompleted") = value
        End Set
    End Property

    Public Property RepeatActive() As Boolean
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatActive")
        End Get
        Set(ByVal value As Boolean)
            EditedAppointmentCopy.CustomFields("RepeatActive") = value
        End Set
    End Property

    Public Property RepeatStartingDate() As Date
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatStartingDate")
        End Get
        Set(ByVal value As Date)
            EditedAppointmentCopy.CustomFields("RepeatStartingDate") = value
        End Set
    End Property

    Public Property RepeatEndingDate() As Date
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatEndingDate")
        End Get
        Set(ByVal value As Date)
            EditedAppointmentCopy.CustomFields("RepeatEndingDate") = value
        End Set
    End Property

    Public Property RepeatFrequency() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatFrequency")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("RepeatFrequency") = value
        End Set
    End Property

    Public Property RepeatTimes() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatTimes")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("RepeatTimes") = value
        End Set
    End Property

    Private Property SourceVisualTimeTransaction() As String
        Get
            Return SourceAppointment.CustomFields("VisualTimeTransaction")
        End Get
        Set(ByVal value As String)
            SourceAppointment.CustomFields("VisualTimeTransaction") = value
        End Set
    End Property

    Private Property SourceIndividualTaskIndicator() As Boolean
        Get
            Return SourceAppointment.CustomFields("IndividualTaskIndicator")
        End Get
        Set(ByVal value As Boolean)
            SourceAppointment.CustomFields("IndividualTaskIndicator") = value
        End Set
    End Property

    Private Property SourcePriority() As Integer
        Get
            Return SourceAppointment.CustomFields("Priority")
        End Get
        Set(ByVal value As Integer)
            SourceAppointment.CustomFields("Priority") = value
        End Set
    End Property

    Private Property SourceStatus() As Integer
        Get
            Return SourceAppointment.CustomFields("Status")
        End Get
        Set(ByVal value As Integer)
            SourceAppointment.CustomFields("Status") = value
        End Set
    End Property

    Private Property SourceRecordType() As Integer
        Get
            Return SourceAppointment.CustomFields("RecordType")
        End Get
        Set(ByVal value As Integer)
            SourceAppointment.CustomFields("RecordType") = value
        End Set
    End Property

    Public Property SourceWarningWhenCompleted() As Boolean
        Get
            Return EditedAppointmentCopy.CustomFields("WarningWhenCompleted")
        End Get
        Set(ByVal value As Boolean)
            EditedAppointmentCopy.CustomFields("WarningWhenCompleted") = value
        End Set
    End Property

    Public Property SourcePercentageCompleted() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("PercentageCompleted")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("PercentageCompleted") = value
        End Set
    End Property

    Public Property SourceRepeatActive() As Boolean
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatActive")
        End Get
        Set(ByVal value As Boolean)
            EditedAppointmentCopy.CustomFields("RepeatActive") = value
        End Set
    End Property

    Public Property SourceRepeatStartingDate() As Date
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatStartingDate")
        End Get
        Set(ByVal value As Date)
            EditedAppointmentCopy.CustomFields("RepeatStartingDate") = value
        End Set
    End Property

    Public Property SourceRepeatEndingDate() As Date
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatEndingDate")
        End Get
        Set(ByVal value As Date)
            EditedAppointmentCopy.CustomFields("RepeatEndingDate") = value
        End Set
    End Property

    Public Property SourceRepeatFrequency() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatFrequency")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("RepeatFrequency") = value
        End Set
    End Property

    Public Property SourceRepeatTimes() As Integer
        Get
            Return EditedAppointmentCopy.CustomFields("RepeatTimes")
        End Get
        Set(ByVal value As Integer)
            EditedAppointmentCopy.CustomFields("RepeatTimes") = value
        End Set
    End Property

    Public Overrides Function IsAppointmentChanged() As Boolean
        If MyBase.IsAppointmentChanged() Then
            Return True
        End If
        Return SourceVisualTimeTransaction <> VisualTimeTransaction OrElse
               SourceIndividualTaskIndicator <> IndividualTaskIndicator OrElse
               SourcePriority <> Priority OrElse
               SourceRecordType <> RecordType OrElse
               WarningWhenCompleted <> WarningWhenCompleted OrElse
               PercentageCompleted <> PercentageCompleted OrElse
               RepeatActive <> RepeatActive OrElse
               RepeatStartingDate <> RepeatStartingDate OrElse
               RepeatEndingDate <> RepeatEndingDate OrElse
               RepeatFrequency <> RepeatFrequency OrElse
               RepeatTimes <> RepeatTimes OrElse
               SourceStatus <> Status

    End Function

    Protected Overrides Sub ApplyCustomFieldsValues()
        SourceVisualTimeTransaction = VisualTimeTransaction
        SourceIndividualTaskIndicator = IndividualTaskIndicator
        SourcePriority = Priority
        SourceStatus = Status
        SourceRecordType = RecordType
        WarningWhenCompleted = WarningWhenCompleted
        PercentageCompleted = PercentageCompleted
        RepeatActive = RepeatActive
        RepeatStartingDate = RepeatStartingDate
        RepeatEndingDate = RepeatEndingDate
        RepeatFrequency = RepeatFrequency
        RepeatTimes = RepeatTimes
    End Sub

End Class

Public Class ResourceFiller

    Public Shared Sub FillResources(ByVal storage As ASPxSchedulerStorage, ByVal count As Integer)
        Dim resources As ResourceCollection = storage.Resources.Items
        storage.BeginUpdate()
        Try


            Dim instance As New InMotionGIT.FrontOffice.Support.Services.FrontOffice

            Dim resourceList As List(Of InMotionGIT.FrontOffice.Support.Services.UsersAndGroups) = instance.UsersAndGroupsList


            'Se agregan los usuarios
            For Each item As InMotionGIT.FrontOffice.Support.Services.UsersAndGroups In From x In resourceList
                                                                                        Where x.IsUser = True
                                                                                        Order By x.Name
                If Not String.IsNullOrEmpty(item.Name) AndAlso item.Name.Trim.Length > 0 AndAlso item.Code > 0 Then
                    resources.Add(New Resource(item.Code, item.Name))
                End If
            Next

            'Se agregan los grupos
            For Each item As InMotionGIT.FrontOffice.Support.Services.UsersAndGroups In From x In resourceList
                                                                                        Where x.IsUser = False
                                                                                        Order By x.Name
                If Not String.IsNullOrEmpty(item.Name) AndAlso item.Name.Trim.Length > 0 AndAlso item.Code > 0 Then
                    resources.Add(New Resource(item.Code * -1, item.Name))
                End If
            Next

        Finally
            storage.EndUpdate()
        End Try
    End Sub

End Class