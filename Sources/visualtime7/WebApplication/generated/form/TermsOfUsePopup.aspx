<%@ Page Title="Términos de Uso" Language="VB" AutoEventWireup="false" CodeFile="TermsOfUsePopup.aspx.vb" Inherits="TermsOfUsePopup" UICulture="auto" Culture="auto" meta:resourcekey="TermsOfUsePageTitleResource"%>

<%@ Register src="TermsOfUseUserControl.ascx" tagname="TermsOfUseUserControl" tagprefix="TermsOfUseUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="TermsOfUse.js" type="text/javascript"></script>
</head>
<body id="TermsOfUseBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="TermsOfUseUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <TermsOfUseUC:TermsOfUseUserControl ID="TermsOfUseUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>