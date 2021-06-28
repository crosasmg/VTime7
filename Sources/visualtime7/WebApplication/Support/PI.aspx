<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PI.aspx.vb" Inherits="Support_PI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Label ID="lblUser" Font-Bold="true" runat="server" Font-Size="15"></asp:Label>
    </div>
    <div style="text-align: center;">
        <div style="width: 50%; margin: 0 auto; text-align: left;">
            <asp:Table Style="width: 100%;" ID="valuesUser" runat="server">
            </asp:Table>
        </div>
        <asp:Label Height="0px" Width="0px" ID="lblKey" runat="server" />
    </div>
    </form>
</body>
</html>