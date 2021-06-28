<%@ Page Language="VB" AutoEventWireup="false" CodeFile="email.aspx.vb" Inherits="Support_email" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email Test</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <div style="text-align: left;  width: 400px;  display: block;  margin-left: auto;  margin-right: auto;">
        <table width="400px">
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel4" Text="Type:" runat="server" />
                </td>
                <td>
                    <dx:ASPxComboBox ID="chkTypeEmail" SelectedIndex="0" runat="server" >
                        <Items>
                            <dxe:ListEditItem Text="SMTP - NetMail" Value="NetMail" />
                            <dxe:ListEditItem Text="ExchangeService" Value="ExchangeService" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel3" Text="Host:" runat="server" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtHost" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" Text="User Name:" runat="server" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtUserName" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel2" Text="Password:" runat="server" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtPassword" runat="server" Password="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel5" Text="Port:" runat="server" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtPort" runat="server" />
                </td>
            </tr>
             <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel6" Text="To:" runat="server" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtTo" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <dx:ASPxCheckBox  ID="rbEnableSSL" Text="Enable SSL:" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <dx:ASPxButton ID="txtSend" Text="Try." runat="server" />
                </td>
            </tr>
        </table>
        <table width="400px">
            <tr>
                <td align="center" colspan="2">
                     <dx:ASPxLabel ID="lblMessage" Visible="false" runat="server" />
                </td>
            </tr>
             <tr>
                <td align="center" colspan="2">
                     <dx:ASPxMemo ID="txtExample"  width="400px" Height="100px" Visible="false" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </div>
    </form>
</body>
</html>