<%@ Page Title="Mi Vida vale" Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeIIALCPopup.aspx.vb" Inherits="NNCotizacionMiVidaValeIIALCPopup" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaValeIIALCPageTitleResource"%>

<%@ Register src="NNCotizacionMiVidaValeIIALCUserControl.ascx" tagname="NNCotizacionMiVidaValeIIALCUserControl" tagprefix="NNCotizacionMiVidaValeIIALCUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="NNCotizacionMiVidaValeIIALC.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</head>
<body id="NNCotizacionMiVidaValeIIALCBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="NNCotizacionMiVidaValeIIALCUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaValeIIALCUC:NNCotizacionMiVidaValeIIALCUserControl ID="NNCotizacionMiVidaValeIIALCUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>