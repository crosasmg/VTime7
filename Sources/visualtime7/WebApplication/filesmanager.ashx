<%@ WebHandler Language="VB" Class="filesmanager" %>

Imports System
Imports System.Web

Public Class filesmanager : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim url As String = ConfigurationManager.AppSettings("Url.WebApplication")
        Dim filename As String = context.Request.QueryString("filename").ToLower()
        Dim path As String = context.Request.QueryString("path").ToLower()
        Dim publishPath As String = ConfigurationManager.AppSettings(String.Format("PhysicalPath.{0}", path))
        Dim mode As String = context.Request.QueryString("mode")

        If filename.Contains(".pdf") Then
            publishPath = publishPath + "pdf\"

        ElseIf filename.Contains(".xml") Then
            publishPath = publishPath + "xml\"
        End If

        If mode.IsEmpty() Then
            mode = "download"
        End If

        filename = publishPath + filename

        Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(filename)

        context.Response.Clear()
        context.Response.AddHeader("Pragma", "no-cache")
        context.Response.AddHeader("Expires", "Mon, 1 Jan 2000 05:00:00 GMT")
        context.Response.AddHeader("Last-Modified", DateTime.Now.ToString("ddd, dd MMM yyyy hh:mm:ss") + " GMT")

        If (mode = "view") Then
            context.Response.ContentType = "application/" + System.IO.Path.GetExtension(filename).Replace(".", "")
            context.Response.AddHeader("Content-Disposition", "filename=" + filename)
        Else
            context.Response.ContentType = "octet/stream"
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.IO.Path.GetFileName(filename))
        End If

        context.Response.AddHeader("Content-Length", fileInfo.Length.ToString())
        context.Response.BufferOutput = True
        context.Response.TransmitFile(filename)
        context.Response.Flush()
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class