<%@ Page Title="Recepción Guías de Despacho" Language="VB" AutoEventWireup="false" CodeFile="SolicitudesDespachoPopup.aspx.vb" Inherits="SolicitudesDespachoPopup" UICulture="auto" Culture="auto" meta:resourcekey="SolicitudesDespachoPageTitleResource"%>

<%@ Register src="SolicitudesDespachoUserControl.ascx" tagname="SolicitudesDespachoUserControl" tagprefix="SolicitudesDespachoUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="SolicitudesDespacho.js" type="text/javascript"></script>
</head>
<body id="SolicitudesDespachoBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="SolicitudesDespachoUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <SolicitudesDespachoUC:SolicitudesDespachoUserControl ID="SolicitudesDespachoUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>