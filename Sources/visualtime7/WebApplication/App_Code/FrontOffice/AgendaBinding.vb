Imports System
Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Xml
Imports System.Xml.Linq
Imports System.Linq
Imports InMotionGIT.FrontOffice.Support
Imports InMotionGIT.FrontOffice.Proxy
Imports System.Web

'TODO: b) Toda esta logica debera pasar el proxy de agenda.
'TODO: a) Es encesario eliminar de la lista de recursos el usuario actual, ya que no tiene sentido y de esta forma asignar automaticamente la tarea a el por medio de un seundo registroen taskonwer.
'TODO: c) Falta el manejo de asiganación multiple es decir la tarea se da por completa en base que todas los usuario la den por terminada, lo que quiere decir que cuando un usuario actualiza la tarea inicialmente debe actualizar los datos de taskowner y en funciona a eso la tarea como tal, no como lo hace ahora directamente sobre al tarea.

<DataObjectAttribute()>
Public Class AgendaBinding

    <DataObjectMethodAttribute(DataObjectMethodType.Select, True)>
    Public Function SelectEventMethodHandler() As List(Of InMotionGIT.Agenda.Contracts.TaskByOwnerView)
        Return InMotionGIT.Agenda.Proxy.Manager.RetrieveTaskByOwnerUserId(InMotionGIT.Agenda.Contracts.Enumerations.EnumRecordType.None)
    End Function

    <DataObjectMethodAttribute(DataObjectMethodType.Select, False)>
    Public Function SelectTaskMethodHandler() As List(Of InMotionGIT.Agenda.Contracts.TaskByOwnerView)
        Return InMotionGIT.Agenda.Proxy.Manager.RetrieveTaskByOwnerUserId(InMotionGIT.Agenda.Contracts.Enumerations.EnumRecordType.Task)
    End Function

    <DataObjectMethodAttribute(DataObjectMethodType.Insert, True)>
    Public Sub InsertEventMethodHandler(ByVal customEvent As InMotionGIT.Agenda.Contracts.TaskByOwnerView)
        Dim taskInstance As New InMotionGIT.Agenda.Contracts.Task
        If IsNothing(customEvent.ResourceInfo) Then
            customEvent.ResourceInfo = "<ResourceIds><ResourceId Type=""System.String"" Value=""" & (New InMotionGIT.Membership.Providers.MemberContext).User.ProviderUserKey.ToString & """ /></ResourceIds>"
        End If

        MappingParametersToClass(taskInstance, customEvent)

        With taskInstance

            .TaskId = Guid.NewGuid.ToString

            ' Fixed Values
            .OriginTypeEnum = InMotionGIT.Agenda.Contracts.Enumerations.EnumOriginType.Manual
            .RecordStatusEnum = InMotionGIT.Agenda.Contracts.Enumerations.EnumRecordStatus.Active

            ' Control Fields
            .Status = InMotionGIT.Agenda.Contracts.Enumerations.EnumStatus.NotInitiated
            .StatusEnum = InMotionGIT.Agenda.Contracts.Enumerations.EnumStatus.NotInitiated

            If .Priority = 0 Then
                .Priority = 2
            End If

            'En caso de estar definido algún recordatorio, se procesa el xml para copiar los valores en los campos del registros
            .AlarmActive = False
            .AlarmDateTime = DateTime.MinValue
            If .ReminderInfo.IsNotEmpty Then
                Dim xmlReminderInfo As XDocument = XDocument.Parse(customEvent.ReminderInfo)
                Dim TimeBeforeStart As XAttribute = (From c In xmlReminderInfo.Descendants("Reminder") Select c.Attribute("TimeBeforeStart")).FirstOrDefault

                If TimeBeforeStart.IsNotEmpty Then
                    Dim value As String = TimeBeforeStart.Value
                    .AlarmActive = True
                    .AlarmDateTime = .StartingDateTime.Subtract(New TimeSpan(value.Substring(0, 2), value.Substring(3, 2), 0))
                End If
            End If

            ' Usuario que crea la tarea
            .TaskOwners = New List(Of InMotionGIT.Agenda.Contracts.TaskOwner)
            Dim taskOwner As New InMotionGIT.Agenda.Contracts.TaskOwner
            .TaskOwners.Add(taskOwner)

            With taskOwner
                .UserId = (New InMotionGIT.Membership.Providers.MemberContext).User.ProviderUserKey.ToString
                .OwnerIndicator = True
                .UserIndicator = True
                .RecordStatusEnum = InMotionGIT.Agenda.Contracts.Enumerations.EnumRecordStatus.Active
            End With

            If Not IsNothing(customEvent.ResourceInfo) Then
                Dim xmlData As XDocument = XDocument.Parse(customEvent.ResourceInfo)
                Dim ResourceIds = From c In xmlData.Descendants("ResourceId") Select c.Attribute("Value")

                For Each item As XAttribute In ResourceIds.ToList

                    Dim taskOwnerDestiny As New InMotionGIT.Agenda.Contracts.TaskOwner
                    .TaskOwners.Add(taskOwnerDestiny)

                    With taskOwnerDestiny
                        .UserId = item.Value
                        .OwnerIndicator = False
                        ' UserIndicator: Pendiente por el cambio en la coleccion para verificar el "+" o "-"
                        ' que representa el grupo o usuario
                        If .UserId > 0 Then
                            .UserIndicator = True
                        Else
                            .UserIndicator = False
                            .UserId = .UserId * -1
                        End If

                        .RecordStatus = InMotionGIT.Agenda.Contracts.Enumerations.EnumRecordStatus.Active
                    End With
                Next
            End If
            'If .TaskOwners.Count = 1 Then
            '    taskOwner = New TaskManagerService.TaskOwner
            '    .TaskOwners.Add(taskOwner)

            '    With taskOwner
            '        .UserId = HttpContext.Current.Session("nUserCode")
            '        .OwnerIndicator = False
            '        .UserIndicator = True
            '        .RecordStatusEnum = TaskManagerService.EnumRecordStatus.Active
            '    End With
            '    .ResourceInfo =
            'End If


            InMotionGIT.Agenda.Proxy.SchedulerManarge.CreateTask(taskInstance, (New InMotionGIT.Membership.Providers.MemberContext).User.ProviderUserKey.ToString)

        End With

    End Sub

    <DataObjectMethodAttribute(DataObjectMethodType.Update, True)>
    Public Sub UpdateEventMethodHandler(ByVal customEvent As InMotionGIT.Agenda.Contracts.TaskByOwnerView)

        Dim taskInstance As InMotionGIT.Agenda.Contracts.Task = InMotionGIT.Agenda.Proxy.Manager.RetrieveTaskByTaskId(customEvent.TaskId)
        Dim isChangeStatusOrPercentageCompleted As Boolean = False

        If IsNothing(customEvent.ResourceInfo) Then
            customEvent.ResourceInfo = "<ResourceIds><ResourceId Type=""System.String"" Value=""" & (New InMotionGIT.Membership.Providers.MemberContext).User.ProviderUserKey.ToString & """ /></ResourceIds>"
        End If

        If taskInstance.Status <> customEvent.Status Or taskInstance.PercentageCompleted <> customEvent.PercentageCompleted Then
            isChangeStatusOrPercentageCompleted = True
        End If

        MappingParametersToClass(taskInstance, customEvent)

        With taskInstance

            .TaskId = customEvent.TaskId

            ' Fixed Values
            .OriginTypeEnum = customEvent.OriginType
            .RecordStatusEnum = customEvent.RecordStatus


            ' Control Fields
            .Status = customEvent.Status
            .StatusEnum = customEvent.Status



            'En caso de estar definido algún recordatorio, se procesa el xml para copiar los valores en los campos del registros
            .AlarmActive = False
            .AlarmDateTime = DateTime.MinValue
            If .ReminderInfo.IsNotEmpty Then
                Dim xmlReminderInfo As XDocument = XDocument.Parse(customEvent.ReminderInfo)
                Dim TimeBeforeStart As XAttribute = (From c In xmlReminderInfo.Descendants("Reminder") Select c.Attribute("TimeBeforeStart")).FirstOrDefault

                If TimeBeforeStart.IsNotEmpty Then
                    Dim value As String = TimeBeforeStart.Value
                    .AlarmActive = True
                    .AlarmDateTime = .StartingDateTime.Subtract(New TimeSpan(value.Substring(0, 2), value.Substring(3, 2), 0))
                End If
            End If

            If Not IsNothing(customEvent.ResourceInfo) Then

                Dim xmlData As XDocument = XDocument.Parse(customEvent.ResourceInfo)

                Dim ResourceIds = From c In xmlData.Descendants("ResourceId") Select c.Attribute("Value")

                For Each item As XAttribute In ResourceIds.ToList
                    Dim value As String = item.Value

                    Dim exists = (From p In taskInstance.TaskOwners
                                  Where p.UserId = IIf(value < 0, value * -1, value).ToString
                                  Select p).ToList

                    If exists.Count = 0 Then
                        Dim taskOwnerDestiny As New InMotionGIT.Agenda.Contracts.TaskOwner
                        .TaskOwners.Add(taskOwnerDestiny)

                        With taskOwnerDestiny
                            .UserId = item.Value
                            If .UserId > 0 Then
                                .UserIndicator = True
                            Else
                                .UserIndicator = False
                                .UserId = .UserId * -1
                            End If
                            ' UserIndicator: Pendiente por el cambio en la coleccion para verificar el "+" o "-"
                            ' que representa el grupo o usuario
                            .RecordStatusEnum = InMotionGIT.Agenda.Contracts.Enumerations.EnumRecordStatus.Active
                            .IsNew = True
                        End With
                    End If

                Next

                ' Los que no esten seleccionados se marcan para borrar
                For Each taskOwner As InMotionGIT.Agenda.Contracts.TaskOwner In taskInstance.TaskOwners

                    Dim userId As String = taskOwner.UserId.ToString

                    Dim existsXml = From c In xmlData.Descendants("ResourceId")
                                    Where IsGroup(c.Attribute("Value")).Equals(IIf(userId < 0, userId * -1, userId).ToString)
                                    Select c.Attribute("Value")

                    If Not taskOwner.OwnerIndicator AndAlso existsXml.ToList.Count = 0 Then
                        taskOwner.IsDeletedMark = True
                    End If

                Next

            Else
                For Each taskOwner As InMotionGIT.Agenda.Contracts.TaskOwner In taskInstance.TaskOwners
                    If Not taskOwner.OwnerIndicator Then
                        taskOwner.IsDeletedMark = True
                    End If
                Next
            End If

        End With

        If taskInstance.TaskOwners.IsNotEmpty AndAlso taskInstance.TaskOwners.Count <> 0 Then
            For Each ItemTaskOwner In taskInstance.TaskOwners
                If ItemTaskOwner.UserId < 0 Then
                    ItemTaskOwner.UserId = ItemTaskOwner.UserId * -1
                End If
            Next
        End If

        InMotionGIT.Agenda.Proxy.SchedulerManarge.UpdateTaskInformation(taskInstance, (New InMotionGIT.Membership.Providers.MemberContext).User.ProviderUserKey.ToString, LanguageHelper.CurrentCultureToLanguage, isChangeStatusOrPercentageCompleted)

    End Sub

    Function IsGroup(id As String) As String
        Dim result As String = String.Empty
        If id < 0 Then
            result = id * -1
        Else
            result = id
        End If
        Return result
    End Function

    <DataObjectMethodAttribute(DataObjectMethodType.Delete, True)>
    Public Sub DeleteEventMethodHandler(ByVal customEvent As InMotionGIT.Agenda.Contracts.TaskByOwnerView)
        InMotionGIT.Agenda.Proxy.Manager.DeleteTask(customEvent.TaskId, (New InMotionGIT.Membership.Providers.MemberContext).User.ProviderUserKey.ToString)
    End Sub

    Private Sub MappingParametersToClass(ByVal taskInstance As InMotionGIT.Agenda.Contracts.Task,
                                         ByVal customEvent As InMotionGIT.Agenda.Contracts.TaskByOwnerView)

        Try


            With taskInstance

                ' Control Fields
                .TaskShortDescription = customEvent.TaskShortDescription
                .StartingDateTime = customEvent.StartingDateTime
                .EndingDateTime = customEvent.EndingDateTime
                .ShowTimeAs = customEvent.ShowTimeAs
                .Location = customEvent.Location
                .Label = customEvent.Label
                .ReminderInfo = customEvent.ReminderInfo
                .AllDayActivity = customEvent.AllDayActivity
                .TaskLongDescription = customEvent.TaskLongDescription

                ' Custom Fields
                .VisualTIMETransaction = customEvent.VisualTimeTransaction
                .IndividualTaskIndicator = customEvent.IndividualTaskIndicator
                .RecordType = customEvent.RecordType
                .Priority = customEvent.Priority
                .WarningWhenCompleted = customEvent.WarningWhenCompleted
                .PercentageCompleted = customEvent.PercentageCompleted
                .RepeatActive = customEvent.RepeatActive
                .RepeatStartingDate = customEvent.RepeatStartingDate
                .RepeatEndingDate = customEvent.RepeatEndingDate
                .RepeatFrequency = customEvent.RepeatFrequency
                .RepeatTimes = customEvent.RepeatTimes
                .ResourceInfo = customEvent.ResourceInfo
                .RecurrenceInfo = customEvent.RecurrenceInfo

                If .RepeatTimes < 0 Then
                    .RepeatTimes = 0
                End If

            End With
        Catch ex As Exception
            Dim ddd = ex
        End Try
    End Sub

End Class