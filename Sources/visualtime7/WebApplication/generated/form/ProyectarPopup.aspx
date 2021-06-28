<%@ Page Title="Proyectar" Language="VB" AutoEventWireup="false" CodeFile="ProyectarPopup.aspx.vb" Inherits="ProyectarPopup" UICulture="auto" Culture="auto" meta:resourcekey="ProyectarPageTitleResource"%>

<%@ Register src="ProyectarUserControl.ascx" tagname="ProyectarUserControl" tagprefix="ProyectarUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Proyectar.js" type="text/javascript"></script>
</head>
<body id="ProyectarBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <ProyectarUC:ProyectarUserControl ID="ProyectarUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>