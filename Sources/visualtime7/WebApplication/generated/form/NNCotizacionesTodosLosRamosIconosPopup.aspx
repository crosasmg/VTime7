<%@ Page Title="Cotizaciones" Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionesTodosLosRamosIconosPopup.aspx.vb" Inherits="NNCotizacionesTodosLosRamosIconosPopup" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionesTodosLosRamosIconosPageTitleResource"%>

<%--<%@ Register src="NNCotizacionesTodosLosRamosIconosUserControl.ascx" tagname="NNCotizacionesTodosLosRamosIconosUserControl" tagprefix="NNCotizacionesTodosLosRamosIconosUC" %>--%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="NNCotizacionesTodosLosRamosIconos.js" type="text/javascript"></script>
</head>
<body id="NNCotizacionesTodosLosRamosIconosBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="NNCotizacionesTodosLosRamosIconosUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <%--<NNCotizacionesTodosLosRamosIconosUC:NNCotizacionesTodosLosRamosIconosUserControl ID="NNCotizacionesTodosLosRamosIconosUserControl" runat="server" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>