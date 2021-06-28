#Region "using"

Imports InMotionGIT.FrontOffice.Proxy

#End Region

Partial Class ContractOwnerInformationControl
    Inherits System.Web.UI.UserControl

#Region "Methods"

    Public Sub GetUserData(selectedUserType As String, ByRef userInformation As UserService.UserInformation)
        If String.Equals(selectedUserType, "Client", StringComparison.CurrentCultureIgnoreCase) Then
            userInformation.ClientID = RegistrationCodeTextBox.Text
        End If
    End Sub

    Public Sub SetUserData(userInformation As UserService.UserInformation)
        If userInformation.UserType = UserService.EnumPortaUserType.Client Then
            RegistrationCodeTextBox.Text = userInformation.ClientID
        End If
    End Sub

#End Region
    
End Class
