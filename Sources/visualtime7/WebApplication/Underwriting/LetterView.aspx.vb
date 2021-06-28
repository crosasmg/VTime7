#Region "using"

Imports InMotionGIT.Correspondence.Support.Mail
Imports InMotionGIT.Correspondence.Support.Enumerations
Imports System.Data

#End Region

Partial Class Underwriting_LetterView
    Inherits System.Web.UI.Page

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim jobId As String = Request.QueryString("jobid")

        If Not String.IsNullOrEmpty(jobId) Then
            Dim jobs As DataTable = RetrieveJobInformation(jobId)

            If Not IsNothing(jobs) AndAlso jobs.Rows.Count > 0 Then
                Dim job As DataRow = jobs.Rows(0)
                Dim status As EnumJobStatus = job("STATUS")

                ToTextBox.Text = job("EMAIL")
                SubjectTextBox.Text = job("SUBJECT")
                BodyHtmlEditor.Html = job("BODY")

                With StatusImage
                    Select Case status
                        Case EnumJobStatus.Sended
                            .ImageUrl = "~/Underwriting/Images/emailSended.png"
                            .ToolTip = GetLocalResourceObject("SendedToolTip")

                        Case EnumJobStatus.Sending
                            .ImageUrl = "~/Underwriting/Images/emailSending.png"
                            .ToolTip = GetLocalResourceObject("SendingToolTip")

                        Case EnumJobStatus.Pending
                            .ImageUrl = "~/Underwriting/Images/emailPending.png"
                            .ToolTip = GetLocalResourceObject("PendingToolTip")

                        Case EnumJobStatus.Retry
                            .ImageUrl = "~/Underwriting/Images/emailRetry.png"
                            .ToolTip = GetLocalResourceObject("RetryToolTip")

                        Case Else
                    End Select
                End With
             
            End If
        End If
    End Sub

#End Region

End Class
