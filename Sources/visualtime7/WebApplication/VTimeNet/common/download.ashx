<%@ WebHandler Language="VB" Class="download" %>

Imports System
Imports System.Web

Public Class download : Implements IHttpHandler, IRequiresSessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim filename As String
        If Not String.IsNullOrEmpty(context.Session("sFile")) Then
            With New eFunctions.Values
                filename = .insGetSetting("MASSIVELOAD", String.Empty, "PATHS").Trim & String.Format("\{0}.XLS", context.Session("sFile"))
            End With

            With context.Response
                .ContentType = "application/ms-excel"
                .AddHeader("content-disposition", String.Format("inline; filename={0}.XLS", context.Session("sFile")))
                .TransmitFile(filename)
                .Flush()
            End With
        End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class