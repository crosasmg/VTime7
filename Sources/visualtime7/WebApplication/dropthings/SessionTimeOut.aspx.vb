
Partial Class dropthings_SessionTimeOut
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("Origin") = "VT" Then
            Response.Write("<script>opener.location.href='../dropthings/Default.aspx?SessionTimeOut=Yes'</script>")
            Response.Write("<script>window.close();</script>")
            FormsAuthentication.SignOut()
        End If

        'popupDelete.ShowOnPageLoad = True

        'If IsPostBack Then
        '    FormsAuthentication.SignOut()
        '    Response.Redirect("~\dropthings\LogOnPage.aspx?SessionTimeOut=Yes")
        'End If

    End Sub
End Class
