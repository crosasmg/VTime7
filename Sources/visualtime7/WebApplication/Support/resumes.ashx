<%@ WebHandler Language="VB" Class="resumes" %>

Imports System
Imports System.Web
Imports System.Activities
Imports System.Activities.Statements
Imports System.Activities.DurableInstancing

Public Class resumes : Implements IHttpHandler
    
    
    Public ReadOnly Property IsReusable As Boolean Implements System.Web.IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub ProcessRequest(context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
        context.Response.Write("Ok")
        
        InMotionGIT.Workflow.Support.Runtime.ResumePending("Process Test2",
                                                    85,
                                                    1,
                                                    "A9771D3F-3784-4D5B-A374-FB519C658458",
                                                    "Andy",
                                                    Nothing)
        InMotionGIT.Workflow.Support.Runtime.Resume("Process Test2",
                                                    85,
                                                    1,
                                                    "A9771D3F-3784-4D5B-A374-FB519C658458",
                                                    "Andy",
                                                    Nothing)
    End Sub
End Class
