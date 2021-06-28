<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="ForgotPassword.aspx.vb"
    Inherits="Authentication_ForgotPassword" meta:resourcekey="PageResource" %>

<%@ Register Src="ChangeAddressSecurityControl.ascx" TagName="ChangeAddressSecurityControl"
    TagPrefix="uc1" %>
<%@ Register Src="ProfileSecurityControl.ascx" TagName="ProfileSecurityControl" TagPrefix="uc2" %>
<%@ Register Src="UserNameSecurityControl.ascx" TagName="UserNameSecurityControl"
    TagPrefix="uc3" %>
<%@ Register Src="AddressSecurityControl.ascx" TagName="AddressSecurityControl" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="padding: 15px; margin: 0px; background-image: none;">
    <form id="LoginForm" runat="server">
    <div>
        <asp:MultiView ID="StepMultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="MainView" runat="server">
                <table style="height: 65px; width: 350px">
                    <tr align="center">
                        <td width="100%">
                            <dxe:ASPxLabel ID="MessageLabel" runat="server" meta:resourcekey="MessageLabelResource"
                                Text="You credential reminder will be sent to the e-mail address entered bellow">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="center">
                        <td width="100%">
                            <div align="center">
                                <dxe:ASPxTextBox ID="EmailAddressTextBox" runat="server" Paddings-PaddingLeft="8px"
                                    Width="260px" meta:resourcekey="EmailAddressTextBoxResource">
                                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                                        Repeat="NoRepeat" VerticalPosition="center" />
                                    <Paddings PaddingLeft="8px"></Paddings>
                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired='True' ErrorText='The e-mail address is required' />
                                        <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$"
                                            ErrorText="The format is invalid" />
                                    </ValidationSettings>
                                </dxe:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                    <tr align="center">
                        <td width="100%">
                            <div align="center">
                                <dxe:ASPxLabel ID="InvalidEmailLabel" runat="server" EnableViewState="False" ForeColor="Red"
                                    Text="" Visible="False" />
                            </div>
                        </td>
                    </tr>
                    <tr align="center">
                        <td width="100%">
                            <dxe:ASPxButton ID="SendButton" runat="server" Text="Validate" meta:resourcekey="SendButtonResource">
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" style="position: absolute; bottom: 10px; width: 320px">
                    <tr align="center">
                        <td width="100%">
                            <dxe:ASPxLabel ID="MessageResetLabel" runat="server" meta:resourcekey="MessageResetLabelResource"
                                Text="If your e-mail address is different from the one registered then click Reset.">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ChangeAddressSecurityControlView" runat="server">
                <uc1:ChangeAddressSecurityControl ID="ChangeAddressSecurityControl1" runat="server" />
            </asp:View>
            <asp:View ID="ProfileSecurityControlView" runat="server">
                <uc2:ProfileSecurityControl ID="ProfileSecurityControl1" runat="server" />
            </asp:View>
            <asp:View ID="UserNameSecurityControlView" runat="server">
                <uc3:UserNameSecurityControl ID="UserNameSecurityControl1" runat="server" />
            </asp:View>
            <asp:View ID="AddressSecurityControlView" runat="server">
                <uc4:AddressSecurityControl ID="AddressSecurityControl1" runat="server" />
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
