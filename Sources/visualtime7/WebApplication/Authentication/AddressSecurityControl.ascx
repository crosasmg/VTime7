<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AddressSecurityControl.ascx.vb"
    Inherits="AddressSecurityControl" %>
<table style="height: 65px; width: 350px">
    <tr align="center">
        <td width="100%" colspan="2">
            <div align="center">
                <dxe:ASPxLabel ID="MessageLabel" runat="server" Text="Please, enter your new email address"
                    meta:resourcekey="MessageLabelResource">
                </dxe:ASPxLabel>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr align="center" valign='top' style="height: 1%">
        <td width="190px">
            <div align="left">
                <dxe:ASPxLabel ID="EmailAddressLabel" runat="server" Text="Email Address" AssociatedControlID="EmailAddressTextBox"
                    meta:resourcekey="EmailAddressLabelResource">
                </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left" width="200px">
            <dxe:ASPxTextBox ID="EmailAddressTextBox" runat="server" Paddings-PaddingLeft="8px"
                meta:resourcekey="EmailAddressTextBoxResource" Width="190px">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The e-mail address is required' />
                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$"
                        ErrorText="The format is invalid" />
                </ValidationSettings>
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr align="center" valign='top' style="height: 1%">
        <td width="190px">
            <div align="left">
                <dxe:ASPxLabel ID="VerifyEmailLabel" runat="server" Text="Verify Email Address" meta:resourcekey="VerifyEmailLabelResource"
                    AssociatedControlID="VerifyEmailTextBox">
                </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left" width="200px">
            <dxe:ASPxTextBox ID="VerifyEmailTextBox" runat="server" Paddings-PaddingLeft="8px"
                meta:resourcekey="VerifyEmailTextBoxResource" Width="190px">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The e-mail address is required' />
                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$"
                        ErrorText="The format is invalid" />
                </ValidationSettings>
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr align="center">
        <td width="100%" colspan="2">
            <div align="center">
                <asp:CompareValidator ID="EmailCompareValidator" runat="server" ControlToValidate="EmailAddressTextBox"
                    ControlToCompare="VerifyEmailTextBox" ErrorMessage="The e-mail address should identical to the previous."
                    ForeColor="Red" meta:resourcekey="EmailCompareValidatorResource" />
            </div>
        </td>
    </tr>  
    <tr valign="top" style="height: 1%">
        <td align="left" colspan="2">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblSendMessage" ID="lblSendMessage"
                Visible="false" Text="In few minutes you will send an email with your new password." />
            <asp:Panel ID="OptionsPanel" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td width="100%" align="center">
                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage" meta:resourcekey="lblMessageResource"
                                Text="We are sorry but the email address you provided is already registered." />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            <dxe:ASPxLabel runat="server" ClientInstanceName="YouCouldLabel" ID="YouCouldLabel"
                                meta:resourcekey="YouCouldLabelResource" Text="-You could" />
                            &nbsp;<dxe:ASPxHyperLink ID="GoBackHyperLink" runat="server" AutoPostBack="False" NavigateUrl="#"
                                ClientInstanceName="GoBackHyperLink" Text="go back" meta:resourcekey="GoBackHyperLinkResource">                               
                                <ClientSideEvents Click="function(s, e) {
	document.getElementById('AddressSecurityControl1_OptionsPanel').style.visibility='hidden';
	return false;
}" />
                            </dxe:ASPxHyperLink>
                            &nbsp;<dxe:ASPxLabel runat="server" ClientInstanceName="VerifyMailLabel" ID="VerifyMailLabel"
                                meta:resourcekey="VerifyMailLabelResource" Text="and verify your email address" />
                            <br />
                            <dxe:ASPxLabel runat="server" ClientInstanceName="PleaseMailLabel" ID="PleaseMailLabel"
                                meta:resourcekey="PleaseMailLabelResource" Text="-Please request an email" />
                            &nbsp;<dxe:ASPxHyperLink ID="CredentialsReminderHyperLink" runat="server" AutoPostBack="False"
                                ClientInstanceName="CredentialsReminderHyperLink" meta:resourcekey="CredentialsReminderHyperLinkResource"
                                Text="credentials reminder"
                                 NavigateUrl="javascript:var parentWindow=window.parent;parentWindow.ForgotPasswordPopupControl.Hide();parentWindow.LogInPopupControl.Show();"
                              >
                            </dxe:ASPxHyperLink>
                            <br />
                            <dxe:ASPxLabel runat="server" ClientInstanceName="SendMailLabel" ID="SendMailLabel"
                                meta:resourcekey="SendMailLabelResource" Text="-Send us a" />
                            &nbsp;
                            <dxe:ASPxHyperLink ID="SendEmailHyperLink" runat="server" Text="message" AutoPostBack="False"
                                meta:resourcekey="SendEmailHyperLinkResource" ClientInstanceName="SendEmailHyperLink"
                                NavigateUrl="javascript:var parentWindow=window.parent;parentWindow.ForgotPasswordPopupControl.Hide();parentWindow.location.href='/Authentication/SendEmailAdministrator.aspx';">
                            </dxe:ASPxHyperLink>
                            &nbsp;<dxe:ASPxLabel runat="server" ClientInstanceName="SendErrorLabel" ID="SendErrorLabel"
                                meta:resourcekey="SendErrorLabelResource" Text="if you think this is an error" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table width="100%" style="position: absolute; bottom: 10px; width: 320px">
    <tr align="center">
        <td width="100%" align="center">
            <dxe:ASPxButton ID="UpdateButton" runat="server" Text="Update" meta:resourcekey="UpdateButtonResource">
            </dxe:ASPxButton>
               <dxe:ASPxButton ID="CloseButton" runat="server" Text="Close" Visible="false" meta:resourcekey="CloseButtonResource">
            </dxe:ASPxButton>
        </td>
    </tr>
</table>
