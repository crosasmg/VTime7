<%@ Page Title="Buscadores" Language="VB" AutoEventWireup="false" CodeFile="BuscadoresPopup.aspx.vb" Inherits="BuscadoresPopup" UICulture="auto" Culture="auto" meta:resourcekey="BuscadoresPageTitleResource"%>

<%@ Register src="BuscadoresUserControl.ascx" tagname="BuscadoresUserControl" tagprefix="BuscadoresUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Buscadores.js" type="text/javascript"></script>
</head>
<body id="BuscadoresBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="BuscadoresUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <BuscadoresUC:BuscadoresUserControl ID="BuscadoresUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>