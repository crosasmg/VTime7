<%@ WebHandler Language="VB" Class="DeployPackages" %>

Public Class DeployPackages : Implements IHttpHandler

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
        InMotionGIT.Workbench.Deploy.Handler.ProcessRequest(context)
    End Sub

End Class