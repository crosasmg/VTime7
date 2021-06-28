<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PersonalInformationControl.ascx.vb"
    Inherits="PersonalInformationControl" %>
<table style='width: 100%;'>
    <tr valign='top'>
        <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="FirstNameLabel" runat="server" Text="First Name" AssociatedControlID="FirstNameTextBox"
                meta:resourcekey="FirstNameLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <dxe:ASPxTextBox ID="FirstNameTextBox" runat="server" Width="220px">
            </dxe:ASPxTextBox>
        </td>
        <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="MiddleNameLabel" runat="server" Text="Middle Name" AssociatedControlID="MiddleNameTextBox"
                meta:resourcekey="MiddleNameLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <dxe:ASPxTextBox ID="MiddleNameTextBox" runat="server" Width="220px">
            </dxe:ASPxTextBox>
        </td>
        <td style='width: 17%; padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="LastNameLabel" runat="server" Text="Last Name" AssociatedControlID="LastNameTextBox"
                meta:resourcekey="LastNameLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td style='width: 17%;' align='left'>
            <dxe:ASPxTextBox ID="LastNameTextBox" runat="server" Width="220px">
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr valign='top'>
        <td style='width: 17%; padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="SecondLastNameLabel" runat="server" Text="Second Last Name" AssociatedControlID="SecondLastNameTextBox"
                meta:resourcekey="SecondLastNameLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td style='width: 17%;' align='left'>
            <dxe:ASPxTextBox ID="SecondLastNameTextBox" runat="server" Width="220px">
            </dxe:ASPxTextBox>
        </td>
        <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="DateOfBirthLabel" runat="server" Text="Date of birth" AssociatedControlID="BirthdayDateEdit"
                meta:resourcekey="DateOfBirthLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <dxe:ASPxDateEdit runat="server" ID="BirthdayDateEdit" ClientInstanceName="BirthdayDateEdit" Width="220px">
            </dxe:ASPxDateEdit>          
        </td>
        <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="GenderLabel" runat="server" Text="Gender" AssociatedControlID="GenderComboBox"
                meta:resourcekey="GenderLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
          <dxe:ASPxComboBox ID="GenderComboBox" runat="server" AutoPostBack="false" ClientIDMode="AutoID"
                Width="120px" DropDownRows="2" ValueType="System.String" IncrementalFilteringMode="StartsWith">
                <Items>
                    <dxe:ListEditItem Text="Male" Value="Male"  meta:resourcekey="MaleItemResource"/>
                    <dxe:ListEditItem Text="Female" Value="Female"  meta:resourcekey="FemaleItemResource"/>              
                </Items>
            </dxe:ASPxComboBox>
        </td>    
    </tr>
    <tr valign='top'>        
        <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="AddressLabel" runat="server" Text="Address" AssociatedControlID="AddressMemo"
                meta:resourcekey="AddressLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <dxe:ASPxMemo ID="AddressMemo" runat="server" Width="100%">
            </dxe:ASPxMemo>
        </td>
        <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="CityLabel" runat="server" Text="City" AssociatedControlID="CityTextBox"
                meta:resourcekey="CityLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <dxe:ASPxTextBox ID="CityTextBox" runat="server" Width="220px">
            </dxe:ASPxTextBox>
        </td>
         <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="StateLabel" runat="server" Text="State" AssociatedControlID="StateTextBox"
                meta:resourcekey="StateLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <dxe:ASPxTextBox ID="StateTextBox" runat="server" Width="220px">
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr valign='top'>
       
        <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="CountryLabel" runat="server" Text="Country" AssociatedControlID="CountryComboBox"
                meta:resourcekey="CountryLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
             <dxe:ASPxComboBox ID="CountryComboBox" ClientInstanceName="CountryComboBox" runat="server" ClientIDMode='Static'
                               CallbackPageSize="20" DropDownStyle="DropDown" DropDownRows="20" AutoResizeWithContainer="false"
                               EnableCallbackMode="true" IncrementalFilteringMode="Contains" IncrementalFilteringDelay="500" FilterMinLength = "0"
                               ValueType="System.Int32" TextField="SDESCRIPT" ValueField="NCOUNTRY">
            </dxe:ASPxComboBox>
        </td>
          <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="TelephoneLabel" runat="server" Text="Telephone No." AssociatedControlID="AreaTelephoneTextBox"
                meta:resourcekey="TelephoneLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <table>
                <tr>
                    <td valign='top'>
                        <dxe:ASPxTextBox ID="AreaTelephoneTextBox" runat="server" Width="100px" ToolTip="Area Number"
                            HorizontalAlign='Right' MaskSettings-IncludeLiterals="None" ValidationSettings-ErrorTextPosition="Bottom"
                            MaskSettings-Mask='<0..9999>' meta:resourcekey="AreaTelephoneTextBoxResource">
                        </dxe:ASPxTextBox>
                    </td>
                    <td valign='top'>
                        <dxe:ASPxTextBox ID="TelephoneTextBox" runat="server" Width="100px" ToolTip="Telephone Number"
                            meta:resourcekey="TelephoneTextBoxResource" HorizontalAlign='Right' MaskSettings-IncludeLiterals="None"
                            ValidationSettings-ErrorTextPosition="Bottom" MaskSettings-Mask='<0..99999999>'>
                        </dxe:ASPxTextBox>
                    </td>
                    <td valign='top'>
                        <dxe:ASPxTextBox ID="ExtensionTelephoneTextBox" runat="server" Width="100px" ToolTip="Extension Number"
                            meta:resourcekey="ExtensionTelephoneTextBoxResource" HorizontalAlign='Right'
                            MaskSettings-IncludeLiterals="None" ValidationSettings-ErrorTextPosition="Bottom"
                            MaskSettings-Mask='<0..9999>'>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </td>
          <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="CompanyLabel" runat="server" Text="Company" AssociatedControlID="CompanyTextBox"
                meta:resourcekey="CompanyLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <dxe:ASPxTextBox ID="CompanyTextBox" runat="server" Paddings-PaddingLeft="8px" Width="220px"
                meta:resourcekey="CompanyTextBoxResource">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The company name is required' />
                </ValidationSettings>
            </dxe:ASPxTextBox>
        </td> 
    </tr>    
</table>
















