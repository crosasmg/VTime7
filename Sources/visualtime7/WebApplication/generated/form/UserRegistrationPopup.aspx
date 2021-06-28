<%@ Page Title="User Registration." Language="VB" AutoEventWireup="false" CodeFile="UserRegistrationPopup.aspx.vb" Inherits="UserRegistrationPopup" UICulture="auto" Culture="auto" meta:resourcekey="UserRegistrationPageTitleResource"%>

<%@ Register src="UserRegistrationUserControl.ascx" tagname="UserRegistrationUserControl" tagprefix="UserRegistrationUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="UserRegistration.js" type="text/javascript"></script>
</head>
<body id="UserRegistrationBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="UserRegistrationUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <UserRegistrationUC:UserRegistrationUserControl ID="UserRegistrationUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>