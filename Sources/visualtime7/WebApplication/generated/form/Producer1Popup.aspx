<%@ Page Title="Form1" Language="VB" AutoEventWireup="false" CodeFile="Producer1Popup.aspx.vb" Inherits="Producer1Popup" UICulture="auto" Culture="auto" meta:resourcekey="Producer1PageTitleResource"%>

<%@ Register src="Producer1UserControl.ascx" tagname="Producer1UserControl" tagprefix="Producer1UC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Producer1.js" type="text/javascript"></script>
</head>
<body id="Producer1Body">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="Producer1UpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <Producer1UC:Producer1UserControl ID="Producer1UserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>