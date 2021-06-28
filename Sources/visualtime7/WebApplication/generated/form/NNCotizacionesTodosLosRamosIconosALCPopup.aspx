<%@ Page Title="Cotizaciones" Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionesTodosLosRamosIconosALCPopup.aspx.vb" Inherits="NNCotizacionesTodosLosRamosIconosALCPopup" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionesTodosLosRamosIconosALCPageTitleResource"%>

<%@ Register src="NNCotizacionesTodosLosRamosIconosALCUserControl.ascx" tagname="NNCotizacionesTodosLosRamosIconosALCUserControl" tagprefix="NNCotizacionesTodosLosRamosIconosALCUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="NNCotizacionesTodosLosRamosIconosALC.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</head>
<body id="NNCotizacionesTodosLosRamosIconosALCBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="NNCotizacionesTodosLosRamosIconosALCUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionesTodosLosRamosIconosALCUC:NNCotizacionesTodosLosRamosIconosALCUserControl ID="NNCotizacionesTodosLosRamosIconosALCUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>