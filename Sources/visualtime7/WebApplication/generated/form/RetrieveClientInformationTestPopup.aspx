<%@ Page Title="Retrieve Client Information" Language="VB" AutoEventWireup="false" CodeFile="RetrieveClientInformationTestPopup.aspx.vb" Inherits="RetrieveClientInformationTestPopup" UICulture="auto" Culture="auto" meta:resourcekey="RetrieveClientInformationTestPageTitleResource"%>

<%@ Register src="RetrieveClientInformationTestUserControl.ascx" tagname="RetrieveClientInformationTestUserControl" tagprefix="RetrieveClientInformationTestUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="RetrieveClientInformationTest.js" type="text/javascript"></script>
</head>
<body id="RetrieveClientInformationTestBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="RetrieveClientInformationTestUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <RetrieveClientInformationTestUC:RetrieveClientInformationTestUserControl ID="RetrieveClientInformationTestUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>