<%@ Page Title="Mi Vida Vale - Resumen" Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionVidaResumenPopup.aspx.vb" Inherits="NNCotizacionVidaResumenPopup" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionVidaResumenPageTitleResource"%>

<%@ Register src="NNCotizacionVidaResumenUserControl.ascx" tagname="NNCotizacionVidaResumenUserControl" tagprefix="NNCotizacionVidaResumenUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="NNCotizacionVidaResumen.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</head>
<body id="NNCotizacionVidaResumenBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="NNCotizacionVidaResumenUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionVidaResumenUC:NNCotizacionVidaResumenUserControl ID="NNCotizacionVidaResumenUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>