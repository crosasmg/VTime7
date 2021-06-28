<%@ Page Title="Selección Seguros de Vida" Language="VB" AutoEventWireup="false" CodeFile="SegurosDeVidaPopup.aspx.vb" Inherits="SegurosDeVidaPopup" UICulture="auto" Culture="auto" meta:resourcekey="SegurosDeVidaPageTitleResource"%>

<%@ Register src="SegurosDeVidaUserControl.ascx" tagname="SegurosDeVidaUserControl" tagprefix="SegurosDeVidaUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="SegurosDeVida.js" type="text/javascript"></script>
</head>
<body id="SegurosDeVidaBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <SegurosDeVidaUC:SegurosDeVidaUserControl ID="SegurosDeVidaUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>