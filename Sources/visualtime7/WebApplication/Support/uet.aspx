<%@ Page Language="VB" AutoEventWireup="false" CodeFile="uet.aspx.vb" Inherits="Support_uet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Query Support</title>
    <link href="/styles/bootstrap.min.css" rel="stylesheet" />
    <link href="/styles/font-awesome.min.css" rel="stylesheet" />
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/scripts/bootstrap.min.js" type="text/javascript"></script>
    <link rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>
    <script src="../Scripts/uet.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="keyValid" ClientIDMode="Static" Value="0" runat="server" />
    <div style="text-align: center">
        <asp:Label ID="lblTitle" Font-Bold="true" Text="Query Support" runat="server" Font-Size="15"></asp:Label>
    </div>
    <div style="text-align: center;">
        <asp:Label ID="lblQuery" runat="server" Font-Bold="true" Text="Result query"></asp:Label>
    </div>
    <div style="text-align: center;">
        <asp:TextBox ID="txtQuery" Width="90%" Height="200px" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div style="text-align: center;">
        <asp:RadioButton ID="cbxBackOffice" GroupName="Source" Checked="true" Text="BackOffice"
            runat="server" />
        <asp:RadioButton ID="cbxFrontOffice" GroupName="Source" Text="FrontOffice" runat="server" />
    </div>
    <div style="text-align: center;">
        <input type="button" id="btnExecute" value="Execute" />
        <%--  <button id="btnExecute">
            Execute</button>--%>
    </div>
    <div style="text-align: center;">
        <asp:Label ID="lblResult" runat="server" Font-Bold="true" Text="Result query"></asp:Label>
    </div>
    <div id="dvTable" style="text-align: center;" />
    </form>
</body>
</html>