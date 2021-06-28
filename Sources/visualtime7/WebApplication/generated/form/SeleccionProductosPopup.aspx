<%@ Page Title="Seleccionar Producto" Language="VB" AutoEventWireup="false" CodeFile="SeleccionProductosPopup.aspx.vb" Inherits="SeleccionProductosPopup" UICulture="auto" Culture="auto" meta:resourcekey="SeleccionProductosPageTitleResource"%>

<%@ Register src="SeleccionProductosUserControl.ascx" tagname="SeleccionProductosUserControl" tagprefix="SeleccionProductosUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="SeleccionProductos.js" type="text/javascript"></script>
</head>
<body id="SeleccionProductosBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <SeleccionProductosUC:SeleccionProductosUserControl ID="SeleccionProductosUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>