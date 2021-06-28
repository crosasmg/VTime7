<%@ WebHandler Language="VB" Class="WorkflowExecute" %>

#Region "using"

Imports System
Imports System.Web
Imports InMotionGIT.Workflow.Support.Runtime
Imports InMotionGIT.Common.Contracts
Imports InMotionGIT.FrontOffice.Support

#End Region

Public Class WorkflowExecute : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            With context.Request
                If Not IsNothing(.QueryString) AndAlso .QueryString.Count > 0 Then
                    Dim arguments As New Dictionary(Of String, Object)
                    Dim modelName As String = .QueryString("WorkflowName")
                    Dim modelId As String = .QueryString("WorkflowModelId")
                    Dim queryName As String = .QueryString("queryName")
                    Dim queryModelId As String = .QueryString("queryModelId")
                    Dim message As String = .QueryString("executedMessage")
                    Dim formContext As New Context(LanguageHelper.CurrentCultureToLanguage, queryModelId)

                    For Each queryString As String In .QueryString.AllKeys
                        If ValidateQueryStringName(queryString) Then
                            If String.Equals(queryString, "context", StringComparison.CurrentCultureIgnoreCase) Then
                                arguments.Add(queryString, formContext)
                            Else
                                arguments.Add(queryString, .QueryString(queryString))
                            End If
                        End If
                    Next

                    DoWorkFromForm(modelName, modelId, -1, arguments, False, False, queryName, queryModelId)

                    context.Response.Write(message)
                End If
            End With

        Catch ex As Exception
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("WorkflowExecute", "ProcessRequest", ex)


            context.Response.Write(ex.Message)
        End Try
    End Sub

    Private Function ValidateQueryStringName(queryString As String) As Boolean
        Dim result As Boolean = True

        Select Case queryString
            Case "WorkflowName",
                 "WorkflowModelId",
                 "queryName",
                 "queryModelId",
                 "executedMessage"
                result = False
        End Select

        Return result
    End Function

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class