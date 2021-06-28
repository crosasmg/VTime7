<%@ Page Title="Form1" Language="VB" AutoEventWireup="false" CodeFile="DPSiNVESMENTFUNDSPopup.aspx.vb" Inherits="DPSiNVESMENTFUNDSPopup" UICulture="auto" Culture="auto" meta:resourcekey="DPSiNVESMENTFUNDSPageTitleResource"%>

<%@ Register src="DPSiNVESMENTFUNDSUserControl.ascx" tagname="DPSiNVESMENTFUNDSUserControl" tagprefix="DPSiNVESMENTFUNDSUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="DPSiNVESMENTFUNDS.js" type="text/javascript"></script>
</head>
<body id="DPSiNVESMENTFUNDSBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <DPSiNVESMENTFUNDSUC:DPSiNVESMENTFUNDSUserControl ID="DPSiNVESMENTFUNDSUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>