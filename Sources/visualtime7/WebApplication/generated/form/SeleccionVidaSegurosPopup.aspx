<%@ Page Title="Selección Seguros de Vida" Language="VB" AutoEventWireup="false" CodeFile="SeleccionVidaSegurosPopup.aspx.vb" Inherits="SeleccionVidaSegurosPopup" UICulture="auto" Culture="auto" meta:resourcekey="SeleccionVidaSegurosPageTitleResource"%>

<%@ Register src="SeleccionVidaSegurosUserControl.ascx" tagname="SeleccionVidaSegurosUserControl" tagprefix="SeleccionVidaSegurosUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="SeleccionVidaSeguros.js" type="text/javascript"></script>
</head>
<body id="SeleccionVidaSegurosBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="SeleccionVidaSegurosUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <SeleccionVidaSegurosUC:SeleccionVidaSegurosUserControl ID="SeleccionVidaSegurosUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>