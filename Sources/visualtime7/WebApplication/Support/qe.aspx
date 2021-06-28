<%@ Page Language="VB" AutoEventWireup="false" CodeFile="qe.aspx.vb" Inherits="Support_qe" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <title>qe</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <asp:TextBox ID="txtQuery" Width="90%" Height="40px" runat="server" TextMode="MultiLine"
            Visible="false"></asp:TextBox>
    </div>
    <div style="margin: auto; width: 90%">
        <span style="float: left;">
            <asp:RadioButton ID="cbxBackOffice" GroupName="Source" Checked="true" Text="BackOffice"
                runat="server" Visible="false" />
            <asp:RadioButton ID="cbxFrontOffice" GroupName="Source" Text="FrontOffice" runat="server"
                Visible="false" />
        </span><span style="float: right;">
            <asp:Button ID="btnExecute" runat="server" Text="Execute" Visible="false" />
        </span>
    </div>
    <div style="text-align: center; margin: auto; width: 90%; padding-top: 26px;">
        <asp:GridView ID="grvResult" Width="100%" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
