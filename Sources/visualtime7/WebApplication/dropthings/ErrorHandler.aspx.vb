Imports System.Web.Script.Services
Imports System.Web.Services
Imports GIT.Core

Partial Class dropthings_ErrorHandler
    Inherits PageBase

#Region "Method"

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function MessageConfiguration(code As String, detail As String) As Object
        Dim DetailVisible As Boolean = False
        Dim Message As String = ""
        Dim result As New Object
        Dim page = DirectCast(HttpContext.Current.CurrentHandler, Page)
        Dim urlRelativePath As String = page.AppRelativeVirtualPath.Replace("~", "")
        Message = HttpContext.GetLocalResourceObject(urlRelativePath, "ErrorMessage", System.Threading.Thread.CurrentThread.CurrentCulture)
        If code.IsEmpty Then
        Else

        End If

        If System.Configuration.ConfigurationManager.AppSettings("ErrorDetails").IsNotEmpty() AndAlso
                            System.Configuration.ConfigurationManager.AppSettings("ErrorDetails").ToString().ToLower().Equals("true") Then
            DetailVisible = True
        End If

        result = New With {.Message = Message, .DetailVisible = DetailVisible}
        Return result
    End Function

#End Region

    Private Sub dropthings_ErrorHandler_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim message As String = GetLocalResourceObject("ErrorMessage")
        Dim messageDetail As String = String.Empty
        lblErrorDetail.Visible = False
        If Request.QueryString("CustomCode").IsEmpty() Then
            If Request.QueryString("ErrorMessage").IsNotEmpty() Then
                message = Request.QueryString("ErrorMessage")
            End If

            If message.IsEmpty Then
                message = GetLocalResourceObject("ErrorMessage")
            End If

            If Request.QueryString("detail").IsNotEmpty() AndAlso
                        System.Configuration.ConfigurationManager.AppSettings("ErrorDetails").IsNotEmpty() AndAlso
                            System.Configuration.ConfigurationManager.AppSettings("ErrorDetails").ToString().ToLower().Equals("true") Then
                lblErrorDetail.Text = String.Format("<pre>{0}</pre>", HttpContext.Current.Application(Request.QueryString("detail")))
                lblErrorDetail.Visible = True
            End If

            ErrorLabel.Text = message
        Else

            Select Case Request.QueryString("CustomCode")
                Case "403"
                    messageDetail = "Acceso denegado"
                Case "404"
                    messageDetail = "Recurso no encontrado"
                Case Else
                    messageDetail = "Error desconocido"
            End Select

            If System.Configuration.ConfigurationManager.AppSettings("ErrorDetails").IsNotEmpty() AndAlso
                            System.Configuration.ConfigurationManager.AppSettings("ErrorDetails").ToString().ToLower().Equals("true") Then
                lblErrorDetail.Text = String.Format("<pre>{0}</pre>", messageDetail)
                lblErrorDetail.Visible = True
            End If
            ErrorLabel.Text = message
        End If

    End Sub

End Class