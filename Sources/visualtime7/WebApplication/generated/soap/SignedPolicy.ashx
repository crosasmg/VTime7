<%@ WebHandler Language="VB" Class="SignedPolicy" %>

Imports System
Imports System.Web
Imports System.Diagnostics

Public Class SignedPolicy : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim patente As String = context.Request.QueryString("patente")
        Dim status As InMotionGIT.Chile.Entity.Contracts.Acepta.FirmaStatus
		status = (New InMotionGIT.Chile.Services.Manager).SignedPolicyGetPDF(66, 0, context.Request.QueryString("poliza"), context.Request.QueryString("patente"))
		If Not status.WithError AndAlso status.SignedPDFFullPath.IsNotEmpty Then
			context.Response.Buffer = True
			context.Response.Clear()
			context.Response.AddHeader("content-disposition", "attachment; filename=" + IO.Path.GetFileName(status.SignedPDFFullPath))
			context.Response.ContentType = "octet/stream"

			context.Response.WriteFile(status.SignedPDFFullPath)
		Else
			context.Response.Write("<p align='center'>En este momento no se encuentra disponible la póliza para su impresión, por favor intente en pocos minutos.<p>")
		End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get       
    End Property
    
End Class