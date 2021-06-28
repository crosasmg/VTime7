<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserType.ascx.vb" Inherits="UserTypeUserControl" %>
<br />
<br />
<table width="30%" align="center">
    <tr>
        <td>
            <div align="right">
                <dxe:ASPxLabel ID="IamLabel" runat="server" Text="I am a(n)" AssociatedControlID="IamRadioButtonList"
                    meta:resourcekey="IamLabelResource">
                </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left">
            <dxe:ASPxRadioButtonList ID="IamRadioButtonList" runat="server" meta:resourcekey="IamRadioButtonListResource"
                ClientIDMode="AutoID" SelectedIndex="0" RepeatDirection="Horizontal" Border-BorderStyle="None">
                <Items>
                    <dxe:ListEditItem Text="Agent" Value="Agent" meta:resourcekey="IamRadioButtonListItemResourceValue1" />
                    <dxe:ListEditItem Text="Client" Value="Client" meta:resourcekey="IamRadioButtonListItemResourceValue2" />                  
                </Items>
            </dxe:ASPxRadioButtonList>
        </td>
    </tr>
   
</table>

<table width="50%" align="center">
      <tr align="center">
        <td width="100%">
           <dxe:ASPxCaptcha ID="SecurityCodeCaptcha" meta:resourcekey="SecurityCodeCaptchaResource"
                ClientInstanceName="SecurityCodeCaptcha" runat="server" ClientIDMode="AutoID">
                <ValidationSettings ErrorText="The security code is incorrect">
                    <RequiredField IsRequired="True" ErrorText="The security code is required" />
                </ValidationSettings>
                <RefreshButton Text="Show another security code">
                </RefreshButton>
                <TextBox LabelText="Type the security code shown:" />
                <ChallengeImage FontFamily="Courier New">
                </ChallengeImage>
            </dxe:ASPxCaptcha>
        </td>
    </tr>
    <tr align="center">
        <td>
           
        </td>
    </tr>
    <tr align="center">
        <td width="100%">
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="CancelButton" runat="server" Text="Cancel" CausesValidation="false"
                    AutoPostBack="false" meta:resourcekey="CancelButtonResource">
                    <ClientSideEvents Click="function(s, e) {window.location.href = '/dropthings/Default.aspx';} " />
                </dxe:ASPxButton>
                    </td>
                         <td>
                      &nbsp;
                        </td>
                             <td>
                      &nbsp;
                        </td>
                    <td>
                         <dxe:ASPxButton ID="NextButton" runat="server" Text="Next" meta:resourcekey="NextButtonResource">
            </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
     <tr align="center">
        <td>
           
        </td>
    </tr>
    <tr align="center">
        <td width="100%">
            <div align="center">
                <dxe:ASPxLabel ID="MessageASPxLabel" runat="server" Text="Information about you will be request according to your selection"
                    meta:resourcekey="MessageASPxLabelResource">
                </dxe:ASPxLabel>
            </div>
        </td>
    </tr>
</table>

