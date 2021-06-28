<%@ WebHandler Language="VB" Class="cache" %>

Imports System
Imports System.Web
Imports System.Activities
Imports System.Activities.Statements
Imports System.Activities.DurableInstancing

Public Class cache : Implements IHttpHandler
    
    
    Public ReadOnly Property IsReusable As Boolean Implements System.Web.IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub ProcessRequest(context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
        Dim clean As Boolean = context.Request.QueryString("clean").IsNotEmpty AndAlso
                               context.Request.QueryString("clean").ToString.StartsWith("y", StringComparison.CurrentCultureIgnoreCase)
        
        Dim cache As Boolean = context.Request.QueryString("cache").IsNotEmpty AndAlso
                               context.Request.QueryString("cache").ToString.StartsWith("y", StringComparison.CurrentCultureIgnoreCase)

        If clean Then
            InMotionGIT.Common.Helpers.Caching.Clean()            
            context.Response.Write("<b>Cleaned</b></br>")           
        End If
        
        If cache AndAlso
           IO.Directory.Exists("C:\VisualTimeNet\Temp\Cache") Then
            
            CleanDirectory("C:\VisualTimeNet\Temp\Cache")
            
            context.Response.Write("<b>Cache Cleaned</b></br>")
        End If
        
        With context.Response
            .Write(InMotionGIT.Common.Helpers.Caching.CacheCatalog())
        End With
		
    End Sub
    
    
    Private Sub CleanDirectory(path As String)
        For Each folder In IO.Directory.GetDirectories(path)
            CleanDirectory(folder)
        Next
        For Each file In IO.Directory.GetFiles(path)
            IO.File.Delete(file)
        Next
    End Sub
    
    
End Class
