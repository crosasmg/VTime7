Partial Class UserTypeUserControl
    Inherits AuthenticationUserControlBase

    Public Property SelectedUserType As Integer

    Protected Sub NextButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextButton.Click
        If SecurityCodeCaptcha.IsValid Then

            Dim value As String = IamRadioButtonList.SelectedItem.Value
            Dim url = ConfigurationManager.AppSettings("FASI.DevExpress.Security.UserRegistrationPage")
            Select Case value
                Case "Agent"
                    value = "2"
                Case "Client"
                    value = "1"
            End Select

            Response.Redirect(String.Format("{0}?&typeUser={1}", url, value))

        End If
    End Sub

End Class

Public Class AuthenticationUserControlBase
    Inherits UserControl

    Public Event Navigation(ByVal sender As Object, ByVal tagName As String)

    Protected Overridable Sub OnNavigation(ByVal sender As Object, ByVal tagName As String)
        RaiseEvent Navigation(sender, tagName)
    End Sub

End Class