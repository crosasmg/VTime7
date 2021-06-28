Option Strict Off
Option Explicit On

Imports System.Web
Imports System.Web.SessionState
Imports System.Configuration

Public Class ASPSupport

    Private mobjRequest As HttpRequest

    Private mobjSession As HttpSessionState

    Private mobjContext As HttpContext

    Private mobjQueryString As HttpRequest

    Public Function GetASPQueryStringValue(ByVal sVariableName As String) As Object
        Call GetQueryString()
        If Not mobjQueryString Is Nothing Then
            GetASPQueryStringValue = mobjQueryString.QueryString.Item(sVariableName)
        Else
            GetASPQueryStringValue = String.Empty
        End If
    End Function

    Private Sub GetQueryString()
        If mobjContext Is Nothing Then
            mobjContext = System.Web.HttpContext.Current
        End If

        If Not mobjContext Is Nothing Then
            If mobjQueryString Is Nothing Then
                mobjQueryString = mobjContext.Request
            End If
        End If
    End Sub


    Public Function GetASPSessionValue(ByVal sVariableName As String) As Object
        Dim result As Object = Nothing
        If ServiceEnviroment.isServiceConsumer Then
            result = ServiceEnviroment.GetValue(sVariableName)
        Else
            Call GetSession()
            If Not mobjSession Is Nothing Then
                result = mobjSession(sVariableName)
            Else
                result = ConfigurationManager.AppSettings(String.Format("Session.{0}", sVariableName))
                If IsNothing(result) Then
                    result = GetSetting("Visual TIME", "Session", sVariableName, String.Empty)
                End If
            End If
        End If
        Return result
    End Function

    Public Sub SetASPSessionValue(ByVal sVariableName As String, ByVal vValue As Object)
        If Not ServiceEnviroment.isServiceConsumer Then
            Call GetSession()
            If Not mobjSession Is Nothing Then
                mobjSession(sVariableName) = vValue
            Else
                If vValue Is Nothing Then
                    Call SaveSetting("Visual TIME", "Session", sVariableName, "")
                Else
                    Call SaveSetting("Visual TIME", "Session", sVariableName, vValue)
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        mobjSession = Nothing
        mobjContext = Nothing

        MyBase.Finalize()
    End Sub

    Public Property Session() As Object
        Get
            Call GetSession()
            Session = mobjSession
        End Get
        Set(ByVal Value As Object)
            mobjSession = Value
        End Set
    End Property

    Public ReadOnly Property SessionID() As String
        Get
            If mobjSession Is Nothing Then
                Call GetSession()
            End If

            If mobjSession Is Nothing Then
                SessionID = "000000000"
            Else
                SessionID = mobjSession.SessionID
            End If
        End Get
    End Property

    Private Sub GetSession()
        If mobjContext Is Nothing Then
            mobjContext = System.Web.HttpContext.Current
        End If
        If Not mobjContext Is Nothing Then
            If mobjSession Is Nothing Then
                mobjSession = mobjContext.Session
            End If
        End If
    End Sub


    Private Sub GetRequest()
        If mobjContext Is Nothing Then
            mobjContext = System.Web.HttpContext.Current
        End If
        If Not mobjContext Is Nothing Then
            If mobjRequest Is Nothing Then
                mobjRequest = mobjContext.Request
            End If
        End If
    End Sub

    '**%Objective:
    '**%Parameters:
    '**%    sVariableName -
    '%Objetivo:
    '%Parámetros:
    '%      sVariableName -
    Public Function GetASPRequestValue(ByVal sVariableName As String) As Object

        Call GetRequest()
        If Not mobjRequest Is Nothing Then
            GetASPRequestValue = mobjRequest(sVariableName)
        Else
            GetASPRequestValue = GetSetting("Visual TIME", "Session", sVariableName, String.Empty)
        End If
    End Function
End Class






