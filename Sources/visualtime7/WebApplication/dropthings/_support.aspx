<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_support.aspx.vb" Inherits="_support" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function AdminUserButton_Click(s, e) {
            AdminUserCallbackPanel.PerformCallback();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <table id='tableBegin' runat='server' style='width: 100%;'>
        <tr valign='top'>
            <td>
            </td>
        </tr>
        <tr valign='top'>
            <td style='width: 100%'>
                <dxrp:ASPxRoundPanel ID="zonePrincipal" runat="server" Width="100%" Visible="true"
                    HeaderText="Soporte / Support">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <table style='width: 100%;'>
                                <tr align="left">
                                    <td colspan='2'>
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    <dxe:ASPxLabel ID='lblUser' runat="server" SkinID="ASPxLabelInformation" Text="User:" />
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID='txtUser' runat="server" SkinID="ASPxLabelInformation" Text="Admin" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <dxe:ASPxLabel ID='lblEmail' runat="server" SkinID="ASPxLabelInformation" Text="Email:" />
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID='txtEmail' runat="server" SkinID="ASPxLabelInformation" Text="admin@visualtime.com" />
                                                </td>
                                            </tr>
                                            <tr>
                                               <td colspan='2' align="center">
                                                    <dxe:ASPxRadioButton ID='chkIsEmployee' runat="server" Text="Is Employee:" SkinID="ASPxLabelInformation" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr valign='top'>
                                    <td colspan='2'>
                                        <dxe:ASPxLabel ID='GenerateInstructionLabel' runat="server" SkinID="ASPxLabelInformation"
                                            Text="Presione el botón para crear el usuario admin.
                                                Press the button to create the admin user." />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr valign='top'>
                                    <td style="width: 0%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 0%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign='top'>
                                    <td style='width: 30%' align='Left'>
                                        <dxe:ASPxButton ID='AdminUserButton' runat='server' ToolTip='' Text='Add User.' Visible='true'
                                            Enabled='True' AutoPostBack='false'>
                                            <ClientSideEvents Click="AdminUserButton_Click" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="width: 70%" align='Left'>
                                        <dxcp:ASPxCallbackPanel ID="AdminUserCallbackPanel" ClientInstanceName="AdminUserCallbackPanel"
                                            runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent5" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxImage ID="AdminUserImageOK" runat="server" Visible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                    Height="20" Width="20">
                                                                </dxe:ASPxImage>
                                                                <dxe:ASPxImage ID="AdminUserImageFail" runat="server" Visible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                    Height="20" Width="20">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="AdminUserLabel" runat="server" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxCallbackPanel>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>