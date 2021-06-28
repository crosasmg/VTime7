#Region "using"

Imports InMotionGIT.FrontOffice.Proxy
Imports GIT.Core.Helpers.Enumerations

#End Region

Partial Class UserPreferencesControl
    Inherits System.Web.UI.UserControl

    Public Sub GetUserData(ByRef userInformation As UserService.UserInformation)
        With userInformation
            If Not IsNothing(ThemeList.SelectedItem) Then
                .Theme = ThemeList.SelectedItem.Value
            End If

            If Not IsNothing(Language.SelectedItem) Then
                .Language = Language.SelectedItem.Value
            End If
        End With
    End Sub

    Public Sub SetUserData(userInformation As UserService.UserInformation)
        With userInformation
            If Not String.IsNullOrEmpty(.Theme) Then
                ThemeList.Value = .Theme
            End If

            If Not String.IsNullOrEmpty(.Language) Then

                Language.Value = .Language
            End If
        End With
    End Sub

End Class
