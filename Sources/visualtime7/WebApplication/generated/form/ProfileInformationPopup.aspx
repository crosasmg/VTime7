<%@ Page Title="Profile Information" Language="VB" AutoEventWireup="false" CodeFile="ProfileInformationPopup.aspx.vb" Inherits="ProfileInformationPopup" UICulture="auto" Culture="auto" meta:resourcekey="ProfileInformationPageTitleResource"%>

<%@ Register src="ProfileInformationUserControl.ascx" tagname="ProfileInformationUserControl" tagprefix="ProfileInformationUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="ProfileInformation.js" type="text/javascript"></script>
</head>
<body id="ProfileInformationBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <ProfileInformationUC:ProfileInformationUserControl ID="ProfileInformationUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>