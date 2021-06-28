<%@ WebHandler Language="VB" Class="reportdownload" %>

Imports System
Imports System.Web

Public Class reportdownload : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim filename As String = context.Request.QueryString(0)
        Dim mode As String = String.Empty

        If filename.Contains(",") Then
            mode = filename.Split(",")(1)
            filename = filename.Split(",")(0)
        End If
        
        Dim lclsGetsettings As New InMotionGIT.Common.VisualTimeConfig
        Dim physicalFile As String = String.Format("{0}\{1}", lclsGetsettings.LoadSetting("ExportDirectoryReport", "\\Reports\\", "Paths"), filename)      
        Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(physicalFile)
        
        context.Response.Clear()
        context.Response.AddHeader("Pragma", "no-cache")
        context.Response.AddHeader("Expires", "Mon, 1 Jan 2000 05:00:00 GMT")
        context.Response.AddHeader("Last-Modified", DateTime.Now.ToString("ddd, dd MMM yyyy hh:mm:ss") + " GMT")

        If (mode = "view") Then
            context.Response.ContentType = "application/pdf"
			context.Response.AddHeader("Content-Disposition", "filename=" + filename)
        Else
            context.Response.ContentType = "octet/stream"
			context.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename)
        End If
        
        context.Response.AddHeader("Content-Length", fileInfo.Length.ToString())		
        context.Response.BufferOutput = True
        context.Response.TransmitFile(physicalFile)
        context.Response.Flush()	
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class