Imports Dropthings.Widget.Framework
Imports System.Xml.Linq
Imports Artem.Web.UI.Controls
Imports Artem.Google.UI

Namespace Dropthings.Widgets

    Partial Class GoogleMapWidgetUserControl
        Inherits System.Web.UI.UserControl
        Implements IWidget


#Region "State Handler"

        Private _host As IWidgetHost
        Private _State As XElement

        Private ReadOnly Property State() As XElement
            Get
                If IsNothing(_State) Then
                    Dim stateSrc As String = Me._host.GetState()
                    If String.IsNullOrEmpty(stateSrc) Then
                        _State = New XElement("state", _
                                New XElement("latitude", "9.945666"), _
                                New XElement("longitude", "-84.117358"), _
                                New XElement("zoom", "16"))
                    Else
                        _State = XElement.Parse(stateSrc)
                    End If
                End If
                Return _State
            End Get
        End Property

        Private Sub SaveState()
            Me._host.SaveState(Me.State.Xml())
        End Sub

#End Region

#Region "IWidget Members"

        Public Sub Init1(ByVal host As Dropthings.Widget.Framework.IWidgetHost) Implements Dropthings.Widget.Framework.IWidget.Init
            Me._host = host
        End Sub

        Public Sub Closed() Implements Dropthings.Widget.Framework.IWidget.Closed

        End Sub


        Public Sub Maximized() Implements Dropthings.Widget.Framework.IWidget.Maximized

        End Sub

        Public Sub Minimized() Implements Dropthings.Widget.Framework.IWidget.Minimized

        End Sub

        Public Sub ShowSettings() Implements Dropthings.Widget.Framework.IWidget.ShowSettings
            txtLatitude.Text = Me.Latitude
            txtLongitude.Text = Me.Longitude
            txtZoom.Text = Me.Zoom
            SettingsPanel.Visible = True
        End Sub

        Public Sub HideSettings() Implements Dropthings.Widget.Framework.IWidget.HideSettings
            SettingsPanel.Visible = False
        End Sub

#End Region

        Public Property Latitude() As String
            Get
                Return State.Element("latitude").Value.ToString
            End Get
            Set(ByVal value As String)
                State.Element("latitude").Value = value.ToString()
                _State.Element("latitude").Value = value.ToString()
            End Set
        End Property

        Public Property Longitude() As String
            Get
                Return State.Element("longitude").Value.ToString
            End Get
            Set(ByVal value As String)
                State.Element("longitude").Value = value.ToString()
            End Set
        End Property

        Public Property Zoom() As Integer
            Get
                Return Convert.ToInt16(State.Element("zoom").Value)
            End Get
            Set(ByVal value As Integer)
                State.Element("zoom").Value = value.ToString
            End Set
        End Property


        Protected Sub SaveSettingsButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveSettingsButton.Click

            Me.Latitude = txtLatitude.Text
            Me.Longitude = txtLongitude.Text
            Me.Zoom = txtZoom.Text
            Me.SaveState()
            GoogleMap1.Focus()
            Me._host.HideSettings()
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'GoogleMap1.Latitude = 42.1229 ' Me.Latitude
            'GoogleMap1.Longitude = 24.7879 'Me.Longitude
            'GoogleMap1.Zoom = Me.Zoom
            'If IsPostBack Then
            '    Dim street As String = _txtAddress.Text.Trim

            '    If street.Length > 0 Then

            '        Dim maker As New GoogleMarkers(street)
            '        GoogleMap1.Address = street
            '        GoogleMap1.Markers.Clear()
            '        maker.Text = street
            '        GoogleMap1.Markers.Add(maker)
            '    Else
            '        Dim maker As New GoogleMarker(9.9445776460312381, -84.117880761623383)
            '        GoogleMap1.Markers.Clear()
            '        maker.Text = "<b>Global Insurance Technology</b><br/>Costa Rica"
            '        GoogleMap1.Markers.Add(maker)
            '    End If
            'Else
            '    Dim maker As New GoogleMarker(9.9445776460312381, -84.117880761623383)
            '    GoogleMap1.Markers.Clear()
            '    maker.Text = "<b>Global Insurance Technology</b><br/>Costa Rica"
            '    GoogleMap1.Markers.Add(maker)
            'End If
        End Sub
    End Class
End Namespace
