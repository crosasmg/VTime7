<%@ Page Title="ClientControl" Language="VB" AutoEventWireup="false" CodeFile="ClientControlPopup.aspx.vb" Inherits="ClientControlPopup" UICulture="auto" Culture="auto" meta:resourcekey="ClientControlPageTitleResource"%>

<%@ Register src="ClientControlUserControl.ascx" tagname="ClientControlUserControl" tagprefix="ClientControlUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="ClientControl.js" type="text/javascript"></script>
    <script src="/customscripts/clientcontrol_devexpress.js" type="text/javascript"></script>
</head>
<body id="ClientControlBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="ClientControlUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <ClientControlUC:ClientControlUserControl ID="ClientControlUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>