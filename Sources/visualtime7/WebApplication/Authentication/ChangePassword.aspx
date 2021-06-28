<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb"
    Inherits="Authentication_ChangePassword" meta:resourcekey="PageResource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="padding: 15px; margin: 0px; background-image: none;">
    <form id="LoginForm" runat="server">
        <div>
            <table style="height: 65px; width: 360px">
                <tr align="center" valign='top' style="height: 1%">
                    <td width="125px">
                        <div align="left">
                            <dxe:ASPxLabel ID="OldPasswordLabel" runat="server" Text="Old Password" meta:resourcekey="OldPasswordLabelResource"
                                AssociatedControlID="OldPasswordTextBox">
                            </dxe:ASPxLabel>
                        </div>
                    </td>
                    <td align="left" width="200px">
                        <dxe:ASPxTextBox ID="OldPasswordTextBox" runat="server" Password="True" Paddings-PaddingLeft="8px"
                            meta:resourcekey="OldPasswordTextBoxResource" Width="200px">
                            <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                                Repeat="NoRepeat" VerticalPosition="center" />
                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired='True' ErrorText='The old password is required' />
                            </ValidationSettings>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr align="center" valign='top' style="height: 1%">
                    <td width="125px">
                        <div align="left">
                            <dxe:ASPxLabel ID="NewPasswordLabel" runat="server" Text="Type New Password" meta:resourcekey="NewPasswordLabelResource"
                                AssociatedControlID="NewPasswordTextBox">
                            </dxe:ASPxLabel>
                        </div>
                    </td>
                    <td align="left" width="200px">
                        <dxe:ASPxTextBox ID="NewPasswordTextBox" runat="server" Password="True" Paddings-PaddingLeft="8px"
                            Width="200px" meta:resourcekey="NewPasswordTextBoxResource" MaxLength="20">
                            <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                                Repeat="NoRepeat" VerticalPosition="center" />
                            <ValidationSettings CausesValidation="True" ErrorDisplayMode="Text" Display="Dynamic"
                                ErrorTextPosition="Bottom">
                                <RequiredField IsRequired='True' ErrorText='The new password is required' />
                                <RegularExpression ValidationExpression='.{6,20}' ErrorText='The new password must be between 6 and 20 characters' />
                            </ValidationSettings>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr align="center" valign='top' style="height: 1%">
                    <td width="125px">
                        <div align="left">
                            <dxe:ASPxLabel ID="RetypeNewPasswordLabel" runat="server" Text="Retype New Password"
                                AssociatedControlID="RetypeNewPasswordTextBox" meta:resourcekey="RetypeNewPasswordLabelResource">
                            </dxe:ASPxLabel>
                        </div>
                    </td>
                    <td align="left" width="200px">
                        <dxe:ASPxTextBox ID="RetypeNewPasswordTextBox" runat="server" Password="True" Paddings-PaddingLeft="8px"
                            Width="200px" meta:resourcekey="RetypeNewPasswordTextBoxResource">
                            <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                                Repeat="NoRepeat" VerticalPosition="center" />
                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired='True' ErrorText='The new password is required' />
                            </ValidationSettings>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr align="center">
                    <td width="100%" colspan="2">
                        <div align="center">
                            <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToValidate="NewPasswordTextBox"
                                ControlToCompare="RetypeNewPasswordTextBox" ErrorMessage="The password should identical to the previous."
                                meta:resourcekey="PasswordCompareValidatorResource" />
                            <br />
                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage" Text="" Font-Bold="True"
                                meta:resourcekey="lblMessageResource" />
                        </div>
                    </td>
                </tr>
                <tr align="center">
                    <td width="100%" colspan="2">
                        <dxe:ASPxLabel runat="server" ClientInstanceName="HelpLabel" ID="HelpLabel" Text="Help"
                            meta:resourcekey="HelpLabelResource" />
                    </td>
                </tr>
            </table>
            <table width="100%" style="position: absolute; bottom: 10px; width: 320px">
                <tr align="center">
                    <td width="50%" align="right">
                        <dxe:ASPxButton ID="SaveButton" runat="server" Text="Save" meta:resourcekey="SaveButtonResource">
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td width="50%" align="left">
                        <dxe:ASPxButton ID="CancelButton" runat="server" Text="Cancel" CausesValidation="false"
                            meta:resourcekey="CancelButtonResource">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>