<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProducerInformationControl.ascx.vb" Inherits="ProducerInformationControl" %>
<table width="50%" align="left">
    <tr align="center">
        <td width="25%">
            <dxe:ASPxLabel ID="AgentIdLabel" runat="server" Text="Agent No." AssociatedControlID="AgentIdTextBox"
                meta:resourcekey="AgentIdLabelResource">
            </dxe:ASPxLabel>
        </td>
        <td align='left'>
            <dxe:ASPxTextBox ID="AgentIdTextBox" runat="server" Width="100px" Paddings-PaddingLeft="8px"
                meta:resourcekey="AgentIdTextBoxResource" HorizontalAlign="Right">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The agent identification is required' />
                </ValidationSettings>
            </dxe:ASPxTextBox>
        </td>
    </tr>
</table>
