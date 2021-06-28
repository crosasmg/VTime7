<%@ Page Title="Bienvenido" Language="VB" AutoEventWireup="false" CodeFile="BienvenidaPortalPopup.aspx.vb" Inherits="BienvenidaPortalPopup" UICulture="auto" Culture="auto" meta:resourcekey="BienvenidaPortalPageTitleResource"%>

<%@ Register src="BienvenidaPortalUserControl.ascx" tagname="BienvenidaPortalUserControl" tagprefix="BienvenidaPortalUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="BienvenidaPortal.js" type="text/javascript"></script>
</head>
<body id="BienvenidaPortalBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="BienvenidaPortalUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <BienvenidaPortalUC:BienvenidaPortalUserControl ID="BienvenidaPortalUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>