<%@ Page Title="Mi Vida vale" Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaVale1BasicoPopup.aspx.vb" Inherits="NNCotizacionMiVidaVale1BasicoPopup" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaVale1BasicoPageTitleResource"%>

<%@ Register src="NNCotizacionMiVidaVale1BasicoUserControl.ascx" tagname="NNCotizacionMiVidaVale1BasicoUserControl" tagprefix="NNCotizacionMiVidaVale1BasicoUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="NNCotizacionMiVidaVale1Basico.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</head>
<body id="NNCotizacionMiVidaVale1BasicoBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="NNCotizacionMiVidaVale1BasicoUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaVale1BasicoUC:NNCotizacionMiVidaVale1BasicoUserControl ID="NNCotizacionMiVidaVale1BasicoUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>