#Region "using"

Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports InMotionGIT.Common.Proxy

#End Region

Partial Class Underwriting_Controls_Communication
    Inherits System.Web.UI.UserControl

#Region "Private fields, to hold the state of the entity"

    Private Const _UnderwritingSessionTimeOutaspx As String = "~\Underwriting\SessionTimeOut.aspx"

#End Region

#Region "GridView Events"

    Protected Function GetRowValue(container As GridViewDataItemTemplateContainer) As String
        Return container.Grid.GetRowValuesByKeyValue(container.KeyValue, "JobId").ToString()
    End Function

    Protected Sub gvCommunication_HtmlRowCreated(ByVal sender As Object, ByVal e As ASPxGridViewTableRowEventArgs) Handles gvCommunication.HtmlRowCreated
        If e.RowType <> GridViewRowType.Data Then
            Return
        End If

        Dim dataColumn As GridViewDataColumn = TryCast(TryCast(sender, ASPxGridView).Columns("EntryType"), GridViewDataColumn)
        Dim entryImage As ASPxImage = TryCast(sender, ASPxGridView).FindRowCellTemplateControl(e.VisibleIndex, dataColumn, "entryImage")
        Dim entry As Integer = Convert.ToInt32(DirectCast(sender, ASPxGridView).GetRowValues(e.VisibleIndex, "EntryType"))

        If Not IsNothing(entryImage) Then
            Select Case entry
                Case 1
                    entryImage.ImageUrl = "..//Images/email.png"

                Case Else
                    entryImage.ImageUrl = "..//Images/empty.png"

            End Select
        End If
    End Sub

#End Region

#Region "Main Methods"

    Public Sub RebindGridView()
        gvCommunication.DataBind()
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

#Region "Lookups"

    Protected Sub gvCommunication_DataBinding(sender As Object, e As System.EventArgs) Handles gvCommunication.DataBinding
        With DirectCast(gvCommunication.Columns("Role"), GridViewDataComboBoxColumn)
            .PropertiesComboBox.DataSource = InMotionGIT.Underwriting.Proxy.Lookups.RolesLkp(Session("LanguageID"), False)
        End With
    End Sub


#End Region
End Class