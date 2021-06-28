Imports Dropthings.Widget.Framework


Namespace Dropthings.Widgets

    Partial Class NotFound
        Inherits System.Web.UI.UserControl
        Implements IWidget

#Region "IWidget Members"


        Private _Host As IWidgetHost
        Private _State As XElement

        Public Sub Closed() Implements Widget.Framework.IWidget.Closed

        End Sub

        Public Sub HideSettings() Implements Widget.Framework.IWidget.HideSettings

        End Sub

        Public Sub Init1(host As Widget.Framework.IWidgetHost) Implements Widget.Framework.IWidget.Init
            _Host = host
        End Sub

        Public Sub Maximized() Implements Widget.Framework.IWidget.Maximized

        End Sub

        Public Sub Minimized() Implements Widget.Framework.IWidget.Minimized

        End Sub

        Public Sub ShowSettings() Implements Widget.Framework.IWidget.ShowSettings

        End Sub

#End Region

    End Class
End Namespace
