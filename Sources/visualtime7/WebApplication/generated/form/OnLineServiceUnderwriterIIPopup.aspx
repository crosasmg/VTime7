<%@ Page Title="Auto servicio" Language="VB" AutoEventWireup="false" CodeFile="OnLineServiceUnderwriterIIPopup.aspx.vb" Inherits="OnLineServiceUnderwriterIIPopup" UICulture="auto" Culture="auto" meta:resourcekey="OnLineServiceUnderwriterIIPageTitleResource"%>

<%@ Register src="OnLineServiceUnderwriterIIUserControl.ascx" tagname="OnLineServiceUnderwriterIIUserControl" tagprefix="OnLineServiceUnderwriterIIUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="OnLineServiceUnderwriterII.js" type="text/javascript"></script>
</head>
<body id="OnLineServiceUnderwriterIIBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="OnLineServiceUnderwriterIIUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <OnLineServiceUnderwriterIIUC:OnLineServiceUnderwriterIIUserControl ID="OnLineServiceUnderwriterIIUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>