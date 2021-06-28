<%@ WebHandler Language="VB" Class="download" %>

#Region "using"

Imports System.Web
Imports System.Web.Security
Imports System.Collections.Generic
Imports System.Globalization

#End Region

Public Class download : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim filename As String = context.Request.QueryString("File")
        Dim directory As String = context.Request.QueryString("Directory")

        If Not String.IsNullOrEmpty(directory) Then
            filename = String.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}",
                                     ConfigurationManager.AppSettings("Path.Workbench.Synch"), directory, filename)
        End If

        'Validate the file name and make sure it is one that the user may access
        context.Response.Buffer = True
        context.Response.Clear()
        context.Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=") & filename)
        context.Response.ContentType = "octet/stream"

        context.Response.WriteFile(filename)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class