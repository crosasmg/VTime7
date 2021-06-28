<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserRegistrationUserControl.ascx.vb" Inherits="UserRegistrationUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgbtnSeeTermsInformationMessageResource = '<asp:Localize runat="server" Text="Please wait..." meta:resourcekey="btnSeeTermsInformationMessageResource"></asp:Localize>';
    var titlebtnSeeTermsInformationMessageResource = '<asp:Localize runat="server" Text="Information Message" meta:resourcekey="titlebtnSeeTermsInformationMessageResource"></asp:Localize>';
    var msgbtnRegisterInformationMessageResource = '<asp:Localize runat="server" Text="Please wait..." meta:resourcekey="btnRegisterInformationMessageResource"></asp:Localize>';
    var titlebtnRegisterInformationMessageResource = '<asp:Localize runat="server" Text="Information Message" meta:resourcekey="titlebtnRegisterInformationMessageResource"></asp:Localize>';
</script>

<script src="/generated/form/UserRegistration.js" type="text/javascript"></script>
<asp:UpdatePanel ID="UserRegistrationUpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources" />
        <table id='UserRegistrationTablePage' runat='server' style='width: 100%; margin: auto;'>
            <tr valign='top'>
                <td colspan='2'></td>
            </tr>
            <tr valign='top'>
                <td style='width: 100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneAccount" ClientInstanceName="zoneAccount" runat="server" HeaderText="Account information" ToolTip="Account information" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneAccountResource"
                        Width="100%" SkinID="CaptionAndSquareBorder">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table style='width: 100%;'>
                                    <tr valign='top'>
                                        <td colspan='4'></td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 25%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='UserNameLabel' EncodeHtml='false' ClientInstanceName='UserNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="UserNameLabelResource" Text="User Name" ClientEnabled='true' ClientVisible='true' AssociatedControlID='UserName'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 75%;' colspan='3' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='UserName' ClientInstanceName='UserName' ToolTip="UserName" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="UserNameResource" Width='180px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zoneAccount">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                </ValidationSettings>
                                                <ClientSideEvents ValueChanged="UserNameValueChanged" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 25%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='EmailLabel' EncodeHtml='false' ClientInstanceName='EmailLabel' runat='server' ClientIDMode='Static' meta:resourcekey="EmailLabelResource" Text="Email" ClientEnabled='true' ClientVisible='true' AssociatedControlID='Email'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 25%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='Email' ClientInstanceName='Email' ToolTip="Email" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="EmailResource" Width='270px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$" ErrorText="Invalid email" />
                                                </ValidationSettings>
                                                <ClientSideEvents ValueChanged="EmailValueChanged" />
                                            </dxe:ASPxTextBox>
                                        </td>

                                        <td style='width: 25%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='EmailVerificationLabel' EncodeHtml='false' ClientInstanceName='EmailVerificationLabel' runat='server' ClientIDMode='Static' meta:resourcekey="EmailVerificationLabelResource" Text="Email Verification:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='EmailVerification'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 25%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='EmailVerification' ClientInstanceName='EmailVerification' ToolTip="Email Verification" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="EmailVerificationResource" Width='270px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zoneAccount">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$" ErrorText="Invalid email" />
                                                </ValidationSettings>
                                                <ClientSideEvents ValueChanged="EmailVerificationValueChanged" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 25%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='SecurityQuestionLabel' EncodeHtml='false' ClientInstanceName='SecurityQuestionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SecurityQuestionLabelResource" Text="Security Question:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='SecurityQuestion'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 25%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='SecurityQuestion' ClientInstanceName='SecurityQuestion' ToolTip="Security Question" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="SecurityQuestionResource" Width='270px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zoneAccount">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>

                                        <td style='width: 25%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='SecurityAnswerLabel' EncodeHtml='false' ClientInstanceName='SecurityAnswerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SecurityAnswerLabelResource" Text="Security Answer:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='SecurityAnswer'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 25%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='SecurityAnswer' ClientInstanceName='SecurityAnswer' ToolTip="Security Answer" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="SecurityAnswerResource" Width='270px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zoneAccount">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr valign='top'>
                <td style='width: 100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zonePersonal" ClientInstanceName="zonePersonal" runat="server" HeaderText="Personal information" ToolTip="Personal information" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zonePersonalResource"
                        Width="100%" SkinID="CaptionAndSquareBorder">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table style='width: 100%;'>
                                    <tr valign='top'>
                                        <td colspan='6'></td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='FirstNameLabel' EncodeHtml='false' ClientInstanceName='FirstNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FirstNameLabelResource" Text="First Name:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='FirstName'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 16.5%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='FirstName' ClientInstanceName='FirstName' ToolTip="text10" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="FirstNameResource" Width='160px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zonePersonal">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>

                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='SurNameLabel' EncodeHtml='false' ClientInstanceName='SurNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SurNameLabelResource" Text="SurName:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='SurName'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 16.5%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='SurName' ClientInstanceName='SurName' ToolTip="text13" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="SurNameResource" Width='160px' ClientEnabled='True'>
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zonePersonal">
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>

                                        <td style='width: 17%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='LastNameLabel' EncodeHtml='false' ClientInstanceName='LastNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastNameLabelResource" Text="LastName:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='LastName'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 17%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='LastName' ClientInstanceName='LastName' ToolTip="text11" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="LastNameResource" Width='160px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zonePersonal">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='SecondLastNameLabel' EncodeHtml='false' ClientInstanceName='SecondLastNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SecondLastNameLabelResource" Text="Second LastName:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='SecondLastName'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 16.5%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='SecondLastName' ClientInstanceName='SecondLastName' ToolTip="text12" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="SecondLastNameResource" Width='160px' ClientEnabled='True'>
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zonePersonal">
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>

                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='DateOfBirthLabel' EncodeHtml='false' ClientInstanceName='DateOfBirthLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateOfBirthLabelResource" Text="Date Of Birth:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='DateOfBirth'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 16.5%;' align='left'>

                                            <dxe:ASPxDateEdit runat='server' ID='DateOfBirth' ToolTip="datepicker14" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateOfBirthResource" Width='160px' ClientEnabled='True'>
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zonePersonal">
                                                </ValidationSettings>
                                            </dxe:ASPxDateEdit>
                                        </td>

                                        <td style='width: 17%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='GenderLabel' EncodeHtml='false' ClientInstanceName='GenderLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GenderLabelResource" Text="Gender:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='Gender'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 17%;' align='left'>

                                            <dxe:ASPxComboBox ID='Gender' runat='server' ClientInstanceName='Gender' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="dropdownlist15" ClientVisible='true' ClientEnabled='True' meta:resourcekey="GenderResource" Width='160px' ValueType='System.String'>
                                                <Items>
                                                    <dxe:ListEditItem Value='Male' Text='Male' meta:resourcekey="GenderListItemValue1Resource" />
                                                    <dxe:ListEditItem Value='Female' Text='Female' meta:resourcekey="GenderListItemValue2Resource" />
                                                </Items>
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zonePersonal">
                                                </ValidationSettings>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='AddressHomeLabel' EncodeHtml='false' ClientInstanceName='AddressHomeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AddressHomeLabelResource" Text="Address Home:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='AddressHome'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 16.5%;' align='left'>

                                            <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='AddressHome' ToolTip="notes16" meta:resourcekey="AddressHomeResource" Columns='35' Rows='3' Size='0' NullText="" ClientVisible='True' ClientEnabled='True'>
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zonePersonal">
                                                </ValidationSettings>
                                            </dxe:ASPxMemo>
                                        </td>

                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='CityLabel' EncodeHtml='false' ClientInstanceName='CityLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CityLabelResource" Text="City:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='City'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 16.5%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='City' ClientInstanceName='City' ToolTip="text17" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="CityResource" Width='160px' ClientEnabled='True'>
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zonePersonal">
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>

                                        <td style='width: 17%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='StateLabel' EncodeHtml='false' ClientInstanceName='StateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="StateLabelResource" Text="State:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='State'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 17%;' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='State' ClientInstanceName='State' ToolTip="text18" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="StateResource" Width='160px' ClientEnabled='True'>
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zonePersonal">
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='CountryLabel' EncodeHtml='false' ClientInstanceName='CountryLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CountryLabelResource" Text="Country:" ClientEnabled='true' ClientVisible='true' AssociatedControlID='Country'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 16.5%;' align='left'>

                                            <dxe:ASPxComboBox ID='Country' runat='server' ClientInstanceName='Country' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="text19" ClientVisible='true' ClientEnabled='True' meta:resourcekey="CountryResource" Width='160px' ValueType='System.Int32' TextField='SDESCRIPT' ValueField='NCOUNTRY'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zonePersonal">
                                                    <RequiredField IsRequired='true' ErrorText="" />
                                                </ValidationSettings>
                                            </dxe:ASPxComboBox>
                                        </td>

                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='AreaNumberLabel' EncodeHtml='false' ClientInstanceName='AreaNumberLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AreaNumberLabelResource" Text="AreaNumber" ClientEnabled='true' ClientVisible='true' AssociatedControlID='AreaNumber'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 49.5%;' colspan='5' align='left'>
                                            <div style='float: left;'>

                                                <dxe:ASPxTextBox runat='server' ID='AreaNumber' ClientInstanceName='AreaNumber' ToolTip="numeric1" Size='3' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="AreaNumberResource" Width='36px' Text='0' ClientEnabled='true' ClientVisible='true' MaskSettings-Mask=' <0..999>'>
                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="zonePersonal">
                                                    </ValidationSettings>
                                                </dxe:ASPxTextBox>
                                            </div>

                                            <dxe:ASPxLabel ID='TelephoneNumberLabel' EncodeHtml='false' ClientInstanceName='TelephoneNumberLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TelephoneNumberLabelResource" Text="TelephoneNumber" ClientEnabled='true' ClientVisible='true' AssociatedControlID='TelephoneNumber'></dxe:ASPxLabel>
                                            <div style='float: left;'>

                                                <dxe:ASPxTextBox runat='server' ID='TelephoneNumber' ClientInstanceName='TelephoneNumber' ToolTip="numeric3" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TelephoneNumberResource" Width='90px' Text='0' ClientEnabled='true' ClientVisible='true' MaskSettings-Mask=' <0..99999999>'>
                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="zonePersonal">
                                                    </ValidationSettings>
                                                </dxe:ASPxTextBox>
                                            </div>

                                            <dxe:ASPxLabel ID='ExtensionNumberLabel' EncodeHtml='false' ClientInstanceName='ExtensionNumberLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ExtensionNumberLabelResource" Text="ExtensionNumber" ClientEnabled='true' ClientVisible='true' AssociatedControlID='ExtensionNumber'></dxe:ASPxLabel>
                                            <div style='float: left;'>

                                                <dxe:ASPxTextBox runat='server' ID='ExtensionNumber' ClientInstanceName='ExtensionNumber' ToolTip="numeric2" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="ExtensionNumberResource" Width='45px' Text='0' ClientEnabled='true' ClientVisible='true' MaskSettings-Mask=' <0..9999>'>
                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="zonePersonal">
                                                    </ValidationSettings>
                                                </dxe:ASPxTextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr valign='top'>
                <td style='width: 100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneIntermediary" ClientInstanceName="zoneIntermediary" runat="server" HeaderText="Information intermediary" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneIntermediaryResource"
                        Width="100%" SkinID="CaptionAndRoundedBorder">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table style='width: 100%;'>
                                    <tr valign='top'>
                                        <td colspan='6'></td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='text1Label' EncodeHtml='false' ClientInstanceName='text1Label' runat='server' ClientIDMode='Static' meta:resourcekey="text1LabelResource" Text="Identification of the agent" ClientEnabled='true' ClientVisible='true' AssociatedControlID='text1'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 83.5%;' colspan='5' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='text1' ClientInstanceName='text1' ToolTip="text1" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="text1Resource" Width='135px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zoneIntermediary">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                </ValidationSettings>
                                                <ClientSideEvents ValueChanged="IntermediaryValueChanged" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr valign='top'>
                <td style='width: 100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneClient" ClientInstanceName="zoneClient" runat="server" HeaderText="Contract owner information" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneClientResource"
                        Width="100%" SkinID="CaptionAndRoundedBorder">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table style='width: 100%;'>
                                    <tr valign='top'>
                                        <td colspan='6'></td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 16.5%; padding-top: 3px;' colspan='1' align='Left'>
                                            <dxe:ASPxLabel ID='text3Label' EncodeHtml='false' ClientInstanceName='text3Label' runat='server' ClientIDMode='Static' meta:resourcekey="text3LabelResource" Text="Customer identification code" ClientEnabled='true' ClientVisible='true' AssociatedControlID='text3'></dxe:ASPxLabel>
                                        </td>
                                        <td style='width: 83.5%;' colspan='5' align='left'>

                                            <dxe:ASPxTextBox runat='server' ID='text3' ClientInstanceName='text3' ToolTip="text3" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="text3Resource" Width='135px' ClientEnabled='True'>
                                                <Paddings PaddingLeft="8px" />
                                                <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zoneClient">
                                                    <RequiredField IsRequired='True' ErrorText="" />
                                                </ValidationSettings>
                                                <ClientSideEvents ValueChanged="ClientValueChanged" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr valign='top'>
                <td style='width: 100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneTerm" ClientInstanceName="zoneTerm" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneTermResource"
                        Width="100%" SkinID="SquareBorderAndNotCaption">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table style='width: 100%;'>
                                    <tr valign='top'>
                                        <td colspan='2'></td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 100%; padding-top: 3px;' colspan='2' align='Center'>
                                            <dxe:ASPxCheckBox ID='AcceptConditions' runat='server' Text="I accept the Terms and Conditions of Use" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="AcceptConditions" AutoPostBack='false' EncodeHtml='false'>
                                                <ClientSideEvents CheckedChanged="AcceptConditionsCheckedChanged" />
                                            </dxe:ASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 100%' colspan='2' align='Center'>

                                            <dxe:ASPxButton ID='btnSeeTerms' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="See Terms" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnSeeTermsResource" Text="See Terms and Conditions of Use" Height='16px' AutoPostBack='false'>
                                                <ClientSideEvents Click="btnSeeTermsClick" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr valign='top'>
                <td style='width: 100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone32" ClientInstanceName="zone32" runat="server" HeaderText="zone" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone32Resource"
                        Width="100%" SkinID="SquareBorderAndNotCaption">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table style='width: 100%;'>
                                    <tr valign='top'>
                                        <td colspan='2'></td>
                                    </tr>
                                    <tr valign='top'>
                                        <td style='width: 100%' colspan='2' align='Center'>

                                            <dxe:ASPxButton ID='btnRegister' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="button31" ClientVisible='True' ClientEnabled='False' meta:resourcekey="btnRegisterResource" Text="Register." OnClick='btnRegister_Click' AutoPostBack='false'>
                                                <ClientSideEvents Click="btnRegisterClick" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr valign='top'>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr valign='top'>
                <td style='width: 100%; padding-top: 3px;' colspan='2' align='Center'>
                    <dxe:ASPxLabel ID='lblResult' EncodeHtml='false' ClientInstanceName='lblResult' runat='server' ClientIDMode='Static' meta:resourcekey="lblResultResource" Text="lblResult" ClientEnabled='true' ClientVisible='true'></dxe:ASPxLabel>
                </td>
            </tr>
            <tr valign='top'>
            </tr>
        </table>

        <dxlp:ASPxLoadingPanel ID="LoadingPanelGridView" runat="server" ClientInstanceName="LoadingPanelGridView" Modal="True" Text="<%$ Resources:Resource, Working %>" />
        <table style="width: 100%;">
            <tr valign='top'>
                <td>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanelErrors" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="ErrorsGridView" runat="server" AutoGenerateColumns="False" Visible="False"
                                SkinID="Main" Width="74px" PageSize="50">
                                <Columns>
                                    <asp:TemplateField HeaderText="" SortExpression="Severity">
                                        <ItemTemplate>
                                            <asp:Image ID="imgButton" runat="server" ImageUrl='<%# eval("SeverityImage") %>'></asp:Image>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# eval("Category") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ErrorButton" runat="server" Text='<%# eval("Message") %>' CommandArgument="<%# DirectCast(Container,GridViewRow).RowIndex %>" CommandName='<%# eval("ControlIdFullPath") %>'> </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="ErrorLabel" runat="server" Font-Bold="True" ForeColor="Red" Text="Errors"></asp:Label>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <dxpc:ASPxPopupControl ShowPageScrollbarWhenModal="true" ID="popControl" runat="server" ClientInstanceName="popControl"
            ShowCloseButton="False" CloseAction="None" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            EnableHotTrack="False">
            <SizeGripImage Height="16px" Width="16px" />
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <CloseButtonImage Height="12px" Width="13px" />
            <HeaderStyle>
                <Paddings PaddingRight="6px" />
            </HeaderStyle>
            <Windows>
                <dxpc:PopupWindow Name="pwUno" Modal="true">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl>
                            <table width="100%">
                                <tr>
                                    <td width="100%" colspan="2" align="center">
                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage"></dxe:ASPxLabel>
                                        <br />
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%" align="right">
                                        <dxe:ASPxButton ID="btnConfirm" runat="server" AutoPostBack="False"
                                            ClientInstanceName="btnConfirm" Text="Confirm">
                                            <ClientSideEvents Click="function(s,e){Confirmation_Actions();}" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td width="50%">
                                        <dxe:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False"
                                            ClientInstanceName="btnCancel" Text="Cancel">
                                            <ClientSideEvents Click="function(s,e){ASPxClientPopupControl.GetPopupControlCollection().HideAllWindows();}" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:PopupWindow>
            </Windows>
        </dxpc:ASPxPopupControl>
        <asp:Label ID="_FormID" runat="server" Visible="False"></asp:Label>
        <table id='MessageTable' runat='server' style='width: 100%;' visible='false'>
            <tr valign='top' align='center'>
                <td>
                    <div class='FormMessage'>
                        <dxe:ASPxLabel EncodeHtml='false' ClientInstanceName='FormMessageLabel' ID='FormMessageLabel' ClientIDMode='Static' runat='server' Text=''>
                        </dxe:ASPxLabel>
                    </div>
                </td>
            </tr>
        </table>
        <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" SkinID="GroupBoxRoundedBorder"
            ID="popupDelete" runat="server" ClientInstanceName="popupDelete" EnableHotTrack="False">
            <SizeGripImage Height="16px" Width="16px" />
            <ClientSideEvents Init="function(s,e){
                                    popupDelete_Init(popupDelete)
                                    } " />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="Popupcontrolcontentcontrol1" runat="server">
                    <uc1:ConfirmDelete ID="ConfirmDelete1" runat="server" />
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <CloseButtonImage Height="12px" Width="13px" />
            <HeaderStyle>
                <Paddings PaddingRight="6px" />
            </HeaderStyle>
        </dxpc:ASPxPopupControl>

        <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupNotifyMessage" HeaderText=" "
            runat="server" ClientInstanceName="popupNotifyMessage" EnableHotTrack="False" Height="26px">
            <ModalBackgroundStyle>
                <BackgroundImage HorizontalPosition="center" />
            </ModalBackgroundStyle>
            <SizeGripImage Height="16px" Width="16px" />
            <ContentCollection>
                <dxpc:PopupControlContentControl>
                    <table style='width: 100%;'>
                        <tr>
                            <td rowspan="2">
                                <dxe:ASPxImage ID="MessageImage" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                                </dxe:ASPxImage>
                            </td>
                            <td>
                                <asp:Literal ID='NotifyMessageLabel' Text="" runat='server'>
                                </asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style='width: 100%;'>
                        <tr>
                            <td rowspan="2" align='Center'>
                                <dxe:ASPxButton ID="btnOkNotificy" runat="server" AutoPostBack="False" Text="<%$ Resources:Resource, AcceptBtnResource %>"
                                    Height="22px" Width="60px">
                                    <ClientSideEvents Click="function(s, e) {popupNotifyMessage.Hide(); }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <CloseButtonImage Height="12px" Width="13px" />
            <HeaderStyle>
                <Paddings PaddingRight="6px" />
            </HeaderStyle>
        </dxpc:ASPxPopupControl>
    </ContentTemplate>
</asp:UpdatePanel>