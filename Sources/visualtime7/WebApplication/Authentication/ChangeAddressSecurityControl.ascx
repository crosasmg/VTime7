<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChangeAddressSecurityControl.ascx.vb"
    Inherits="ChangeAddressSecurityControl" %>
<table style="height: 65px; width: 350px">
    <tr align="center">
        <td width="100%" colspan="2">
            <div align="center">
                <dxe:ASPxLabel ID="MessageLabel" runat="server" Text="In order, to be able to reset your credentials it is nedded that you provide us withn your registered email address."
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
    <tr align="center">
        <td width="100%" colspan="2" align="center">
            <div align="center">
                <dxe:ASPxTextBox ID="EmailAddressTextBox" runat="server" Paddings-PaddingLeft="8px"
                    meta:resourcekey="EmailAddressLabelResource" Width="260px">
                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                        Repeat="NoRepeat" VerticalPosition="center" />
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
        <td width="100%" colspan="2">
            <div align="center">
                <dxe:ASPxLabel ID="InvalidEmailLabel" runat="server" EnableViewState="False" ForeColor="Red"
                    Text="" Visible="False" />
            </div>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr valign="top" style="height: 1%">
        <td align="left" colspan="2">
            <asp:Panel ID="OptionsPanel" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td width="100%" align="center">
                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage" meta:resourcekey="lblMessageResource"
                                Text="We are sorry but the email you provided was not found on our files" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            <dxe:ASPxLabel runat="server" ClientInstanceName="YouCouldLabel" ID="YouCouldLabel"
                                meta:resourcekey="YouCouldLabelResource" Text="-You could" />
                            &nbsp;
                            <dxe:ASPxHyperLink ID="GoBackHyperLink" runat="server" AutoPostBack="False" ClientInstanceName="GoBackHyperLink"
                                Text="go back" NavigateUrl="#" meta:resourcekey="GoBackHyperLinkResource">
                                <ClientSideEvents Click="function(s, e) {
	document.getElementById('ChangeAddressSecurityControl1_OptionsPanel').style.visibility='hidden';
	return false;
}" />
                            </dxe:ASPxHyperLink>
                            &nbsp;<dxe:ASPxLabel runat="server" ClientInstanceName="VerifyMailLabel" ID="VerifyMailLabel"
                                meta:resourcekey="VerifyMailLabelResource" Text="and verify your email address" />
                            <br />
                            <dxe:ASPxLabel runat="server" ClientInstanceName="PleaseMailLabel" ID="PleaseMailLabel"
                                meta:resourcekey="PleaseMailLabelResource" Text="-Please" />
                            &nbsp;
                            <dxe:ASPxHyperLink ID="RegisterHyperLink" runat="server" AutoPostBack="False" ClientInstanceName="RegisterHyperLink"
                                Text="register" meta:resourcekey="RegisterHyperLinkResource" NavigateUrl="javascript:var parentWindow=window.parent;parentWindow.ForgotPasswordPopupControl.Hide();parentWindow.LogInPopupControl.Show();">
                            </dxe:ASPxHyperLink>
                            &nbsp;
                            <dxe:ASPxLabel runat="server" ClientInstanceName="OrderLabel" ID="OrderLabel" meta:resourcekey="OrderLabelResource"
                                Text="in order to obtain full system access" />
                            <br />
                            <dxe:ASPxLabel runat="server" ClientInstanceName="SendMailLabel" ID="SendMailLabel"
                                meta:resourcekey="SendMailLabelResource" Text="-Send us a" />
                            &nbsp;
                            <dxe:ASPxHyperLink ID="SendEmailHyperLink" runat="server" Text="message" AutoPostBack="False"
                                ClientInstanceName="SendEmailHyperLink"   NavigateUrl="javascript:var parentWindow=window.parent;parentWindow.ForgotPasswordPopupControl.Hide();parentWindow.location.href='/Authentication/SendEmailAdministrator.aspx';">
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
    
       <td width="50%" align="right">
            <dxe:ASPxButton ID="BackButton" runat="server" Text="Back" CausesValidation="false"
                meta:resourcekey="BackButtonResource" Width="80px" AutoPostBack="false" >
               <ClientSideEvents Click="function(s, e) {window.parent.ForgotPasswordPopupControl.SetContentUrl('/Authentication/ForgotPassword.aspx?View=0');}"/>
            </dxe:ASPxButton>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td width="50%" align="left">
            <dxe:ASPxButton ID="FindButton" runat="server" Text="Find" Width="80px" meta:resourcekey="FindButtonResource">
            </dxe:ASPxButton>
        </td>
    </tr>
</table>
