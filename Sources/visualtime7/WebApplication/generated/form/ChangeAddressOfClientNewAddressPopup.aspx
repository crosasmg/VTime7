<%@ Page Title="Cambio de la dirección de un cliente (New Address)" Language="VB" AutoEventWireup="false" CodeFile="ChangeAddressOfClientNewAddressPopup.aspx.vb" Inherits="ChangeAddressOfClientNewAddressPopup" UICulture="auto" Culture="auto" meta:resourcekey="ChangeAddressOfClientNewAddressPageTitleResource"%>

<%@ Register src="ChangeAddressOfClientNewAddressUserControl.ascx" tagname="ChangeAddressOfClientNewAddressUserControl" tagprefix="ChangeAddressOfClientNewAddressUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="ChangeAddressOfClientNewAddress.js" type="text/javascript"></script>
</head>
<body id="ChangeAddressOfClientNewAddressBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <ChangeAddressOfClientNewAddressUC:ChangeAddressOfClientNewAddressUserControl ID="ChangeAddressOfClientNewAddressUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>