Imports System.Globalization
Imports System.Data
Imports InMotionGIT.Common.Extensions

Partial Class Support_qe
    Inherits System.Web.UI.Page

#Region "Event page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString.IsNotEmpty AndAlso Request.QueryString("Key").IsNotEmpty Then
                If InMotionGIT.Common.Helpers.KeyValidator.KeyValidator(Request.QueryString("Key")) Then
                    txtQuery.Visible = True
                    cbxBackOffice.Visible = True
                    cbxFrontOffice.Visible = True
                    btnExecute.Visible = True
                    btnExecute.Visible = True
                End If
            End If
        End If
    End Sub

    Protected Sub btnExecute_Click(sender As Object, e As System.EventArgs) Handles btnExecute.Click
        Dim resultQuery As DataTable = Nothing
        Try
            With New InMotionGIT.Common.Proxy.DataManagerFactory(txtQuery.Text,
                                                                 "GENERIC",
                                                                 If(cbxBackOffice.Checked,
                                                                                      "BackOfficeConnectionString",
                                                                                      "FrontOfficeConnectionString").ToString)
                resultQuery = .QueryExecuteToTable(True)
                If resultQuery.Rows.Count >= 200 Then
                    For index = 200 To resultQuery.Rows.Count - 1
                        resultQuery.Rows(index).Delete()
                    Next
                    resultQuery.AcceptChanges()
                End If
            End With
        Catch ex As Exception
            Dim messageUser As String = "An error occurred while trying to execute the query, please check details in the error log"
            Throw New Exception(messageUser)
        End Try

        grvResult.DataSource = resultQuery
        grvResult.AutoGenerateColumns = True
        grvResult.DataBind()
    End Sub
#End Region


End Class





