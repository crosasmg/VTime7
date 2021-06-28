<%@ Page Title="Mi Vida vale" Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeIIPopup.aspx.vb" Inherits="NNCotizacionMiVidaValeIIPopup" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaValeIIPageTitleResource"%>

<%@ Register src="NNCotizacionMiVidaValeIIUserControl.ascx" tagname="NNCotizacionMiVidaValeIIUserControl" tagprefix="NNCotizacionMiVidaValeIIUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="NNCotizacionMiVidaValeII.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</head>
<body id="NNCotizacionMiVidaValeIIBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="NNCotizacionMiVidaValeIIUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaValeIIUC:NNCotizacionMiVidaValeIIUserControl ID="NNCotizacionMiVidaValeIIUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>