#Region "using"

Imports InMotionGIT.FrontOffice.Proxy

#End Region

Partial Class ProducerInformationControl
    Inherits System.Web.UI.UserControl

#Region "Methods"

    Public Sub GetUserData(selectedUserType As String, ByRef userInformation As UserService.UserInformation)
        If String.Equals(selectedUserType, "Agent", StringComparison.CurrentCultureIgnoreCase) Then
            userInformation.ProducerID = AgentIdTextBox.Text
        End If
    End Sub

    Public Sub SetUserData(userInformation As UserService.UserInformation)
        If userInformation.UserType = UserService.EnumPortaUserType.Agent Then
            AgentIdTextBox.Text = userInformation.ProducerID
        End If
    End Sub

#End Region

End Class
