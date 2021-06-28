<%@ Page Title="SolicitudesAsociadasAGuiaDespacho" Language="VB" AutoEventWireup="false" CodeFile="SolicitudesAsociadasAGuiaDespachoPopup.aspx.vb" Inherits="SolicitudesAsociadasAGuiaDespachoPopup" UICulture="auto" Culture="auto" meta:resourcekey="SolicitudesAsociadasAGuiaDespachoPageTitleResource"%>

<%@ Register src="SolicitudesAsociadasAGuiaDespachoUserControl.ascx" tagname="SolicitudesAsociadasAGuiaDespachoUserControl" tagprefix="SolicitudesAsociadasAGuiaDespachoUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="SolicitudesAsociadasAGuiaDespacho.js" type="text/javascript"></script>
</head>
<body id="SolicitudesAsociadasAGuiaDespachoBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="SolicitudesAsociadasAGuiaDespachoUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <SolicitudesAsociadasAGuiaDespachoUC:SolicitudesAsociadasAGuiaDespachoUserControl ID="SolicitudesAsociadasAGuiaDespachoUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>