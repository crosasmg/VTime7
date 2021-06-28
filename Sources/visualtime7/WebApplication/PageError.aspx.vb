Imports GIT.Core

Partial Class generated_pageError
    Inherits PageBase

    Public message As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lasterror As Exception = Session("LastError")

        If Not IsNothing(lasterror) Then
            Session.Remove("LastError")
            message = lasterror.Message
        End If
    End Sub
End Class
