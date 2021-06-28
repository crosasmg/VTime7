<%@ Page Title="TextoLibre" Language="VB" AutoEventWireup="false" CodeFile="TextoLibrePopup.aspx.vb" Inherits="TextoLibrePopup" UICulture="auto" Culture="auto" meta:resourcekey="TextoLibrePageTitleResource"%>

<%@ Register src="TextoLibreUserControl.ascx" tagname="TextoLibreUserControl" tagprefix="TextoLibreUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="TextoLibre.js" type="text/javascript"></script>
</head>
<body id="TextoLibreBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="TextoLibreUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <TextoLibreUC:TextoLibreUserControl ID="TextoLibreUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>