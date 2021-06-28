<%@ Page Title="Confirmación" Language="VB" AutoEventWireup="false" CodeFile="ConfirmaciónPopup.aspx.vb" Inherits="ConfirmaciónPopup" UICulture="auto" Culture="auto" meta:resourcekey="ConfirmaciónPageTitleResource"%>

<%@ Register src="ConfirmaciónUserControl.ascx" tagname="ConfirmaciónUserControl" tagprefix="ConfirmaciónUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Confirmación.js" type="text/javascript"></script>
</head>
<body id="ConfirmaciónBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="ConfirmaciónUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <ConfirmaciónUC:ConfirmaciónUserControl ID="ConfirmaciónUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>