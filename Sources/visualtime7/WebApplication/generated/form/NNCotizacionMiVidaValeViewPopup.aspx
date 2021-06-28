<%@ Page Title="Mi Vida Vale - Consulta del riesgo" Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeViewPopup.aspx.vb" Inherits="NNCotizacionMiVidaValeViewPopup" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaValeViewPageTitleResource"%>

<%@ Register src="NNCotizacionMiVidaValeViewUserControl.ascx" tagname="NNCotizacionMiVidaValeViewUserControl" tagprefix="NNCotizacionMiVidaValeViewUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="NNCotizacionMiVidaValeView.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</head>
<body id="NNCotizacionMiVidaValeViewBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="NNCotizacionMiVidaValeViewUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaValeViewUC:NNCotizacionMiVidaValeViewUserControl ID="NNCotizacionMiVidaValeViewUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>