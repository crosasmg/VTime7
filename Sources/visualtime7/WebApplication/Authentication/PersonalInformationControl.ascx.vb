#Region "using"

Imports DevExpress.Web.ASPxEditors
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.Common.Proxy

#End Region

Partial Class PersonalInformationControl
    Inherits System.Web.UI.UserControl

#Region "Methods"

    Public Sub ShowHideControls(selectedUserType As String)
        If String.Equals(selectedUserType, "Agent") Then
            CompanyLabel.Visible = True
            CompanyTextBox.Visible = True

        Else
            CompanyLabel.Visible = False
            CompanyTextBox.Visible = False
        End If

        TelephoneLabel.Visible = True
        AreaTelephoneTextBox.Visible = True
        TelephoneTextBox.Visible = True
        ExtensionTelephoneTextBox.Visible = True

        LoadCountryDataSource()
    End Sub

    Public Sub GetUserData(ByRef userInformation As UserService.UserInformation)
        With userInformation
            .FirstName = FirstNameTextBox.Text
            .SurName = MiddleNameTextBox.Text
            .LastName = LastNameTextBox.Text
            .SecondLastName = SecondLastNameTextBox.Text

            If Not String.IsNullOrEmpty(BirthdayDateEdit.Text) Then
                .DateOfBirth = BirthdayDateEdit.Text
            End If

            If Not IsNothing(GenderComboBox.SelectedItem) Then
                .Gender = GenderComboBox.SelectedItem.Value
            End If

            .CompanyName = CompanyTextBox.Text
            .AddressHome = AddressMemo.Text

            .City = CityTextBox.Text
            .State = StateTextBox.Text

            If Not IsNothing(CountryComboBox.SelectedItem) Then
                .Country = CountryComboBox.SelectedItem.Text
            End If

            If Not String.IsNullOrEmpty(AreaTelephoneTextBox.Text) Then
                .AreaNumber = AreaTelephoneTextBox.Text
            End If

            If Not String.IsNullOrEmpty(TelephoneTextBox.Text) Then
                .TelephoneNumber = TelephoneTextBox.Text
            End If

            If Not String.IsNullOrEmpty(ExtensionTelephoneTextBox.Text) Then

                .ExtensionNumber = ExtensionTelephoneTextBox.Text
            End If
        End With
    End Sub

    Public Sub SetUserData(userInformation As UserService.UserInformation)
        With userInformation
            FirstNameTextBox.Text = .FirstName
            MiddleNameTextBox.Text = .SurName
            LastNameTextBox.Text = .LastName
            SecondLastNameTextBox.Text = .SecondLastName

            If Not IsNothing(.DateOfBirth) AndAlso Not String.IsNullOrEmpty(.DateOfBirth.ToString) Then
                BirthdayDateEdit.Text = .DateOfBirth
            End If

            CompanyTextBox.Text = .CompanyName
            AddressMemo.Text = .AddressHome

            CityTextBox.Text = .City
            StateTextBox.Text = .State

            If Not IsNothing(.Gender) AndAlso Not String.IsNullOrEmpty(.Gender) Then
                GenderComboBox.Value = .Gender
            End If

            If Not IsNothing(.Country) AndAlso Not String.IsNullOrEmpty(.Country) Then
                CountryComboBox.Value = .Country
            End If

            If Not String.IsNullOrEmpty(.AreaNumber) Then
                AreaTelephoneTextBox.Text = .AreaNumber
            End If

            If Not String.IsNullOrEmpty(.TelephoneNumber) Then
                TelephoneTextBox.Text = .TelephoneNumber
            End If

            If Not String.IsNullOrEmpty(.ExtensionNumber) Then
                ExtensionTelephoneTextBox.Text = .ExtensionNumber
            End If
        End With
    End Sub

    Private Sub LoadCountryDataSource()
        If IsNothing(CountryComboBox.DataSource) Then
            With New DataManagerFactory("SELECT NCOUNTRY, SDESCRIPT FROM INSUDB.TABLE66 WHERE NOT SDESCRIPT IS NULL AND SSTATREGT = 1 ORDER BY SDESCRIPT",
                                        "TABLE66", "BackOfficeConnectionString")

                CountryComboBox.DataSource = .QueryExecuteToTable(True)
                CountryComboBox.DataBind()
            End With
        End If
    End Sub
#End Region

End Class
