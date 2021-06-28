<%@ Page Title="Telefonos" Language="VB" AutoEventWireup="false" CodeFile="TelefonosPopup.aspx.vb" Inherits="TelefonosPopup" UICulture="auto" Culture="auto" meta:resourcekey="TelefonosPageTitleResource"%>

<%@ Register src="TelefonosUserControl.ascx" tagname="TelefonosUserControl" tagprefix="TelefonosUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Telefonos.js" type="text/javascript"></script>
</head>
<body id="TelefonosBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="TelefonosUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <TelefonosUC:TelefonosUserControl ID="TelefonosUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>