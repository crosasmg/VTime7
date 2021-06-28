<%@ Page Title="DECLARACIÓN PERSONAL DE SALUD DEL PROPUESTO ASEGURADO" Language="VB" AutoEventWireup="false" CodeFile="SMFDIDPSPopup.aspx.vb" Inherits="SMFDIDPSPopup" UICulture="auto" Culture="auto" meta:resourcekey="SMFDIDPSPageTitleResource"%>

<%@ Register src="SMFDIDPSUserControl.ascx" tagname="SMFDIDPSUserControl" tagprefix="SMFDIDPSUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="SMFDIDPS.js" type="text/javascript"></script>
</head>
<body id="SMFDIDPSBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <SMFDIDPSUC:SMFDIDPSUserControl ID="SMFDIDPSUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>