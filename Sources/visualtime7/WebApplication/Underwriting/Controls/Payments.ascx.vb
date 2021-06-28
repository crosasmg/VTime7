#Region "using"

Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxClasses

#End Region

Partial Class Underwriting_Controls_Payments
    Inherits System.Web.UI.UserControl

#Region "Private fields, to hold the state of the entity"

    Private Const _UnderwritingSessionTimeOutaspx As String = "~\Underwriting\SessionTimeOut.aspx"

#End Region

#Region "GridView Events"

    Protected Sub gvPayments_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs) Handles gvPayments.CustomColumnDisplayText
        If IsNumeric(e.Value) AndAlso e.Value = -1 Then
            e.DisplayText = ""
        End If
    End Sub

#End Region

#Region "Main Methods"

    Public Sub RebindGridView()
        gvPayments.DataBind()
    End Sub

    Public Sub RedirectOnSessionTimeout()
        If Session("SessionTimeOut") <> "Yes" Then
            If Not Context.Session Is Nothing Then
                If Session.IsNewSession Then
                    Dim cookieHeader = Request.Headers("Cookie")

                    If Not cookieHeader Is Nothing AndAlso cookieHeader.IndexOf("ASP.NET_SessionId") >= 0 Then
                        If Page.IsCallback Then
                            ASPxWebControl.RedirectOnCallback(_UnderwritingSessionTimeOutaspx)
                        Else
                            Response.Redirect(_UnderwritingSessionTimeOutaspx)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

#End Region

End Class
