<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProfileSecurityControl.ascx.vb"
    Inherits="ProfileSecurityControl" %>
<table style="height: 65px; width: 350px">
    <tr align="center">
        <td width="100%" colspan="2">
            <div align="center">
                <dxe:ASPxLabel ID="MessageLabel" runat="server" Text="Please, answer your security question."
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
        <td width="140px">
            <div align="left">
                <dxe:ASPxLabel ID="EmailAddressLabel" runat="server" Text="Email Address" AssociatedControlID="EmailAddressTextBox"
                    meta:resourcekey="EmailAddressLabelResource">
                </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left" width="200px">
            <dxe:ASPxTextBox ID="EmailAddressTextBox" runat="server" Enabled="False" Width="220px">
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr align="center" valign='top' style="height: 1%">
        <td width="140px">
            <div align="left">
                <dxe:ASPxLabel ID="SecurityQuestionLabel" runat="server" Text="Security Question"
                    AssociatedControlID="SecurityQuestionMemo" meta:resourcekey="SecurityQuestionLabelResource">
                </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left" width="200px">
            <dxe:ASPxMemo ID="SecurityQuestionMemo" runat="server" Enabled="False" Width="220px"
                meta:resourcekey="SecurityQuestionMemoResource">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The security question is required' />
                </ValidationSettings>
            </dxe:ASPxMemo>
        </td>
    </tr>
    <tr align="center" valign='top' style="height: 1%">
        <td width="140px">
            <div align="left">
                <dxe:ASPxLabel ID="SecurityAnswerLabel" runat="server" Text="Security Answer" AssociatedControlID="SecurityAnswerTextBox"
                    meta:resourcekey="SecurityAnswerLabelResource">
                </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left" width="200px">
            <dxe:ASPxTextBox ID="SecurityAnswerTextBox" runat="server" Paddings-PaddingLeft="8px"
                meta:resourcekey="SecurityAnswerTextBoxResource" Width="220px">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                    <RequiredField IsRequired='True' ErrorText='The security answer is required' />
                </ValidationSettings>
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr valign="top" style="height: 1%">
        <td align="left" colspan="2">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage" Visible="false"
                Text="In a few minutes you will receive your password by email." />
            <asp:Panel ID="BlokedAccountPanel" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td width="100%" align="center">
                            <dxe:ASPxLabel runat="server" ClientInstanceName="AccountLabel" ID="AccountLabel"
                                meta:resourcekey="AccountLabelResource" Text="The account is locked. You can attempting to enter" />
                            &nbsp;<dxe:ASPxHyperLink ID="AccountHyperLink" runat="server" AutoPostBack="False"
                                ClientInstanceName="AccountHyperLink" Text="another account" meta:resourcekey="AccountHyperLinkResource"
                                 NavigateUrl="javascript:var parentWindow=window.parent;parentWindow.ForgotPasswordPopupControl.Hide();parentWindow.LogInPopupControl.Show();">
                            </dxe:ASPxHyperLink>
                            &nbsp;<dxe:ASPxLabel runat="server" ClientInstanceName="SendLabel" ID="SendLabel"
                                meta:resourcekey="SendLabelResource" Text=",send us a" />
                            &nbsp;<dxe:ASPxHyperLink ID="SendEmailHyperLink" runat="server" Text="message" AutoPostBack="False"
                                ClientInstanceName="SendEmailHyperLink" meta:resourcekey="SendEmailHyperLinkResource"
                                NavigateUrl="javascript:var parentWindow=window.parent;parentWindow.ForgotPasswordPopupControl.Hide();parentWindow.location.href='/Authentication/SendEmailAdministrator.aspx';">
                            </dxe:ASPxHyperLink>
                            &nbsp;<dxe:ASPxLabel runat="server" ClientInstanceName="RecoverLabel" ID="RecoverLabel"
                                meta:resourcekey="RecoverLabelResource" Text="to try to recover your account or you could" />
                            &nbsp;<dxe:ASPxHyperLink ID="GoBackHyperLink1" runat="server" AutoPostBack="False"
                                NavigateUrl="#" ClientInstanceName="GoBackHyperLink1" Text="go back" meta:resourcekey="GoBackHyperLink1Resource"
                                ClientIDMode="AutoID">
                                <ClientSideEvents Click="function(s, e) {
	document.getElementById('ProfileSecurityControl1_BlokedAccountPanell').style.visibility='hidden';
	return false;
}" />
                            </dxe:ASPxHyperLink>
                            &nbsp;<dxe:ASPxLabel runat="server" ClientInstanceName="VerifyLabel" ID="VerifyLabel"
                                meta:resourcekey="VerifyLabelResource" Text="and verify your answer" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="SecurityAnswerPanel" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td width="100%" align="center">
                            <dxe:ASPxLabel runat="server" ClientInstanceName="ProvidedMessage" ID="ProvidedMessage"
                                meta:resourcekey="ProvidedMessageResource" Text="We are sorry but the answer you provided does not match the one on file." />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            <dxe:ASPxLabel runat="server" ClientInstanceName="YouCouldLabel" ID="YouCouldLabel"
                                meta:resourcekey="YouCouldLabelResource" Text="You could" />
                            
                            &nbsp;<dxe:ASPxHyperLink ID="GoBackHyperLink" runat="server" 
                                AutoPostBack="False" ClientInstanceName="GoBackHyperLink" 
                                meta:resourcekey="GoBackHyperLinkResource" NavigateUrl="#" 
                                Text="go back">
                                      <ClientSideEvents Click="function(s, e) {
	document.getElementById('ProfileSecurityControl1_SecurityAnswerPanel').style.visibility='hidden';
	return false;
}" />
                            </dxe:ASPxHyperLink>
                            &nbsp;<dxe:ASPxLabel runat="server" ClientInstanceName="VerifyMailLabel" ID="VerifyMailLabel"
                                meta:resourcekey="VerifyMailLabelResource" Text="and verify your answer" />

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
            <dxe:ASPxButton ID="VerifyButton" runat="server" Text="Verify" meta:resourcekey="VerifyButtonResource">
            </dxe:ASPxButton>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td width="50%" align="left">
            <dxe:ASPxButton ID="CancelButton" runat="server" Text="Cancel" CausesValidation="false"
                meta:resourcekey="CancelButtonResource" AutoPostBack="false">
                <ClientSideEvents Click="function(s, e) {window.parent.ForgotPasswordPopupControl.SetContentUrl('/Authentication/ForgotPassword.aspx?View=0');}"/>
            </dxe:ASPxButton>
        </td>
    </tr>
</table>



