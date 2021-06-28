<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserNameSecurityControl.ascx.vb"
    Inherits="UserNameSecurityControl" %>
<table style="height: 65px; width: 350px">
    <tr align="center">
        <td width="100%" colspan="2">
            <div align="center">
                <dxe:ASPxLabel ID="MessageLabel" runat="server" Text="Please, enter your new domain user name"
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
        <td width="100px">
            <div align="left">
                <dxe:ASPxLabel ID="UserNameLabel" runat="server" Text="User Name" meta:resourcekey="UserNameLabelResource"
                    AssociatedControlID="UserNameTextBox">
                </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left" width="200px">
            <dxe:ASPxTextBox ID="UserNameTextBox" runat="server" Paddings-PaddingLeft="8px" Width="180px"
                meta:resourcekey="UserNameTextBoxResource">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                    <RequiredField IsRequired='True' ErrorText='The user name is required' />
                </ValidationSettings>
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr align="center" valign='top' style="height: 1%">
        <td width="100px">
            <div align="left">
                <dxe:ASPxLabel ID="VerifyUserNameLabel" runat="server" Text="Verify User Name" meta:resourcekey="VerifyUserNameLabelResource"
                    AssociatedControlID="VerifyUserNameTextBox">
                </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left" width="200px">
            <dxe:ASPxTextBox ID="VerifyUserNameTextBox" runat="server" Paddings-PaddingLeft="8px"
                Width="180px" meta:resourcekey="VerifyUserNameTextBoxResource">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                    <RequiredField IsRequired='True' ErrorText='The user name is required' />
                </ValidationSettings>
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr align="center">
        <td width="100%" colspan="2">
            <div align="center">
                <asp:CompareValidator ID="UserNameCompareValidator" runat="server" ControlToValidate="UserNameTextBox"
                    ControlToCompare="VerifyUserNameTextBox" ErrorMessage="The user name should identical to the previous."
                    ForeColor="Red" meta:resourcekey="UserNameCompareValidatorResource" />
            </div>
        </td>
    </tr>
      <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr valign="top" style="height: 1%">
        <td align="center" colspan="2">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessageUpdate" ID="lblMessageUpdate"
                Visible="false" Text="" />
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage" Visible="false"
                meta:resourcekey="lblMessageResource" Text="The user name entered already exists. Try again." />
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
