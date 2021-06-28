<%@ Page Title="Mi Vida vale - Secuencia" Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeSecuenciaPopup.aspx.vb" Inherits="NNCotizacionMiVidaValeSecuenciaPopup" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaValeSecuenciaPageTitleResource"%>

<%@ Register src="NNCotizacionMiVidaValeSecuenciaUserControl.ascx" tagname="NNCotizacionMiVidaValeSecuenciaUserControl" tagprefix="NNCotizacionMiVidaValeSecuenciaUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="NNCotizacionMiVidaValeSecuencia.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</head>
<body id="NNCotizacionMiVidaValeSecuenciaBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="NNCotizacionMiVidaValeSecuenciaUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaValeSecuenciaUC:NNCotizacionMiVidaValeSecuenciaUserControl ID="NNCotizacionMiVidaValeSecuenciaUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>