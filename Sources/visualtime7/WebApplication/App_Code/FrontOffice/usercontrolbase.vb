#Region "using"

Imports DevExpress.Web.ASPxGridView
Imports InMotionGIT.Common.Extensions
Imports Microsoft.VisualBasic

#End Region

Namespace GIT.Core

    Public Class UserControlBase
        Inherits System.Web.UI.UserControl

#Region "Events"

        Public Event WithErrors(sender As Object, e As InMotionGIT.FrontOffice.Support.Events.ErrorEventArgs)

        Public Sub RaiseErrors(ByRef errors As InMotionGIT.Common.Contracts.Errors.ErrorCollection)
            RaiseEvent WithErrors(Nothing, New InMotionGIT.FrontOffice.Support.Events.ErrorEventArgs(errors))
        End Sub

#End Region

#Region "Find Controls Methods"

        Public Sub BehaviorShowControls(behaviorcontrols As String)
            Dim controlname As String = String.Empty
            Dim userControlname As String = String.Empty
            Dim showproperty As String = String.Empty
            Dim controlFound As Object

            For Each control As String In behaviorcontrols.Split(";")
                controlname = control.Split(",")(0)
                showproperty = control.Split(",")(1)
                If controlname.Contains(".") Then
                    controlname = controlname.Split(".")(0)
                End If

                If Me.ID = controlname Then
                    controlFound = Me
                Else
                    controlFound = FindControlRecursive(Me, controlname)
                End If

                If Not IsNothing(controlFound) Then
                    FindValueProperty(controlFound, showproperty)
                    controlFound = FindControlRecursive(Me, controlname & "Label")

                    If Not IsNothing(controlFound) Then
                        FindValueProperty(controlFound, showproperty)
                    End If

                    controlFound = FindControlRecursive(Me, controlname & "MeasureLabel")

                    If Not IsNothing(controlFound) Then
                        FindValueProperty(controlFound, showproperty)
                    End If

                    controlFound = FindControlRecursive(Me, controlname & "LegendLabel")

                    If Not IsNothing(controlFound) Then
                        FindValueProperty(controlFound, showproperty)
                    End If
                End If
            Next
        End Sub

        Public Function FindControls(controlName As String) As Control
            Dim controlFound As Control
            Dim name As String = controlName
            Dim userControlname As String = AppRelativeVirtualPath.Split("/")(AppRelativeVirtualPath.Split("/").Length - 1)

            If name.Contains(".") Then
                name = name.Split(".")(0)
            End If

            userControlname = userControlname.Replace(".aspx", "")

            controlFound = FindControlRecursive(Me, String.Format("{0}{1}UC1", userControlname, name))

            If IsNothing(controlFound) Then
                controlFound = FindControlRecursive(Me, name)

                If IsNothing(controlFound) Then
                    controlFound = FindControlRecursive(Me, String.Format("{0}Label", controlName))
                End If
            End If

            Return controlFound
        End Function

        Private Shared Function FindControlRecursive(Root As Control, Id As String) As Object
            Dim FoundCtl As Object = Nothing

            For Each Ctl As Control In Root.Controls
                If Not IsNothing(Ctl.ID) AndAlso Ctl.ID.ToLower = Id.ToLower Then
                    FoundCtl = Ctl
                Else

                    Select Case Ctl.GetType.FullName
                        Case "DevExpress.Web.ASPxTabControl.ASPxPageControl"
                            FoundCtl = DirectCast(Ctl, DevExpress.Web.ASPxTabControl.ASPxPageControl).TabPages.FindByName(Id)

                        Case "DevExpress.Web.ASPxGridView.ASPxGridView"
                            FoundCtl = DirectCast(Ctl, DevExpress.Web.ASPxGridView.ASPxGridView).Columns(Id)
                    End Select

                    If IsNothing(FoundCtl) AndAlso Not IsNothing(Ctl.Controls) Then
                        FoundCtl = FindControlRecursive(Ctl, Id)
                    End If
                End If

                If Not IsNothing(FoundCtl) Then
                    Exit For
                End If
            Next

            Return FoundCtl
        End Function

        Private Shared Sub FindValueProperty(control As Object, showproperty As String)
            Select Case showproperty
                Case "Hidden"
                    InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("ClientVisible", control, False)

                Case "Enabled"
                    If String.Equals(control.GetType.FullName, "DevExpress.Web.ASPxRoundPanel.ASPxRoundPanel",
                                     StringComparison.CurrentCultureIgnoreCase) Then

                        InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("Enabled", control, True)
                    Else
                        InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("ClientEnabled", control, True)
                    End If

                Case "Disabled"
                    If String.Equals(control.GetType.FullName, "DevExpress.Web.ASPxRoundPanel.ASPxRoundPanel",
                                     StringComparison.CurrentCultureIgnoreCase) Then

                        InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("Enabled", control, False)
                    Else
                        InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("ClientEnabled", control, False)
                    End If

                Case Else 'Or "Visible"
                    InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("ClientVisible", control, True)

            End Select
        End Sub

        Private Shared Sub FindColumGrid(gridcontrol As ASPxGridView, columnname As String, showproperty As String)
            For Each Ctl As GridViewColumn In gridcontrol.Columns
                If Ctl.Name = columnname Then
                    Select Case showproperty
                        Case "Hidden"
                            Ctl.Visible = False

                        Case "Enabled"
                            Throw New Exception("Not implements")

                        Case "Disabled"
                            Throw New Exception("Not implements")

                        Case Else 'Or "Visible"
                            Ctl.Visible = True
                    End Select
                End If
            Next
        End Sub

#End Region

        Private _UserInfo As InMotionGIT.Membership.Providers.MemberContext

        Public Property UserInfo() As InMotionGIT.Membership.Providers.MemberContext
            Get
                If _UserInfo.IsEmpty Then
                    _UserInfo = New InMotionGIT.Membership.Providers.MemberContext
                End If
                Return _UserInfo
            End Get
            Set(ByVal value As InMotionGIT.Membership.Providers.MemberContext)
                _UserInfo = value
            End Set
        End Property

    End Class

End Namespace